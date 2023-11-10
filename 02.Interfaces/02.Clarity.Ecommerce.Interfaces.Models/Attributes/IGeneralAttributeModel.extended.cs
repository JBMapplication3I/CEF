// <copyright file="IGeneralAttributeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IGeneralAttributeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for general attribute model.</summary>
    public partial interface IGeneralAttributeModel
    {
        /// <summary>Gets or sets a value indicating whether this IGeneralAttributeModel is filter.</summary>
        /// <value>True if this IGeneralAttributeModel is filter, false if not.</value>
        bool IsFilter { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttributeModel is markup.</summary>
        /// <value>True if this IGeneralAttributeModel is markup, false if not.</value>
        bool IsMarkup { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttributeModel is predefined.</summary>
        /// <value>True if this IGeneralAttributeModel is predefined, false if not.</value>
        bool IsPredefined { get; set; }

        /// <summary>Gets or sets a value indicating whether from storefront is hidden.</summary>
        /// <value>True if hide from storefront, false if not.</value>
        bool HideFromStorefront { get; set; }

        /// <summary>Gets or sets a value indicating whether from suppliers is hidden.</summary>
        /// <value>True if hide from suppliers, false if not.</value>
        bool HideFromSuppliers { get; set; }

        /// <summary>Gets or sets a value indicating whether from catalog views is hidden.</summary>
        /// <value>True if hide from catalog views, false if not.</value>
        bool HideFromCatalogViews { get; set; }

        /// <summary>Gets or sets a value indicating whether from product detail view is hidden.</summary>
        /// <value>True if hide from product detail view, false if not.</value>
        bool HideFromProductDetailView { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttributeModel is comparable.</summary>
        /// <value>True if this IGeneralAttributeModel is comparable, false if not.</value>
        bool IsComparable { get; set; }

        /// <summary>Gets or sets a value indicating whether this IGeneralAttributeModel is tab.</summary>
        /// <value>True if this IGeneralAttributeModel is tab, false if not.</value>
        bool IsTab { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        string? Group { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the attribute group.</summary>
        /// <value>The identifier of the attribute group.</value>
        int? AttributeGroupID { get; set; }

        /// <summary>Gets or sets the attribute group key.</summary>
        /// <value>The attribute group key.</value>
        string? AttributeGroupKey { get; set; }

        /// <summary>Gets or sets the name of the attribute group.</summary>
        /// <value>The name of the attribute group.</value>
        string? AttributeGroupName { get; set; }

        /// <summary>Gets or sets the name of the attribute group display.</summary>
        /// <value>The name of the attribute group display.</value>
        string? AttributeGroupDisplayName { get; set; }

        /// <summary>Gets or sets the group the attribute belongs to.</summary>
        /// <value>The attribute group.</value>
        IAttributeGroupModel? AttributeGroup { get; set; }

        /// <summary>Gets or sets the identifier of the attribute tab.</summary>
        /// <value>The identifier of the attribute tab.</value>
        int? AttributeTabID { get; set; }

        /// <summary>Gets or sets the attribute tab key.</summary>
        /// <value>The attribute tab key.</value>
        string? AttributeTabKey { get; set; }

        /// <summary>Gets or sets the name of the attribute tab.</summary>
        /// <value>The name of the attribute tab.</value>
        string? AttributeTabName { get; set; }

        /// <summary>Gets or sets the name of the attribute tab display.</summary>
        /// <value>The name of the attribute tab display.</value>
        string? AttributeTabDisplayName { get; set; }

        /// <summary>Gets or sets the attribute tab.</summary>
        /// <value>The attribute tab.</value>
        IAttributeTabModel? AttributeTab { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets options for controlling the general attribute predefined.</summary>
        /// <value>Options that control the general attribute predefined.</value>
        List<IGeneralAttributePredefinedOptionModel>? GeneralAttributePredefinedOptions { get; set; }
        #endregion
    }
}
