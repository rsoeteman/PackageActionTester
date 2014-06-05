using Newtonsoft.Json;

namespace PackageActionTester.Models
{
    /// <summary>
    /// Model for package action tester
    /// </summary>
    public class PackageActionTesterModel
    {
        /// <summary>
        /// Gets or sets the alias of the package action.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the sample xml we can modify in the editor.
        /// </summary>
        public string SampleXMl { get; set; }
    }
}