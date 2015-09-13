// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityLoggingEventSourceCore.cs" company="Email Hippo Ltd">
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

    using System;
    using System.Diagnostics.Tracing;
    
    using EmailHippo.EmailVerify.Api.Client.Diagnostics.Common;

    #endregion

    /// <summary>
    ///     The activity logging event source.
    /// </summary>
    [EventSource(Name = SourceNames.ActivityLogging)]
    public sealed partial class ActivityLoggingEventSource : EventSource
    {
        #region Static Fields

        /// <summary>
        ///     The instance.
        /// </summary>
        private static readonly Lazy<ActivityLoggingEventSource> Instance =
            new Lazy<ActivityLoggingEventSource>(() => new ActivityLoggingEventSource());

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="ActivityLoggingEventSource" /> class from being created.
        /// </summary>
        private ActivityLoggingEventSource()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the log.
        /// </summary>
        public static ActivityLoggingEventSource Log
        {
            get
            {
                return Instance.Value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Warnings the specified module.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [Event((int)EventIds.Initialized, Message = Messages.Initialized, Level = EventLevel.Informational, 
            Keywords = Keywords.GeneralDiagnostic)]
        internal void Initialized(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.Initialized, module, source);
            }
        }

        /// <summary>
        /// The initializing.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [Event((int)EventIds.Initializing, Message = Messages.Initializing, Level = EventLevel.Informational, 
            Keywords = Keywords.GeneralDiagnostic)]
        internal void Initializing(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.Initializing, module, source);
            }
        }

        /// <summary>
        /// The method enter.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [Event((int)EventIds.MethodEnter, Message = Messages.MethodEnter, Level = EventLevel.Informational, 
            Keywords = Keywords.GeneralDiagnostic)]
        internal void MethodEnter(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.MethodEnter, module, source);
            }
        }

        /// <summary>
        /// The method exit.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [Event((int)EventIds.MethodExit, Message = Messages.MethodExit, Level = EventLevel.Informational, 
            Keywords = Keywords.GeneralDiagnostic)]
        internal void MethodExit(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.MethodExit, module, source);
            }
        }

        /// <summary>
        /// The timer logging.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="elapsed">
        /// The elapsed milliseconds.
        /// </param>
        [Event((int)EventIds.TimerLogging, Message = Messages.TimerLogging, 
            Level = EventLevel.Informational, 
            Keywords = Keywords.Perf)]
        internal void TimerLogging(string module, string source, long elapsed)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Perf))
            {
                this.WriteEvent((int)EventIds.TimerLogging, module, source, elapsed);
            }
        }

        /// <summary>
        /// HTTPs the get request logging.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="source">The source.</param>
        /// <param name="url">The URL.</param>
        [Event((int)EventIds.HttpGetRequest, 
            Message = Messages.HttpGetRequest, 
            Level = EventLevel.Informational, 
            Keywords = Keywords.Io)]
        internal void HttpGetRequestLogging(string module, string source, string url)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Io))
            {
                this.WriteEvent((int)EventIds.HttpGetRequest,module, source, url);
            }
        }

        [Event((int)EventIds.RestResponse, 
            Message = Messages.RestResponse,
            Level = EventLevel.Informational,
            Keywords = Keywords.Io)]
        internal void RestResponseLogging(string module, string source, string restResponseString)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Io))
            {
                this.WriteEvent((int)EventIds.RestResponse, module, source, restResponseString);
            }
        }

        #endregion

        /// <summary>
        ///     The keywords.
        /// </summary>
        public sealed class Keywords
        {
            #region Constants

            /// <summary>
            ///     The general diagnostic.
            /// </summary>
            public const EventKeywords GeneralDiagnostic = (EventKeywords)1;

            /// <summary>
            /// The IO keyword.
            /// </summary>
            public const EventKeywords Io = (EventKeywords)2;

            /// <summary>
            ///     The performance keyword.
            /// </summary>
            public const EventKeywords Perf = (EventKeywords)4;

            #endregion
        }
    }
}