// <copyright file="IZipCodeSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IZipCodeSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for zip code search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public partial interface IZipCodeSearchModel
    {
        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The zip code.</value>
        string? ZipCode { get; set; }
    }
}
