// <copyright file="IGeneralAttributeSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IGeneralAttributeSearchModel interface</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for general attribute search model.</summary>
    public partial interface IGeneralAttributeSearchModel
    {
        /// <summary>Gets or sets the name of the include general with type.</summary>
        /// <value>The name of the include general with type.</value>
        bool? IncludeGeneralWithTypeName { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        string? Group { get; set; }

        /// <summary>Gets or sets the attribute group key.</summary>
        /// <value>The attribute group key.</value>
        string? AttributeGroupKey { get; set; }

        /// <summary>Gets or sets the name of the attribute group.</summary>
        /// <value>The name of the attribute group.</value>
        string? AttributeGroupName { get; set; }

        /// <summary>Gets or sets the name of the attribute group display.</summary>
        /// <value>The name of the attribute group display.</value>
        string? AttributeGroupDisplayName { get; set; }

        /// <summary>Gets or sets the attribute tab key.</summary>
        /// <value>The attribute tab key.</value>
        string? AttributeTabKey { get; set; }

        /// <summary>Gets or sets the name of the attribute tab.</summary>
        /// <value>The name of the attribute tab.</value>
        string? AttributeTabName { get; set; }

        /// <summary>Gets or sets the name of the attribute tab display.</summary>
        /// <value>The name of the attribute tab display.</value>
        string? AttributeTabDisplayName { get; set; }
    }
}
