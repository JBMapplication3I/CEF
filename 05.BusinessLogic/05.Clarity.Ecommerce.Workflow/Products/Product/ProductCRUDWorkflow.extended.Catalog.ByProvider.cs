// <copyright file="ProductCRUDWorkflow.extended.Catalog.ByProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Utilities;

    public partial class ProductWorkflow
    {
        /// <summary>Gets or sets the default type identifier.</summary>
        /// <value>The default type identifier.</value>
        private static int? DefaultTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the package type.</summary>
        /// <value>The identifier of the package type.</value>
        private static int? PackageTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the master pack type.</summary>
        /// <value>The identifier of the master pack type.</value>
        private static int? MasterPackTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the pallet type.</summary>
        /// <value>The identifier of the pallet type.</value>
        private static int? PalletTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the non physical type.</summary>
        /// <value>The identifier of the non physical type.</value>
        private static int? NonPhysicalTypeID { get; set; }

        /// <summary>Enforce "Good Data" Rules like must have a Type on the Products.</summary>
        /// <param name="productIDs">        The product IDs.</param>
        /// <param name="context">           The context.</param>
        /// <returns>A Task.</returns>
        public async Task CleanProductsAsync(int[] productIDs, IClarityEcommerceEntities context)
        {
            var idsToFix = await context.Products
                .FilterByIDs(productIDs)
                .Select(x => x.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            if (idsToFix.Count == 0)
            {
                return;
            }
            CleanProductsRule02(productIDs, context);
            await CleanProductsRule03Async(productIDs, context).ConfigureAwait(false);
            // TODO@JTG: CleanProductsRule04: If there is no primary image, set one
        }

        /// <summary>Clean products rule 02.</summary>
        /// <param name="idsToFix">The identifiers to fix.</param>
        /// <param name="context"> The context.</param>
        private static void CleanProductsRule02(IEnumerable<int> idsToFix, IClarityEcommerceEntities context)
        {
            // Rule 2: Must have blank StockQuantity and StockQuantityAllocated values when using PILS (PILS overrides simple stock values)
            var changed = false;
            if (CEFConfigDictionary.InventoryAdvancedEnabled)
            {
                foreach (var id in idsToFix)
                {
                    var product = context.Products.FilterByID(id).Single();
                    if (product.StockQuantity != null)
                    {
                        product.StockQuantity = null;
                        changed = true;
                    }
                    if (product.StockQuantityAllocated == null)
                    {
                        continue;
                    }
                    product.StockQuantityAllocated = null;
                    changed = true;
                }
            }
            else
            {
                // Delete all PILS
            }
            if (changed)
            {
                context.SaveUnitOfWork(true);
            }
        }

        /// <summary>Ensures that package type IDs.</summary>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        private async Task EnsurePackageTypeIDsAsync(IClarityEcommerceEntities context)
        {
            PackageTypeID ??= Contract.CheckValidKey(context.ContextProfileName) // Tests just assume the ID
                ? 1
                : await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Package",
                        "Package",
                        "Package",
                        null,
                        context)
                    .ConfigureAwait(false);
            MasterPackTypeID ??= Contract.CheckValidKey(context.ContextProfileName)
                ? 2
                : await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Master Pack",
                        "Master Pack",
                        "Master Pack",
                        null,
                        context)
                    .ConfigureAwait(false);
            PalletTypeID ??= Contract.CheckValidKey(context.ContextProfileName)
                ? 3
                : await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Pallet",
                        "Pallet",
                        "Pallet",
                        null,
                        context)
                    .ConfigureAwait(false);
            NonPhysicalTypeID ??= Contract.CheckValidKey(context.ContextProfileName)
                ? 4
                : await Workflows.PackageTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Non-Physical",
                        "Non-Physical",
                        "Non-Physical",
                        null,
                        context)
                    .ConfigureAwait(false);
        }

        /// <summary>Clean products rule 03.</summary>
        /// <param name="idsToFix">The identifiers to fix.</param>
        /// <param name="context"> The context.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once FunctionComplexityOverflow
        private async Task CleanProductsRule03Async(IEnumerable<int> idsToFix, IClarityEcommerceEntities context)
        {
            // Rule 3: OOB dimensions and the multiple Packages dimensions must be fleshed out with their data where possible (missing UofMs, etc.)
            await EnsurePackageTypeIDsAsync(context).ConfigureAwait(false);
            var changed = false;
            foreach (var id in idsToFix)
            {
                var product = await context.Products.FilterByID(id).SingleAsync();
                if (!Contract.CheckValidKey(product.DepthUnitOfMeasure))
                {
                    product.DepthUnitOfMeasure = "in";
                    changed = true;
                }
                if (!Contract.CheckValidKey(product.WidthUnitOfMeasure))
                {
                    product.WidthUnitOfMeasure = "in";
                    changed = true;
                }
                if (!Contract.CheckValidKey(product.HeightUnitOfMeasure))
                {
                    product.HeightUnitOfMeasure = "in";
                    changed = true;
                }
                if (!Contract.CheckValidKey(product.WeightUnitOfMeasure))
                {
                    product.WeightUnitOfMeasure = "lbs";
                    changed = true;
                }
                if (product.Package != null)
                {
                    if (!Contract.CheckValidKey(product.Package.DepthUnitOfMeasure))
                    {
                        product.Package.DepthUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Package.WidthUnitOfMeasure))
                    {
                        product.Package.WidthUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Package.HeightUnitOfMeasure))
                    {
                        product.Package.HeightUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Package.WeightUnitOfMeasure))
                    {
                        product.Package.WeightUnitOfMeasure = "lbs";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Package.DimensionalWeightUnitOfMeasure))
                    {
                        product.Package.DimensionalWeightUnitOfMeasure = "lbs";
                        changed = true;
                    }
                    if (!Contract.CheckValidID(product.Package.TypeID))
                    {
                        product.Package.TypeID = PackageTypeID!.Value;
                        changed = true;
                    }
                }
                if (product.MasterPack != null)
                {
                    if (!Contract.CheckValidKey(product.MasterPack.DepthUnitOfMeasure))
                    {
                        product.MasterPack.DepthUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.MasterPack.WidthUnitOfMeasure))
                    {
                        product.MasterPack.WidthUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.MasterPack.HeightUnitOfMeasure))
                    {
                        product.MasterPack.HeightUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.MasterPack.WeightUnitOfMeasure))
                    {
                        product.MasterPack.WeightUnitOfMeasure = "lbs";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.MasterPack.DimensionalWeightUnitOfMeasure))
                    {
                        product.MasterPack.DimensionalWeightUnitOfMeasure = "lbs";
                        changed = true;
                    }
                    if (!Contract.CheckValidID(product.MasterPack.TypeID))
                    {
                        product.MasterPack.TypeID = MasterPackTypeID!.Value;
                        changed = true;
                    }
                }
                if (product.Pallet != null)
                {
                    if (!Contract.CheckValidKey(product.Pallet.DepthUnitOfMeasure))
                    {
                        product.Pallet.DepthUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Pallet.WidthUnitOfMeasure))
                    {
                        product.Pallet.WidthUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Pallet.HeightUnitOfMeasure))
                    {
                        product.Pallet.HeightUnitOfMeasure = "in";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Pallet.WeightUnitOfMeasure))
                    {
                        product.Pallet.WeightUnitOfMeasure = "lbs";
                        changed = true;
                    }
                    if (!Contract.CheckValidKey(product.Pallet.DimensionalWeightUnitOfMeasure))
                    {
                        product.Pallet.DimensionalWeightUnitOfMeasure = "lbs";
                        changed = true;
                    }
                    if (!Contract.CheckValidID(product.Pallet.TypeID))
                    {
                        product.Pallet.TypeID = PalletTypeID!.Value;
                        changed = true;
                    }
                }
            }
            if (changed)
            {
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
        }
    }
}
