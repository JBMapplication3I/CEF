// <copyright file="UiTranslationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the translation model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the translation.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IUiTranslationModel"/>
    public partial class UiTranslationModel
    {
        #region UiTranslation Objects
        /// <inheritdoc/>
        public string? Locale { get; set; }

        /// <inheritdoc/>
        public string? Value { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int UiKeyID { get; set; }

        /// <inheritdoc/>
        public string? UiKeyKey { get; set; }

        /// <inheritdoc cref="IUiTranslationModel.UiKey"/>
        public UiKeyModel? UiKey { get; set; }

        /// <inheritdoc/>
        IUiKeyModel? IUiTranslationModel.UiKey { get => UiKey; set => UiKey = (UiKeyModel?)value; }
        #endregion
    }
}
