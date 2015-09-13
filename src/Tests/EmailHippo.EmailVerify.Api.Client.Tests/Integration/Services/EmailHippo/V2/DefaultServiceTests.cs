// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultServiceTests.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Tests.Integration.Services.EmailHippo.V2
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Tracing;
    using System.Threading;

    using global::EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources;
    using global::EmailHippo.EmailVerify.Api.Client.Entities.Configuration.V2;
    using global::EmailHippo.EmailVerify.Api.Client.Entities.Service.V2;
    
    using global::EmailHippo.EmailVerify.Api.Client.Interfaces.Configuration;
    using global::EmailHippo.EmailVerify.Api.Client.Logic.Clients.EmailHippo.V2;
    using global::EmailHippo.EmailVerify.Api.Client.Services.EmailHippo.V2;

    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

    using Moq;

    using Newtonsoft.Json;

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// The default service tests.
    /// </summary>
    [Ignore("Tested and passed successfully 14 Sept '15. ROC")]
    [TestFixture]
    public class DefaultServiceTests : TestBase
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
        
        /*Visit https://www.emailhippo.com to get your license key*/
        private const string MyLicenseKey = @"{your key here}";
        

        #region Public Methods and Operators

        [Test]
        public void Process_WhenTestList1_ExpectLoggingAndTimingOutput()
        {
            // arrange
            var mockConfiguration = new Mock<IConfiguration<KeyAuthentication>>();

            mockConfiguration.Setup(r => r.Get).Returns(() => new KeyAuthentication { LicenseKey = MyLicenseKey });

            var defaultClient = new DefaultClient(mockConfiguration.Object);

            var defaultService = new DefaultService(defaultClient);
            defaultService.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));

            // act
            var stopwatch = Stopwatch.StartNew();
            var verificationResponses = defaultService.Process(new VerificationRequest { Emails = TestList1 });
            stopwatch.Stop();

            // assert
            Console.WriteLine("# emails checked: {0}", verificationResponses.Results.Count);
            Console.WriteLine(JsonConvert.SerializeObject(verificationResponses));
            WriteTimeElapsed(stopwatch.ElapsedMilliseconds);
        }

        [Test]
        public void ProcessAsync_WhenTestList1_ExpectLoggingAndTimingOutput()
        {
            // arrange
            var mockConfiguration = new Mock<IConfiguration<KeyAuthentication>>();

            mockConfiguration.Setup(r => r.Get).Returns(() => new KeyAuthentication { LicenseKey = MyLicenseKey });

            var defaultClient = new DefaultClient(mockConfiguration.Object);

            var defaultService = new DefaultService(defaultClient);
            defaultService.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));

            // act
            var stopwatch = Stopwatch.StartNew();
            var verificationResponses = defaultService.ProcessAsync(new VerificationRequest { Emails = TestList1 }, CancellationToken.None).Result;
            stopwatch.Stop();

            // assert
            Console.WriteLine("# emails checked: {0}", verificationResponses.Results.Count);
            Console.WriteLine(JsonConvert.SerializeObject(verificationResponses));
            WriteTimeElapsed(stopwatch.ElapsedMilliseconds);
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
            this.listener1.EnableEvents(ExceptionLoggingEventSource.Log, EventLevel.Error);

            this.listener1.LogToConsole();

            this.listener2 = new ObservableEventListener();
            this.listener2.EnableEvents(ActivityLoggingEventSource.Log, EventLevel.Error, Keywords.All);

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

        #region Properties

        /// <summary>
        /// Gets the test list1.
        /// </summary>
        /// <value>
        /// The test list1.
        /// </value>
        private static List<string> TestList1
        {
            get
            {
                return new List<string>
                           {
                               "abuse@hotmail.com",
                               "abuse@aol.com",
                               "abuse@yahoo.com",
                               "abuse@bbc.co.uk",
                               "abuse@mailinator.com",
                               "abuse@abc.com",
                               "abuse@microsoft.com",
                               "abuse@gmail.com"
                           };
            }
        }

        #endregion
    }
}