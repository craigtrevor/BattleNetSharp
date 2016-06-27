// Copyright (C) 2011 by Sherif Elmetainy (Grendiser@Kazzak-EU)
// Copyright (C) 2016 by Craig Trevor
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BattleNetSharp.Community
{
    /// <summary>
    ///   A Blizzard's battle.net community API client
    /// </summary>
    public abstract class ApiClient
    {

        /// <summary>
        ///   An object that implements _cacheManager
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        ///   Reference date for Unix time
        /// </summary>
        private static readonly DateTime _unixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        protected ApiClient(Region region, string locale)
        {
            if (region == null)
                region = Region.Default;
            Region = region;
            Locale = Region.GetSupportedLocale(locale);
        }

        protected ApiClient(Region region, string locale, ICacheManager cacheManager)
        {
            if (region == null)
                region = Region.Default;
            Region = region;
            Locale = Region.GetSupportedLocale(locale);
            _cacheManager = cacheManager;
        }

        protected ApiClient(Region region, string publicKey, string locale)
            : this(region, locale)
        {

        }

        protected ApiClient(Region region, string publicKey, string locale, ICacheManager cacheManager)
            : this(region, locale, cacheManager)
        {

        }

        /// <summary>
        ///   Gets the region to which this ApiClient connects
        /// </summary>
        public Region Region { get; private set; }

        /// <summary>
        ///   Gets the locale which is used to get item names
        /// </summary>
        public string Locale { get; private set; }

        /// <summary>
        /// Causes the API to throw serialization exceptions when 
        /// the JSON returned by Blizzard's API contains a property 
        /// not found in the class being deserialized. 
        /// This is useful to detect changes by Blizzard API since they have the habit of changing 
        /// things without announcing it. 
        /// This property is set to true by unit tests
        /// </summary>
        public static bool TestMode { get; set; }

        /// <summary>
        /// Performs Http Get request asynchronously
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="path">relative URL of the object to get</param>
        /// <param name="objectToRefresh">object to refresh</param>
        /// <returns>A task object for the async HTTP get</returns>
        internal async Task<T> GetAsync<T>(string path, T objectToRefresh) where T : class
        {
            var objResult = await GetAsync(path, typeof(T), objectToRefresh);
            return (T)objResult;
        }

        /// <summary>
        /// Performs Http Get request asynchronously
        /// </summary>
        /// <param name="path">relative URL of the object to get</param>
        /// <param name="objectType">object type</param>
        /// <param name="objectToRefresh">object to refresh</param>
        /// <returns>A task object for the async HTTP get</returns>
        internal async Task<object> GetAsync(string path, Type objectType, object objectToRefresh)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (_cacheManager != null && objectToRefresh == null)
            {
                var cachedObject = await GetObjectFromCacheAsync(path);
                if (cachedObject != null)
                {
                    return cachedObject;
                }
            }
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var client = new HttpClient(handler))
            {
                var objectApiResponse = objectToRefresh as ApiResponse;
                if (objectApiResponse != null && path == objectApiResponse.Path &&
                    objectApiResponse.LastModifiedUtc != DateTime.MinValue)
                {
                    client.DefaultRequestHeaders.IfModifiedSince = objectApiResponse.LastModifiedUtc;
                }
                string uri;
                if (path.EndsWith(".json"))
                {
                    uri = path;
                }
                else
                {
                    uri = "https://" + Region.Host + path;
                }
                var responseMessage = await client.GetAsync(uri);
                if (responseMessage.StatusCode == HttpStatusCode.NotModified)
                {
                    return objectToRefresh;
                }
                else if (responseMessage.IsSuccessStatusCode)
                {
                    object obj;
                    if (typeof(ApiResponse).IsAssignableFrom(objectType))
                    {
                        obj = await DeserializeResponse(path, objectType, responseMessage);
                    }
                    else
                    {
                        obj = await responseMessage.Content.ReadAsByteArrayAsync();
                    }
                    if (_cacheManager != null)
                    {
                        try
                        {
                            await _cacheManager.AddDataAsync(Region.Name
                                                             + "/" + Locale + "/" + path, obj);
                        }
                        catch (CacheManagerException)
                        {
                            // if we failed to add item to cache, swallow and return normally
                        }
                    }
                    return obj;
                }
                else
                {
                    ApiError apiError = null;
                    try
                    {
                        apiError = (ApiError)await DeserializeResponse(path, typeof(ApiError), responseMessage);
                    }
                    catch (JsonException)
                    {
                        // Failed to deserialize error
                        apiError = null;
                    }
                    throw new ApiException(apiError, responseMessage.StatusCode, null);
                }
            }
        }


        /// <summary>
        /// Deserializers
        /// </summary>
        /// <param name="path"></param>
        /// <param name="objectType"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private async Task<object> DeserializeResponse(string path, Type objectType, HttpResponseMessage responseMessage)
        {
            var responseStream = await responseMessage.Content.ReadAsStreamAsync();
            try
            {
                var textReader = new StreamReader(responseStream);
                // textReader now owns responseStream and will dispose it
                // Make sure that responseStream.Dispose is not called more than once
                // by setting responseStream to null
                responseStream = null;
                try
                {
                    var serializer = CreateJsonSerializer(responseMessage);
                    using (var jsonReader = new JsonTextReader(textReader))
                    {
                        // jsonReader now owns textReader and will dispose it.
                        // Make sure that responseStream.Dispose is not called more than once 
                        // by setting textReader to null
                        textReader = null;
                        ApiResponse apiResponseResult;

                        // Deserialization is a CPU bound operation
                        // But since some calls return big data (such as auction dump api)
                        // Serialization can take a long time
                        // therefore make it a separate task so that it doesn't block main thread
                        var deserializeTask =
                            new Task<ApiResponse>(() => (ApiResponse)serializer.Deserialize(jsonReader, objectType));
                        deserializeTask.Start();
                        apiResponseResult = await deserializeTask;

                        // Post serialization
                        apiResponseResult.Path = path;
                        return apiResponseResult;
                    }
                }
                finally
                {
                    if (textReader != null)
                        textReader.Dispose();
                }
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Dispose();
            }
        }

        /// <summary>
        /// Creates a JSON serializer to serialize objects
        /// </summary>
        /// <param name="responseMessage">Http response message</param>
        /// <returns>Json serializer</returns>
        internal virtual JsonSerializer CreateJsonSerializer(HttpResponseMessage responseMessage)
        {
            // Store information about serialization context 
            // So that JsonApiResponseConverter would correctly 
            // populate ApiClient property for ApiResponse object
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JsonApiResponseConverter(this, responseMessage));

            // This is needed for Testing to make sure that new data added by blizzard is not missed
            // Blizzard has the habit of adding new things to the API without an announcement
            // And without a change of documentation
            if (TestMode)
            {
                serializer.MissingMemberHandling = MissingMemberHandling.Error;
            }
            return serializer;
        }

        /// <summary>
        /// Tries to fetch an object from cache
        /// </summary>
        /// <param name="path">object path</param>
        /// <returns>The cached object if found or null otherwise.</returns>
        private async Task<object> GetObjectFromCacheAsync(string path)
        {
            object cachedObject = null;
            try
            {
                cachedObject = await _cacheManager.LookupDataAsync(Region.Name
                                                                   + "/" + Locale + "/" + path);
            }
            catch (CacheManagerException)
            {
                // if cacheManager fail swallow and continue without caching
                return null;
            }
            return cachedObject;
        }

        /// <summary>
        ///   Gets the Utc DateTime object from the Unix time returned by the API
        /// </summary>
        /// <param name="value"> time value returned by API </param>
        /// <returns> Utc time object </returns>
        internal static DateTime GetUtcDateFromUnixTimeMilliseconds(long value)
        {
            try
            {
                return _unixStartDate.AddMilliseconds(value);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///   Gets the Unit time value from the Date time object
        /// </summary>
        /// <param name="date"> date time object </param>
        /// <returns> Unix time value </returns>
        internal static long GetUnixTimeFromDateMilliseconds(DateTime date)
        {
            return (long)Math.Round((date - _unixStartDate).TotalMilliseconds, 0);
        }

        /// <summary>
        ///   Gets the Utc DateTime object from the Unix time returned by the API
        /// </summary>
        /// <param name="value"> time value returned by API </param>
        /// <returns> Utc time object </returns>
        internal static DateTime GetUtcDateFromUnixTimeSeconds(long value)
        {
            try
            {
                return _unixStartDate.AddSeconds(value);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///   Gets the Unit time value from the Date time object
        /// </summary>
        /// <param name="date"> date time object </param>
        /// <returns> Unix time value </returns>
        internal static long GetUnixTimeFromDateSeconds(DateTime date)
        {
            return (long)Math.Round((date - _unixStartDate).TotalSeconds, 0);
        }

    }
}