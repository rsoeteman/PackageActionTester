using System.Runtime.Serialization;

namespace PackageActionTester.Models
{
    /// <summary>
    /// Result when installing the package actions
    /// </summary>
    public class PackageActionResult
    {
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result { get; set; }
    }
}