// <copyright file="Global.asax.cs" company="clarity-ventures.com">
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
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;
    using DataModel;
    using Funq;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using JetBrains.Annotations;
    using JSConfigs;
    using Newtonsoft.Json;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Caching;
    using ServiceStack.Host;
    using ServiceStack.Redis;
    using ServiceStack.Text;
    using ServiceStack.Web;
    using Utilities;

    /// <summary>A service application common.</summary>
    public static class ServiceAppCommonManufacturerAdmin
    {
        private static IManufacturerWorkflow ManufacturerWorkflow { get; } = RegistryLoaderWrapper.GetInstance<IManufacturerWorkflow>();

        private static IEventLogWorkflow EventLogWorkflow { get; } = RegistryLoaderWrapper.GetInstance<IEventLogWorkflow>();

        private static IUserWorkflow UserWorkflow { get; } = RegistryLoaderWrapper.GetInstance<IUserWorkflow>();

        private static HandleServiceExceptionDelegate ServiceExceptionHandler => (httpReq, _, ex1) =>
        {
            // log your exceptions here
            var logID = string.Empty;
            try
            {
                logID = WriteEventLogEntry(
                    $"Service Exception Handler: {ex1.GetType().Name}",
                    Logger.LogLevels.Error,
                    new
                    {
                        httpReq.UserHostAddress,
                        httpReq.Dto,
                        Exception = ex1,
                    });
                if (OverrideResponseIfCoded(ex1, logID, out var retVal))
                {
                    return retVal;
                }
            }
            catch (Exception ex2)
            {
                if (OverrideResponseIfCoded(ex2, logID, out var retVal))
                {
                    return retVal;
                }
            }
            // Continue with default Error Handling
            return null;
            // or return your own custom response
            // return DtoUtils.CreateErrorResponse(request, exception);
        };

        private static HandleUncaughtExceptionDelegate UncaughtExceptionHandler => (req, res, operationName, ex1) =>
        {
            if (ex1 is HttpError error)
            {
                // If it's a basic 401 or 403 type error, just write basic error response info
                res.StatusCode = error.Status;
                res.StatusDescription = error.StatusDescription;
                res.Write(
                    JsonConvert.SerializeObject(
                        new
                        {
                            res.StatusCode,
                            res.StatusDescription,
                            error.Message,
                        }));
                res.EndRequest(skipHeaders: true);
                return;
            }
            var logID = string.Empty;
            try
            {
                logID = WriteEventLogEntry(
                    $"Uncaught Exception Handler: {ex1.GetType().Name}",
                    Logger.LogLevels.Error,
                    new
                    {
                        req.UserHostAddress,
                        req.Dto,
                        OperationName = operationName,
                        Exception = ex1,
                    });
                if (OverrideResponseIfCoded(res, ex1, logID))
                {
                    return;
                }
            }
            catch (Exception ex2)
            {
                if (OverrideResponseIfCoded(res, ex2, logID))
                {
                    return;
                }
            }
            res.Write(JsonConvert.SerializeObject(new ErrorCodeWithMessage(ex1.GetType().Name, logID, ex1.Message)));
            res.EndRequest(skipHeaders: true);
        };

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
            ClarityEcommerceServiceBase.CurrentAPIKind = Enums.APIKind.ManufacturerAdmin;
            // Redis Caching for endpoint calls
            SetupRedis(container, appHost);
            // Allow cross-site scripting for stand-alone development
            SetupCORS(appHost);
            // Makes it easier to use Postman
            SetupPostman(appHost);
            // Set up authentication methods
            SetupAuthFeature(appHost, rawProviders);
            // Enable cancellable requests so we can stop long-running requests
            appHost.Plugins.Add(new CancellableRequestsFeature());
            // Request Filters
            SetupRequestFilters(appHost);
            // Setup Metadata
            SetupMetadata(appHost);
            // Use ISO-8601 yyyy-MM-ddThh:mm:ss.0000000 format Date Strings in JSON instead of /Date(...)/ format
            JsConfig.DateHandler = DateHandler.ISO8601;
            // Never output __type properties onto the serialized content
            JsConfig.IncludeTypeInfo = false;
            // Strict mode means you can't return a bool, string or int (you have to use value types, like CEFActionResponse)
            appHost.Config.StrictMode = false;
            // Load the mapper Expression Func Definitions
            Mapper.BaseModelMapper.Initialize();
            // Load Custom Plugins (CEF Providers that have services in them)
            LoadPlugins(appHost);
        }

        private static void SetupMetadata(IAppHost appHost)
        {
            if (CEFConfigDictionary.APIDisableMetadata)
            {
                appHost.Config.EnableFeatures = Feature.All.Remove(Feature.Metadata);
            }
        }

        private static bool OverrideResponseIfCoded(Exception ex, string logID, out object? retVal)
        {
            var code = ex.ConvertRawExceptionToErrorCode(logID);
            if (code is ErrorCodeWithMessage codeWithMessage)
            {
                retVal = DtoUtils.CreateErrorResponse(
                    codeWithMessage.Code,
                    $"{(Contract.CheckValidKey(codeWithMessage.LogID) ? $"Log ID: {codeWithMessage.LogID} | " : string.Empty)}{codeWithMessage.Message}",
                    null);
                return true;
            }
            retVal = null;
            return false;
        }

        private static bool OverrideResponseIfCoded(IResponse res, Exception ex, string logID)
        {
            var code = ex.ConvertRawExceptionToErrorCode(logID);
            if (code is not ErrorCodeWithMessage codeWithMessage)
            {
                return false;
            }
            res.Write(JsonConvert.SerializeObject(codeWithMessage));
            res.EndRequest(skipHeaders: true);
            return true;
        }

        private static void SetupRedis(Container container, IAppHost appHost)
        {
            if (!CEFConfigDictionary.CachingRedisEnabled)
            {
                return;
            }
            // See https://github.com/ServiceStack/ServiceStack.Redis
            string ConnectionStringFromAppSettings()
            {
                var connectionString = new StringBuilder(CEFConfigDictionary.CachingRedisHostUri);
                if (Contract.CheckValidID(CEFConfigDictionary.CachingRedisHostPort))
                {
                    connectionString.Append(":").Append(CEFConfigDictionary.CachingRedisHostPort);
                }
                var queryParams = new NameValueCollection();
                if (Contract.CheckValidKey(CEFConfigDictionary.CachingRedisUsername))
                {
                    queryParams.Add("username", CEFConfigDictionary.CachingRedisUsername);
                }
                if (Contract.CheckValidKey(CEFConfigDictionary.CachingRedisPassword))
                {
                    queryParams.Add("password", CEFConfigDictionary.CachingRedisPassword);
                }
                if (CEFConfigDictionary.CachingRedisRequiredSSL)
                {
                    queryParams.Add("ssl", true.ToString());
                }
                if (CEFConfigDictionary.CachingRedisAbortConnect)
                {
                    queryParams.Add("abortConnect", true.ToString());
                }
                var queryParamsArray = (
                        from key in queryParams.AllKeys
                        from value in queryParams.GetValues(key)
                        select $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}")
                    .ToArray();
                var queryString = string.Join("&", queryParamsArray);
                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    connectionString.Append('?').Append(queryString);
                }
                return connectionString.ToString();
            }
            var redisHost = ConnectionStringFromAppSettings();
            if (string.IsNullOrWhiteSpace(redisHost))
            {
                throw new ConfigurationErrorsException("Redis is enabled but does not have the Host setting set up.");
            }
            var prefix = $"CEFCache:{CEFConfigDictionary.SiteRouteHostUrl?.Replace("http://", string.Empty).Replace("https://", string.Empty)}:";
            container.Register<IRedisClientsManager>(_ => new PooledRedisClientManager(redisHost));
            container.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient().WithPrefix(prefix));
            appHost.Plugins.RemoveAll(x => x is HttpCacheFeature);
            appHost.Plugins.Add(new Core.CEFHttpCacheFeature
            {
                // CacheControlForOptimizedResults = "max-age=1800, must-revalidate",
                CacheControlForOptimizedResults = "max-age=61, must-revalidate, public",
                DefaultMaxAge = TimeSpan.FromSeconds(30/*1800*/),
                // DefaultExpiresIn = TimeSpan.FromSeconds(/*3600*/),
            });
        }

        private static void SetupPostman(IAppHost appHost)
        {
            appHost.Plugins.Add(new PostmanFeature
            {
                EnableSessionExport = true,
                DefaultLabelFmt = new() { "type:english", " ", "route" },
                Headers = "Accept: application/json\nContent-Type: application/json",
                FriendlyTypeNames = { { "DateTime", "Date" } },
            });
        }

        private static void SetupCORS(IAppHost appHost)
        {
            // Cookie sharing between sub-domains
            if (Contract.CheckValidKey(CEFConfigDictionary.CookiesDomain))
            {
                appHost.Config.RestrictAllCookiesToDomain = CEFConfigDictionary.CookiesDomain.Trim();
            }
            appHost.Config.AllowSessionIdsInHttpParams = false;
            if (CEFConfigDictionary.CookiesRequireSecure)
            {
                appHost.Config.OnlySendSessionCookiesSecurely = true;
            }
            appHost.Config.AllowNonHttpOnlyCookies = !CEFConfigDictionary.CookiesRequireHTTPOnly;
            appHost.Config.UseHttpsLinks = true;
            appHost.Config.GlobalResponseHeaders.Remove(HttpHeaders.Vary);
            appHost.Config.GlobalResponseHeaders.Add(HttpHeaders.Vary, "accept,origin");
            appHost.Plugins.Add(new CorsFeature(
                allowCredentials: true,
                maxAge: -1,
                allowedMethods: "DELETE, GET, OPTIONS, PATCH, POST, PUT",
                // ReSharper disable once StyleCop.SA1118
                allowedHeaders: "Accept, Accept-Encoding, Accept-Language, Cache-Control, Connection, Content-Length, Content-Type, DNT, Origin, Pragma, Referer, X-Requested-With, X-ss-pid, X-ss-id, X-ss-opt, X-uaid, Vary",
                // ReSharper disable once StyleCop.SA1118
                allowOriginWhitelist: CEFConfigDictionary.ServiceStackOriginsWhiteList ?? new[] { "*" }));
        }

        private static void SetupAuthFeature(ServiceStackHost appHost, IEnumerable<string> rawProviders)
        {
            var providers = new List<IAuthProvider>();
            static IAuthSession SessionFactory() => RegistryLoaderWrapper.GetInstance<ICMSAuthUserSession>();
            var includeRegistrationService = false;
            foreach (var rawProvider in rawProviders)
            {
                switch (rawProvider.Trim().ToLower())
                {
                    // ReSharper disable StyleCop.SA1107
                    case "cef":
                    case "identity":
                    {
                        providers.Add(new AspNetIdentityAuthProvider());
                        includeRegistrationService = true;
                        var repo = new AspNetIdentityUserAuthRepository();
                        appHost.Container.Register<IAuthRepository>(repo);
                        appHost.Container.Register(repo);
                        break;
                    }
                    case "dnnsso":
                    case "dotnetnukesso":
                    {
                        providers.Add(new DotNetNukeSSOAuthProvider());
                        var repo = new DotNetNukeSSOUserAuthRepository();
                        appHost.Container.Register<IAuthRepository>(repo);
                        appHost.Container.Register(repo);
                        break;
                    }
                    case "cobalt":
                    {
                        providers.Add(new CobaltAuthProvider());
                        var repo = new CobaltUserAuthRepository();
                        appHost.Container.Register<IAuthRepository>(repo);
                        appHost.Container.Register(repo);
                        break;
                    }
                    case "tokenized":
                    {
                        providers.Add(new TokenizedCredentialsAuthProvider());
                        break;
                    }
                    case "openid":
                    {
                        providers.Add(new OpenIDAuthProvider());
                        includeRegistrationService = true;
                        var repo = new OpenIDUserAuthRepository();
                        appHost.Container.Register<IAuthRepository>(repo);
                        appHost.Container.Register(repo);
                        break;
                    }
                    case "okta":
                    {
                        providers.Add(new OktaAuthProvider());
                        includeRegistrationService = true;
                        var repo = new OktaUserAuthRepository();
                        appHost.Container.Register<IAuthRepository>(repo);
                        appHost.Container.Register(repo);
                        break;
                    }
                    default:
                    {
                        throw new ConfigurationErrorsException(
                            $"Authentication Provider '{rawProvider}' isn't valid, please contact the administrator");
                    }
                }
            }
            var authFeature = new AuthFeature(SessionFactory, providers.ToArray())
            {
                HtmlRedirect = null,
                IncludeRegistrationService = includeRegistrationService,
                GenerateNewSessionCookiesOnAuthentication = false,
                DeleteSessionCookiesOnLogout = true,
            };
            appHost.Plugins.Add(authFeature);
        }

        private static void SetupRequestFilters(IAppHost appHost)
        {
            // Allows the body of the request to be read multiple times, no performance impact
            appHost.PreRequestFilters.Insert(0, (httpReq, _) => httpReq.UseBufferedStream = true);
            appHost.PreRequestFilters.Add(
                (_, res) =>
                {
                    var aspRes = (HttpResponseBase)res.OriginalResponse;
                    aspRes.SuppressFormsAuthenticationRedirect = true;
                });
            if (CEFConfigDictionary.LogEveryRequest)
            {
                appHost.GlobalRequestFiltersAsync.Add(HipaaRequestFilter());
            }
            appHost.GlobalRequestFiltersAsync.Add(SecureRequestFilter());
            appHost.GlobalRequestFiltersAsync.Add(SSOSessionLockRequestFilter());
            if (CEFConfigDictionary.APIRequestsValidateManufacturer)
            {
                appHost.GlobalRequestFiltersAsync.Add(ManufacturerAdminsRequestFilter());
            }
            // TODO: Correct the assignment issue that looses the real value appHost.GlobalRequestFiltersAsync.Add(LanguageRequestFilterAsync());
            appHost.GlobalRequestFiltersAsync.Add(CurrencyRequestFilter());
            appHost.ServiceExceptionHandlers.Add(ServiceExceptionHandler);
            appHost.UncaughtExceptionHandlers.Add(UncaughtExceptionHandler);
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

        private static Func<IRequest, IResponse, object, Task> HipaaRequestFilter()
        {
            return (req, _, _) => Task.Run(
                () => WriteEventLogEntry(
                    "Log HIPAA Info",
                    Logger.LogLevels.Information,
                    new { req.UserHostAddress, req.Dto }));
        }

        private static Func<IRequest, IResponse, object, Task> SecureRequestFilter()
        {
            return (req, res, dto) => Task.Run(
                async () =>
                {
                    // Validate SSL if set
                    if (CEFConfigDictionary.APIRequestsRequireHTTPS && !req.IsSecureConnection)
                    {
                        // Requests are required to be run over SSL
                        WriteEventLogEntry(
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
                        res.SetCookie(MakeACookie(
                            key,
                            "0",
                            req.Cookies[key].Expires > DateTime.Now ? req.Cookies[key].Expires : DateTime.Now.AddMinutes(30)));
                    }
                    foreach (var key in cookiesToSetToOne)
                    {
                        res.SetCookie(MakeACookie(
                            key,
                            "1",
                            req.Cookies[key].Expires > DateTime.Now ? req.Cookies[key].Expires : DateTime.Now.AddMinutes(30)));
                    }
                });
        }

        private static Cookie MakeACookie(string name, string value, DateTime? expires = null)
        {
            var cookie = new Cookie
            {
                Name = name,
                Value = value,
                Path = Contract.CheckValidKey(CEFConfigDictionary.CookiesPath) ? CEFConfigDictionary.CookiesPath : "/",
                Domain = HostConfig.Instance.RestrictAllCookiesToDomain,
            };
            if (expires.HasValue)
            {
                cookie.Expires = expires.Value;
            }
            if (CEFConfigDictionary.CookiesRequireSecure)
            {
                cookie.Secure = true;
            }
            cookie.HttpOnly = CEFConfigDictionary.CookiesRequireHTTPOnly;
            return cookie;
        }

        private static Func<IRequest, IResponse, object, Task> SSOSessionLockRequestFilter()
        {
            return (_, _, dto) => Task.Run(
                () =>
                {
                    if (dto is not Authenticate authDTO)
                    {
                        return;
                    }
                    authDTO.RememberMe = true;
                });
        }

        private static Func<IRequest, IResponse, object, Task> ManufacturerAdminsRequestFilter()
        {
            return (req, _, dto) => Task.Run(
                async () =>
                {
                    try
                    {
                        if (dto is not IAmManufacturerAdminModified)
                        {
                            return;
                        }
                        var start = DateExtensions.GenDateTime;
                        var timeoutAt = start.AddSeconds(60);
                        int? manufacturerID = null;
                        while (DateExtensions.GenDateTime < timeoutAt)
                        {
                            try
                            {
                                var response = await ManufacturerWorkflow.GetIDByAssignedUserIDAsync(
                                        int.Parse(req.GetSession().UserAuthId),
                                        null)
                                    .ConfigureAwait(false);
                                manufacturerID = response.Result;
                                break;
                            }
                            catch
                            {
                                // Probably still spinning up, try again shortly
                                System.Threading.Thread.Sleep(250);
                            }
                        }
                        if (Contract.CheckInvalidID(manufacturerID))
                        {
                            throw HttpError.NotFound("Could not find a Manufacturer tied to this User");
                        }
                        req.Headers["CEF-Manufacturer-Admin-ID"] = manufacturerID.ToString();
                        switch (dto)
                        {
                            case IAmManufacturerAdminModified dto2:
                            {
                                dto2.IsManufacturerAdmin = true;
                                dto2.ManufacturerAdminID = manufacturerID;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        // Do Nothing
                    }
                });
        }

        [PublicAPI]
        private static Func<IRequest, IResponse, object, Task> LanguageRequestFilter()
        {
            return (req, res, _) =>
            {
                var session = req.GetSession();
                if (!session.IsAuthenticated)
                {
                    return Task.CompletedTask;
                }
                var cookieValue = req.GetItemOrCookie("NG_TRANSLATE_LANG_KEY");
                if (string.IsNullOrWhiteSpace(cookieValue))
                {
                    cookieValue = $"%22{CEFConfigDictionary.DefaultLanguage}%22";
                    res.SetPermanentCookie("NG_TRANSLATE_LANG_KEY", cookieValue);
                }
                var username = session.UserName;
                var key = cookieValue.Replace("%22", string.Empty);
                return UserWorkflow.UpsertSelectedLanguageAsync(username, key, null);
            };
        }

        private static Func<IRequest, IResponse, object, Task> CurrencyRequestFilter()
        {
            return (req, res, _) =>
            {
                var session = req.GetSession();
                if (!session.IsAuthenticated)
                {
                    return Task.CompletedTask;
                }
                if (string.IsNullOrWhiteSpace(req.GetItemOrCookie("CURRENCY_KEY")))
                {
                    res.SetPermanentCookie("CURRENCY_KEY", CEFConfigDictionary.DefaultCurrency);
                }
                var username = session.UserName;
                var key = req.GetItemOrCookie("CURRENCY_KEY");
                return UserWorkflow.UpsertSelectedCurrencyAsync(username, key, null);
            };
        }

        private static void LoadPlugins(IAppHost appHost)
        {
            var pluginsLocation = CEFConfigDictionary.PluginsPath;
            if (!string.IsNullOrWhiteSpace(CEFConfigDictionary.PluginsPath))
            {
                pluginsLocation = CEFConfigDictionary.PluginsPath;
            }
            if (pluginsLocation.Contains("{CEF_RootPath}"))
            {
                pluginsLocation = pluginsLocation.Replace("{CEF_RootPath}", Globals.CEFRootPath);
            }
            if (!Directory.Exists(pluginsLocation))
            {
                return;
            }
            var assembliesToAdd = new List<Assembly>();
            foreach (var dll in Directory.GetFiles(pluginsLocation, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(dll);
                assembliesToAdd.Add(assembly);
                var types = assembly.GetTypes()
                    .Where(x => x.HasInterface(typeof(IPlugin)));
                foreach (var type in types)
                {
                    appHost.Plugins.Add((IPlugin)Activator.CreateInstance(type));
                }
            }
            appHost.AddPluginsFromAssembly(assembliesToAdd.ToArray());
        }
    }
}
