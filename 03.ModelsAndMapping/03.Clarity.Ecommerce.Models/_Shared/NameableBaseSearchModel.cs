// <copyright file="NameableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the nameable base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the nameable base search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="INameableBaseSearchModel"/>
    public abstract class NameableBaseSearchModel : BaseSearchModel, INameableBaseSearchModel
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
