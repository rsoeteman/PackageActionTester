using System.IO;
using System.Xml;
using PackageActionTester.Extensions;
using Umbraco.Core.IO;
using umbraco.cms.businesslogic.packager.standardPackageActions;
using umbraco.interfaces;

namespace PackageActionTester.PackageActions
{
    /// <summary>
    /// Just a demo action for testing the package action tester
    /// </summary>
    public class CreateDummyConfigFileAction : IPackageAction
    {
        /// <summary>
        /// Executes action
        /// </summary>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="xmlNode">The XML node.</param>
        /// <returns></returns>
        public bool Execute(string packageName, XmlNode xmlNode)
        {
            var fileName = IOHelper.MapPath(xmlNode.GetAttributeValueFromNode("file"));

            if (!File.Exists(fileName))
            {
                //File doesn't exists
                //Make sure folder gets created
                var targetFolder = Path.GetDirectoryName(fileName);
                Directory.CreateDirectory(targetFolder);
                File.WriteAllText(fileName,"TEST Config file for testing a package action.");

            }
            return true;
        }

        public string Alias()
        {
            return "PackageActionTester_DemoAction";
        }

        /// <summary>
        /// Undoes the specified package name.
        /// </summary>
        /// <param name="packageName">Name of the package.</param>
        /// <param name="xmlNode">The XML node.</param>
        /// <returns></returns>
        public bool Undo(string packageName, XmlNode xmlNode)
        {
            var fileName = IOHelper.MapPath(xmlNode.GetAttributeValueFromNode("file"));
            File.Delete(fileName);

            return true;
        }

        public XmlNode SampleXml()
        {
            return helper.parseStringToXmlNode(string.Format("<Action runat=\"install\" alias=\"{0}\" file=\"~/app_data/temp/packageactiontester.config\"/>", Alias()));
        }
    }
}