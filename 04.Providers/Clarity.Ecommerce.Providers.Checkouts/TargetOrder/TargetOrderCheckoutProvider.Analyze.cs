// <copyright file="TargetOrderCheckoutProvider.Analyze.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout provider class</summary>
// ReSharper disable ArrangeObjectCreationWhenTypeEvident, InconsistentNaming
namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class TargetOrderCheckoutProvider
    {
        /// <summary>(Immutable) The true values.</summary>
        protected static readonly string[] TrueValues
            = { "TRUE", "True", "true", "1", "Y", "y", "YES", "Yes", "yes" };

        /// <summary>(Immutable) Dictionary of category split order required.</summary>
        protected static readonly Dictionary<string, string?[]?>? CategorySplitOrderRequiredDict
            = new() { ["Category-Split-Order-Required"] = TrueValues };

        /// <summary>(Immutable) Dictionary of store split order required.</summary>
        protected static readonly Dictionary<string, string?[]?>? StoreSplitOrderRequiredDict
            = new() { ["Store-Split-Order-Required"] = TrueValues };

        /// <summary>(Immutable) Dictionary of brand split order required.</summary>
        protected static readonly Dictionary<string, string?[]?>? BrandSplitOrderRequiredDict
            = new() { ["Brand-Split-Order-Required"] = TrueValues };

        /// <summary>(Immutable) Dictionary of manufacturer split order required.</summary>
        protected static readonly Dictionary<string, string?[]?>? ManufacturerSplitOrderRequiredDict
            = new() { ["Manufacturer-Split-Order-Required"] = TrueValues };

        /// <summary>(Immutable) Dictionary of vendor split order required.</summary>
        protected static readonly Dictionary<string, string?[]?>? VendorSplitOrderRequiredDict
            = new() { ["Vendor-Split-Order-Required"] = TrueValues };

        /// <summary>Gets or sets the cart status identifier for New.</summary>
        /// <value>The cart status identifier new.</value>
        protected static int CartStatusIDNew { get; set; }

        /// <summary>Gets or sets the cart state identifier for Work.</summary>
        /// <value>The cart state identifier work.</value>
        protected static int CartStateIDWork { get; set; }

        /// <summary>Gets or sets the identifier of the ship to home target type.</summary>
        /// <value>The identifier of the ship to home target type.</value>
        protected static int ShipToHomeTargetTypeID { get; set; }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            _ = Contract.RequiresNotNull(checkout);
            // Load the original cart and do quick validation on it
            var originalCartResponse = await TryResolveCartAsync(
                    checkout: checkout,
                    pricingFactoryContext: pricingFactoryContext,
                    lookupKey: lookupKey,
                    taxesProvider: taxesProvider,
                    validate: true,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!originalCartResponse.ActionSucceeded
                || originalCartResponse.Messages.Any(x => x.StartsWith("ERROR")))
            {
                return new(
                    new() { originalCartResponse.Result! },
                    false,
                    originalCartResponse.Messages.ToArray());
            }
            return await AnalyzeInnerAsync(
                    checkout: checkout,
                    originalCart: originalCartResponse.Result!,
                    lookupKey: lookupKey,
                    asAdmin: false,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            _ = Contract.RequiresNotNull(checkout);
            // Load the original cart and do quick validation on it
            var cart = await Workflows.Carts.AdminGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false)
                ?? throw new ArgumentException("ERROR! Must supply lookup information which matches an existing record.");
            return await AnalyzeInnerAsync(
                    checkout: checkout,
                    // ReSharper disable once StyleCop.SA1118
                    originalCart: cart,
                    lookupKey: lookupKey.ToSessionLookupKey(cart.TypeKey!, cart.SessionID!.Value),
                    asAdmin: true,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            _ = Contract.RequiresNotNull(checkout);
            // Load the original cart and do quick validation on it
            var originalCartResponse = await TryResolveCartAsync(
                    checkout: checkout,
                    pricingFactoryContext: pricingFactoryContext,
                    lookupKey: lookupKey,
                    taxesProvider: taxesProvider,
                    validate: true,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!originalCartResponse.ActionSucceeded
                || originalCartResponse.Messages.Any(x => x.StartsWith("ERROR")))
            {
                return CEFAR.FailingCEFAR(originalCartResponse.Messages.ToArray());
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var targetCartIDs = await context.Carts
                .AsNoTracking()
                .FilterCartsByLookupKey(
                    lookupKey: new(
                        typeKey: "Target-Grouping-",
                        sessionID: originalCartResponse.Result!.SessionID!.Value,
                        userID: lookupKey.UserID,
                        accountID: lookupKey.AccountID,
                        brandID: lookupKey.BrandID,
                        franchiseID: lookupKey.FranchiseID,
                        storeID: lookupKey.StoreID),
                    useStartsWithPrefix: true)
                .Select(x => x.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            return await CEFAR.AggregateAsync(
                    targetCartIDs,
                    id => Workflows.Carts.DeleteAsync(id, contextProfileName))
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            CartByIDLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            _ = Contract.RequiresNotNull(checkout);
            // Load the original cart and do quick validation on it
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var sessionID = await context.Carts
                .AsNoTracking()
                .FilterCartsByLookupKey(lookupKey)
                .Select(x => x.SessionID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (sessionID is null || sessionID == default(Guid))
            {
                return CEFAR.FailingCEFAR("No session to read for");
            }
            var targetCartIDs = await context.Carts
                .AsNoTracking()
                .FilterCartsByLookupKey(
                    lookupKey: new(
                        typeKey: "Target-Grouping-",
                        sessionID: sessionID.Value,
                        userID: lookupKey.UserID,
                        accountID: lookupKey.AccountID,
                        brandID: lookupKey.BrandID,
                        franchiseID: lookupKey.FranchiseID,
                        storeID: lookupKey.StoreID),
                    useStartsWithPrefix: true)
                .Select(x => x.ID)
                .ToListAsync()
                .ConfigureAwait(false);
            return await CEFAR.AggregateAsync(
                    targetCartIDs,
                    id => Workflows.Carts.DeleteAsync(id, contextProfileName))
                .ConfigureAwait(false);
        }

        /// <summary>Updates the billing contact for cart.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected static Task<CEFActionResponse> UpdateBillingContactForCartAsync(
            ICartModel cart,
            string? contextProfileName)
        {
            return Workflows.Carts.SetBillingContactAsync(
                cart.ID,
                cart.BillingContact,
                contextProfileName);
        }

        /// <summary>Analyze inner.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="originalCart">         The original cart.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="asAdmin">              True to as admin.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A list of carts.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity, FunctionComplexityOverflow
        protected virtual async Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeInnerAsync(
            ICheckoutModel checkout,
            ICartModel originalCart,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            bool asAdmin,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            // Step 01. Create a timestamp for all of our actions to make it easier to identify database record
            // updates together by time-frame)
            var timestamp = DateExtensions.GenDateTime;
            // Step 02. Verify the cart object we loaded wasn't null, fail out if it was
            var nullCheckResult = DoCartIsNullCheck(originalCart);
            if (!nullCheckResult.ActionSucceeded)
            {
                return nullCheckResult.ChangeFailingCEFARType<List<ICartModel?>?>();
            }
            // Step 03. Verify the cart object we loaded had items in it, fail out if it didn't
            var emptyCartCheckResult = DoCartEmptyCheck(originalCart);
            if (!emptyCartCheckResult.ActionSucceeded)
            {
                return emptyCartCheckResult.ChangeFailingCEFARType<List<ICartModel?>?>();
            }
            // Step 04. Verify the cart has a session ID, fail out if it doesn't
            if (!originalCart.SessionID.HasValue)
            {
                return CEFAR.FailingCEFAR<List<ICartModel?>?>("No Session ID");
            }
            _ = Contract.RequiresNotNull(originalCart.SessionID, "Original Cart has a null SessionID, cannot continue analyzer");
            var originalDiscountIDs = originalCart.Discounts?.Select(x => x.SlaveID).ToList() ?? new List<int>();
            originalDiscountIDs.AddRange(
                originalCart.SalesItems!
                    .SelectMany(x => x.Discounts?.Select(y => y.SlaveID).ToList() ?? new List<int>()));
            originalDiscountIDs = originalDiscountIDs.Distinct().ToList();
            // Apply the billing contact information from the incoming Checkout request to the original cart. First,
            // by checking for and applying via the ID if present, otherwise by a model passed, if present.
            async Task ReloadOriginalCartAsync(bool validate, int? currentAccountID)
            {
                var lookupKey = SessionCartBySessionAndTypeLookupKey.FromCart(originalCart);
                lookupKey.AltAccountID = currentAccountID;
                originalCart = (await TryResolveCartAsync(
                            checkout: checkout,
                            pricingFactoryContext: pricingFactoryContext,
                            lookupKey: lookupKey,
                            taxesProvider: taxesProvider,
                            validate: validate,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .Result!;
                _ = Contract.RequiresAllValidIDs(originalCart.SalesItems!.Select(x => x.ID).ToArray());
                _ = Contract.RequiresAllValidIDs(originalCart.SalesItems!.SelectMany(x => x.Targets!.Select(y => y.ID)).ToArray());
            }
            async Task<CEFActionResponse<List<ICartModel>?>> UpdateBillingAsync()
            {
                var result = await UpdateBillingContactForCartAsync(
                        originalCart,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (!result.ActionSucceeded)
                {
                    // Updating resulted in some kind of error, fail out
                    return result.ChangeFailingCEFARType<List<ICartModel>?>();
                }
                // Since we made an update, pull the cart again
                await ReloadOriginalCartAsync(false, lookupKey.AltAccountID).ConfigureAwait(false);
                return CEFAR.PassingCEFAR<List<ICartModel>?>(null);
            }
            if (originalCart.BillingContact == null && Contract.CheckValidID(originalCart.BillingContactID))
            {
                var result = await UpdateBillingAsync().ConfigureAwait(false);
                if (!result.ActionSucceeded)
                {
                    return result.ChangeFailingCEFARType<List<ICartModel?>?>();
                }
            }
            // NOTE: Not an "else if" because the above may have resulted in a null
            if (originalCart.BillingContact == null && checkout.Billing != null)
            {
                originalCart.BillingContact = checkout.Billing;
                var result = await UpdateBillingAsync().ConfigureAwait(false);
                if (!result.ActionSucceeded)
                {
                    return result.ChangeFailingCEFARType<List<ICartModel?>?>();
                }
            }
            // Step 05. Determine the default billing contact ID for this account. "Default" will start with the entry
            // specified at checkout, and look to the address book if that wasn't present
            //// int? accountID = null;
            var defaultBillingContactID = originalCart.BillingContactID ?? 0;
            if (!Contract.CheckValidID(defaultBillingContactID) && Contract.CheckValidID(lookupKey.UserID))
            {
                // Try resolve to account's default billing
                if (Contract.CheckValidID(lookupKey.AccountID))
                {
                    var billing = await Workflows.AddressBooks.GetAddressBookPrimaryBillingAsync(
                            lookupKey.AccountID!.Value,
                            contextProfileName)
                        .ConfigureAwait(false);
                    defaultBillingContactID = billing?.SlaveID ?? 0;
                }
            }
            //// // Step 06. Determine the default shipping contact ID for this account. "Default" will start with the entry
            //// // specified at checkout, and look to the address book if that wasn't present
            //// var defaultShippingContactID = originalCart.ShippingContactID;
            //// if (!Contract.CheckValidID(defaultShippingContactID) && Contract.CheckValidID(lookupKey.UserID))
            //// {
            ////     // Try resolve to account's default shipping
            ////     accountID ??= await Workflows.Accounts.GetIDByUserIDAsync(
            ////             lookupKey.UserID.Value,
            ////             contextProfileName)
            ////         .ConfigureAwait(false);
            ////     if (Contract.CheckValidID(accountID))
            ////     {
            ////         var shipping = await Workflows.AddressBooks.GetAddressBookPrimaryShippingAsync(
            ////                 accountID.Value,
            ////                 contextProfileName)
            ////             .ConfigureAwait(false);
            ////         defaultShippingContactID = shipping?.SlaveID;
            ////     }
            //// }
            // Step 07. Process through cart items to assign default targets, or clean up existing targets, as needed
            // Step 07.a. Create some memory caches to maintain repeatable information and reduce database hits for
            // performance
            var productIDLookup = new Dictionary<(int?, string?, string?), int?>();
            var productToPILSLookup = new Dictionary<int, int?>();
            var productToStoreLookup = new Dictionary<(int?, int), int?>();
            var productToBrandLookup = new Dictionary<(int?, int), int?>();
            var productToVendorLookup = new Dictionary<int, int?>();
            var productsThatRequireSplitOrderByType = new Dictionary<int, bool>();
            var loadedProducts = new Dictionary<int, (string key, string name, string? desc, string? seoUrl, bool nothingToShip)>();
            var loadedContacts = new Dictionary<int, IContactModel>();
            var loadedStoreProducts = new Dictionary<int, IStoreProductModel>();
            var loadedBrandProducts = new Dictionary<int, IBrandProductModel>();
            var loadedVendorProducts = new Dictionary<int, IVendorProductModel>();
            var loadedTargetTypes = new Dictionary<int, ITypeModel>();
            var loadedPILS = new Dictionary<int, IProductInventoryLocationSectionModel>();
            if (Contract.CheckInvalidID(ShipToHomeTargetTypeID))
            {
                ShipToHomeTargetTypeID = await Workflows.SalesItemTargetTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "ShipToHome",
                        byName: "ShipToHome",
                        byDisplayName: null,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            var targetGroupings = new Dictionary<string, (int? destID, List<ISalesItemBaseModel> salesItems)>();
            BaseModelMapper.Initialize();
            async Task LoadContactForTargetAsync(IClarityEcommerceEntities context, ISalesItemTargetBaseModel target)
            {
                if (!loadedContacts.ContainsKey(target.DestinationContactID))
                {
                    try
                    {
                        loadedContacts[target.DestinationContactID] = context.Contacts
                            ////.AsNoTracking() // This is causing an issue
                            .FilterByActive(true)
                            .FilterByID(target.DestinationContactID)
                            .SelectFirstFullContactAndMapToContactModel(contextProfileName)!;
                    }
                    catch (Exception ex1)
                    {
                        await Logger.LogErrorAsync(
                                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex1.GetType().Name}",
                                message: $"Failed to get destination contact for Target {target.ID} Seeking: '{target.DestinationContactID}'",
                                ex: ex1,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
                target.DestinationContact = loadedContacts[target.DestinationContactID];
            }
            Task LoadStoreProductForTargetAsync(IClarityEcommerceEntities context, ISalesItemTargetBaseModel target)
            {
                ////if (!loadedStoreProducts.ContainsKey(Contract.RequiresValidID(target.OriginStoreProductID)))
                ////{
                ////    try
                ////    {
                ////        loadedStoreProducts[target.OriginStoreProductID!.Value] = context.StoreProducts
                ////            ////.AsNoTracking() // This is causing an issue
                ////            .FilterByActive(true)
                ////            .FilterByID(target.OriginStoreProductID)
                ////            .Where(x => x.Master!.Active)
                ////            .SelectFirstFullStoreProductAndMapToStoreProductModel(contextProfileName)!;
                ////    }
                ////    catch (Exception ex2)
                ////    {
                ////        await Logger.LogErrorAsync(
                ////                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex2.GetType().Name}",
                ////                message: $"Failed to get store product for Target {target.ID} Seeking: '{target.OriginStoreProductID}'",
                ////                ex: ex2,
                ////                contextProfileName: contextProfileName)
                ////            .ConfigureAwait(false);
                ////        throw;
                ////    }
                ////}
                ////target.OriginStoreProduct = loadedStoreProducts[target.OriginStoreProductID!.Value];
                return Task.CompletedTask;
            }
            async Task LoadBrandProductForTargetAsync(IClarityEcommerceEntities context, ISalesItemTargetBaseModel target)
            {
                if (!loadedBrandProducts.ContainsKey(Contract.RequiresValidID(target.BrandProductID)))
                {
                    try
                    {
                        loadedBrandProducts[target.BrandProductID!.Value] = context.BrandProducts
                            ////.AsNoTracking() // This is causing an issue
                            .FilterByActive(true)
                            .FilterByID(target.BrandProductID)
                            .Where(x => x.Master!.Active)
                            .SelectFirstFullBrandProductAndMapToBrandProductModel(contextProfileName)!;
                    }
                    catch (Exception ex2)
                    {
                        await Logger.LogErrorAsync(
                                $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex2.GetType().Name}",
                                $"Failed to get brand product for Target {target.ID} Seeking: '{target.BrandProductID}'",
                                ex2,
                                contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
                target.BrandProduct = loadedBrandProducts[target.BrandProductID!.Value];
            }
            Task LoadVendorProductForTargetAsync(IClarityEcommerceEntities context, ISalesItemTargetBaseModel target)
            {
                ////if (!loadedVendorProducts.ContainsKey(Contract.RequiresValidID(target.OriginVendorProductID)))
                ////{
                ////    try
                ////    {
                ////        loadedVendorProducts[target.OriginVendorProductID!.Value] = context.VendorProducts
                ////            ////.AsNoTracking() // This is causing an issue
                ////            .FilterByActive(true)
                ////            .FilterByID(target.OriginVendorProductID)
                ////            .Where(x => x.Master!.Active)
                ////            .SelectFirstFullVendorProductAndMapToVendorProductModel(contextProfileName)!;
                ////    }
                ////    catch (Exception ex3)
                ////    {
                ////        await Logger.LogErrorAsync(
                ////                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex3.GetType().Name}",
                ////                message: $"Failed to get vendor product for Target {target.ID} Seeking: '{target.OriginVendorProductID}'",
                ////                ex: ex3,
                ////                contextProfileName: contextProfileName)
                ////            .ConfigureAwait(false);
                ////        throw;
                ////    }
                ////}
                ////target.OriginVendorProduct = loadedVendorProducts[target.OriginVendorProductID!.Value];
                return Task.CompletedTask;
            }
            Task LoadPILSForTargetAsync(IClarityEcommerceEntities context, ISalesItemTargetBaseModel target)
            {
                ////if (!loadedPILS.ContainsKey(Contract.RequiresValidID(target.OriginProductInventoryLocationSectionID)))
                ////{
                ////    try
                ////    {
                ////        var originPILS = context.ProductInventoryLocationSections
                ////            ////.AsNoTracking()
                ////            .FilterByActive(true)
                ////            .FilterByID(target.OriginProductInventoryLocationSectionID)
                ////            .Where(x => x.Slave!.Active && x.Slave.InventoryLocation!.Active)
                ////            .SelectFirstFullProductInventoryLocationSectionAndMapToProductInventoryLocationSectionModel(contextProfileName);
                ////        if (CEFConfigDictionary.GetClosestWarehouseWithStock && originPILS!.Quantity <= 0)
                ////        {
                ////            var regionID = Contract.RequiresValidID(target.DestinationContact?.Address?.RegionID);
                ////            var code = await context.Regions
                ////                .AsNoTracking()
                ////                .FilterByID(regionID)
                ////                .Select(x => x.Code!)
                ////                .SingleAsync()
                ////                .ConfigureAwait(false);
                ////            var newPILS = Workflows.ProductInventoryLocationSections.GetClosestWarehouseByRegionCode(
                ////                code,
                ////                originPILS.MasterID,
                ////                contextProfileName);
                ////            if (newPILS != null)
                ////            {
                ////                loadedPILS[target.OriginProductInventoryLocationSectionID!.Value] = newPILS;
                ////            }
                ////            else
                ////            {
                ////                loadedPILS[target.OriginProductInventoryLocationSectionID!.Value] = originPILS;
                ////            }
                ////        }
                ////        else
                ////        {
                ////            loadedPILS[target.OriginProductInventoryLocationSectionID!.Value] = originPILS!;
                ////        }
                ////    }
                ////    catch (Exception ex4)
                ////    {
                ////        await Logger.LogErrorAsync(
                ////                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex4.GetType().Name}",
                ////                message: $"Failed to get PILS for Target {target.ID} Seeking: '{target.OriginProductInventoryLocationSectionID}'",
                ////                ex: ex4,
                ////                contextProfileName: contextProfileName)
                ////            .ConfigureAwait(false);
                ////        throw;
                ////    }
                ////}
                ////target.OriginProductInventoryLocationSection = loadedPILS[target.OriginProductInventoryLocationSectionID!.Value];
                return Task.CompletedTask;
            }
            async Task LoadTypeKeyForTargetAsync(IClarityEcommerceEntities context, ISalesItemTargetBaseModel target)
            {
                if (!loadedTargetTypes.ContainsKey(target.TypeID))
                {
                    try
                    {
                        loadedTargetTypes[target.TypeID] = context.SalesItemTargetTypes
                            ////.AsNoTracking() // This is causing an issue
                            .FilterByActive(true)
                            .FilterByID(target.TypeID)
                            .SelectFirstFullSalesItemTargetTypeAndMapToTypeModel(contextProfileName)!;
                    }
                    catch (Exception ex5)
                    {
                        await Logger.LogErrorAsync(
                                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex5.GetType().Name}",
                                message: $"Failed to get Type for Target {target.ID} Seeking: '{target.TypeID}'",
                                ex: ex5,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
                target.Type = loadedTargetTypes[target.TypeID];
                target.TypeKey = target.Type.CustomKey;
            }
            async Task LoadProductForSalesItemAsync(
                IClarityEcommerceEntities context,
                int productID,
                ISalesItemBaseModel salesItem)
            {
                (string key, string name, string? desc, string? seoUrl, bool nothingToShip) product;
                if (loadedProducts.ContainsKey(productID)
                    && loadedProducts.TryGetValue(productID, out var value))
                {
                    product = value;
                }
                else
                {
                    try
                    {
                        var fromDB = await context.Products
                            .AsNoTracking()
                            .FilterByActive(true)
                            .FilterByID(productID)
                            .Select(x => new
                            {
                                x.CustomKey,
                                x.Name,
                                x.Description,
                                x.SeoUrl,
                                x.NothingToShip,
                            }
                            !)
                            .SingleAsync()
                            .ConfigureAwait(false);
                        product.key = fromDB.CustomKey!;
                        product.name = fromDB.Name!;
                        product.desc = fromDB.Description;
                        product.seoUrl = fromDB.SeoUrl;
                        product.nothingToShip = fromDB.NothingToShip;
                        loadedProducts[productID] = product;
                    }
                    catch (Exception ex1)
                    {
                        await Logger.LogErrorAsync(
                                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex1.GetType().Name}",
                                message: $"Failed to get product '{productID}'",
                                ex: ex1,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
                // Assign all the product information so we can use it later
                salesItem.ProductID = productID;
                salesItem.ProductKey = product.key;
                salesItem.ProductName = product.name;
                salesItem.ProductDescription = product.desc;
                salesItem.ProductSeoUrl = product.seoUrl;
                salesItem.ProductNothingToShip = product.nothingToShip;
            }
            Task ValidateAndFallBackStoreIDAndStoreProductIDAsync(
                IClarityEcommerceEntities context,
                int? referringStoreID,
                int productID,
                ISalesItemTargetBaseModel target)
            {
                ////var key = (referringStoreID, productID);
                ////if (productToStoreLookup.ContainsKey(key))
                ////{
                ////    target.OriginStoreProductID = productToStoreLookup[key];
                ////    return;
                ////}
                ////if (!await context.StoreProducts
                ////        .AsNoTracking()
                ////        .FilterByActive(true)
                ////        .FilterStoreProductsByProductID(productID)
                ////        .AnyAsync()
                ////        .ConfigureAwait(false))
                ////{
                ////    // There are no Store assignments for this Product
                ////    target.OriginStoreProductID = productToStoreLookup[key] = null;
                ////    return;
                ////}
                ////if (Contract.CheckValidID(target.OriginStoreProductID)
                ////    && await context.StoreProducts
                ////            .AsNoTracking()
                ////            .FilterByActive(true)
                ////            .FilterByID(target.OriginStoreProductID)
                ////            .AnyAsync()
                ////            .ConfigureAwait(false))
                ////{
                ////    // The one we have is good
                ////    productToStoreLookup[key] = target.OriginStoreProductID;
                ////    return;
                ////}
                ////// It was bad, so for sure don't keep it
                ////target.OriginStoreProductID = null;
                ////if (!Contract.CheckValidID(target.OriginStoreProductID)
                ////    && Contract.CheckValidID(referringStoreID))
                ////{
                ////    // Try to find one that is tied to this store
                ////    var result2 = await context.StoreProducts
                ////        .AsNoTracking()
                ////        .FilterByActive(true)
                ////        .FilterStoreProductsByProductID(productID)
                ////        .FilterStoreProductsByStoreID(referringStoreID)
                ////        .Select(x => new { StoreProductID = x.ID, x.MasterID })
                ////        .FirstOrDefaultAsync()
                ////        .ConfigureAwait(false);
                ////    target.OriginStoreProductID = productToStoreLookup[key] = result2?.StoreProductID;
                ////}
                ////if (Contract.CheckValidID(target.OriginStoreProductID))
                ////{
                ////    productToStoreLookup[key] = target.OriginStoreProductID;
                ////    return;
                ////}
                ////// Fall back to any store if the previous didn't work
                ////var result3 = await context.StoreProducts
                ////    .AsNoTracking()
                ////    .FilterByActive(true)
                ////    .FilterStoreProductsByProductID(productID)
                ////    .Select(x => new { StoreProductID = x.ID, x.MasterID })
                ////    .FirstOrDefaultAsync()
                ////    .ConfigureAwait(false);
                ////target.OriginStoreProductID = productToStoreLookup[key] = result3?.StoreProductID;
                return Task.CompletedTask;
            }
            async Task ValidateAndFallBackBrandIDAndBrandProductIDAsync(
                IClarityEcommerceEntities context,
                int? referringBrandID,
                int productID,
                ISalesItemTargetBaseModel target)
            {
                var key = (referringBrandID, productID);
                if (productToBrandLookup.ContainsKey(key))
                {
                    target.BrandProductID = productToBrandLookup[key];
                    return;
                }
                if (!await context.BrandProducts
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterBrandProductsByProductID(productID)
                        .Where(x => x.Master!.Active)
                        .AnyAsync()
                        .ConfigureAwait(false))
                {
                    // There are no Brand assignments for this Product
                    target.BrandProductID = productToBrandLookup[key] = null;
                    return;
                }
                if (Contract.CheckValidID(target.BrandProductID)
                    && await context.BrandProducts
                            .AsNoTracking()
                            .FilterByActive(true)
                            .FilterByID(target.BrandProductID)
                            .Where(x => x.Master!.Active)
                            .AnyAsync()
                            .ConfigureAwait(false))
                {
                    // The one we have is good
                    productToBrandLookup[key] = target.BrandProductID;
                    return;
                }
                // It was bad, so for sure don't keep it
                target.BrandProductID = null;
                if (!Contract.CheckValidID(target.BrandProductID)
                    && Contract.CheckValidID(referringBrandID))
                {
                    // Try to find one that is tied to this brand
                    var result2 = await context.BrandProducts
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterBrandProductsByProductID(productID)
                        .FilterBrandProductsByBrandID(referringBrandID)
                        .Where(x => x.Master!.Active)
                        .Select(x => new { BrandProductID = x.ID, x.MasterID })
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);
                    target.BrandProductID = productToBrandLookup[key] = result2?.BrandProductID;
                }
                if (Contract.CheckValidID(target.BrandProductID))
                {
                    productToBrandLookup[key] = target.BrandProductID;
                    return;
                }
                // Fall back to any brand if the previous didn't work
                var result3 = await context.BrandProducts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterBrandProductsByProductID(productID)
                    .Where(x => x.Master!.Active)
                    .Select(x => new { BrandProductID = x.ID, x.MasterID })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                target.BrandProductID = productToBrandLookup[key] = result3?.BrandProductID;
            }
            Task ValidateAndFallBackVendorIDAndVendorProductIDAsync(
                IClarityEcommerceEntities context,
                int productID,
                ISalesItemTargetBaseModel targetData)
            {
                ////var key = productID;
                ////if (productToVendorLookup.ContainsKey(key))
                ////{
                ////    targetData.OriginVendorProductID = productToVendorLookup[key];
                ////    return;
                ////}
                ////if (!await context.VendorProducts
                ////        .AsNoTracking()
                ////        .FilterByActive(true)
                ////        .FilterVendorProductsByProductID(productID)
                ////        .Where(x => x.Master!.Active)
                ////        .AnyAsync()
                ////        .ConfigureAwait(false))
                ////{
                ////    // There are no Vendor assignments for this Product
                ////    targetData.OriginVendorProductID = productToVendorLookup[key] = null;
                ////    return;
                ////}
                ////if (Contract.CheckValidID(targetData.OriginVendorProductID)
                ////    && await context.VendorProducts
                ////            .AsNoTracking()
                ////            .FilterByActive(true)
                ////            .FilterByID(targetData.OriginVendorProductID)
                ////            .Where(x => x.Master!.Active)
                ////            .AnyAsync()
                ////            .ConfigureAwait(false))
                ////{
                ////    // The one we have is good
                ////    productToVendorLookup[key] = targetData.OriginVendorProductID;
                ////    return;
                ////}
                ////// It was bad, so for sure don't keep it
                ////targetData.OriginVendorProductID = null;
                ////if (Contract.CheckValidID(targetData.OriginVendorProductID))
                ////{
                ////    productToVendorLookup[key] = targetData.OriginVendorProductID;
                ////    return;
                ////}
                ////// Fall back to any vendor if the previous didn't work
                ////var result3 = await context.VendorProducts
                ////    .AsNoTracking()
                ////    .FilterByActive(true)
                ////    .FilterVendorProductsByProductID(productID)
                ////    .Where(x => x.Master!.Active)
                ////    .Select(x => new { VendorProductID = x.ID, x.MasterID })
                ////    .FirstAsync()
                ////    .ConfigureAwait(false);
                ////targetData.OriginVendorProductID = productToVendorLookup[key] = result3?.VendorProductID;
                return Task.CompletedTask;
            }
            Task ValidateAndFallBackPILSIDAsync(
                IClarityEcommerceEntities context,
                int productID,
                ISalesItemTargetBaseModel target)
            {
                ////if (productToPILSLookup.ContainsKey(productID))
                ////{
                ////    target.OriginProductInventoryLocationSectionID = productToPILSLookup[productID];
                ////    return;
                ////}
                ////if (!CEFConfigDictionary.InventoryAdvancedEnabled)
                ////{
                ////    // PILS is disabled
                ////    target.OriginProductInventoryLocationSectionID = productToPILSLookup[productID] = null;
                ////    return;
                ////}
                ////if (!await context.ProductInventoryLocationSections
                ////        .AsNoTracking()
                ////        .FilterByActive(true)
                ////        .FilterPILSByProductID(productID)
                ////        .Where(x => x.Slave!.Active && x.Slave.InventoryLocation!.Active)
                ////        .AnyAsync()
                ////        .ConfigureAwait(false))
                ////{
                ////    // There are no inventory assignments for this product
                ////    target.OriginProductInventoryLocationSectionID = productToPILSLookup[productID] = null;
                ////    return;
                ////}
                ////if (Contract.CheckValidID(target.OriginProductInventoryLocationSectionID)
                ////    && await context.ProductInventoryLocationSections
                ////            .AsNoTracking()
                ////            .FilterByActive(true)
                ////            .FilterByID(target.OriginProductInventoryLocationSectionID)
                ////            .Where(x => x.Slave!.Active && x.Slave.InventoryLocation!.Active)
                ////            .AnyAsync()
                ////            .ConfigureAwait(false))
                ////{
                ////    // The one we have is good
                ////    productToPILSLookup[productID] = target.OriginProductInventoryLocationSectionID;
                ////    return;
                ////}
                ////// It was bad, so for sure don't keep it
                ////target.OriginProductInventoryLocationSectionID
                ////    = productToPILSLookup[productID]
                ////    = await context.ProductInventoryLocationSections
                ////        .AsNoTracking()
                ////        .FilterByActive(true)
                ////        .FilterPILSByProductID(productID)
                ////        .Where(x => x.Master!.Active)
                ////        .Select(x => x.ID)
                ////        .OrderBy(x => x)
                ////        .FirstAsync()
                ////        .ConfigureAwait(false);
                return Task.CompletedTask;
            }
            async Task<CEFActionResponse<List<ISalesItemTargetBaseModel>>> ReloadTargetsToSalesItemAsync(
                IClarityEcommerceEntities context,
                int? productID,
                bool nothingToShip,
                ISalesItemBaseModel salesItem)
            {
                if (Contract.CheckValidID(salesItem.ProductID)
                    && loadedProducts[salesItem.ProductID!.Value].nothingToShip)
                {
                    var target = new SalesItemTargetBaseModel
                    {
                        Active = true,
                        NothingToShip = true,
                        DestinationContactID = defaultBillingContactID,
                        DestinationContact = null,
                        TypeID = Contract.RequiresValidID(ShipToHomeTargetTypeID),
                        TypeKey = "ShipToHome",
                        Type = null,
                        SerializableAttributes = new(),
                        Quantity = salesItem.Quantity,
                    };
                    await LoadContactForTargetAsync(context, target).ConfigureAwait(false);
                    return new List<ISalesItemTargetBaseModel> { target }.WrapInPassingCEFAR()!;
                }
                if (Contract.CheckEmpty(salesItem.Targets))
                {
                    // First load wasn't null but didn't get any results or the load above didn't get any, try to get
                    // the data from the DB again
                    salesItem.Targets = (await context.CartItemTargets
                            .AsNoTracking()
                            .FilterByActive(true)
                            .FilterSalesItemTargetBasesByMasterID(salesItem.ID)
                            .Select(x => new
                            {
                                // Base Properties
                                x.ID,
                                x.CustomKey,
                                x.CreatedDate,
                                x.UpdatedDate,
                                x.Active,
                                x.Hash,
                                x.JsonAttributes,
                                // Target Properties
                                x.Quantity,
                                x.NothingToShip,
                                // Related Objects
                                x.MasterID,
                                x.TypeID,
                                x.DestinationContactID,
                                x.OriginProductInventoryLocationSectionID,
                                x.OriginStoreProductID,
                                x.BrandProductID,
                                x.OriginVendorProductID,
                                x.SelectedRateQuoteID,
                            }
                            !)
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => new SalesItemTargetBaseModel
                        {
                            // Base Properties
                            ID = x.ID,
                            CustomKey = x.CustomKey,
                            CreatedDate = x.CreatedDate,
                            UpdatedDate = x.UpdatedDate,
                            Active = x.Active,
                            Hash = x.Hash,
                            SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                            // Target Properties
                            Quantity = x.Quantity,
                            NothingToShip = x.NothingToShip,
                            // Related Objects
                            MasterID = x.MasterID,
                            TypeID = x.TypeID,
                            DestinationContactID = x.DestinationContactID,
                            BrandProductID = x.BrandProductID,
                            SelectedRateQuoteID = x.SelectedRateQuoteID,
                        })
                        .ToList<ISalesItemTargetBaseModel>();
                }
                if (salesItem.Targets!.Count == 0)
                {
                    // NOTE: This should never actually happen because the UI is assigning a default with one target
                    const string Message = "ERROR! No targets were detected for a cart item, at least one target"
                        + " should have been created for each item by the UI before the analyzer was run.";
                    await Logger.LogErrorAsync(
                            name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.NoTargets",
                            message: Message,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    return CEFAR.FailingCEFAR<List<ISalesItemTargetBaseModel>>(Message);
                }
                // Step 07.f. Create a copy of the targets list in memory for manipulation, we'll assign back later
                var targets = salesItem.Targets;
                // Step 07.g. Loop through the targets and back-fill data that may not have been assigned yet per
                // the settings for the application
                foreach (var target in targets)
                {
                    if (Contract.CheckValidID(productID))
                    {
                        // These three are only resolvable with real products
                        await ValidateAndFallBackStoreIDAndStoreProductIDAsync(
                                context: context,
                                referringStoreID: checkout.ReferringStoreID,
                                productID: productID!.Value,
                                target: target)
                            .ConfigureAwait(false);
                        await ValidateAndFallBackBrandIDAndBrandProductIDAsync(
                                context: context,
                                referringBrandID: checkout.ReferringBrandID,
                                productID: productID.Value,
                                target: target)
                            .ConfigureAwait(false);
                        await ValidateAndFallBackVendorIDAndVendorProductIDAsync(
                                context: context,
                                productID: productID.Value,
                                targetData: target)
                            .ConfigureAwait(false);
                        await ValidateAndFallBackPILSIDAsync(
                                context: context,
                                productID: productID.Value,
                                target: target)
                            .ConfigureAwait(false);
                    }
                    // These can be assigned using data without a real product
                    target.NothingToShip = nothingToShip;
                    if (nothingToShip)
                    {
                        target.DestinationContactID = defaultBillingContactID;
                        target.DestinationContact = null;
                        await LoadContactForTargetAsync(context, target).ConfigureAwait(false);
                    }
                    if (!Contract.CheckValidID(target.TypeID))
                    {
                        target.TypeID = Contract.RequiresValidID(ShipToHomeTargetTypeID);
                        target.TypeKey = "ShipToHome";
                        target.Type = null;
                    }
                    target.SerializableAttributes ??= new();
                    if (target.Quantity <= 0m)
                    {
                        // TODO@JTG: Should this be an error? The allocation should have come from the front end
                        target.Quantity = 1m;
                    }
                    // Pull the object data if necessary for each thing on the target
                    if (Contract.CheckValidID(target.DestinationContactID)
                        && target.DestinationContact == null)
                    {
                        await LoadContactForTargetAsync(context, target).ConfigureAwait(false);
                    }
                    ////if (CEFConfigDictionary.StoresEnabled
                    ////    && Contract.CheckValidID(target.OriginStoreProductID)
                    ////    && target.OriginStoreProduct == null)
                    ////{
                    ////    await LoadStoreProductForTargetAsync(context, target).ConfigureAwait(false);
                    ////}
                    if (CEFConfigDictionary.BrandsEnabled
                        && Contract.CheckValidID(target.BrandProductID)
                        && target.BrandProduct == null)
                    {
                        await LoadBrandProductForTargetAsync(context, target).ConfigureAwait(false);
                    }
                    ////if (CEFConfigDictionary.VendorsEnabled
                    ////    && Contract.CheckValidID(target.OriginVendorProductID)
                    ////    && target.OriginVendorProduct == null)
                    ////{
                    ////    await LoadVendorProductForTargetAsync(context, target).ConfigureAwait(false);
                    ////}
                    ////if (CEFConfigDictionary.InventoryAdvancedEnabled
                    ////    && Contract.CheckValidID(target.OriginProductInventoryLocationSectionID)
                    ////    && target.OriginProductInventoryLocationSection?.Slave?.InventoryLocationID == null)
                    ////{
                    ////    await LoadPILSForTargetAsync(context, target).ConfigureAwait(false);
                    ////}
                    if (Contract.CheckValidID(target.TypeID) && !Contract.CheckValidKey(target.TypeKey))
                    {
                        await LoadTypeKeyForTargetAsync(context, target).ConfigureAwait(false);
                    }
                    var customSplitKey = new StringBuilder();
                    if (salesItem.ProductSerializableAttributes!.ContainsKey("Category-Split-Order-Type")
                        && salesItem.ProductSerializableAttributes.TryGetValue("Category-Split-Order-Type", out var value)
                        && Contract.CheckValidKey(value.Value))
                    {
                        // We know this product has the value, now check the Category or Stores it's assigned to for
                        // the '...Required' attribute
                        async Task<bool> IsRequired2Async()
                        {
                            var required = await context.Products
                                .AsNoTracking()
                                .FilterByID(productID)
                                .SelectMany(x => x.Categories!)
                                .FilterByActive(true)
                                .Select(x => x.Slave!)
                                .FilterByActive(true)
                                .FilterObjectsWithJsonAttributesByValues(CategorySplitOrderRequiredDict)
                                .AnyAsync()
                                .ConfigureAwait(false);
                            if (!required)
                            {
                                required = await context.Products
                                    .AsNoTracking()
                                    .FilterByID(productID)
                                    .SelectMany(x => x.Stores!)
                                    .FilterByActive(true)
                                    .Select(x => x.Master!)
                                    .FilterByActive(true)
                                    .FilterObjectsWithJsonAttributesByValues(StoreSplitOrderRequiredDict)
                                    .AnyAsync()
                                    .ConfigureAwait(false);
                            }
                            if (!required)
                            {
                                required = await context.Products
                                    .AsNoTracking()
                                    .FilterByID(productID)
                                    .SelectMany(x => x.Brands!)
                                    .FilterByActive(true)
                                    .Select(x => x.Master!)
                                    .FilterByActive(true)
                                    .FilterObjectsWithJsonAttributesByValues(BrandSplitOrderRequiredDict)
                                    .AnyAsync()
                                    .ConfigureAwait(false);
                            }
                            if (!required)
                            {
                                required = await context.Products
                                    .AsNoTracking()
                                    .FilterByID(productID)
                                    .SelectMany(x => x.Vendors!)
                                    .FilterByActive(true)
                                    .Select(x => x.Master!)
                                    .FilterByActive(true)
                                    .FilterObjectsWithJsonAttributesByValues(VendorSplitOrderRequiredDict)
                                    .AnyAsync()
                                    .ConfigureAwait(false);
                            }
                            if (!required)
                            {
                                required = await context.Products
                                    .AsNoTracking()
                                    .FilterByID(productID)
                                    .SelectMany(x => x.Manufacturers!)
                                    .FilterByActive(true)
                                    .Select(x => x.Master!)
                                    .FilterByActive(true)
                                    .FilterObjectsWithJsonAttributesByValues(ManufacturerSplitOrderRequiredDict)
                                    .AnyAsync()
                                    .ConfigureAwait(false);
                            }
                            return required;
                        }
                        if (!productsThatRequireSplitOrderByType.ContainsKey(salesItem.ProductID!.Value))
                        {
                            productsThatRequireSplitOrderByType[salesItem.ProductID.Value]
                                = await IsRequired2Async().ConfigureAwait(false);
                        }
                        if (productsThatRequireSplitOrderByType[salesItem.ProductID.Value])
                        {
                            customSplitKey.Append(value.Value);
                        }
                    }
                    if (originalCart.SerializableAttributes.ContainsKey("Categories-Mixed-Prepaid-Freight-Split-Choice")
                        && originalCart.SerializableAttributes.TryGetValue("Categories-Mixed-Prepaid-Freight-Split-Choice", out var value2)
                        && bool.TryParse(value2.Value, out var parsedValue)
                        && parsedValue
                        && salesItem.ProductSerializableAttributes.TryGetValue("Mixed-Prepaid-Freight-Type", out var value3)
                        && Contract.CheckValidKey(value3.Value))
                    {
                        customSplitKey.Append(value3.Value);
                    }
                    if (salesItem.ProductDropShipOnly
                        || salesItem.ProductSerializableAttributes.ContainsKey("DropShip")
                        && salesItem.ProductSerializableAttributes.TryGetValue("DropShip", out var dropShipAttrObject)
                        && bool.TryParse(dropShipAttrObject.Value, out var parsedDropShipValue)
                        && parsedDropShipValue)
                    {
                        customSplitKey.Append("DropShipRequired");
                    }
                    else
                    {
                        customSplitKey.Append("NormalShip");
                    }
                    // TODO: Append custom split logic per client
                    target.CustomSplitKey = customSplitKey.ToString();
                    // Hash this data together to see if we have changed anything
                    target.Hash = Digest.Crc64(target.ToHashableString());
                }
                return targets
                    .WrapInPassingCEFARIfNotNullOrEmpty<List<ISalesItemTargetBaseModel>, ISalesItemTargetBaseModel>();
            }
            /*
            * Step 08. Load out the == Split Rules == using the following rules
            * Item Quantity Targets:
            * By Kind (Pickup in Store vs Ship to Store vs Ship to Home)
            * By Brand (Accounting)
            * By Store (Origin/Accounting)
            * By Vendor (Origin/Accounting)
            * By Warehouse (Origin)
            * By NothingToShip (Product)
            * By Destination (Shipping Address)
            */
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                foreach (var salesItem in originalCart.SalesItems!)
                {
                    // Step 07.d. Resolve the product, first using the inputs to get a verified record ID, then to pull
                    // the basic information that we need to use about the product for this process (and no further
                    // data)
                    var productLookupKey = (salesItem.ProductID, salesItem.ProductKey, salesItem.ProductName);
                    var productID = productIDLookup.ContainsKey(productLookupKey)
                        ? productIDLookup[productLookupKey]
                        : productIDLookup[productLookupKey] = await Workflows.Products.ResolveToIDOptionalAsync(
                                byID: salesItem.ProductID,
                                byKey: salesItem.ProductKey,
                                byName: salesItem.ProductName,
                                model: null,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    var nothingToShip = false;
                    if (Contract.CheckValidID(productID))
                    {
                        await LoadProductForSalesItemAsync(context, productID!.Value, salesItem).ConfigureAwait(false);
                        nothingToShip = salesItem.ProductNothingToShip;
                    }
                    // Step 07.e. Get the status of existing Targets for this cart item
                    var result = await ReloadTargetsToSalesItemAsync(
                            context: context,
                            productID: productID,
                            nothingToShip: nothingToShip,
                            salesItem: salesItem)
                        .ConfigureAwait(false);
                    if (!result.ActionSucceeded)
                    {
                        return result.ChangeFailingCEFARType<List<ICartModel?>?>();
                    }
                    // Step 07.h. Assign the modified targets list back to the line item and then Update it in a manner
                    // which is similar to how the main Update process would do it, but don't mess with anything but
                    // targets specifically (reduces chance of other data getting messed up by accident).
                    // Any targets not in this list will be deactivated/deleted by the Associate workflow further down
                    salesItem.Targets = result.Result;
                    try
                    {
                        var entity = await context.CartItems
                            .FilterByActive(true)
                            .FilterByID(salesItem.ID)
                            .SingleAsync()
                            .ConfigureAwait(false);
                        if (Contract.CheckValidID(productID))
                        {
                            // 07.h.i. Ensure the validated product ID gets back on to the entity
                            entity.ProductID = productID!.Value;
                        }
                        // 07.h.ii. Resolve out the targets with the database records and assign/add as appropriate any sub-records
                        await Workflows.CartItemWithTargetsAssociation.AssociateObjectsAsync(
                                entity,
                                salesItem,
                                timestamp,
                                contextProfileName)
                            .ConfigureAwait(false);
                        // 07.h.iii. Save
                        await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                        // 07.h.iv. Now that we've saved, we need to get the item again with all the latest data, especially IDs
                        salesItem.Targets = null;
                        // 07.h.v. Add it to the list we'll use to save modifications with
                        var resultAfterUpdate = await ReloadTargetsToSalesItemAsync(
                                context,
                                productID,
                                nothingToShip,
                                salesItem)
                            .ConfigureAwait(false);
                        if (!resultAfterUpdate.ActionSucceeded)
                        {
                            return resultAfterUpdate.ChangeFailingCEFARType<List<ICartModel?>?>();
                        }
                        salesItem.Targets = resultAfterUpdate.Result;
                        foreach (var target in salesItem.Targets!)
                        {
                            // Create a key and fill it in
                            var dictKey = new TargetGroupingKey(
                                    target.TypeKey,
                                    null,
                                    CEFConfigDictionary.BrandsEnabled ? target.BrandProduct?.MasterID : null,
                                    null,
                                    null,
                                    target.NothingToShip,
                                    target.CustomSplitKey,
                                    target.DestinationContact!.ConvertContactToComparableHashedValue())
                                .ToString();
                            // Ensure the key we filled in is setup in the dictionary
                            if (!targetGroupings.ContainsKey(dictKey) || targetGroupings[dictKey].salesItems == null)
                            {
                                targetGroupings[dictKey] = (target.DestinationContactID, new());
                            }
                            // Add this item to the dictionary using this key
                            targetGroupings[dictKey].salesItems.Add(salesItem);
                        }
                    }
                    catch (Exception ex3)
                    {
                        await Logger.LogErrorAsync(
                                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex3.GetType().Name}",
                                message: $"Failed to get salesItem for {salesItem.ID} (have previous targets)",
                                ex: ex3,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
            }
            // We should now know all of the separate targets and what items would end up in each target's list
            // Create separate Carts so they can run their own shipping's, taxes, discounts and totals
            var resultingCarts = new List<ICartModel>();
            originalCart.Totals.Tax = 0;
            foreach (var targetGrouping in targetGroupings)
            {
                var doUpdate = false;
                var doCreate = false;
                // See if a cart already exists for this target grouping
                CEFActionResponse<ICartModel?> targetCartResult;
                // Make sure there aren't any invalid carts sitting
                await Workflows.Carts.RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
                if (asAdmin)
                {
                    targetCartResult = (await Workflows.Carts.AdminGetAsync(
                                lookupKey: new SessionCartBySessionAndTypeLookupKey(
                                    sessionID: originalCart.SessionID.Value,
                                    typeKey: $"Target-Grouping-{targetGrouping.Key}",
                                    userID: lookupKey.UserID!.Value,
                                    accountID: lookupKey.AccountID!.Value,
                                    brandID: lookupKey.BrandID,
                                    franchiseID: lookupKey.FranchiseID,
                                    storeID: lookupKey.StoreID),
                                pricingFactoryContext: pricingFactoryContext,
                                taxesProvider: taxesProvider,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .WrapInPassingCEFARIfNotNull<ICartModel?>();
                }
                else
                {
                    targetCartResult = (await Workflows.Carts.SessionGetAsync(
                                lookupKey: new SessionCartBySessionAndTypeLookupKey(
                                    sessionID: originalCart.SessionID.Value,
                                    typeKey: $"Target-Grouping-{targetGrouping.Key}",
                                    userID: lookupKey.UserID!.Value,
                                    accountID: lookupKey.AccountID!.Value,
                                    brandID: lookupKey.BrandID,
                                    franchiseID: lookupKey.FranchiseID,
                                    storeID: lookupKey.StoreID),
                                pricingFactoryContext: pricingFactoryContext,
                                taxesProvider: taxesProvider,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .cartResponse;
                }
                var targetCartModel = targetCartResult.Result;
                if (targetCartModel == null)
                {
                    // There wasn't, make one
                    targetCartModel = RegistryLoaderWrapper.GetInstance<ICartModel>(contextProfileName);
                    targetCartModel.Active = true;
                    targetCartModel.CreatedDate = timestamp;
                    doCreate = true;
                }
                if (!targetCartModel.Active)
                {
                    targetCartModel.Active = true;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.SessionID != originalCart.SessionID)
                {
                    targetCartModel.SessionID = originalCart.SessionID;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.UserID != lookupKey.UserID)
                {
                    targetCartModel.UserID = lookupKey.UserID;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.AccountID != lookupKey.AccountID)
                {
                    targetCartModel.AccountID = lookupKey.AccountID;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.BrandID != lookupKey.BrandID)
                {
                    targetCartModel.BrandID = lookupKey.BrandID;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.FranchiseID != lookupKey.FranchiseID)
                {
                    targetCartModel.FranchiseID = lookupKey.FranchiseID;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.StoreID != lookupKey.StoreID)
                {
                    targetCartModel.StoreID = lookupKey.StoreID;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.BillingContactID != originalCart.BillingContactID)
                {
                    targetCartModel.BillingContactID = originalCart.BillingContactID;
                    doUpdate |= !doCreate;
                }
                if (targetCartModel.ShippingContactID != targetGrouping.Value.destID)
                {
                    targetCartModel.ShippingContactID = targetGrouping.Value.destID;
                    doUpdate |= !doCreate;
                }
                if (!Contract.CheckValidIDOrAnyValidKey(
                        targetCartModel.TypeID,
                        targetCartModel.TypeKey,
                        targetCartModel.TypeName,
                        targetCartModel.TypeDisplayName))
                {
                    targetCartModel.TypeID = await Workflows.CartTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: $"Target-Grouping-{targetGrouping.Key}",
                            byName: $"Target-Grouping-{targetGrouping.Key}",
                            byDisplayName: $"Target-Grouping-{targetGrouping.Key}",
                            model: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    doUpdate |= !doCreate;
                }
                if (!Contract.CheckValidIDOrAnyValidKey(
                        targetCartModel.StatusID,
                        targetCartModel.StatusKey,
                        targetCartModel.StatusName,
                        targetCartModel.StatusDisplayName))
                {
                    if (Contract.CheckInvalidID(CartStatusIDNew))
                    {
                        CartStatusIDNew = await Workflows.CartStatuses.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "New",
                                byName: "New",
                                byDisplayName: "New",
                                model: null,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    }
                    targetCartModel.StatusID = CartStatusIDNew;
                    doUpdate |= !doCreate;
                }
                if (!Contract.CheckValidIDOrAnyValidKey(
                        targetCartModel.StateID,
                        targetCartModel.StateKey,
                        targetCartModel.StateName,
                        targetCartModel.StateDisplayName))
                {
                    if (Contract.CheckInvalidID(CartStateIDWork))
                    {
                        CartStateIDWork = await Workflows.CartStates.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "WORK",
                                byName: "Work",
                                byDisplayName: "Work",
                                model: null,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    }
                    targetCartModel.StateID = CartStateIDWork;
                    doUpdate |= !doCreate;
                }
                targetCartModel.Totals ??= RegistryLoaderWrapper.GetInstance<ICartTotals>(contextProfileName);
                // Update the Items and their Targets
                var targetSalesItems = (targetCartModel.SalesItems ?? new List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>())
                    .OrderBy(x => x.ProductID)
                    .ThenBy(x => x.ForceUniqueLineItemKey)
                    .ThenBy(x => x.TotalQuantity)
                    .ToList();
                var originalItemsForThisGrouping = targetGrouping.Value.salesItems
                    .OrderBy(x => x.ProductID)
                    .ThenBy(x => x.ForceUniqueLineItemKey)
                    .ThenBy(x => x.TotalQuantity)
                    .ToList();
                var processedExistingIDs = new List<(int?, int?)>();
                var targetGrouping2 = TargetGroupingKey.FromString(targetGrouping.Key);
                // Process the existing ones for anything we need to change
                foreach (var targetSalesItem in targetSalesItems)
                {
                    if (!originalItemsForThisGrouping
                            .Select(x => new { x.ProductID, x.ForceUniqueLineItemKey }!)
                            .Contains(new { targetSalesItem.ProductID, targetSalesItem.ForceUniqueLineItemKey }))
                    {
                        processedExistingIDs.Add((targetSalesItem.ID, null));
                        // This product is no longer in this grouping,
                        // deactivate it so it will get removed
                        targetSalesItem.Active = false;
                        targetSalesItem.UpdatedDate = timestamp;
                        targetSalesItem.SerializableAttributes ??= new();
                        doUpdate |= !doCreate;
                        continue;
                    }
                    try
                    {
                        var originalItem = originalItemsForThisGrouping
                            .Single(x => x.Active
                                && x.ProductID == targetSalesItem.ProductID
                                && (Contract.CheckAllInvalidKeys(x.ForceUniqueLineItemKey, targetSalesItem.ForceUniqueLineItemKey)
                                    || x.ForceUniqueLineItemKey == targetSalesItem.ForceUniqueLineItemKey));
                        var originalTarget = originalItem.Targets!
                            .Single(x => x.Active
                                && x.BrandProduct?.BrandID == targetGrouping2.BrandID
                                && x.DestinationContact!.ConvertContactToComparableHashedValue() == targetGrouping2.HashedDestination
                                && x.TypeKey == targetGrouping2.TypeKey
                                && x.CustomSplitKey == targetGrouping2.CustomSplitKey
                                && x.NothingToShip == targetGrouping2.NothingToShip);
                        if (originalTarget.Quantity < 1m)
                        {
                            originalTarget.Quantity = 1m;
                        }
                        if (targetSalesItem.UnitOfMeasure != originalItem.UnitOfMeasure)
                        {
                            targetSalesItem.UnitOfMeasure = originalItem.UnitOfMeasure;
                            doUpdate |= !doCreate;
                        }
                        processedExistingIDs.Add((targetSalesItem.ID, originalItem.ID));
                        if (targetSalesItem.TotalQuantity == originalTarget.Quantity)
                        {
                            // This one is good, we don't need to update it
                            continue;
                        }
                        // We need to update the quantity, apply PreSold amounts first, fallback to Back Ordered amounts
                        if ((targetSalesItem.QuantityPreSold ?? 0) > 0m)
                        {
                            targetSalesItem.QuantityPreSold = originalTarget.Quantity;
                        }
                        else if ((targetSalesItem.QuantityBackOrdered ?? 0) > 0m)
                        {
                            targetSalesItem.QuantityBackOrdered = originalTarget.Quantity;
                        }
                        else
                        {
                            targetSalesItem.Quantity = originalTarget.Quantity;
                        }
                    }
                    catch (Exception ex6)
                    {
                        await Logger.LogErrorAsync(
                                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex6.GetType().Name}",
                                message: $"Failed to get originalTarget for {targetGrouping.Key}",
                                ex: ex6,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                    targetSalesItem.UpdatedDate = timestamp;
                    targetSalesItem.SerializableAttributes ??= new();
                    doUpdate |= !doCreate;
                }
                // Process ones that haven't been processed from original set so we can add them to this cart
                foreach (var originalItem in originalItemsForThisGrouping)
                {
                    // Skip if we matched it above
                    if (processedExistingIDs.Any(x => x.Item2 == originalItem.ID))
                    {
                        continue;
                    }
                    // Add the item as new to this target cart
                    var newItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(contextProfileName);
                    newItem.Active = true;
                    newItem.CreatedDate = timestamp;
                    newItem.Hash = originalItem.Hash;
                    newItem.SerializableAttributes = new();
                    newItem.ProductID = originalItem.ProductID;
                    try
                    {
                        var originalTarget = originalItem.Targets!
                            .Single(x => x.Active
                                && x.BrandProduct?.BrandID == targetGrouping2.BrandID
                                && x.DestinationContact!.ConvertContactToComparableHashedValue() == targetGrouping2.HashedDestination
                                && x.TypeKey == targetGrouping2.TypeKey
                                && x.CustomSplitKey == targetGrouping2.CustomSplitKey
                                && x.NothingToShip == targetGrouping2.NothingToShip);
                        if (originalItem.Targets!.Count == 1 && originalTarget.Quantity < 1m)
                        {
                            originalTarget.Quantity = 1m;
                        }
                        newItem.Sku = originalItem.Sku;
                        newItem.ForceUniqueLineItemKey = originalItem.ForceUniqueLineItemKey;
                        newItem.UnitOfMeasure = originalItem.UnitOfMeasure;
                        newItem.UserID = originalItem.UserID;
                        var newTargets = new List<ISalesItemTargetBaseModel>();
                        var newTarget = RegistryLoaderWrapper.GetInstance<ISalesItemTargetBaseModel>(contextProfileName);
                        newTarget.Active = true;
                        newTarget.CreatedDate = timestamp;
                        // TODO: Auto-Switch DestinationContactID based on TypeKey (Ship to Home, Ship to Store, Pickup in Store)
                        newTarget.DestinationContactID = targetGrouping.Value.destID ?? 0;
                        ////if (CEFConfigDictionary.InventoryAdvancedEnabled)
                        ////{
                        ////    newTarget.OriginProductInventoryLocationSectionID = originalTarget.OriginProductInventoryLocationSectionID;
                        ////}
                        ////if (CEFConfigDictionary.StoresEnabled)
                        ////{
                        ////    newTarget.OriginStoreProductID = originalTarget.OriginStoreProductID;
                        ////}
                        ////if (CEFConfigDictionary.BrandsEnabled)
                        ////{
                        ////    newTarget.BrandProductID = originalTarget.BrandProductID;
                        ////}
                        ////if (CEFConfigDictionary.VendorsEnabled)
                        ////{
                        ////    newTarget.OriginVendorProductID = originalTarget.OriginVendorProductID;
                        ////}
                        newTarget.TypeKey = targetGrouping2.TypeKey;
                        newTarget.NothingToShip = originalTarget.NothingToShip;
                        if (newTarget.NothingToShip)
                        {
                            newTarget.TypeID = ShipToHomeTargetTypeID;
                            newTarget.TypeKey = "ShipToHome";
                        }
                        // TODO@JTG: Determine how we will apply quantity back ordered/pre-sold here
                        newItem.Quantity = newTarget.Quantity = originalTarget.Quantity;
                        ////newItem.QuantityBackOrdered = originalItem.QuantityBackOrdered;
                        ////newItem.QuantityPreSold = originalItem.QuantityPreSold;
                        newItem.UnitCorePrice = originalItem.UnitCorePrice;
                        newItem.UnitSoldPrice = originalItem.UnitSoldPrice;
                        // TODO@JTG: Recalculate Extended Price to the quantity on this order, don't lose volume pricing from the full order
                        // TODO@JTG: Recalculate Multi-Currency
                        newTargets.Add(newTarget);
                        newItem.Targets = newTargets;
                        targetSalesItems.Add(newItem);
                        doUpdate |= !doCreate;
                    }
                    catch (Exception ex5)
                    {
                        await Logger.LogErrorAsync(
                                name: $"{nameof(TargetOrderCheckoutProvider)}.{nameof(AnalyzeInnerAsync)}.{ex5.GetType().Name}",
                                message: $"Failed to get originalTarget for {targetGrouping}",
                                ex: ex5,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        throw;
                    }
                }
                async Task DoTaxes2Async(ISalesCollectionBaseModel original)
                {
                    if (taxesProvider == null)
                    {
                        return;
                    }
                    var taxes = await taxesProvider.CalculateCartAsync(
                            targetCartModel,
                            lookupKey.UserID,
                            lookupKey.AltAccountID,
                            contextProfileName,
                            targetGrouping2)
                        .ConfigureAwait(false);
                    // TODO: Handle taxes.ErrorMessages.Any()
                    original.Totals.Tax += taxes.TotalTaxes;
                    if (targetCartModel.Totals.Tax == taxes.TotalTaxes)
                    {
                        return;
                    }
                    targetCartModel.Totals.Tax = taxes.TotalTaxes;
                    doUpdate |= !doCreate;
                }
                if (doCreate)
                {
                    targetCartModel.SalesItems = targetSalesItems;
                    await DoTaxes2Async(originalCart).ConfigureAwait(false);
                    // Make sure there aren't any invalid carts sitting
                    await Workflows.Carts.RemoveCartsThatAreEmptyAsync(contextProfileName).ConfigureAwait(false);
                    if (asAdmin)
                    {
                        var response = await Workflows.Carts.AdminCreateAsync(targetCartModel, contextProfileName).ConfigureAwait(false);
                        if (response.ActionSucceeded)
                        {
                            var targetCart = await Workflows.Carts.AdminGetAsync(
                                    lookupKey: new CartByIDLookupKey(
                                        cartID: response.Result,
                                        userID: lookupKey.UserID!.Value,
                                        accountID: lookupKey.AccountID!.Value,
                                        brandID: lookupKey.BrandID,
                                        franchiseID: lookupKey.FranchiseID,
                                        storeID: lookupKey.StoreID),
                                    pricingFactoryContext: pricingFactoryContext,
                                    taxesProvider: taxesProvider,
                                    contextProfileName: contextProfileName)
                                .ConfigureAwait(false);
                            if (targetCart == null)
                            {
                                throw new InvalidOperationException("Target Cart was null [5]");
                            }
                            resultingCarts.Add(targetCart);
                        }
                        else
                        {
                            throw new ArgumentException(
                                $"Unable to generate the target cart: {targetCartModel.TypeKey}");
                        }
                    }
                    else
                    {
                        await Workflows.Carts.CreateAsync(targetCartModel, contextProfileName).ConfigureAwait(false);
                        var targetCart = await TryResolveCartAsync(
                                checkout: checkout,
                                pricingFactoryContext: pricingFactoryContext,
                                lookupKey: new(
                                    sessionID: originalCart.SessionID!.Value,
                                    typeKey: $"Target-Grouping-{targetGrouping.Key}",
                                    userID: lookupKey.UserID!.Value,
                                    accountID: lookupKey.AccountID!.Value,
                                    brandID: lookupKey.BrandID,
                                    franchiseID: lookupKey.FranchiseID,
                                    storeID: lookupKey.StoreID),
                                taxesProvider: taxesProvider,
                                validate: false,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        if (!targetCart.ActionSucceeded)
                        {
                            throw new InvalidOperationException("Target Cart was null [4a]");
                        }
                        if (targetCart == null)
                        {
                            throw new InvalidOperationException("Target Cart was null [4b]");
                        }
                        _ = Contract.RequiresAllValidIDs(targetCart.Result!.SalesItems!.Select(x => x.ID).ToArray());
                        _ = Contract.RequiresAllValidIDs(targetCart.Result!.SalesItems!.SelectMany(x => x.Targets!.Select(y => y.ID)).ToArray());
                        resultingCarts.Add(targetCart.Result!);
                    }
                }
                else if (doUpdate)
                {
                    targetCartModel.SalesItems = targetSalesItems;
                    await DoTaxes2Async(originalCart).ConfigureAwait(false);
                    var totalTargetCartQuantity = targetCartModel.SalesItems
                        .Where(x => x.Active)
                        .Select(x => x.TotalQuantity)
                        .DefaultIfEmpty(0m)
                        .Sum();
                    if (totalTargetCartQuantity <= 0m)
                    {
                        // Since the target cart is empty, we don't need to try and resolve it back out, just remove it
                        try
                        {
                            await Workflows.Carts.DeleteAsync(targetCartModel.ID, contextProfileName).ConfigureAwait(false);
                        }
                        catch
                        {
                            // Do Nothing
                        }
                    }
                    else if (asAdmin)
                    {
                        // The target cart still has contents so we need to try and resolve it back out after pushing the update
                        var response = await Workflows.Carts.AdminUpdateAsync(targetCartModel, contextProfileName).ConfigureAwait(false);
                        if (!response.ActionSucceeded)
                        {
                            throw new ArgumentException(
                                $"Unable to update the target cart: {targetCartModel.TypeKey}");
                        }
                        var targetCart = await Workflows.Carts.AdminGetAsync(
                                lookupKey: new CartByIDLookupKey(
                                    cartID: targetCartModel.ID,
                                    userID: lookupKey.UserID!.Value,
                                    accountID: lookupKey.AccountID!.Value,
                                    brandID: lookupKey.BrandID,
                                    franchiseID: lookupKey.FranchiseID,
                                    storeID: lookupKey.StoreID),
                                pricingFactoryContext: pricingFactoryContext,
                                taxesProvider: taxesProvider,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        if (targetCart == null)
                        {
                            throw new InvalidOperationException("Target Cart was null [3]");
                        }
                        _ = Contract.RequiresAllValidIDs(targetCart.SalesItems!.Select(x => x.ID).ToArray());
                        _ = Contract.RequiresAllValidIDs(targetCart.SalesItems!.SelectMany(x => x.Targets!.Select(y => y.ID)).ToArray());
                        resultingCarts.Add(targetCart);
                    }
                    else
                    {
                        // The target cart still has contents so we need to try and resolve it back out after pushing the update
                        await Workflows.Carts.UpdateAsync(targetCartModel, contextProfileName).ConfigureAwait(false);
                        var targetCart = await TryResolveCartAsync(
                                checkout: checkout,
                                pricingFactoryContext: pricingFactoryContext,
                                lookupKey: new(
                                    sessionID: originalCart.SessionID.Value,
                                    typeKey: $"Target-Grouping-{targetGrouping.Key}",
                                    userID: lookupKey.UserID!.Value,
                                    accountID: lookupKey.AccountID!.Value,
                                    brandID: lookupKey.BrandID,
                                    franchiseID: lookupKey.FranchiseID,
                                    storeID: lookupKey.StoreID),
                                taxesProvider: taxesProvider,
                                validate: false,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        if (targetCart?.Result == null)
                        {
                            throw new InvalidOperationException("Target Cart was null [2]");
                        }
                        _ = Contract.RequiresAllValidIDs(targetCart.Result.SalesItems!.Select(x => x.ID).ToArray());
                        _ = Contract.RequiresAllValidIDs(targetCart.Result.SalesItems!.SelectMany(x => x.Targets!.Select(y => y.ID)).ToArray());
                        resultingCarts.Add(targetCart.Result);
                    }
                }
                else
                {
                    if (targetCartModel == null)
                    {
                        throw new InvalidOperationException("Target Cart was null [1]");
                    }
                    _ = Contract.RequiresAllValidIDs(targetCartModel.SalesItems!.Select(x => x.ID).ToArray());
                    _ = Contract.RequiresAllValidIDs(targetCartModel.SalesItems!.SelectMany(x => x.Targets!.Select(y => y.ID)).ToArray());
                    resultingCarts.Add(targetCartModel);
                }
            }
            await Workflows.Carts.UpdateAsync(originalCart, contextProfileName).ConfigureAwait(false);
            await ReloadOriginalCartAsync(false, lookupKey.AltAccountID).ConfigureAwait(false);
            if (resultingCarts.Count == 0)
            {
                resultingCarts.Add(originalCart);
                return resultingCarts
                    .WrapInFailingCEFAR("No carts were available after analysis")
                    .ChangeFailingCEFARType<List<ICartModel?>?>();
            }
            var discountsResult = await Workflows.DiscountManager.AddDiscountsByIDsAsync(
                    discountIDs: originalDiscountIDs,
                    cartID: originalCart.ID,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (discountsResult.ActionSucceeded)
            {
                await ReloadOriginalCartAsync(false, lookupKey.AltAccountID).ConfigureAwait(false);
            }
            resultingCarts.Insert(0, originalCart);
            return resultingCarts.WrapInPassingCEFAR()!;
        }

        /// <summary>Updates the shipping contact for cart.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        // ReSharper disable once UnusedMember.Local
        private static Task<CEFActionResponse> UpdateShippingContactForCartAsync(
            ISalesCollectionBaseModel cart,
            string? contextProfileName)
        {
            return Workflows.Carts.SetShippingContactAsync(
                cart.ID,
                cart.ShippingContact,
                contextProfileName);
        }
    }
}
