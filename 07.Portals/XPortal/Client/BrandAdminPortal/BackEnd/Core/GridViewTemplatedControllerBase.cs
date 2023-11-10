// <copyright file="TemplatedControllerBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
// ReSharper disable InvertIf
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.MVC.Core
{
    using System;
    using System.Threading.Tasks;
    using Api.Callers;
    using Api.Models;
    using Api.Options;
    using Blazorise.DataGrid;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.JSInterop;
    using ServiceStack;

    /// <summary>A grid view templated controller base.</summary>
    /// <typeparam name="TData">    Type of the data (Usually inherits <see cref="PagedResultsBase{TSelected}"/> but not required to).</typeparam>
    /// <typeparam name="TSelected">Type of the selected record (Usually inherits <see cref="BaseModel"/> but not required to).</typeparam>
    /// <typeparam name="TSearchModel">Type of the search model (Usually inherits <see cref="BaseSearchModel"/> but not required to).</typeparam>
    /// <seealso cref="TemplatedControllerBase"/>
    public abstract class GridViewTemplatedControllerBase<TData, TSelected, TSearchModel> : TemplatedControllerBase
        where TData : class
        where TSelected : class
        where TSearchModel : class, new()
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

        #region Static Settings for CV-Grid Views (these don't change) // TODO: Tie to a settings object
        /// <summary>Gets a value indicating whether the grid pager is shown.</summary>
        /// <value>True if show pager, false if not.</value>
        protected virtual bool ShowPager => true;

        /// <summary>Gets a value indicating whether this grid is filterable.</summary>
        /// <value>True if filterable, false if not.</value>
        protected virtual bool Filterable => true;

        /// <summary>Gets a value indicating whether the grid is responsive (bootstrap resizable).</summary>
        /// <value>True if responsive, false if not.</value>
        protected virtual bool Responsive => true;

        /// <summary>Gets a value indicating whether the grid should use narrow cell paddings.</summary>
        /// <value>True if narrow, false if not.</value>
        protected virtual bool Narrow => true;

        /// <summary>Gets a value indicating whether the grid is hoverable (rows highlight when hovered).</summary>
        /// <value>True if hoverable, false if not.</value>
        protected virtual bool Hoverable => true;

        /// <summary>Gets a value indicating whether the grid rows are striped.</summary>
        /// <value>True if striped, false if not.</value>
        protected virtual bool Striped => true;

        /// <summary>Gets a value indicating whether the grid is bordered.</summary>
        /// <value>True if bordered, false if not.</value>
        protected virtual bool Bordered => true;

        /// <summary>Gets a value indicating whether the grid captions are shown.</summary>
        /// <value>True if show captions, false if not.</value>
        protected virtual bool ShowCaptions => true;

        /// <summary>Gets a value indicating whether this grid is sortable.</summary>
        /// <value>True if sortable, false if not.</value>
        protected virtual bool Sortable => true;

        /// <summary>Gets a value indicating whether the grid page size selector control is shown.</summary>
        /// <value>True if show page sizes, false if not.</value>
        protected virtual bool ShowPageSizes => true;

        /// <summary>Gets a value indicating whether this grid is resizable.</summary>
        /// <value>True if resizable, false if not.</value>
        protected virtual bool Resizable => false;

        /// <summary>Gets a value indicating whether the grid is borderless.</summary>
        /// <value>True if borderless, false if not.</value>
        protected virtual bool Borderless => !Bordered;

        /// <summary>Gets the maximum pagination links.</summary>
        /// <value>The maximum pagination links.</value>
        protected virtual int MaxPaginationLinks => 5;

        /// <summary>Gets a list of sizes of the pages.</summary>
        /// <value>A list of sizes of the pages.</value>
        protected virtual int[] PageSizes { get; } = { 10, 20, 30 };
        #endregion

        #region Dynamic Settings for CV-Grid Views (these change as the user interacts) // TODO: Tie initial values to a settings object
        /// <summary>Gets or sets the current page.</summary>
        /// <value>The current page.</value>
        protected virtual int CurrentPage { get; set; } = 1;

        /// <summary>Gets or sets the size of the page.</summary>
        /// <value>The size of the page.</value>
        protected virtual int CurrentPageSize { get; set; } = 10;

        /// <summary>Gets or sets the current data collection.</summary>
        /// <value>A Collection of current data.</value>
        protected virtual TData? CurrentDataCollection { get; set; }

        /// <summary>Gets or sets the selected.</summary>
        /// <value>The selected.</value>
        protected virtual TSelected? Selected { get; set; }

        /// <summary>Gets or sets the search model.</summary>
        /// <value>The search model.</value>
        protected virtual TSearchModel dto { get; set; } = new();

        /// <summary>Gets or sets the custom filter value.</summary>
        /// <value>The custom filter value.</value>
        protected virtual string? CustomFilterValue { get; set; }
        #endregion

        #region Abstract Properties and Methods to be implemented by inheritors
        /// <summary>Gets the editor route base.</summary>
        /// <value>The editor route base.</value>
        protected abstract string EditorRouteBase { get; }

        /// <summary>Gets the creator route base.</summary>
        /// <value>The creator route base.</value>
        protected abstract string CreatorRouteBase { get; }

        /// <summary>Gets the grid view route base.</summary>
        /// <value>The grid view route base.</value>
        protected abstract string GridViewRouteBase { get; }

        /// <summary>Gets or sets the edit context.</summary>
        /// <value>The edit context.</value>
        protected virtual EditContext EditContext { get; set; } = null!;

        /// <summary>Initial data load call.</summary>
        /// <returns>A Task{IHttpPromiseCallbackArg{TData?}}.</returns>
        protected abstract Task<IHttpPromiseCallbackArg<TData?>> InitialDataLoadCallAsync();

        /// <summary>Filtered data load call.</summary>
        /// <returns>A Task{IHttpPromiseCallbackArg{TData?}}.</returns>
        protected abstract Task<IHttpPromiseCallbackArg<TData?>> FilteredDataLoadCallAsync(
            DataGridReadDataEventArgs<TSelected>? e);

        /// <summary>Custom filter check.</summary>
        /// <param name="model">The model.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected abstract bool CustomFilterCheck(TSelected model);
        #endregion

        #region Events
        /// <summary>Executes the initialized action.</summary>
        /// <returns>A Task.</returns>
        protected override async Task OnInitializedAsync()
        {
            DebugBeginMethod();
            await base.OnInitializedAsync().ConfigureAwait(false);
            await SetRunningAsync().ConfigureAwait(false);
            if (!await cvApi.CEFService.IsAuthenticatedAsync(JSRuntime).ConfigureAwait(false))
            {
                await FinishRunningAsync(true, "You are not logged in, re-routing to login page").ConfigureAwait(false);
                ViewState.loading = false;
                NavigationManager.NavigateTo("/login?returnUrl=" + new Uri(NavigationManager.Uri).PathAndQuery.UrlEncode());
                return;
            }
            EditContext = new(dto);
            EditContext.OnFieldChanged += HandleFieldChanged;
            PortalRoute = RoutingOptions.GetRouteByPath(GridViewRouteBase);
            await LoadInitialDataAsync().ConfigureAwait(false);
            await LoadExtendedInitialDataAsync().ConfigureAwait(false);
            await FinishRunningAsync().ConfigureAwait(false);
            ViewState.loading = false;
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
            StateHasChanged();
            ViewState.MakeDirty();
            OnFilter();
            DebugEndMethod();
        }

        /// <summary>Loads extended initial data.</summary>
        /// <returns>The extended initial data.</returns>
        protected virtual Task LoadExtendedInitialDataAsync()
        {
            // Do nothing by default, override to load additional data at page init
            return Task.CompletedTask;
        }

        /// <summary>Executes the selected row changed action.</summary>
        /// <param name="selected">The selected.</param>
        protected virtual void OnSelectedRowChanged(TSelected selected)
        {
            DebugBeginMethod();
            if (Selected == selected)
            {
                DebugEndMethod();
                return;
            }
            Selected = selected;
            StateHasChanged();
            DebugEndMethod();
        }

        /// <summary>Executes the item view clicked action.</summary>
        /// <param name="contextID">Identifier for the context.</param>
        protected virtual void OnItemViewClicked(int contextID)
        {
            DebugBeginMethod();
            NavigationManager.NavigateTo(EditorRouteBase + contextID);
            DebugEndMethod();
        }

        /// <summary>Executes the read data action.</summary>
        /// <param name="e">The DataGridReadDataEventArgs{TSelected} to process.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task OnReadData(DataGridReadDataEventArgs<TSelected>? e)
        {
            DebugBeginMethod();
            var result = await FilteredDataLoadCallAsync(e).ConfigureAwait(false);
            ////ConsoleDebug(result.data);
            if (result.data is not null)
            {
                CurrentDataCollection = result.data;
            }
            StateHasChanged();
            DebugEndMethod();
        }

        /// <summary>Loads initial data.</summary>
        /// <returns>A Task.</returns>
        protected virtual async Task LoadInitialDataAsync()
        {
            DebugBeginMethod();
            var result = await InitialDataLoadCallAsync().ConfigureAwait(false);
            ////ConsoleDebug(result.data);
            if (result.data is not null)
            {
                CurrentDataCollection = result.data;
            }
            DebugEndMethod();
        }

        /// <summary>Executes the custom filter action.</summary>
        /// <param name="model">The model.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual bool OnCustomFilter(TSelected model)
        {
            DebugBeginMethod();
            // We want to accept empty value as valid or otherwise datagrid will not show anything.
            if (string.IsNullOrEmpty(CustomFilterValue))
            {
                DebugEndMethod();
                return true;
            }
            DebugEndMethod();
            return CustomFilterCheck(model);
        }

        /// <summary>Executes the filter action.</summary>
        protected virtual void OnFilter()
        {
            DebugBeginMethod();
            CurrentPage = 1;
            OnReadData(null).Start();
            DebugEndMethod();
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
        /// <see cref="GridViewTemplatedControllerBase{TData, TSelected, TSearchModel}" /> and optionally releases
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
