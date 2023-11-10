// <copyright file="AdStoreSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad store search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the ad store search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="IAdStoreSearchModel"/>
    public partial class AdStoreSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserID), IsRequired = false, ParameterType = "query", DataType = "int?",
            Description = "User ID For Search")]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ZoneID), IsRequired = false, ParameterType = "query", DataType = "int?",
            Description = "Zone ID For Search")]
        public int? ZoneID { get; set; }
    }
}
