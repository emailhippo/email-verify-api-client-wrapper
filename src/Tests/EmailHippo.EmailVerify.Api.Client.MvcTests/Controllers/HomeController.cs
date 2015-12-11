// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Email Hippo Ltd">
//   © Rolosoft Ltd
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

namespace EmailHippo.EmailVerify.Api.Client.MvcTests.Controllers
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using EmailHippo.EmailVerify.Api.Client.Entities.Service.V2;
    using EmailHippo.EmailVerify.Api.Client.MvcTests.Models;

    #endregion

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailValidationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            ApiClientFactoryV2.Initialize(model.LicenseKey);

            var myService = ApiClientFactoryV2.Create();

            var verificationResponses =
                await myService.ProcessAsync(new VerificationRequest { Emails = new List<string> { model.EmailAddress } }, CancellationToken.None);

            var readOnlyCollection = verificationResponses.Results;

            VerificationResponse firstOrDefault = readOnlyCollection.FirstOrDefault();

            if (firstOrDefault != null)
            {
                model.PrimaryResult = firstOrDefault.Result.ToString();
                model.ResultReason = firstOrDefault.Reason.ToString();
                model.ExecutionTime = firstOrDefault.Duration;
            }

            model.DisplayResults = true;

            return this.View(model);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// The about.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// The contact.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}