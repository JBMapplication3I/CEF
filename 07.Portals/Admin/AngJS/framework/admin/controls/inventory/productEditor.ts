/**
 * @file framework/admin/controls/inventory/productEditor.ts
 * @author Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
 * @desc Product editor class for CEF Administrators
 */
module cef.admin.controls.inventory {
    class AssociatedProductSearchParams { ProductCode: string; ProductName: string; }
    interface AssociatedProductListViewModel extends api.ProductModel { Selected?: boolean; Quantity?: number; }
    class ProductDetailDisplaySetting { ShowAssociatedProductPanel: boolean; }

    class ProductDetailController extends DetailBaseController<api.ProductModel> {
        // Forced overrides
        detailName = "Product";
        // Collections
        types: api.TypeModel[];
        statuses: api.StatusModel[];
        imageTypes: api.TypeModel[];
        downloadTypes: api.TypeModel[];
        packages: api.PackageModel[];
        productKits: api.ProductModel[];
        associationTypes: api.TypeModel[];
        vendors: api.VendorModel[];
        manufacturers: api.ManufacturerModel[];
        stores: api.StoreModel[];
        brands: api.BrandModel[];
        currencies: api.CurrencyModel[];
        associatedProducts: AssociatedProductListViewModel[];
        inventories: api.InventoryLocationSectionModel[];
        inventoryHistory: api.ProductInventoryLocationSectionModel[];
        pricePoints: api.PricePointModel[];
        countries: api.CountryModel[];
        regions: api.RegionModel[];
        accountTypes: api.TypeModel[];
        supplierMode: boolean;
        supplierKind: string;
        vendorID: number;
        vendor: api.VendorModel;
        storeID: number;
        store: api.StoreModel;
        brandID: number;
        brand: api.BrandModel;
        noPackageObject: api.PackageModel;
        noMasterPackObject: api.PackageModel;
        noPalletObject: api.PackageModel;
        // UI Data
        associationSearchParams: AssociatedProductSearchParams = { ProductCode: null, ProductName: null };
        display: ProductDetailDisplaySetting = { ShowAssociatedProductPanel: false };
        isCopy = false;
        now = new Date();
        uiMaskOptions = {
            maskDefinitions: {
                "9": /\d/,
                "#": /\d{0,1}/,
                ".": /\.{0,1}/,
                "A": /[a-zA-Z]/,
                "a": /[a-zA-Z]{0,1}/,
                "*": /[a-zA-Z0-9]/
            },
            clearOnBlur: true,
            clearOnBluePlaceholder: false,
            eventsToHandle: ["input", "keyup", "click", "focus"],
            addDefaultPlaceholder: true,
            escChar: "\\",
            allowInvalidValue: true
        };
        unbindAttributesChanged: Function;
        footerEl: angular.IAugmentedJQuery;
        bodyEl: angular.IAugmentedJQuery;
        productPricePoints: api.ProductPricePointModel[];
        // Required Functions
        protected constructorPreAction(): ng.IPromise<void> {
            this.consoleDebug("constructorPreAction:product:0");
            return this.$q.resolve();
        }
        protected loadCollections(): ng.IPromise<void> {
            this.consoleDebug("loadCollections:product:0");
            const paging = <api.Paging>{ Size: 500, StartIndex: 1 };
            const standardDto = { Active: true, AsListing: true, Paging: paging };
            this.consoleDebug("loadCollections:product:1");
            return this.$q((resolve, reject) => {
                this.consoleDebug("loadCollections:product:2");
                const promises = [];
                // 1-5
                promises.push(this.cvApi.products.GetProductTypes(standardDto));
                promises.push(this.cvApi.products.GetProductStatuses(standardDto));
                promises.push(this.cvApi.products.GetProductImageTypes(standardDto));
                promises.push(this.cvApi.products.GetProductDownloadTypes(standardDto));
                promises.push(this.cvApi.shipping.GetPackages(<api.GetPackagesDto>{ Active: true, Paging: paging, IsCustom: false }));
                // 6-10
                promises.push(this.cefConfig.featureSet.pricing.pricePoints.enabled
                    ? this.cvApi.pricing.GetPricePoints(standardDto)
                    : this.$q.resolve(<ng.IHttpPromiseCallbackArg<api.PricePointPagedResults>>{ status: 200, data: { Results: [] } }));
                promises.push(this.cvApi.products.GetProductAssociationTypes(standardDto));
                promises.push(this.cefConfig.featureSet.inventory.advanced.enabled
                    ? this.cvApi.inventory.GetInventoryLocationSections(standardDto)
                    : this.$q.resolve(<ng.IHttpPromiseCallbackArg<api.InventoryLocationSectionModel[]>>{ status: 200, data: [] }));
                promises.push(this.cefConfig.featureSet.vendors.enabled
                    ? this.cvApi.vendors.GetVendors(standardDto)
                    : this.$q.resolve(<ng.IHttpPromiseCallbackArg<api.VendorPagedResults>>{ status: 200, data: { Results: [] } }));
                promises.push(this.cefConfig.featureSet.manufacturers.enabled
                    ? this.cvApi.manufacturers.GetManufacturers(standardDto)
                    : this.$q.resolve(<ng.IHttpPromiseCallbackArg<api.ManufacturerPagedResults>>{ status: 200, data: { Results: [] } }));
                // 11-15
                promises.push(this.cefConfig.featureSet.multiCurrency.enabled
                    ? this.cvApi.currencies.GetCurrencies(standardDto)
                    : this.$q.resolve(<ng.IHttpPromiseCallbackArg<api.CurrencyPagedResults>>{ status: 200, data: { Results: [] } }));
                promises.push(this.cefConfig.featureSet.stores.enabled
                    ? this.cvApi.stores.GetStores(standardDto)
                    : this.$q.resolve(<ng.IHttpPromiseCallbackArg<api.StorePagedResults>>{ status: 200, data: { Results: [] } }));
                promises.push(this.cefConfig.featureSet.brands.enabled
                    ? this.cvApi.brands.GetBrands(standardDto)
                    : this.$q.resolve(<ng.IHttpPromiseCallbackArg<api.BrandPagedResults>>{ status: 200, data: { Results: [] } }));
                promises.push(this.cvApi.geography.GetCountries(standardDto));
                // 16-17
                promises.push(this.cvApi.geography.GetRegions(standardDto));
                promises.push(this.cvApi.accounts.GetAccountTypes(standardDto));
                this.consoleDebug("loadCollections:product:3");
                this.$q.all(promises).then((rarr: ng.IHttpPromiseCallbackArg<any>[]) => {
                    this.consoleDebug("loadCollections:product:4");
                    let index = 0;
                    // 1-5
                    this.types              = rarr[index++].data.Results;
                    this.statuses           = rarr[index++].data.Results;
                    this.imageTypes         = rarr[index++].data.Results;
                    this.downloadTypes      = rarr[index++].data.Results;
                    this.noPackageObject = {
                        ID: 0,
                        SortOrder: -1,
                        "IsDummy": true,
                        Name: "No Package",
                        IsCustom: false,
                        TypeKey: "Package"
                    } as any as api.PackageModel;
                    this.noMasterPackObject = {
                        ID: -2,
                        SortOrder: -1,
                        "IsDummy": true,
                        Name: "No Master Pack",
                        IsCustom: false,
                        TypeKey: "Master Pack"
                    } as any as api.PackageModel;
                    this.noPalletObject = {
                        ID: -3,
                        SortOrder: -1,
                        "IsDummy": true,
                        Name: "No Pallet",
                        IsCustom: false,
                        TypeKey: "Pallet"
                    } as any as api.PackageModel;
                    this.packages           = rarr[index++].data.Results;
                    this.packages.unshift(this.noPackageObject);
                    this.packages.unshift(this.noMasterPackObject);
                    this.packages.unshift(this.noPalletObject);
                    this.dummyizePackages(this.record);
                    // 6-10
                    this.consoleDebug("loadCollections:product:5");
                    this.pricePoints        = rarr[index++].data.Results;
                    this.associationTypes   = rarr[index++].data.Results;
                    this.inventories        = rarr[index++].data;
                    this.vendors            = rarr[index++].data.Results;
                    this.manufacturers      = rarr[index++].data.Results;
                    // 11-15
                    this.consoleDebug("loadCollections:product:6");
                    this.currencies         = rarr[index++].data.Results;
                    this.stores             = rarr[index++].data.Results;
                    this.brands             = rarr[index++].data.Results;
                    this.countries          = rarr[index++].data.Results;
                    // 16-17
                    this.consoleDebug("loadCollections:product:7");
                    this.regions            = rarr[index++].data.Results;
                    this.accountTypes       = rarr[index++].data.Results;
                    //
                    this.consoleDebug("loadCollections:product:8");
                    this.$scope.footerEl
                        = this.footerEl
                        = angular.element(document.querySelector("#footerProductEditor"));
                    this.$scope.bodyEl
                        = this.bodyEl
                        = angular.element(document.querySelector(".row.details-body"));
                    if (this.brands.length) {
                        this.consoleDebug("loadCollections:product:9");
                        if (!this.record.Brands) {
                            this.record.Brands = [];
                        }
                        /* TODO: Wrap this in a setting
                        this.brands.forEach(x => {
                            const found = _.find(this.record.Brands, y => y.BrandID === x.ID || y.MasterID === x.ID);
                            if (found) {
                                return;
                            }
                            this.record.Brands.push(<api.BrandProductModel>{
                                // Base Properties
                                ID: 0,
                                CustomKey: null,
                                Active: true,
                                CreatedDate: new Date(),
                                UpdatedDate: null,
                                //
                                BrandID: x.ID,
                                ProductID: this.record.ID || null,
                                //
                                IsVisibleInBrand: true
                            });
                        });
                        // */
                    }
                    this.consoleDebug("loadCollections:product:10");
                    this.loadPricesForEditing();
                    this.consoleDebug("loadCollections:product:11");
                    this.loadInventoryForEditing();
                    this.consoleDebug("loadCollections:product:12");
                    this.loadCategoryTree();
                    this.consoleDebug("loadCollections:product:13");
                    resolve();
                }).catch(reason => this.fullReject(reject, reason));
                this.consoleDebug("loadCollections:product:3x");
            });
        }
        loadNewRecord(): ng.IPromise<api.ProductModel> {
            this.consoleDebug("loadNewRecord:product:1");
            this.record = <api.ProductModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // NameableBase Properties
                Name: null,
                Description: null,
                // HaveSeoBase Properties
                SeoUrl: null,
                SeoPageTitle: null,
                SeoKeywords: null,
                SeoDescription: null,
                SeoMetaData: null,
                // HaveNullableDimensions Properties
                Weight: null,
                WeightUnitOfMeasure: "lbs",
                Width: null,
                WidthUnitOfMeasure: "in",
                Height: null,
                HeightUnitOfMeasure: "in",
                Depth: null,
                DepthUnitOfMeasure: "in",
                // Product Properties
                // Flags/Toggles
                IsVisible: true,
                IsDiscontinued: false,
                IsEligibleForReturn: false,
                IsTaxable: true,
                AllowBackOrder: true,
                AllowPreSale: false,
                IsUnlimitedStock: false,
                IsFreeShipping: false,
                NothingToShip: false,
                DropShipOnly: false,
                ShippingLeadTimeIsCalendarDays: false,
                // Descriptors
                ShortDescription: null,
                ManufacturerPartNumber: null,
                BrandName: null,
                TaxCode: null,
                UnitOfMeasure: "EACH",
                // Values
                AvailableStartDate: null,
                AvailableEndDate: null,
                HandlingCharge: null,
                FlatShippingCharge: null,
                RestockingFeePercent: null,
                RestockingFeeAmount: null,
                StockQuantity: 0,
                StockQuantityAllocated: null,
                MinimumPurchaseQuantity: null,
                MinimumPurchaseQuantityIfPastPurchased: null,
                MaximumPurchaseQuantity: null,
                MaximumPurchaseQuantityIfPastPurchased: null,
                QuantityPerMasterPack: null,
                QuantityMasterPackPerPallet: null,
                QuantityLayersPerPallet: null,
                QuantityMasterPackLayersPerPallet: null,
                QuantityMasterPackPerLayer: null,
                QuantityPerLayer: null,
                QuantityPerPallet: null,
                SortOrder: null,
                MustPurchaseInMultiplesOfAmountOverrideFeeIsPercent: false,
                DocumentRequiredForPurchaseOverrideFeeIsPercent: false,
                // Related Objects
                TypeID: 1,
                Type: null,
                StatusID: 1,
                Status: null,
                CompanyID: 0,
                Company: null,
                ProductTypeID: 0,
                ProductType: null,
                PackageID: 0,
                Package: null,
                // Associated Objects
                Images: [],
                StoredFiles: [],
                Vendors: [],
                Manufacturers: [],
                Stores: [],
                Brands: [],
                Accounts: [],
                ProductCategories: [],
                ProductAssociations: [],
                ProductRestrictions: [],
                ProductNotifications: [],
                ProductDownloads: [],
                ProductSubscriptionTypes: [],
                ProductsAssociatedWith: [],
                RequiresRolesList: [],
                RequiresRolesListAlt: [],
                // Convenience Properties
                IsShippingRestricted: false,
                HasChildren: false,
                InWishList: false,
                InNotifyMeList: false,
                InFavoritesList: false,
                InCompareCart: false,
                PurchaseableByCurrentUser: true,
                ImageFileName: null,
                VariationsSum: null,
                CategoryIDs: [],
                // Deprecated
                KitBaseQuantityPriceMultiplier: 1
            };
            this.consoleDebug("loadNewRecord:product:2");
            ////this.versions = [];
            ////this.consoleDebug("loadNewRecord:product:3");
            return this.$q.resolve(this.record);
        }
        loadRecordCall(id: number): ng.IHttpPromise<api.ProductModel> {
            this.consoleDebug("loadRecordCall:product:7");
            return this.cvApi.products.AdminGetProductFull(id);
        }
        protected loadRecordActionAfterSuccess(result: api.ProductModel): ng.IPromise<api.ProductModel> {
            this.consoleDebug("loadRecordActionAfterSuccess:product:1");
            return this.$q((resolve, reject) => {
                this.consoleDebug("loadRecordActionAfterSuccess:product:2");
                this.listenToAttributes();
                this.consoleDebug("loadRecordActionAfterSuccess:product:3");
                this.fixDates();
                this.consoleDebug("loadRecordActionAfterSuccess:product:4");
                this.loadSupplierModeInfo();
                this.consoleDebug("loadRecordActionAfterSuccess:product:5");
                resolve(result);
                ////this.handleVersions(resolve, reject, result);
            });
        }

