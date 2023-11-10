// <copyright file="OracleTaxesProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle taxes provider class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.Oracle
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;

    /// <summary>An oracle taxes provider.</summary>
    /// <seealso cref="TaxesProviderBase"/>
    public class OracleTaxesProvider : TaxesProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => OracleTaxesProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
                    // Generate an Oracle Rate Request
                    var total = items.Where(x => x.ProductIsTaxable).Sum(x => x.ExtendedPrice);
                    var request = OracleTaxService.CreateOracleRequest(cart.BillingContact, cart.ShippingContact, total);
                    var response = await OracleTaxService.GetOracleResponseAsync(request).ConfigureAwait(false);
                    var taxRate = response?.ShippingTax?.FirstOrDefault()?.TaxRate ?? response?.OriginTax?.FirstOrDefault()?.TaxRate;
                    var rate = taxRate ?? 0m;
                    return total * (rate > 1m ? rate / 100m : rate);
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
            var total = cart.SalesItems!.Where(x => x.ProductIsTaxable).Sum(x => x.ExtendedPrice);
            var request = OracleTaxService.CreateOracleRequest(cart.BillingContact, cart.ShippingContact, total);
            var response = await OracleTaxService.GetOracleResponseAsync(request).ConfigureAwait(false);
            var taxRate = response?.ShippingTax?.FirstOrDefault()?.TaxRate ?? response?.OriginTax?.FirstOrDefault()?.TaxRate;
            var rate = taxRate ?? 0m;
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
                if (cart.SalesItems?.All(x => !x.Active) != false)
                {
                    return result;
                }
                var total = cart.SalesItems.Where(x => x.ProductIsTaxable).Sum(x => x.ExtendedPrice);
                var request = OracleTaxService.CreateOracleRequest(cart.BillingContact, cart.ShippingContact, total);
                var response = await OracleTaxService.GetOracleResponseAsync(request).ConfigureAwait(false);
                var taxRate = response?.ShippingTax?.FirstOrDefault()?.TaxRate ?? response?.OriginTax?.FirstOrDefault()?.TaxRate;
                var rate = taxRate ?? 0m;
                if (rate <= 0m)
                {
                    return result;
                }
                result.TaxLineItems.AddRange(cart.SalesItems.Select(x => new TaxLineItemResult
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
            catch (Exception e)
            {
                await Logger.LogErrorAsync(nameof(OracleTaxesProvider), e.Message, e, contextProfileName).ConfigureAwait(false);
                return new(); // TODO: Add an error message to the result?
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

        public override Task<TaxesResult> CalculateCartAsync(ICartModel cart, int? userId, int? currentAccountId, string? contextProfileName, TargetGroupingKey? key = null, string? vatId = null)
        {
            throw new NotImplementedException();
        }
    }
}
