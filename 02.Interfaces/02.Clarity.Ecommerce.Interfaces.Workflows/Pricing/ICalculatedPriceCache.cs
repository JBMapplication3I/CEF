// <copyright file="ICalculatedPriceCache.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICalculatedPriceCache interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Pricing
{
    using System;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for calculated price cache.</summary>
    public interface ICalculatedPriceCache
    {
        /// <summary>Gets a calculated price using the given price key.</summary>
        /// <param name="priceKey">The price key to get.</param>
        /// <returns>A CalculatedPrice.</returns>
        Task<ICalculatedPrice> GetAsync(string priceKey);

        /// <summary>Sets the calculated price into the cache.</summary>
        /// <param name="priceKey">       The price key.</param>
        /// <param name="calculatedPrice">The calculated price.</param>
        /// <param name="timeToLive">     The time to live.</param>
        /// <returns>A Task.</returns>
        Task SetAsync(string priceKey, ICalculatedPrice calculatedPrice, TimeSpan? timeToLive = null);
    }
}
