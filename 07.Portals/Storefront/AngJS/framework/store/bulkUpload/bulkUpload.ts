module cef.store.bulkUpload {
    export class BulkUploadController extends core.TemplatedControllerBase {
        // Properties
        items: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[];
        uploadData: {};
        mappingData: any;
        message: string[];
        viewstate = {
            processing: false,
            manual: false,
            upload: false
        };
        productList: api.ProductModel[];
        orderTotal: any;
        useStores: boolean; // Bound by Scope
        flyoutCount: number; // Bound by Scope
        private vendorViewValue: string; // Populated by Input
        private vendors: Array<api.VendorModel | api.StoreModel>; // Populated by Directive
        get vendorIsSelected(): boolean { return this._vendor && typeof(this._vendor) !== "string"; }
        private _vendor: api.VendorModel | api.StoreModel = null; // Populated by Directive
        get vendor(): api.VendorModel | api.StoreModel { return this._vendor; } // Populated by Directive
        set vendor(value: api.VendorModel | api.StoreModel) { // Populated by Directive
            this._vendor = value;
        }
        // Functions
        clearBulkUpload(): void {
            this.cvCartService.clearCart("Quote Cart");
        }
        getTotals(obj: any): number {
            var totals = [];
            // obj.Product.ProductInventoryLocationSections.forEach((loc) => {
            //     totals.push(loc.RequestedQuantity);
            // });
            totals.push(obj.Quantity);
            return obj.total = totals.reduce((a, b) => a + b, 0);
        }
        private errorFinishAndReject(reason: string, reject: (...args) => void) {
            this.finishRunning(true, reason);
            reject(reason);
        }
        grabVendors(search: string): ng.IPromise<api.VendorModel[]> {
            this.setRunning();
            return this.$q((resolve, reject) => {
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
            });
        }
        inputKeyPress(event: JQueryKeyEventObject): void { // Angular returns this type of object per their docs
            const eventCode = event.charCode ? event.charCode : event.which;  // Enable this functionality to work in Firefox
            if (eventCode !== 13) { return; } // Only do anything if it was the enter key
            if (!this.vendorViewValue) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
        }

        getBulkUploadData(): void {
            this.cvCartService.loadCart("Quote Cart", false, "getBulkUploadData").then(response => {
                this.items = response.Result.SalesItems;
                // this.items.forEach(item => {
                //     var first = 0;
                //     item["Product"].ProductInventoryLocationSections.forEach(loc => {
                //         if (first === 0) {
                //             loc["RequestedQuantity"] = item.Quantity;
                //         } else {
                //             loc["RequestedQuantity"] = null;
                //         }
                //         loc["RequestedQuote"] = null;
                //         first++;
                //         return loc["RequestedQuantity"];
                //     });
                // });
                this.items.forEach(item => this.getTotals(item));
                this.viewstate.processing = false;
            }, () => {
                console.error("There was a problem getting the requested cart.");
                this.viewstate.processing = false;
            });
        }

        clearForm(): void {
            this.clearBulkUpload();
            this.items = [];
            this.uploadData = {};
            this.viewstate.manual = true;
        }

        startOver(): void {
            this.clearBulkUpload();
            this.uploadData = {};
            this.viewstate = {
                processing: false,
                manual: false,
                upload: false
            };
        }

        uploadFile(params: api.BulkOrderDto): void {
            this.viewstate.processing = true;
            if (this.vendor) {
                params.CartType = "Quote Cart";
                params.VendorID = this.vendor.ID;
            }
            this.cvApi.shopping.BulkOrder(params).then(response => {
                if (response && response.data.ActionSucceeded) {
                    this.getBulkUploadData();
                    this.viewstate.processing = false;
                    this.viewstate.manual = true;
                    return;
                }
                this.message = response.data.Messages;
                this.viewstate.processing = false;
            }, () => {
                console.error("There was a problem with the request.", params);
                this.viewstate.processing = false;
            });
        }

        loadProductList(search: string): ng.IPromise<api.ProductModel[]> {
            return this.cvApi.products.GetProducts(<api.GetProductsDto>{
                Active: true,
                AsListing: true,
                IDOrCustomKeyOrName: search,
                Paging: <api.Paging>{ Size: 50000, StartIndex: 1 }
            }).then(r => this.productList = r.data.Results);
        }

        manualAdd(id: number, type: string, quantity: number): void {
            this.cvCartService.addCartItem(id, type, quantity).then(() => this.getBulkUploadData());
        }

        addToCart(items: api.SalesItemBaseModel<api.AppliedDiscountBaseModel>[]): void {
            this.setRunning();
            this.cvApi.shopping.AddCartItems({ Items: items as any }).then(r => {
                if(!r.data.ActionSucceeded){
                    this.finishRunning(true, null, r.data.Messages);
                    return;
                }
                this.finishRunning();
                this.$filter("goToCORSLink")("/Cart");
            });
        }

        sendItemsToCart(cartItems: api.SalesItemBaseModel<api.AppliedCartItemDiscountModel>[]): void {
            var purchaseItems = [];
            cartItems.forEach(item => {
                item["Product"].ProductInventoryLocationSections.forEach(location => {
                    if (location["RequestedQuote"]) {
                        const quoteProduct = {
                            ProductID: location.ProductID,
                            Quantity: location["RequestedQuote"],
                            TypeName: this.cvServiceStrings.carts.types.quote,
                            ProductInventoryLocationSectionID: location.InventoryLocationSectionID
                        };
                        purchaseItems.push(quoteProduct);
                    }
                    if (location["RequestedQuantity"]) {
                        const cartProduct = {
                            ProductID: location.ProductID,
                            Quantity: location["RequestedQuantity"],
                            TypeName: this.cvServiceStrings.carts.types.cart,
                            ProductInventoryLocationSectionID: location.InventoryLocationSectionID
                        };
                        purchaseItems.push(cartProduct);
                    }
                });
            });
            this.addToCart(purchaseItems);
        }
        // Constructor
        constructor(
                protected readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $filter: ng.IFilterService,
                protected readonly $window: ng.IWindowService,
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCartService: services.ICartService) {
            super(cefConfig);
            this.uploadData = {};
            this.clearBulkUpload();
            const unbind1 = $scope.$on("BulkUploadFileName", (e, data) => {
                this.consoleLog("data", data)
                this.uploadData["FileName"] = data;
            });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("cefBulkUploadLanding", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/bulkUpload/bulkUploadLanding.html", "ui"),
        link: ($scope: any) => {
            $scope.viewstate = {
                upload: false,
                manual: false,
                progress: false,
                imported: false,
                flyoutCount: "=?"
            }
        },
        controller: BulkUploadController,
        controllerAs: "cefBulkUploadCtrl"
    }));
    cefApp.directive("cefBulkUploadForm", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/bulkUpload/bulkUploadForm.html", "ui"),
    }));
    cefApp.directive("cefBulkUploadWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/bulkUpload/bulkUploadWidget.html", "ui"),
    }));
    cefApp.directive("cefBulkUploadTable", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/bulkUpload/bulkUploadTable.html", "ui"),
    }));
    cefApp.directive("cefBulkUploadManualAdd", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/bulkUpload/bulkUploadManualAdd.html", "ui"),
    }));
}
