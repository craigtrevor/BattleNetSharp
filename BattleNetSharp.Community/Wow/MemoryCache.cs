﻿// Copyright (C) 2011 by Sherif Elmetainy (Grendiser@Kazzak-EU)
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

using System.Collections.Generic;

namespace BattleNetSharp.Community.Wow
{
    /// <summary>
    ///   A memory cache (for objects that are cached permanently, such as race information)
    /// </summary>
    /// <typeparam name="TKey"> Key type </typeparam>
    /// <typeparam name="TValue"> Value type </typeparam>
    /// <remarks>
    ///   This class is thread safe
    /// </remarks>
    internal class MemoryCache<TKey, TValue> where TValue : class
    {
        /// <summary>
        ///   Dictionary for the cache
        /// </summary>
        private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        /// <summary>
        ///   Reader writer lock to synchronize access to this object.
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        ///   Looks up value in cache, and calls cacheMissFunction to retrieve it in case of a cache miss
        /// </summary>
        /// <param name="key"> key to lookup </param>
        /// <returns> The value </returns>
        public TValue LookupValue(TKey key)
        {
            // read-only lock
            lock (_lock)
            {
                TValue value;
                _dictionary.TryGetValue(key, out value);
                return value;
            }
        }

        /// <summary>
        ///   Adds a value to the cache
        /// </summary>
        /// <param name="key"> key to add </param>
        /// <param name="value"> the value to add </param>
        public void AddValue(TKey key, TValue value)
        {
            // read-only lock
            lock (_lock)
            {
                if (!_dictionary.ContainsKey(key))
                {
                    _dictionary.Add(key, value);
                }
            }
        }
    }
}
