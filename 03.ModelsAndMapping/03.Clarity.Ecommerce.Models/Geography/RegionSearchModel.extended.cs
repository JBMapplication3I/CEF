// <copyright file="RegionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the region search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IRegionSearchModel"/>
    public partial class RegionSearchModel
    {
        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        [ApiMember(Name = nameof(RegionID), DataType = "int", ParameterType = "query", IsRequired = false,
            Description = "The Region ID")]
        public int? RegionID { get; set; }
    }
}
