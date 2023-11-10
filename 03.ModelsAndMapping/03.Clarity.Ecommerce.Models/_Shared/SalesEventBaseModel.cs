// <copyright file="SalesEventBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item target base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the sales event base.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ISalesEventBaseModel"/>
    public class SalesEventBaseModel : NameableBaseModel, ISalesEventBaseModel
    {
        #region IHaveATypeBaseModel<ITypeModel>
        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the Type of this Account, required if no TypeModel present"), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel{ITypeModel}.Type"/>
        [ApiMember(Name = nameof(Type), DataType = "TypeModel", ParameterType = "body", IsRequired = false,
            Description = "Model for Type of this object, required if no TypeID present"), DefaultValue(null)]
        public TypeModel? Type { get; set; }

        /// <inheritdoc/>
        ITypeModel? IHaveATypeBaseModel<ITypeModel>.Type { get => Type; set => Type = (TypeModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Key for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Name for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Display Name for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Translation Key for the Type of this object, read-only"), DefaultValue(null)]
        public string? TypeTranslationKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sort Order for the Type of this object, read-only"), DefaultValue(null)]
        public int? TypeSortOrder { get; set; }
        #endregion

        /// <inheritdoc/>
        public int? OldStateID { get; set; }

        /// <inheritdoc/>
        public int? NewStateID { get; set; }

        /// <inheritdoc/>
        public int? OldStatusID { get; set; }

        /// <inheritdoc/>
        public int? NewStatusID { get; set; }

        /// <inheritdoc/>
        public int? OldTypeID { get; set; }

        /// <inheritdoc/>
        public int? NewTypeID { get; set; }

        /// <inheritdoc/>
        public long? OldHash { get; set; }

        /// <inheritdoc/>
        public long? NewHash { get; set; }

        /// <inheritdoc/>
        public string? OldRecordSerialized { get; set; }

        /// <inheritdoc/>
        public string? NewRecordSerialized { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int MasterID { get; set; }
        #endregion
    }
}
