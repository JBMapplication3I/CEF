// <copyright file="ILanguageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ILanguageModel interface</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for language model.</summary>
    public partial interface ILanguageModel
    {
        #region Language Properties
        /// <summary>Gets or sets the locale.</summary>
        /// <value>The locale.</value>
        string? Locale { get; set; }

        /// <summary>Gets or sets the name of the unicode.</summary>
        /// <value>The name of the unicode.</value>
        string? UnicodeName { get; set; }

        /// <summary>Gets or sets the ISO 639 1 2002 code.</summary>
        /// <value>The ISO 639 1 2002 code.</value>
        string? ISO639_1_2002 { get; set; }

        /// <summary>Gets or sets the ISO 639 2 1998 code.</summary>
        /// <value>The ISO 639 2 1998 code.</value>
        string? ISO639_2_1998 { get; set; }

        /// <summary>Gets or sets the ISO 639 3 2007 code.</summary>
        /// <value>The ISO 639 3 2007 code.</value>
        string? ISO639_3_2007 { get; set; }

        /// <summary>Gets or sets the ISO 639 5 2008 code.</summary>
        /// <value>The ISO 639 5 2008 code.</value>
        string? ISO639_5_2008 { get; set; }
        #endregion
    }
}
