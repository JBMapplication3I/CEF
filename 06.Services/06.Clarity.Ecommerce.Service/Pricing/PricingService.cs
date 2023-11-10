// <copyright file="PricingService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pricing service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Pricing/Prices/ForProduct/{ProductID}/Quantity/{Quantity}", Verbs = "POST",
            Summary = "Calculate the price for a product by ID.")]
    public class CalculatePricesForProduct : IReturn<CEFActionResponse<CalculatedPrice>>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int ProductID { get; set; }

        [ApiMember(Name = nameof(Quantity), DataType = "decimal", ParameterType = "path", IsRequired = true)]
        public decimal Quantity { get; set; }
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Route("/Pricing/Prices/ForProducts", Verbs = "POST",
            Summary = "Calculate the prices for multiple products by IDs.")]
    public class CalculatePricesForProducts : IReturn<CEFActionResponse<Dictionary<int, CalculatedPrice>>>
    {
        [ApiMember(Name = nameof(ProductIDs), DataType = "List<int>", ParameterType = "query", IsRequired = true)]
        public List<int> ProductIDs { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront,
        Route("/Pricing/Prices/RangeForVariants", Verbs = "POST",
        Summary = "Calculates the minimum and maximum price for variants")]
    public class CalculateMinMaxVariantPrices : IReturn<CEFActionResponse<IMinMaxCalculatedPrices>>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", IsRequired = true, ParameterType = "query")]
        public int ProductID { get; set; }
    }

    public partial class PricingService : ClarityEcommerceServiceBase
    {
        public async Task<object?> Post(CalculatePricesForProduct request)
        {
            var factoryContext = await GetPricingFactoryContextWithQuantityAsync(request.Quantity).ConfigureAwait(false);
            var calculatedPrice = (CalculatedPrice)await Workflows.PricingFactory.CalculatePriceAsync(
                    productID: request.ProductID,
                    pricingFactoryContext: factoryContext,
                    salesItemAttributes: null,
                    contextProfileName: null,
                    forceNoCache: true)
                .ConfigureAwait(false);
            calculatedPrice.RelevantRules = null;
            calculatedPrice.UsedRules = null;
            calculatedPrice.PriceKey = null;
            calculatedPrice.PriceKeyAlt = null;
            calculatedPrice.PricingProvider = null;
            return calculatedPrice.IsValid
                ? calculatedPrice.WrapInPassingCEFAR()
                : CEFAR.FailingCEFAR<CalculatedPrice?>("ERROR! Unable to calculate the prices");
        }

        public async Task<object?> Post(CalculatePricesForProducts request)
        {
            // NOTE: Do not cache this endpoint for 304's, the internal pricing cache
            var factoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            var response = new CEFActionResponse<Dictionary<int, CalculatedPrice>>(
                new(),
                true);
            foreach (var id in request.ProductIDs)
            {
                var calculatedPrice = (CalculatedPrice)await Workflows.PricingFactory.CalculatePriceAsync(
                        productID: id,
                        pricingFactoryContext: factoryContext,
                        salesItemAttributes: null,
                        contextProfileName: ServiceContextProfileName,
                        forceNoCache: true)
                    .ConfigureAwait(false);
                calculatedPrice.RelevantRules = null;
                calculatedPrice.UsedRules = null;
                calculatedPrice.PriceKey = null;
                calculatedPrice.PriceKeyAlt = null;
                calculatedPrice.PricingProvider = null;
                var innerResponse = calculatedPrice.IsValid
                    ? calculatedPrice.WrapInPassingCEFAR()
                    : CEFAR.FailingCEFAR<CalculatedPrice?>(
                        $"ERROR! Unable to calculate the prices for product ID {id}");
                if (innerResponse.Messages.Any())
                {
                    response.Messages.AddRange(innerResponse.Messages);
                }
                response.Result![id] = calculatedPrice;
            }
            return response;
        }

        public async Task<object?> Post(CalculateMinMaxVariantPrices request)
        {
            var factoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            var result = await Workflows.Products.CalculatePriceRangeForVariantsAsync(
                    Contract.RequiresValidID(request.ProductID),
                    factoryContext,
                    null)
                .ConfigureAwait(false);
            if (result == null
                || result.MinPrice?.IsValid == false
                || result.MaxPrice?.IsValid == false)
            {
                return CEFAR.FailingCEFAR<IMinMaxCalculatedPrices>(
                    $"ERROR! Unable to calculate the prices for product ID {request.ProductID}");
            }
            result.MinPrice!.RelevantRules = null;
            result.MinPrice.UsedRules = null;
            result.MinPrice.PriceKey = null;
            result.MinPrice.PriceKeyAlt = null;
            result.MinPrice.PricingProvider = null;
            result.MaxPrice!.RelevantRules = null;
            result.MaxPrice.UsedRules = null;
            result.MaxPrice.PriceKey = null;
            result.MaxPrice.PriceKeyAlt = null;
            result.MaxPrice.PricingProvider = null;
            return CEFAR.PassingCEFAR(result);
        }
    }

    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Pricing/Prices/ForProduct/{ProductID}/Quantity/{Quantity}/AsUser/{UserID}", Verbs = "POST",
            Summary = "Calculate the price for a product by ID as another user. Admins only.")]
    public class GetPricesForProductAsUser : IReturn<CEFActionResponse<CalculatedPrice>>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int ProductID { get; set; }

        [ApiMember(Name = nameof(Quantity), DataType = "decimal", ParameterType = "path", IsRequired = true)]
        public decimal Quantity { get; set; }

        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int UserID { get; set; }
    }

    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Pricing/Prices/ForProductsAsUser", Verbs = "POST",
            Summary = "Calculate the prices for multiple products by IDs as another user. Admins only.")]
    public class GetPricesForProductsAsUser : IReturn<CEFActionResponse<Dictionary<int, CalculatedPrice>>>
    {
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int UserID { get; set; }

        [ApiMember(Name = nameof(ProductIDs), DataType = "List<int>", ParameterType = "query", IsRequired = true)]
        public List<int> ProductIDs { get; set; } = null!;
    }

    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Pricing/Prices/RawForProduct/ID/{ID}", Verbs = "GET",
            Summary = "Calculate the prices for multiple products by IDs as another user. Admins only.")]
    public class GetRawPricesForProduct : ImplementsIDBase, IReturn<CEFActionResponse<RawProductPricesModel>>
    {
    }

    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Pricing/Prices/RawForProduct/Update", Verbs = "PATCH",
            Summary = "Calculate the prices for multiple products by IDs as another user. Admins only.")]
    public class UpdateRawPricesForProduct : RawProductPricesModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Pricing/Prices/RawForProduct/BulkUpdate", Verbs = "PATCH",
            Summary = "Calculate the prices for multiple products by IDs as another user. Admins only.")]
    public class BulkUpdateRawPricesForProduct : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(RawPricesToPush), DataType = "List<RawProductPricesModel>", ParameterType = "body", IsRequired = true)]
        public List<RawProductPricesModel> RawPricesToPush { get; set; } = null!;
    }

    [PublicAPI, Route("/Pricing/PricePoint/ExistsNonNull/Key/{Key*}", "GET", Priority = 1)]
    public partial class CheckPricePointExistsNonNullByKey : ImplementsKeyBase, IReturn<DigestModel>
    {
    }

    [PublicAPI]
    public partial class PricingService
    {
        public async Task<object?> Post(GetPricesForProductAsUser request)
        {
            var factoryContext = await PricingFactoryContextForOtherUserAsync(
                    request.UserID,
                    request.Quantity)
                .ConfigureAwait(false);
            var calculatedPrice = (CalculatedPrice)await Workflows.PricingFactory.CalculatePriceAsync(
                    productID: request.ProductID,
                    pricingFactoryContext: factoryContext,
                    salesItemAttributes: null,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return calculatedPrice.IsValid
                ? calculatedPrice.WrapInPassingCEFAR()
                : CEFAR.FailingCEFAR<CalculatedPrice?>("ERROR! Unable to calculate the prices");
        }

        public async Task<object?> Post(GetPricesForProductsAsUser request)
        {
            // TODO: Cached Research
            var factoryContext = await PricingFactoryContextForOtherUserAsync(request.UserID).ConfigureAwait(false);
            var response = new CEFActionResponse<Dictionary<int, CalculatedPrice>>(
                new(),
                true);
            foreach (var id in request.ProductIDs)
            {
                var calculatedPrice = (CalculatedPrice)await Workflows.PricingFactory.CalculatePriceAsync(
                        productID: id,
                        pricingFactoryContext: factoryContext,
                        salesItemAttributes: null,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
                var innerResponse = calculatedPrice.IsValid
                    ? calculatedPrice.WrapInPassingCEFAR()
                    : CEFAR.FailingCEFAR<CalculatedPrice?>(
                        $"ERROR! Unable to calculate the prices for product ID {id}");
                if (innerResponse.Messages.Any())
                {
                    response.Messages.AddRange(innerResponse.Messages);
                }
                response.Result![id] = calculatedPrice;
            }
            return response;
        }

        public async Task<object?> Get(GetRawPricesForProduct request)
        {
            return await Workflows.Products.GetRawPricesAsync(
                    Contract.RequiresValidID(request.ID),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(UpdateRawPricesForProduct request)
        {
            _ = Contract.RequiresValidID(request.ID);
            return await Workflows.Products.UpdateRawPricesAsync(
                    request,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(BulkUpdateRawPricesForProduct request)
        {
            return await Workflows.Products.BulkUpdateRawPricesAsync(
                    Contract.RequiresNotEmpty(request.RawPricesToPush).ToList<IRawProductPricesModel>(),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object> Get(CheckPricePointExistsNonNullByKey request)
        {
            var model = new DigestModel();
            var id = await Workflows.PricePoints.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
            model.ID = id ?? 0;
            return model;
        }
    }
}
