// <copyright file="ITypeModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for type model.</summary>
    /// <seealso cref="ITypableBaseModel"/>
    /// <seealso cref="IAmFilterableByNullableStoreModel"/>
    /// <seealso cref="IAmFilterableByNullableBrandModel"/>
    public interface ITypeModel
        : ITypableBaseModel,
            IAmFilterableByNullableStoreModel,
            IAmFilterableByNullableBrandModel
    {
    }
}
