// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultClientTests.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Tests.Unit.Logic.Clients.EmailHippo.V2
{
    #region Usings

    using System.Diagnostics.Tracing;
    using System.Threading;

    using global::EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources;
    using global::EmailHippo.EmailVerify.Api.Client.Entities.Clients.V2;

    using global::EmailHippo.EmailVerify.Api.Client.Entities.Common.V2;

    using global::EmailHippo.EmailVerify.Api.Client.Entities.Configuration.V2;

    using global::EmailHippo.EmailVerify.Api.Client.Interfaces.Configuration;

    using global::EmailHippo.EmailVerify.Api.Client.Logic.Clients.EmailHippo.V2;

    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

    using Moq;

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// The default client tests.
    /// </summary>
    [Ignore("Tested and passed successfully 16 Feb '16. ROC")]
    [TestFixture]
    public class DefaultClientTests : TestBase
    {
        #region Fields

        /// <summary>
        ///     The listener 1.
        /// </summary>
        private ObservableEventListener listener1;

        /// <summary>
        /// The listener2
        /// </summary>
        private ObservableEventListener listener2;

        #endregion
        
        #region Constants

        /*Visit https://www.emailhippo.com to get your license key*/
        private const string MyLicenseKey = @"{your license here}";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The process async tests.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="MainStatusResponseType"/>.
        /// </returns>
        [TestCase("abuse@hotmail.com", ExpectedResult = MainStatusResponseType.Ok)]
        [TestCase("abuse@yahoo.com", ExpectedResult = MainStatusResponseType.Ok)]
        public MainStatusResponseType ProcessAsyncTests(string emailAddress)
        {
            // arrange
            var mockConfiguration = new Mock<IConfiguration<KeyAuthentication>>();

            mockConfiguration.Setup(r => r.Get).Returns(() => new KeyAuthentication { LicenseKey = MyLicenseKey });

            var defaultClient = new DefaultClient(mockConfiguration.Object);

            // act
            var verificationResponse =
                defaultClient.ProcessAsync(new VerificationRequest { Email = emailAddress }, CancellationToken.None)
                    .Result;

            // assert
            return verificationResponse.Result;
        }

        /// <summary>
        /// The process tests.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="MainStatusResponseType"/>.
        /// </returns>
        [TestCase("abuse@hotmail.com", ExpectedResult = MainStatusResponseType.Ok)]
        [TestCase("abuse@yahoo.com", ExpectedResult = MainStatusResponseType.Ok)]
        public MainStatusResponseType ProcessTests(string emailAddress)
        {
            // arrange
            var mockConfiguration = new Mock<IConfiguration<KeyAuthentication>>();

            mockConfiguration.Setup(r => r.Get).Returns(() => new KeyAuthentication { LicenseKey = MyLicenseKey });

            var defaultClient = new DefaultClient(mockConfiguration.Object);

            // act
            var verificationResponse = defaultClient.Process(new VerificationRequest { Email = emailAddress });

            // assert
            return verificationResponse.Result;
        }

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        /// <summary>
        /// The test fixture setup.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            this.listener1 = new ObservableEventListener();
            this.listener1.EnableEvents(ExceptionLoggingEventSource.Log, EventLevel.LogAlways);

            this.listener1.LogToConsole();

            this.listener2 = new ObservableEventListener();
            this.listener2.EnableEvents(ActivityLoggingEventSource.Log, EventLevel.LogAlways, Keywords.All);

            this.listener2.LogToConsole();
        }

        /// <summary>
        /// The test fixture tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            this.listener1.DisableEvents(ExceptionLoggingEventSource.Log);

            this.listener2.DisableEvents(ActivityLoggingEventSource.Log);
        }

        #endregion
    }
}