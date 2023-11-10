// <copyright file="BrandLanguageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand language model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class BrandLanguageModel
    {
        #region BrandLanguage Properties
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
        #endregion
    }
}
