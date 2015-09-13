// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeLinq.cs" company="Email Hippo Ltd">
//   © Email Hippo Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region License

// Copyright 2015 Email Hippo Ltd
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace EmailHippo.EmailVerify.Api.Client.Helpers
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    ///     Safe LINQ extensions.
    /// </summary>
    internal static class SafeLinq
    {
        #region Public Methods and Operators

        /// <summary>
        /// The any safe.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// Type of parameter.
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AnySafe<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        /// <summary>
        /// Any with predicate (safe).
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AnySafe<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) where T : class
        {
            return enumerable != null && enumerable.Any(predicate);
        }

        /// <summary>
        /// The count safe.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int CountSafe<T>(this IEnumerable<T> source)
        {
            return source == null ? 0 : source.Count();
        }

        /// <summary>
        /// The count safe.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int CountSafe<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) where T : class
        {
            return enumerable == null ? 0 : enumerable.Count(predicate);
        }

        /// <summary>
        /// The to safe enumerable.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> ToSafeEnumerable<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// The to safe list.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public static IList<T> ToSafeList<T>(this IList<T> source)
        {
            return source ?? Enumerable.Empty<T>().ToList();
        }

        #endregion
    }
}