// <copyright file="bulkUploadService.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>bulk upload service class</summary>
module cef.store.bulkUpload {
    export class BulkUploadItem {
        totalPrice: number = null;
        hasPrice: boolean;
        isDiscountPrice: boolean;
        totalQuantity = 0;
        // Functions
        updateValues(): void {
            this.totalQuantity = 0;
            // Clean up quantity values and get total
            if (angular.isNumber(this.orderQuantity) && this.orderQuantity >= 0) {
                this.totalQuantity += this.orderQuantity;
            } else {
                this.orderQuantity = 0;
            }
            if (angular.isNumber(this.quoteQuantity) && this.quoteQuantity >= 0) {
                this.totalQuantity += this.quoteQuantity;
            } else {
                this.quoteQuantity = 0;
            }
            // Calculate total price based on ORDER QUANTITY only
            if (!this.hasPrice) {
                return;
            }
            this.totalPrice = 0;
            if (this.isDiscountPrice) {
                this.totalPrice += this.orderQuantity * this.discountPrice;
            } else {
                this.totalPrice += this.orderQuantity * this.originalPrice;
            }
        }
        // Constructor
        constructor(
                public productKey: string,
                public name: string,
                public locationId: number,
                public locationName: string,
                public originalPrice: number,
                public discountPrice: number,
                public requestedQuantity: number,
                public stockQuantity: number,
                public orderQuantity: number,
                public quoteQuantity: number) {
            // Check values given
            this.requestedQuantity = angular.isNumber(requestedQuantity) && requestedQuantity >= 0
                ? requestedQuantity
                : 0;
            this.orderQuantity = angular.isNumber(orderQuantity) && orderQuantity >= 0
                ? orderQuantity
                : 0;
            this.quoteQuantity = angular.isNumber(quoteQuantity) && quoteQuantity >= 0
                ? quoteQuantity
                : 0;
            if (angular.isNumber(originalPrice) || angular.isNumber(discountPrice)) {
                this.hasPrice = true;
                this.isDiscountPrice = angular.isNumber(discountPrice)
                    && discountPrice !== originalPrice;
            } else {
                this.hasPrice = false;
                this.isDiscountPrice = false;
                this.originalPrice = null;
                this.discountPrice = null;
            }
            if (originalPrice < 0) {
                originalPrice = 0;
            }
            if (discountPrice < 0) {
                discountPrice = 0;
            }
            if (this.hasPrice) {
                this.originalPrice = originalPrice;
                if (this.isDiscountPrice) {
                    this.discountPrice = discountPrice;
                }
            }
            this.updateValues();
        }
    }

    export class ProductAvailability {
    }

