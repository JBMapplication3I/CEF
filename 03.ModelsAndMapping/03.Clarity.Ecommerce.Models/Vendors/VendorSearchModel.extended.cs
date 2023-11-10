// <copyright file="VendorSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    public partial class VendorSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Notes), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Notes for search")]
        public string? Notes { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AddressID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Address ID for search")]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TermID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Vendor Term ID for search")]
        public int? TermID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(VendorsShipViaID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Vendors Ship Via ID for search")]
        public int? VendorsShipViaID { get; set; }
    }
}
