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

namespace EmailHippo.EmailVerify.Api.Client.Tests.Unit.Services.EmailHippo.V2
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using global::EmailHippo.EmailVerify.Api.Client.Entities.Common.V2;
    using global::EmailHippo.EmailVerify.Api.Client.Entities.Service.V2;
    using global::EmailHippo.EmailVerify.Api.Client.Interfaces.Clients;
    using global::EmailHippo.EmailVerify.Api.Client.Services.EmailHippo.V2;

    using Moq;

    using Newtonsoft.Json;

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// The default service tests.
    /// </summary>
    [TestFixture]
    public class DefaultServiceTests : TestBase
    {
        #region Public Methods and Operators

        [Test]
        public void CalculatePercentageProgress_WhenZeroProgress_ExpectZeroPercent()
        {
            // arrange
            
            // act
            var calculatePercentageProgress = DefaultService.CalculatePercentageProgress(0, 0);

            // assert
            Assert.AreEqual(0, calculatePercentageProgress);
        }

        [Test]
        public void CalculatePercentageProgress_WhenHalfProgress_ExpectFiftyPercent()
        {
            // arrange

            // act
            var calculatePercentageProgress = DefaultService.CalculatePercentageProgress(50, 100);

            // assert
            Assert.AreEqual(50, calculatePercentageProgress);
        }

        [Test]
        public void CalculatePercentageProgress_WhenFullProgress_ExpectHundredPercent()
        {
            // arrange

            // act
            var calculatePercentageProgress = DefaultService.CalculatePercentageProgress(100, 100);

            // assert
            Assert.AreEqual(100, calculatePercentageProgress);
        }

        [Test]
        public void Process_WhenTestList1_ExpectLoggingAndTimingOutput()
        {
            // arrange
            var mockClientProxy = new Mock<IClientProxy<Entities.Clients.V2.VerificationRequest, Entities.Clients.V2.VerificationResponse>>();
            
            mockClientProxy.Setup(
                r =>
                r.ProcessAsync(It.IsAny<Entities.Clients.V2.VerificationRequest>(), It.IsAny<CancellationToken>()))
                .Returns(
                    () => Task.FromResult(
                        new Entities.Clients.V2.VerificationResponse
                        {
                            Domain = @"here.com",
                            Duration = 710,
                            Email = @"me@here.com",
                            IsDisposable = false,
                            IsFree = false,
                            IsRole = false,
                            MailServerLocation = "US",
                            Reason = AdditionalStatusResponseType.None,
                            Result = MainStatusResponseType.Ok,
                            User = @"me"
                        }));

            var defaultService = new DefaultService(mockClientProxy.Object);
            defaultService.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));

            // act
            var stopwatch = Stopwatch.StartNew();
            var verificationResponses = defaultService.Process(new VerificationRequest { Emails = TestList1 });
            stopwatch.Stop();

            // assert
            /*mockClientProxy.Verify(
                r => r.ProcessAsync(It.IsAny<Entities.Clients.V2.VerificationRequest>(), It.IsAny<CancellationToken>()),
                Times.Exactly(TestList1.Count));*/
            Console.WriteLine("# emails checked: {0}", verificationResponses.Results.Count);
            Console.WriteLine(JsonConvert.SerializeObject(verificationResponses));
            WriteTimeElapsed(stopwatch.ElapsedMilliseconds);
        }

        [Test]
        public void ProcessAsync_WhenTestList1_ExpectLoggingAndTimingOutput()
        {
            // arrange
            var mockClientProxy = new Mock<IClientProxy<Entities.Clients.V2.VerificationRequest, Entities.Clients.V2.VerificationResponse>>();

            mockClientProxy.Setup(
                r =>
                r.ProcessAsync(It.IsAny<Entities.Clients.V2.VerificationRequest>(), It.IsAny<CancellationToken>()))
                .Returns(
                    () => Task.FromResult(
                        new Entities.Clients.V2.VerificationResponse
                        {
                            Domain = @"here.com",
                            Duration = 710,
                            Email = @"me@here.com",
                            IsDisposable = false,
                            IsFree = false,
                            IsRole = false,
                            MailServerLocation = "US",
                            Reason = AdditionalStatusResponseType.None,
                            Result = MainStatusResponseType.Ok,
                            User = @"me"
                        }));

            var defaultService = new DefaultService(mockClientProxy.Object);
            defaultService.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));

            // act
            var stopwatch = Stopwatch.StartNew();
            var verificationResponses = defaultService.ProcessAsync(new VerificationRequest { Emails = TestList1 }, CancellationToken.None).Result;
            stopwatch.Stop();

            // assert
            /*mockClientProxy.Verify(
                r => r.ProcessAsync(It.IsAny<Entities.Clients.V2.VerificationRequest>(), It.IsAny<CancellationToken>()),
                Times.Exactly(TestList1.Count));*/
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
        }

        /// <summary>
        /// The test fixture tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        #endregion

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
    }
}