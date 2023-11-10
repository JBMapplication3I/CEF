// <copyright file="RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the recommend products by categories of products previously viewed personalization provider class</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Providers.Personalization.RecommendByCategory
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Utilities;

    /// <summary>A recommend products by categories of products previously viewed personalization provider.</summary>
    /// <seealso cref="PersonalizationProviderBase"/>
    public class RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProvider : PersonalizationProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => RecommendProductsByCategoriesOfProductsPreviouslyViewedPersonalizationProviderConfig.IsValid(
                IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<List<int>> GetResultingProductIDsForUserIDAsync(
            int? userID,
            int minimum,
            string? contextProfileName)
        {
            try
            {
                var productViewCounts = GetProductViewCounts(contextProfileName);
                if (!productViewCounts.Any())
                {
                    return new();
                }
                var categoryProductsThatHaveBeenViewed = GetCategoryProductsThatHaveBeenViewed(contextProfileName);
                if (!categoryProductsThatHaveBeenViewed.Any())
                {
                    return new();
                }
                var productsViewedByEachUser = GetProductsViewedByEachUser(contextProfileName);
                if (!productsViewedByEachUser.Any())
                {
                    return new();
                }
                var thisUsersCategories = GetThisUsersCategories(
                    userID,
                    minimum,
                    categoryProductsThatHaveBeenViewed,
                    productsViewedByEachUser,
                    contextProfileName);
                var productIDs = GetProductIDsToSendToUser(
                    minimum,
                    categoryProductsThatHaveBeenViewed,
                    thisUsersCategories,
                    productViewCounts,
                    contextProfileName);
                return productIDs;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting."
                        or "The connection was not closed. The connection's current state is open."
                        or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return await GetResultingProductIDsForUserIDAsync(userID, minimum, contextProfileName).ConfigureAwait(false);
                }
                throw;
            }
        }

        /// <inheritdoc/>
        public override async Task<List<int>> GetResultingCategoryIDsForUserIDAsync(
            int? userID, int minimum, string? contextProfileName)
        {
            try
            {
                var productViewCounts = GetProductViewCounts(contextProfileName);
                if (!productViewCounts.Any())
                {
                    return new();
                }
                var categoryProductsThatHaveBeenViewed = GetCategoryProductsThatHaveBeenViewed(contextProfileName);
                if (!categoryProductsThatHaveBeenViewed.Any())
                {
                    return new();
                }
                var productsViewedByEachUser = GetProductsViewedByEachUser(contextProfileName);
                if (productsViewedByEachUser.All(x => x.Key != userID))
                {
                    return new();
                }
                var thisUsersCategories = GetThisUsersCategories(
                    userID,
                    minimum,
                    categoryProductsThatHaveBeenViewed,
                    productsViewedByEachUser,
                    contextProfileName);
                return thisUsersCategories;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting." or "The connection was not closed. The connection's current state is open." or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return await GetResultingCategoryIDsForUserIDAsync(userID, minimum, contextProfileName).ConfigureAwait(false);
                }
                throw;
            }
        }

        /// <inheritdoc/>
        public override async Task<Dictionary<int, List<int>>> GetResultingFeedForUserIDAsync(
            int? userID, int minimum, string? contextProfileName)
        {
            try
            {
                var productViewCounts = GetProductViewCounts(contextProfileName);
                if (!productViewCounts.Any())
                {
                    return new();
                }
                var categoryProductsThatHaveBeenViewed = GetCategoryProductsThatHaveBeenViewed(contextProfileName);
                if (!categoryProductsThatHaveBeenViewed.Any())
                {
                    return new();
                }
                var productsViewedByEachUser = GetProductsViewedByEachUser(contextProfileName);
                if (!productsViewedByEachUser.Any())
                {
                    return new();
                }
                var thisUsersCategories = GetThisUsersCategories(
                    userID,
                    minimum,
                    categoryProductsThatHaveBeenViewed,
                    productsViewedByEachUser,
                    contextProfileName);
                var productIDsToSendToUser = GetProductIDsToSendToUser(
                    minimum,
                    categoryProductsThatHaveBeenViewed,
                    thisUsersCategories,
                    productViewCounts,
                    contextProfileName);
                var feedData = new Dictionary<int, List<int>>();
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                foreach (var categoryID in thisUsersCategories)
                {
                    if (!feedData.ContainsKey(categoryID))
                    {
                        feedData[categoryID] = new();
                    }
                    if (categoryProductsThatHaveBeenViewed.ContainsKey(categoryID))
                    {
                        feedData[categoryID].AddRange(categoryProductsThatHaveBeenViewed[categoryID]
                            .Where(x => productIDsToSendToUser.Contains(x)).ToList());
                        if (feedData[categoryID].Count >= minimum)
                        {
                            continue;
                        }
                        var backFill = categoryProductsThatHaveBeenViewed[categoryID]
                            .Where(x => !productIDsToSendToUser.Contains(x))
                            .Take(minimum - feedData[categoryID].Count)
                            .ToList();
                        if (backFill.Any())
                        {
                            feedData[categoryID].AddRange(backFill);
                        }
                    }
                    if (feedData[categoryID].Count >= minimum)
                    {
                        continue;
                    }
                    var backFill2 = await context.Products
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByExcludedIDs(productIDsToSendToUser.ToArray())
                        .FilterProductsByAncestorCategoryID(categoryID)
                        .OrderByDescending(x => x.TotalPurchasedAmount ?? 0)
                        .Select(x => x.ID)
                        .Take(minimum - feedData[categoryID].Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
                    if (backFill2.Any())
                    {
                        feedData[categoryID].AddRange(backFill2);
                    }
                }
                return feedData;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting." or "The connection was not closed. The connection's current state is open." or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return await GetResultingFeedForUserIDAsync(userID, minimum, contextProfileName).ConfigureAwait(false);
                }
                throw;
            }
        }

        /// <summary>Gets product view counts.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product view counts.</returns>
        private static Dictionary<int, int> GetProductViewCounts(string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                return context.PageViews
                    .AsNoTracking()
                    .Include(x => x.Product)
                    .FilterByActive(true)
                    .Where(x => x.ProductID.HasValue && x.Product!.Active && !x.Product.IsDiscontinued)
                    .GroupBy(x => x.ProductID!.Value)
                    .Select(x => new { x.Key, Count = x.Count() })
                    .ToDictionary(x => x.Key, x => x.Count);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting." or "The connection was not closed. The connection's current state is open." or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return GetProductViewCounts(contextProfileName);
                }
                throw;
            }
        }

        /// <summary>Gets category products that have been viewed.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The category products that have been viewed.</returns>
        private static Dictionary<int, IEnumerable<int>> GetCategoryProductsThatHaveBeenViewed(string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                return context.PageViews
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.ProductID.HasValue && x.Product!.Active && !x.Product.IsDiscontinued)
                    .Select(x => new
                    {
                        ProductID = x.ProductID!.Value,
                        CategoryIDs = x.Product!.Categories!.Where(y => y.Active && y.Slave!.Active).Select(y => y.SlaveID),
                    })
                    .SelectMany(x => x.CategoryIDs.Select(y => new { CategoryID = y, x.ProductID }))
                    .GroupBy(x => x.CategoryID)
                    .ToDictionary(x => x.Key, v => v.Select(k => k.ProductID).Distinct());
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting." or "The connection was not closed. The connection's current state is open." or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return GetCategoryProductsThatHaveBeenViewed(contextProfileName);
                }
                throw;
            }
        }

        /// <summary>Gets products viewed by each user.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The products viewed by each user.</returns>
        private static Dictionary<int, IEnumerable<int>> GetProductsViewedByEachUser(string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                return context.PageViews
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.ProductID.HasValue && x.Product!.Active && !x.Product.IsDiscontinued)
                    .Select(x => new { x.UserID, x.ProductID })
                    .GroupBy(x => x.UserID ?? -1)
                    .ToDictionary(x => x.Key, x => x.Select(y => y.ProductID!.Value).Distinct());
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting." or "The connection was not closed. The connection's current state is open." or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return GetProductsViewedByEachUser(contextProfileName);
                }
                throw;
            }
        }

        /// <summary>Gets this users categories.</summary>
        /// <param name="userID">                            Identifier for the user.</param>
        /// <param name="minimum">                           The minimum.</param>
        /// <param name="categoryProductsThatHaveBeenViewed">The category products that have been viewed.</param>
        /// <param name="productsViewedByEachUser">          The products viewed by each user.</param>
        /// <param name="contextProfileName">                Name of the context profile.</param>
        /// <returns>this users categories.</returns>
        private static List<int> GetThisUsersCategories(
            int? userID,
            int minimum,
            IReadOnlyDictionary<int, IEnumerable<int>> categoryProductsThatHaveBeenViewed,
            IReadOnlyDictionary<int, IEnumerable<int>> productsViewedByEachUser,
            string? contextProfileName)
        {
            try
            {
                var thisUsersCategories = new List<int>();
                if (Contract.CheckValidID(userID))
                {
                    foreach (var productID in productsViewedByEachUser[userID!.Value])
                    {
                        thisUsersCategories.AddRange(categoryProductsThatHaveBeenViewed.Keys
                            .Where(categoryID => categoryProductsThatHaveBeenViewed[categoryID].Contains(productID)));
                    }
                }
                if (thisUsersCategories.Count >= minimum)
                {
                    // We have enough to send back
                    return thisUsersCategories;
                }
                // We need to add more until we reach minimum (use other categories that have view counts)
                foreach (var categoryID in categoryProductsThatHaveBeenViewed.Keys.Where(x => !thisUsersCategories.Contains(x)))
                {
                    if (thisUsersCategories.Count >= minimum)
                    {
                        break;
                    }
                    thisUsersCategories.Add(categoryID);
                }
                if (thisUsersCategories.Count >= minimum)
                {
                    // We have enough to send back
                    return thisUsersCategories;
                }
                // We need to add more until we reach minimum (use any category with a product in order of name)
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                foreach (var categoryID in context.Categories
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterCategoriesByIsVisible(true)
                    .FilterByExcludedIDs(thisUsersCategories.ToArray())
                    .Where(x => x.Products!.Any(y => y.Active && y.Master!.Active))
                    .OrderBy(x => x.Name)
                    .Select(x => x.ID))
                {
                    if (thisUsersCategories.Count >= minimum)
                    {
                        break;
                    }
                    thisUsersCategories.Add(categoryID);
                }
                return thisUsersCategories;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting." or "The connection was not closed. The connection's current state is open." or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return GetThisUsersCategories(
                        userID, minimum, categoryProductsThatHaveBeenViewed, productsViewedByEachUser, contextProfileName);
                }
                throw;
            }
        }

        /// <summary>Gets product IDs to send to user.</summary>
        /// <param name="minimum">                           The minimum.</param>
        /// <param name="categoryProductsThatHaveBeenViewed">The category products that have been viewed.</param>
        /// <param name="thisUsersCategories">               Categories this users belongs to.</param>
        /// <param name="productViewCounts">                 The product view counts.</param>
        /// <param name="contextProfileName">                Name of the context profile.</param>
        /// <returns>The product IDs to send to user.</returns>
        private static List<int> GetProductIDsToSendToUser(
            int minimum,
            IReadOnlyDictionary<int, IEnumerable<int>> categoryProductsThatHaveBeenViewed,
            IReadOnlyCollection<int> thisUsersCategories,
            IReadOnlyDictionary<int, int> productViewCounts,
            string? contextProfileName)
        {
            try
            {
                // Fill "minimum" spots with the top viewed products from these categories
                // by going through each category and taking the top 1 viewed from each,
                // then the second from each, etc. If that doesn't get enough, back-fill
                // with more products in descending order of quantity sold
                var productIDsToSendToUser = new List<int>();
                var loopCounterA = 0;
                var loopCounterB = 0;
                // Don't loop forever if there aren't enough products
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                while (productIDsToSendToUser.Count < minimum && loopCounterA < minimum)
                {
                    foreach (var categoryID in thisUsersCategories)
                    {
                        if (categoryProductsThatHaveBeenViewed.ContainsKey(categoryID))
                        {
                            ++loopCounterA;
                            var categoryProducts = categoryProductsThatHaveBeenViewed[categoryID];
                            var toAddA = categoryProducts
                                .OrderByDescending(x => productViewCounts[x])
                                .Skip(loopCounterA)
                                .Take(1)
                                .ToList();
                            if (toAddA.Any())
                            {
                                productIDsToSendToUser.AddRange(toAddA);
                            }
                        }
                        else
                        {
                            ++loopCounterB;
                            var toAddB = context.Products
                                .AsNoTracking()
                                .FilterByActive(true)
                                .FilterByExcludedIDs(productIDsToSendToUser.ToArray())
                                .FilterProductsByAncestorCategoryID(categoryID)
                                .OrderByDescending(x => x.TotalPurchasedAmount ?? 0)
                                .Select(x => x.ID)
                                .Skip(loopCounterB)
                                .Take(1)
                                .ToList();
                            if (toAddB.Any())
                            {
                                productIDsToSendToUser.AddRange(toAddB);
                            }
                        }
                    }
                }
                var productIDs = context.Products
                    .AsNoTracking()
                    .FilterByIDs(productIDsToSendToUser.ToArray())
                    .OrderBy(x => x.Name)
                    .Select(x => x.ID)
                    .Take(minimum)
                    .ToList();
                if (productIDs.Count >= minimum)
                {
                    return productIDs;
                } // We have enough
                  // We need to add more products to fill it in, doesn't matter where from
                var fillerIDs = context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterProductsByIsVisible(true)
                    .FilterProductsByIsDiscontinued(false)
                    .FilterByExcludedIDs(productIDs.ToArray())
                    .OrderByDescending(x => x.TotalPurchasedAmount ?? 0)
                    .Select(x => x.ID)
                    .Take(minimum - productIDs.Count)
                    .ToList();
                if (fillerIDs.Any())
                {
                    productIDs.AddRange(fillerIDs);
                }
                return productIDs;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("The context cannot be used while the model is being created.")
                    || ex.Message is "The connection was not closed. The connection's current state is connecting." or "The connection was not closed. The connection's current state is open." or "ExecuteReader requires an open and available Connection. The connection's current state is closed.")
                {
                    return GetProductIDsToSendToUser(
                        minimum,
                        categoryProductsThatHaveBeenViewed,
                        thisUsersCategories,
                        productViewCounts,
                        contextProfileName);
                }
                throw;
            }
        }
    }
}
