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

namespace BattleNetSharp.Community.Wow
{
    /// <summary>
    ///   PvP zone Status enumeration
    /// </summary>
    public enum PvpZoneStatus
    {
        /// <summary>
        ///   Unknown status
        /// </summary>
        Unknown = -1,

        /// <summary>
        ///   Idle (There is no battle atm)
        /// </summary>
        Idle = 0,

        /// <summary>
        ///   Populating (The battle is about to start)
        /// </summary>
        Populating = 1,

        /// <summary>
        ///   Active (there is a battle atm, Go HORDE)
        /// </summary>
        Active = 2,

        /// <summary>
        ///   The battle has just concluded
        /// </summary>
        Concluded = 3
    }
}