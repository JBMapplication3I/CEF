// <copyright file="GeneralAttributeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the general attribute model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class GeneralAttributeModel
    {
        #region GeneralAttribute Properties
        /// <inheritdoc/>
        public bool HideFromStorefront { get; set; }

        /// <inheritdoc/>
        public bool HideFromSuppliers { get; set; }

        /// <inheritdoc/>
        public bool HideFromCatalogViews { get; set; }

        /// <inheritdoc/>
        public bool HideFromProductDetailView { get; set; }

        /// <inheritdoc/>
        public bool IsComparable { get; set; }

        /// <inheritdoc/>
        public bool IsFilter { get; set; }

        /// <inheritdoc/>
        public bool IsMarkup { get; set; }

        /// <inheritdoc/>
        public bool IsPredefined { get; set; }

        /// <inheritdoc/>
        public bool IsTab { get; set; }

        /// <inheritdoc/>
        public string? Group { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? AttributeGroupID { get; set; }

        /// <inheritdoc/>
        public string? AttributeGroupKey { get; set; }

        /// <inheritdoc/>
        public string? AttributeGroupName { get; set; }

        /// <inheritdoc/>
        public string? AttributeGroupDisplayName { get; set; }

        /// <inheritdoc cref="IGeneralAttributeModel.AttributeGroup"/>
        public AttributeGroupModel? AttributeGroup { get; set; }

        /// <inheritdoc/>
        IAttributeGroupModel? IGeneralAttributeModel.AttributeGroup { get => AttributeGroup; set => AttributeGroup = (AttributeGroupModel?)value; }

        /// <inheritdoc/>
        public int? AttributeTabID { get; set; }

        /// <inheritdoc/>
        public string? AttributeTabKey { get; set; }

        /// <inheritdoc/>
        public string? AttributeTabName { get; set; }

        /// <inheritdoc/>
        public string? AttributeTabDisplayName { get; set; }

        /// <inheritdoc cref="IGeneralAttributeModel.AttributeTab"/>
        public AttributeTabModel? AttributeTab { get; set; }

        /// <inheritdoc/>
        IAttributeTabModel? IGeneralAttributeModel.AttributeTab { get => AttributeTab; set => AttributeTab = (AttributeTabModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IGeneralAttributeModel.GeneralAttributePredefinedOptions"/>
        public List<GeneralAttributePredefinedOptionModel>? GeneralAttributePredefinedOptions { get; set; }

        /// <inheritdoc/>
        List<IGeneralAttributePredefinedOptionModel>? IGeneralAttributeModel.GeneralAttributePredefinedOptions { get => GeneralAttributePredefinedOptions?.ToList<IGeneralAttributePredefinedOptionModel>(); set => GeneralAttributePredefinedOptions = value?.Cast<GeneralAttributePredefinedOptionModel>().ToList(); }
        #endregion
    }
}
