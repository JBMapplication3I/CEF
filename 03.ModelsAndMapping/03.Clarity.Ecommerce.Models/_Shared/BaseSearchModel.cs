// <copyright file="BaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the base search.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public abstract class BaseSearchModel : IBaseSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Objects with the ID (will return 0 or 1 object)")]
        public int? ID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IDs), DataType = "int[]", ParameterType = "query", IsRequired = false,
            Description = "Objects with any one of the IDs")]
        public int[]? IDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The results will not contain the ExcludedID")]
        public int? ExcludedID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(NotIDs), DataType = "int[]", ParameterType = "query", IsRequired = false,
            Description = "Objects without any one of the IDs")]
        public int[]? NotIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Active), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Can force the Active check to be something other than the default of all")]
        public bool? Active { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Custom Keys (Case-Insensitive)")]
        public string? CustomKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKeyStrict), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Forces Strict mode on or off for CustomKey searching (includes IDOrCustomKey search)")]
        public bool? CustomKeyStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKeys), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "Objects with any one of the custom keys")]
        public string?[]? CustomKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomKeysStrict), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Forces Strict mode on or off for CustomKeys array searching")]
        public bool? CustomKeysStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IDOrCustomKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Custom Keys or ID (Case-Insensitive)")]
        public string? IDOrCustomKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ModifiedSince), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Will return objects Modified Since this timestamp as noted by CreatedDate or UpdatedDate")]
        public DateTime? ModifiedSince { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinUpdatedOrCreatedDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Will return objects Modified Since this timestamp as noted by CreatedDate or UpdatedDate")]
        public DateTime? MinUpdatedOrCreatedDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxUpdatedOrCreatedDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Will return objects Modified Since this timestamp as noted by CreatedDate or UpdatedDate")]
        public DateTime? MaxUpdatedOrCreatedDate { get; set; }

        #region IHavePaging
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Paging), DataType = "Paging", ParameterType = "query", IsRequired = false,
            Description = "Server-side paging information")]
        public Paging? Paging { get; set; }
        #endregion

        #region IHaveGrouping
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Groupings), DataType = "Grouping", ParameterType = "query", IsRequired = false,
            Description = "Server-side grouping information")]
        public Grouping[]? Groupings { get; set; }
        #endregion

        #region IHaveSorting
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Sorts), DataType = "Sort[]", ParameterType = "query", IsRequired = false,
            Description = "Server-side sorting information")]
        public Sort[]? Sorts { get; set; }
        #endregion

        #region IHaveJsonAttributesBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(JsonAttributes), DataType = "Dictionary<string, string[]>", ParameterType = "query", IsRequired = false,
            Description = "A dictionary of keys and possible values for attributes")]
        public Dictionary<string, string?[]?>? JsonAttributes { get; set; }
        #endregion

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AsListing), DataType = "bool", ParameterType = "query", IsRequired = true,
            Description = "A flag indicating whether to get the results as listing or as lite mapping (default is false = lite)")]
        public bool AsListing { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(noCache), DataType = "bool", ParameterType = "long", IsRequired = false,
            Description = "Specifying a value will reduce or prevent chance of getting cached data.")]
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
        public long? noCache { get; set; }
    }
}
