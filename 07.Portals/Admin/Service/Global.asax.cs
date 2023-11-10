﻿// <copyright file="Global.asax.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the global class</summary>
// ReSharper disable UnusedParameter.Local
#nullable enable
#pragma warning disable SA1649 // File name should match first type name
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;
    using DataModel;
    using Funq;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using JSConfigs;
    using Newtonsoft.Json;
    using ServiceStack;
    using ServiceStack.Text;
    using ServiceStack.Web;
    using Utilities;

    /// <summary>A service application common.</summary>
    public static class ServiceAppCommonAdmin
    {
        private static IEventLogWorkflow EventLogWorkflow { get; } = RegistryLoaderWrapper.GetInstance<IEventLogWorkflow>();

        private static IUserWorkflow UserWorkflow { get; } = RegistryLoaderWrapper.GetInstance<IUserWorkflow>();

        /// <summary>Configures the ServiceStack application startup actions.</summary>
        /// <exception cref="ConfigurationErrorsException">Thrown when a Configuration Errors error condition occurs.</exception>
        /// <param name="appHost">  The application host.</param>
        /// <param name="container">The container.</param>
        public static void Configure(ServiceStackHost appHost, Container container)
        {
            // Validate web.appSettings.config Configuration
            if (!Contract.CheckValidKey(CEFConfigDictionary.AuthProviders))
            {
                throw new ConfigurationErrorsException(
                    "AuthProviders isn't set, please contact the administrator to add this app setting to the web config.");
            }
            var rawProviders = CEFConfigDictionary.AuthProviders.ToLower().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (rawProviders.Length == 0)
            {
                throw new ConfigurationErrorsException(
                    "The AuthProviders setting is invalid, please contact the administrator to add this app setting to the web config.");
            }
            if (!Contract.CheckValidKey(CEFConfigDictionary.SiteRouteHostUrl))
            {
                throw new ConfigurationErrorsException(
                    "SiteRootUrl isn't set, please contact the administrator to add this app setting to the web config.");
            }
            DateExtensions.UseUtc = CEFConfigDictionary.APIUseUTC;
            container.Adapter = new StructureMapContainerAdapter();
            // Redis Caching for endpoint calls
            ServiceAppCommon.SetupRedis(container, appHost);
            // Allow cross-site scripting for stand-alone development
            ServiceAppCommon.SetupCORS(appHost);
            // Makes it easier to use Postman
            ServiceAppCommon.SetupPostman(appHost);
            // Set up authentication methods
            ServiceAppCommon.SetupAuthFeature(appHost, rawProviders);
            // Enable cancellable requests so we can stop long-running requests
            appHost.Plugins.Add(new CancellableRequestsFeature());
            // Request Filters
            SetupRequestFilters(appHost);
            // Setup Metadata
            ServiceAppCommon.SetupMetadata(appHost);
            // Use ISO-8601 yyyy-MM-ddThh:mm:ss.0000000 format Date Strings in JSON instead of /Date(...)/ format
            JsConfig.DateHandler = DateHandler.ISO8601;
            // Never output __type properties onto the serialized content
            JsConfig.IncludeTypeInfo = false;
            // Strict mode means you can't return a bool, string or int (you have to use value types, like CEFActionResponse)
            appHost.Config.StrictMode = false;
            // Load the mapper Expression Func Definitions
            Mapper.BaseModelMapper.Initialize();
            // Load Custom Plugins (CEF Providers that have services in them)
            ServiceAppCommon.LoadPlugins(appHost);
        }

        private static void SetupRequestFilters(IAppHost appHost)
        {
            // Allows the body of the request to be read multiple times, no performance impact
            appHost.PreRequestFilters.Insert(0, (httpReq, httpRes) => httpReq.UseBufferedStream = true);
            appHost.PreRequestFilters.Add(
                (req, res) =>
                {
                    var aspRes = (HttpResponseBase)res.OriginalResponse;
                    aspRes.SuppressFormsAuthenticationRedirect = true;
                });
            if (CEFConfigDictionary.LogEveryRequest)
            {
                appHost.GlobalRequestFiltersAsync.Add(ServiceAppCommon.HipaaRequestFilter());
            }
            appHost.GlobalRequestFiltersAsync.Add(SecureRequestFilterAsync());
            appHost.GlobalRequestFiltersAsync.Add(ServiceAppCommon.SSOSessionLockRequestFilter());
            // TODO: Correct the assignment issue that looses the real value appHost.GlobalRequestFiltersAsync.Add(LanguageRequestFilterAsync());
            appHost.GlobalRequestFiltersAsync.Add(ServiceAppCommon.CurrencyRequestFilter());
            appHost.ServiceExceptionHandlers.Add(ServiceAppCommon.ServiceExceptionHandler);
            appHost.UncaughtExceptionHandlers.Add(ServiceAppCommon.UncaughtExceptionHandler);
        }

        private static string WriteEventLogEntry(string name, Logger.LogLevels logLevel, object toSerialize)
        {
            return WriteEventLogEntry(name, (int)logLevel, toSerialize);
        }

        private static string WriteEventLogEntry(string name, int logLevel, object toSerialize)
        {
            var eventLogModel = RegistryLoaderWrapper.GetInstance<IEventLogModel>();
            eventLogModel.Active = true;
            eventLogModel.CreatedDate = DateExtensions.GenDateTime;
            var logID = eventLogModel.CustomKey = "CEF: " + Guid.NewGuid();
            eventLogModel.Name = name;
            eventLogModel.Description = JsonConvert.SerializeObject(
                toSerialize,
                SerializableAttributesDictionaryExtensions.JsonSettings);
            eventLogModel.LogLevel = logLevel;
            EventLogWorkflow.CreateAsync(eventLogModel, contextProfileName: null);
            return logID;
        }

        private static Func<IRequest, IResponse, object, Task> SecureRequestFilterAsync()
        {
            return (req, res, dto) => Task.Run(
                async () =>
                {
                    // Validate SSL if set
                    if (CEFConfigDictionary.APIRequestsRequireHTTPS && !req.IsSecureConnection)
                    {
                        // Requests are required to be run over SSL
                        ServiceAppCommon.WriteEventLogEntry(
                            "Secure Request Filter: A secure connection must be established",
                            Logger.LogLevels.Error,
                            new
                            {
                                req.UserHostAddress,
                                req.Dto,
                            });
                        throw HttpError.Forbidden("A secure connection must be established");
                    }
                    // Validate Secure Cookies if set
                    if (!CEFConfigDictionary.CookiesRequireSecure
                        && !CEFConfigDictionary.CookiesRequireHTTPOnly
                        && !CEFConfigDictionary.CookiesValidateAuthValuesEveryRequest)
                    {
                        return;
                    }
                    if (dto is CurrentUserHasPermission or CurrentUserHasRole or CurrentUserHasAnyPermission or CurrentUserHasAnyRole)
                    {
                        // Don't run for these as they are so numerous and they are taking care of each their own
                        return;
                    }
                    var session = req.GetSession();
                    var cookiesToSetToZero = new List<string>();
                    var cookiesToSetToOne = new List<string>();
                    var roleNamesForUser = Array.Empty<string>();
                    var permissionNamesForUser = Array.Empty<string>();
                    if (session.IsAuthenticated && CEFConfigDictionary.CookiesValidateAuthValuesEveryRequest)
                    {
                        using var context = RegistryLoaderWrapper.GetContext(null);
                        using var userStore = new CEFUserStore(context);
                        using var userManager = new CEFUserManager(userStore);
                        roleNamesForUser = await userManager.GetRoleNamesForUserAsync(int.Parse(session.UserAuthId)).ConfigureAwait(false);
                        permissionNamesForUser = await userManager.GetPermissionNamesForUserAsync(int.Parse(session.UserAuthId)).ConfigureAwait(false);
                    }
                    foreach (var cookie in req.Cookies)
                    {
                        if (CEFConfigDictionary.CookiesIgnoredNonCEF?.Contains(cookie.Key) == true)
                        {
                            continue;
                        }
                        if (CEFConfigDictionary.CookiesRequireSecure
                            && !cookie.Value.Secure
                            && CEFConfigDictionary.CookiesRequireSecureIgnoredCEF?.Contains(cookie.Key) == false)
                        {
                            WriteEventLogEntry(
                                "Secure Request Filter: An insecure cookie was detected",
                                Logger.LogLevels.Error,
                                new
                                {
                                    req.UserHostAddress,
                                    req.Dto,
                                    Name = cookie.Key,
                                    cookie,
                                });
                            throw HttpError.Forbidden("An insecure cookie was detected " + cookie.Key);
                        }
                        if (CEFConfigDictionary.CookiesRequireHTTPOnly
                            && !cookie.Value.HttpOnly
                            && CEFConfigDictionary.CookiesRequireHTTPOnlyIgnoredCEF?.Contains(cookie.Key) == false)
                        {
                            WriteEventLogEntry(
                                "Secure Request Filter: An insecure cookie was found",
                                Logger.LogLevels.Error,
                                new
                                {
                                    req.UserHostAddress,
                                    req.Dto,
                                    Name = cookie.Key,
                                    cookie,
                                });
                            throw HttpError.Forbidden("An insecure cookie was found " + cookie.Key);
                        }
                        if (!CEFConfigDictionary.CookiesValidateAuthValuesEveryRequest
                            || !cookie.Key.StartsWith("cef_"))
                        {
                            continue;
                        }
                        if (!session.IsAuthenticated && cookie.Value.Value == "1")
                        {
                            // Record that we need to set the value to 0 after this loop, we don't need to throw for this
                            cookiesToSetToZero.Add(cookie.Key);
                            continue;
                        }
                        var has = false;
                        if (cookie.Key.StartsWith(Globals.HasRoleCookiePrefix))
                        {
                            has = roleNamesForUser.Contains(
                                cookie.Key[Globals.HasRoleCookiePrefix.Length..].Replace("%20", " "));
                        }
                        else if (cookie.Key.StartsWith(Globals.HasAnyRoleCookiePrefix))
                        {
                            var regex = new Regex(cookie.Key[Globals.HasAnyRoleCookiePrefix.Length..].Trim('/'));
                            has = roleNamesForUser.Any(regex.IsMatch);
                        }
                        else if (cookie.Key.StartsWith(Globals.HasPermissionCookiePrefix))
                        {
                            has = permissionNamesForUser.Contains(
                                cookie.Key[Globals.HasPermissionCookiePrefix.Length..].Replace("%20", " "));
                        }
                        else if (cookie.Key.StartsWith(Globals.HasAnyPermissionCookiePrefix))
                        {
                            var regex = new Regex(cookie.Key[Globals.HasAnyPermissionCookiePrefix.Length..].Trim('/'));
                            has = permissionNamesForUser.Any(regex.IsMatch);
                        }
                        if (has && cookie.Value.Value == "1"
                            || !has && cookie.Value.Value == "0")
                        {
                            // They both say true or false
                            continue;
                        }
                        if (has && cookie.Value.Value == "0")
                        {
                            // Record that we need to set the value to 1 after this loop, we don't need to throw for this
                            cookiesToSetToOne.Add(cookie.Key);
                            continue;
                        }
                        if (!has && cookie.Value.Value == "1")
                        {
                            // Record that we need to set the value to 0 after this loop, we don't need to throw for this
                            cookiesToSetToZero.Add(cookie.Key);
                        }
                    }
                    foreach (var key in cookiesToSetToZero)
                    {
                        res.SetCookie(ServiceAppCommon.MakeACookie(
                            key,
                            "0",
                            req.Cookies[key].Expires > DateTime.Now ? req.Cookies[key].Expires : DateTime.Now.AddMinutes(30)));
                    }
                    foreach (var key in cookiesToSetToOne)
                    {
                        res.SetCookie(ServiceAppCommon.MakeACookie(
                            key,
                            "1",
                            req.Cookies[key].Expires > DateTime.Now ? req.Cookies[key].Expires : DateTime.Now.AddMinutes(30)));
                    }
                });
        }
    }
}
