// <copyright file="IAmAGeographicalLocation.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAGeographicalLocation interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    public interface IAmAGeographicalLocation<TThis, TLanguage, TCurrency, TImage, TImageType, TTaxes>
        : INameableBase,
            IHaveLanguagesBase<TLanguage>,
            IHaveCurrenciesBase<TCurrency>,
            IHaveImagesBase<TThis, TImage, TImageType>
        where TLanguage : IBase
        where TCurrency : IBase
        where TThis : IBase
        where TImage : IImageBase<TThis, TImageType>
        where TImageType : ITypableBase
        where TTaxes : INameableBase
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }

        /// <summary>Gets or sets the taxes.</summary>
        /// <value>The taxes.</value>
        ICollection<TTaxes>? Taxes { get; set; }
    }
}
