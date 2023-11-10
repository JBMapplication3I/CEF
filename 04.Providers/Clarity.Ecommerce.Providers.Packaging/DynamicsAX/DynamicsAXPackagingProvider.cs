// <copyright file="DynamicsAXPackagingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Dynamics AX packaging provider class</summary>
namespace Clarity.Ecommerce.Providers.Packaging.DynamicsAx
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Models;

    /// <summary>A Dynamics AX packaging provider.</summary>
    /// <seealso cref="PackagingProviderBase"/>
    public class DynamicsAXPackagingProvider : PackagingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => DynamicsAXPackagingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<CEFActionResponse<List<IProviderShipment>>> GetItemPackagesAsync(
            int cartID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.CartItems
                .AsNoTracking()
                .Include(x => x.Product)
                .FilterByActive(true)
                .FilterSalesItemsByMasterID<CartItem, AppliedCartItemDiscount, CartItemTarget>(cartID)
                .FilterCartItemsByProductActive()
                .FilterCartItemsByProductHasSomethingToShip()
                .FilterCartItemsByHasQuantity()
                .Where(x => x.Product != null);
            var request = query
                .AsEnumerable()
                .Select(x => new FreightDimRequestDataContract
                {
                    ItemId = x.Product != null ? x.Product.CustomKey : x.Sku,
                    OrderQty = x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m),
                })
                .ToArray();
            var packagesList = new List<IProviderShipment>();
            try
            {
                if (request.Length > 0)
                {
                    DynamicsAXPackageService.SetServiceUrl(DynamicsAXPackagingProviderConfig.FreightServiceUrl);
                    var response = DynamicsAXPackageService.GetPackage(request);
                    if (response.Packages!.Any(x => !x.Success.HasValue || !x.Success.Value))
                    {
                        return Task.FromResult(CEFAR.FailingCEFAR<List<IProviderShipment>>(response.ErrorMessage));
                    }
                    packagesList.AddRange(response.Packages!.Select(MapPackageToProviderShipment));
                }
            }
            catch
            {
                return Task.FromResult(
                    CEFAR.FailingCEFAR<List<IProviderShipment>>(
                        "ERROR! Unable to create package data for all cart items."));
            }
            return Task.FromResult(packagesList.WrapInPassingCEFAR())!;
        }

        /// <summary>The map from product.</summary>
        /// <param name="package">The package.</param>
        /// <returns>An IProviderShipment.</returns>
        private static IProviderShipment MapPackageToProviderShipment(ShipCostCalculatorDataContract package) => new ProviderShipment
        {
            Weight = package.PackageWeight ?? 0m,
            WeightUnitOfMeasure = "lbs",
            Width = package.PackageWidth,
            WidthUnitOfMeasure = "in",
            HazardClass = package.PackageHazardClass,
            Height = package.PackageHeight,
            HeightUnitOfMeasure = "in",
            Depth = package.PackageDepth,
            DepthUnitOfMeasure = "in",
            DimensionalWeight = package.PackageWeight ?? 0m,
            DimensionalWeightUnitOfMeasure = "lbs",
            PackageQuantity = 1,
            ItemCode = package.PackageItemId,
            ItemName = string.Empty,
            ProductIsFreeShipping = false,
        };
    }
}
