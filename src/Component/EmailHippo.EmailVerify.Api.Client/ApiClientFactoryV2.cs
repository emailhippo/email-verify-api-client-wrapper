// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiClientFactoryV2.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client
{
    #region Usings

    using System;

    using EmailHippo.EmailVerify.Api.Client.Entities.Service.V2;
    using EmailHippo.EmailVerify.Api.Client.Interfaces.Service;
    using EmailHippo.EmailVerify.Api.Client.Logic.Clients.EmailHippo.V2;
    using EmailHippo.EmailVerify.Api.Client.Logic.Configuration.V2;
    using EmailHippo.EmailVerify.Api.Client.Services.EmailHippo.V2;

    #endregion

    /// <summary>
    /// The API client factory for V2 endpoint.
    /// </summary>
    public static class ApiClientFactoryV2
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates the specified license key.
        /// </summary>
        /// <param name="licenseKey">
        /// The license key.
        /// </param>
        /// <returns>
        /// The <see cref="IService"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// licenseKey is null
        /// </exception>
        public static IService<VerificationRequest, VerificationResponses, ProgressEventArgs> Create(string licenseKey)
        {
            if (string.IsNullOrWhiteSpace(licenseKey))
            {
                throw new ArgumentNullException("licenseKey");
            }

            return
                new DefaultService(
                    new DefaultClient(
                        new KeyAuthentication
                            {
                                Get =
                                    new Entities.Configuration.V2.KeyAuthentication
                                        {
                                            LicenseKey =
                                                licenseKey
                                        }
                            }));
        }

        #endregion
    }
}