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

namespace EmailHippo.EmailVerify.Api.Client.Entities.Clients.V2
{
    using EmailHippo.EmailVerify.Api.Client.Entities.Common.V2;
    using EmailHippo.EmailVerify.Api.Client.Json;

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
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is disposable.
        /// </summary>
        [JsonProperty("disposable")]
        [JsonConverter(typeof(LowerCaseBoolJsonConverter))]
        public bool IsDisposable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is free.
        /// </summary>
        [JsonProperty("free")]
        [JsonConverter(typeof(LowerCaseBoolJsonConverter))]
        public bool IsFree { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is role.
        /// </summary>
        [JsonProperty("role")]
        [JsonConverter(typeof(LowerCaseBoolJsonConverter))]
        public bool IsRole { get; set; }

        /// <summary>
        /// Gets or sets the mail server location.
        /// </summary>
        [JsonProperty("mailServerLocation")]
        public string MailServerLocation { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        [JsonProperty("reason")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AdditionalStatusResponseType Reason { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        [JsonProperty("result")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MainStatusResponseType Result { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [JsonProperty("user")]
        public string User { get; set; }

        #endregion
    }
}