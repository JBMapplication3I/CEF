/**
 * @file framework/store/searchCatalog/controls/cardForProducts/productCard2SkuWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #2: SKU widget
 */
module cef.store.searchCatalog.controls.results {
    class ProductCardSkuWidgetController extends ProductCardWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        hideSku: boolean;
        hideLabels: boolean;
        left: boolean;
        useMnfNumber: boolean;
        displayHCPC: boolean = false;
        uomArray: api.SerializableAttributeObject[] = [];
        uomSelections: any[] = [];
        // Properties
        // <See inherited>
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
        protected get hideK(): boolean {
            return Boolean(this.hideSku)
                 || false; // Default
        }
        protected get manufacturer(): string{
            return this.product?.SerializableAttributes
                && this.product.SerializableAttributes["Manufacturer"]?.Value
        }
        checkIfUserCanViewInventoryAndHCPC(): ng.IPromise<void> {
            return this.$q((resolve) => {
                this.cvAuthenticationService.getCurrentUserPromise().then(user => {
                    if (!user.userID || !user.SerializableAttributes) return;
                    this.displayHCPC = user.SerializableAttributes["CanViewHcpc"]?.Value?.toLowerCase() === "true" ? true : false;
                    resolve();
                });
            });
        }
        loadProductUOM(): void {
            if (this.product?.SerializableAttributes
                && this.product.SerializableAttributes["AvailableUOMs"]?.Value) {
                let uoms = this.product.SerializableAttributes["AvailableUOMs"]?.Value.toString().split(",");
                uoms.forEach(uom => {
                    try {
                        let temp = this.product.SerializableAttributes[uom];
                        temp.Value = this.product.$_rawPricesUOMs[temp.Key]["SalePrice"].toString();
                        this.uomArray.push(temp);
                    } catch (err) {
                        return;
                    }
                });
                let i = 0;
                this.uomArray.sort((a, b) => parseInt(a.Value) - parseInt(b.Value))
                this.uomArray.forEach(x => {
                    this.uomSelections.push({
                        "ID": x.Value.concat("|", x.Key).concat("|", this.product.ID.toString()),
                        "Name": x.Key,
                        "SortOrder": i++
                    });
                });
                this.cvProductService.productUOMSetInitalValue(this.uomArray, this.productId);
            }
        }
        // Functions
        // <See inherited>
        // Events
        load(): void {
            const unbind1 = this.$rootScope.$watch(() => this.product && this.product.$_rawPricesUOMs, (newValue, oldValue) => {
                if (!_.isObject(oldValue) && !_.isObject(newValue)) {
                    return;
                }
                this.loadProductUOM();
            });
            this.$rootScope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
        // Constructor
        constructor(
                protected readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService,
                protected readonly $rootScope: ng.IRootScopeService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
            this.checkIfUserCanViewInventoryAndHCPC()
            this.load();
        }
    }

    cefApp.directive("cefProductCardSkuWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            productId: "=?",
            hideSku: "=?",
            hideLabels: "=?",
            uomSelections: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/searchCatalog/controls/cardForProducts/productCard2SkuWidget.html", "ui"),
        controller: ProductCardSkuWidgetController,
        controllerAs: "pckwCtrl",
        bindToController: true
    }));
}
