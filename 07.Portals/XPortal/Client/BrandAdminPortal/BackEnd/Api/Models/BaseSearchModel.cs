// <copyright file="BaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    public partial class BaseSearchModel
    {
        [ApiMember(Name = "ID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Objects with the ID (will return 0 or 1 object)")]
        public int? ID { get; set; }

        [ApiMember(Name = "IDs", DataType = "int[]", ParameterType = "query", IsRequired = false,
            Description = "Objects with any one of the IDs")]
        public int[]? IDs { get; set; }

        [ApiMember(Name = "ExcludedID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The results will not contain the ExcludedID")]
        public int? ExcludedID { get; set; }

        [ApiMember(Name = "NotIDs", DataType = "int[]", ParameterType = "query", IsRequired = false,
            Description = "Objects without any one of the IDs")]
        public int[]? NotIDs { get; set; }

        [ApiMember(Name = "Active", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Can force the Active check to be something other than the default of all")]
        public bool? Active { get; set; }

        [ApiMember(Name = "CustomKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Custom Keys (Case-Insensitive)")]
        public string? CustomKey { get; set; }

        [ApiMember(Name = "CustomKeyStrict", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Forces Strict mode on or off for CustomKey searching (includes IDOrCustomKey search)")]
        public bool? CustomKeyStrict { get; set; }

        [ApiMember(Name = "CustomKeys", DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "Objects with any one of the custom keys")]
        public string[]? CustomKeys { get; set; }

        [ApiMember(Name = "CustomKeysStrict", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Forces Strict mode on or off for CustomKeys array searching")]
        public bool? CustomKeysStrict { get; set; }

        [ApiMember(Name = "IDOrCustomKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Custom Keys or ID (Case-Insensitive)")]
        public string? IDOrCustomKey { get; set; }

        [ApiMember(Name = "ModifiedSince", DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Will return objects Modified Since this timestamp as noted by CreatedDate or UpdatedDate")]
        public DateTime? ModifiedSince { get; set; }

        [ApiMember(Name = "MinUpdatedOrCreatedDate", DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Will return objects Modified Since this timestamp as noted by CreatedDate or UpdatedDate")]
        public DateTime? MinUpdatedOrCreatedDate { get; set; }

        [ApiMember(Name = "MaxUpdatedOrCreatedDate", DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Will return objects Modified Since this timestamp as noted by CreatedDate or UpdatedDate")]
        public DateTime? MaxUpdatedOrCreatedDate { get; set; }

        [ApiMember(Name = "Paging", DataType = "Paging", ParameterType = "query", IsRequired = false,
            Description = "Server-side paging information")]
        public Paging? Paging { get; set; }

        [ApiMember(Name = "Groupings", DataType = "Grouping", ParameterType = "query", IsRequired = false,
            Description = "Server-side grouping information")]
        public Grouping[]? Groupings { get; set; }

        [ApiMember(Name = "Sorts", DataType = "Sort[]", ParameterType = "query", IsRequired = false,
            Description = "Server-side sorting information")]
        public Sort[]? Sorts { get; set; }

        [ApiMember(Name = "JsonAttributes", DataType = "Dictionary<string, string[]>", ParameterType = "query", IsRequired = false,
            Description = "A dictionary of keys and possible values for attributes")]
        public Dictionary<string, string[]>? JsonAttributes { get; set; }

        [ApiMember(Name = "AsListing", DataType = "bool", ParameterType = "query", IsRequired = true,
            Description = "A flag indicating whether to get the results as listing or as lite mapping (default is false = lite)")]
        public bool AsListing { get; set; }

        [ApiMember(Name = "noCache", DataType = "bool", ParameterType = "long", IsRequired = false,
            Description = "Specifying a value will reduce or prevent chance of getting cached data.")]
        public long? noCache { get; set; }
    }
}
