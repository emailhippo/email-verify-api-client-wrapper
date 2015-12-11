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
    using System.Configuration;
    using System.Threading;

    using EmailHippo.EmailVerify.Api.Client.Entities.Service.V2;
    using EmailHippo.EmailVerify.Api.Client.Interfaces.Service;
    using EmailHippo.EmailVerify.Api.Client.Logic.Clients.EmailHippo.V2;
    using EmailHippo.EmailVerify.Api.Client.Logic.Configuration.V2;
    using EmailHippo.EmailVerify.Api.Client.Services.EmailHippo.V2;

    #endregion

    /// <summary>
    ///     The API client factory for V2 endpoint.
    /// </summary>
    public static class ApiClientFactoryV2
    {
        #region Static Fields

        /// <summary>
        ///     The default client lazy.
        /// </summary>
        private static readonly Lazy<DefaultClient> DefaultClientLazy =
            new Lazy<DefaultClient>(() => new DefaultClient(KeyAuthenticationLazy.Value));

        /// <summary>
        ///     The default service lazy.
        /// </summary>
        private static readonly Lazy<DefaultService> DefaultServiceLazy =
            new Lazy<DefaultService>(() => new DefaultService(DefaultClientLazy.Value));

        /// <summary>
        ///     The key authentication lazy.
        /// </summary>
        private static readonly Lazy<KeyAuthentication> KeyAuthenticationLazy =
            new Lazy<KeyAuthentication>(
                () =>
                new KeyAuthentication
                    {
                        Get =
                            new Entities.Configuration.V2.KeyAuthentication
                                {
                                    LicenseKey =
                                        appDomainLicenseKey
                                }
                    });

        /// <summary>
        ///     The app domain license key.
        /// </summary>
        private static string appDomainLicenseKey;

        /// <summary>
        ///     The initialized.
        /// </summary>
        private static long initialized;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Creates the specified license key.
        /// </summary>
        /// <returns>
        ///     The <see cref="IService" />.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     License key not set. Call Initialize method first and either add key
        ///     to appSettings, key='Hippo.EmailVerifyApiKey' or supply licenseKey parameter to Initialize(licenseKey) method.
        /// </exception>
        public static IService<VerificationRequest, VerificationResponses, ProgressEventArgs> Create()
        {
            if (Interlocked.Read(ref initialized) < 1)
            {
                throw new InvalidOperationException(
                    "License key not set. Call Initialize method first and either add key to appSettings, key='Hippo.EmailVerifyApiKey' or supply licenseKey parameter to Initialize(licenseKey) method.");
            }

            return DefaultServiceLazy.Value;
        }

        /// <summary>
        /// Initializes the software.
        ///     <remarks>
        /// This needs to be called only once per app domain.
        /// </remarks>
        /// </summary>
        /// <param name="licenseKey">
        /// <para>
        /// [Optional] The license key. If not supplied, software looks for the key in appSettings,
        ///         key='Hippo.EmailVerifyApiKey'
        ///     </para>
        /// </param>
        public static void Initialize(string licenseKey = null)
        {
            if (Interlocked.Read(ref initialized) > 0)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(licenseKey))
            {
                appDomainLicenseKey = licenseKey;
            }
            else
            {
                var appSetting = ConfigurationManager.AppSettings["Hippo.EmailVerifyApiKey"];

                if (string.IsNullOrWhiteSpace(appSetting))
                {
                    throw new InvalidOperationException("licenseKey not set in Initialize(licenseKey..) parameter. Also, key not found in appSettings, key='Hippo.EmailVerifyApiKey'. To rectify this error, please call Initialize method first and either add key to appSettings, key='Hippo.EmailVerifyApiKey' or supply licenseKey parameter to Initialize(licenseKey) method.");
                }

                appDomainLicenseKey = appSetting;
            }

            Interlocked.Exchange(ref initialized, 1);
        }

        #endregion
    }
}