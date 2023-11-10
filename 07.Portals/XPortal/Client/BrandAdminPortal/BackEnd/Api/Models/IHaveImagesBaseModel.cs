// <copyright file="IHaveImagesBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveImagesBaseModel interface</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have images base model.</summary>
    /// <typeparam name="TIImageModel">    Type of the image model's interface.</typeparam>
    /// <typeparam name="TIImageTypeModel">Type of the image type model's interface.</typeparam>
    public interface IHaveImagesBaseModel<TIImageModel, TIImageTypeModel>
        : IBaseModel
        where TIImageModel : IImageBaseModel<TIImageTypeModel>
        where TIImageTypeModel : ITypableBaseModel
    {
        /// <summary>Gets or sets the images.</summary>
        /// <value>The images.</value>
        List<TIImageModel>? Images { get; set; }

        /// <summary>Gets or sets the primary image.</summary>
        /// <value>The primary image.</value>
        string? PrimaryImageFileName { get; set; }
    }
}
