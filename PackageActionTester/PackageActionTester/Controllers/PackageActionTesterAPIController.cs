using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PackageActionTester.Extensions;
using PackageActionTester.Models;
using PackageActionTester.Services;
using Umbraco.Core;
using Umbraco.Web.Editors;
using umbraco.cms.businesslogic.packager;
using umbraco.interfaces;

namespace PackageActionTester.Controllers
{
    [Umbraco.Web.Mvc.PluginController("PackageActionTester")]
    public class PackageActionTesterAPIController : UmbracoAuthorizedJsonController
    {
        /// <summary>
        /// Returns all actions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PackageActionModel> GetAll()
        {
         return   PackageActionService.GetAll();
        }

        /// <summary>
        /// Executes the package actions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<PackageActionResult> ExecutePackageActions(PackageActionExecuteModel model)
        {
            return PackageActionService.ExecutePackageActions(model);
        }
    }
}