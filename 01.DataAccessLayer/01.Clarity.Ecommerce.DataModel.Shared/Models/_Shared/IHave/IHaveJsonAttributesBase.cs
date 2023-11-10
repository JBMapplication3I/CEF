// <copyright file="IHaveJsonAttributesBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveJsonAttributesBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.ComponentModel;
    using Models;

    /// <summary>Interface for have JSON attributes base.</summary>
    public interface IHaveJsonAttributesBase
    {
        /// <summary>Gets or sets the JSON attributes.</summary>
        /// <value>The JSON attributes.</value>
        string? JsonAttributes { get; set; }

        /// <summary>Gets the serializable attributes.</summary>
        /// <value>The serializable attributes.</value>
        [ReadOnly(true)]
        SerializableAttributesDictionary SerializableAttributes { get; }
    }
}
