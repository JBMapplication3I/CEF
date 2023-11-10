// <copyright file="BasicPackagingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic packaging provider class</summary>
namespace Clarity.Ecommerce.Providers.Packaging.Basic
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Models;
    using Utilities;

    /// <inheritdoc/>
    public class BasicPackagingProvider : PackagingProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BasicPackagingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IProviderShipment>>> GetItemPackagesAsync(
            int cartID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.CartItems
                    .AsNoTracking()
                    .Where(x => x.Active
                        && x.MasterID == cartID
                        && x.Product != null
                        && x.Product.Active
                        && x.Product.PackageID > 0
                        && !x.Product.NothingToShip
                        && x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m) > 0m)
                    .FilterObjectsWithJsonAttributesByValues(
                        BasicPackagingProviderConfig.UseSpecificShipOptions
                        && Contract.CheckValidKey(BasicPackagingProviderConfig.SpecificShipOptions)
                            ? new()
                            {
                                ["ShipOption"] = new[] { BasicPackagingProviderConfig.SpecificShipOptions },
                            }
                            : null)
                    .Select(x => new
                    {
                        TotalItemQuantity = x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m),
                        ItemName = x.Product!.Name,
                        ItemCode = x.Product.CustomKey,
                        x.Product.Package!.Weight,
                        x.Product.Package.WeightUnitOfMeasure,
                        x.Product.Package.Width,
                        x.Product.Package.WidthUnitOfMeasure,
                        x.Product.Package.Height,
                        x.Product.Package.HeightUnitOfMeasure,
                        x.Product.Package.Depth,
                        x.Product.Package.DepthUnitOfMeasure,
                        x.Product.Package.DimensionalWeight,
                        x.Product.Package.DimensionalWeightUnitOfMeasure,
                        x.Product.IsFreeShipping,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new ProviderShipment
                {
                    ItemName = x.ItemName,
                    ItemCode = x.ItemCode,
                    Weight = x.Weight,
                    WeightUnitOfMeasure = x.WeightUnitOfMeasure,
                    Width = x.Width,
                    WidthUnitOfMeasure = x.WidthUnitOfMeasure,
                    Height = x.Height,
                    HeightUnitOfMeasure = x.HeightUnitOfMeasure,
                    Depth = x.Depth,
                    DepthUnitOfMeasure = x.DepthUnitOfMeasure,
                    DimensionalWeight = x.DimensionalWeight,
                    DimensionalWeightUnitOfMeasure = x.DimensionalWeightUnitOfMeasure,
                    ProductIsFreeShipping = x.IsFreeShipping,
                    PackageQuantity = x.TotalItemQuantity > 1m ? x.TotalItemQuantity : 1m,
                })
                .ToList<IProviderShipment>()
                .WrapInPassingCEFARIfNotNull();
        }
    }
}