        ////protected createRecordPreAction(toSend: api.ProductModel): ng.IPromise<api.ProductModel> {
        ////    return this.$q((resolve, reject) => {
        ////        this.cvCreateVersionModalFactory(toSend)
        ////            .then(r => resolve(r.record))
        ////            .catch(reject);
        ////    });
        ////}
        ////createRecordCall(routeParams: api.ProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
        ////    // Call already made, just pass the object back
        ////    return this.$q.resolve({ data: routeParams });
        ////}
        createRecordCall(routeParams: api.ProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.products.CreateProduct(routeParams);
        }
        createRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            return this.$q((resolve, reject) => this.savePrices(result.Result)
                .then(() => {
                    return this.saveInventory(result.Result)
                })
                .then(() => resolve(result.Result))
                .catch(reason1 => reject(reason1)));
        }

        ////protected updateRecordPreAction(toSend: api.ProductModel): ng.IPromise<api.ProductModel> {
        ////    return this.$q((resolve, reject) => {
        ////        this.cvCreateVersionModalFactory(toSend)
        ////            .then(r => resolve(r.record))
        ////            .catch(reject);
        ////    });
        ////}
        ////updateRecordCall(routeParams: api.ProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
        ////    // Call already made, just pass the object back
        ////    return this.$q.resolve({ data: routeParams });
        ////}
        updateRecordCall(routeParams: api.ProductModel): ng.IHttpPromise<api.CEFActionResponseT<number>> {
            return this.cvApi.products.UpdateProduct(routeParams);
        }
        updateRecordActionAfterSuccess(result: api.CEFActionResponseT<number>): ng.IPromise<number> {
            if (!result.ActionSucceeded) {
                this.consoleLog(result);
                return this.$q.reject(result);
            }
            return this.$q((resolve, reject) => this.savePrices(result.Result)
                .then(() => {
                    return this.saveInventory(result.Result)
                })
                .then(() => resolve(result.Result))
                .catch(reason1 => reject(reason1)));
        }

        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.products.DeactivateProductByID(id);
        }

        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.products.ReactivateProductByID(id);
        }

        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> {
            return this.cvApi.products.DeleteProductByID(id);
        }

        // Supportive Functions
        fixDates = (): void => {
            // NOTE: This function doesn't need setRunning
            // converts AvailableStartDate and AvailableEndDate to Date Object. This is necessary for AngularJS
            if (this.record.AvailableStartDate) {
                // start date
                let tmpStartDateString = this.record.AvailableStartDate;
                this.record.AvailableStartDate = new Date(tmpStartDateString.toString());
            }
            if (this.record.AvailableEndDate) {
                // end date
                let tmpEndDateString = this.record.AvailableEndDate;
                this.record.AvailableEndDate = new Date(tmpEndDateString.toString());
            }
            if (!this.cefConfig.featureSet.pricing.pricePoints.enabled) {
                this.productPricePoints = null;
                return;
            }
            if (!this.record.ID) {
                this.productPricePoints = [];
                return;
            }
            // TODO: Verify this can run every time vs initializing a separated paging control
            this.cvApi.products.GetProductPricePoints({
                Active: true,
                AsListing: true,
                Paging: { StartIndex: 1, Size: 16 }
            }).then(r => {
                const results = r.data.Results;
                results.forEach(x => {
                    if (x.From != null) {
                        const tmpFrom = new Date(x.From.toString());
                        tmpFrom.setTime(tmpFrom.getTime() + tmpFrom.getTimezoneOffset()*60*1000);
                        x.From = tmpFrom;
                    }
                    if (x.To != null) {
                        const tmpTo = new Date(x.To.toString());
                        tmpTo.setTime(tmpTo.getTime() + tmpTo.getTimezoneOffset()*60*1000);
                        x.To = tmpTo;
                    }
                });
                this.productPricePoints = results;
            });
        }
        private listenToAttributes(): void {
            // NOTE: This function doesn't need setRunning
            this.unbindAttributesChanged = this.unbindAttributesChanged
                || this.$scope.$on(this.cvServiceStrings.events.attributes.changed,
                    ($event: ng.IAngularEvent, changedList: widgets.IAttributeChangedEventArg[]) => {
                        if (!this.forms || !this.forms["Attributes"]) {
                            return;
                        }
                        if (!_.some(changedList,
                                x => x.property == "Value"
                                && x.newValue !== null
                                && x.oldValue !== undefined)) {
                            return;
                        }
                        this.forms["Attributes"].$setDirty();
                    });
        }
        private dummyizePackages(result: api.ProductModel): void {
            this.consoleDebug("dummyizePackages:product:1");
            // NOTE: This function doesn't need setRunning
            ////if (!result.Package) { this.resetPackage(); }
            ////if (!result.MasterPack) { this.resetMasterPack(); }
            ////if (!result.Pallet) { this.resetPallet(); }
            if (!result.Package && !result.PackageID) {
                this.consoleDebug("dummyizePackages:product:1.1");
                result.PackageID = -1;
                result.Package = this.noPackageObject;
                delete result.PackageKey;
                delete result.PackageName;
            } else if (!result.Package && result.PackageID > 0) {
                this.consoleDebug("dummyizePackages:product:1.2");
                this.cvApi.shipping.GetPackageByID(result.PackageID).then(r => {
                    this.consoleDebug("dummyizePackages:product:1.2.1");
                    if (!r || !r.data) {
                        console.warn("package not found");
                        return;
                    }
                    if (!_.some(this.packages, x => x.ID === r.data.ID)) {
                        this.consoleDebug("dummyizePackages:product:1.2.1.1");
                        this.packages.push(r.data);
                    }
                    this.consoleDebug("dummyizePackages:product:1.2.2");
                    result.Package = r.data;
                }).catch(reason => console.error(reason));
            }
            this.consoleDebug("dummyizePackages:product:2");
            if (!result.MasterPack && !result.MasterPackID) {
                this.consoleDebug("dummyizePackages:product:2.1");
                result.MasterPackID = -2;
                result.MasterPack = this.noMasterPackObject;
                delete result.MasterPackKey;
                delete result.MasterPackName;
            } else if (!result.MasterPack && result.MasterPackID > 0) {
                this.consoleDebug("dummyizePackages:product:2.2");
                this.cvApi.shipping.GetPackageByID(result.MasterPackID).then(r => {
                    this.consoleDebug("dummyizePackages:product:2.2.1");
                    if (!r || !r.data) {
                        console.warn("master pack not found");
                        return;
                    }
                    if (!_.some(this.packages, x => x.ID === r.data.ID)) {
                        this.consoleDebug("dummyizePackages:product:2.2.1.1");
                        this.packages.push(r.data);
                    }
                    this.consoleDebug("dummyizePackages:product:2.2.2");
                    result.MasterPack = r.data;
                }).catch(reason => console.error(reason));
            }
            this.consoleDebug("dummyizePackages:product:3");
            if (!result.Pallet && !result.PalletID) {
                this.consoleDebug("dummyizePackages:product:3.1");
                result.PalletID = -3;
                result.Pallet = this.noPalletObject;
                delete result.PalletKey;
                delete result.PalletName;
            } else if (!result.Package && result.PackageID > 0) {
                this.consoleDebug("dummyizePackages:product:3.2");
                this.cvApi.shipping.GetPackageByID(result.PalletID).then(r => {
                    this.consoleDebug("dummyizePackages:product:3.2.1");
                    if (!r || !r.data) {
                        console.warn("pallet not found");
                        return;
                    }
                    if (!_.some(this.packages, x => x.ID === r.data.ID)) {
                        this.consoleDebug("dummyizePackages:product:3.2.1.1");
                        this.packages.push(r.data);
                    }
                    this.consoleDebug("dummyizePackages:product:3.2.2");
                    result.Pallet = r.data;
                }).catch(reason => console.error(reason));
            }
        }
        loadSupplierModeInfo(): void {
            // NOTE: This function doesn't need setRunning
            if (!this.$stateParams["vendorID"] && !this.$stateParams["storeID"]) {
                this.supplierKind = "off";
                return;
            }
            this.record.IsVisible = false; // Defaulting this to off so it doesn't get picked up by the catalog
            if (this.$stateParams["vendorID"]) {
                this.supplierKind = "vendor";
                this.vendorID = Number(this.$stateParams["vendorID"]);
                this.cvApi.vendors.GetVendorByID(this.vendorID).then(r => this.vendor = r.data);
                if (!this.record.Vendors) { this.record.Vendors = []; }
                if (_.find(this.record.Vendors, x => x.MasterID === this.vendorID)) { return; }
                this.record.Vendors.push(<api.VendorProductModel>{
                    ID: 0,
                    CreatedDate: new Date(),
                    Active: true,
                    ProductID: 0,
                    VendorID: this.vendorID,
                });
                return;
            }
            this.supplierKind = "store";
            this.storeID = Number(this.$stateParams["storeID"]);
            this.cvApi.stores.GetStoreByID(this.storeID).then(r => this.store = r.data);
            if (!this.record.Stores) { this.record.Stores = []; }
            if (_.find(this.record.Stores, x => x.MasterID === this.storeID)) { return; }
            this.record.Stores.push(<api.StoreProductModel>{
                ID: 0,
                CreatedDate: new Date(),
                Active: true,
                ProductID: 0,
                StoreID: this.storeID,
                IsVisibleIn: true
            });
        }
        clone(): void {
            const copyProduct = _.cloneDeep(this.record);
            // Clear identification so it can't auto-resolve the same product
            delete copyProduct.ID;
            delete copyProduct.CustomKey;
            delete copyProduct.SeoUrl;
            // Give it a "copy" name
            copyProduct.Name = copyProduct.Name + " (Copy)"; // TODO: Numbered or Date based append?
            // Clear these associations and don't associate as new
            delete copyProduct.Accounts;
            delete copyProduct.ProductNotifications;
            // Keep these association lists, but clear the original product info so they associate as new
            if (copyProduct.ProductCategories && copyProduct.ProductCategories.length) {
                copyProduct.ProductCategories.forEach(x => {
                    delete x["ProductID"];
                    delete x["ProductKey"];
                    delete x["ProductName"];
                    delete x["ProductSeoUrl"];
                    delete x["Product"];
                    delete x.MasterID;
                    delete x.MasterKey;
                    delete x.MasterName;
                });
            }
            if (copyProduct.Images && copyProduct.Images.length) {
                copyProduct.Images.forEach(x => {
                    delete x.MasterID;
                    delete x.MasterKey;
                });
            }
            if (copyProduct.StoredFiles && copyProduct.StoredFiles.length) {
                copyProduct.StoredFiles.forEach(x => {
                    delete x.MasterID;
                    delete x.MasterKey;
                });
            }
            if (copyProduct.Vendors && copyProduct.Vendors.length) {
                copyProduct.Vendors.forEach(x => {
                    delete x["ProductID"];
                    delete x["ProductKey"];
                    delete x["ProductName"];
                    delete x["ProductSeoUrl"];
                    delete x["Product"];
                    delete x.SlaveID;
                    delete x.SlaveKey;
                    delete x.SlaveName;
                });
            }
            if (copyProduct.Manufacturers && copyProduct.Manufacturers.length) {
                copyProduct.Manufacturers.forEach(x => {
                    delete x["ProductID"];
                    delete x["ProductKey"];
                    delete x["ProductName"];
                    delete x["ProductSeoUrl"];
                    delete x["Product"];
                    delete x.SlaveID;
                    delete x.SlaveKey;
                    delete x.SlaveName;
                });
            }
            if (copyProduct.Stores && copyProduct.Stores.length) {
                copyProduct.Stores.forEach(x => {
                    delete x["ProductID"];
                    delete x["ProductKey"];
                    delete x["ProductName"];
                    delete x["ProductSeoUrl"];
                    delete x["Product"];
                    delete x.SlaveID;
                    delete x.SlaveKey;
                    delete x.SlaveName;
                });
            }
            if (copyProduct.Brands && copyProduct.Brands.length) {
                copyProduct.Brands.forEach(x => {
                    delete x["ProductID"];
                    delete x["ProductKey"];
                    delete x["ProductName"];
                    delete x["ProductSeoUrl"];
                    delete x["Product"];
                    delete x.SlaveID;
                    delete x.SlaveKey;
                    delete x.SlaveName;
                });
            }
            if (copyProduct.ProductAssociations && copyProduct.ProductAssociations.length) {
                copyProduct.ProductAssociations.forEach(x => {
                    delete x.MasterID;
                    delete x.MasterKey;
                    delete x.MasterName;
                    delete x.MasterSeoUrl;
                });
            }
            if (copyProduct.ProductRestrictions && copyProduct.ProductRestrictions.length) {
                copyProduct.ProductRestrictions.forEach(x => {
                    delete x.ProductID;
                    delete x.ProductKey;
                    delete x.ProductName;
                    delete x.ProductSeoUrl;
                    delete x.Product;
                });
            }
            // Create this copy as a product record
            this.cvApi.products.CreateProduct(copyProduct)
                .then(r => this.$state.go("inventory.products.detail",
                    { ID: r.data.Result },
                    <ng.ui.IStateOptions>{ reload: true, notify: true, inherit: false, location: true, relative: this.$state.$current }));
        }

        // Category Management Events
        categoryTree: kendo.ui.TreeView;
        categoryTreeOptions = <kendo.ui.TreeViewOptions>{
            checkboxes: <kendo.ui.TreeViewCheckboxes>{
                checkChildren: true,
                // NOTE: This is a required inline template to work with Kendo
                template: `<input type="checkbox" disabled ng-model="dataItem.IsSelfSelected" />`
            },
            // NOTE: This is a required inline template to work with Kendo
            template: `<button type="button" class="btn btn-xs btn-success"`
                       + ` ng-class="{'disabled': dataItem.IsSelfSelected}"`
                       + ` ng-disabled="dataItem.IsSelfSelected"`
                       + ` ng-click="ipeCtrl.addCategory(dataItem)">`
                       + `<i class="far fa-plus"></i><span class="sr-only">Add</span></button>`
                    + `&nbsp;{{dataItem.ID | zeroPadNumber: 6}}:&nbsp;{{dataItem.Name}}`
                    + `<span ng-if="dataItem.CustomKey">&nbsp;[{{dataItem.CustomKey}}]</span>`,
            loadOnDemand: true
        };
        selectedCategory: api.ProductCategorySelectorModel;
        editCategory: api.ProductCategoryModel;
        categorySelected(di/*: kendo.ui.TreeViewSelectEvent*/): void {
            this.selectedCategory = di;
        }
        categoryTreeData: kendo.data.HierarchicalDataSource;
        loadCategoryTree(): void {
            // NOTE: This function doesn't need setRunning
            // Get all Categories with no parents, we'll lazy-load children afterward
            this.categoryTreeData = new kendo.data.HierarchicalDataSource({
                transport: <kendo.data.DataSourceTransport>{
                    read: (options => {
                        this.cvApi.categories.GetCategoryTree(<api.GetCategoryTreeDto>{
                            Active: true,
                            ParentID: (options.data as api.ProductCategorySelectorModel).ID || null,
                            IncludeChildrenInResults: false,
                            DisregardParents: false,
                            ProductID: this.record.ID || null,
                            Sorts: [<api.Sort>{ dir: "asc", field: "Name", order: 0 }]
                        }).success(results => options.success(results));
                    }) as kendo.data.DataSourceTransportRead
                },
                schema: <kendo.data.HierarchicalDataSourceSchema>{
                    model: {
                        id: "ID",
                        hasChildren: "HasChildren"
                    }
                }
            });
        }
        addCategory(pcs: api.ProductCategorySelectorModel): void {
            if (!this.record.Categories) {
                this.record.Categories = [];
            }
            var existing = _.some(this.record.Categories, x => x.SlaveID === pcs.ID);
            if (existing) { return; } // Prevent Duplicates
            this.record.Categories.push(<api.ProductCategoryModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                CustomKey: null,
                SortOrder: null,
                ProductID: 0,
                ProductKey: null,
                SlaveID: pcs.ID,
                SlaveKey: pcs.CustomKey,
                Slave: <api.CategoryModel> {
                    ID: pcs.ID,
                    CustomKey: pcs.CustomKey,
                    Active: true,
                    CreatedDate: pcs.CreatedDate,
                    Name: pcs.Name,
                    TypeID: 1,
                    IsVisible: true,
                    IncludeInMenu: true,
                    HasChildren: false
                }
            });
            this.forms["Categories"].$setDirty();
        }
        removeCategory(index: number): void {
            this.record.Categories.splice(index, 1);
            this.forms["Categories"].$setDirty();
        }

        // Packaging Management Events
        switchPackage(isCustom: boolean): void {
            if (this.record.Package["IsDummy"]) {
                // This is a dummy (null), so we don't have another package to go off of
                if (isCustom) {
                    // Set up a new blank package where the user can fill in minimal data to save
                    this.record.PackageID = null;
                    this.record.Package = this.createEmptyPackageModel("Package");
                    delete this.record.PackageKey;
                    delete this.record.PackageName;
                } else {
                    // Match the package with the record from the dropdown
                    this.record.Package = _.find(this.packages, x => x.ID === this.record.PackageID);
                    delete this.record.PackageKey;
                    delete this.record.PackageName;
                }
            } else {
                // This is not a dummy (null), so we have a package to work from
                if (isCustom) {
                    const existing = _.find(this.packages, x => x.ID === this.record.PackageID);
                    this.record.PackageID = null;
                    this.record.Package = this.copyExistingPackageModelAsCustom("Package", existing);
                    delete this.record.PackageKey;
                    delete this.record.PackageName;
                } else {
                    // Match the package with the record from the dropdown
                    this.record.Package = _.find(this.packages, x => x.ID === this.record.PackageID);
                    if (!this.record.Package || this.record.Package.IsCustom) {
                        this.record.PackageID = -1;
                        this.record.Package = this.noPackageObject;
                        delete this.record.PackageKey;
                        delete this.record.PackageName;
                    }
                    delete this.record.PackageKey;
                    delete this.record.PackageName;
                }
            }
        }
        switchMasterPack(isCustom: boolean): void {
            if (!this.record.MasterPack || this.record.MasterPack["IsDummy"]) {
                // This is a dummy (null), so we don't have another package to go off of
                if (isCustom) {
                    // Set up a new blank package where the user can fill in minimal data to save
                    this.record.MasterPackID = null;
                    this.record.MasterPack = this.createEmptyPackageModel("Master Pack");
                    delete this.record.MasterPackKey;
                    delete this.record.MasterPackName;
                } else {
                    // Match the package with the record from the dropdown
                    this.record.MasterPack = _.find(this.packages, x => x.ID === this.record.MasterPackID);
                    delete this.record.MasterPackKey;
                    delete this.record.MasterPackName;
                }
            } else {
                // This is not a dummy (null), so we have a package to work from
                if (isCustom) {
                    const existing = _.find(this.packages, x => x.ID === this.record.MasterPackID);
                    this.record.MasterPackID = null;
                    this.record.MasterPack = this.copyExistingPackageModelAsCustom("Master Pack", existing);
                    delete this.record.MasterPackKey;
                    delete this.record.MasterPackName;
                } else {
                    // Match the package with the record from the dropdown
                    this.record.MasterPack = _.find(this.packages, x => x.ID === this.record.MasterPackID);
                    if (!this.record.MasterPack || this.record.MasterPack.IsCustom) {
                        this.record.MasterPackID = -1;
                        this.record.MasterPack = this.noMasterPackObject;
                        delete this.record.MasterPackKey;
                        delete this.record.MasterPackName;
                    }
                    delete this.record.MasterPackKey;
                    delete this.record.MasterPackName;
                }
            }
        }
        switchPallet(isCustom: boolean): void {
            if (!this.record.Pallet || this.record.Pallet["IsDummy"]) {
                // This is a dummy (null), so we don't have another package to go off of
                if (isCustom) {
                    // Set up a new blank package where the user can fill in minimal data to save
                    this.record.PalletID = null;
                    this.record.Pallet = this.createEmptyPackageModel("Pallet");
                    delete this.record.PalletKey;
                    delete this.record.PalletName;
                } else {
                    // Match the package with the record from the dropdown
                    this.record.Pallet = _.find(this.packages, x => x.ID === this.record.PalletID);
                    delete this.record.PalletKey;
                    delete this.record.PalletName;
                }
            } else {
                // This is not a dummy (null), so we have a package to work from
                if (isCustom) {
                    const existing = _.find(this.packages, x => x.ID === this.record.PalletID);
                    this.record.PalletID = null;
                    this.record.Pallet = this.copyExistingPackageModelAsCustom("Pallet", existing);
                    delete this.record.PalletKey;
                    delete this.record.PalletName;
                } else {
                    // Match the package with the record from the dropdown
                    this.record.Pallet = _.find(this.packages, x => x.ID === this.record.PalletID);
                    if (!this.record.Pallet || this.record.Pallet.IsCustom) {
                        this.record.PalletID = -1;
                        this.record.Pallet = this.noPalletObject;
                        delete this.record.PalletKey;
                        delete this.record.PalletName;
                    }
                    delete this.record.PalletKey;
                    delete this.record.PalletName;
                }
            }
        }
        private createEmptyPackageModel(typeKey: string): api.PackageModel {
            return  <api.PackageModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: `Custom ${typeKey} for ${this.record.Name}`,
                Description: null,
                Weight: 0,
                WeightUnitOfMeasure: "lbs",
                Width: 0,
                WidthUnitOfMeasure: "in",
                Height: 0,
                HeightUnitOfMeasure: "in",
                Depth: 0,
                DepthUnitOfMeasure: "in",
                DimensionalWeight: 0,
                DimensionalWeightUnitOfMeasure: "lbs",
                IsCustom: true,
                PackageQuantity: null,
                TypeID: 0,
                TypeKey: typeKey
            };
        }
        private copyExistingPackageModelAsCustom(typeKey: string, existing: api.PackageModel): api.PackageModel {
            return  <api.PackageModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                Name: `Custom ${typeKey} for ${this.record.Name}`,
                Description: null,
                Weight: existing && existing.Weight,
                WeightUnitOfMeasure: existing && existing.WeightUnitOfMeasure,
                Width: existing && existing.Width,
                WidthUnitOfMeasure: existing && existing.WidthUnitOfMeasure,
                Height: existing && existing.Height,
                HeightUnitOfMeasure: existing && existing.HeightUnitOfMeasure,
                Depth: existing && existing.Depth,
                DepthUnitOfMeasure: existing && existing.DepthUnitOfMeasure,
                DimensionalWeight: existing && existing.DimensionalWeight,
                DimensionalWeightUnitOfMeasure: existing && existing.DimensionalWeightUnitOfMeasure,
                IsCustom: true,
                PackageQuantity: 1,
                TypeID: existing && existing.TypeID,
                TypeKey: existing && existing.TypeKey || typeKey
            };
        }

        // Price Management Events
        rawPrices: api.RawProductPricesModel;
        loadPricesForEditing(): void {
            // NOTE: This function doesn't need setRunning
            if (!this.record || !this.record.ID) {
                this.rawPrices = { ID: 0, PriceBase: 0 };
                return;
            }
            this.cvApi.pricing.GetRawPricesForProduct(this.record.ID).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    console.error(r && r.data);
                    return;
                }
                this.rawPrices = r.data.Result;
            });
        }
        savePrices(id: number): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                this.rawPrices.ID = this.record.ID || id;
                this.cvApi.pricing.UpdateRawPricesForProduct(this.rawPrices)
                    .then(resolve)
                    .catch(reject);
            });
        }

        // Price Point Management Events
        addPricePoint(): void {
            if (!this.cefConfig.featureSet.pricing.pricePoints.enabled) {
                this.productPricePoints = null;
                return;
            }
            if (!this.productPricePoints) { this.productPricePoints = []; }
            this.productPricePoints.push(<api.ProductPricePointModel>{
                // Base Properties
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: this.record.ID,
                MasterKey: this.record.CustomKey,
                SlaveID: 0,
                // ProductPricePoint Properties
                MinQuantity: 1,
                MaxQuantity: 2147483647, // int.MaxValue
                PercentDiscount: null,
                Price: 1,
                UnitOfMeasure: "Each"
            });
            this.forms["Pricing"].$setDirty();
        }
        addAllPricePoints(): void {
            if (!this.cefConfig.featureSet.pricing.pricePoints.enabled) {
                this.productPricePoints = null;
                return;
            }
            if (!this.productPricePoints) {
                this.productPricePoints = [];
            }
            this.pricePoints.forEach(value => {
                var found = _.some(this.productPricePoints, x => x.SlaveID === value.ID);
                if (found) { return; }
                this.productPricePoints.push(<api.ProductPricePointModel>{
                    // Base Properties
                    CustomKey: null,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    SerializableAttributes: { },
                    // IAmARelationshipTable Properties
                    MasterID: this.record.ID,
                    MasterKey: this.record.CustomKey,
                    SlaveID: value.ID,
                    SlaveKey: value.CustomKey,
                    // ProductPricePoint Properties
                    MinQuantity: 1,
                    MaxQuantity: 2147483647, // int.MaxValue
                    PercentDiscount: null,
                    Price: 1,
                    UnitOfMeasure: "Each"
                });
            });
            this.forms["Pricing"].$setDirty();
        }
        removePricePoint(index: number): void {
            this.productPricePoints.splice(index, 1);
            this.forms["Pricing"].$setDirty();
        }

        // Inventory Management Events
        rawInventory: api.CalculatedInventory;
        loadInventoryForEditing(): void {
            // NOTE: This function doesn't need setRunning
            if (!this.cefConfig.featureSet.inventory.enabled) {
                return;
            }
            if (!this.record || !this.record.ID) {
                this.rawInventory = <api.CalculatedInventory>{
                    ProductID: 0,
                    // These are not set by this but required on the model
                    IsDiscontinued: null,
                    IsUnlimitedStock: null,
                    AllowBackOrder: null,
                    AllowPreSale: null,
                };
                if (this.cefConfig.featureSet.inventory.advanced.enabled) {
                    this.rawInventory.RelevantLocations = [];
                }
                return;
            }
            this.cvApi.providers.CalculateInventory(this.record.ID).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    console.error(r && r.data);
                    return;
                }
                this.rawInventory = r.data.Result;
            });
        }
        saveInventory(id: number): ng.IPromise<void> {
            if (!this.cefConfig.featureSet.inventory.enabled) {
                return this.$q.resolve();
            }
            return this.$q((resolve, reject) => {
                if (!this.cefConfig.featureSet.inventory.advanced.enabled) {
                    const dto = <api.UpdateInventoryForProductDto> {
                        ID: this.record.ID || id,
                        Quantity: this.rawInventory.QuantityPresent,
                        QuantityAllocated: this.rawInventory.QuantityAllocated,
                    };
                    if (this.cefConfig.featureSet.inventory.preSale.enabled) {
                        dto.QuantityPreSold = this.rawInventory.QuantityPreSold;
                    }
                    this.cvApi.providers.UpdateInventoryForProduct(dto)
                        .then(resolve)
                        .catch(reject);
                    return;
                }
                if (!this.rawInventory.RelevantLocations) {
                    this.rawInventory.RelevantLocations = [];
                }
                // TODO: Filter to $dirty locations only, we don't need to call
                // update for every record that hasn't changed
                this.$q.all(
                    this.rawInventory.RelevantLocations.map(x => {
                        const dto = <api.UpdateInventoryForProductDto> {
                            ID: this.record.ID || id,
                            RelevantHash: x.Hash,
                            RelevantLocationID: x.SlaveID,
                            Quantity: x.Quantity,
                            QuantityAllocated: x.QuantityAllocated,
                        };
                        if (this.cefConfig.featureSet.inventory.preSale.enabled) {
                            dto.QuantityPreSold = x.QuantityPreSold;
                        }
                        return this.cvApi.providers.UpdateInventoryForProduct(dto);
                    }))
                    .then(resolve)
                    .catch(reject);
            });
        }
        // PILS Management Events
        addInventory(): void {
            if (!this.cefConfig.featureSet.inventory.advanced.enabled) {
                delete this.rawInventory.RelevantLocations;
                return;
            }
            let inventoryID = 0;
            if (this.inventories.length > 0) {
                inventoryID = this.inventories[0].ID;
            }
            if (!this.rawInventory.RelevantLocations) {
                this.rawInventory.RelevantLocations = [];
            }
            this.rawInventory.RelevantLocations.push(<api.ProductInventoryLocationSectionModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: this.record.ID,
                MasterKey: this.record.CustomKey,
                SlaveID: inventoryID,
                SlaveName: null,
                // PILS Properties
                Quantity: 0,
                QuantityAllocated: null,
                QuantityBroken: null,
                // Convenience Properties
                InventoryLocationID: 0,
                InventoryLocationName: null
            });
            this.forms["Inventory"].$setDirty();
        }
        removeInventory(index: number): void {
            if (!this.cefConfig.featureSet.inventory.advanced.enabled) {
                delete this.rawInventory.RelevantLocations;
                return;
            }
            this.rawInventory.RelevantLocations.splice(index, 1);
            this.forms["Inventory"].$setDirty();
        }
        // Kit Inventory Management Events
        breakKit(): void {
            if (!this.cefConfig.featureSet.inventory.advanced.enabled) {
                delete this.rawInventory.RelevantLocations;
                return;
            }
            let inventoryID = 0;
            if (this.inventories.length > 0) {
                inventoryID = this.inventories[0].ID;
            }
            if (!this.rawInventory.RelevantLocations) {
                this.rawInventory.RelevantLocations = [];
            }
            this.rawInventory.RelevantLocations.push(<api.ProductInventoryLocationSectionModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                InventoryLocationSectionID: inventoryID,
                InventoryLocationSectionName: null,
                InventoryLocationID: 0,
                InventoryLocationName: null,
                ProductID: this.record.ID,
                ProductKey: this.record.CustomKey,
                Quantity: 0
            });
            this.forms["Inventory"].$setDirty();
        }
        reassembleKit(): void {
            if (!this.cefConfig.featureSet.inventory.advanced.enabled) {
                delete this.rawInventory.RelevantLocations;
                return;
            }
            let inventoryID = 0;
            if (this.inventories.length > 0) {
                inventoryID = this.inventories[0].ID;
            }
            if (!this.rawInventory.RelevantLocations) {
                this.rawInventory.RelevantLocations = [];
            }
            this.rawInventory.RelevantLocations.push(<api.ProductInventoryLocationSectionModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                InventoryLocationSectionID: inventoryID,
                InventoryLocationSectionName: null,
                InventoryLocationID: 0,
                InventoryLocationName: null,
                ProductID: this.record.ID,
                ProductKey: this.record.CustomKey,
                Quantity: 0
            });
            this.forms["Inventory"].$setDirty();
        }
        assembleKit(): void {
            if (!this.cefConfig.featureSet.inventory.advanced.enabled) {
                delete this.rawInventory.RelevantLocations;
                return;
            }
            let inventoryID = 0;
            if (this.inventories.length > 0) {
                inventoryID = this.inventories[0].ID;
            }
            if (!this.rawInventory.RelevantLocations) {
                this.rawInventory.RelevantLocations = [];
            }
            this.rawInventory.RelevantLocations.push(<api.ProductInventoryLocationSectionModel>{
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                CustomKey: null,
                InventoryLocationSectionID: inventoryID,
                InventoryLocationSectionName: null,
                InventoryLocationID: 0,
                InventoryLocationName: null,
                ProductID: this.record.ID,
                Quantity: 0
            });
            this.forms["Inventory"].$setDirty();
        }

        // Vendor Management Events
        addVendor(): void {
            if (!this.cefConfig.featureSet.vendors.enabled) {
                this.record.Vendors = null;
                return;
            }
            let vendorID = 0;
            if (!this.vendors) {
                this.vendors = [];
            }
            if (this.vendors.length > 0) {
                vendorID = this.vendors[0].ID;
            }
            if (!this.record.Vendors) {
                this.record.Vendors = [];
            }
            this.record.Vendors.push(<api.VendorProductModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: vendorID,
                SlaveID: this.record.ID,
                SlaveKey: this.record.CustomKey,
                // VendorProduct Properties
                Bin: null,
                MinimumInventory: null,
                MaximumInventory: null,
                InventoryCount: null,
                CostMultiplier: null,
                ListedPrice: null,
                ActualCost: null
            });
            this.forms["Vendors"].$setDirty();
        }
        removeVendor(index: number): void {
            if (!this.cefConfig.featureSet.vendors.enabled) {
                this.record.Vendors = null;
                return;
            }
            this.record.Vendors.splice(index, 1);
            this.forms["Vendors"].$setDirty();
        }

        // Manufacturer Management Events
        addManufacturer(): void {
            if (!this.cefConfig.featureSet.manufacturers.enabled) {
                this.record.Manufacturers = null;
                return;
            }
            let manufacturerID = 0;
            if (this.manufacturers.length > 0) { manufacturerID = this.manufacturers[0].ID; }
            if (!this.record.Manufacturers) {
                this.record.Manufacturers = [];
            }
            this.record.Manufacturers.push(<api.ManufacturerProductModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: manufacturerID,
                SlaveID: this.record.ID,
                SlaveKey: this.record.CustomKey
            });
            this.forms["Manufacturers"].$setDirty();
        }
        removeManufacturer(index: number): void {
            if (!this.cefConfig.featureSet.manufacturers.enabled) {
                this.record.Manufacturers = null;
                return;
            }
            this.record.Manufacturers.splice(index, 1);
            this.forms["Manufacturers"].$setDirty();
        }

        // Store Management Events
        addStore(): void {
            if (!this.cefConfig.featureSet.stores.enabled) {
                this.record.Stores = null;
                return;
            }
            let storeID = 0;
            if (this.stores.length > 0) { storeID = this.stores[0].ID; }
            if (!this.record.Stores) {
                this.record.Stores = [];
            }
            this.record.Stores.push(<api.StoreProductModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: storeID,
                SlaveID: this.record.ID,
                SlaveKey: this.record.CustomKey,
                // StoreProduct Properies
                IsVisibleIn: true,
                PriceBase: null,
                PriceMsrp: null,
                PriceReduction: null,
                PriceSale: null
            });
            this.forms["Stores"].$setDirty();
        }
        removeStore(index: number): void {
            if (!this.cefConfig.featureSet.stores.enabled) {
                this.record.Stores = null;
                return;
            }
            this.record.Stores.splice(index, 1);
            this.forms["Stores"].$setDirty();
        }

        // Brand Management Events
        addBrand(): void {
            if (!this.cefConfig.featureSet.brands.enabled) {
                this.record.Brands = null;
                return;
            }
            let brandID = 0;
            if (this.brands.length > 0) { brandID = this.brands[0].ID; }
            if (!this.record.Brands) {
                this.record.Brands = [];
            }
            this.record.Brands.push(<api.BrandProductModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: brandID,
                SlaveID: this.record.ID,
                SlaveKey: this.record.CustomKey,
                // BrandProduct Properies
                IsVisibleIn: true,
                PriceBase: null,
                PriceMsrp: null,
                PriceReduction: null,
                PriceSale: null
            });
            this.forms["Brands"].$setDirty();
        }
        removeBrand(index: number): void {
            if (!this.cefConfig.featureSet.brands.enabled) {
                this.record.Brands = null;
                return;
            }
            this.record.Brands.splice(index, 1);
            this.forms["Brands"].$setDirty();
        }

        // Product Restriction Management Events
        addRestriction(): void {
            if (!this.cefConfig.featureSet.products.restrictions.enabled) {
                this.record.ProductRestrictions = null;
                return;
            }
            if (!this.record.ProductRestrictions) {
                this.record.ProductRestrictions = [];
            }
            this.record.ProductRestrictions.push(<api.ProductRestrictionModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // ProductRestriction Properties
                ProductID: this.record.ID,
                CanPurchaseDomestically: true,
                CanPurchaseInternationally: true,
                CanPurchaseIntraRegion: true,
                CanShipDomestically: true,
                CanShipInternationally: true,
                CanShipIntraRegion: true,
            });
            this.forms["Restrictions"].$setDirty();
        }
        removeRestriction(index: number): void {
            if (!this.cefConfig.featureSet.products.restrictions.enabled) {
                this.record.ProductRestrictions = null;
                return;
            }
            this.record.ProductRestrictions.splice(index, 1);
            this.forms["Restrictions"].$setDirty();
        }

        // Product Notification Management Events
        addNotification(): void {
            if (!this.cefConfig.featureSet.products.notifications.enabled) {
                this.record.ProductNotifications = null;
                return;
            }
            if (!this.record.ProductNotifications) {
                this.record.ProductNotifications = [];
            }
            this.record.ProductNotifications.push(<api.ProductNotificationModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // Nameablebase Properties
                Name: null,
                Description: null,
                // Related Objects
                ProductID: this.record.ID,
            });
            this.forms["Notifications"].$setDirty();
        }
        removeNotification(index: number): void {
            if (!this.cefConfig.featureSet.products.notifications.enabled) {
                this.record.ProductNotifications = null;
                return;
            }
            this.record.ProductNotifications.splice(index, 1);
            this.forms["Notifications"].$setDirty();
        }

        // Associated Product Management Events
        showAddAssociatedProductsPanel(): void {
            this.display.ShowAssociatedProductPanel = true;
        }
        hideAddAssociatedProductsPanel(): void {
            this.display.ShowAssociatedProductPanel = false;
        }
        searchAssociatedProducts(): void {
            this.cvApi.products.GetProducts({
                Active: true,
                AsListing: true,
                Name: this.associationSearchParams.ProductName,
                CustomKey: this.associationSearchParams.ProductCode,
                Paging: <api.Paging>{ Size: 20, StartIndex: 1 },
                Sorts: [
                    { field: "Name", order: 0, dir: "asc" },
                    { field: "CustomKey", order: 1, dir: "asc" },
                    { field: "ID", order: 2, dir: "asc" }
                ]
            }).then(r => this.associatedProducts = r.data.Results);
        }
        addAssociatedProducts(): void {
            if (!this.record.ProductAssociations) {
                this.record.ProductAssociations = [];
            }
            this.associatedProducts.forEach(p => {
                if (p.Selected) {
                    this.addAssociatedProduct(p.ID);
                    p.Selected = false;
                }
            });
            this.hideAddAssociatedProductsPanel();
            this.forms["Associations"].$setDirty();
        }
        addAssociatedProduct(id: number): void {
            var defaultTypeID = null;
            if (this.associationTypes.length > 0) {
                defaultTypeID = this.associationTypes[0].ID;
            }
            this.associatedProducts.filter(x => x.ID === id).forEach(p => {
                this.record.ProductAssociations.push(<api.ProductAssociationModel>{
                    // Base Properties
                    ID: 0,
                    Active: true,
                    CustomKey: null,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    SerializableAttributes: { },
                    // IHaveAType Properties
                    TypeID: defaultTypeID,
                    Type: null,
                    // IAmARelationshipTable Properties
                    MasterID: this.record.ID,
                    MasterKey: this.record.CustomKey,
                    SlaveID: id,
                    SlaveKey: p.CustomKey,
                    SlaveName: p.Name,
                    // ProductAssociation Properies
                    Quantity: null,
                    UnitOfMeasure: null,
                    SortOrder: null
                });
            });
            this.forms["Associations"].$setDirty();
        }
        removeAssociatedProduct(index: number): void {
            this.record.ProductAssociations.splice(index, 1);
            this.forms["Associations"].$setDirty();
        }
        updateAssocType($index: number): void {
            if (angular.isUndefined($index) || $index === null || $index < 0) {
                return;
            }
            const type = _.find(this.associationTypes,
                x => x.ID === this.record.ProductAssociations[$index].TypeID);
            this.record.ProductAssociations[$index].Type = type;
            this.record.ProductAssociations[$index].TypeKey = type.CustomKey;
            this.record.ProductAssociations[$index].TypeName = type.Name;
            this.record.ProductAssociations[$index].TypeDisplayName = type.DisplayName;
            this.record.ProductAssociations[$index].TypeSortOrder = type.SortOrder;
            this.record.ProductAssociations[$index].TypeTranslationKey = type.TranslationKey;
        }

        setAvailableDates(): void {
            this.record.AvailableStartDate = this.$scope.AvailableStartDate;
            this.record.AvailableEndDate = this.$scope.AvailableEndDate;
        }

        // Image & Document Management Events
        removeImageNew(index: number): void {
            this.record.Images.splice(index, 1);
            this.forms["Images"].$setDirty();
        }
        removeFileNew(index: number): void {
            this.record.StoredFiles.splice(index, 1);
            this.forms["StoredFiles"].$setDirty();
        }
        removeDownload(index: number): void {
            this.record.ProductDownloads.splice(index, 1);
            this.forms["ProductDownloads"].$setDirty();
        }

        ////// Versioning
        ////loadingVersion: boolean;
        ////haveVersionLoaded: boolean;
        ////versionLoadedToUI: string | number;
        ////versions: api.RecordVersionModel[] = [];
        ////versionIDs: number[] = [];
        ////selectedRecordVersion: api.RecordVersionModel;
        ////selectedRecordVersionID: number;
        ////get selectedRecordVersionName(): string {
        ////    return this.selectedRecordVersion && this.selectedRecordVersion.Name;
        ////}
        ////originallyLoadedVersion: string;
        ////versionTypes: api.TypeModel[] = [];
        ////openVersionManager(): void {
        ////    this.record["version"] = this.versionLoadedToUI;
        ////    this.cvProductVersionSelectorModalFactory(this.record).then(result => {
        ////        this.record = result.record;
        ////        this.versionLoadedToUI = result.version.Name;
        ////        this.selectedRecordVersionID = result.version.ID;
        ////        this.selectedRecordVersion = result.version;
        ////        this.versions.forEach(x => {
        ////            x["currentLoaded"] = x.ID === result.version.ID;
        ////        });
        ////    });
        ////}
        ////handleVersions(
        ////    resolve: (r: api.ProductModel) => void,
        ////    reject: (r?: any) => void,
        ////    result: api.ProductModel)
        ////    : void {
        ////    this.consoleDebug("handleVersions:product:0");
        ////    if (!result.ID) {
        ////        this.consoleDebug("handleVersions:product:1");
        ////        this.versions = [];
        ////        this.consoleDebug("handleVersions:product:2");
        ////        resolve(result);
        ////        this.consoleDebug("handleVersions:product:3");
        ////        return;
        ////    }
        ////    this.consoleDebug("handleVersions:product:4");
        ////    this.cvApi.structure.GetRecordVersions({
        ////        Active: true,
        ////        AsListing: true,
        ////        RecordID: result.ID,
        ////        Sorts: [<api.Sort>{ dir: "asc", field: "ID" }]
        ////    }).then(rv => {
        ////        this.consoleDebug("handleVersions:product:5");
        ////        const promises = [];
        ////        rv.data.Results.forEach(x => promises.push(this.cvApi.structure.GetRecordVersionByID(x.ID)));
        ////        this.consoleDebug("handleVersions:product:5.-1");
        ////        this.$q.all(promises)
        ////            .then((rarr: ng.IHttpPromiseCallbackArg<api.RecordVersionModel>[]) => {
        ////                this.consoleDebug("handleVersions:product:5.0");
        ////                this.versions = rarr.map(x => x.data);
        ////                const serializedInput = productModelToVersionJson(result);
        ////                this.originallyLoadedVersion = result["version"];
        ////                let isInitial = true;
        ////                //
        ////                this.consoleDebug("handleVersions:product:5.1");
        ////                if (this.$stateParams.versionID > 0) {
        ////                    this.consoleDebug("handleVersions:product:5.2");
        ////                    isInitial = false;
        ////                    const existing = _.find(this.versions, x => x.ID === this.$stateParams.versionID);
        ////                    this.consoleDebug("handleVersions:product:5.3");
        ////                    if (existing) {
        ////                        this.consoleDebug("handleVersions:product:5.4");
        ////                        this.selectedRecordVersion = existing;
        ////                        this.selectedRecordVersionID = this.selectedRecordVersion.ID;
        ////                        existing["currentLoaded"] = true;
        ////                        this.consoleDebug("handleVersions:product:5.5");
        ////                        result = mergeProductModelWithVersion(
        ////                            result,
        ////                            this.selectedRecordVersion.Name,
        ////                            this.selectedRecordVersion.SerializedRecord);
        ////                        this.consoleDebug("handleVersions:product:5.6");
        ////                        this.record = result;
        ////                        this.consoleDebug("handleVersions:product:5.7");
        ////                        this.versionLoadedToUI = this.selectedRecordVersion.Name;
        ////                        this.consoleDebug("handleVersions:product:5.8");
        ////                        resolve(result);
        ////                        this.consoleDebug("handleVersions:product:5.9");
        ////                        return;
        ////                    }
        ////                }
        ////                this.consoleDebug("handleVersions:product:5.10");
        ////                //
        ////                let found = result["version"]
        ////                    && _.find(this.versions, x => x.Name === result["version"]);
        ////                let newName = "Initial Version " + this.$filter("date")(new Date(), "yyyy MMM dd hh:mm a");
        ////                let matchedByInput = false;
        ////                if (!found) {
        ////                    this.consoleDebug("handleVersions:product:6");
        ////                    const deserialiedInput = angular.fromJson(serializedInput);
        ////                    found = _.find(this.versions,
        ////                        x => angular.equals(angular.fromJson(x.SerializedRecord), deserialiedInput));
        ////                    if (found && !this.originallyLoadedVersion) {
        ////                        this.consoleDebug("handleVersions:product:7");
        ////                        this.originallyLoadedVersion = found.Name;
        ////                        matchedByInput = true;
        ////                    }
        ////                }
        ////                this.consoleDebug("handleVersions:product:8");
        ////                if (found && !matchedByInput) {
        ////                    this.consoleDebug("handleVersions:product:9");
        ////                    // Grab the name and tell the user it's been modified since this record
        ////                    // was created
        ////                    newName = found.Name + " (Modified)";
        ////                    isInitial = false;
        ////                    // Clear the old record that was found so the UI makes a new one
        ////                    found = null;
        ////                    this.consoleDebug("handleVersions:product:10");
        ////                }
        ////                this.consoleDebug("handleVersions:product:11");
        ////                if (found) {
        ////                    this.consoleDebug("handleVersions:product:12");
        ////                    this.selectedRecordVersion = found;
        ////                    this.selectedRecordVersionID = this.selectedRecordVersion.ID;
        ////                    found["currentPublished"] = true;
        ////                    found["currentLoaded"] = true;
        ////                    isInitial = false;
        ////                    this.consoleDebug("handleVersions:product:13");
        ////                    resolve(result);
        ////                    this.consoleDebug("handleVersions:product:14");
        ////                    return;
        ////                }
        ////                this.consoleDebug("handleVersions:product:15");
        ////                if (this.versions.length) {
        ////                    this.consoleDebug("handleVersions:product:16");
        ////                    newName = "New Version " + this.$filter("date")(new Date(), "yyyy MMM dd hh:mm a");
        ////                    isInitial = false;
        ////                    this.consoleDebug("handleVersions:product:17");
        ////                }
        ////                this.consoleDebug("handleVersions:product:18");
        ////                (this.versionTypes && this.versionTypes.length
        ////                    ? this.$q(resolve => resolve({
        ////                        data: <api.RecordVersionPagedResults>{ Results: this.versionTypes }
        ////                    }))
        ////                    : this.cvApi.structure.GetRecordVersionTypes({
        ////                        Active: true,
        ////                        AsListing: true,
        ////                        Paging: <api.Paging>{ Size: 500, StartIndex: 1 }
        ////                    }))
        ////                .then((rt: ng.IHttpPromiseCallbackArg<api.RecordVersionPagedResults>) => {
        ////                    this.consoleDebug("handleVersions:product:19");
        ////                    if (!this.versionTypes || !this.versionTypes.length) {
        ////                        this.versionTypes = rt.data.Results;
        ////                    }
        ////                    this.consoleDebug("handleVersions:product:20");
        ////                    this.selectedRecordVersion = <api.RecordVersionModel>{
        ////                        // Base Properties
        ////                        ID: 0,
        ////                        CustomKey: null,
        ////                        Active: true,
        ////                        CreatedDate: new Date(),
        ////                        UpdatedDate: null,
        ////                        Hash: null,
        ////                        SerializableAttributes: new api.SerializableAttributesDictionary(),
        ////                        // NameableBase Properties
        ////                        Name: newName,
        ////                        Description: null,
        ////                        // IHaveATypeBase Properties
        ////                        TypeID: this.versionTypes[0].ID,
        ////                        Type: this.versionTypes[0],
        ////                        TypeKey: this.versionTypes[0].CustomKey,
        ////                        TypeName: this.versionTypes[0].Name,
        ////                        TypeDisplayName: this.versionTypes[0].DisplayName,
        ////                        TypeTranslationKey: this.versionTypes[0].TranslationKey,
        ////                        TypeSortOrder: this.versionTypes[0].SortOrder,
        ////                        // IAmFilterableByBrands Properties
        ////                        BrandID: 0,
        ////                        Brand: null,
        ////                        BrandKey: null,
        ////                        BrandName: null,
        ////                        // IAmFilterableByStores Properties
        ////                        StoreID: 0,
        ////                        Store: null,
        ////                        StoreKey: null,
        ////                        StoreName: null,
        ////                        StoreSeoUrl: null,
        ////                        // RecordVersion Properties
        ////                        IsDraft: false,
        ////                        OriginalPublishDate: result.CreatedDate,
        ////                        MostRecentPublishDate: result.UpdatedDate || result.CreatedDate,
        ////                        RecordID: result.ID,
        ////                        SerializedRecord: productModelToVersionJson(result),
        ////                        // Related Objects
        ////                        PublishedByUserID: 0,
        ////                        PublishedByUser: null,
        ////                        PublishedByUserKey: null,
        ////                    };
        ////                    this.consoleDebug("handleVersions:product:21");
        ////                    if (!isInitial) {
        ////                        this.consoleDebug("handleVersions:product:22");
        ////                        this.selectedRecordVersionID = this.selectedRecordVersion.ID;
        ////                        this.consoleDebug("handleVersions:product:23");
        ////                        resolve(result);
        ////                        this.consoleDebug("handleVersions:product:24");
        ////                        return;
        ////                    }
        ////                    this.consoleDebug("handleVersions:product:25");
        ////                    // Always force an initial version if it's not detected
        ////                    this.createVersion(result, this.selectedRecordVersion).then(r2 => {
        ////                        this.consoleDebug("handleVersions:product:26");
        ////                        this.handleVersions(resolve, reject, r2); // Will eventually call resolve
        ////                        this.consoleDebug("handleVersions:product:27");
        ////                    }).catch(reason4 => reject(reason4));
        ////                }).catch(reason3 => reject(reason3));
        ////            }).catch(reason2 => reject(reason2));
        ////        this.consoleDebug("handleVersions:product:5.-2");
        ////    }).catch(reason1 => reject(reason1));
        ////}
        ////createVersion(record: api.ProductModel, version: api.RecordVersionModel): ng.IPromise<api.ProductModel> {
        ////    this.consoleDebug("createVersion:product:0");
        ////    return this.$q((resolve, reject) => {
        ////        this.consoleDebug("createVersion:product:1");
        ////        this.setRunning(this.$translate("ui.admin.versioning.CreatingAVersion.Ellipses"));
        ////        this.consoleDebug("createVersion:product:2");
        ////        ////this.cvApi.structure.CheckRecordVersionExistsByName({
        ////        ////    Name: this.selectedRecordVersion.Name
        ////        ////}).then(r1 => {
        ////        ////    if (r1.data) {
        ////        ////        this.fullReject(reject, "This Version Name already exists");
        ////        ////        return;
        ////        ////    }
        ////            delete version.ID; // Clear the ID
        ////            delete version.CustomKey; // Clear the CustomKey
        ////            this.cvApi.structure.CreateRecordVersion(version).then(r2 => {
        ////                this.consoleDebug("createVersion:product:3");
        ////                if (!r2 || !r2.data) {
        ////                    this.consoleDebug("createVersion:product:4");
        ////                    this.fullReject(reject, "No data returned from create version call");
        ////                    this.consoleDebug("createVersion:product:5");
        ////                    return;
        ////                }
        ////                this.consoleDebug("createVersion:product:6");
        ////                this.selectedRecordVersion = r2.data;
        ////                this.selectedRecordVersionID = this.selectedRecordVersion.ID;
        ////                record["version"] = this.selectedRecordVersion.Name;
        ////                this.consoleDebug("createVersion:product:7");
        ////                this.finishRunning();
        ////                resolve(record);
        ////                this.consoleDebug("createVersion:product:8");
        ////            }).catch(reason2 => this.fullReject(reject, reason2));
        ////        ////}).catch(reason => this.fullReject(reject, reason));
        ////    });
        ////}
        ////loadVersion(version: api.RecordVersionModel): void {
        ////    //* TODO: Versioning
        ////    if (this.dirtyForms()) {
        ////        this.cvConfirmModalFactory(
        ////                this.$translate("ui.admin.versioning.AreYouSureYouWanToLoadADifferentVersion.Confirmation"))
        ////            .then(result => {
        ////                if (result) {
        ////                    this.loadVersionInner(version);
        ////                }
        ////            });
        ////        return;
        ////    }
        ////    //*/
        ////    this.loadVersionInner(version);
        ////}
        ////private loadVersionInner(version: api.RecordVersionModel): void {
        ////    this.consoleDebug("loadVersionInner:product:0");
        ////    this.setRunning("Load version inner");
        ////    this.consoleDebug("loadVersionInner:product:1");
        ////    this.cvApi.structure.GetRecordVersionByID(version.ID).then(r => {
        ////        if (!r || !r.data || !r.data.SerializedRecord) {
        ////            this.finishRunning(true, "Could not load full version record from server");
        ////            return;
        ////        }
        ////        this.selectedRecordVersion = r.data;
        ////        this.selectedRecordVersionID = r.data.ID;
        ////        this.consoleDebug("loadVersionInner:product:2");
        ////        this.cvApi.products.GetProductFull(this.record.ID).then(r => {
        ////            this.consoleDebug("loadVersionInner:product:3");
        ////            const merged = mergeProductModelWithVersion(
        ////                r.data,
        ////                this.selectedRecordVersion.Name,
        ////                this.selectedRecordVersion.SerializedRecord)
        ////            this.consoleDebug("loadVersionInner:product:4");
        ////            // Finish and exit out
        ////            this.record = merged;
        ////            this.consoleDebug("loadVersionInner:product:5");
        ////            this.loadRecordActionAfterSuccess(this.record).then(() => {
        ////                this.consoleDebug("loadVersionInner:product:6");
        ////                this.finishRunning();
        ////                this.consoleDebug("loadVersionInner:product:7");
        ////            }).catch(reason => this.finishRunning(true, reason));
        ////        }).catch(reason => this.finishRunning(true, reason));
        ////    }).catch(reason => this.finishRunning(true, reason));
        ////}
        ////loadVersionInMgr(version: api.RecordVersionModel): void {
        ////    this.record["version"] = this.versionLoadedToUI;
        ////    this.cvProductVersionSelectorModalFactory(this.record, version).then(result => {
        ////        this.record = result.record;
        ////        this.versionLoadedToUI = result.version.Name;
        ////        this.selectedRecordVersionID = result.version.ID;
        ////        this.selectedRecordVersion = result.version;
        ////        this.versions.forEach(x => {
        ////            x["currentLoaded"] = x.ID === result.version.ID;
        ////        });
        ////    });
        ////}
        ////private saveVersionOnly(): ng.IPromise<void> {
        ////    this.consoleDebug("saveVersionOnly:product:0");
        ////    if (!(this.record.ID > 0)) {
        ////        this.consoleDebug("saveVersionOnly:product:1");
        ////        return this.$q.reject("No record ID, cannot save a version only");
        ////    }
        ////    this.consoleDebug("saveVersionOnly:product:2");
        ////    this.setRunning(`Saving Version of ${this.detailName}...`);
        ////    this.consoleDebug("saveVersionOnly:product:3");
        ////    return this.$q((resolve, __) => {
        ////        this.cvCreateVersionModalFactory(this.record, true).then(r => {
        ////            this.consoleDebug("saveVersionOnly:product:4");
        ////            // Always transition to the version that was created and make the forms pristine
        ////            this.$state.go(
        ////                "inventory.products.detail",
        ////                { ID: this.record.ID, versionID: r.version.ID },
        ////                <ng.ui.IStateOptions>{ reload: true, notify: true, inherit: false, location: true, relative: this.$state.$current });
        ////            this.consoleDebug("saveVersionOnly:product:5");
        ////        }).catch(() => {
        ////            // We resolve here because the user cancelled the modal, which was an intended action
        ////            this.finishRunning();
        ////            resolve();
        ////        });
        ////    });
        ////}
        ////private publishVersion(): ng.IPromise<void> {
        ////    this.consoleDebug("publishVersion:product:0");
        ////    this.setRunning(`Publishing Version of ${this.detailName}...`);
        ////    return this.$q((__, reject) => {
        ////        // Resolve is not used because the successful run will update the router state
        ////        this.cvApi.products.UpdateProduct(this.record).then(r => {
        ////            this.consoleDebug("publishVersion:product:1");
        ////            if (!r || !r.data) {
        ////                this.consoleDebug("publishVersion:product:2");
        ////                this.fullReject(reject, null, ["No data returned on update call"])
        ////                return;
        ////            }
        ////            this.consoleDebug("publishVersion:product:3");
        ////            this.updateRecordActionAfterSuccess(r.data).then(r2 => {
        ////                this.consoleDebug("publishVersion:product:4");
        ////                this.$state.go(
        ////                    "inventory.products.detail",
        ////                    { ID: this.record.ID },
        ////                    <ng.ui.IStateOptions>{ reload: true, notify: true, inherit: false, location: true, relative: this.$state.$current });
        ////                this.consoleDebug("publishVersion:product:5");
        ////            }).catch(reason2 => this.fullReject(reject, null, [reason2]));
        ////        }).catch(reason1 => this.fullReject(reject, null, [reason1]));
        ////    });
        ////}
        ////
        // Standard Detail Footer Buttons Widget pull-in to override
        // Properties
        ////get includeSave(): boolean {
        ////    if (!this.record) {
        ////        return false;
        ////    }
        ////    return this.record.ID > 0
        ////        ? this.cvSecurityService.hasPermission("Products.Product.Update")
        ////        : this.cvSecurityService.hasPermission("Products.Product.Create");
        ////}
        ////get includeSaveAndClose(): boolean {
        ////    return false;
        ////    ////if (!this.record) {
        ////    ////    return false;
        ////    ////}
        ////    ////return this.record.ID > 0
        ////    ////    ? this.cvSecurityService.hasPermission("Products.Product.Update")
        ////    ////    : this.cvSecurityService.hasPermission("Products.Product.Create");
        ////}
        ////get includeSaveAndPublish(): boolean {
        ////    if (!this.record) {
        ////        return false;
        ////    }
        ////    return this.record.ID > 0
        ////        ? this.cvSecurityService.hasPermission("Products.Product.Update")
        ////        : this.cvSecurityService.hasPermission("Products.Product.Create");
        ////}
        ////get includeDeactivate(): boolean {
        ////    return this.record
        ////        && this.record.ID > 0
        ////        && this.record.Active
        ////        && this.cvSecurityService.hasPermission("Products.Product.Deactivate");
        ////}
        ////get includeReactivate(): boolean {
        ////    return this.record
        ////        && this.record.ID > 0
        ////        && !this.record.Active
        ////        && this.cvSecurityService.hasPermission("Products.Product.Reactivate");
        ////}
        ////get includeDelete(): boolean {
        ////    return this.record
        ////        && this.record.ID > 0
        ////        && this.cvSecurityService.hasPermission("Products.Product.Delete");
        ////}
        ////get canPublish(): boolean {
        ////    return !this.dirtyForms()
        ////        && this.selectedRecordVersion
        ////        && productModelToVersionJson(this.record) === this.selectedRecordVersion.SerializedRecord;
        ////}
        // Functions
        ////validForms(): boolean {
        ////    if (!this.forms) {
        ////        return false;
        ////    }
        ////    var valid = true;
        ////    Object.keys(this.forms).forEach(key => {
        ////        if (!this.forms[key].$valid) {
        ////            valid = false;
        ////            return false;
        ////        }
        ////        return true;
        ////    });
        ////    return valid;
        ////}
        ////dirtyForms(): boolean {
        ////    if (!this.forms) {
        ////        return false;
        ////    }
        ////    var dirty = false;
        ////    Object.keys(this.forms).forEach(key => {
        ////        if (this.forms[key]
        ////            && this.forms[key].$dirty) {
        ////            dirty = true;
        ////            return false;
        ////        }
        ////        return true;
        ////    });
        ////    return dirty;
        ////}
        ////save(): void {
        ////    this.saveVersionOnly()
        ////        .then(() => { console.log("saveVersionOnly then"); })
        ////        .catch(() => { console.log("saveVersionOnly catch"); })
        ////        .finally(() => { console.log("saveVersionOnly exited"); });
        ////}
        ////saveAndClose(): void {
        ////    this.saveRecord("inventory.products.list")
        ////        .then(() => { console.log("saveAndClose then"); })
        ////        .catch(() => { console.log("saveAndClose catch"); })
        ////        .finally(() => { console.log("saveAndClose exited"); });
        ////}
        ////saveAndPublish(): void {
        ////    this.saveRecord()
        ////        .then(() => { console.log("saveAndPublish then"); })
        ////        .catch(() => { console.log("saveAndPublish catch"); })
        ////        .finally(() => { console.log("saveAndPublish exited"); });
        ////}
        ////publish(): void {
        ////    this.publishVersion()
        ////        .then(() => { console.log("publishVersion then"); })
        ////        .catch(() => { console.log("publishVersion catch"); })
        ////        .finally(() => { console.log("publishVersion exited"); });
        ////}
        ////deactivate(): void {
        ////    this.deactivateRecord();
        ////}
        ////reactivate(): void {
        ////    this.reactivateRecord();
        ////}
        ////delete(): void {
        ////    this.deleteRecord("inventory.products.list");
        ////}
        ////backCancel(): void {
        ////    if (!this.dirtyForms()) {
        ////        this.cancel("inventory.products.list");
        ////        return;
        ////    }
        ////    // Ask the user if they really want to leave
        ////    this.cvConfirmModalFactory(
        ////            this.$translate("ui.admin.controls.backCancel.UnsavedChangesConfirmation.Message"))
        ////        .then(result => {
        ////            if (result) {
        ////                this.cancel("inventory.products.list");
        ////            }
        ////        });
        ////}
        //*/

        // Constructor
        constructor(
                public    readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                public    readonly $q: ng.IQService,
                public    readonly $filter: ng.IFilterService,
                public    readonly $window: ng.IWindowService,
                public    readonly $state: ng.ui.IStateService,
                public    readonly $stateParams: ng.ui.IStateParamsService,
                public    readonly $translate: ng.translate.ITranslateService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: admin.services.IServiceStrings,
                public    readonly cvApi: api.ICEFAPI,
                protected readonly cvSecurityService: services.SecurityService,
                public    readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory/*,
                protected readonly cvCreateVersionModalFactory: modals.ICreateVersionModalFactory<api.ProductModel>,
                protected readonly cvProductVersionSelectorModalFactory: modals.IProductVersionSelectorModalFactory*/) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
            ////const unbind1 = this.$scope.$on(this.cvServiceStrings.events.products.recordVersionCreated,
            ////    () => this.loadRecordActionAfterSuccess(this.record));
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(this.unbindAttributesChanged)) { this.unbindAttributesChanged(); }
                ////if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    adminApp.directive("cefProductEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { supplierMode: "=?" },
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/productEditor.html", "ui"),
        controller: ProductDetailController,
        controllerAs: "ipeCtrl",
        bindToController: true
    }));
}
