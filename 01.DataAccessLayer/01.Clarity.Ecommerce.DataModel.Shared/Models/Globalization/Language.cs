// <copyright file="Language.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the language class</summary>
// ReSharper disable InconsistentNaming
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ILanguage : IBase, IHaveImagesBase<Language, LanguageImage, LanguageImageType>
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

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Globalization", "Language")]
    public class Language : Base, ILanguage
    {
        private ICollection<LanguageImage>? images;

        public Language()
        {
            // IHaveImagesBase Properties
            images = new HashSet<LanguageImage>();
        }

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<LanguageImage>? Images { get => images; set => images = value; }
        #endregion

        #region Language Properties
        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(true), Index, DefaultValue(null)]
        public string? Locale { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(true), DefaultValue(null)]
        public string? UnicodeName { get; set; }

        /// <inheritdoc/>
        [StringLength(2), StringIsUnicode(false), DefaultValue(null)]
        public string? ISO639_1_2002 { get; set; }

        /// <inheritdoc/>
        [StringLength(3), StringIsUnicode(false), DefaultValue(null)]
        public string? ISO639_2_1998 { get; set; }

        /// <inheritdoc/>
        [StringLength(3), StringIsUnicode(false), DefaultValue(null)]
        public string? ISO639_3_2007 { get; set; }

        /// <inheritdoc/>
        [StringLength(3), StringIsUnicode(false), DefaultValue(null)]
        public string? ISO639_5_2008 { get; set; }
        #endregion
    }
}
