// <copyright file="FranchiseLanguageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise language model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class FranchiseLanguageModel
    {
        /// <inheritdoc/>
        public string? OverrideLocale { get; set; }

        /// <inheritdoc/>
        public string? OverrideUnicodeName { get; set; }

        /// <inheritdoc/>
        public string? OverrideISO639_1_2002 { get; set; }

        /// <inheritdoc/>
        public string? OverrideISO639_2_1998 { get; set; }

        /// <inheritdoc/>
        public string? OverrideISO639_3_2007 { get; set; }

        /// <inheritdoc/>
        public string? OverrideISO639_5_2008 { get; set; }
    }
}
