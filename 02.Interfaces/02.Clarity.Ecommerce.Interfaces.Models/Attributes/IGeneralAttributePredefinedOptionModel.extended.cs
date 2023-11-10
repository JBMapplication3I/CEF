// <copyright file="IGeneralAttributePredefinedOptionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IGeneralAttributePredefinedOptionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for general attribute predefined option model.</summary>
    public partial interface IGeneralAttributePredefinedOptionModel
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string Value { get; set; }

        /// <summary>Gets or sets the uof m.</summary>
        /// <value>The uof m.</value>
        string UofM { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the attribute.</summary>
        /// <value>The identifier of the attribute.</value>
        int AttributeID { get; set; }

        /// <summary>Gets or sets the attribute key.</summary>
        /// <value>The attribute key.</value>
        string? AttributeKey { get; set; }

        /// <summary>Gets or sets the name of the attribute.</summary>
        /// <value>The name of the attribute.</value>
        string? AttributeName { get; set; }

        /// <summary>Gets or sets the attribute.</summary>
        /// <value>The attribute.</value>
        IGeneralAttributeModel? Attribute { get; set; }
        #endregion
    }
}
