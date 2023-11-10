// <copyright file="BasicTaxesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic taxes provider class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.Basic
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A basic tax provider.</summary>
    /// <seealso cref="TaxesProviderBase"/>
    public class BasicTaxesProvider : TaxesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BasicTaxesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override Task<bool> InitAsync(string? contextProfileName)
        {
            IsInitialized = true;
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public override async Task<decimal> CalculateAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cart,
            string? contextProfileName)
        {
            // ReSharper disable once UseNullPropagationWhenPossible
            if (cart == null
                || cart.Account?.IsTaxable == false
                || cart.SalesItems?.Any(x => x.Active) != true)
            {
                return 0m;
            }
            var destination = cart.ShippingContact?.Address;
            if (destination?.RegionID == null)
            {
                return 0m;
            }
            var items = cart.SalesItems;
            while (true)
            {
                try
                {
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    decimal rate = 0m;
                    if (CEFConfigDictionary.DistrictLevelTaxesEnabled)
                    {
                        var productID = await context.Products
                            .AsNoTracking()
                            .FilterByID(cart.SalesItems.Select(x => x.ProductID).FirstOrDefault())
                            .Select(x => x.ID)
                            .SingleOrDefaultAsync()
                            .ConfigureAwait(false);
                        var storeID = await context.StoreProducts
                            .AsNoTracking()
                            .Where(x => x.SlaveID == productID)
                            .Select(y => y.MasterID)
                            .FirstOrDefaultAsync()
                            .ConfigureAwait(false);
                        var districtID = await context.StoreDistricts
                            .AsNoTracking()
                            .Where(x => x.MasterID == storeID)
                            .Select(y => y.SlaveID)
                            .FirstOrDefaultAsync()
                            .ConfigureAwait(false);
                        rate = await context.TaxDistricts
                            .AsNoTracking()
                            .Where(x => x.DistrictID == districtID)
                            .Select(t => t.Rate)
                            .FirstOrDefaultAsync()
                            .ConfigureAwait(false);
                    }
                    else
                    {
                        try
                        {
                            rate = await context.TaxRegions
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByCustomKey(destination.PostalCode)
                        .FilterTaxRegionsByRegionID(destination.RegionID)
                        .Select(t => t.Rate)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);
                        }
                        catch (InvalidOperationException)
                        {
                            // Do Nothing
                        }
                    }
                    var total = items.Where(x => x.ProductIsTaxable).Sum(x => x.ExtendedPrice);
                    return total * (rate > 1 ? rate / 100 : rate);
                }
                catch (InvalidOperationException)
                {
                    // Do Nothing
                }
            }
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateWithLineItemsAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cart,
            string? contextProfileName)
        {
            if (cart?.ID == null
                || cart.Account?.IsTaxable == false
                || !cart.SalesItems!.Any(x => x.Active))
            {
                return new() { TotalTaxes = 0 };
            }
            var rate = 0m;
            var destination = cart.ShippingContact?.Address;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (CEFConfigDictionary.DistrictLevelTaxesEnabled)
            {
                var productID = await context.Products
                    .AsNoTracking()
                    .FilterByID(cart!.SalesItems!.Select(x => x.ProductID).FirstOrDefault())
                    .Select(x => x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                var storeID = await context.StoreProducts
                    .AsNoTracking()
                    .Where(x => x.SlaveID == productID)
                    .Select(y => y.MasterID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                var districtID = await context.StoreDistricts
                    .AsNoTracking()
                    .Where(x => x.MasterID == storeID)
                    .Select(y => y.SlaveID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                rate = Contract.CheckValidID(districtID)
                    ? await context.TaxDistricts
                        .AsNoTracking()
                        .Where(x => x.DistrictID == districtID)
                        .Select(t => t.Rate)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false)
                    : 0m;
            }
            else
            {
                var taxRate = Contract.CheckValidID(destination?.RegionID)
                    ? await context.TaxRegions
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByCustomKey(destination!.PostalCode)
                        .FilterTaxRegionsByRegionID(destination.RegionID)
                        .Select(t => t.Rate)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false)
                    : 0m;
            }
            var result = new TaxesResult
            {
                CartID = cart.ID,
                CartSessionID = cart.SessionID,
                TaxLineItems = cart.SalesItems!
                    .Select(x => new TaxLineItemResult
                    {
                        CartItemID = x.ID,
                        ProductID = x.ProductID,
                        SKU = x.Sku,
                        Tax = x.ProductIsTaxable
                            ? x.ExtendedPrice * (rate > 1m ? rate / 100m : rate)
                            : 0m,
                    })
                    .ToList(),
            };
            result.TotalTaxes = result.TaxLineItems.Sum(x => x.Tax);
            return result;
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> CommitAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cartModel,
            string purchaseOrderNumber,
            string? contextProfileName)
        {
            return Task.FromResult<CEFActionResponse<string>>(
                string.Empty.WrapInPassingCEFAR(
                    "NOTE! BasicTaxesProvider does not implement this functionality as it is not needed.")!);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> VoidAsync(string taxTransactionID, string? contextProfileName)
        {
            return Task.FromResult<CEFActionResponse<string>>(
                string.Empty.WrapInPassingCEFAR(
                    "NOTE! BasicTaxesProvider does not implement this functionality as it is not needed.")!);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> TestServiceAsync()
        {
            return Task.FromResult(CEFAR.PassingCEFAR());
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userID,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null)
        {
            try
            {
                var result = new TaxesResult
                {
                    TaxLineItems = new(),
                    TotalTaxes = 0m,
                };
                if (cart == null)
                {
                    return result;
                }
                bool isTaxable;
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                if (cart.Account == null)
                {
                    if (Contract.CheckValidID(cart.UserID))
                    {
                        Contract.RequiresNotNull(context.Accounts); // Tells unit tests what's wrong instead of an obscure error
                        var taxable = await Contract.RequiresNotNull(context.Users)
                            .AsNoTracking()
                            .FilterByID(cart.UserID)
                            .Select(x => (bool?)x.Account!.IsTaxable)
                            .SingleOrDefaultAsync();
                        isTaxable = taxable ?? true; // if null, assume true
                    }
                    else
                    {
                        isTaxable = true;
                    }
                }
                else
                {
                    isTaxable = cart.Account.IsTaxable;
                }
                if (!isTaxable || !cart.SalesItems!.Any(x => x.Active))
                {
                    return result;
                }
                var rate = await GetRateAsync(cart, context).ConfigureAwait(false);
                if (rate <= 0m)
                {
                    return result;
                }
                result.TaxLineItems.AddRange(cart.SalesItems!.Select(x => new TaxLineItemResult
                {
                    CartItemID = x.ID,
                    ProductID = x.ProductID,
                    SKU = x.Sku,
                    Tax = x.ProductIsTaxable
                        ? x.ExtendedPrice * (rate > 1m ? rate / 100m : rate)
                        : 0m,
                }));
                result.TotalTaxes += result.TaxLineItems.Sum(x => x.Tax);
                return result;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(BasicTaxesProvider)}.{nameof(CalculateCartAsync)}.{ex.GetType().Name}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return new()
                {
                    ErrorMessages = new()
                    {
                        "An exception occurred: " + ex.Message,
                    },
                };
            }
        }

#pragma warning disable 1998
        /// <inheritdoc/>
        public override async Task<TaxesResult> CommitCartAsync(
            ICartModel cart,
            int? userID,
            string? contextProfileName,
            string? purchaseOrderNumber = null,
            string? vatId = null)
        {
            return new()
            {
                TaxLineItems = new(),
                TotalTaxes = 0,
            };
        }

        /// <inheritdoc/>
        public override async Task CommitReturnAsync(
            ISalesReturnModel salesReturn,
            string description,
            string? contextProfileName)
        {
        }

        /// <inheritdoc/>
        public override async Task VoidOrderAsync(ISalesOrderModel salesOrder, string? contextProfileName)
        {
        }

        /// <inheritdoc/>
        public override async Task VoidReturnAsync(ISalesReturnModel salesReturn, string? contextProfileName)
        {
        }
#pragma warning restore 1998

        private static async Task<decimal> GetRateAsync(ISalesCollectionBaseModel cart, IClarityEcommerceEntities context)
        {
            if (cart.Store?.SerializableAttributes != null
                && cart.Store.SerializableAttributes.ContainsKey("StoreTaxRate")
                && decimal.TryParse(cart.Store.SerializableAttributes["StoreTaxRate"].Value, out var storeTaxRate))
            {
                return storeTaxRate;
            }
            var rate = 0m;
            var cartModelForTaxRate = await context.Carts.Where(x => x.ID == cart.ID).SingleOrDefaultAsync();
            if (CEFConfigDictionary.DistrictLevelTaxesEnabled
                    && cartModelForTaxRate != null
                    && cartModelForTaxRate.SalesItems!.Any(x => x.Active))
            {
                var productID = await context.Products
                    .AsNoTracking()
                    .FilterByID(cartModelForTaxRate!.SalesItems!.Select(x => x.ProductID).FirstOrDefault())
                    .Select(x => x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                var storeID = await context.StoreProducts
                    .AsNoTracking()
                    .Where(x => x.SlaveID == productID)
                    .Select(y => y.MasterID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                var districtID = await context.StoreDistricts
                    .AsNoTracking()
                    .Where(x => x.MasterID == storeID)
                    .Select(y => y.SlaveID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                rate = Contract.CheckValidID(districtID)
                    ? await context.TaxDistricts
                        .AsNoTracking()
                        .Where(x => x.DistrictID == districtID)
                        .Select(t => t.Rate)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false)
                    : 0m;
            }
            else
            {
                if (!Contract.CheckValidID(cart.ShippingContact?.Address?.RegionID)
                    || !Contract.CheckValidKey(cart.ShippingContact!.Address!.PostalCode))
                {
                    return 0m;
                }
                rate = await context.TaxRegions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByCustomKey(cart.ShippingContact.Address.PostalCode, true)
                    .FilterTaxRegionsByRegionID(cart.ShippingContact.Address.RegionID)
                    .Select(x => x.Rate)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            return rate > 0m ? rate : 0m;
        }

        /// <inheritdoc/>
        public override async Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userID,
            int? currentAccountID,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null)
        {
            try
            {
                var result = new TaxesResult
                {
                    TaxLineItems = new(),
                    TotalTaxes = 0m,
                };
                if (cart == null)
                {
                    return result;
                }
                bool isTaxable;
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                if (cart.Account == null)
                {
                    if (Contract.CheckValidID(cart.UserID))
                    {
                        Contract.RequiresNotNull(context.Accounts); // Tells unit tests what's wrong instead of an obscure error
                        var taxable = await Contract.RequiresNotNull(context.Users)
                            .AsNoTracking()
                            .FilterByID(cart.UserID)
                            .Select(x => (bool?)x.Account!.IsTaxable)
                            .SingleOrDefaultAsync();
                        isTaxable = taxable ?? true; // if null, assume true
                    }
                    else
                    {
                        isTaxable = true;
                    }
                }
                else
                {
                    isTaxable = cart.Account.IsTaxable;
                }
                if (!isTaxable || !cart.SalesItems!.Any(x => x.Active))
                {
                    return result;
                }
                var rate = await GetRateAsync(cart, context).ConfigureAwait(false);
                if (rate <= 0m)
                {
                    return result;
                }
                result.TaxLineItems.AddRange(cart.SalesItems!.Select(x => new TaxLineItemResult
                {
                    CartItemID = x.ID,
                    ProductID = x.ProductID,
                    SKU = x.Sku,
                    Tax = x.ProductIsTaxable
                        ? x.ExtendedPrice * (rate > 1m ? rate / 100m : rate)
                        : 0m,
                }));
                result.TotalTaxes += result.TaxLineItems.Sum(x => x.Tax);
                return result;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(BasicTaxesProvider)}.{nameof(CalculateCartAsync)}.{ex.GetType().Name}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return new()
                {
                    ErrorMessages = new()
                    {
                        "An exception occurred: " + ex.Message,
                    },
                };
            }
        }
    }
}
