// <copyright file="GeneralAttribute.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the general attribute class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IGeneralAttribute : IHaveATypeBase<AttributeType>, ITypableBase
    {
        #region GeneralAttribute Properties
        /// <summary>Gets or sets a value indicating whether this IGeneralAttribute is filter.</summary>
        /// <value>True if this IGeneralAttribute is filter, false if not.</value>
        bool IsFilter { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttribute is comparable.</summary>
        /// <value>True if this IGeneralAttribute is comparable, false if not.</value>
        bool IsComparable { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttribute is predefined.</summary>
        /// <value>True if this IGeneralAttribute is predefined, false if not.</value>
        bool IsPredefined { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttribute is markup.</summary>
        /// <value>True if this IGeneralAttribute is markup, false if not.</value>
        bool IsMarkup { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttribute is tab.</summary>
        /// <value>True if this IGeneralAttribute is tab, false if not.</value>
        bool IsTab { get; set; }

        /// <summary>Gets or sets a value indicating whether from storefront is hidden.</summary>
        /// <value>True if hide from storefront, false if not.</value>
        bool HideFromStorefront { get; set; }

        /// <summary>Gets or sets a value indicating whether from suppliers is hidden.</summary>
        /// <value>True if hide from suppliers, false if not.</value>
        bool HideFromSuppliers { get; set; }

        /// <summary>Gets or sets a value indicating whether from product detail view is hidden.</summary>
        /// <value>True if hide from product detail view, false if not.</value>
        bool HideFromProductDetailView { get; set; }

        /// <summary>Gets or sets a value indicating whether from catalog views is hidden.</summary>
        /// <value>True if hide from catalog views, false if not.</value>
        bool HideFromCatalogViews { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the attribute tab.</summary>
        /// <value>The identifier of the attribute tab.</value>
        int? AttributeTabID { get; set; }

        /// <summary>Gets or sets the attribute tab.</summary>
        /// <value>The attribute tab.</value>
        AttributeTab? AttributeTab { get; set; }

        /// <summary>Gets or sets the identifier of the attribute group.</summary>
        /// <value>The identifier of the attribute group.</value>
        int? AttributeGroupID { get; set; }

        /// <summary>Gets or sets the group the attribute belongs to.</summary>
        /// <value>The attribute group.</value>
        AttributeGroup? AttributeGroup { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets options for controlling the general attribute predefined.</summary>
        /// <value>Options that control the general attribute predefined.</value>
        ICollection<GeneralAttributePredefinedOption>? GeneralAttributePredefinedOptions { get; set; }
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

    [SqlSchema("Attributes", "GeneralAttribute")]
    public class GeneralAttribute : TypableBase, IGeneralAttribute
    {
        private ICollection<GeneralAttributePredefinedOption>? generalAttributePredefinedOptions;

        public GeneralAttribute()
        {
            generalAttributePredefinedOptions = new HashSet<GeneralAttributePredefinedOption>();
        }

        /// <inheritdoc/>
        [Required, StringLength(128), StringIsUnicode(false), Index(IsUnique = true), DefaultValue(null)]
        public override string? CustomKey { get; set; }

        #region GeneralAttribute Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsFilter { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsComparable { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsPredefined { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsMarkup { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsTab { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HideFromStorefront { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HideFromSuppliers { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HideFromProductDetailView { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HideFromCatalogViews { get; set; } = false;
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AttributeType? Type { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(AttributeTab)), DefaultValue(null)]
        public int? AttributeTabID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AttributeTab? AttributeTab { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(AttributeGroup)), DefaultValue(null)]
        public int? AttributeGroupID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AttributeGroup? AttributeGroup { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<GeneralAttributePredefinedOption>? GeneralAttributePredefinedOptions { get => generalAttributePredefinedOptions; set => generalAttributePredefinedOptions = value; }
        #endregion
    }
}
