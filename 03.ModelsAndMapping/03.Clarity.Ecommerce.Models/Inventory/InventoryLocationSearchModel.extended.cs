// <copyright file="InventoryLocationSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the inventory location search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IInventoryLocationSearchModel"/>
    public partial class InventoryLocationSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CountryName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CountryName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? StateName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PostalCode), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? PostalCode { get; set; }
    }
}
