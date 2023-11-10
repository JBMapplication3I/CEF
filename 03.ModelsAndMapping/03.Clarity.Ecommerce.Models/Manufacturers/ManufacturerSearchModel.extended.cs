// <copyright file="ManufacturerSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the manufacturer search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IManufacturerSearchModel"/>
    public partial class ManufacturerSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AddressID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Address ID for search")]
        public int? AddressID { get; set; }
    }
}
