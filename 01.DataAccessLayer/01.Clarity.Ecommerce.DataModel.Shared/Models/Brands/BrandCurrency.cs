// <copyright file="BrandCurrency.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand currency class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IBrandCurrency
        : IAmABrandRelationshipTableWhereBrandIsTheMaster<Currency>
    {
        #region BrandCurrency
        /// <summary>Gets or sets a value indicating whether this  is primary.</summary>
        /// <value>True if this  is primary, false if not.</value>
        bool IsPrimary { get; set; }

        /// <summary>Gets or sets the name of the custom.</summary>
        /// <value>The name of the custom.</value>
        string? CustomName { get; set; }

        /// <summary>Gets or sets the custom translation key.</summary>
        /// <value>The custom translation key.</value>
        string? CustomTranslationKey { get; set; }

        /// <summary>Gets or sets the override unicode symbol value.</summary>
        /// <value>The override unicode symbol value.</value>
        decimal OverrideUnicodeSymbolValue { get; set; }

        /// <summary>Gets or sets the override HTML character code.</summary>
        /// <value>The override HTML character code.</value>
        string? OverrideHtmlCharacterCode { get; set; }

        /// <summary>Gets or sets the override raw character.</summary>
        /// <value>The override raw character.</value>
        string? OverrideRawCharacter { get; set; }

        /// <summary>Gets or sets the override decimal place accuracy.</summary>
        /// <value>The override decimal place accuracy.</value>
        int? OverrideDecimalPlaceAccuracy { get; set; }

        /// <summary>Gets or sets the override use separator.</summary>
        /// <value>The override use separator.</value>
        bool? OverrideUseSeparator { get; set; }

        /// <summary>Gets or sets the override raw decimal character.</summary>
        /// <value>The override raw decimal character.</value>
        string? OverrideRawDecimalCharacter { get; set; }

        /// <summary>Gets or sets the override HTML decimal character code.</summary>
        /// <value>The override HTML decimal character code.</value>
        string? OverrideHtmlDecimalCharacterCode { get; set; }

        /// <summary>Gets or sets the override raw separator character.</summary>
        /// <value>The override raw separator character.</value>
        string? OverrideRawSeparatorCharacter { get; set; }

        /// <summary>Gets or sets the override HTML separator character code.</summary>
        /// <value>The override HTML separator character code.</value>
        string? OverrideHtmlSeparatorCharacterCode { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Brands", "BrandCurrency")]
    public class BrandCurrency : Base, IBrandCurrency
    {
        #region IAmARelationshipTable<Brand, Currency>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Brand? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Currency? Slave { get; set; }
        #endregion

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByBrand.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmFilterableByBrand.Brand { get => Master; set => Master = value; }
        #endregion

        #region IAmABrandRelationshipTableWhereBrandIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmABrandRelationshipTableWhereBrandIsTheMaster<Currency>.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmABrandRelationshipTableWhereBrandIsTheMaster<Currency>.Brand { get => Master; set => Master = value; }
        #endregion

        #region BrandCurrency
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsPrimary { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? CustomName { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? CustomTranslationKey { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal OverrideUnicodeSymbolValue { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(12), DefaultValue(null)]
        public string? OverrideHtmlCharacterCode { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(5), DefaultValue(null)]
        public string? OverrideRawCharacter { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? OverrideDecimalPlaceAccuracy { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool? OverrideUseSeparator { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(5), DefaultValue(null)]
        public string? OverrideRawDecimalCharacter { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(12), DefaultValue(null)]
        public string? OverrideHtmlDecimalCharacterCode { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(5), DefaultValue(null)]
        public string? OverrideRawSeparatorCharacter { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(12), DefaultValue(null)]
        public string? OverrideHtmlSeparatorCharacterCode { get; set; }
        #endregion
    }
}
