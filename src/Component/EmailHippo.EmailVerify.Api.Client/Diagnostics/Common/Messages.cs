// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Messages.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Diagnostics.Common
{
    /// <summary>
    ///     The messages.
    /// </summary>
    internal static class Messages
    {
        #region Constants

        /// <summary>
        ///     The critical
        /// </summary>
        public const string Critical = @"Critical. Module:{0}, Source:{1}, Error:{2}";

        /// <summary>
        ///     The error
        /// </summary>
        public const string Error = @"Error. Module:{0}, Source:{1}, Error:{2}";

        /// <summary>
        ///     The informational
        /// </summary>
        public const string Informational = @"Informational. Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        ///     The initialized.
        /// </summary>
        public const string Initialized = @"Initialization complete. Module:{0}";

        /// <summary>
        ///     The initializing.
        /// </summary>
        public const string Initializing = @"Initializing. Module:{0}";

        /// <summary>
        ///     The log always
        /// </summary>
        public const string LogAlways = @"LogAlways: Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        ///     The method enter
        /// </summary>
        public const string MethodEnter = @"MethodEnter. Module:{0}, Source:{1}";

        /// <summary>
        ///     The method exit
        /// </summary>
        public const string MethodExit = @"MethodExit. Module:{0}, Source:{1}";

        /// <summary>
        ///     The timer logging
        /// </summary>
        public const string TimerLogging = @"Timer logging. Module:{0}, Source:{1}, Elapsed:{2}ms";

        /// <summary>
        ///     The verbose
        /// </summary>
        public const string Verbose = @"Verbose. Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        ///     The warning.
        /// </summary>
        public const string Warning = @"Warning. Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        /// The HTTP get request.
        /// </summary>
        public const string HttpGetRequest = @"HttpGetRequest. Module:{0}, Source:{1}, Url:{2}";

        /// <summary>
        /// The REST response
        /// </summary>
        public const string RestResponse = @"RestResponse. Module:{0}, Source:{1}, RestResponse:{2}";

        #endregion
    }
}