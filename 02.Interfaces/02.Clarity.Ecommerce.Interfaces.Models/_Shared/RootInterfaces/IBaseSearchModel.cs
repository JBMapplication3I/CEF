// <copyright file="IBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for base search model.</summary>
    public interface IBaseSearchModel
        : IHavePaging, IHaveGrouping, IHaveSorting, IHaveJsonAttributesBaseSearchModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int? ID { get; set; }

        /// <summary>Gets or sets the identifiers.</summary>
        /// <value>The identifiers.</value>
        // ReSharper disable once InconsistentNaming
        int[]? IDs { get; set; }

        /// <summary>Gets or sets the identifier of the excluded.</summary>
        /// <value>The identifier of the excluded.</value>
        /// <remarks>ExcludedID is an ID that we want to filter out of the results.</remarks>
        int? ExcludedID { get; set; }

        /// <summary>Gets or sets the not IDs.</summary>
        /// <value>The not IDs.</value>
        int[]? NotIDs { get; set; }

        /// <summary>Gets or sets the active.</summary>
        /// <value>The active.</value>
        bool? Active { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        string? CustomKey { get; set; }

        /// <summary>Gets or sets the custom key strict.</summary>
        /// <value>The custom key strict.</value>
        bool? CustomKeyStrict { get; set; }

        /// <summary>Gets or sets the custom keys.</summary>
        /// <value>The custom keys.</value>
        string?[]? CustomKeys { get; set; }

        /// <summary>Gets or sets the custom keys strict.</summary>
        /// <value>The custom keys strict.</value>
        bool? CustomKeysStrict { get; set; }

        /// <summary>Gets or sets the identifier or custom key.</summary>
        /// <value>The identifier or custom key.</value>
        string? IDOrCustomKey { get; set; }

        /// <summary>Gets or sets the Date/Time of the modified since.</summary>
        /// <value>The modified since.</value>
        DateTime? ModifiedSince { get; set; }

        /// <summary>Gets or sets the minimum updated or created date.</summary>
        /// <value>The minimum updated or created date.</value>
        DateTime? MinUpdatedOrCreatedDate { get; set; }

        /// <summary>Gets or sets the maximum updated or created date.</summary>
        /// <value>The maximum updated or created date.</value>
        DateTime? MaxUpdatedOrCreatedDate { get; set; }

        /// <summary>Gets or sets a value indicating whether to get the results as listing.</summary>
        /// <value>True to get as listing, false if not.</value>
        bool AsListing { get; set; }

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
#pragma warning disable SA1300,IDE1006
        long? noCache { get; set; }
#pragma warning restore SA1300,IDE1006
    }
}
