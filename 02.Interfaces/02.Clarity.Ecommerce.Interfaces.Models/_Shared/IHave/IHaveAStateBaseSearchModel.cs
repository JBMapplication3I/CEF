// <copyright file="IHaveAStateBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStateBaseSearchModel interface</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have a state base search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public interface IHaveAStateBaseSearchModel : IBaseSearchModel
    {
        /// <summary>Gets or sets the identifier of the state.</summary>
        /// <value>The identifier of the state.</value>
        int? StateID { get; set; }

        /// <summary>Gets or sets the state IDs.</summary>
        /// <value>The state IDs.</value>
        int?[]? StateIDs { get; set; }

        /// <summary>Gets or sets the identifier of the excluded state.</summary>
        /// <value>The identifier of the excluded state.</value>
        int? ExcludedStateID { get; set; }

        /// <summary>Gets or sets the excluded state ids.</summary>
        /// <value>The excluded state IDs.</value>
        int?[]? ExcludedStateIDs { get; set; }

        /// <summary>Gets or sets the state key.</summary>
        /// <value>The state key.</value>
        string? StateKey { get; set; }

        /// <summary>Gets or sets the state keys.</summary>
        /// <value>The state keys.</value>
        string?[]? StateKeys { get; set; }

        /// <summary>Gets or sets the excluded state key.</summary>
        /// <value>The excluded state key.</value>
        string? ExcludedStateKey { get; set; }

        /// <summary>Gets or sets the excluded state keys.</summary>
        /// <value>The excluded state keys.</value>
        string?[]? ExcludedStateKeys { get; set; }

        /// <summary>Gets or sets the name of the state.</summary>
        /// <value>The name of the state.</value>
        string? StateName { get; set; }

        /// <summary>Gets or sets a list of names of the states.</summary>
        /// <value>A list of names of the states.</value>
        string?[]? StateNames { get; set; }

        /// <summary>Gets or sets the name of the excluded state.</summary>
        /// <value>The name of the excluded state.</value>
        string? ExcludedStateName { get; set; }

        /// <summary>Gets or sets a list of names of the excluded states.</summary>
        /// <value>A list of names of the excluded states.</value>
        string?[]? ExcludedStateNames { get; set; }

        /// <summary>Gets or sets the name of the state display.</summary>
        /// <value>The name of the state display.</value>
        string? StateDisplayName { get; set; }

        /// <summary>Gets or sets a list of names of the state displays.</summary>
        /// <value>A list of names of the state displays.</value>
        string?[]? StateDisplayNames { get; set; }

        /// <summary>Gets or sets the name of the excluded state display.</summary>
        /// <value>The name of the excluded state display.</value>
        string? ExcludedStateDisplayName { get; set; }

        /// <summary>Gets or sets a list of names of the excluded state displays.</summary>
        /// <value>A list of names of the excluded state displays.</value>
        string?[]? ExcludedStateDisplayNames { get; set; }

        /// <summary>Gets or sets the state translation key.</summary>
        /// <value>The state translation key.</value>
        string? StateTranslationKey { get; set; }
    }
}
