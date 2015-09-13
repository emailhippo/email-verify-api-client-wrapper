// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionLoggingEventSourceTests.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Tests.Unit.Diagnostics.EventSources
{
    #region Usings

    using EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources;

    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;

    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     The exception logging event source tests.
    /// </summary>
    [TestFixture]
    public class ExceptionLoggingEventSourceTests : TestBase
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// The should validate event source.
        /// </summary>
        [Test]
        public void ShouldValidateEventSource()
        {
            EventSourceAnalyzer.InspectAll(ExceptionLoggingEventSource.Log);
        }

        /// <summary>
        ///     The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        /// <summary>
        ///     The test fixture setup.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
        }

        /// <summary>
        ///     The test fixture tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        #endregion
    }
}