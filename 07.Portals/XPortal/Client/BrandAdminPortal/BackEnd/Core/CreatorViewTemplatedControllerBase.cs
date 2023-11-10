﻿// <copyright file="CreatorViewTemplatedControllerBase.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the creator view templated controller base class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    using System;
    using System.Threading.Tasks;
    using Api.Callers;
    using Api.Models;
    using Api.Options;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.JSInterop;
    using ServiceStack;
    using Utilities;

    /// <summary>A creator view templated controller base.</summary>
    /// <typeparam name="TRecord">  Type of the record.</typeparam>
    /// <typeparam name="TEndpoint">Type of the endpoint.</typeparam>
    /// <seealso cref="TemplatedControllerBase"/>
    public abstract class CreatorViewTemplatedControllerBase<TRecord, TEndpoint>
        : TemplatedControllerBase, IDisposable
        where TRecord : BaseModel
        where TEndpoint : TRecord, new()
    {
        private bool disposedValue;

        #region Injections
        /// <summary>Gets or sets the manager for navigation.</summary>
        /// <value>The navigation manager.</value>
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        /// <summary>Gets or sets the CEF API service client.</summary>
        /// <value>The CEF API service client.</value>
        [Inject]
#pragma warning disable IDE1006 // Naming Styles
        // ReSharper disable once InconsistentNaming
        public CEFAPI cvApi { get; set; } = null!;
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>Gets or sets the js runtime.</summary>
        /// <value>The js runtime.</value>
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        /// <summary>Gets or sets options for controlling the routing.</summary>
        /// <value>Options that control the routing.</value>
        [Inject]
        public RoutingOptions RoutingOptions { get; set; } = null!;
        #endregion

        #region Static Settings for Creator Views (these don't change) // TODO: Tie to a settings object
        #endregion

        #region Dynamic Settings for Creator Views (these change as the user interacts) // TODO: Tie initial values to a settings object
        /// <summary>Gets the name, but clipped to 25 characters and appending an ellipses if too long. Also falls back
        /// to Custom Key if name is not available/set.</summary>
        /// <value>The clipped name.</value>
        protected virtual string ClippedName
        {
            get
            {
                if (Record == null!)
                {
                    return string.Empty;
                }
                if (Record is not INameableBaseModel nRecord
                    || !Contract.CheckValidKey(nRecord.Name))
                {
                    return Truncate(Record.CustomKey, 25);
                }
                return Truncate(nRecord.Name, 25);
            }
        }

        /// <summary>Truncates a string.</summary>
        /// <param name="source">         Source for the.</param>
        /// <param name="limit">          The limit.</param>
        /// <param name="truncatedSuffix">The truncated suffix.</param>
        /// <returns>A string.</returns>
        protected static string Truncate(string? source, int limit, string truncatedSuffix = "...")
        {
            return Contract.CheckValidKey(source)
                ? source!.Length > Contract.RequiresValidID(limit)
                    ? source[..limit] + truncatedSuffix
                    : source
                : string.Empty;
        }
        #endregion

        #region Abstract Properties and Methods to be implemented by inheritors
        /// <summary>Gets the grid route format.</summary>
        /// <value>The grid route format.</value>
        protected abstract string GridRouteFormat { get; }

        /// <summary>Gets the editor route format.</summary>
        /// <value>The editor route format.</value>
        protected abstract string EditorRouteFormat { get; }

        /// <summary>Gets the record (as an instance of it's create endpoint).</summary>
        /// <value>The record.</value>
        protected virtual TRecord Record { get; } = new TEndpoint();

        /// <summary>Gets the starting tab.</summary>
        /// <value>The starting tab.</value>
        protected virtual string StartingTab => "details";

        /// <summary>Gets or sets the selected tab.</summary>
        /// <value>The selected tab.</value>
        protected virtual string? SelectedTab { get; set; }

        /// <summary>Gets or sets the edit context.</summary>
        /// <value>The edit context.</value>
        protected virtual EditContext EditContext { get; set; } = null!;

        /// <summary>Gets or sets a value indicating whether the form is invalid.</summary>
        /// <value>True if form is invalid, false if not.</value>
        protected virtual bool FormInvalid { get; set; }

        /// <summary>Loads extended data.</summary>
        /// <returns>The extended data.</returns>
        protected abstract Task LoadExtendedDataAsync();
        #endregion

        #region Events
        /// <summary>Executes the initialized action.</summary>
        /// <returns>A Task.</returns>
        protected override async Task OnInitializedAsync()
        {
            DebugBeginMethod();
            await base.OnInitializedAsync().ConfigureAwait(false);
            await SetRunningAsync().ConfigureAwait(false);
            // ReSharper disable once ConstantNullCoalescingCondition
            Record.SerializableAttributes ??= new();
            EditContext = new(Record);
            EditContext.OnFieldChanged += HandleFieldChanged;
            Master = await cvApi.GetCurrentMasterAsync().ConfigureAwait(false);
            SelectedTab = StartingTab;
            if (!await cvApi.CEFService.IsAuthenticatedAsync(JSRuntime).ConfigureAwait(false))
            {
                await FinishRunningAsync(true, "You are not logged in, re-routing to login page").ConfigureAwait(false);
                ViewState.loading = false;
                NavigationManager.NavigateTo("/login?returnUrl=" + new Uri(NavigationManager.Uri).PathAndQuery.UrlEncode());
                return;
            }
            await LoadExtendedDataAsync().ConfigureAwait(false);
            await FinishRunningAsync().ConfigureAwait(false);
            ViewState.loading = false;
            DebugEndMethod();
        }

        /// <summary>Executes the selected tab changed action.</summary>
        /// <param name="name">The name.</param>
        protected virtual void OnSelectedTabChanged(string name)
        {
            DebugBeginMethod();
            SelectedTab = name;
            DebugEndMethod();
        }

        /// <summary>Handles the field changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">     Field changed event information.</param>
        protected virtual void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
        {
            DebugBeginMethod();
            if (EditContext == null!)
            {
                return;
            }
            FormInvalid = !EditContext.Validate();
            StateHasChanged();
            ViewState.MakeDirty();
            DebugEndMethod();
        }

        /// <summary>Extended record data enforcement on save.</summary>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtendedRecordDataEnforcementOnSaveAsync(DateTime timestamp)
        {
            // Do nothing by default, override to perform additional actions
            return Task.CompletedTask;
        }

        /// <summary>Gets the create caller function.</summary>
        /// <value>The create caller function.</value>
        protected abstract Func<TEndpoint, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> CreateCallerFunc { get; }

        /// <summary>Gets the create caller function.</summary>
        /// <value>The create caller function.</value>
        protected abstract Func<int, DateTime, Task<IHttpPromiseCallbackArg<CEFActionResponse<int>>>> AssocToMasterCallerFunc { get; }

        /// <summary>Executes the save action.</summary>
        /// <returns>A Task.</returns>
        protected virtual async Task OnSave()
        {
            DebugBeginMethod();
            if (Record is null)
            {
                throw new InvalidOperationException();
            }
            var timestamp = DateTime.Now;
            Record.Active = true;
            Record.CreatedDate = timestamp;
            await ExtendedRecordDataEnforcementOnSaveAsync(timestamp);
            var newID = (await CreateCallerFunc((TEndpoint)Record).ConfigureAwait(false)).data?.Result ?? 0;
            ConsoleDebug("Save: Create finished, associating to master...");
            await AssocToMasterCallerFunc(newID, timestamp).ConfigureAwait(false);
            ConsoleDebug("Save: Association finished. Moving to editor...");
            NavigateToEditorAfterSave(newID);
            DebugEndMethod();
        }

        /// <summary>Navigate to editor after save.</summary>
        /// <param name="newID">Identifier for the new.</param>
        protected virtual void NavigateToEditorAfterSave(int newID)
        {
            NavigationManager.NavigateTo(string.Format(EditorRouteFormat, newID));
        }
        #endregion

        #region IDisposable
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        /// resources.</summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases the unmanaged resources used by the
        /// <see cref="CreatorViewTemplatedControllerBase{TRecord, TEndpoint}" /> and optionally releases
        /// the managed resources.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                if (EditContext == null!)
                {
                    return;
                }
                EditContext.OnFieldChanged -= HandleFieldChanged;
            }
            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
        #endregion
    }
}
