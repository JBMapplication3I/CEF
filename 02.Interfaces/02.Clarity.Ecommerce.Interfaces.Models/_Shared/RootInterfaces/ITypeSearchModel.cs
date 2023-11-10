// <copyright file="ITypeSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITypeSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for type search model.</summary>
    /// <seealso cref="ITypableBaseSearchModel"/>
    public interface ITypeSearchModel
        : ITypableBaseSearchModel,
            IAmFilterableByBrandSearchModel,
            IAmFilterableByStoreSearchModel
    {
    }
}
