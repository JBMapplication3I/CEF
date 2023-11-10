// <copyright file="SalesEventBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales event base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the sales event base search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="ISalesEventBaseSearchModel"/>
    public class SalesEventBaseSearchModel : NameableBaseSearchModel, ISalesEventBaseSearchModel
    {
        #region IHaveATypeBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects")]
        public int? TypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically include")]
        public int?[]? TypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects to specifically exclude")]
        public int? ExcludedTypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically exclude")]
        public int?[]? ExcludedTypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects")]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically include")]
        public string?[]? TypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects to specifically exclude")]
        public string? ExcludedTypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically exclude")]
        public string?[]? ExcludedTypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects")]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically include")]
        public string?[]? TypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects to specifically exclude")]
        public string? ExcludedTypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically include")]
        public string?[]? TypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects to specifically exclude")]
        public string? ExcludedTypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Translation Key for objects")]
        public string? TypeTranslationKey { get; set; }
        #endregion

        /* Not allowed to search by these for now
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
        */

        #region Related Objects
        /// <inheritdoc/>
        public int? MasterID { get; set; }
        #endregion
    }
}
