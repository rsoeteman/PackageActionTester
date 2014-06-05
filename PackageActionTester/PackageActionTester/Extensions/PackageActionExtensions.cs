using umbraco.interfaces;

namespace PackageActionTester.Extensions
{
    public static class PackageActionExtensions
    {
        public static string GetSampleOrDefault(this IPackageAction action)
        {
            try
            {
                return action.SampleXml().OuterXml;
            }
            catch
            {
                return  string.Format("<Action runat=\"install\" alias=\"{0}\"/>",action.Alias());
            }
        }
    }
}