// <copyright file="ProductPricePointCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price point workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class ProductPricePointWorkflow
    {
        /// <inheritdoc/>
        public override Task<IProductPricePointModel?> GetAsync(string key, string? contextProfileName)
        {
            return GetAsync(TranslateKeyToSearchModel(key, true), contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<int?> CheckExistsAsync(string key, string? contextProfileName)
        {
            return CheckExistsAsync(TranslateKeyToSearchModel(key, true), contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<int?> CheckExistsAsync(IProductPricePointSearchModel search, string? contextProfileName)
        {
            return (await GetAsync(search, contextProfileName).ConfigureAwait(false))?.ID;
        }

        /// <inheritdoc/>
        public Task<IProductPricePointModel?> GetAsync(IProductPricePointSearchModel search, string? contextProfileName)
        {
            // This is a complicated lookup, so validate the parameters first
            Contract.RequiresValidKey(search.MasterKey, "Must provide a Product/Master Key to match against");
            Contract.RequiresValidKey(search.SlaveKey, "Must provide a Price Point/Slave Key to match against");
            Contract.RequiresValidKey(search.UnitOfMeasure, "Must provide a Unit of Measure to match against");
            Contract.Requires<ArgumentOutOfRangeException>(
                search.MinQuantity is null or >= 0,
                "If set, MinQuantity must be greater than or equal to 0");
            Contract.Requires<ArgumentOutOfRangeException>(
                search.MaxQuantity is null or >= 0,
                "If set, MaxQuantity must be greater than or equal to 0");
            Contract.Requires<ArgumentOutOfRangeException>(
                !search.MinQuantity.HasValue && !search.MaxQuantity.HasValue || search.MaxQuantity >= search.MinQuantity,
                "If set, MaxQuantity must be greater than or equal to MinQuantity");
            Contract.Requires<ArgumentOutOfRangeException>(
                !search.From.HasValue || search.From > DateTime.MinValue,
                "If set, From must be greater than DateTime.MinValue");
            Contract.Requires<ArgumentOutOfRangeException>(
                !search.To.HasValue || search.To < DateTime.MaxValue,
                "If set, To must be less than DateTime.MaxValue");
            Contract.Requires<ArgumentOutOfRangeException>(
                !search.From.HasValue || !search.To.HasValue || search.To >= search.From,
                "If both set, To must be greater than From");
            // Passed!
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.ProductPricePoints
                    .AsNoTracking()
                    .FilterByActive(search.Active)
                    .FilterIAmARelationshipTableByMasterKey<ProductPricePoint, Product, PricePoint>(search.MasterKey)
                    .FilterIAmARelationshipTableBySlaveKey<ProductPricePoint, Product, PricePoint>(search.SlaveKey)
                    .FilterProductPricePointsByStoreKey(search.StoreKey)
                    .FilterProductPricePointsByCurrencyKey(search.CurrencyKey)
                    .FilterProductPricePointsByUnitOfMeasure(search.UnitOfMeasure)
                    .FilterProductPricePointsByMinQuantity(search.MinQuantity)
                    .FilterProductPricePointsByMaxQuantity(search.MaxQuantity)
                    .FilterProductPricePointsByFrom(search.From)
                    .FilterProductPricePointsByTo(search.To)
                    .SelectFirstFullProductPricePointAndMapToProductPricePointModel(contextProfileName));
        }

        /// <inheritdoc/>
        public override Task<(List<IProductPricePointModel> results, int totalPages, int totalCount)> SearchAsync(
            IProductPricePointSearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.ProductPricePoints
                .AsNoTracking()
                .FilterByBaseSearchModel(search)
                // Price Point
                .FilterProductPricePointsByPricePointIDs(search.PricePointIDs)
                .FilterIAmARelationshipTableBySlaveID<ProductPricePoint, Product, PricePoint>(search.SlaveID)
                .FilterIAmARelationshipTableBySlaveKey<ProductPricePoint, Product, PricePoint>(search.SlaveKey)
                .FilterIAmARelationshipTableBySlaveName<ProductPricePoint, Product, PricePoint>(search.SlaveName)
                // Product
                .FilterProductPricePointsByProductID(search.ProductID)
                .FilterProductPricePointsByProductKey(search.ProductKey)
                .FilterProductPricePointsByProductName(search.ProductName)
                // Store
                .FilterProductPricePointsByStoreID(search.StoreID)
                .FilterProductPricePointsByStoreKey(search.StoreKey)
                .FilterProductPricePointsByStoreName(search.StoreName)
                // Account
                .FilterProductPricePointsByAccountID(search.AccountID)
                .FilterProductPricePointsByAccountKey(search.AccountKey)
                .FilterProductPricePointsByAccountName(search.AccountName)
                // Currency
                .FilterProductPricePointsByCurrencyID(search.CurrencyID)
                .FilterProductPricePointsByCurrencyKey(search.CurrencyKey)
                .FilterProductPricePointsByCurrencyName(search.CurrencyName)
                // Other
                .FilterProductPricePointsByUnitOfMeasure(search.UnitOfMeasure)
                .FilterProductPricePointsByMinQuantity(search.MinQuantity)
                .FilterProductPricePointsByMaxQuantity(search.MaxQuantity)
                .FilterProductPricePointsByFrom(search.From)
                .FilterProductPricePointsByTo(search.To)
                // Default
                .FilterByModifiedSince(search.ModifiedSince);
            var (models, totalPages, totalCount) = asListing
                ? query.SelectListProductPricePointAndMapToProductPricePointModel(search.Paging, search.Sorts, search.Groupings, contextProfileName)
                : query.SelectLiteProductPricePointAndMapToProductPricePointModel(search.Paging, search.Sorts, search.Groupings, contextProfileName);
            return Task.FromResult((models.ToList(), totalPages, totalCount));
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<ProductPricePoint?>> CreateEntityWithoutSavingAsync(
            IProductPricePointModel model,
            DateTime? timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresInvalidID(Contract.RequiresNotNull(model).ID);
            timestamp = timestamp.GetValueOrDefault(DateExtensions.GenDateTime);
            var (product, store, pricePoint, currency) = await ValidateIncomingModelRequirementsAsync(
                    model,
                    context)
                .ConfigureAwait(false);
            var entity = RegistryLoaderWrapper.GetInstance<ProductPricePoint>(context.ContextProfileName);
            // Base Properties
            entity.Active = true;
            entity.CustomKey = $"{product.CustomKey}|{pricePoint.CustomKey}|{currency?.CustomKey}|{model.UnitOfMeasure}"
                + $"|{model.MinQuantity}|{model.MaxQuantity}|{model.From:O}|{model.To:O}|{store?.CustomKey}";
            entity.CreatedDate = timestamp.Value;
            entity.JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary();
            entity.Hash = model.Hash;
            entity.SlaveID = pricePoint.ID;
            entity.MasterID = product.ID;
            entity.StoreID = store?.ID;
            entity.UnitOfMeasure = model.UnitOfMeasure;
            entity.MaxQuantity = model.MaxQuantity;
            entity.MinQuantity = model.MinQuantity;
            entity.From = model.From;
            entity.To = model.To;
            entity.PercentDiscount = model.PercentDiscount;
            entity.Price = model.Price;
            entity.PriceRoundingID = model.PriceRoundingID;
            await AssignAdditionalPropertiesAsync(entity, model, timestamp.Value, context).ConfigureAwait(false);
            return entity.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> CreateAsync(
            IProductPricePointModel model,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(model);
            Contract.RequiresInvalidID(model.ID);
            if (!OverrideDuplicateCheck)
            {
                await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            }
            await ValidateIncomingModelRequirementsAsync(model, context).ConfigureAwait(false);
            ////// Check for existing entities (do not allow duplicates)
            ////var searchModel = new ProductPricePointSearchModel
            ////{
            ////    PricePointID = pricePoint.ID,
            ////    PricePointKey = pricePoint.CustomKey,
            ////    ProductID = product.ID,
            ////    ProductKey = product.CustomKey,
            ////    StoreID = store?.ID,
            ////    StoreKey = store?.CustomKey,
            ////    UnitOfMeasure = model.UnitOfMeasure,
            ////    MaxQuantity = model.MaxQuantity,
            ////    MinQuantity = model.MinQuantity,
            ////    From = model.From,
            ////    To = model.To
            ////};
            ////// ReSharper disable UnusedVariable
            ////var existingRecords = Search(searchModel, false, out var totalPages, out var totalCount);
            ////// ReSharper restore UnusedVariable
            ////if (existingRecords.Any())
            ////{
            ////    // ReSharper disable once PossibleInvalidOperationException
            ////    throw new InvalidOperationException("Another record with matching data points was found. Cannot perform this operation.");
            ////}
            // Add the new entity
            var createResponse = await CreateEntityWithoutSavingAsync(model, null, context).ConfigureAwait(false);
            if (!createResponse.ActionSucceeded)
            {
                return createResponse.ChangeFailingCEFARType<int>();
            }
            context.ProductPricePoints.Add(createResponse.Result!);
            var saveWorked = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new InvalidDataException(
                    $"Something about creating '{model.GetType().Name}' and saving it failed");
            }
            return createResponse.Result!.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> UpdateAsync(
            IProductPricePointModel model,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresValidID(Contract.RequiresNotNull(model).ID);
            var (product, store, pricePoint, currency) = await ValidateIncomingModelRequirementsAsync(model, context).ConfigureAwait(false);
            // Locate Entity
            var entity = await context.ProductPricePoints
                .FilterByID(model.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false)
                ?? (!Contract.CheckValidKey(model.CustomKey)
                    ? null
                    : await context.ProductPricePoints
                        .FilterByCustomKey(model.CustomKey, true)
                        .OrderByDescending(x => x.Active)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false));
            // ReSharper disable once PossibleInvalidOperationException
            if (entity == null)
            {
                throw new ArgumentException($"No Product Price Point with ID {model.ID} was found");
            }
            if (entity.CustomKey != model.CustomKey)
            {
                await DuplicateCheckAsync(model, context).ConfigureAwait(false); // This will throw if it finds another entity with this model's key
            }
            //// Validate Input against Entity
            //// ReSharper disable once SuspiciousTypeConversion.Global
            ////if (model.Equals(entity))
            ////{
            ////    // No changes to make, don't update, return original via Get
            ////    return Get(entity.ID);
            ////}
            var timestamp = DateExtensions.GenDateTime;
            // ReSharper disable PossibleInvalidOperationException
            entity.SlaveID = pricePoint.ID;
            entity.MasterID = product.ID;
            entity.StoreID = store?.ID;
            // ReSharper restore PossibleInvalidOperationException
            entity.CustomKey = $"{product.CustomKey}|{pricePoint.CustomKey}|{currency?.CustomKey}|{model.UnitOfMeasure}|{model.MinQuantity}|{model.MaxQuantity}|{model.From:O}|{model.To:O}|{store?.CustomKey}";
            entity.UnitOfMeasure = model.UnitOfMeasure;
            entity.MaxQuantity = model.MaxQuantity;
            entity.MinQuantity = model.MinQuantity;
            entity.From = model.From;
            entity.To = model.To;
            entity.PercentDiscount = model.PercentDiscount;
            entity.Price = model.Price;
            entity.PriceRoundingID = model.PriceRoundingID;
            entity.Active = model.Active;
            entity.Hash = model.Hash;
            entity.UpdatedDate = timestamp;
            var saveWorked = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new InvalidDataException("Something about updating this object and saving it failed");
            }
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <summary>Translate key to search model.</summary>
        /// <param name="key">   The key to get.</param>
        /// <param name="active">The active.</param>
        /// <returns>An IProductPricePointSearchModel.</returns>
        private static IProductPricePointSearchModel TranslateKeyToSearchModel(string key, bool? active)
        {
            Contract.RequiresValidKey(key);
            Contract.Requires<InvalidOperationException>(key.Contains("|"));
            var keys = key.Split('|');
            if (keys.Length < 9)
            {
                throw new InvalidOperationException("Incorrect Split values for key to properties");
            }
            var haveMin = decimal.TryParse(keys[4], out var min);
            var haveMax = decimal.TryParse(keys[5], out var max);
            var haveFrom = DateTime.TryParse(keys[6], out var from);
            var haveTo = DateTime.TryParse(keys[7], out var to);
            return new ProductPricePointSearchModel
            {
                MasterKey = keys[0],
                SlaveKey = keys[1],
                CurrencyKey = keys[2],
                UnitOfMeasure = keys[3],
                MinQuantity = haveMin ? min : null,
                MaxQuantity = haveMax ? max : null,
                From = haveFrom ? from : null,
                To = haveTo ? to : null,
                StoreKey = keys[8],
                Active = active,
            };
        }

        /// <summary>Validates the incoming model requirements.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task{(IProductModel product,IStoreModel store,IPricePointModel pricePoint,ICurrencyModel currency)}.</returns>
        private async Task<(IProductModel product, IStoreModel? store, IPricePointModel pricePoint, ICurrencyModel? currency)> ValidateIncomingModelRequirementsAsync(
            IProductPricePointModel model,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<ArgumentException>(
                model.MasterID > 0 && model.MasterID != int.MaxValue
                || !string.IsNullOrWhiteSpace(model.MasterKey),
                "ProductID or ProductKey must have a value relating to a Product");
            Contract.Requires<ArgumentException>(
                model.SlaveID > 0 && model.SlaveID != int.MaxValue
                || !string.IsNullOrWhiteSpace(model.SlaveKey),
                "PricePointID or PricePointKey must have a value relating to a PricePoint");
            // Product
            var productID = model.MasterID > 0
                ? model.MasterID
                : await Workflows.Products.CheckExistsAsync(model.MasterKey!, context).ConfigureAwait(false) ?? 0;
            ////product = (model.ProductID.HasValue ? ProductWorkflow.Get(model.ProductID.Value, new List<int>(), "WEB", 1) : null)
            ////    ?? ProductWorkflow.Get(model.ProductKey, new List<int>(), "WEB", 1);
            if (productID <= 0)
            {
                throw new InvalidOperationException(
                    $"{nameof(model.MasterID)} or {nameof(model.MasterKey)}"
                    + " must have a value relating to an existing Product (the Product Record must be Created first)");
            }
            var product = await Workflows.Products.GetAsync(
                    id: productID,
                    context: context,
                    isVendorAdmin: false,
                    vendorAdminID: null,
                    previewID: null)
                .ConfigureAwait(false);
            if (product == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(model.MasterID)} or {nameof(model.MasterKey)}"
                    + " must have a value relating to an existing Product (the Product Record must be Created first)");
            }
            model.MasterID = productID;
            // Price Point
            // TODO@BE: Replace below with PricePointWorkflow.ResolveWithAutoGenerate
            var pricePoint = (Contract.CheckValidID(model.SlaveID)
                    ? await Workflows.PricePoints.GetAsync(model.SlaveID, context).ConfigureAwait(false)
                    : null)
                ?? await Workflows.PricePoints.GetAsync(model.SlaveKey!, context).ConfigureAwait(false);
            if (pricePoint == null && !string.IsNullOrEmpty(model.SlaveKey))
            {
                var createResponse = await Workflows.PricePoints.CreateAsync(
                        new PricePointModel { Active = true, CustomKey = model.SlaveKey, Name = model.SlaveKey, },
                        context)
                    .ConfigureAwait(false);
                pricePoint = (await GetAsync(createResponse.ThrowIfFailed().Result, context).ConfigureAwait(false))!.Slave;
            }
            if (pricePoint?.ID == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(model.SlaveID)} or {nameof(model.SlaveKey)} "
                    + "must have a value relating to a Price Point (the Price Point Record must be Created first)");
            }
            model.SlaveID = pricePoint.ID;
            // Currency
            ICurrencyModel? currency;
            if (!Contract.CheckValidIDOrAnyValidKey(model.CurrencyID, model.CurrencyKey, model.CurrencyName))
            {
                currency = null;
            }
            else
            {
                currency = (Contract.CheckValidID(model.CurrencyID)
                        ? await Workflows.Currencies.GetAsync(model.CurrencyID!.Value, context).ConfigureAwait(false)
                        : null)
                    ?? await Workflows.Currencies.GetAsync(model.CurrencyKey!, context).ConfigureAwait(false);
                if (currency?.ID == null)
                {
                    throw new InvalidOperationException(
                        $"{nameof(model.CurrencyID)}, {nameof(model.CurrencyKey)} or {nameof(model.CurrencyName)} "
                        + "must have a value relating to a Currency if set (the Currency Record must be Created first)");
                }
                model.CurrencyID = currency.ID;
            }
            // Store
            IStoreModel? store;
            if (!Contract.CheckValidIDOrAnyValidKey(model.StoreID, model.StoreKey, model.StoreName))
            {
                store = null;
            }
            else
            {
                store = (model.StoreID > 0 ? await Workflows.Stores.GetAsync(model.StoreID.Value, context).ConfigureAwait(false) : null)
                    ?? await Workflows.Stores.GetAsync(model.StoreKey!, context).ConfigureAwait(false);
                if (store?.ID == null)
                {
                    throw new InvalidOperationException(
                        $"{nameof(model.StoreID)}, {nameof(model.StoreKey)} or {nameof(model.StoreName)} "
                        + "must have a value relating to a Store if set (the Store Record must be Created first)");
                }
                model.StoreID = store.ID;
            }
            return (product, store, pricePoint, currency)!;
        }
    }
}
