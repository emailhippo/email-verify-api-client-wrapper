﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultService.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Services.EmailHippo.V2
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    using global::EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources;

    using global::EmailHippo.EmailVerify.Api.Client.Entities.Service.V2;

    using global::EmailHippo.EmailVerify.Api.Client.Helpers;

    using global::EmailHippo.EmailVerify.Api.Client.Interfaces.Clients;

    using global::EmailHippo.EmailVerify.Api.Client.Interfaces.Service;

    #endregion

    /// <summary>
    ///     The default service.
    /// </summary>
    internal sealed class DefaultService : IService<VerificationRequest, VerificationResponses, ProgressEventArgs>
    {
        #region Fields

        /// <summary>
        ///     The client proxy.
        /// </summary>
        private readonly
            IClientProxy<Entities.Clients.V2.VerificationRequest, Entities.Clients.V2.VerificationResponse> clientProxy;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultService"/> class.
        /// </summary>
        /// <param name="clientProxy">
        /// The client proxy.
        /// </param>
        internal DefaultService(
            IClientProxy<Entities.Clients.V2.VerificationRequest, Entities.Clients.V2.VerificationResponse> clientProxy)
        {
            if (clientProxy == null)
            {
                throw new ArgumentNullException("clientProxy");
            }

            this.clientProxy = clientProxy;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     The progress changed.
        /// </summary>
        public event EventHandler<ProgressEventArgs> ProgressChanged;
        
        #endregion
        
        #region Public Methods and Operators

        /// <summary>
        /// The process.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="VerificationResponse"/>.
        /// </returns>
        public VerificationResponses Process(VerificationRequest request)
        {
            ActivityLoggingEventSource.Log.MethodEnter("DefaultService.Process");

            try
            {
                request.Validate();
            }
            catch (ValidationException exception)
            {
                ExceptionLoggingEventSource.Log.Error(exception);
                throw;
            }

            var stopwatch = Stopwatch.StartNew();

            VerificationResponses processLocalAsync = null;

            try
            {
                processLocalAsync = this.ProcessLocalAsync(request.Emails.ToSafeEnumerable().ToList(), CancellationToken.None).Result;
            }
            catch (Exception exception)
            {
                ExceptionLoggingEventSource.Log.Critical(exception);
            }
            finally
            {
                stopwatch.Stop();
            }

            ActivityLoggingEventSource.Log.TimerLogging("DefaultService.Process", stopwatch.ElapsedMilliseconds);
            ActivityLoggingEventSource.Log.MethodExit("DefaultService.Process");

            return processLocalAsync;
        }

        /// <summary>
        /// The process async.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<VerificationResponses> ProcessAsync(
            VerificationRequest request, 
            CancellationToken cancellationToken)
        {
            ActivityLoggingEventSource.Log.MethodEnter("DefaultService.ProcessAsync");

            try
            {
                request.Validate();
            }
            catch (ValidationException exception)
            {
                ExceptionLoggingEventSource.Log.Error(exception);
                throw;
            }

            var stopwatch = Stopwatch.StartNew();

            VerificationResponses processLocalAsync = null;

            try
            {
                processLocalAsync =
                    await this.ProcessLocalAsync(request.Emails.ToSafeEnumerable().ToList(), cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                ExceptionLoggingEventSource.Log.Critical(exception);
            }
            finally
            {
                stopwatch.Stop();
            }

            ActivityLoggingEventSource.Log.TimerLogging("DefaultService.ProcessAsync", stopwatch.ElapsedMilliseconds);
            ActivityLoggingEventSource.Log.MethodExit("DefaultService.ProcessAsync");

            return processLocalAsync;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the percentage progress.
        /// </summary>
        /// <param name="currentCountDone">The current count done.</param>
        /// <param name="myTotalCount">My total count.</param>
        /// <returns>Percentage progress.</returns>
        internal static int CalculatePercentageProgress(int currentCountDone, int myTotalCount)
        {
            if (myTotalCount == 0 
                || currentCountDone == 0)
            {
                return 0;
            }

            if (currentCountDone >= myTotalCount)
            {
                return 100;
            }

            var d = Convert.ToDouble(currentCountDone, CultureInfo.InvariantCulture);

            var d1 = Convert.ToDouble(myTotalCount, CultureInfo.InvariantCulture);
            
            var d2 = (d / d1) * 100;

            return (int)d2;
        }

        /// <summary>
        /// Processes the local asynchronous.
        /// </summary>
        /// <param name="emails">
        /// The emails.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        internal async Task<VerificationResponses> ProcessLocalAsync(
            IEnumerable<string> emails, 
            CancellationToken cancellationToken)
        {
            
            var processingList = emails.ToSafeEnumerable().ToList();

            var totalCount = processingList.Count;

            var queue = new BatchBlock<string>(5);

            Produce(queue, processingList);

            var consumeAsync = this.ConsumeAsync(queue, totalCount, cancellationToken);

            await Task.WhenAll(consumeAsync, queue.Completion);

            var verificationResponses = await consumeAsync.ConfigureAwait(false);

            return verificationResponses;
        }

        /// <summary>
        /// Raises the <see cref="E:ProgressChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ProgressEventArgs"/> instance containing the event data.</param>
        private void OnProgressChanged(ProgressEventArgs e)
        {
            var handler = this.ProgressChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Produces the specified queue.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="values">The values.</param>
        private static void Produce(ITargetBlock<string> queue, IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                queue.Post(value);
            }

            queue.Complete();
        }

        /// <summary>
        /// Consumes the asynchronous.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task<VerificationResponses> ConsumeAsync(BatchBlock<string> queue, int totalCount, CancellationToken cancellationToken)
        {
            var rtnBuilder = new List<VerificationResponse>();

            var currentIndexCounter = 0;
            Interlocked.Exchange(ref currentIndexCounter, 0);

            while (await queue.OutputAvailableAsync(cancellationToken).ConfigureAwait(false))
            {
                Entities.Clients.V2.VerificationResponse verificationResponse = null;

                IEnumerable<string> emails = await queue.ReceiveAsync(cancellationToken).ConfigureAwait(false);

                await Task.Delay(500, cancellationToken).ConfigureAwait(false);

                if (emails == null || !emails.Any())
                {
                    continue;
                }

                var list = emails.ToList();

                foreach (var email in list)
                {
                    try
                    {
                        verificationResponse =
                            await
                            this.clientProxy.ProcessAsync(
                                new Entities.Clients.V2.VerificationRequest { Email = email },
                                cancellationToken).ConfigureAwait(false);
                    }
                    catch (AggregateException aggregateException)
                    {
                        aggregateException.Handle(
                            ae =>
                                {
                                    ExceptionLoggingEventSource.Log.Error(ae);
                                    return false;
                                });
                    }
                    catch (Exception exception)
                    {
                        ExceptionLoggingEventSource.Log.Error(exception);
                    }

                    if (verificationResponse != null)
                    {
                        var response = new VerificationResponse
                                           {
                                               Domain = verificationResponse.Domain,
                                               Duration = verificationResponse.Duration,
                                               Email = verificationResponse.Email,
                                               IsDisposable = verificationResponse.IsDisposable,
                                               IsFree = verificationResponse.IsFree,
                                               IsRole = verificationResponse.IsRole,
                                               MailServerLocation = verificationResponse.MailServerLocation,
                                               Reason = verificationResponse.Reason,
                                               Result = verificationResponse.Result,
                                               User = verificationResponse.User
                                           };

                        rtnBuilder.Add(response);
                        Interlocked.Increment(ref currentIndexCounter);

                        /*Progress calculations are meaningless for parallel processing therefore set to zero. In parallel mode, event will still return response*/
                        var i = CalculatePercentageProgress(currentIndexCounter, totalCount);

                        this.OnProgressChanged(new ProgressEventArgs(totalCount, i, response));
                    }
                    else
                    {
                        ExceptionLoggingEventSource.Log.Warning(
                            "DefaultService.ProcessLocalAsync verificationResponse is null!");
                    }        
                }
            }

            var readOnlyCollection = new ReadOnlyCollection<VerificationResponse>(rtnBuilder);

            return new VerificationResponses { Results = readOnlyCollection };
        }

        #endregion
    }
}