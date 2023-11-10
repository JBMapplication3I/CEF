// <copyright file="ImporterProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the importer provider base class</summary>
namespace Clarity.Ecommerce.Providers.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Interfaces.Models;
    using Interfaces.Models.Import;
    using Interfaces.Providers.Importer;
    using Models;
    using Utilities;

    /// <summary>An importer provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IImporterProviderBase"/>
    public abstract partial class ImporterProviderBase : ProviderBase, IImporterProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Importer;

        /// <inheritdoc/>
        public abstract override bool HasValidConfiguration { get; }

        /// <inheritdoc/>
        public override bool HasDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <summary>Gets a list of parsing errors.</summary>
        /// <value>A List of parsing errors.</value>
        protected List<string> ParsingErrorList { get; } = new();

        /// <inheritdoc/>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        public async Task<CEFActionResponse> IntegrateProductsAsync(
            ISpreadsheetImportModel spreadsheetModel,
            string? contextProfileName)
        {
            var loadRet = await LoadAsync(spreadsheetModel).ConfigureAwait(false);
            if (!loadRet)
            {
                return CEFAR.FailingCEFAR("Error loading Google spreadsheet");
            }
            var items = (await ParseAsync(contextProfileName).ConfigureAwait(false))?.ToList();
            var errors = new List<string>();
            errors.AddRange(await GetParsingErrorAsync().ConfigureAwait(false));
            var inventoryProvider = RegistryLoaderWrapper.GetInventoryProvider(contextProfileName);
            if (items?.Any() != true)
            {
                return CEFAR.FailingCEFAR(errors.ToArray());
            }
            try
            {
                // RESOLVE ITEMS FROM IMPORTER TO CEF MODELS
                var models = await ResolveAsync(
                        items,
                        async key => (ProductModel?)await Workflows.Products.GetFullAsync(key, contextProfileName).ConfigureAwait(false),
                        contextProfileName)
                    .ConfigureAwait(false);
                if (models?.Any() != true)
                {
                    return CEFAR.FailingCEFAR("Error while resolving items");
                }
                foreach (var (model, pricing, inventory) in models.Where(model => !string.IsNullOrEmpty(model.model?.CustomKey)))
                {
                    if (model is null)
                    {
                        continue;
                    }
                    try
                    {
                        var entityID = await Workflows.Products.CheckExistsAsync(model.CustomKey!, contextProfileName).ConfigureAwait(false);
                        if (model.ProductAssociations?.Any() == true)
                        {
                            // Create associated product if not exist
                            foreach (var assocProduct in model.ProductAssociations)
                            {
                                try
                                {
                                    var existingID = Contract.CheckValidKey(assocProduct.Slave?.CustomKey)
                                        ? await Workflows.Products.CheckExistsAsync(assocProduct.Slave!.CustomKey!, contextProfileName).ConfigureAwait(false)
                                        : Contract.CheckValidKey(assocProduct.SlaveKey)
                                            ? await Workflows.Products.CheckExistsAsync(assocProduct.SlaveKey!, contextProfileName).ConfigureAwait(false)
                                            : null;
                                    if (Contract.CheckInvalidID(existingID))
                                    {
                                        if (Contract.CheckInvalidID(GeneralTypeID))
                                        {
                                            GeneralTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                                                    byID: null,
                                                    byKey: "General",
                                                    byName: "General",
                                                    byDisplayName: "General",
                                                    model: null,
                                                    contextProfileName: contextProfileName)
                                                .ConfigureAwait(false);
                                        }
                                        existingID = (await Workflows.Products.CreateAsync(
                                                    new ProductModel
                                                    {
                                                        Active = true,
                                                        CustomKey = assocProduct.Slave?.CustomKey ?? assocProduct.SlaveKey,
                                                        CreatedDate = DateExtensions.GenDateTime,
                                                        Name = assocProduct.Slave?.CustomKey ?? assocProduct.SlaveKey,
                                                        TypeID = GeneralTypeID,
                                                        StatusID = NormalStatusID,
                                                        IsVisible = false,
                                                    },
                                                    contextProfileName)
                                                .ConfigureAwait(false))
                                            .Result;
                                    }
                                    assocProduct.Active = true;
                                    assocProduct.SlaveID = existingID!.Value;
                                    assocProduct.MasterKey = model.CustomKey;
                                }
                                catch (Exception ex)
                                {
                                    var msg = $"Error associating product: {assocProduct?.Slave?.CustomKey ?? assocProduct?.SlaveKey ?? "No Associated Product Key!"}"
                                        + $" To: {model.CustomKey ?? "No Primary Product Key!"} - Error : {ex.Message}";
                                    await Logger.LogErrorAsync("ProductImport.Error", msg, ex, contextProfileName).ConfigureAwait(false);
                                    errors.Add(msg);
                                }
                            }
                            model.ProductAssociations.RemoveAll(
                                x => !Contract.CheckValidIDOrAnyValidKey(x.SlaveID, x.SlaveKey, x.Slave?.CustomKey));
                        }
                        // SET PRODUCT KEY TO STORES -- WILL NOT CREATE IF NOT SET
                        model.Stores?.ForEach(s => s.MasterKey = model.CustomKey);
                        // model.ProductInventoryLocationSections.First().InventoryLocationSection.InventoryLocation
                        // Importer doesn't set a type, set General by default
                        if (model.Type == null)
                        {
                            var type = (TypeModel)(await Workflows.ProductTypes.GetAsync(
                                    GeneralTypeID,
                                    contextProfileName)
                                .ConfigureAwait(false))!;
                            model.Type = type;
                            model.TypeID = type.ID;
                        }
                        if (model.Status == null)
                        {
                            var status = (StatusModel)(await Workflows.ProductStatuses.GetAsync(
                                    NormalStatusID,
                                    contextProfileName)
                                .ConfigureAwait(false))!;
                            model.Status = status;
                            model.StatusID = status.ID;
                        }
                        model.ID = entityID ?? -1;
                        await Workflows.Products.UpsertAsync(model, contextProfileName).ConfigureAwait(false);
                        if (!Contract.CheckValidID(model.ID))
                        {
                            model.ID = (await Workflows.Products.CheckExistsAsync(
                                        model.CustomKey!,
                                        contextProfileName)
                                    .ConfigureAwait(false))!
                                .Value;
                        }
                        if (!Contract.CheckValidID(model.ID))
                        {
                            model.ID = await Workflows.Products.CheckExistsByNameAsync(
                                    model.Name!,
                                    contextProfileName)
                                .ConfigureAwait(false)
                                ?? throw new InvalidOperationException(
                                    "ERROR! No product ID could be detected after main record import by CustomKey or Name,"
                                    + " please use at least one of these two fields when importing from a spreadsheet");
                        }
                        if (!Contract.CheckValidID(model.ID))
                        {
                            throw new InvalidOperationException(
                                "ERROR! No product ID could be detected after main record import by CustomKey or Name,"
                                + " please use at least one of these two fields when importing from a spreadsheet");
                        }
                        if (pricing != null)
                        {
                            pricing.ID = model.ID;
                            var response = await Workflows.Products.UpdateRawPricesAsync(
                                    pricing,
                                    contextProfileName)
                                .ConfigureAwait(false);
                            if (!response.ActionSucceeded)
                            {
                                throw new InvalidOperationException(
                                    "ERROR! Something when wrong attempting to set the pricing: "
                                    + response.Messages.Aggregate((c, n) => c + ". " + n));
                            }
                        }
                        if (inventory is not null && inventoryProvider is not null)
                        {
                            var response = await inventoryProvider.UpdateInventoryForProductAsync(
                                    productID: model.ID,
                                    quantity: inventory.Quantity,
                                    quantityAllocated: inventory.QuantityAllocated,
                                    quantityPreSold: inventory.QuantityPreSold,
                                    relevantLocationID: null,
                                    relevantHash: null,
                                    contextProfileName: contextProfileName)
                                .ConfigureAwait(false);
                            if (!response.ActionSucceeded)
                            {
                                throw new InvalidOperationException(
                                    "ERROR! Something when wrong attempting to set the inventory: "
                                    + response.Messages.Aggregate((c, n) => c + ". " + n));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var msg = $"Error with product: {model.CustomKey ?? "No Key!"} - Error : {ex.Message}";
                        await Logger.LogErrorAsync("ProductImport.Error", msg, ex, contextProfileName).ConfigureAwait(false);
                        errors.Add(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = $"Error while parsing spreadsheet : {ex.Message}";
                await Logger.LogErrorAsync("ProductImport.Error", msg, ex, contextProfileName).ConfigureAwait(false);
                errors.Add(msg);
            }
            return CEFAR.FailingCEFAR(errors.ToArray());
        }

        /// <inheritdoc/>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        public async Task<CEFActionResponse> UpdateSeoKeywordsAsync(
            ISpreadsheetImportModel spreadsheetModel,
            string? contextProfileName)
        {
            var loadRet = await LoadAsync(spreadsheetModel).ConfigureAwait(false);
            if (!loadRet)
            {
                return CEFAR.FailingCEFAR("Error loading Google spreadsheet");
            }
            var items = (await ParseAsync(contextProfileName).ConfigureAwait(false))?.ToList();
            var errors = new List<string>();
            errors.AddRange(await GetParsingErrorAsync().ConfigureAwait(false));
            if (items?.Any() != true)
            {
                return CEFAR.FailingCEFAR(errors.ToArray());
            }
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var products = await context.Products
                    .FilterByCustomKeys(items.SelectMany(x => x.Fields).Where(y => y.Name!.ToLower() == "itemnumber").Select(z => z.Value), true)
                    .ToListAsync()
                    .ConfigureAwait(false);
                foreach (var prd in products)
                {
                    var addedKeywords = items
                        .Where(x => x.Fields.Any(y => y.Value == prd.CustomKey))
                        .SelectMany(z => z.Fields)
                        .Where(v => v.Name!.ToLower() == "seokeywords")
                        .FirstOrDefault()
                        .Value;
                    if (Contract.CheckValidKey(prd.SeoKeywords))
                    {
                        prd.SeoKeywords += $",{addedKeywords}";
                    }
                    else
                    {
                        prd.SeoKeywords = addedKeywords;
                    }
                    context.Products.Add(prd);
                    context.Entry(prd).State = EntityState.Modified;
                }
                try
                {
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    var msg = $"Error while updating keywords : {e.Message}";
                    await Logger.LogErrorAsync("ProductKewordImporter.Error", msg, e, contextProfileName).ConfigureAwait(false);
                    errors.Add(msg);
                }
            }
            catch (Exception ex)
            {
                var msg = $"Error while parsing spreadsheet : {ex.Message}";
                await Logger.LogErrorAsync("ProductImport.Error", msg, ex, contextProfileName).ConfigureAwait(false);
                errors.Add(msg);
            }
            return CEFAR.FailingCEFAR(errors.ToArray());
        }
    }
}
