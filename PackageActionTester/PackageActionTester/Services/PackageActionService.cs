using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using PackageActionTester.Extensions;
using PackageActionTester.Models;
using Umbraco.Core;
using umbraco.cms.businesslogic.packager;
using umbraco.cms.businesslogic.packager.standardPackageActions;
using umbraco.interfaces;

namespace PackageActionTester.Services
{
    public static class PackageActionService
    {
        /// <summary>
        /// Gets all available package actions.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PackageActionModel> GetAll()
        {
            var foundValueParsers = TypeFinder.FindClassesOfType<IPackageAction>();
            return foundValueParsers.Select(type => Activator.CreateInstance(type) as IPackageAction).Select(a => new PackageActionModel { Alias = a.Alias(), SampleXMl = a.GetSampleOrDefault() }).OrderBy(p=>p.Alias).ToList();
        }
        /// <summary>
        /// Executes the package actions.
        /// </summary>
        /// <param name="executeModel">The execute model.</param>
        /// <returns></returns>
        public static IEnumerable<PackageActionResult> ExecutePackageActions(PackageActionExecuteModel executeModel)
        {
            var result = new List<PackageActionResult>();
            try
            {
                var xmlDocument = helper.parseStringToXmlNode(string.Format("<actions>{0}</actions>", executeModel.Xml));
                var nodeList = xmlDocument.SelectNodes("//Action");
                if (nodeList.Count == 0)
                {
                    result.Add(new PackageActionResult { Alias = "Error", Result = "Invalid action xml" });
                }
                foreach (XmlNode node in nodeList)
                {
                    var packageAlias = node.GetAttributeValueFromNode("alias");
                    if (string.IsNullOrWhiteSpace(packageAlias))
                    {
                        result.Add(new PackageActionResult { Alias = "Error", Result = string.Format("Error parsing xml: {0}", node.OuterXml) });
                        break;
                    }
                    if (executeModel.InstallAction.Equals("install", StringComparison.InvariantCultureIgnoreCase))
                    {
                        InstallAction(packageAlias, node, result);
                    }
                    else
                    {
                        UnInstallAction(packageAlias, node, result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(new PackageActionResult { Alias = "Error", Result = string.Format("Error parsing xml: {0}",ex) });
            }

            return result;
        }

        /// <summary>
        /// Installs the action.
        /// </summary>
        /// <param name="packageAlias">The package alias.</param>
        /// <param name="node">The node.</param>
        /// <param name="result">The result.</param>
        private static void InstallAction(string packageAlias, XmlNode node, List<PackageActionResult> result)
        {
            try
            {
                PackageAction.RunPackageAction("Packageactiontester", packageAlias, node);
                result.Add(new PackageActionResult { Alias = packageAlias, Result = "Installed" });
            }
            catch (Exception ex)
            {
                result.Add(new PackageActionResult { Alias = packageAlias, Result = string.Format("Error installing: {0}", ex) });
            }
        }
        
        /// <summary>
        /// Uninstalls the action.
        /// </summary>
        /// <param name="packageAlias">The package alias.</param>
        /// <param name="node">The node.</param>
        /// <param name="result">The result.</param>
        private static void UnInstallAction(string packageAlias, XmlNode node, List<PackageActionResult> result)
        {
            try
            {
                PackageAction.UndoPackageAction("Packageactiontester", packageAlias, node);
                result.Add(new PackageActionResult { Alias = packageAlias, Result = "Uninstalled" });
            }
            catch (Exception ex)
            {
                result.Add(new PackageActionResult { Alias = packageAlias, Result = string.Format("Error uninstalling: {0}", ex) });
            }
        }
    }
}