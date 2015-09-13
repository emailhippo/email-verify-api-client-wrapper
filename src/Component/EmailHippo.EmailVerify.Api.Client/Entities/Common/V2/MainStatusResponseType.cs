// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainStatusResponseType.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Entities.Common.V2
{
    /// <summary>
    /// The main status response type.
    /// </summary>
    public enum MainStatusResponseType
    {
        /// <summary>
        /// Verification passes all checks including Syntax, DNS, MX, Mailbox, Deep Server Configuration, Grey Listing.
        /// </summary>
        Ok, 

        /// <summary>
        /// Verification fails checks for definitive reasons (e.g. mailbox does not exist).
        /// </summary>
        Bad, 

        /// <summary>
        /// Conclusive verification result cannot be achieved at this time. Please try again later. - This is ShutDowns, IPBlock, TimeOuts.
        /// </summary>
        RetryLater, 
        
        /// <summary>
        /// Conclusive verification result cannot be achieved due to mail server configuration or anti-spam measures. See table “Additional Status Codes”.
        /// </summary>
        Unverifiable
    }
}