    export class BulkUploadService {
        // Properties
        viewstate = {
            bulkIsProcessing: false
        }
        items: BulkUploadItem[] = [];
        // Functions
        addItem(key: string, quantity: number): void {
            if (!angular.isNumber(quantity) || quantity <= 0) { return; }
            // Look up product from backend.
            this.getProduct(key).then(product => {
                if (product == null || !product.Name) {
                    this.cvMessageModalFactory(this.$translate("ui.storefront.bulkUpload.buildUploadService.ProductNotFound"));
                    return;
                }
                let itemsAdded = false;
                // Get inventory info.
                this.$q.all([
                    this.getInventoryInfo(product.ID),
                    this.getInventoryLocations(),
                    this.getInventorySections()
                ]).then(results => {
                    const inventories = <api.ProductInventoryLocationSectionPagedResults>results[0];
                    const locations = <api.InventoryLocationPagedResults>results[1];
                    const sections = <api.InventoryLocationSectionPagedResults>results[2];
                    let inventoryLocations: api.InventoryLocationModel[];
                    let inventorySections: api.InventoryLocationSectionModel[] = [];
                    const quantityRequested = quantity;
                    // Filter inventories for this product.
                    const productInventories = inventories.Results.filter(inventory => inventory.MasterID === product.ID);
                    // Filter sections with inventory for this product.
                    if (productInventories.length > 0) {
                        inventorySections = sections.Results
                            .filter(x => productInventories.map(y => y.SlaveID).indexOf(x.ID) !== -1);
                    }
                    // Filter locations based on sections
                    if (inventorySections.length > 0) {
                        inventoryLocations = locations.Results
                            .filter(x => inventorySections.map(y => y.InventoryLocationID).indexOf(x.ID) !== -1);
                    }
                    // Get the unique inventory locations.
                    const locationIds = inventorySections
                        .map(section => section.InventoryLocationID)
                        .filter((v, i) => locationIds.indexOf(v) === i);
                    // For distributing order quantity over multiple locations.
                    let quantityNeeded = quantityRequested;
                    // Loop through each unique location.
                    locationIds.forEach(locationId => {
                        const location = locations.Results.filter(l => l.ID === locationId)[0];
                        const locationSectionIds = inventorySections
                            .filter(x => x.InventoryLocationID === locationId)
                            .map(x => x.ID);
                        const inventoriesThisLocation = productInventories
                            .filter(x => locationSectionIds.indexOf(x.SlaveID) !== -1);
                        let quantityAvailable = 0; // Starting available quantity for this location.
                        let quantityOrder: number;
                        const quantityQuote = 0;
                        // Loop through each section to accumulate available quantities.
                        inventoriesThisLocation.forEach(x => {
                            quantityAvailable += (x.Quantity || 0) - (x.QuantityAllocated || 0);
                        });
                        // Take as much quantity as possible from this location.
                        if (quantityNeeded <= quantityAvailable) {
                            quantityOrder = quantityNeeded;
                            quantityNeeded = 0;
                        } else {
                            quantityOrder = quantityAvailable;
                            quantityNeeded = quantityNeeded - quantityAvailable;
                        }
                        itemsAdded = true;
                        let prices: api.CalculatedPrices = { base: null, loading: true };
                        if (angular.isFunction(product.readPrices)) {
                            prices = product.readPrices();
                        }
                        this.items.push(new BulkUploadItem(
                            product.CustomKey,
                            product.Name,
                            locationId,
                            location.Name,
                            prices.base,
                            prices.sale,
                            quantityRequested,
                            quantityAvailable,
                            quantityOrder,
                            quantityQuote
                        ));
                    }, this);
                    // If no inventory available add an item with zero quantity and no location info.
                    if (!itemsAdded) {
                        let prices: api.CalculatedPrices = { base: null, loading: true };
                        if (angular.isFunction(product.readPrices)) {
                            prices = product.readPrices();
                        }
                        this.items.push(new BulkUploadItem(
                            product.CustomKey,
                            product.Name,
                            0,
                            "--",
                            prices.base,
                            prices.sale,
                            quantityRequested,
                            0,
                            0,
                            0
                        ));
                    }
                });
            });
        }

        getProduct(key: string): ng.IPromise<api.ProductModel> {
            return this.$q((resolve, reject) => {
                this.cvProductService.get({ key: key })
                    .then(resolve)
                    .catch(reject);
            });
        }

        getInventoryLocations(): ng.IPromise<api.InventoryLocationPagedResults> {
            return this.$q((resolve, reject) => {
                this.cvApi.inventory.GetInventoryLocations({ Active: true, AsListing: true })
                    .then(r => resolve(r.data))
                    .catch(reject);
            });
        }

        getInventorySections(): ng.IPromise<api.InventoryLocationSectionPagedResults> {
            return this.$q((resolve, reject) => {
                this.cvApi.inventory.GetInventoryLocationSections({ Active: true, AsListing: true })
                    .then(r => resolve(r.data))
                    .catch(reject);
            });
        }

        getInventoryInfo(id: number): ng.IPromise<api.ProductInventoryLocationSectionPagedResults> {
            return this.$q((resolve, reject) => {
                this.cvApi.products.GetProductInventoryLocationSections({
                    Active: true,
                    AsListing: true,
                    ProductID: id
                }).then(r => resolve(r.data))
                .catch(reject);
            });
        }

        updateItem(productKey: string, locationId: number) {
            const item = this.items
                .filter(x => x.productKey === productKey
                          && x.locationId === locationId)[0];
            item.updateValues();
        }

        importItems(): void {
            // Accept data already parsed by backend or take the file uploaded and
            // send it to the backend from here, then add the results.
            // ...
        }
        // Constructor
        constructor(
            private readonly $q: ng.IQService,
            private readonly $translate: ng.translate.ITranslateService,
            private readonly cvApi: api.ICEFAPI,
            private readonly cvProductService: services.IProductService,
            private readonly cvMessageModalFactory: modals.IMessageModalFactory) { }
    }

    cefApp.service("$bulkUpload", BulkUploadService);
}
