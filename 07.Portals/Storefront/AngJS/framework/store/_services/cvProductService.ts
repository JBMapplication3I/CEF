/**
 * @file framework/store/_services/cvProductService.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Service class, stores product data that has been loaded
 * to reduce calls to the server, and provide a single source of
 * information for all areas of the application.
 */
module cef.store.services {
    export interface IProductLookupInterface extends ILookupInterfaceBase {
        seoUrl?: string;
        storeID?: number;
        previewID?: number;
    }

    export interface IProductService extends IDataServiceBase<api.ProductModel, IProductLookupInterface> {
        appendToSalesItems(salesItems: any[]): ng.IPromise<any[]>;
        bulkGet(productIDs: number[], storeID?: number): ng.IPromise<api.ProductModel[]>;
        factoryAssign(product: api.ProductModel): api.ProductModel;
        productUOMSelectionOnChange(value: string, id: number): void;
        productUOMSetInitalValue(obj: any, productID: number): void;
        productUOMSelection: string;
        productUOMSelectionAbv: string;
        productUOMSelectionQty: string;
        productUOMSelectionID: string;
        productUOMSelectionObject: any;
        initalProductUOMObject: any;
    }

    export class ProductService
        extends DataServiceBase<api.ProductModel, IProductLookupInterface>
        implements IProductService {
        // Properties
        private seoUrlToIDLookup: { [seoUrl: string]: number } = { };
        private bySeoUrlPromises: { [seoUrl: string]: ng.IPromise<number> } = { };
        productUOMSelection: string;
        productUOMSelectionAbv: string;
        productUOMSelectionQty: string;
        productUOMSelectionID: string;
        productUOMSelectionObject: any = { };
        initalProductUOMObject: any = { };
        // Functions
        productUOMSelectionOnChange(value: string, id: number): any {
            if (value === null) {
                delete this.productUOMSelectionObject[id];
            } else {
                this.productUOMSelection = value;
                this.productUOMSelectionQty = value?.split("|")[0];
                this.productUOMSelectionAbv = value?.split("|")[1];
                this.productUOMSelectionID = value?.split("|")[2];
                this.productUOMSelectionObject[this.productUOMSelectionID] = [this.productUOMSelectionQty, this.productUOMSelectionAbv];
            }
        }
        productUOMSetInitalValue(objArr: any, productID: number): any {
            if (!objArr.length) {
                return
            };
            let initalSelection = objArr[0];
            this.productUOMSelection = `${initalSelection.Value}|${initalSelection.Key}|${productID}`;
            this.productUOMSelectionQty = initalSelection.Value;
            this.productUOMSelectionAbv = initalSelection.Key;
            this.productUOMSelectionID = initalSelection.ID;
            this.productUOMSelectionObject[this.productUOMSelectionID] = [this.productUOMSelectionQty, this.productUOMSelectionAbv];
            this.initalProductUOMObject[productID] = this.productUOMSelection;
            this.productUOMSelectionOnChange(this.productUOMSelection, productID);
        }
        private genBlankSortObj(): api.CalculatedSortOrder {
            return {
                order: null,
                loading: true,
            };
        }
        // Abstract
        getByIDPromise              = this.cvApi.products.GetProductByID;
        getByIDsPromise             = this.cvApi.products.GetProducts;
        checkExistsByIDPromise      = this.cvApi.products.CheckProductExistsByID;
        checkExistsByKeyPromise     = this.cvApi.products.CheckProductExistsByKey;
        checkExistsByNamePromise    = this.cvApi.products.CheckProductExistsByName;
        checkExistsBySeoUrlPromise  = this.cvApi.products.CheckProductExistsBySeoUrl;
        // Functions
        getCached(lookup: IProductLookupInterface): api.ProductModel {
            if (!lookup || !lookup.id && !lookup.name && !lookup.seoUrl) {
                return null;
            }
            if (!lookup.id) {
                if (lookup.key) {
                    if (!this.keyToIDLookup[lookup.key]) {
                        this.get(lookup);
                        return null; // Waiting on promise to cache
                    }
                    lookup.id = this.keyToIDLookup[lookup.key];
                } else if (lookup.name) {
                    if (!this.nameToIDLookup[lookup.name]) {
                        this.get(lookup);
                        return null; // Waiting on promise to cache
                    }
                    lookup.id = this.nameToIDLookup[lookup.name];
                } else if (lookup.seoUrl) {
                    if (!this.seoUrlToIDLookup[lookup.seoUrl]) {
                        this.get(lookup);
                        return null; // Waiting on promise to cache
                    }
                    lookup.id = this.seoUrlToIDLookup[lookup.seoUrl];
                }
            }
            if (!lookup.id) {
                return null;
            }
            if (this.recordCache[lookup.id] && !lookup.force) {
                return this.recordCache[lookup.id];
            }
            this.get(lookup);
            return null; // Waiting on promise to cache
        }
        get(lookup: IProductLookupInterface): ng.IPromise<api.ProductModel> {
            if (!lookup || !lookup.id && !lookup.name && !lookup.seoUrl) {
                return this.$q.reject("ERROR! Nothing to look up by");
            }
            return this.$q((resolve, reject) => {
                if (!lookup.id) {
                    if (lookup.key) {
                        if (this.keyToIDLookup[lookup.key]) {
                            lookup.id = this.keyToIDLookup[lookup.key];
                        } else if (this.byKeyPromises[lookup.key]) {
                            resolve(this.byKeyPromises[lookup.key]);
                            return;
                        } else {
                            this.byKeyPromises[lookup.key] = this.checkExistsByKeyPromise(lookup.key).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate product by Key");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.keyToIDLookup[lookup.key] = r.data;
                                // Loop back in with the id
                                const newLookup = <IProductLookupInterface>{
                                    id: r.data,
                                    ref: lookup.ref
                                };
                                if (lookup.previewID) {
                                    newLookup.previewID = lookup.previewID;
                                }
                                resolve(this.get(newLookup));
                            }).catch(reject);
                            return;
                        }
                    } else if (lookup.name) {
                        if (this.nameToIDLookup[lookup.name]) {
                            lookup.id = this.nameToIDLookup[lookup.name];
                        } else if (this.byNamePromises[lookup.name]) {
                            resolve(this.byNamePromises[lookup.name]);
                            return;
                        } else {
                            this.byNamePromises[lookup.name] = this.checkExistsByNamePromise({ Name: lookup.name }).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate product by Name");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.nameToIDLookup[lookup.name] = r.data;
                                // Loop back in with the id
                                const newLookup = <IProductLookupInterface>{
                                    id: r.data,
                                    ref: lookup.ref
                                };
                                if (lookup.previewID) {
                                    newLookup.previewID = lookup.previewID;
                                }
                                resolve(this.get(newLookup));
                            }).catch(reject);
                            return;
                        }
                    } else if (lookup.seoUrl) {
                        if (this.seoUrlToIDLookup[lookup.seoUrl]) {
                            lookup.id = this.seoUrlToIDLookup[lookup.seoUrl];
                        } else if (this.bySeoUrlPromises[lookup.seoUrl]) {
                            resolve(this.bySeoUrlPromises[lookup.seoUrl]);
                            return;
                        } else {
                            this.bySeoUrlPromises[lookup.seoUrl] = this.checkExistsBySeoUrlPromise({ SeoUrl: lookup.seoUrl }).then(r => {
                                if (!r || !r.data) {
                                    reject("ERROR! Unable to locate product by SeoUrl");
                                    return;
                                }
                                // Save it so we don't have to look again
                                this.seoUrlToIDLookup[lookup.seoUrl] = r.data;
                                // Loop back in with the id
                                const newLookup = <IProductLookupInterface>{
                                    id: r.data,
                                    ref: lookup.ref
                                };
                                if (lookup.previewID) {
                                    newLookup.previewID = lookup.previewID;
                                }
                                resolve(this.get(newLookup));
                            }).catch(reject);
                            return;
                        }
                    }
                }
                if (!lookup.id) {
                    reject("ERROR! Nothing to look up by");
                    return;
                }
                if (this.recordCache[lookup.id] && !lookup.force) {
                    if (lookup.ref) {
                        const clone = angular.fromJson(angular.toJson(this.recordCache[lookup.id]));
                        clone["ref"] = lookup.ref;
                        resolve(clone);
                        return;
                    }
                    resolve(this.recordCache[lookup.id]);
                    return;
                } else if (this.byIDPromises[lookup.id]) {
                    // if (lookup.force) {
                    //     // Cancel the existing HHR request
                    //     try {
                    //         this.byIDPromises[lookup.id].abort();
                    //     } catch (Error) {
                    //         // Do Nothing
                    //     }
                    // } else {
                        resolve(this.byIDPromises[lookup.id]);
                        return;
                    // }
                }
                const body = <api.GetProductByIDDto>{
                    StoreID: lookup.storeID
                };
                if (lookup.previewID) {
                    body.PreviewID = lookup.previewID;
                }
                this.byIDPromises[lookup.id] = this.$q<{ [key: number]: ng.IHttpPromiseCallbackArg<api.ProductModel> }>((resolve, reject) => {
                    this.cvPromiseFactory.addTimeoutToPromises(
                        10*1000,
                        3,
                        {
                            key: String(lookup.id),
                            promiseFn: () => this.getByIDPromise(lookup.id, body)
                        })
                        .then(resolve)
                        .catch(reject);
                }).then(r => {
                    if (!r || !r[String(lookup.id)] || !r[String(lookup.id)].data) {
                        reject("ERROR! Unable to locate product by ID");
                        return;
                    }
                    const pre = r[String(lookup.id)].data as api.ProductModel;
                    // Fix date formats for Available Start/End
                    if (pre.AvailableStartDate) {
                        pre.AvailableStartDate = new Date(pre.AvailableStartDate.toString());
                    }
                    if (pre.AvailableEndDate) {
                        pre.AvailableEndDate = new Date(pre.AvailableEndDate.toString());
                    }
                    // If the images came with it, we need to ensure the Primary is in the data at
                    // first position of the array
                    if (pre.Images && pre.Images.length > 1) {
                        pre.Images = pre.Images.sort((a, b) => {
                            if (a.IsPrimary && !b.IsPrimary) {
                                return -1;
                            }
                            if (!a.IsPrimary && b.IsPrimary) {
                                return 1;
                            }
                            // Both are either primary or not primary
                            return 0;
                        });
                    }
                    // We need to trust several values that could contain HTML from the server
                    if (pre.Description) {
                        pre.Description = this.$sce.trustAsHtml(pre.Description);
                    }
                    if (pre.SerializableAttributes) {
                        Object.keys(pre.SerializableAttributes)
                            .forEach(key => pre.SerializableAttributes[key].Value
                                = this.$sce.trustAsHtml(pre.SerializableAttributes[key].Value));
                    }
                    // Save it so we don't have to look again
                    this.recordCache[lookup.id] = this.cvPricingService.factoryAssignWithUOMs(pre);
                    // Return the good item after getting the inventory info
                    const productWI = this.appendInventoryInfo(this.recordCache[lookup.id]);
                    if (!productWI) {
                        reject("Undefined after appending inventory info");
                        return;
                    }
                    this.recordCache[lookup.id] = productWI;
                    if (lookup.ref) { // Used in sales collection (carts, orders, etc.) append product data calls
                        const clone = angular.fromJson(angular.toJson(this.recordCache[lookup.id])) as api.ProductModel;
                        clone["ref"] = lookup.ref;
                        delete clone.readInventory;
                        delete clone["$_rawInventory"];
                        delete clone.readPrices;
                        delete clone["$_rawPrices"];
                        resolve(clone);
                        return;
                    }
                    resolve(this.recordCache[lookup.id]);
                }).catch(reject);
            });
        }
        bulkGet(productIDs: number[], storeID: number = null): ng.IPromise<api.ProductModel[]> {
            if (!productIDs || !productIDs.length) {
                return this.$q.reject("No IDs to search with");
            }
            const have: number[] = [];
            const have2: api.ProductModel[] = [];
            const need: number[] = [];
            productIDs.forEach(x => {
                if (!this.recordCache[x]) {
                    need.push(x);
                    return;
                }
                have.push(x);
                have2.push(this.recordCache[x]);
            });
            if (!need.length) {
                // We already have all of these products
                return this.$q.resolve(have2);
            }
            return this.$q((resolve, reject) => {
                /* NOTE: This causes issue with too much data pulled at once from the server and redis caching
                // Using alternative version where it asks for each product individually
                const dto = <api.GetProductsByIDsDto>{
                    IDs: need,
                };
                if (storeID) {
                    dto.StoreID = storeID;
                }
                this.getByIDsPromise(dto).then(r => {
                    if (!r || !r.data || !r.data.length) {
                        reject("No data returned");
                        return;
                    }
                    this.cvPricingService.bulkFactoryAssign(r.data)
                        .then(r2 => this.cvInventoryService.bulkFactoryAssign(r2)
                            .then(r3 => {
                                const final: api.ProductModel[] = [];
                                final.push(...have2);
                                r3.forEach(x => {
                                    this.recordCache[x.ID] = x;
                                    if (x.Name) {
                                        this.nameToIDLookup[x.Name] = x.ID;
                                    }
                                    if (x.CustomKey) {
                                        this.keyToIDLookup[x.CustomKey] = x.ID;
                                    }
                                    if (x.SeoUrl) {
                                        this.seoUrlToIDLookup[x.SeoUrl] = x.ID;
                                    }
                                    final.push(x);
                                });
                                resolve(final);
                            }).catch(reject)
                        ).catch(reject);
                }).catch(reject);
                */
                /*
                // NOTE: This method has issues where-in if any one promise in the chain fails,
                // then the entire catalog doesn't load
                // Alternative method: one at a time, but individually cached to memory
                this.$q.all(need.map(x => this.get({ id: x }))).then((rarr: api.ProductModel[]) => {
                    if (!rarr || !rarr || !rarr.length) {
                        reject("No data returned");
                        return;
                    }
                    this.cvPricingService.bulkFactoryAssign(rarr)
                        .then(r2 => this.cvInventoryService.bulkFactoryAssign(r2)
                            .then(r3 => {
                                const final: api.ProductModel[] = [];
                                final.push(...have2);
                                r3.forEach(x => {
                                    this.recordCache[x.ID] = x;
                                    if (x.Name) {
                                        this.nameToIDLookup[x.Name] = x.ID;
                                    }
                                    if (x.CustomKey) {
                                        this.keyToIDLookup[x.CustomKey] = x.ID;
                                    }
                                    if (x.SeoUrl) {
                                        this.seoUrlToIDLookup[x.SeoUrl] = x.ID;
                                    }
                                    final.push(x);
                                });
                                resolve(final);
                            }).catch(reject)
                        ).catch(reject);
                }).catch(reject);
                */
                // Alternative method #2: Create a timeout for a set of indexed promises so that
                // if any promises aren't fully resolved by the timeout, they will be clipped
                // and, if allowed, retried, otherwise ignored and just return the results which
                // we did get
                console.debug("cvProductService.bulkGet.have:", have);
                console.debug("cvProductService.bulkGet.askingFor:", need);
                this.cvPromiseFactory.addTimeoutToPromises(
                    10 * 1000, // timeout after x ms
                    3, // Retries allowed for failed/timed out promises
                    ...need.map(x => {
                        return {
                            key: `cvProductService-${x}`, // index it so results are coallated
                            promiseFn: () => this.get({ id: x }),
                            retryPromiseFn: () => this.get({ id: x })
                        };
                    })
                ).then(indexedResults => {
                    const products: api.ProductModel[] = Object.keys(indexedResults).map(key => {
                        console.debug("cvProductService.bulkGet.indexedResults:", key, indexedResults[key]);
                        return indexedResults[key];
                    });
                    const final: api.ProductModel[] = [];
                    final.push(...have2);
                    final.push(...products);
                    console.debug("cvProductService.bulkGet.final:", final);
                    resolve(final);
                }).catch(reject);
            });
        }
        appendToSalesItems(salesItems: any[]): ng.IPromise<any[]> {
            return this.$q((resolve, reject) => {
                this.$q.all(
                    _.filter(salesItems, x => x.ProductID)
                        .map(x => this.get({ id: x.ProductID, ref: x.ID }))
                ).then((products: any[]) => {
                    if (_.some(products, x => !x)) {
                        // Came back null for some reason, retry
                        resolve(this.appendToSalesItems(salesItems));
                        return;
                    }
                    for (let i = 0; i < products.length; i++) {
                        if (!products[i]) {
                            // Came back null for some reason, retry?
                            continue;
                        }
                        if (products[i]["ref"]) {
                            const salesItem = _.find(salesItems,
                                x => x.ID == products[i]["ref"]) as api.SalesItemBaseModel<api.AppliedDiscountBaseModel>;
                            if (salesItem) {
                                const product = products[i] as api.ProductModel;
                                salesItem["Product"] = product;
                                salesItem.ProductKey = product.CustomKey;
                                salesItem.ProductName = product.Name;
                                salesItem.ProductPrimaryImage = product.PrimaryImageFileName;
                                salesItem.ProductDescription = product.Description;
                                salesItem.ProductSeoUrl = product.SeoUrl;
                                salesItem.ProductIsUnlimitedStock = product.IsUnlimitedStock;
                                salesItem.ProductAllowBackOrder = product.AllowBackOrder;
                                salesItem.ProductAllowPreSale = product.AllowPreSale;
                                salesItem.ProductIsEligibleForReturn = product.IsEligibleForReturn;
                                salesItem.ProductRestockingFeePercent = product.RestockingFeePercent;
                                salesItem.ProductRestockingFeeAmount = product.RestockingFeeAmount;
                                salesItem.ProductNothingToShip = product.NothingToShip;
                                salesItem.ProductIsTaxable = product.IsTaxable;
                                salesItem.ProductTaxCode = product.TaxCode;
                                salesItem.ProductShortDescription = product.ShortDescription;
                                salesItem.ProductUnitOfMeasure = product.UnitOfMeasure;
                                salesItem.ProductSerializableAttributes = product.SerializableAttributes;
                                salesItem.ProductTypeID = product.TypeID;
                                salesItem.ProductTypeKey = product.TypeKey;
                                salesItem.ProductIsDiscontinued = product.IsDiscontinued;
                                salesItem.ProductMinimumPurchaseQuantity = product.MinimumPurchaseQuantity;
                                salesItem.ProductMinimumPurchaseQuantityIfPastPurchased = product.MinimumPurchaseQuantityIfPastPurchased;
                                salesItem.ProductMaximumPurchaseQuantity = product.MaximumPurchaseQuantity;
                                salesItem.ProductMaximumPurchaseQuantityIfPastPurchased = product.MaximumPurchaseQuantityIfPastPurchased;
                            }
                        }
                        const salesItemsWithThisProductID = _.filter(salesItems,
                            x => x.ProductID == products[i].ID && (!products[i]["ref"] || products[i]["ref"] !== x.ID));
                        for (let j = 0; j < salesItemsWithThisProductID.length; j++) {
                            const salesItem = salesItemsWithThisProductID[j] as api.SalesItemBaseModel<api.AppliedDiscountBaseModel>;
                            const product = products[i] as api.ProductModel;
                            salesItem["Product"] = product;
                            salesItem.ProductKey = product.CustomKey;
                            salesItem.ProductName = product.Name;
                            salesItem.ProductPrimaryImage = product.PrimaryImageFileName;
                            salesItem.ProductDescription = product.Description;
                            salesItem.ProductSeoUrl = product.SeoUrl;
                            salesItem.ProductIsUnlimitedStock = product.IsUnlimitedStock;
                            salesItem.ProductAllowBackOrder = product.AllowBackOrder;
                            salesItem.ProductAllowPreSale = product.AllowPreSale;
                            salesItem.ProductIsEligibleForReturn = product.IsEligibleForReturn;
                            salesItem.ProductRestockingFeePercent = product.RestockingFeePercent;
                            salesItem.ProductRestockingFeeAmount = product.RestockingFeeAmount;
                            salesItem.ProductNothingToShip = product.NothingToShip;
                            salesItem.ProductIsTaxable = product.IsTaxable;
                            salesItem.ProductTaxCode = product.TaxCode;
                            salesItem.ProductShortDescription = product.ShortDescription;
                            salesItem.ProductUnitOfMeasure = product.UnitOfMeasure;
                            salesItem.ProductSerializableAttributes = product.SerializableAttributes;
                            salesItem.ProductTypeID = product.TypeID;
                            salesItem.ProductTypeKey = product.TypeKey;
                            salesItem.ProductIsDiscontinued = product.IsDiscontinued;
                            salesItem.ProductMinimumPurchaseQuantity = product.MinimumPurchaseQuantity;
                            salesItem.ProductMinimumPurchaseQuantityIfPastPurchased = product.MinimumPurchaseQuantityIfPastPurchased;
                            salesItem.ProductMaximumPurchaseQuantity = product.MaximumPurchaseQuantity;
                            salesItem.ProductMaximumPurchaseQuantityIfPastPurchased = product.MaximumPurchaseQuantityIfPastPurchased;
                        }
                    }
                    resolve(salesItems);
                }).catch(reject);
            });
        }
        factoryAssign(product: api.ProductModel): api.ProductModel {
            if (angular.isFunction(product.readSortOrder)) {
                // Already processed
                return product;
            }
            product["$_rawSortOrder"] = this.genBlankSortObj();
            product.readSortOrder = () => product["$_rawSortOrder"];
            const sortOrder = product["$_rawSortOrder"] as api.CalculatedSortOrder;
            sortOrder.order = product.SortOrder ? product.SortOrder : null;
            sortOrder.loading = false;
            product["$_rawSortOrder"] = sortOrder;
            return product;
        }
        private appendInventoryInfo(product: api.ProductModel): api.ProductModel {
            // TODO@JTG: Rework with Restrictions Schema
            // if (product.SerializableAttributes
            //     && product.SerializableAttributes["SKU-Restrictions"]
            //     && product.SerializableAttributes["SKU-Restrictions"].Value
            //         .indexOf('"RestrictShipFlag":"Y"') !== -1) {
            //     product.inventoryObject.restrictShipFlag = true;
            // }
            this.cvInventoryService.factoryAssignWithUOMs(product);
            // Check for associated products data, which will need their own copy of this same action
            if (!product.ProductAssociations
                || !product.ProductAssociations.filter(x => x.Slave).length) {
                return product;
            }
            this.cvInventoryService.bulkFactoryAssign(
                product.ProductAssociations
                    .filter(x => x.Slave)
                    .map(x => x.Slave));
            return product;
        }
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly $sce: ng.ISCEService,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvInventoryService: IInventoryService,
                protected readonly cvPricingService: IPricingService,
                protected readonly cvPromiseFactory: factories.IPromiseFactory,
                private readonly $rootScope: ng.IRootScopeService) {
            super($q, $sce, cvPromiseFactory);
        }
    }
}
