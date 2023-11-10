/**
 * @file framework/store/_services/cvSearchCatalogProductCompareService.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Search catalog product compare service class
 */
module cef.store.services {
    export interface ISearchCatalogProductCompareService {
        addItem(id: number): ng.IPromise<Array<api.ProductModel>>;
        removeItem(id: number): ng.IPromise<boolean>;
        clearItems(): ng.IPromise<any>;
        isInCompareCart(id: number): boolean;
        getItems(): Array<api.ProductModel>; // used by Compare Cart Monitor Template
        getRowTitles(): Array<string>;
    }

    export class SearchCatalogProductCompareService implements ISearchCatalogProductCompareService {
        // SearchCatalogProductCompareService Properties
        compareItems: Array<api.ProductModel> = [];
        attributes: Array<api.GeneralAttributeModel>;
        paging: core.Paging<api.ProductModel> = null;
        // Functions
        addItem(id: number): ng.IPromise<Array<api.ProductModel>> {
            return this.$q((resolve, reject): void => {
                if (!id) {
                    reject(this.$translate("ui.storefront.errors.searchCatalog.NoIDSuppliedToCompare"));
                    return;
                }
                resolve(this.cvCartService.addCartItem(id, this.cvServiceStrings.carts.types.compare));
            });
        }
        removeItem(id: number): ng.IPromise<boolean> {
            return this.$q((resolve, reject) => {
                if (!id) {
                    reject(this.$translate("ui.storefront.errors.searchCatalog.NoIDSuppliedToCompare"));
                    return;
                }
                resolve(this.cvCartService.removeCartItemByType(id, this.cvServiceStrings.carts.types.compare));
            });
        }
        clearItems(): ng.IPromise<any> {
            return this.cvCartService.clearCart(this.cvServiceStrings.carts.types.compare);
        }
        isInCompareCart(id: number): boolean {
            if (!id) { return false; }
            return this.cvCartService.cartContainsItem(id, this.cvServiceStrings.carts.types.compare);
        }
        setLocalItems(items: Array<api.ProductModel>): ng.IPromise<void> {
            if (!angular.isArray(items) || this.leaveIfEmpty(items)) {
                return this.$q.resolve();
            }
            this.paging.data = this.compareItems = items;
            this.paging.resetCachedFilteredData();
            this.paging.currentPage = 0;
            return this.$q.resolve();
        }
        leaveIfEmpty(arr: any[]): boolean {
            if (!arr || !arr.length) {
                // Need at least 2 items to stay in compare view
                this.paging.data = this.compareItems = []; // Also empty the arrays
                if (this.$state.includes("searchCatalog.products.compare")) {
                    window.history.back();
                }
                return true;
            }
            if (arr && arr.length === 1) {
                this.paging.data = this.compareItems = arr;
                // Need at least 2 items to stay in compare view
                if (this.$state.includes("searchCatalog.products.compare")) {
                    window.history.back();
                }
                return true;
            }
            return false;
        }
        // NOTE: This must remain an arrow function to resolve 'this' correctly
        onCartLoaded_RunObservableCallbacks = (__: ng.IAngularEvent, cartType: string, event: string): void => {
            if (cartType !== this.cvServiceStrings.carts.types.compare) { return; }
            const cart = this.cvCartService.accessCart(this.cvServiceStrings.carts.types.compare);
            if (!cart || angular.toJson(cart) === "{}") {
                this.setLocalItems([]);
                return;
            }
            this.$q.all(cart.SalesItems.map(item => this.cvProductService.get({ id: item.ProductID })))
                .then((products: api.ProductModel[]) => this.setLocalItems(products))
                .then(() => {
                    if (cartType !== this.cvServiceStrings.carts.types.compare
                        || event !== "itemAdded") {
                        return;
                    }
                    this.cvCartService.loadCart(this.cvServiceStrings.carts.types.compare, true, "")
                    if ((this.cvCartService.accessCart(this.cvServiceStrings.carts.types.compare) as any) === ({} as any)
                        || !this.cvCartService.accessCart(this.cvServiceStrings.carts.types.compare).SalesItems
                        || this.cvCartService.accessCart(this.cvServiceStrings.carts.types.compare).SalesItems.length < 2) {
                        // Don't show the modal if there's only one item in compare cart
                        return;
                    }
                    this.cvConfirmModalFactory(
                            this.$translate(
                                "ui.storefront.product.widgets.compare.CompareModalConfirmationMessage",
                                { type: cartType }))
                        .then(accepted => {
                            if (!accepted) { return; }
                            // Go to compare view TODO: May need corsLink?
                            const stateName = "searchCatalog.products.compare";
                            if (window.location.href.toLowerCase().indexOf(this.cefConfig.routes.catalog.root.toLowerCase()) === -1) {
                                this.$filter("goToCORSLink")(this.cefConfig.routes.catalog.root + ":" + stateName, "site", "primary");
                                return;
                            }
                            this.$state.go(stateName);
                        });
                });
        }
        getItems(): api.ProductModel[] { return [...this.compareItems]; }
        getRowTitles(): Array<string> {
            const rows: Array<string> = [];
            const skipKeys = [
                // Angular properties
                "$$hashKey", "__type",
                // Explicitly called out so don't iterate over
                "CustomKey", "Name", "Description", "ShortDescription",
                "Width", "WidthUnitOfMeasure",
                "Depth", "DepthUnitOfMeasure",
                "Height", "HeightUnitOfMeasure",
                "Weight", "WeightUnitOfMeasure",
                // General Skips
                "ID", "Active", "CreatedDate", "UpdatedDate", "Hash", "JsonAttributes", "jsonAttributes",
                "Type", "TypeID", "TypeKey", "TypeName", "TypeDisplayName", "TypeSortOrder",
                "Status", "StatusID", "StatusKey", "StatusName", "StatusDisplayName", "StatusSortOrder",
                "Accounts", "Vendors", "Manufacturers", "ProductAssociations",
                "ProductInventoryLocationSections", "ProductPricePoints",
                "ProductsAssociatedWith", "ProductSubscriptionTypes", "ProductFiles",
                "ProductImages", "PrimaryImageFileName", "ImageFileName", "IsEligibleForReturn",
                "Stores", "Images", "StoredFiles", "SortOrder", "KitQuantityOfParent", "KitCapacity",
                "KitBaseQuantityPriceMultiplier", "IsFreeShipping", "IsShippingRestricted", "ProductCategories",
                "SeoUrl", "SeoDescription", "SeoKeywords", "SeoPageTitle",
                "Package", "PackageID", "PackageKey", "PackageName",
                "MasterPack", "MasterPackID", "MasterPackKey", "MasterPackName",
                "Pallet", "PalletID", "PalletKey", "PalletName",
                "ProductRestrictions",
                "IsVisible", "IsTaxable", "IsDiscontinued", "AllowBackOrder", "IsUnlimitedStock",
                "StockQuantity", "StockQuantityAllocated", "StockQuantityPreSold",
                "TotalPurchasedAmount", "TotalPurchasedQuantity",
                "DocumentRequiredForPurchaseOverrideFeeIsPercent",
                "MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent",
                "ProductNotifications", "AllowPreSale", "DropShipOnly", "NothingToShip",
                "ShippingLeadTimeIsCalendarDays",
                "RequiresRolesList", "RequiresRolesListAlt",
                // Properties and functions that extend in Angular
                "inventoryObject", "isOutOfStock", "countStock", "quantity", "CurrentShipOption",
                "readPrices", "$_rawPrices", "readInventory", "$_rawInventory"
            ];
            this.compareItems.forEach(item => {
                var keys = Object.keys(item);
                keys.forEach(key => {
                    if (skipKeys.indexOf(key) > -1) { return; }
                    if (rows.indexOf(key) > -1) { return; }
                    if (key === "SerializableAttributes" && item.SerializableAttributes) {
                        const attrKeys = Object.keys(item.SerializableAttributes);
                        attrKeys.forEach(attrKey => {
                            if (!item.SerializableAttributes[attrKey].Value) { return; }
                            if (attrKey.endsWith("_UOM")) { return; } // TAL-Specific
                            if (item.SerializableAttributes[attrKey].Value === "000000000000") { return; } // TAL-Specific
                            if (item.SerializableAttributes[attrKey].Value === "0000000") { return; } // TAL-Specific
                            if (this.attributes.every(x => x.CustomKey !== attrKey)) { return; } // Not a comparable attribute
                            if (rows.indexOf(`SerializableAttributes.${attrKey}`) > -1) { return; }
                            rows.push(`SerializableAttributes.${attrKey}`);
                        });
                        return;
                    }
                    rows.push(key);
                });
            });
            return rows;
        }
        // Constructor
        constructor(
                readonly $rootScope: ng.IRootScopeService,
                readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $state: ng.ui.IStateService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly cvCartService: services.ICartService,
                private readonly cefConfig: core.CefConfig, // Used by UI
                private readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                private readonly cvApi: api.ICEFAPI,
                // private readonly cvAttributeService: services.IAttributeService,
                private readonly cvProductService: services.IProductService,
                private readonly cvServiceStrings: services.IServiceStrings) {
            $rootScope["cvSearchCatalogProductCompareService"] = this;
            this.paging = new core.Paging<api.ProductModel>($filter);
            this.paging.pageSize = 3;
            this.paging.pageSetSize = 3;
            const dto = <api.GetGeneralAttributesDto>{
                Active: true,
                IsComparable: true,
                IsMarkup: false,
                AsListing: true
            };
            // this.cvAttributeService.search(dto).then(attrs => this.attributes = attrs);
            // Need a more specific search and not to mess with other data for other controls
            this.cvApi.attributes.GetGeneralAttributes(dto).then(r => {
                if (!r || !r.data || !r.data.Results) {
                    return;
                }
                this.attributes = r.data.Results;
            });
            $rootScope.$on(cvServiceStrings.events.carts.loaded, this.onCartLoaded_RunObservableCallbacks);
            $rootScope.$on(cvServiceStrings.events.carts.cleared, (__: ng.IAngularEvent, type: string) => {
                if (type !== cvServiceStrings.carts.types.compare) { return; }
                this.paging.data = this.compareItems = [];
                this.paging.resetCachedFilteredData();
                this.paging.currentPage = 0;
            });
            this.cvCartService.loadCart(cvServiceStrings.carts.types.compare, false, "SearchCatalogProductCompareService.ctor");
        }
    }
}
