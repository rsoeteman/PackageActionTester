using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PackageActionTester.Extensions;
using PackageActionTester.Models;
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
        public IEnumerable<PackageActionTesterModel> GetAll()
        {
            //return TypeFinder .FindClassesOfType<IPackageAction>()

            //Add dynamic assemblies 
            var foundValueParsers = TypeFinder.FindClassesOfType<IPackageAction>();
            var lst = foundValueParsers.Select(type => Activator.CreateInstance(type) as IPackageAction).Select(a => new PackageActionTesterModel { Alias = a.Alias(), SampleXMl = a.GetSampleOrDefault() }).ToList();
            return lst;
        }
    }
}