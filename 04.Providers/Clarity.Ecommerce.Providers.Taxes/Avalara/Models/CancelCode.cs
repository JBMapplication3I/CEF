// <copyright file="CancelCode.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cancel code class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    /// <summary>Values that represent cancel codes.</summary>
    public enum CancelCode
    {
        /// <summary>An enum constant representing the unspecified option.</summary>
        Unspecified,

        /// <summary>An enum constant representing the post failed option.</summary>
        PostFailed,

        /// <summary>An enum constant representing the Document deleted option.</summary>
        DocDeleted,

        /// <summary>An enum constant representing the Document voided option.</summary>
        DocVoided,

        /// <summary>An enum constant representing the adjustment cancelled option.</summary>
        AdjustmentCancelled,
    }
}
