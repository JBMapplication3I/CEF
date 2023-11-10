// <copyright file="ServiceApplicationStorefront.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the service application storefront class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using JetBrains.Annotations;
    using JSConfigs;
    using Newtonsoft.Json;
    using ServiceStack;
    using ServiceStack.Configuration;
    using ServiceStack.Host;
    using ServiceStack.Logging;
    using ServiceStack.Logging.Log4Net;
    using ServiceStack.Serialization;
    using ServiceStack.Text;
    using ServiceStack.Web;

    /// <summary>A service application storefront.</summary>
    /// <seealso cref="ServiceApplication"/>
    [PublicAPI]
    public class ServiceApplicationStorefront : ServiceApplication
    {
        private ServiceAppHostStorefront serviceStackHost = null!;

        /// <summary>Application start.</summary>
        protected override void Application_Start()
        {
            try
            {
                LogManager.LogFactory = new Log4NetFactory(true);
                CEFConfigDictionary.Load();
                /* Update to the latest migration on first initialization
                 !!! Don't do this with storefront, let the admin API deal with it. Don't want more than one API trying to upgrade at the same time
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ClarityEcommerceEntities, Configuration>());
                var configuration = new Configuration();
                var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
                if (migrator.GetPendingMigrations().Any())
                {
                    migrator.Update();
                }
                */
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

        /// <summary>A service application host storefront.</summary>
        /// <seealso cref="ServiceApplication.ServiceAppHost"/>
        public class ServiceAppHostStorefront : ServiceAppHost
        {
            /// <summary>Initializes a new instance of the <see cref="ServiceAppHostStorefront"/> class.</summary>
            public ServiceAppHostStorefront()
                : base(CEFConfigDictionary.APIName, typeof(CEFSharedServiceBase).Assembly)
            {
            }

            /// <inheritdoc/>
            public override void Configure(Funq.Container container)
            {
                container.Register<IAppSettings>(new AppSettings());
                // ContentTypes.Register(
                //     contentType: "application/json",
                //     responseSerializer: (req, response, stream) =>
                //     {
                //         var toJson = JsonConvert.SerializeObject(response, ServiceAppCommon.ResponseSettings);
                //         stream.Write(toJson);
                //     },
                //     streamDeserializer: (type, stream) =>
                //     {
                //         using var reader = new StreamReader(stream);
                //         var json = reader.ReadToEnd();
                //         return JsonConvert.DeserializeObject(json, type);
                //     });
                ServiceAppCommonStorefront.Configure(this, container);
            }

            /// <summary>Creates service controller.</summary>
            /// <param name="assembliesWithServices">A variable-length parameters list containing assemblies with services.</param>
            /// <returns>The new service controller.</returns>
            protected override ServiceController CreateServiceController(params Assembly[] assembliesWithServices)
            {
                var serviceHandlerClasses = new List<Type>(ServiceHandlerClassesLoadedFromAssemblies(assembliesWithServices));
                serviceHandlerClasses.AddRange(ServiceHandlerClassesLoadedFromPlugins());
                serviceHandlerClasses.AddRange(ServiceHandlerClassesLoadedFromClients());
                serviceHandlerClasses = serviceHandlerClasses.Distinct().ToList();
                if (serviceHandlerClasses.Count == 0)
                {
                    return base.CreateServiceController(assembliesWithServices);
                }
                // Pass in an empty list, because we have to filter the endpoint handlers manually
                var controller = new ServiceController(this, () => new List<Type>());
                var typeFactory = new ContainerResolveCache(Container);
                foreach (var serviceHandlerClass in serviceHandlerClasses.Distinct())
                {
                    RegisterService(controller, this, typeFactory, serviceHandlerClass);
                }
                return controller;
            }

            /// <summary>Registers the service.</summary>
            /// <param name="controller">      The controller.</param>
            /// <param name="appHost">         The application host.</param>
            /// <param name="serviceFactoryFn">The service factory function.</param>
            /// <param name="serviceType">     Type of the service.</param>
            protected void RegisterService(
                ServiceController controller,
                ServiceAppHostStorefront appHost,
                ITypeFactory serviceFactoryFn,
                Type serviceType)
            {
                if (!ServiceController.IsServiceType(serviceType))
                {
                    return;
                }
                var typeSet = new HashSet<Type>();
                foreach (var action in serviceType.GetActions())
                {
                    var requestType = action.GetParameters()[0].ParameterType;
                    if (typeSet.Contains(requestType))
                    {
                        // All routes on a single class are processed at the same time, so multiple handlers with the
                        // same class passed in for different verbs don't need to be reprocessed
                        continue;
                    }
                    typeSet.Add(requestType);
                    if (!requestType.HasAttribute<UsedInStorefrontAttribute>())
                    {
                        // Don't process endpoints not marked for use in storefront
                        continue;
                    }
                    controller.RegisterServiceExecutor(requestType, serviceType, serviceFactoryFn);
                    var typeDefinitionOf = requestType.GetTypeWithGenericTypeDefinitionOf(typeof(IReturn<>));
                    var responseType = typeDefinitionOf != null
                        ? typeDefinitionOf.GetGenericArguments()[0]
                        : !(action.ReturnType != typeof(object)) || !(action.ReturnType != typeof(void))
                            ? AssemblyUtils.FindType(requestType.FullName + "Response")
                            : action.ReturnType;
                    if (responseType?.Name == "Task`1" && responseType.GetGenericArguments()[0] != typeof(object))
                    {
                        responseType = responseType.GetGenericArguments()[0];
                    }
                    controller.RegisterRestPaths(requestType);
                    appHost.Metadata.Add(serviceType, requestType, responseType);
                    if (typeof(IRequiresRequestStream).IsAssignableFrom(requestType))
                    {
                        controller.RequestTypeFactoryMap[requestType] = req =>
                        {
                            var route = req.GetRoute();
                            var requiresRequestStream = route != null
                                ? (IRequiresRequestStream)RestHandler.CreateRequest(
                                        req,
                                        route,
                                        req.GetRequestParams(),
                                        requestType.CreateInstance())
                                : (IRequiresRequestStream)KeyValueDataContractDeserializer.Instance.Parse(
                                        req.QueryString,
                                        requestType);
                            requiresRequestStream.RequestStream = req.InputStream;
                            return requiresRequestStream;
                        };
                    }
                    /* Log is private
                    if (ServiceController.Log.IsDebugEnabled)
                    {
                        ServiceController.Log.DebugFormat(
                            "Registering {0} service '{1}' with request '{2}'",
                            responseType != (Type)null ? (object)"Reply" : (object)"OneWay",
                            (object)serviceType.GetOperationName(),
                            (object)requestType.GetOperationName());
                    }
                    */
                }
            }
        }
    }
}
