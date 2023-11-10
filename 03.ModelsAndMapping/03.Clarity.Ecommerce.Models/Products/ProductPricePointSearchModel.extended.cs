// <copyright file="ProductPricePointSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price point search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    /// <summary>A data Model for the product price point search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IProductPricePointSearchModel"/>
    public partial class ProductPricePointSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinQuantity), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MinQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxQuantity), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MaxQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(From), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? From { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(To), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? To { get; set; }

        // Account

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? AccountName { get; set; }

        // Product
        // T4 generated

        // Store
        // T4 generated

        // Price Point
        // T4 generated

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointIDs), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public List<int?>? PricePointIDs { get; set; }

        // Currency

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CurrencyKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CurrencyName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CurrencyName { get; set; }
    }
}
