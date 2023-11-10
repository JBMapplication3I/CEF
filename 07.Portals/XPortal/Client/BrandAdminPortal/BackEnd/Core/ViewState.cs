// <copyright file="ViewState.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    using System.Collections.Generic;

    /// <summary>A view state.</summary>
    /// <seealso cref="IViewState"/>
    public class ViewState : IViewState
    {
        /// <inheritdoc/>
        public bool running { get; set; } = true;

        /// <inheritdoc/>
        public bool loading { get; set; } = true;

        /// <inheritdoc/>
        public bool saving { get; set; }

        /// <inheritdoc/>
        public bool success { get; set; }

        /// <inheritdoc/>
        public bool dirty { get; set; }

        /// <inheritdoc/>
        public bool pristine { get => !dirty; set => dirty = !value; }

        /// <inheritdoc/>
        public bool InputDisable => running || loading || saving;

        /// <inheritdoc/>
        public bool hasError { get; set; }

        /// <inheritdoc/>
        public string? errorMessage { get; set; }

        /// <inheritdoc/>
        public string[]? errorMessages { get; set; }

        /// <inheritdoc/>
        public string? waitMessage { get; set; }

        /// <inheritdoc/>
        public List<string> logMessages { get; set; } = new();

        /// <inheritdoc/>
        public void MakeDirty()
        {
            dirty = true;
        }

        /// <inheritdoc/>
        public void MakePristine()
        {
            pristine = true;
        }

        /// <inheritdoc/>
        public string AsHTMLDebugBlock()
        {
            return
$@"<div class=""d-block"">
  <ul class=""pl-3"">
    <li>running: {running}</li>
    <li>loading: {loading}</li>
    <li>saving: {saving}</li>
    <li>success: {success}</li>
    <li>dirty: {dirty}</li>
    <li>pristine: {pristine}</li>
    <li>hasError: {hasError}</li>
    <li>errorMessage: {errorMessage ?? "<none>"}</li>
    <li>waitMessage: {waitMessage ?? "<none>"}</li>
  </ul>
</div>";
        }
    }
}
