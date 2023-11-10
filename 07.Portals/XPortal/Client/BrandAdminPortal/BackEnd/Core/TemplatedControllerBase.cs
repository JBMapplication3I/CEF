// <copyright file="TemplatedControllerBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
// ReSharper disable InvertIf
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.MVC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Api.Models;
    using Api.Options;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Utilities;

    // type IWaitMessageArg = string | ng.IPromise<string>;
    // type IErrorMessageArg = string | ng.IPromise<string> | ng.IHttpPromiseCallbackArg<ResponseError>;

    /// <summary>A templated controller base.</summary>
    /// <seealso cref="ComponentBase"/>
    public class TemplatedControllerBase : ComponentBase
    {
        /// <summary>Gets or sets the CEF configuration.</summary>
        /// <value>The CEF configuration.</value>
        [Inject]
        protected CEFConfig CEFConfig { get; set; } = null!;

        /// <summary>Gets or sets the state of the view.</summary>
        /// <value>The view state.</value>
        [CascadingParameter]
        protected virtual IViewState ViewState { get; set; } = null!;

        /// <summary>Gets or sets the logger.</summary>
        /// <value>The logger.</value>
        [Inject]
        private ILogger<TemplatedControllerBase> Logger { get; set; } = null!;

        #region Properties
        /// <summary>Gets or sets the portal route.</summary>
        /// <value>The portal route.</value>
        protected PortalRoute? PortalRoute { get; set; }

        /// <summary>Gets or sets the master (the main record this portal is administrating).</summary>
        /// <value>The master.</value>
        /// <remarks>NOTE: Including this here as it has to be repeated in a lot of controls.</remarks>
#if BRANDADMIN
        protected virtual BrandModel? Master { get; set; }
#elif FRANCHISEADMIN
        protected virtual FranchiseModel? Master { get; set; }
#elif MANUFACTURERADMIN
        protected virtual ManufacturerModel? Master { get; set; }
#elif STOREADMIN
        protected virtual StoreModel? Master { get; set; }
#elif VENDORADMIN
        protected virtual VendorModel? Master { get; set; }
#endif
        #endregion

        #region Functions
        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(false);
            // ReSharper disable once ConstantNullCoalescingCondition
            ViewState ??= new ViewState();
        }

        /// <summary>Makes the form dirty.</summary>
        /// <param name="e">     Change event information.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task.</returns>
        public virtual Task MakeDirty(ChangeEventArgs e, string caller)
        {
            ViewState.MakeDirty();
            return Task.CompletedTask;
        }

        /// <summary>Makes the form pristine.</summary>
        /// <returns>A Task.</returns>
        public virtual Task MakePristine()
        {
            ViewState.MakePristine();
            return Task.CompletedTask;
        }

        /// <summary>Sets running.</summary>
        /// <param name="waitMessage">Message describing the wait.</param>
        /// <returns>A Task.</returns>
        public virtual async Task SetRunningAsync(/*IWaitMessageArg*/object? waitMessage = null)
        {
            ClearSuccess();
            ClearError();
            switch (waitMessage)
            {
                case null:
                {
                    return;
                }
                case Task<string> waitMsgTask:
                {
                    // Only apply if it is still running
                    ViewState.waitMessage = ViewState.running
                        ? await waitMsgTask.ConfigureAwait(false)
                        : null;
                    return;
                }
                default:
                {
                    ViewState.waitMessage = waitMessage as string;
                    break;
                }
            }
        }

        /// <summary>Finishes running.</summary>
        /// <param name="hasError">     True if this TemplatedControllerBase has error.</param>
        /// <param name="errorMessage"> Message describing the error.</param>
        /// <param name="errorMessages">The error messages.</param>
        /// <returns>A Task.</returns>
        public virtual async Task FinishRunningAsync(
            bool hasError = false,
            /*IErrorMessageArg*/object? errorMessage = null,
            string[]? errorMessages = null)
        {
            ViewState.running = false;
            ViewState.waitMessage = null;
            if (hasError)
            {
                await SetErrorAsync(errorMessage, errorMessages).ConfigureAwait(false);
                return;
            }
            ClearError();
            ViewState.success = true;
        }

        /// <summary>Debug begin method.</summary>
        /// <param name="methodName">Name of the method.</param>
        protected virtual void DebugBeginMethod(
            [CallerMemberName] string? methodName = "",
            [CallerFilePath] string? sourceFilePath = "",
            [CallerLineNumber] int lineNumber = 0)
            => ConsoleDebug($"{sourceFilePath}:{methodName}:{lineNumber}: Start");

        /// <summary>Debug end method.</summary>
        /// <param name="methodName">Name of the method.</param>
        protected virtual void DebugEndMethod(
            [CallerMemberName] string? methodName = "",
            [CallerFilePath] string? sourceFilePath = "",
            [CallerLineNumber] int lineNumber = 0)
            => ConsoleDebug($"{sourceFilePath}:{methodName}:{lineNumber}: End");

        /// <summary>Console debug.</summary>
        /// <param name="args">A variable-length parameters list containing arguments.</param>
        protected virtual void ConsoleDebug(params object?[]? args)
        {
            if (Contract.CheckEmpty(args))
            {
                return;
            }
            foreach (var arg in args!.Where(x => x is not null))
            {
                if (arg is string asStr)
                {
                    Console.WriteLine(arg);
                    ViewState?.logMessages.Add(asStr);
                    continue;
                }
                try
                {
                    var str = JsonConvert.SerializeObject(arg);
                    Console.WriteLine(str);
                    ViewState?.logMessages.Add(str);
                }
                catch
                {
                    Console.WriteLine(arg!.ToString()!);
                    ViewState?.logMessages.Add(arg.ToString()!);
                }
            }
        }

        /// <summary>Console log.</summary>
        /// <param name="args">A variable-length parameters list containing arguments.</param>
        protected virtual void ConsoleLog(params object?[]? args)
        {
            if (args is null || args.Length == 0)
            {
                return;
            }
            foreach (var arg in args.Where(x => x is not null))
            {
                if (arg is string asStr)
                {
                    Console.WriteLine(arg);
                    ViewState?.logMessages.Add(asStr);
                    continue;
                }
                try
                {
                    var str = JsonConvert.SerializeObject(arg);
                    Console.WriteLine(str);
                    ViewState?.logMessages.Add(str);
                }
                catch
                {
                    Console.WriteLine(arg!.ToString()!);
                    ViewState?.logMessages.Add(arg.ToString()!);
                }
            }
        }

        /// <summary>Console warn.</summary>
        /// <param name="args">A variable-length parameters list containing arguments.</param>
        protected virtual void ConsoleWarn(params object?[]? args)
        {
            if (Contract.CheckEmpty(args))
            {
                return;
            }
            foreach (var arg in args!.Where(x => x is not null))
            {
                if (arg is string asStr)
                {
                    Console.WriteLine(arg);
                    ViewState?.logMessages.Add(asStr);
                    Logger.LogWarning(asStr);
                    continue;
                }
                try
                {
                    var str = JsonConvert.SerializeObject(arg);
                    Console.WriteLine(str);
                    ViewState?.logMessages.Add(str);
                    Logger.LogWarning(str);
                }
                catch
                {
                    Console.WriteLine(arg!.ToString()!);
                    ViewState?.logMessages.Add(arg.ToString()!);
                    Logger.LogWarning(arg.ToString()!);
                }
            }
        }

        /// <summary>Loads a collection.</summary>
        /// <typeparam name="TSearchModel"> Type of the search model.</typeparam>
        /// <typeparam name="TPagedResults">Type of the paged results.</typeparam>
        /// <typeparam name="TModel">       Type of the model.</typeparam>
        /// <param name="apiCall">    The API call.</param>
        /// <param name="searchModel">The search model.</param>
        /// <returns>The collection.</returns>
        protected virtual async Task<List<TModel>?> LoadCollection<TSearchModel, TPagedResults, TModel>(
                Func<TSearchModel, Task<IHttpPromiseCallbackArg<TPagedResults>>> apiCall,
                TSearchModel searchModel)
            where TPagedResults : PagedResultsBase<TModel>
        {
            var logPrefix = $"LoadCollection<TSearchModel '{typeof(TSearchModel).Name}', TPagedResults '{typeof(TPagedResults).Name}', TModel '{typeof(TModel).Name}'>: ";
            ////ConsoleDebug(logPrefix + "Start");
            var r = await apiCall(searchModel).ConfigureAwait(false);
            ////ConsoleDebug(logPrefix + "Call finished");
            if (r.data is null)
            {
                await FinishRunningAsync(true, r.data).ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-a: Return r.data");
                return default;
            }
            if (r.data.Results is null)
            {
                await FinishRunningAsync(true, "The result was null, please contact support.").ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-b: Return default");
                return default;
            }
            ////ConsoleDebug(logPrefix + "Return r.data.Results");
            return r.data.Results;
        }

        /// <summary>Loads a record.</summary>
        /// <typeparam name="TEndpoint">Type of the endpoint.</typeparam>
        /// <typeparam name="TModel">   Type of the model.</typeparam>
        /// <param name="apiCall"> The API call.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns>The record.</returns>
        protected virtual async Task<TModel?> LoadRecord<TEndpoint, TModel>(
            Func<TEndpoint, Task<IHttpPromiseCallbackArg<TModel>>> apiCall,
            TEndpoint endpoint)
            where TModel : BaseModel
        {
            var logPrefix = $"LoadRecord<TEndpoint '{typeof(TEndpoint).Name}', TModel '{typeof(TModel).Name}'>: ";
            ////ConsoleDebug(logPrefix + "Start");
            var r = await apiCall(endpoint).ConfigureAwait(false);
            ////ConsoleDebug(logPrefix + "Call finished");
            if (r.data is null)
            {
                await FinishRunningAsync(true, r.data).ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-a: Return r.data");
                return default;
            }
            if (r.data is null)
            {
                await FinishRunningAsync(true, "The result was null, please contact support.").ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-b: Return default");
                return default;
            }
            ////ConsoleDebug(logPrefix + "Return r.data");
            return r.data;
        }

        /// <summary>Loads record in cefar.</summary>
        /// <typeparam name="TEndpoint">Type of the endpoint.</typeparam>
        /// <typeparam name="TModel">   Type of the model.</typeparam>
        /// <param name="apiCall">The API call.</param>
        /// <returns>The record in cefar.</returns>
        protected virtual async Task<TModel?> LoadRecordInCEFAR<TEndpoint, TModel>(
            Func<Task<IHttpPromiseCallbackArg<CEFActionResponse<TModel>>>> apiCall)
            where TModel : BaseModel
        {
            var logPrefix = $"LoadRecordInCEFAR<TEndpoint '{typeof(TEndpoint).Name}', TModel '{typeof(TModel).Name}'>: ";
            ////ConsoleDebug(logPrefix + "Start");
            var r = await apiCall().ConfigureAwait(false);
            ////ConsoleDebug(logPrefix + "Call finished");
            if (r.data?.ActionSucceeded != true)
            {
                /*
                if (r?.data is null)
                {
                    ConsoleDebug(logPrefix + "r?.data is null");
                }
                else if (r.data.ActionSucceeded == false)
                {
                    ConsoleDebug(logPrefix + "r.data.ActionSucceeded is false");
                    if (r.data.Messages is null)
                    {
                        ConsoleDebug(logPrefix + "r.data.Messages is null");
                    }
                    else if (r.data.Messages.Count == 0)
                    {
                        ConsoleDebug(logPrefix + "r.data.Messages is empty");
                    }
                    else
                    {
                        ConsoleDebug(logPrefix + $"r.data.Messages ({r.data.Messages.Count}):");
                        foreach (var message in r.data.Messages)
                        {
                            ConsoleDebug(logPrefix + ": r.data.Message: " + message);
                        }
                    }
                }
                */
                await FinishRunningAsync(true, errorMessages: r.data?.Messages.ToArray()).ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-a: Return default");
                return default;
            }
            if (r.data?.Result is null)
            {
                await FinishRunningAsync(true, "The result was null, please contact support.").ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-b: Return default");
                return default;
            }
            ////ConsoleDebug(logPrefix + "Return r.data.Result");
            return r.data.Result;
        }

        /// <summary>Loads record in cefar.</summary>
        /// <typeparam name="TEndpoint">Type of the endpoint.</typeparam>
        /// <typeparam name="TModel">   Type of the model.</typeparam>
        /// <param name="apiCall"> The API call.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns>The record in cefar.</returns>
        protected virtual async Task<TModel?> LoadRecordInCEFAR<TEndpoint, TModel>(
            Func<TEndpoint, Task<IHttpPromiseCallbackArg<CEFActionResponse<TModel>>>> apiCall,
            TEndpoint endpoint)
            where TModel : BaseModel
        {
            var logPrefix = $"LoadRecordInCEFAR<TEndpoint '{typeof(TEndpoint).Name}', TModel '{typeof(TModel).Name}'>: ";
            ////ConsoleDebug(logPrefix + "Start");
            var r = await apiCall(endpoint).ConfigureAwait(false);
            ////ConsoleDebug(logPrefix + "Call finished");
            if (r.data?.ActionSucceeded != true)
            {
                /*
                if (r?.data is null)
                {
                    ConsoleDebug(logPrefix + "r?.data is null");
                }
                else if (r.data.ActionSucceeded == false)
                {
                    ConsoleDebug(logPrefix + "r.data.ActionSucceeded is false");
                    if (r.data.Messages is null)
                    {
                        ConsoleDebug(logPrefix + "r.data.Messages is null");
                    }
                    else if (r.data.Messages.Count == 0)
                    {
                        ConsoleDebug(logPrefix + "r.data.Messages is empty");
                    }
                    else
                    {
                        ConsoleDebug(logPrefix + $"r.data.Messages ({r.data.Messages.Count}):");
                        foreach (var message in r.data.Messages)
                        {
                            ConsoleDebug(logPrefix + ": r.data.Message: " + message);
                        }
                    }
                }
                */
                await FinishRunningAsync(true, errorMessages: r.data?.Messages.ToArray()).ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-a: Return default");
                return default;
            }
            if (r.data?.Result is null)
            {
                await FinishRunningAsync(true, "The result was null, please contact support.").ConfigureAwait(false);
                ConsoleDebug(logPrefix + "Result null-b: Return default");
                return default;
            }
            ////ConsoleDebug(logPrefix + "Return r.data.Result");
            return r.data.Result;
        }

        /// <summary>CORS image link.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="area">    The area.</param>
        /// <returns>A string.</returns>
        protected virtual string CorsImageLink(string? fileName, string? area)
        {
            // TODO: CORS links
            return "/Content/" + (fileName ?? string.Empty);
        }

        /// <summary>Sets error.</summary>
        /// <param name="message"> The message.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task SetErrorAsync(
            /*IErrorMessageArg*/object? message,
            string[]? messages)
        {
            ClearSuccess();
            ConsoleDebug("SetErrorAsync invoked");
            ViewState.hasError = true;
            ViewState.success = false;
            ViewState.errorMessages = messages;
            switch (message)
            {
                case null:
                {
                    if (messages is { Length: > 0 })
                    {
                        ViewState.errorMessage = messages[0];
                        ViewState.errorMessages = messages;
                    }
                    else
                    {
                        ViewState.errorMessage = "An unknown error occurred";
                    }
                    break;
                }
                case Task<string> msgTask:
                {
                    ViewState.errorMessage = await msgTask.ConfigureAwait(false);
                    break;
                }
                case IHttpPromiseCallbackArg<ResponseError> { status: > 0 } asArg:
                {
                    ViewState.errorMessage = asArg.status // 401
                        + ": " + asArg.statusText // "Unauthorized"
                        + (asArg.data?.ResponseStatus?.Message is null
                            ? JsonConvert.SerializeObject(asArg)
                            : ": " + asArg.data.ResponseStatus.Message); // "No active user in session."
                    break;
                }
                default:
                {
                    ViewState.errorMessage = message as string;
                    break;
                }
            }
            ConsoleDebug(ViewState.errorMessage);
        }

        /// <summary>Clears the error.</summary>
        protected virtual void ClearError()
        {
            ViewState.hasError = false;
            ViewState.errorMessage = null;
            ViewState.errorMessages = null;
        }

        /// <summary>Clears the success.</summary>
        protected virtual void ClearSuccess()
        {
            ViewState.success = false;
        }
        #endregion
    }
}
