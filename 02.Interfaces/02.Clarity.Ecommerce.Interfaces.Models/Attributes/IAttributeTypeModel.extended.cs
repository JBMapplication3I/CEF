// <copyright file="IAttributeTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAttributeTypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for attribute type model.</summary>
    public partial interface IAttributeTypeModel
    {
        /// <summary>Gets or sets the general attributes.</summary>
        /// <value>The general attributes.</value>
        List<IGeneralAttributeModel>? GeneralAttributes { get; set; }
    }
}
