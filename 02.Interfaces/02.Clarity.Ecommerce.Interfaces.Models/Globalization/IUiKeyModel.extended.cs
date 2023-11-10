// <copyright file="IUiKeyModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUiKeyModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for user interface key model.</summary>
    public partial interface IUiKeyModel
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        string? Type { get; set; }

        #region Associated Objects
        /// <summary>Gets or sets the translations.</summary>
        /// <value>The user interface translations.</value>
        List<IUiTranslationModel>? UiTranslations { get; set; }
        #endregion
    }
}
