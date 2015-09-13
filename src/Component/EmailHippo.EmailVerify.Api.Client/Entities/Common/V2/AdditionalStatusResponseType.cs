// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdditionalStatusResponseType.cs" company="Email Hippo Ltd">
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
    /// The additional status response type.
    /// </summary>
    public enum AdditionalStatusResponseType
    {
        /// <summary>
        /// <para>No additional information is available.</para>
        /// <para>This status differs from a TransientNetworkFault as it should not be retried (the result will not change).</para>
        /// <para>There are a few known reasons for this status code for example the target mx record uses Office 365 or a mail provider implementing custom mailbox shutdowns.</para>
        /// </summary>
        None, 

        /// <summary>
        /// <para>The required ‘@’ sign is not found in email address.</para>
        /// </summary>
        AtSignNotFound, 

        /// <summary>
        /// <para>The domain (i.e. the bit after the ‘@’ character) defined in the email address does not exist, according to DNS records.</para>
        /// <para>A domain that does not exist cannot have email boxes. A domain that does not exist cannot have email boxes.</para>
        /// </summary>
        DomainIsInexistent, 

        /// <summary>
        /// <para>The domain is a well known Disposable Email Address DEA.</para>
        /// <para>There are many services available that permit users to use a one-time only email address. Typically, these email addresses are used by individuals wishing to gain access to content or services requiring registration of email addresses but same individuals not wishing to divulge their true identities (e.g. permanent email addresses).</para>
        /// <para>DEA addresses should not be regarded as valid for email send purposes as it is unlikely that messages sent to DEA addresses will ever be read.</para>
        /// </summary>
        DomainIsWellKnownDea, 

        /// <summary>
        /// <para>Grey Listing is in operation. It is not possible to validate email boxes in real-time where grey listing is in operation.</para>
        /// </summary>
        GreyListing, 

        /// <summary>
        /// <para>The mailbox is full.</para>
        /// <para>Mailboxes that are full are unable to receive any further email messages until such time as the user empties the mail box or the system administrator grants extra storage quota.</para>
        /// <para>Most full mailboxes usually indicate accounts that have been abandoned by users and will therefore never be looked at again.</para>
        /// <para>We do not recommend sending emails to email addresses identified as full.</para>
        /// </summary>
        MailboxFull, 

        /// <summary>
        /// <para>The mailbox does not exist.</para>
        /// <para>100% confidence that the mail box does not exist.</para>
        /// </summary>
        MailboxDoesNotExist, 

        /// <summary>
        /// <para>There are no mail servers defined for this domain, according to DNS.</para>
        /// <para>Email addresses cannot be valid if there are no email servers defined in DNS for the domain.</para>
        /// </summary>
        NoMxServersFound, 

        /// <summary>
        /// <para>The server does not support international mailboxes.</para>
        /// <para>International email boxes are those that use international character sets such as Chinese / Kanji etc.</para>
        /// <para>International email boxes require systems in place for Punycode translation.</para>
        /// <para>Where these systems are not in place, email verification or delivery is not possible.</para>
        /// <para>For further information see Punycode.</para>
        /// </summary>
        ServerDoesNotSupportInternationalMailboxes, 

        /// <summary>
        /// <para>The server is configured for catch all and responds to all email verifications with a status of Ok.</para>
        /// <para>Mail servers can be configured with a policy known as Catch All. Catch all redirects any email address sent to a particular domain to a central email box for manual inspection. Catch all configured servers cannot respond to requests for email address verification.</para>
        /// </summary>
        ServerIsCatchAll, 

        /// <summary>
        /// <para>Successful verification.</para>
        /// <para>100% confidence that the mailbox exists.</para>
        /// </summary>
        Success, 

        /// <summary>
        /// <para>Too many ‘@’ signs found in email address.</para>
        /// <para>Only one ‘@’ character is allowed in email addresses.</para>
        /// </summary>
        TooManyAtSignsFound, 

        /// <summary>
        /// <para>The reason for the verification result is unknown.</para>
        /// </summary>
        Unknown, 

        /// <summary>
        /// <para>A temporary network fault occurred during verification. Please try again later.</para>
        /// <para>Verification operations on remote mail servers can sometimes fail for a number of reasons such as loss of network connection, remote servers timing out etc.</para>
        /// <para>These conditions are usually temporary. Retrying verification at a later time will usually result in a positive response from mail servers.</para>
        /// <para>Please note that setting an infinite retry policy around this status code is inadvisable as there is no way of knowing when the issue will be resolved within the target domain or the grey listing resolved, and this may affect your daily quota.</para>
        /// </summary>
        TransientNetworkFault, 

        /// <summary>
        /// <para>A possible spam trap email address or domain has been detected.</para>
        /// <para>Spam traps are email addresses or domains deliberately placed on-line in order to capture and flag potential spam based operations.</para>
        /// <para>Our advanced detection heuristics are capable of detecting likely spam trap addresses or domains known to be associated with spam trap techniques.</para>
        /// <para>We do not recommend sending emails to addresses identified as associated with known spam trap behaviour.</para>
        /// <para>Sending emails to known spam traps or domains will result in your ESP being subjected to email blocks from a DNS Block List.</para>
        /// <para>An ESP cannot tolerate entries in a Block List (as it adversely affects email deliverability for all customers) and will actively refuse to send emails on behalf of customers with a history of generating entries in a Block List.</para>
        /// </summary>
        PossibleSpamTrapDetected, 
    }
}