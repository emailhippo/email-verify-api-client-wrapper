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
namespace EmailHippo.EmailVerify.Api.Client.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Diagnostics.Tracing;
    using System.IO;
    using System.Threading;

    using EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources;
    using EmailHippo.EmailVerify.Api.Client.Entities.Service.V2;
    
    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [Ignore("Tested and passed successfully 16 Feb '16. ROC")]
    [TestFixture]
    public class ApiClientFactoryV2Tests : TestBase
    {
        /*Visit https://www.emailhippo.com to get your license key*/
        private const string MyLicenseKey = @"{your license here}";
        
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

        [Test]
        public void CreateAndRunWork_ExpectNoErrors()
        {
            // arrange
            var service = ApiClientFactoryV2.Create();
            service.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));

            // act
            var stopwatch = Stopwatch.StartNew();
            /*var verificationResponses = service.Process(
                new VerificationRequest { Emails = TestList1 });*/
            var verificationResponses = service.ProcessAsync(
                new VerificationRequest { Emails = TestList1 },
                CancellationToken.None).Result;
            stopwatch.Stop();


            // assert
            Assert.IsNotNull(verificationResponses);
            Console.WriteLine(
                JsonConvert.SerializeObject(verificationResponses));
            WriteTimeElapsed(stopwatch.ElapsedMilliseconds);

            service.ProgressChanged -= (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));
        }


        [Test]
        public void CreateAndRunPerformanceTest_ExpectTimingsOutputOnly()
        {
            // arrange
            var service = ApiClientFactoryV2.Create();
            service.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));

            // act
            var stopwatch = Stopwatch.StartNew();
            /*var verificationResponses = service.Process(
                new VerificationRequest { Emails = TestList1 });*/
            var verificationResponses = service.ProcessAsync(
                new VerificationRequest { Emails = PerformanceTestList1 },
                CancellationToken.None).Result;
            stopwatch.Stop();


            // assert
            Assert.IsNotNull(verificationResponses);
            Console.WriteLine(
                JsonConvert.SerializeObject(verificationResponses));
            WriteTimeElapsed(stopwatch.ElapsedMilliseconds);

            service.ProgressChanged -= (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));
        }


        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            this.listener1 = new ObservableEventListener();
            this.listener1.EnableEvents(ExceptionLoggingEventSource.Log, EventLevel.Error);

            this.listener1.LogToConsole();

            this.listener2 = new ObservableEventListener();
            this.listener2.EnableEvents(ActivityLoggingEventSource.Log, EventLevel.Error, Keywords.All);

            this.listener2.LogToConsole();

            ApiClientFactoryV2.Initialize(MyLicenseKey);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            this.listener1.DisableEvents(ExceptionLoggingEventSource.Log);

            this.listener2.DisableEvents(ActivityLoggingEventSource.Log);
        }

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

        /// <summary>
        /// Gets the performance test list1.
        /// </summary>
        /// <value>
        /// The performance test list1.
        /// </value>
        private static List<string> PerformanceTestList1
        {
            get
            {
                Contract.Ensures(Contract.Result<List<string>>() != null);

                const int ReturnedItems = 50;
                const string DomainToTest = @"gmail.com";

                var rtn = new List<string>();

                for (int i = 0; i < ReturnedItems; i++)
                {
                    var randomFileName = Path.GetRandomFileName();

                    var concat = string.Concat(randomFileName, "@", DomainToTest);

                    rtn.Add(concat);
                }


                return rtn;
            }
        }

        #endregion
    }
}