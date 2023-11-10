// <copyright file="FixerIOCurrencyConversionsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fixer i/o currency conversions provider class</summary>
namespace Clarity.Ecommerce.Providers.CurrencyConversions.FixerIO
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FixerSharp;
    using Interfaces.Models;
    using Models;

    /// <summary>A fixer i/o currency conversions provider.</summary>
    /// <seealso cref="CurrencyConversionProviderBase"/>
    public class FixerIOCurrencyConversionsProvider : CurrencyConversionProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => FixerIOCurrencyConversionsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<double> ConvertAsync(string keyA, string keyB, double value, string? contextProfileName)
        {
            var now = DateExtensions.GenDateTime;
            var twelveHoursAgo = now.AddHours(-12);
            var searchModel = new HistoricalCurrencyRateSearchModel
            {
                ModifiedSince = twelveHoursAgo,
                StartingCurrencyKey = keyA,
                EndingCurrencyKey = keyB,
                Paging = new() { Size = 1, StartIndex = 1 },
                Sorts = new[] { new Sort { Field = "OnDate", Dir = "desc" } },
            };
            var historical = (await Workflows.HistoricalCurrencyRates.SearchForConnectAsync(searchModel, contextProfileName).ConfigureAwait(false)).FirstOrDefault();
            if (historical != null && historical.OnDate >= twelveHoursAgo && historical.OnDate < now)
            {
                var historicalRate = (double)historical.Rate;
                return value * historicalRate;
            }
            var rate = Fixer.Rate(keyA, keyB, now.Date);
            var converted = rate.Convert(value);
            await Workflows.HistoricalCurrencyRates.UpsertAsync(
                    new HistoricalCurrencyRateModel
                    {
                        Active = true,
                        CreatedDate = now,
                        CustomKey = $"{keyA}-{keyB}",
                        StartingCurrencyKey = rate.From,
                        EndingCurrencyKey = rate.To,
                        OnDate = rate.Date,
                        Rate = (decimal)rate.Rate,
                    },
                    contextProfileName)
                .ConfigureAwait(false);
            return converted;
        }

        /// <inheritdoc/>
        public override async Task<double> ConvertAsync(string keyA, string keyB, double value, DateTime onDate, string? contextProfileName)
        {
            var searchModel = new HistoricalCurrencyRateSearchModel
            {
                ModifiedSince = onDate.Date,
                StartingCurrencyKey = keyA,
                EndingCurrencyKey = keyB,
                Paging = new() { Size = 1, StartIndex = 1 },
                Sorts = new[] { new Sort { Field = "OnDate", Dir = "desc" } },
            };
            var historical = (await Workflows.HistoricalCurrencyRates.SearchForConnectAsync(searchModel, contextProfileName).ConfigureAwait(false)).FirstOrDefault();
            if (historical != null && historical.OnDate >= onDate.Date && historical.OnDate < onDate.Date.AddDays(1))
            {
                var historicalRate = (double)historical.Rate;
                return value * historicalRate;
            }
            var rate = Fixer.Rate(keyA, keyB, onDate);
            var converted = rate.Convert(value);
            await Workflows.HistoricalCurrencyRates.UpsertAsync(
                    new HistoricalCurrencyRateModel
                    {
                        Active = true,
                        CreatedDate = DateExtensions.GenDateTime,
                        CustomKey = $"{keyA}-{keyB}",
                        StartingCurrencyKey = rate.From,
                        EndingCurrencyKey = rate.To,
                        OnDate = rate.Date,
                        Rate = (decimal)rate.Rate,
                    },
                    contextProfileName)
                .ConfigureAwait(false);
            return converted;
        }

        /// <inheritdoc/>
        public override Task<double> ConvertAsync(IProductModel product, IAccountModel account, double value, string? contextProfileName)
        {
            throw new NotImplementedException();
        }
    }
}
