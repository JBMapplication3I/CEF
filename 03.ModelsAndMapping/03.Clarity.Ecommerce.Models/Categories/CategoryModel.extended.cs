// <copyright file="CategoryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the category.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ICategoryModel"/>
    public partial class CategoryModel
    {
        #region Category Properties
        /// <inheritdoc/>
        [ApiMember(Name = nameof(DisplayName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The Display Name")]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Sort Order")]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsVisible), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Category is Visible")]
        public bool IsVisible { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IncludeInMenu), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Show category in menu")]
        public bool IncludeInMenu { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HeaderContent), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Category Header Content")]
        public string? HeaderContent { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SidebarContent), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Category Sidebar Content")]
        public string? SidebarContent { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FooterContent), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Category Footer Content")]
        public string? FooterContent { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HandlingCharge), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Category Handling Charge (Fee, applied once during checkout if product in this category is being purchased)")]
        public decimal? HandlingCharge { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RestockingFeePercent), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee as a percentage of Product sold price")]
        public decimal? RestockingFeePercent { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RestockingFeeAmount), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "Restocking Fee Amount")]
        public decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ParentName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Name of the Parent, if have a parent")]
        public string? ParentName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ParentSeoUrl), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "SEO URL of the Parent, if have a parent")]
        public string? ParentSeoUrl { get; set; }

        /// <inheritdoc/>
        public int? RestockingFeeAmountCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? RestockingFeeAmountCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? RestockingFeeAmountCurrencyName { get; set; }

        /// <inheritdoc cref="ICategoryModel.RestockingFeeAmountCurrency"/>
        public CurrencyModel? RestockingFeeAmountCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? ICategoryModel.RestockingFeeAmountCurrency { get => RestockingFeeAmountCurrency; set => RestockingFeeAmountCurrency = (CurrencyModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ICategoryModel.ProductCategories"/>
        [ApiMember(Name = nameof(ProductCategories), DataType = "List<ProductCategoryModel>", ParameterType = "body", IsRequired = false,
            Description = "Products assigned to this Category")]
        public List<ProductCategoryModel>? ProductCategories { get; set; }

        /// <inheritdoc/>
        List<IProductCategoryModel>? ICategoryModel.ProductCategories { get => ProductCategories?.ToList<IProductCategoryModel>(); set => ProductCategories = value?.Cast<ProductCategoryModel>().ToList(); }
        #endregion
    }
}
