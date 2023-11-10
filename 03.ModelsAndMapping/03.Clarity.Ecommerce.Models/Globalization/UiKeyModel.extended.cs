// <copyright file="UiKeyModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ui key model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the UI key.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IUiKeyModel"/>
    public partial class UiKeyModel
    {
        #region UiKey Objects
        /// <inheritdoc/>
        public string? Type { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IUiKeyModel.UiTranslations"/>
        public List<UiTranslationModel>? UiTranslations { get; set; }

        /// <inheritdoc/>
        List<IUiTranslationModel>? IUiKeyModel.UiTranslations { get => UiTranslations?.Cast<IUiTranslationModel>().ToList(); set => UiTranslations = value?.Cast<UiTranslationModel>().ToList(); }
        #endregion
    }
}
