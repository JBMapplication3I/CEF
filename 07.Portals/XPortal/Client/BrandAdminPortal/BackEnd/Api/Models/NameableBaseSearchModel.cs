// <copyright file="NameableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using ServiceStack;

    public partial class NameableBaseSearchModel : BaseSearchModel
    {
        [ApiMember(Name = "Name", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names (Case-Insensitive)")]
        public string? Name { get; set; }

        [ApiMember(Name = "NameStrict", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, causes the Name parameter to require exact matches")]
        public bool? NameStrict { get; set; }

        [ApiMember(Name = "Description", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Descriptions (Case-Insensitive)")]
        public string? Description { get; set; }

        [ApiMember(Name = "CustomKeyOrName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names or Custom Keys (Case-Insensitive)")]
        public string? CustomKeyOrName { get; set; }

        [ApiMember(Name = "CustomKeyOrNameOrDescription", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, Custom Keys or Descriptions (Case-Insensitive)")]
        public string? CustomKeyOrNameOrDescription { get; set; }

        [ApiMember(Name = "IDOrCustomKeyOrName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, IDs or Custom Keys (Case-Insensitive)")]
        public string? IDOrCustomKeyOrName { get; set; }

        [ApiMember(Name = "IDOrCustomKeyOrNameOrDescription", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Will return objects with same or similar Names, IDs, Custom Keys or Descriptions (Case-Insensitive)")]
        public string? IDOrCustomKeyOrNameOrDescription { get; set; }
    }
}
