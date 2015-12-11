// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultClient.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Logic.Clients.EmailHippo.V2
{
    #region Usings

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using global::EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources;
    using global::EmailHippo.EmailVerify.Api.Client.Entities.Clients.V2;

    using global::EmailHippo.EmailVerify.Api.Client.Entities.Configuration.V2;
    using global::EmailHippo.EmailVerify.Api.Client.Helpers;
    using global::EmailHippo.EmailVerify.Api.Client.Interfaces.Clients;

    using global::EmailHippo.EmailVerify.Api.Client.Interfaces.Configuration;

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// The default client.
    /// </summary>
    internal sealed class DefaultClient : IClientProxy<VerificationRequest, VerificationResponse>
    {
        /// <summary>
        /// The API url.
        /// </summary>
        private const string ApiUrl = @"https://api1.27hub.com/api/emh/a/v2";

        /// <summary>
        /// 0 = API URL
        /// 1 = Email address to query
        /// 2 = API Key
        /// </summary>
        private const string QueryFormatString = @"{0}?e={1}&k={2}";

        #region Fields

        /// <summary>
        /// The AUTH configuration.
        /// </summary>
        private readonly IConfiguration<KeyAuthentication> authConfiguration;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultClient"/> class.
        /// </summary>
        /// <param name="authConfiguration">
        /// The authentication configuration.
        /// </param>
        public DefaultClient(IConfiguration<KeyAuthentication> authConfiguration)
        {
            this.authConfiguration = authConfiguration;
        }

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
        public VerificationResponse Process(VerificationRequest request)
        {
            ActivityLoggingEventSource.Log.MethodEnter(@"DefaultClient.Process");

            try
            {
                request.Validate();
            }
            catch (ValidationException exception)
            {
                ExceptionLoggingEventSource.Log.Error(exception);
                throw;
            }

            var requestUrl = string.Format(QueryFormatString, ApiUrl, request.Email, this.authConfiguration.Get.LicenseKey);

            ActivityLoggingEventSource.Log.HttpGetRequestLogging(@"DefaultClient.Process", requestUrl);

            var stopwatch = Stopwatch.StartNew();

            var myRequest = (HttpWebRequest)WebRequest.Create(requestUrl);

            WebResponse webResponse = null;

            string jsonString = null;

            try
            {
                webResponse = myRequest.GetResponse();

                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)webException.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            var errorString = reader.ReadToEnd();

                            ExceptionLoggingEventSource.Log.Error(errorString);
                            
                            throw;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionLoggingEventSource.Log.Error(exception);
                
                throw;
            }
            finally
            {
                stopwatch.Stop();

                if (webResponse != null)
                {
                    webResponse.Dispose();
                }
            }

            ActivityLoggingEventSource.Log.RestResponseLogging(@"DefaultClient.Process", jsonString);
            var verificationResponse = JsonConvert.DeserializeObject<VerificationResponse>(jsonString);

            ActivityLoggingEventSource.Log.TimerLogging(
                "DefaultClient.Process", 
                stopwatch.ElapsedMilliseconds);
            ActivityLoggingEventSource.Log.MethodExit("DefaultClient.Process");

            return verificationResponse;
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
        public async Task<VerificationResponse> ProcessAsync(VerificationRequest request, CancellationToken cancellationToken)
        {
            ActivityLoggingEventSource.Log.MethodEnter(@"DefaultClient.ProcessAsync");

            try
            {
                request.Validate();
            }
            catch (ValidationException exception)
            {
                ExceptionLoggingEventSource.Log.Error(exception);
                throw;
            }

            var requestUrl = string.Format(QueryFormatString, ApiUrl, request.Email, this.authConfiguration.Get.LicenseKey);

            ActivityLoggingEventSource.Log.HttpGetRequestLogging(@"DefaultClient.ProcessAsync", requestUrl);

            var stopwatch = Stopwatch.StartNew();
            
            var myRequest = (HttpWebRequest)WebRequest.Create(requestUrl);

            WebResponse webResponse = null;

            string jsonString = null;

            try
            {
                webResponse = await myRequest.GetResponseAsync();

                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)webException.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            var errorString = reader.ReadToEnd();

                            ExceptionLoggingEventSource.Log.Error(errorString);

                            throw;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionLoggingEventSource.Log.Error(exception);

                throw;
            }
            finally
            {
                stopwatch.Stop();

                if (webResponse != null)
                {
                    webResponse.Dispose();
                }
            }

            ActivityLoggingEventSource.Log.RestResponseLogging(@"DefaultClient.ProcessAsync", jsonString);
            var verificationResponse = JsonConvert.DeserializeObject<VerificationResponse>(jsonString);

            ActivityLoggingEventSource.Log.TimerLogging(
                "DefaultClient.ProcessAsync",
                stopwatch.ElapsedMilliseconds);
            ActivityLoggingEventSource.Log.MethodExit("DefaultClient.ProcessAsync");

            return verificationResponse;
        }

        #endregion
    }
}