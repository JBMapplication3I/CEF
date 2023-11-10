// <copyright file="PriceRoundingCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rounding workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;

    public partial class PriceRoundingWorkflow
    {
        /// <inheritdoc/>
        public Task<IPriceRoundingModel?> GetAsync(IPriceRoundingSearchModel searchModel, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.PriceRoundings
                    .AsNoTracking()
                    .FilterPriceRoundingsByProductKey(searchModel.ProductKey)
                    .FilterPriceRoundingsByPricePointKey(searchModel.PricePointKey)
                    .FilterPriceRoundingsByCurrencyKey(searchModel.CurrencyKey)
                    .FilterPriceRoundingsByUnitOfMeasure(searchModel.UnitOfMeasure)
                    .SelectFirstFullPriceRoundingAndMapToPriceRoundingModel(contextProfileName));
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<PriceRounding>> FilterQueryByModelCustomAsync(
            IQueryable<PriceRounding> query,
            IPriceRoundingSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterPriceRoundingsByProductKey(search.ProductKey)
                .FilterPriceRoundingsByPricePointKey(search.PricePointKey)
                .FilterPriceRoundingsByCurrencyKey(search.CurrencyKey)
                .FilterPriceRoundingsByUnitOfMeasure(search.UnitOfMeasure);
        }
    }
}
