// <copyright file="PriceRuleSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Price Rule Search Model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using ServiceStack;

    /// <summary>A data Model for the price rule search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IPriceRuleSearchModel"/>
    public partial class PriceRuleSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Priority), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The priority of the price rule, must match exact value if set.")]
        public int? Priority { get; set; }

        /// <inheritdoc/>
        public decimal? MinQuantityMin { get; set; }

        /// <inheritdoc/>
        public decimal? MinQuantityMax { get; set; }

        /// <inheritdoc/>
        public decimal? MaxQuantityMin { get; set; }

        /// <inheritdoc/>
        public decimal? MaxQuantityMax { get; set; }

        /// <inheritdoc/>
        public DateTime? StartDateMin { get; set; }

        /// <inheritdoc/>
        public DateTime? StartDateMax { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDateMin { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDateMax { get; set; }
    }
}
