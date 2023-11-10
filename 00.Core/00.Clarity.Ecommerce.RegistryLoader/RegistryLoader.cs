// <copyright file="RegistryLoader.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the registry loader class</summary>
namespace Clarity.Ecommerce
{
    using System;
    using System.Collections.Concurrent;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
#if NET5_0_OR_GREATER
    using Interfaces.DataModel;
    using Lamar;
#else
    using StructureMap;
#endif
    using Utilities;

#if NET5_0_OR_GREATER
    /// <summary>A registry loader.</summary>
    /// <seealso cref="ServiceRegistry"/>
    public class RegistryLoader : ServiceRegistry
#else
    /// <summary>A registry loader.</summary>
    /// <seealso cref="Registry"/>
    public class RegistryLoader : Registry
#endif
    {
        private static readonly Func<Assembly, bool> AssemblyFilter =
            x => x.FullName!.Contains("Clarity") || x.FullName.Contains("ServiceStack.Authentication");
        ////x => x.GetTypes().Any(y => y.IsAssignableFrom(typeof(IProviderBase)));

#if NET5_0_OR_GREATER
        private string pluginsLocation = @"{CEF_RootPath}\Plugins5";
        private string clientsLocation = @"{CEF_RootPath}\ClientPlugins5";
#else
        private string pluginsLocation = @"{CEF_RootPath}\Plugins";
        private string clientsLocation = @"{CEF_RootPath}\ClientPlugins";
#endif

        /// <summary>Prevents a default instance of the <see cref="RegistryLoader"/> class from being created.</summary>
        private RegistryLoader()
        {
            Scan(_ =>
            {
                _.AssembliesFromApplicationBaseDirectory(AssemblyFilter);
                var pluginsPath = ConfigurationManager.AppSettings["Clarity.Providers.PluginsPath"];
                if (!string.IsNullOrWhiteSpace(pluginsPath))
                {
                    pluginsLocation = pluginsPath;
                }
                if (pluginsLocation.Contains("{CEF_RootPath}"))
                {
                    pluginsLocation = pluginsLocation.Replace("{CEF_RootPath}", Globals.CEFRootPath);
                }
                if (Directory.Exists(pluginsLocation))
                {
                    _.AssembliesFromPath(pluginsLocation, AssemblyFilter);
                }
                var clientsPath = ConfigurationManager.AppSettings["Clarity.Providers.ClientsPath"];
                if (!string.IsNullOrWhiteSpace(clientsPath))
                {
                    clientsLocation = clientsPath;
                }
                if (clientsLocation.Contains("{CEF_RootPath}"))
                {
                    clientsLocation = clientsLocation.Replace("{CEF_RootPath}", Globals.CEFRootPath);
                }
                if (Directory.Exists(clientsLocation))
                {
                    _.AssembliesFromPath(clientsLocation, AssemblyFilter);
                }
                _.LookForRegistries();
            });
#if NET5_0_OR_GREATER
            Injectable<IDbContext>();
#endif
        }

        /// <summary>Gets the root container.</summary>
        /// <value>The root container.</value>
        public static IContainer RootContainer { get; } = new Container(new RegistryLoader());

        /// <summary>Gets the container instance.</summary>
        /// <value>The container instance.</value>
        public static IContainer ContainerInstance => OverriddenContainerInstances.Any()
            ? OverriddenContainerInstances.First().Value
            : RootContainer;

        /// <summary>Gets the overridden container instances.</summary>
        /// <value>The overridden container instances.</value>
        private static ConcurrentDictionary<string, IContainer> OverriddenContainerInstances { get; }
            = new();

        /// <summary>Named container instance.</summary>
        /// <param name="name">The name.</param>
        /// <returns>An IContainer.</returns>
        [DebuggerStepThrough]
        public static IContainer NamedContainerInstance(string? name = null)
        {
            if (!Contract.CheckValidKey(name))
            {
                return RootContainer;
            }
            if (!OverriddenContainerInstances.ContainsKey(name!))
            {
                throw new ArgumentException(@"The testing named container instance doesn't exist", nameof(name));
            }
            OverriddenContainerInstances.TryGetValue(name!, out var retVal);
            return retVal!;
        }

        /// <summary>Override container.</summary>
        /// <param name="nestedContainer">The nested container.</param>
        /// <param name="name">           The name.</param>
        [DebuggerStepThrough]
        public static void OverrideContainer(IContainer nestedContainer, string name)
        {
            var tryCount = 0;
            while (++tryCount < 3 && !OverriddenContainerInstances.TryAdd(name, nestedContainer))
            {
            }
        }

        /// <summary>Removes the override container described by name.</summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        public static void RemoveOverrideContainer(string name)
        {
            if (!OverriddenContainerInstances.ContainsKey(name))
            {
                return;
            }
            OverriddenContainerInstances.TryRemove(name, out _);
        }
    }
}
