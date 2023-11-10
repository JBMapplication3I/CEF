// <copyright file="IFranchiseLanguageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFranchiseLanguageModel interface</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IFranchiseLanguageModel
    {
        /// <summary>Gets or sets the locale override.</summary>
        /// <value>The locale override.</value>
        string? OverrideLocale { get; set; }

        /// <summary>Gets or sets the name of the unicode override.</summary>
        /// <value>The name of the unicode override.</value>
        string? OverrideUnicodeName { get; set; }

        /// <summary>Gets or sets the ISO 639 1 2002 code override.</summary>
        /// <value>The ISO 639 1 2002 code override.</value>
        string? OverrideISO639_1_2002 { get; set; }

        /// <summary>Gets or sets the ISO 639 2 1998 code override.</summary>
        /// <value>The ISO 639 2 1998 code override.</value>
        string? OverrideISO639_2_1998 { get; set; }

        /// <summary>Gets or sets the ISO 639 3 2007 code override.</summary>
        /// <value>The ISO 639 3 2007 code override.</value>
        string? OverrideISO639_3_2007 { get; set; }

        /// <summary>Gets or sets the ISO 639 5 2008 code override.</summary>
        /// <value>The ISO 639 5 2008 code override.</value>
        string? OverrideISO639_5_2008 { get; set; }
    }
}
