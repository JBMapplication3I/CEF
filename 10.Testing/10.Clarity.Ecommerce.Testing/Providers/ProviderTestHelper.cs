namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.IO;

    public static class ProviderTestHelper
    {
        private const string Project = "05.Clarity.Ecommerce.Workflow.Testing";

        public static void SetupProviderConfig(string providerType, string providerShortName)
        {
            SetupProviderPluginPathsConfig(new List<ProviderAssemblyRef> { new ProviderAssemblyRef(providerType, providerShortName) });
        }

        public static void SetupProviderPluginPathsConfig(ProviderAssemblyRef assemblyRef)
        {
            SetupProviderPluginPathsConfig(new List<ProviderAssemblyRef> { assemblyRef });
        }

        public static void SetupProviderPluginPathsConfig(List<ProviderAssemblyRef> assemblies)
        {
            SetConfigProviderPluginPaths(assemblies);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void SetConfigProviderPluginPaths(List<ProviderAssemblyRef> assemblies)
        {
            // var pluginPaths = assemblies.Select(a => FormatPluginPath(a.Type, a.ShortName));
            // ConfigurationManager.AppSettings["Clarity.Providers.PluginsPath"] = string.Join(";", pluginPaths);
        }

        public static string FormatPluginPath(string providerType, string providerNameShort)
        {
            return string.Format(ProviderSourcePathFormatTemplate, providerType, providerNameShort);
        }

        private static string ProviderSourcePathFormatTemplate
        {
            get
            {
                var cefRoot = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))?.Replace(Project, string.Empty);
                return $@"{cefRoot}Providers\{{0}}\Clarity.Ecommerce.{{0}}Provider.{{1}}\bin\Debug";
            }
        }
    }

    public class ProviderAssemblyRef
    {
        public string Type { get; }
        public string ShortName { get; }

        public ProviderAssemblyRef(string type, string shortName)
        {
            Type = type;
            ShortName = shortName;
        }
    }
}
