// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerificationRequest.cs" company="Email Hippo Ltd">
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
    #region Usings

    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    /// The verification request.
    /// </summary>
    public sealed class VerificationRequest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        [MaxLength(255)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        #endregion
    }
}