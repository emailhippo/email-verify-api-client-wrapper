// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IService.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Interfaces.Service
{
    #region Usings

    using System;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using EmailHippo.EmailVerify.Api.Client.Interfaces.Contracts;

    #endregion

    /// <summary>
    /// The Service interface.
    /// </summary>
    /// <typeparam name="TRequest">Type of request.</typeparam>
    /// <typeparam name="TResponse">Type of response.</typeparam>
    /// <typeparam name="TProgressReporting">The type of the progress reporting.</typeparam>
    [ContractClass(typeof(ServiceContracts<,,>))]
    public interface IService<in TRequest, TResponse, TProgressReporting>
    {
        #region Public Events

        /// <summary>
        /// The progress changed.
        /// </summary>
        event EventHandler<TProgressReporting> ProgressChanged;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Processes the specified request.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="TResponse"/>.
        /// </returns>
        TResponse Process(TRequest request);

        /// <summary>
        /// Processes the asynchronous.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken);

        #endregion
    }
}