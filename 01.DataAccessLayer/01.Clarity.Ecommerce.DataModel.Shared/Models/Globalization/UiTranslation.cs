// <copyright file="UiTranslation.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the translation class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IUiTranslation : IBase
    {
        #region UiTranslation properties
        /// <summary>Gets or sets the locale.</summary>
        /// <value>The locale.</value>
        string? Locale { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the UI key.</summary>
        /// <value>The identifier of the UI key.</value>
        int UiKeyID { get; set; }

        /// <summary>Gets or sets the UI key.</summary>
        /// <value>The user interface UI key.</value>
        UiKey? UiKey { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Globalization", "UITranslation")]
    public class UiTranslation : Base, IUiTranslation
    {
        #region UiTranslation properties
        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(true), DefaultValue(null)]
        public string? Locale { get; set; }

        /// <inheritdoc/>
        [StringLength(1024), StringIsUnicode(true), DefaultValue(null)]
        public string? Value { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(UiKey)), DefaultValue(0)]
        public int UiKeyID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual UiKey? UiKey { get; set; }
        #endregion
    }
}
