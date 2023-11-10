module cef.store.cart.controls {
    class QuickCaptureModalController extends core.TemplatedControllerBase {
        // Properties
        captureText: string; // Populated by UI
        // Functions
        ok(): void {
            this.$uibModalInstance.close(this.captureText);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Constructor
        constructor(
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    class ManualOrderFormController extends core.TemplatedControllerBase {
        // Properties
        flyoutCount: number; // Bound by Scope
        useStores: boolean; // Bound by Scope
        private vendorViewValue: string; // Populated by Input
        private currentOrder: api.SalesOrderModel = {} as api.SalesOrderModel;
        get vendorIsSelected(): boolean { return this._vendor && typeof(this._vendor) !== "string"; }
        private _vendor: api.VendorModel | api.StoreModel = null; // Populated by Directive
        get vendor(): api.VendorModel | api.StoreModel { return this._vendor; } // Populated by Directive
        set vendor(value: api.VendorModel | api.StoreModel) { // Populated by Directive
            this._vendor = value;
            if (this.vendorIsSelected && (!this.items || !this.items.length)) {
                if (!this.items) { this.items = []; }
                this.addItem();
            }
        }
        private vendors: Array<api.VendorModel | api.StoreModel>; // Populated by Directive
        private vendorsProducts: { [index: number]: Array<api.ProductModel> } = { }; // Populated by Directive
        private items: Array<api.SalesItemBaseModel<api.AppliedDiscountBaseModel>> = []; // Populated by Input
        // Functions
        private errorFinishAndReject(reason: string, reject: (...args) => void) {
            this.finishRunning(true, reason);
            reject(reason);
        }
        grabVendors(search: string): ng.IPromise<api.VendorModel[]> {
            this.setRunning();
            return this.$q((resolve, reject) => {
                if (Boolean(this.useStores)) {
                    this.cvApi.stores.GetStores({
                        Active: true,
                        AsListing: true,
                        IDOrCustomKeyOrName: search,
                        Paging: <api.Paging>{ Size: this.flyoutCount || 8, StartIndex: 1 }
                    }).then(r => {
                        this.vendors = r.data.Results;
                        this.finishRunning();
                        resolve(this.vendors);
                    }, result => this.errorFinishAndReject(result, reject))
                    .catch(reason => this.errorFinishAndReject(reason, reject));
                    return;
                }
                this.cvApi.vendors.GetVendors(<api.GetVendorsDto>{
                    Active: true,
                    AsListing: true,
                    IDOrCustomKeyOrName: search,
                    Paging: <api.Paging>{ Size: this.flyoutCount || 8, StartIndex: 1 }
                }).then(r => {
                    this.vendors = r.data.Results;
                    this.finishRunning();
                    resolve(this.vendors);
                }, result => this.errorFinishAndReject(result, reject))
                .catch(reason => this.errorFinishAndReject(reason, reject));
            });
        }
        grabVendorsProducts(search: string, index: number): ng.IPromise<api.ProductModel[]> {
            this.setRunning();
            return this.$q((resolve, reject) => {
                const dto = <api.GetProductsDto>{
                    IDOrCustomKeyOrName: search,
                    Active: true,
                    AsListing: true,
                    IsVisible: true,
                    IsDiscontinued: false,
                    Paging: <api.Paging>{ Size: this.flyoutCount || 8, StartIndex: 1 }
                };
                if (Boolean(this.useStores)) {
                    dto.StoreID = this.vendor.ID;
                } else {
                    dto.VendorID = this.vendor.ID;
                }
                this.cvApi.products.GetProducts(dto).then(r => {
                    this.vendorsProducts[index] = r.data.Results;
                    this.finishRunning();
                    resolve(this.vendorsProducts[index]);
                }).catch(reason => this.errorFinishAndReject(reason, reject));
            });
        }
        addItem(): void {
            this.items.push({
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                Quantity: 1,
                Sku: null,
                UnitCorePrice: null,
                ExtendedPrice: null,
                ItemType: api.ItemType.Item
            });
        }
        remove(index: number): void {
            this.items.splice(index, 1);
        }
        openQuickCaptureModal(): void {
            this.$uibModal.open({
                size: "md",
                templateUrl: this.$filter("corsLink")("/framework/store/cart/controls/modals/quickCaptureModal.html", "ui"),
                controller: QuickCaptureModalController,
                controllerAs: "quickCaptureModalCtrl"
            }).result.then((result: string) => {
                if (!result) { return; }
                this.setRunning();
                const rows = this.csvToArray(result);
                const indexKey = 0;
                const indexPrice = 1;
                const indexQuantity = 2;
                const indexName = 3;
                rows.forEach(row => {
                    var existing = _.find(this.items, x => x.ProductKey === row[0]);
                    if (existing) {
                        // We can update what is there and stop
                        existing.UnitSoldPrice = Number(row[indexPrice]);
                        existing.Quantity = Number(row[indexQuantity]);
                        return;
                    }
                    // We need to append to the list
                    this.addItem();
                    var index = this.items.length - 1;
                    this.cvApi.products.CheckProductExistsByKey(row[indexKey]).then(r => {
                        if (!r || !r.data) {
                            // This item doesn't exist, append as a dummy record
                            this.items[index].ProductID = null;
                            this.items[index].Sku = row[indexKey];
                            this.items[index].ProductKey = this.vendor.CustomKey + "_CUSTOM";
                            this.items[index]["isDummy"] = true;
                            this.items[index].UnitCorePrice = Number(row[indexPrice]);
                            this.items[index].UnitSoldPrice = Number(row[indexPrice]);
                            this.items[index].Quantity = Number(row[indexQuantity]);
                            this.items[index].Name = row[indexName];
                            this.items[index].ForceUniqueLineItemKey = this.vendor.CustomKey + "_" + this.items[index].Sku + "|" + this.items[index].Name;
                            return;
                        }
                        // This item exists, act like we would for item select from the typeahead
                        this.cvApi.products.GetProductByID(r.data).then(r => {
                            this.items[index]["isDummy"] = false;
                            this.onItemSelect(r.data, row[indexKey], null, null, index);
                            this.items[index].UnitSoldPrice = Number(row[indexPrice]);
                            this.items[index].Quantity = Number(row[indexQuantity]);
                        }, result => this.finishRunning(true, result))
                        .catch(reason => this.finishRunning(true, reason));
                    }, result => this.finishRunning(true, result))
                    .catch(reason => this.finishRunning(true, reason));
                });
            });
        }
        /**
         * ref: http://stackoverflow.com/a/1293163/2343
         * This will parse a delimited string into an array of
         * arrays. The default delimiter is the comma, but this
         * can be overridden in the second argument.
         */
        private csvToArray(strData: string, strDelimiter: string = null): string[][] {
            // Check to see if the delimiter is defined. If not, then default to comma
            strDelimiter = (strDelimiter || ",");
            // Create a regular expression to parse the CSV values.
            const objPattern = new RegExp((
                    // Delimiters
                    `(\\${strDelimiter}|\\r?\\n|\\r|^)`
                    // Quoted fields
                    + `(?:\"([^\"]*(?:\"\"[^\"]*)*)\"|`
                    // Standard fields
                    + `([^\"\\${strDelimiter}\\r\\n]*))`
                ),
                "gi");
            // Create an array to hold our data. Give the array a default empty first row
            const arrData: string[][] = [[]];
            // Create an array to hold our individual pattern matching groups
            let arrMatches: RegExpExecArray;
            // Keep looping over the regular expression matches until we can no longer find a match
            while ((arrMatches = objPattern.exec(strData))) {
                // Get the delimiter that was found.
                const strMatchedDelimiter = arrMatches[1];
                /* Check to see if the given delimiter has a length (is not the start of string) and
                 * if it matches field delimiter. If id does not, then we know that this delimiter
                 * is a row delimiter. */
                if (strMatchedDelimiter.length && strMatchedDelimiter !== strDelimiter) {
                    // Since we have reached a new row of data, add an empty row to our data array
                    arrData.push([]);
                }
                let strMatchedValue;
                /* Now that we have our delimiter out of the way, let's check to see which kind of
                 * value we captured (quoted or unquoted). */
                if (arrMatches[2] && arrMatches[2] != undefined) {
                    // We found a quoted value. When we capture this value, unescape any double quotes
                    strMatchedValue = arrMatches[2].replace(/\"\"/g, "\"");
                } else {
                    // We found a non-quoted value.
                    strMatchedValue = arrMatches[3];
                }
                // Now that we have our value string, let's add it to the data array
                arrData[arrData.length - 1].push(strMatchedValue);
            }
            // Return the parsed data
            return arrData;
        }
        continue(): void {
            this.setRunning();
            const promises = this.items.map(x => {
                return this.$q((resolve, reject) => {
                    // Override the prices in it
                    x.UnitSoldPriceModifier = x.UnitSoldPrice;
                    x.UnitSoldPriceModifierMode = 1; // Override (use modifier instead)
                    if (x.ProductID) {
                        resolve();
                        return;
                    }
                    // Try again to see if we can or need to fill in data
                    var productKey = this.vendor.CustomKey + "_" + (x.Sku || x.ProductKey);
                    this.cvApi.products.CheckProductExistsByKey(productKey).then(r => {
                        if (!r || !r.data) {
                            // This item doesn't exist, append as a dummy record
                            x.ProductID = null;
                            x.ForceUniqueLineItemKey = productKey + "|" + x.Name;
                            x.ProductKey = this.vendor.CustomKey + "_CUSTOM";
                            x["isDummy"] = true;
                            resolve();
                            return;
                        }
                        // This item exists, act like we would for item select from the typeahead
                        this.cvApi.products.GetProductByID(r.data).then(r2 => {
                            x["isDummy"] = false;
                            delete x.ForceUniqueLineItemKey;
                            x.ProductID = r2.data.ID;
                            x.ProductKey = x.Sku = r2.data.CustomKey;
                            resolve();
                        }, result => reject(result)).catch(reason => reject(reason));
                    }, result => reject(result)).catch(reason => reject(reason));
                });
            });
            this.$q.all(promises).then(() => {
                var dto = <api.AddCartItemsDto>{
                    TypeName: this.cvServiceStrings.carts.types.quote,
                    Items: this.items
                };
                if (this.useStores) {
                    dto.StoreID = this.vendor.ID;
                }
                this.cvApi.shopping.AddCartItems(dto).then(r => {
                    if (!r || !r.data || !r.data.ActionSucceeded) {
                        this.finishRunning(true, null, r.data.Messages);
                        return;
                    }
                    // Start Checkout
                    this.$filter("goToCORSLink")(
                        "",
                        "checkout",
                        "primary",
                        false,
                        {
                            "type": this.cvServiceStrings.carts.types.quote,
                            "quoteToOrder": true,
                            "storedFiles": this.currentOrder.StoredFiles ? this.currentOrder.StoredFiles.map(x => x.Name) : null
                        });
                });
            }, result => this.finishRunning(true, result))
            .catch(reason => this.finishRunning(true, reason));
        }
        // Events
        onItemSelect($item: api.ProductModel, $model: string, $label: string, $event: ng.IAngularEvent, index: number): void {
            if (!$item) {
                // TODO: Blank the values or reset to the newProduct() so we can populate manually?
                return;
            }
            this.items[index].ProductID = $item.ID;
            this.items[index].Sku = this.items[index].ProductKey = $item.CustomKey;
            delete this.items[index].ForceUniqueLineItemKey;
            this.items[index].ProductSeoUrl = $item.SeoUrl;
            this.items[index].ProductPrimaryImage = $item.PrimaryImageFileName;
            this.items[index].ProductDescription = $item.ShortDescription;
            let prices: api.CalculatedPrices = { base: null, loading: true };
            if (angular.isFunction($item.readPrices)) {
                prices = $item.readPrices();
            }
            this.items[index].UnitCorePrice = prices.base;
            if (!this.items[index].UnitSoldPrice) {
                this.items[index].UnitSoldPrice = prices.isSale ? prices.sale : prices.base;
            }
            if (!this.items[index].Name) { this.items[index].Name = $item.Name; }
        }
        inputKeyPress(event: JQueryKeyEventObject, index: number): void { // Angular returns this type of object per their docs
            const eventCode = event.charCode ? event.charCode : event.which;  // Enable this functionality to work in Firefox
            if (eventCode !== 13) { return; } // Only do anything if it was the enter key
            if (!this.vendorViewValue) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
        }
        onProductKeyChanged($event: ng.IAngularEvent, index: number): void {
            this.items[index].Sku = this.items[index].ProductKey.replace(this.vendor.CustomKey + "_", "");
        }
        onNameChanged($event: ng.IAngularEvent, index: number): void {
            if (!this.items[index]["isDummy"]) { return; }
            this.items[index].ForceUniqueLineItemKey = this.vendor.CustomKey + "_" + (this.items[index].Sku || this.items[index].ProductKey) + "|" + this.items[index].Name;
        }
        // Constructor
        constructor(
                private readonly $filter: ng.IFilterService,
                private readonly $q: ng.IQService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService, // Used by UI
                private readonly cvCartService: services.ICartService) {
            super(cefConfig);
            this.cvCartService.clearCart(this.cvServiceStrings.carts.types.quote);
        }
    }

    cefApp.directive("cefManualOrderForm", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { flyoutCount: "=?", useStores: "=?" },
        templateUrl: $filter("corsLink")("/framework/store/cart/controls/manualOrderForm.html", "ui"),
        controller: ManualOrderFormController,
        controllerAs: "manualOrderFormCtrl",
        bindToController: true
    }));
}
