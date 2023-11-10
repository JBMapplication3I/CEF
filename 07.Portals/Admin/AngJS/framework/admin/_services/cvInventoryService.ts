/**
 * @file framework/admin/_services/cvInventoryService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Inventory service class
 */
module cef.admin.services {
    export interface IInventoryService {
        /** @deprecated */
        getInventory: (product: api.HasInventoryObject, store?: api.StoreModel) => ng.IPromise<number>;
        /** @deprecated */
        getStoreListWithInventory: (product: api.HasInventoryObject) => ng.IPromise<api.StoreModel[]>;
        /** @deprecated */
        getInventoryObject: (products: api.HasInventoryObject[], store?: api.StoreModel) => ng.IPromise<api.HasInventoryObject[]>;

        factoryAssign(product: api.ProductModel): api.ProductModel;
        bulkFactoryAssign(products: api.ProductModel[]): ng.IPromise<api.ProductModel[]>;
    };

    export class InventoryService {
        /** @deprecated */
        private setCount(quantity: number, allocated?: number): number {
            return quantity - (allocated || 0);
        }
        /** @deprecated */
        private extractProductId(product: api.HasInventoryObject): number {
            if (!product) { return null; }
            return (product as api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>).ProductID
                || (product as api.ProductModel).ID;
        }
        /** @deprecated */
        private extractProductIds(products: api.HasInventoryObject[]): Array<number> {
            return products.map(v => this.extractProductId(v)).filter(x => x); // Must be a valid number to enter the array
        }
        /** @deprecated */
        private setInventoryValues(
                matrixData: api.StoreInventoryLocationsMatrixModel[],
                // inventoryProduct: api.ProductModel,
                product: api.HasInventoryObject,
                storeKey: string,
                calculated: api.CalculatedInventory)
                : api.HasInventoryObject {
            if (!this.cefConfig.featureSet.inventory.enabled) {
                product.inventoryObject = {
                    storeQty: 0,
                    storeDCIQty: 0,
                    anyDCIQty: 0,
                    masterDCQty: 0,
                    totalQty: 0,
                    restrictShipFlag: false
                };
                return product;
            }
            if (!this.cefConfig.featureSet.inventory.advanced.enabled) {
                product.inventoryObject = {
                    storeQty: 0,
                    storeDCIQty: 0,
                    anyDCIQty: 0,
                    masterDCQty: 0,
                    totalQty: calculated.QuantityOnHand,
                    restrictShipFlag: false
                };
                return product;
            }
            if (!calculated.RelevantLocations || !calculated.RelevantLocations.length) {
                product.inventoryObject = {
                    storeQty: 0,
                    storeDCIQty: 0,
                    anyDCIQty: 0,
                    masterDCQty: 0,
                    totalQty: calculated.QuantityOnHand,
                    restrictShipFlag: false
                };
                return product;
            }
            calculated.RelevantLocations.forEach(pilsInstance => {
                const inventory: api.InventoryObject = {
                    storeQty: 0,
                    storeDCIQty: 0,
                    anyDCIQty: 0,
                    masterDCQty: 0,
                    totalQty: 0,
                    restrictShipFlag: false
                };
                const count = this.setCount(pilsInstance.Quantity, pilsInstance.QuantityAllocated);
                const storeMatrixData = storeKey && matrixData
                    .filter(x => x.StoreKey.toLowerCase() === storeKey.toLowerCase())[0];
                if (storeMatrixData && storeMatrixData.DistributionCenterInventoryLocationID > 0) {
                    inventory.anyDCIQty += count;
                    if (pilsInstance.InventoryLocationSectionInventoryLocationKey
                            === storeMatrixData.DistributionCenterInventoryLocationKey) {
                        inventory.storeDCIQty += count;
                        inventory.totalQty += count;
                    }
                }
                if (storeMatrixData && storeMatrixData.InternalInventoryLocationID > 0) {
                    if (pilsInstance.InventoryLocationSectionInventoryLocationKey
                            === storeMatrixData.InternalInventoryLocationKey) {
                        inventory.storeQty += count;
                        inventory.totalQty += count;
                    }
                }
                if (product.inventoryObject) {
                    product.inventoryObject = {
                        storeQty: (product.inventoryObject.storeQty + inventory.storeQty),
                        storeDCIQty: (product.inventoryObject.storeDCIQty + inventory.storeDCIQty),
                        anyDCIQty: (product.inventoryObject.anyDCIQty + inventory.anyDCIQty),
                        masterDCQty: Math.max(product.inventoryObject.masterDCQty, inventory.masterDCQty),
                        totalQty: (product.inventoryObject.totalQty + inventory.totalQty)
                    };
                } else {
                    product.inventoryObject = inventory;
                }
                /*
                if (product.SerializableAttributes && product.SerializableAttributes["SKU-Restrictions"]) {
                    const sku = this.$sce.getTrustedHtml(product.SerializableAttributes["SKU-Restrictions"].Value);
                    if (sku.indexOf('"RestrictShipFlag":"Y"') !== -1 || sku.indexOf("RestrictShipFlag&#34;:&#34;Y&#34;")) {
                        product.inventoryObject.restrictShipFlag = true;
                    }
                };
                */
            });
            // Object.keys(inventoryProduct).forEach(key => {
            //     if (angular.isUndefined(product[key])) {
            //         product[key] = inventoryProduct[key];
            //     }
            // });
            return product;
        }
        /** @deprecated */
        private assignProductIsOutOfStockFunction(
                product: api.HasInventoryObject,
                cefConfig: core.CefConfig,
                calculated: api.CalculatedInventory)
                : void {
            product["isOutOfStock"] = (): boolean => {
                if (!cefConfig.featureSet.inventory.enabled) {
                    // Inventory feature disabled, always return as never out of stock
                    return false;
                }
                if (calculated.IsUnlimitedStock || calculated.AllowBackOrder) {
                    return false;
                }
                const stock = product["countStock"]();
                if (stock !== null) {
                    return stock <= 0;
                }
                return false;
            };
        }
        /** @deprecated */
        private assignProductCountStockFunction(
                product: api.HasInventoryObject,
                cefConfig: core.CefConfig,
                calculated: api.CalculatedInventory)
                : void {
            product["countStock"] = (): number => {
                if (!cefConfig.featureSet.inventory.enabled) {
                    return null;
                }
                // Unlimited Stock
                if (calculated.IsUnlimitedStock) {
                    return 0;
                }
                // Simple Stock
                if (!cefConfig.featureSet.inventory.advanced.enabled) {
                    return calculated.QuantityOnHand;
                }
                // PILS Stock
                if (calculated.RelevantLocations && calculated.RelevantLocations.length > 0
                    && this.cefConfig.featureSet.inventory.advanced.enabled
                    && this.cefConfig.featureSet.stores.enabled
                    && this.cefConfig.featureSet.inventory.advanced.countOnlyThisStoresWarehouseStock) {
                    const matrix = this.getStoreInventoryLocationsMatrixImmediate();
                    if (matrix && matrix.length) {
                        const thisStoresWarehouses = calculated.RelevantLocations
                            .filter(x => _.some(matrix,
                                y => y.InternalInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey
                                  || y.DistributionCenterInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey));
                        return _.sumBy(thisStoresWarehouses,
                            x => (x.Quantity || 0) - (x.QuantityAllocated || 0));
                    }
                }
                return calculated.QuantityOnHand;
            };
        }
        /** @deprecated */
        getInventory(product: api.HasInventoryObject, store: api.StoreModel = null): ng.IPromise<number> {
            if (!this.cefConfig.featureSet.inventory.enabled) {
                return this.$q.reject("Feature disabled");
            }
            if (!product) {
                return this.$q.reject("No product provided to check inventory against");
            }
            return this.$q((resolve, reject) => {
                this.cvApi.providers.CalculateInventory(this.extractProductId(product)).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error(r && r.data);
                        reject(r && r.data);
                        return;
                    }
                    // TODO: Expand this to handle all the new data from the inventory providers 2020.4
                    resolve(this.setCount(r.data.Result.QuantityPresent, r.data.Result.QuantityAllocated));
                }).catch(reject);
            });
        }
        /** @deprecated */
        getInventoryObject(
                products: api.HasInventoryObject[],
                store: api.StoreModel = null)
                : ng.IPromise<api.HasInventoryObject[]> {
            if (!this.cefConfig.featureSet.inventory.enabled) {
                // Just return the original without modifications
                return this.$q.resolve(products);
            }
            const debugMsg = `inventoryService.getInventoryObject(products, store)`;
            ////consoleDebug(debugMsg);
            if (!products || !products.length) {
                console.warn(`${debugMsg} No products provided to check inventory against`);
                return this.$q.reject("No products provided to check inventory against");
            }
            const ids = this.extractProductIds(products);
            if (!ids || ids.length <= 0) {
                console.warn(`${debugMsg} No products provided to check inventory against (extraction of IDs failed)`);
                return this.$q.reject("No products provided to check inventory against (extraction of IDs failed)");
            }
            return this.$q<api.HasInventoryObject[]>((resolve, reject) => {
                this.getStoreInventoryLocationsMatrixPromise().then(matrixData => {
                    this.cvApi.providers.BulkCalculateInventory({
                        ProductIDs: products.map(x => this.extractProductId(x))
                    }).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            console.error(r && r.data);
                            reject(r && r.data);
                            return;
                        }
                        // TODO@JTG: Handle the new inventory provider data from 2020.4
                        Object.keys(r.data.Result).forEach(productID => {
                            const found = _.find(products, x => this.extractProductId(x) === Number(productID));
                            const inventory = r.data.Result[productID];
                            found.inventoryObject = null;
                            this.setInventoryValues(matrixData, /*found,*/ found, store && store.CustomKey, inventory);
                            this.assignProductCountStockFunction(found, this.cefConfig, inventory);
                            this.assignProductIsOutOfStockFunction(found, this.cefConfig, inventory);
                        });
                        resolve(products);
                    }).catch(reject);
                }).catch(reject);
            });
        }
        /** @deprecated */
        getStoreListWithInventory(product: api.HasInventoryObject): ng.IPromise<api.StoreModel[]> {
            if (!this.cefConfig.featureSet.stores.enabled
                || !this.cefConfig.featureSet.inventory.enabled
                || !this.cefConfig.featureSet.inventory.advanced.enabled) {
                return this.$q.reject("Feature disabled");
            }
            if (!product) {
                return this.$q.reject("No product provided to check inventory against");
            }
            return this.$q((resolve, reject) => {
                if (!product) {
                    reject("No product provided to check inventory against");
                    return;
                }
                // this.cvStoreLocationService.getUserNearbyStores().then(stores => {
                    this.cvApi.providers.CalculateInventory(this.extractProductId(product)).then(r => {
                        if (!r || !r.data || !r.data.ActionSucceeded) {
                            console.error(r && r.data);
                            reject(r && r.data);
                            return;
                        }
                        if (!r.data.Result.RelevantLocations) {
                            reject("No relevant locations provided, cannot filter to the store's inventory");
                            return;
                        }
                        resolve(this.stores.filter(store => r.data.Result.RelevantLocations
                            .filter(x => store.CustomKey === x.InventoryLocationSectionInventoryLocationKey)
                            .map(loc => store.Inventory = loc.Quantity - (loc.QuantityAllocated || 0)))
                            .filter(x => x.Inventory > 0 ? x : null));
                    }).catch(reject);
                // }).catch(reject);
            });
        }
        // Storefront has these in the cvCurrentStoreService but admin doesn't have this
        /** @deprecated */
        private cachedStoreInventoryLocationsMatrix: Array<api.StoreInventoryLocationsMatrixModel> = null;
        /** @deprecated */
        private deferIsRunning: boolean = false;
        /** @deprecated */
        private deferPromise: ng.IPromise<Array<api.StoreInventoryLocationsMatrixModel>> = null;
        /** @deprecated */
        private stores: api.StoreModel[] = [];
        /** @deprecated */
        private getStoreInventoryLocationsMatrixImmediate(): Array<api.StoreInventoryLocationsMatrixModel> {
            return this.cachedStoreInventoryLocationsMatrix || [];
        }
        /** @deprecated */
        private getStoreInventoryLocationsMatrixPromise(): ng.IPromise<Array<api.StoreInventoryLocationsMatrixModel>> {
            if (this.cachedStoreInventoryLocationsMatrix) {
                return this.$q.resolve(this.cachedStoreInventoryLocationsMatrix);
            }
            if (this.deferIsRunning) {
                return this.deferPromise;
            }
            var defer = this.$q.defer<Array<api.StoreInventoryLocationsMatrixModel>>();
            this.deferPromise = defer.promise;
            this.deferIsRunning = true;
            this.cvApi.stores.GetStoreInventoryLocationsMatrix().then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    defer.reject("Failed to execute");
                    return;
                }
                this.cachedStoreInventoryLocationsMatrix = r.data.Result;
                defer.resolve(this.cachedStoreInventoryLocationsMatrix);
            }).catch(() => defer.reject("Failed to execute"))
            .finally(() => this.deferIsRunning = false);
            return defer.promise;
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                ////private readonly $sce: ng.ISCEService,
                private readonly cvApi: api.ICEFAPI,
                private readonly cefConfig: core.CefConfig) {
            cvApi.stores.GetStores({ Active: true, AsListing: true }).then(r => this.stores = r.data.Results);
        }
        // Functions
        private genBlankInventoryObj(): api.CalculatedInventories {
            return <api.CalculatedInventories>{
                ProductID: 0,
                IsDiscontinued: false,
                IsUnlimitedStock: false,
                IsOutOfStock: false,
                QuantityPresent: null,
                QuantityAllocated: null,
                QuantityOnHand: null,
                MaximumPurchaseQuantity: null,
                MaximumPurchaseQuantityIfPastPurchased: null,
                AllowBackOrder: false,
                MaximumBackOrderPurchaseQuantity: null,
                MaximumBackOrderPurchaseQuantityIfPastPurchased: null,
                MaximumBackOrderPurchaseQuantityGlobal: null,
                AllowPreSale: false,
                PreSellEndDate: null,
                QuantityPreSellable: null,
                QuantityPreSold: null,
                MaximumPrePurchaseQuantity: null,
                MaximumPrePurchaseQuantityIfPastPurchased: null,
                MaximumPrePurchaseQuantityGlobal: null,
                RelevantLocations: null,
                loading: true
            };
        }
        factoryAssign(product: api.ProductModel): api.ProductModel {
            if (!this.cefConfig.featureSet.inventory.enabled) {
                return product;
            }
            if (angular.isFunction(product.readInventory)) {
                // Already processed
                return product;
            }
            product["$_rawInventory"] = this.genBlankInventoryObj();
            product.readInventory = () => product["$_rawInventory"];
            // TODO: MemCache the results by product ID so we can avoid repeat calls for same product
            this.cvApi.providers.CalculateInventory(product.ID).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    console.error(r && r.data);
                    return;
                }
                let inventory = product["$_rawInventory"] as api.CalculatedInventories;
                if (!inventory) {
                    inventory = { loading: true } as any;
                }
                // Assign updated values
                /*
                 * NOTE: Feature required settings have been run in the server to only assign values
                 * That could be valid both globally and on this individual product's level. All
                 * variables can be assigned here because they will have the correct flags on them
                 * already.
                 */
                inventory.ProductID = r.data.Result.ProductID;
                inventory.IsDiscontinued = r.data.Result.IsDiscontinued;
                inventory.IsUnlimitedStock = r.data.Result.IsUnlimitedStock;
                inventory.IsOutOfStock = r.data.Result.IsOutOfStock;
                inventory.QuantityPresent = r.data.Result.QuantityPresent;
                inventory.QuantityAllocated = r.data.Result.QuantityAllocated;
                inventory.QuantityOnHand = r.data.Result.QuantityOnHand;
                inventory.MaximumPurchaseQuantity = r.data.Result.MaximumPurchaseQuantity;
                inventory.MaximumPurchaseQuantityIfPastPurchased = r.data.Result.MaximumPurchaseQuantityIfPastPurchased;
                inventory.AllowBackOrder = r.data.Result.AllowBackOrder;
                inventory.MaximumBackOrderPurchaseQuantity = r.data.Result.MaximumBackOrderPurchaseQuantity;
                inventory.MaximumBackOrderPurchaseQuantityIfPastPurchased = r.data.Result.MaximumBackOrderPurchaseQuantityIfPastPurchased;
                inventory.MaximumBackOrderPurchaseQuantityGlobal = r.data.Result.MaximumBackOrderPurchaseQuantityGlobal;
                inventory.AllowPreSale = r.data.Result.AllowPreSale;
                inventory.PreSellEndDate = r.data.Result.PreSellEndDate;
                inventory.QuantityPreSellable = r.data.Result.QuantityPreSellable;
                inventory.QuantityPreSold = r.data.Result.QuantityPreSold;
                inventory.MaximumPrePurchaseQuantity = r.data.Result.MaximumPrePurchaseQuantity;
                inventory.MaximumPrePurchaseQuantityIfPastPurchased = r.data.Result.MaximumPrePurchaseQuantityIfPastPurchased;
                inventory.MaximumPrePurchaseQuantityGlobal = r.data.Result.MaximumPrePurchaseQuantityGlobal;
                inventory.RelevantLocations = r.data.Result.RelevantLocations;
                // Assign calculated values
                /*
                // PILS Stock (TODO: Rework this with variables for store only stock)
                if (r.data.Result.RelevantLocations && r.data.Result.RelevantLocations.length > 0
                    && this.cefConfig.featureSet.inventory.advanced.enabled
                    && this.cefConfig.featureSet.stores.enabled
                    && this.cefConfig.featureSet.inventory.advanced.countOnlyThisStoresWarehouseStock) {
                    const matrix = this.cvCurrentStoreService.getStoreInventoryLocationsMatrixImmediate();
                    if (matrix && matrix.length) {
                        const thisStoresWarehouses = r.data.Result.RelevantLocations
                            .filter(x => _.some(matrix,
                                y => y.InternalInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey
                                  || y.DistributionCenterInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey));
                        return _.sumBy(thisStoresWarehouses,
                            x => (x.Quantity || 0) - (x.QuantityAllocated || 0));
                    }
                }
                */
                // Finish
                inventory.loading = false;
                product["$_rawInventory"] = inventory;
            });
            return product;
        }
        bulkFactoryAssign(products: api.ProductModel[]): ng.IPromise<api.ProductModel[]> {
            if (!this.cefConfig.featureSet.inventory.enabled) {
                return this.$q.resolve(products);
            }
            const debugMsg = `inventoryService.bulkFactoryAssign(products)`;
            if (!products || !products.length) {
                console.warn(`${debugMsg} No products provided to check inventory against`);
                return this.$q.reject("No products provided to check inventory against");
            }
            const processed: api.ProductModel[] = [];
            const toProcess: api.ProductModel[] = [];
            products.forEach(product => {
                if (angular.isFunction(product.readInventory)) {
                    processed.push(product);
                } else {
                    product["$_rawInventory"] = this.genBlankInventoryObj();
                    product.readInventory = () => product["$_rawInventory"];
                    toProcess.push(product);
                }
            });
            if (!toProcess.length) {
                return this.$q.resolve(processed);
            }
            return this.$q((resolve, reject) => {
                this.cvApi.providers.BulkCalculateInventory({
                    ProductIDs: toProcess.map(x => x.ID)
                }).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        console.error(r && r.data);
                        reject(r && r.data);
                        return;
                    }
                    Object.keys(r.data.Result).forEach(productID => {
                        const found = _.find(toProcess, x => x.ID === Number(productID));
                        let inventory = found["$_rawInventory"] as api.CalculatedInventories;
                        if (!inventory) {
                            inventory = { loading: true } as any;
                        }
                        // Assign updated values
                        /*
                         * NOTE: Feature required settings have been run in the server to only assign values
                         * That could be valid both globally and on this individual product's level. All
                         * variables can be assigned here because they will have the correct flags on them
                         * already.
                         */
                        inventory.ProductID = r.data.Result[productID].ProductID;
                        inventory.IsDiscontinued = r.data.Result[productID].IsDiscontinued;
                        inventory.IsUnlimitedStock = r.data.Result[productID].IsUnlimitedStock;
                        inventory.IsOutOfStock = r.data.Result[productID].IsOutOfStock;
                        inventory.QuantityPresent = r.data.Result[productID].QuantityPresent;
                        inventory.QuantityAllocated = r.data.Result[productID].QuantityAllocated;
                        inventory.QuantityOnHand = r.data.Result[productID].QuantityOnHand;
                        inventory.MaximumPurchaseQuantity = r.data.Result[productID].MaximumPurchaseQuantity;
                        inventory.MaximumPurchaseQuantityIfPastPurchased = r.data.Result[productID].MaximumPurchaseQuantityIfPastPurchased;
                        inventory.AllowBackOrder = r.data.Result[productID].AllowBackOrder;
                        inventory.MaximumBackOrderPurchaseQuantity = r.data.Result[productID].MaximumBackOrderPurchaseQuantity;
                        inventory.MaximumBackOrderPurchaseQuantityIfPastPurchased = r.data.Result[productID].MaximumBackOrderPurchaseQuantityIfPastPurchased;
                        inventory.MaximumBackOrderPurchaseQuantityGlobal = r.data.Result[productID].MaximumBackOrderPurchaseQuantityGlobal;
                        inventory.AllowPreSale = r.data.Result[productID].AllowPreSale;
                        inventory.PreSellEndDate = r.data.Result[productID].PreSellEndDate;
                        inventory.QuantityPreSellable = r.data.Result[productID].QuantityPreSellable;
                        inventory.QuantityPreSold = r.data.Result[productID].QuantityPreSold;
                        inventory.MaximumPrePurchaseQuantity = r.data.Result[productID].MaximumPrePurchaseQuantity;
                        inventory.MaximumPrePurchaseQuantityIfPastPurchased = r.data.Result[productID].MaximumPrePurchaseQuantityIfPastPurchased;
                        inventory.MaximumPrePurchaseQuantityGlobal = r.data.Result[productID].MaximumPrePurchaseQuantityGlobal;
                        inventory.RelevantLocations = r.data.Result[productID].RelevantLocations;
                        // Assign calculated values
                        /*
                        // PILS Stock (TODO: Rework this with variables for store only stock)
                        if (r.data.Result.RelevantLocations && r.data.Result.RelevantLocations.length > 0
                            && this.cefConfig.featureSet.inventory.advanced.enabled
                            && this.cefConfig.featureSet.stores.enabled
                            && this.cefConfig.featureSet.inventory.advanced.countOnlyThisStoresWarehouseStock) {
                            const matrix = this.cvCurrentStoreService.getStoreInventoryLocationsMatrixImmediate();
                            if (matrix && matrix.length) {
                                const thisStoresWarehouses = r.data.Result.RelevantLocations
                                    .filter(x => _.some(matrix,
                                        y => y.InternalInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey
                                        || y.DistributionCenterInventoryLocationKey === x.InventoryLocationSectionInventoryLocationKey));
                                return _.sumBy(thisStoresWarehouses,
                                    x => (x.Quantity || 0) - (x.QuantityAllocated || 0));
                            }
                        }
                        */
                        // Finish
                        inventory.loading = false;
                        found["$_rawInventory"] = inventory;
                        processed.push(found);
                    });
                    resolve(processed);
                }).catch(reject);
            });
        }
    }
}
