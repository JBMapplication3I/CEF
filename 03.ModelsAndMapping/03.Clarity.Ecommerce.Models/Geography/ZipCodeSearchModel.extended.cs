// <copyright file="ZipCodeSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zip code search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the zip code search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IZipCodeSearchModel"/>
    public partial class ZipCodeSearchModel
    {
        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The zip code.</value>
        [ApiMember(Name = nameof(ZipCode), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Zip Code Number (5-digit)")]
        public string? ZipCode { get; set; }
    }
}
