// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityLoggingEventSource.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources
{
    #region Usings

    using System.Diagnostics;
    using System.Diagnostics.Tracing;

    using EmailHippo.EmailVerify.Api.Client.Diagnostics.Common;

    #endregion

    /// <summary>
    ///     The activity logging event source.
    /// </summary>
    public sealed partial class ActivityLoggingEventSource
    {
        #region Constants

        /// <summary>
        ///     The module name.
        /// </summary>
        private const string ModuleNameCommand = @"Activity";

        #endregion

        #region Static Fields

        /// <summary>
        ///     The trace source
        /// </summary>
        private static readonly TraceSource TraceSource = new TraceSource(SourceNames.ActivityLogging);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The initialized.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        public void Initialized(string source)
        {
            TraceSource.TraceEvent(
                TraceEventType.Information, 
                (int)EventIds.Initialized, 
                Messages.Initialized, 
                ModuleNameCommand);

            this.Initialized(ModuleNameCommand, source);
        }

        /// <summary>
        /// The initializing.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        public void Initializing(string source)
        {
            TraceSource.TraceEvent(
                TraceEventType.Information, 
                (int)EventIds.Initializing, 
                Messages.Initializing, 
                ModuleNameCommand);

            this.Initializing(ModuleNameCommand, source);
        }

        /// <summary>
        /// The method enter.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        [NonEvent]
        public void MethodEnter(string source)
        {
            TraceSource.TraceEvent(
                TraceEventType.Information, 
                (int)EventIds.MethodEnter, 
                Messages.MethodEnter, 
                ModuleNameCommand, 
                source);

            this.MethodEnter(ModuleNameCommand, source);
        }

        /// <summary>
        /// The method exit.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        [NonEvent]
        public void MethodExit(string source)
        {
            TraceSource.TraceEvent(
                TraceEventType.Information, 
                (int)EventIds.MethodExit, 
                Messages.MethodExit, 
                ModuleNameCommand, 
                source);

            this.MethodExit(ModuleNameCommand, ModuleNameCommand);
        }

        /// <summary>
        /// The timer logging.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="elapsed">
        /// The elapsed.
        /// </param>
        [NonEvent]
        public void TimerLogging(string source, long elapsed)
        {
            TraceSource.TraceEvent(
                TraceEventType.Information, 
                (int)EventIds.TimerLogging, 
                Messages.TimerLogging, 
                ModuleNameCommand, 
                source, 
                elapsed);

            this.TimerLogging(ModuleNameCommand, source, elapsed);
        }

        /// <summary>
        /// HTTPs the get request logging.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="url">The URL.</param>
        [NonEvent]
        public void HttpGetRequestLogging(string source, string url)
        {
            TraceSource.TraceEvent(
                TraceEventType.Information,
                (int)EventIds.HttpGetRequest,
                Messages.HttpGetRequest,
                ModuleNameCommand,
                source,
                url);

            this.HttpGetRequestLogging(ModuleNameCommand, source, url);
        }

        /// <summary>
        /// Rests the response logging.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="restResponseString">The rest response string.</param>
        [NonEvent]
        internal void RestResponseLogging(string source, string restResponseString)
        {
            TraceSource.TraceEvent(
                TraceEventType.Information,
                (int)EventIds.RestResponse,
                Messages.RestResponse,
                ModuleNameCommand,
                source,
                restResponseString);

            this.RestResponseLogging(ModuleNameCommand, source, restResponseString);
        }

        #endregion
    }
}