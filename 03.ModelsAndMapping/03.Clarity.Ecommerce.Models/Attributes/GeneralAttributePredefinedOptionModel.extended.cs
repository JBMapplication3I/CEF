// <copyright file="GeneralAttributePredefinedOptionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the general attribute predefined option model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the general attribute predefined option.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IGeneralAttributePredefinedOptionModel"/>
    public partial class GeneralAttributePredefinedOptionModel
    {
        #region GeneralAttributePredefinedOption Properties
        /// <inheritdoc/>
        public string Value { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string UofM { get; set; } = string.Empty;

        /// <inheritdoc/>
        public int? SortOrder { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int AttributeID { get; set; }

        /// <inheritdoc/>
        public string? AttributeKey { get; set; }

        /// <inheritdoc/>
        public string? AttributeName { get; set; }

        /// <inheritdoc cref="IGeneralAttributePredefinedOptionModel.Attribute"/>
        public GeneralAttributeModel? Attribute { get; set; }

        /// <inheritdoc/>
        IGeneralAttributeModel? IGeneralAttributePredefinedOptionModel.Attribute { get => Attribute; set => Attribute = (GeneralAttributeModel?)value; }
        #endregion
    }
}
