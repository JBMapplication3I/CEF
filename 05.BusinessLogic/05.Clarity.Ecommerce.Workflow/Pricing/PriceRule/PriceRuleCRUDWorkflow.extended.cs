// <copyright file="PriceRuleCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class PriceRuleWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<PriceRule>> FilterQueryByModelCustomAsync(
            IQueryable<PriceRule> query,
            IPriceRuleSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterPriceRulesByMinQuantityMin(search.MinQuantityMin)
                .FilterPriceRulesByMinQuantityMax(search.MinQuantityMax)
                .FilterPriceRulesByMaxQuantityMin(search.MaxQuantityMin)
                .FilterPriceRulesByMaxQuantityMax(search.MaxQuantityMax)
                .FilterPriceRulesByStartDateMin(search.StartDateMin)
                .FilterPriceRulesByStartDateMax(search.StartDateMax)
                .FilterPriceRulesByEndDateMin(search.EndDateMin)
                .FilterPriceRulesByEndDateMax(search.EndDateMax)
                .FilterPriceRulesByPriority(search.Priority);
        }
    }
}
