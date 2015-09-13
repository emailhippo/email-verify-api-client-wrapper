// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestBase.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Tests
{
    #region Usings

    using System;
    using System.Diagnostics.Tracing;

    using EmailHippo.EmailVerify.Api.Client.Diagnostics.EventSources;

    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

    #endregion

    /// <summary>
    /// The test base.
    /// </summary>
    public abstract class TestBase
    {
        #region Constructors and Destructors

        #endregion

        #region Methods

        /// <summary>
        /// The write time elapsed.
        /// </summary>
        /// <param name="timerElapsed">
        /// The timer elapsed.
        /// </param>
        protected static void WriteTimeElapsed(long timerElapsed)
        {
            Console.WriteLine("Elapsed timer: {0}ms", timerElapsed);
        }

        /// <summary>
        /// The write time elapsed.
        /// </summary>
        /// <param name="timerElapsed">
        /// The timer elapsed.
        /// </param>
        protected static void WriteTimeElapsed(TimeSpan timerElapsed)
        {
            Console.WriteLine("Elapsed timer: {0}", timerElapsed);
        }

        #endregion
    }
}