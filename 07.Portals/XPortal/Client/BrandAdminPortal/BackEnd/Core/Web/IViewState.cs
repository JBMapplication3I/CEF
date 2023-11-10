// <copyright file="IViewState.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.MVC.Core
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>Interface for view state.</summary>
    [PublicAPI]
    public interface IViewState
    {
        /// <summary>Gets or sets a value indicating whether this IViewState is running.</summary>
        /// <value>True if running, false if not.</value>
        bool running { get; set; }

        /// <summary>Gets or sets a value indicating whether this IViewState is loading.</summary>
        /// <value>True if loading, false if not.</value>
        bool loading { get; set; }

        /// <summary>Gets or sets a value indicating whether this IViewState is saving.</summary>
        /// <value>True if saving, false if not.</value>
        bool saving { get; set; }

        /// <summary>Gets or sets a value indicating whether this IViewState is dirty.</summary>
        /// <value>True if dirty, false if not.</value>
        bool dirty { get; set; }

        /// <summary>Gets or sets a value indicating whether this IViewState is pristine.</summary>
        /// <value>True if pristine, false if not.</value>
        bool pristine { get; set; }

        /// <summary>Gets or sets a value indicating whether this IViewState has success.</summary>
        /// <value>True if success, false if not.</value>
        bool success { get; set; }

        /// <summary>Gets or sets a value indicating whether this IViewState has an error.</summary>
        /// <value>True if this IViewState has error, false if not.</value>
        bool hasError { get; set; }

        /// <summary>Gets or sets a message describing the error.</summary>
        /// <value>A message describing the error.</value>
        string? errorMessage { get; set; }

        /// <summary>Gets a value indicating whether this IViewState is input disabled.</summary>
        /// <value>True if inputs should be disabled, false if not.</value>
        bool InputDisable { get; }

        /// <summary>Gets or sets the error messages.</summary>
        /// <value>The error messages.</value>
        string[]? errorMessages { get; set; }

        /// <summary>Gets or sets a message describing the wait.</summary>
        /// <value>A message describing the wait.</value>
        string? waitMessage { get; set; }

        /// <summary>Gets or sets the log messages.</summary>
        /// <value>The log messages.</value>
        List<string> logMessages { get; set; }

        /// <summary>Makes the view-state dirty.</summary>
        void MakeDirty();

        /// <summary>Makes the view-state pristine.</summary>
        void MakePristine();

        /// <summary>Converts this ViewState to an HTML debug block.</summary>
        /// <returns>A string.</returns>
        string AsHTMLDebugBlock();
    }
}
