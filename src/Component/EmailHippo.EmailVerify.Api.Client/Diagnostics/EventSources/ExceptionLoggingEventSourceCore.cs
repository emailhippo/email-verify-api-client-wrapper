// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionLoggingEventSourceCore.cs" company="Email Hippo Ltd">
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
    using System.Diagnostics;
    using System.Diagnostics.Tracing;
    using System.Globalization;

    using EmailHippo.EmailVerify.Api.Client.Diagnostics.Common;

    #endregion

    /// <summary>
    /// The exception logging event source.
    /// </summary>
    [EventSource(Name = SourceNames.ExceptionLogging)]
    public sealed partial class ExceptionLoggingEventSource : EventSource
    {
        #region Constants

        /// <summary>
        ///     The exception trace log template
        /// </summary>
        private const string ExceptionTraceLogTemplate =
            @"Source:{source}{newline}Exception type: {exceptionType}{newline}Timestamp: {timestamp}{newline}Message: {message}{newline}Priority: {priority}{newline}EventId: {eventid}{newline}Severity: {severity}{newline}Title: {title}{newline}Machine: {localMachine}{newline}ProcessId: {localProcessId}{newline}Process Name: {localProcessName}{newline}Stack Trace:{stackTrace}{newline}";

        /// <summary>
        ///     The module name
        /// </summary>
        private const string ModuleName = @"Exceptions";

        #endregion

        #region Static Fields

        /// <summary>
        ///     The instance.
        /// </summary>
        private static readonly Lazy<ExceptionLoggingEventSource> Instance =
            new Lazy<ExceptionLoggingEventSource>(() => new ExceptionLoggingEventSource());

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ExceptionLoggingEventSource"/> class from being created. 
        /// </summary>
        private ExceptionLoggingEventSource()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the log.
        /// </summary>
        public static ExceptionLoggingEventSource Log
        {
            get
            {
                return Instance.Value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The format exception log entry.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <param name="traceEventType">
        /// The trace event type.
        /// </param>
        /// <param name="eventId">
        /// The event id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string FormatExceptionLogEntry(Exception exception, TraceEventType traceEventType, int eventId)
        {
            if (exception == null)
            {
                return string.Empty;
            }

            var formatTemplate = ExceptionTraceLogTemplate.Replace("{newline}", Environment.NewLine);

            var rtn =
                formatTemplate.Replace("{source}", exception.Source)
                    .Replace("{exceptionType}", exception.GetType().ToString())
                    .Replace("{timestamp}", DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture))
                    .Replace("{message}", exception.Message)
                    .Replace("{priority}", traceEventType.ToString())
                    .Replace("{eventid}", eventId.ToString(CultureInfo.InvariantCulture))
                    .Replace("{severity}", traceEventType.ToString())
                    .Replace("{title}", exception.TargetSite == null ? string.Empty : exception.TargetSite.Name)
                    .Replace("{localMachine}", Environment.MachineName)
                    .Replace("{localProcessId}", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture))
                    .Replace("{localProcessName}", Process.GetCurrentProcess().ProcessName)
                    .Replace("{stackTrace}", exception.StackTrace ?? string.Empty);

            return rtn;
        }

        /// <summary>
        /// The critical.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        [Event((int)EventIds.Critical, Message = Messages.Critical, Level = EventLevel.Critical)]
        private void Critical(string module, string source, string errorMessage)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent((int)EventIds.Critical, module, source, errorMessage);
            }
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        [Event((int)EventIds.Error, Message = Messages.Error, Level = EventLevel.Error)]
        private void Error(string module, string source, string errorMessage)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent((int)EventIds.Error, module, source, errorMessage);
            }
        }

        /// <summary>
        /// The information.
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
        [Event((int)EventIds.Informational, Message = Messages.Informational, Level = EventLevel.Informational)]
        private void Information(string module, string source, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent((int)EventIds.Informational, module, source, message);
            }
        }

        /// <summary>
        /// Logs the always.
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
        [Event((int)EventIds.LogAlways, Message = Messages.LogAlways, Level = EventLevel.LogAlways)]
        private void LogAlways(string module, string source, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent((int)EventIds.LogAlways, module, source, message);
            }
        }

        /// <summary>
        /// Verboses the specified module.
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
        [Event((int)EventIds.Verbose, Message = Messages.Verbose, Level = EventLevel.Verbose)]
        private void Verbose(string module, string source, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent((int)EventIds.Verbose, module, source, message);
            }
        }

        /// <summary>
        /// The warning.
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
        [Event((int)EventIds.Warning, Message = Messages.Warning, Level = EventLevel.Warning)]
        private void Warning(string module, string source, string message)
        {
            if (this.IsEnabled())
            {
                this.WriteEvent((int)EventIds.Warning, module, source, message);
            }
        }

        #endregion
    }
}