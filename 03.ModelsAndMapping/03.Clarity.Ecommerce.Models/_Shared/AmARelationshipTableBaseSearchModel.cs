// <copyright file="AmARelationshipTableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the am a relationship table search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data transfer model for the am a relationship table search base.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    public abstract class AmARelationshipTableBaseSearchModel
        : BaseSearchModel,
            IAmARelationshipTableBaseSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Identifier of the Master Record [Optional]")]
        public int? MasterID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterIDs), DataType = "List<int>", ParameterType = "query", IsRequired = false,
            Description = "The Identifiers of the Master Records [Optional]")]
        public List<int>? MasterIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Custom Key of the Master Record [Optional]")]
        public string? MasterKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Identifier of the Slave Record [Optional]")]
        public int? SlaveID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveIDs), DataType = "List<int>", ParameterType = "query", IsRequired = false,
            Description = "The Identifiers of the Slave Records [Optional]")]
        public List<int>? SlaveIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Custom Key of the Slave Record [Optional]")]
        public string? SlaveKey { get; set; }
    }

    /// <summary>A data Model for the am a relationship table nameable base search.</summary>
    /// <seealso cref="AmARelationshipTableBaseSearchModel"/>
    /// <seealso cref="IAmARelationshipTableNameableBaseSearchModel"/>
    public abstract class AmARelationshipTableNameableBaseSearchModel
        : AmARelationshipTableBaseSearchModel,
            IAmARelationshipTableNameableBaseSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names (Case-Insensitive)")]
        public string? Name { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(NameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, causes the Name parameter to require exact matches")]
        public bool? NameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Description), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Descriptions (Case-Insensitive)")]
        public string? Description { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKeyOrName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names or Custom Keys (Case-Insensitive)")]
        public string? CustomKeyOrName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKeyOrNameOrDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, Custom Keys or Descriptions (Case-Insensitive)")]
        public string? CustomKeyOrNameOrDescription { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IDOrCustomKeyOrName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, IDs or Custom Keys (Case-Insensitive)")]
        public string? IDOrCustomKeyOrName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IDOrCustomKeyOrNameOrDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, IDs, Custom Keys or Descriptions (Case-Insensitive)")]
        public string? IDOrCustomKeyOrNameOrDescription { get; set; }
    }
}
