// <copyright file="LanguageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the language model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class LanguageModel
    {
        /// <inheritdoc/>
        public string? Locale { get; set; }

        /// <inheritdoc/>
        public string? UnicodeName { get; set; }

        /// <inheritdoc/>
        public string? ISO639_1_2002 { get; set; }

        /// <inheritdoc/>
        public string? ISO639_2_1998 { get; set; }

        /// <inheritdoc/>
        public string? ISO639_3_2007 { get; set; }

        /// <inheritdoc/>
        public string? ISO639_5_2008 { get; set; }
    }
}
