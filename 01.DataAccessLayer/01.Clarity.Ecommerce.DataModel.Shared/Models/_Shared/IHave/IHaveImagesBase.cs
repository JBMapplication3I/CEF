// <copyright file="IHaveImagesBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveImagesBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have images base.</summary>
    /// <typeparam name="TMaster">   Type of the master.</typeparam>
    /// <typeparam name="TImage">    Type of the image.</typeparam>
    /// <typeparam name="TImageType">Type of the image type.</typeparam>
    public interface IHaveImagesBase<TMaster, TImage, TImageType>
        where TImage : IImageBase<TMaster, TImageType>
        where TMaster : IBase
        where TImageType : ITypableBase
    {
        /// <summary>Gets or sets the images.</summary>
        /// <value>The images.</value>
        ICollection<TImage>? Images { get; set; }
    }
}
