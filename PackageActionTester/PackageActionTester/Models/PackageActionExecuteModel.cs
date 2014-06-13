using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PackageActionTester.Models
{
    public class PackageActionExecuteModel
    {
        /// <summary>
        /// Gets or sets the install action.
        /// Possible values
        /// Install
        /// Uninstall
        /// </summary>
        /// <value>
        /// The execute action.
        /// </value>
        public string InstallAction { get; set; }

        public string Xml { get; set; }
    }
}