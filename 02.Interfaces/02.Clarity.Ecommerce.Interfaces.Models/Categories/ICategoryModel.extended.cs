// <copyright file="ICategoryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICategoryModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for category model.</summary>
    public partial interface ICategoryModel
    {
        #region Category Properties
        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets a value indicating whether this ICategoryModel is visible.</summary>
        /// <value>True if this ICategoryModel is visible, false if not.</value>
        bool IsVisible { get; set; }

        /// <summary>Gets or sets a value indicating whether the in menu should be included.</summary>
        /// <value>True if include in menu, false if not.</value>
        bool IncludeInMenu { get; set; }

        /// <summary>Gets or sets the header content.</summary>
        /// <value>The header content.</value>
        string? HeaderContent { get; set; }

        /// <summary>Gets or sets the sidebar content.</summary>
        /// <value>The sidebar content.</value>
        string? SidebarContent { get; set; }

        /// <summary>Gets or sets the footer content.</summary>
        /// <value>The footer content.</value>
        string? FooterContent { get; set; }

        /// <summary>Gets or sets the handling charge.</summary>
        /// <value>The handling charge.</value>
        decimal? HandlingCharge { get; set; }

        /// <summary>Gets or sets the restocking fee percent.</summary>
        /// <value>The restocking fee percent.</value>
        decimal? RestockingFeePercent { get; set; }

        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the name of the parent.</summary>
        /// <value>The name of the parent.</value>
        string? ParentName { get; set; }

        /// <summary>Gets or sets URL of the parent SEO.</summary>
        /// <value>The parent SEO URL.</value>
        string? ParentSeoUrl { get; set; }

        /// <summary>Gets or sets the identifier of the restocking fee amount currency.</summary>
        /// <value>The identifier of the restocking fee amount currency.</value>
        int? RestockingFeeAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency.</summary>
        /// <value>The restocking fee amount currency.</value>
        ICurrencyModel? RestockingFeeAmountCurrency { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency key.</summary>
        /// <value>The restocking fee amount currency key.</value>
        string? RestockingFeeAmountCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the restocking fee amount currency.</summary>
        /// <value>The name of the restocking fee amount currency.</value>
        string? RestockingFeeAmountCurrencyName { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the categories the product belongs to.</summary>
        /// <value>The product categories.</value>
        List<IProductCategoryModel>? ProductCategories { get; set; }
        #endregion
    }
}
