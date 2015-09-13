// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceContracts.cs" company="Email Hippo Ltd">
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

namespace EmailHippo.EmailVerify.Api.Client.Interfaces.Contracts
{
    #region Usings

    using System;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using EmailHippo.EmailVerify.Api.Client.Interfaces.Service;

    #endregion


    /// <summary>
    /// Contract class for IService.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <typeparam name="TProgressReporting">The type of the progress reporting.</typeparam>
    [ContractClassFor(typeof(IService<,,>))]
    internal abstract class ServiceContracts<TRequest, TResponse, TProgressReporting> : IService<TRequest, TResponse, TProgressReporting>
    {
        #region Public Events

        /// <summary>
        /// The progress changed.
        /// </summary>
        public event EventHandler<TProgressReporting> ProgressChanged;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The process.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="TResponse"/>.
        /// </returns>
        public virtual TResponse Process(TRequest request)
        {
            Contract.Requires(request != null);
            Contract.Ensures(Contract.Result<TResponse>() != null);

            return default(TResponse);
        }

        /// <summary>
        /// The process async.
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
        public virtual Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken)
        {
            Contract.Requires(request != null);

            return default(Task<TResponse>);
        }

        #endregion
    }
}