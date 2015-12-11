// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailValidationModel.cs" company="Email Hippo Ltd">
//   © Rolosoft Ltd
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

namespace EmailHippo.EmailVerify.Api.Client.MvcTests.Models
{
    #region Usings

    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    ///     The email validation model.
    /// </summary>
    public sealed class EmailValidationModel
    {
        /// <summary>
        ///     Gets or sets the license key.
        /// </summary>
        [Display(Name = "License Key")]
        [Required]
        public string LicenseKey { get; set; }

        /// <summary>
        ///     Gets or sets the email address.
        /// </summary>
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "valid email address format required")]
        [Required]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the primary result.
        /// </summary>
        public string PrimaryResult { get; set; }

        /// <summary>
        /// Gets or sets the result reason.
        /// </summary>
        public string ResultReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display results].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display results]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayResults { get; set; }

        /// <summary>
        /// Gets or sets the execution time.
        /// </summary>
        /// <value>
        /// The execution time.
        /// </value>
        public int ExecutionTime { get; set; }
    }
}