/**
 * @file framework/store/product/controls/cards/productCard5AttributesWidget.ts
 * @author Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
 * @desc Product Card Sub-module #5: Attributes widget
 */
module cef.store.product.controls.cards {
    class ProductCardAttributesGenWidgetController extends ProductCardGenWidgetControllerBase {
        // Bound Scope Properties
        // <See inherited>
        // Properties
        // <See inherited>
        catalogAttrList: api.GeneralAttributeModel[];
        // Convenience Redirects (Reduce binding text/conditions)
        // <See inherited>
        // Functions
        // <See inherited>
        productAttr(key: string): api.SerializableAttributeObject {
            return this.product
                && this.product.SerializableAttributes
                && this.product.SerializableAttributes[key];
        }
        resolveAttributes(): ng.IPromise<void> {
            return this.$q((resolve, reject) => {
                if (!this.cvSearchCatalogService.allAttributes) {
                    console.warn("Search Catalog Attributes not ready!");
                    reject();
                    return;
                }
                this.catalogAttrList = this.cvSearchCatalogService.allAttributes
                    .filter(x => !x.HideFromCatalogViews)
                    .filter(x => Object.keys(this.product.SerializableAttributes || {})
                                    .some(y => y === x.CustomKey));
                resolve();
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $q: ng.IQService,
                readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvSearchCatalogService: services.SearchCatalogService) {
            super(cefConfig, cvAuthenticationService, cvProductService, cvSearchCatalogService);
            const unbind1 = $scope.$on(cvServiceStrings.events.attributes.ready,
                () => this.resolveAttributes());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
            $timeout(() => this.resolveAttributes(), 250); // Fire anyway in case it's ready
            this.load();
        }
    }

    cefApp.directive("cefProductCardAttributesGenWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "=?",
            productId: "=?",
            productSeoUrl: "=?",
            productName: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCard5AttributesWidget.html", "ui"),
        controller: ProductCardAttributesGenWidgetController,
        controllerAs: "pcawCtrl",
        bindToController: true
    }));
}
