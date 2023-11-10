// <copyright file="HistoricalCurrencyRateSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical currency rate search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the historical currency rate search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IHistoricalCurrencyRateSearchModel"/>
    public partial class HistoricalCurrencyRateSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StartingCurrencyKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Starting Currency Key for search")]
        public string? StartingCurrencyKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(EndingCurrencyKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Ending Currency Key for search")]
        public string? EndingCurrencyKey { get; set; }
    }
}
