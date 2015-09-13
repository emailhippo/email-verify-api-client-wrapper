// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationResponse.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Entities.Service.V2
{
    using EmailHippo.EmailVerify.Api.Client.Entities.Common.V2;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The verification response.
    /// </summary>
    public sealed class VerificationResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is disposable.
        /// </summary>
        public bool IsDisposable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is free.
        /// </summary>
        public bool IsFree { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is role.
        /// </summary>
        public bool IsRole { get; set; }

        /// <summary>
        /// Gets or sets the mail server location.
        /// </summary>
        public string MailServerLocation { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AdditionalStatusResponseType Reason { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MainStatusResponseType Result { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public string User { get; set; }

        #endregion
    }
}