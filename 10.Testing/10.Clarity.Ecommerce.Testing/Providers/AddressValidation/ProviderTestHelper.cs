// <copyright file="ProviderTestHelper.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the provider test helper class</summary>
namespace Clarity.Ecommerce.Providers.Testing
{
    using System.Collections.Generic;
    using System.Configuration;

    public static class ProviderTestHelper
    {
        public static void SetupProviderConfig(string providerType, string providerShortName)
        {
            SetupProviderPluginPathsConfig(providerType, providerShortName);
            ConfigurationManager.AppSettings["Clarity.Providers.EnabledProviders"]
                = $"{providerShortName}{providerType}Provider";
            ConfigurationManager.AppSettings[$"Clarity.Providers.{providerType}Provider"]
                = $"{providerShortName}{providerType}Provider";
        }

        public static void SetupProviderPluginPathsConfig(string providerType, string providerShortName)
        {
            SetupProviderPluginPathsConfig(
                new List<ProviderAssemblyRef> { new ProviderAssemblyRef(providerType, providerShortName) });
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
            ////var pluginPaths = assemblies.Select(a => FormatPluginPath(a.Type, a.ShortName));
            ////ConfigurationManager.AppSettings["Clarity.Providers.PluginsPath"] = string.Join(";", pluginPaths);
        }

        ////private static string FormatPluginPath(string providerType, string providerNameShort)
        ////{
        ////    return string.Format(ProviderSourcePathFormatTemplate, providerType, providerNameShort);
        ////}

        ////private static string ProviderSourcePathFormatTemplate
        ////{
        ////    get
        ////    {
        ////        var cefRoot = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        ////        return $"{cefRoot}Providers\\{{0}}\\Clarity.Ecommerce.{{0}}Provider.{{1}}\\bin\\Debug";
        ////    }
        ////}
    }

    public class ProviderAssemblyRef
    {
        public ProviderAssemblyRef(string type, string shortName)
        {
            Type = type;
            ShortName = shortName;
        }

        public string Type { get; }

        public string ShortName { get; }
    }
}
