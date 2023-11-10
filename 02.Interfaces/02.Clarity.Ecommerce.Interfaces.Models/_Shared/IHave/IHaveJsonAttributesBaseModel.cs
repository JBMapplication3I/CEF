// <copyright file="IHaveJsonAttributesBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveJsonAttributesBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have JSON attributes base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveJsonAttributesBaseModel
    {
        /// <summary>Gets or sets the serializable attributes.</summary>
        /// <value>The serializable attributes.</value>
        SerializableAttributesDictionary SerializableAttributes { get; set; }
    }
}
