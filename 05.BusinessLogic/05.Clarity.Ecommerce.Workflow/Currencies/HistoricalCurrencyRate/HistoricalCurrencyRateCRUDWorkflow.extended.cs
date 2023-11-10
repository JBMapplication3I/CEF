// <copyright file="HistoricalCurrencyRateCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical currency rate workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class HistoricalCurrencyRateWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<HistoricalCurrencyRate>> FilterQueryByModelCustomAsync(
            IQueryable<HistoricalCurrencyRate> query,
            IHistoricalCurrencyRateSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterHistoricalCurrencyRatesByStartingCurrencyKey(search.StartingCurrencyKey)
                .FilterHistoricalCurrencyRatesByEndingCurrencyKey(search.EndingCurrencyKey);
        }
    }
}
