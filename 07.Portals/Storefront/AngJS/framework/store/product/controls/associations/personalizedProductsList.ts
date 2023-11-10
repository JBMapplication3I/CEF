/**
 * @file framework/store/product/controls/associations/personalizedProductsList.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Personalization products class
 */
module cef.store.product.controls.associations {
    class PersonalizedProductsListController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        format: string;
        size: number;
        // Properties
        products: Array<api.ProductModel> = [];
        get usersSelectedStore(): api.StoreModel {
            // Read the service memory directly instead of making a copy
            return this.cvStoreLocationService.getUsersSelectedStore();
        }
        // Functions
        load(): void {
            this.cvAuthenticationService.preAuth().finally(() => {
                this.cvApi.products.GetPersonalizationProductsForCurrentUser().then(r => {
                    if (!r || !r.data || !r.data.length) {
                        return;
                    }
                    this.products = r.data;
                });
            });
        }
        // Constructors
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvStoreLocationService: services.IStoreLocationService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("personalizedProductsList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: { size: "=", format: "@" },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/associations/personalizedProductsList.html", "ui"),
        controller: PersonalizedProductsListController,
        controllerAs: "personalizedProductsListCtrl",
        bindToController: true
    }));
}
