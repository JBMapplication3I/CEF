// <copyright file="ServiceApplication.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service application class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Web;
    using DataModel;
    using JetBrains.Annotations;
    using JSConfigs;
    using Newtonsoft.Json;
    using ServiceStack;
    using ServiceStack.Configuration;
    using ServiceStack.Host;
    using ServiceStack.Logging;
    using ServiceStack.Logging.Log4Net;

    /// <summary>A service application.</summary>
    /// <seealso cref="HttpApplication"/>
    [PublicAPI]
    public class ServiceApplication : HttpApplication
    {
        private ServiceAppHost serviceStackHost = null!;

        private static bool UsingOracleDB { get; }
            = bool.Parse(ConfigurationManager.AppSettings["UsingOracle"] ?? "false");

        /// <summary>Application start.</summary>
        protected virtual void Application_Start()
        {
            try
            {
                LogManager.LogFactory = new Log4NetFactory(true);
                CEFConfigDictionary.Load();
                if (UsingOracleDB)
                {
                    /* Disabling this so Oracle doesn't load when it's not supposed to
                    // Update to the latest migration on first initialization
                    Database.SetInitializer(
                        new MigrateDatabaseToLatestVersion<OracleClarityEcommerceEntities, DataModel.Oracle.Migrations.Configuration>());
                    var configuration = new DataModel.Oracle.Migrations.Configuration();
                    var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
                    if (migrator.GetPendingMigrations().Any())
                    {
                        migrator.Update();
                    }
                    */
                }
                else
                {
                    // Update to the latest migration on first initialization
                    Database.SetInitializer(
                        new MigrateDatabaseToLatestVersion<ClarityEcommerceEntities, DataModel.Migrations.Configuration>());
                    var configuration = new DataModel.Migrations.Configuration();
                    var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
                    if (migrator.GetPendingMigrations().Any())
                    {
                        migrator.Update();
                    }
                }
                // Load ServiceStack Service
                serviceStackHost = new();
                serviceStackHost.Init();
                ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
                ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
                // 1.2+ is the only thing that should be allowed
            }
            catch (ReflectionTypeLoadException ex1)
            {
                throw new(
                    ex1.LoaderExceptions.Select(x => x.Message).Aggregate("LoaderExceptions Messages:", (c, n) => c + "\r\n" + n),
                    ex1);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(ex2);
                throw;
            }
        }

        /// <summary>A service application host.</summary>
        /// <seealso cref="AppHostBase"/>
        public class ServiceAppHost : AppHostBase
        {
            /// <summary>Initializes a new instance of the <see cref="ServiceAppHost"/> class.</summary>
            public ServiceAppHost()
                : base(CEFConfigDictionary.APIName, typeof(CEFSharedServiceBase).Assembly)
            {
            }

            /// <summary>Initializes a new instance of the <see cref="ServiceAppHost"/> class.</summary>
            /// <param name="apiName"> Name of the API.</param>
            /// <param name="assembly">The assembly.</param>
            protected ServiceAppHost(string apiName, Assembly assembly)
                : base(apiName, assembly)
            {
            }

            /// <inheritdoc/>
            public override void Configure(Funq.Container container)
            {
                container.Register<IAppSettings>(new AppSettings());
                ContentTypes.Register(
                    contentType: "application/json",
                    responseSerializer: (req, response, stream) =>
                    {
                        var toJson = JsonConvert.SerializeObject(response, ServiceAppCommon.ResponseSettings);
                        stream.Write(toJson);
                    },
                    streamDeserializer: (type, stream) =>
                    {
                        using var reader = new StreamReader(stream);
                        var json = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject(json, type, ServiceAppCommon.ResponseSettings);
                    });
                ServiceAppCommon.Configure(this, container);
            }

            /// <summary>Creates service controller.</summary>
            /// <param name="assembliesWithServices">A variable-length parameters list containing assemblies with services.</param>
            /// <returns>The new service controller.</returns>
            protected override ServiceController CreateServiceController(params Assembly[] assembliesWithServices)
            {
                var assemblies = new List<Assembly>(assembliesWithServices);
                var serviceHandlerClasses = new List<Type>(ServiceHandlerClassesLoadedFromAssemblies(assembliesWithServices));
                serviceHandlerClasses.AddRange(ServiceHandlerClassesLoadedFromPlugins());
                serviceHandlerClasses.AddRange(ServiceHandlerClassesLoadedFromClients());
                serviceHandlerClasses = serviceHandlerClasses.Distinct().ToList();
                return serviceHandlerClasses.Count > 0
                    ? new(this, () => serviceHandlerClasses)
                    : base.CreateServiceController(assemblies.ToArray());
            }

            /// <summary>Enumerates service handler classes loaded from plugins in this collection.</summary>
            /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from plugins
            /// in this collection.</returns>
            protected IEnumerable<Type> ServiceHandlerClassesLoadedFromPlugins()
            {
                return ServiceHandlerClassesLoadedFromPath(CEFConfigDictionary.PluginsPath);
            }

            /// <summary>Enumerates service handler classes loaded from clients in this collection.</summary>
            /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from clients
            /// in this collection.</returns>
            protected IEnumerable<Type> ServiceHandlerClassesLoadedFromClients()
            {
                return ServiceHandlerClassesLoadedFromPath(CEFConfigDictionary.ClientsPath);
            }

            /// <summary>Enumerates service handler classes loaded from path in this collection.</summary>
            /// <param name="originalPath">Full pathname of the original file.</param>
            /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from path in
            /// this collection.</returns>
            protected virtual IEnumerable<Type> ServiceHandlerClassesLoadedFromPath(string originalPath)
            {
                var location = string.IsNullOrWhiteSpace(originalPath)
                    ? @"{CEF_RootPath}\"
                    : originalPath;
                if (location.Contains("{CEF_RootPath}"))
                {
                    if (location.Contains(@"{CEF_RootPath}\") && Globals.CEFRootPath.EndsWith("\\"))
                    {
                        location = location.Replace(@"{CEF_RootPath}\", Globals.CEFRootPath);
                    }
                    else
                    {
                        location = location.Replace("{CEF_RootPath}", Globals.CEFRootPath);
                    }
                }
                return Directory.Exists(location)
                    ? ServiceHandlerClassesLoadedFromAssemblies(Directory.GetFiles(location, "Clarity.*.dll").Select(Assembly.LoadFrom))
                    : new List<Type>();
            }

            /// <summary>Enumerates service handler classes loaded from assemblies in this collection.</summary>
            /// <param name="assemblies">The assemblies.</param>
            /// <returns>An enumerator that allows foreach to be used to process service handler classes loaded from
            /// assemblies in this collection.</returns>
            protected virtual IEnumerable<Type> ServiceHandlerClassesLoadedFromAssemblies(IEnumerable<Assembly> assemblies)
            {
                return assemblies
                    .SelectMany(x => x.GetTypes()
                        .Where(ServiceController.IsServiceType)
                        .Distinct());
            }
        }
    }
}
