module cef.store.product.controls.associations {
    class RelatedProductsListController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        products: api.ProductAssociationModel[];
        size: number;
        type: string;
        includeInvisible: boolean;
        template: string;
        // Properties
        skip = 0;
        paging: core.Paging<api.ProductAssociationModel>;
        thumbWidth = 200;
        thumbHeight = 200;
        get actionButtonView(): string { return "addToCart"; }
        get pricingDisplayStyle(): string { return "sideBySide"; }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            this.paging = new core.Paging<api.ProductAssociationModel>($filter);
            this.paging.pageSize = this.size;
            this.paging.pageSetSize = 100;
            const unbind1 = $scope.$watchCollection(
                () => this.products,
                () => this.products
                    && (this.paging.data = this.products
                        .filter(x => x.TypeName === this.type
                                  && (this.includeInvisible || x.SlaveIsVisible))));
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    cefApp.directive("relatedProductsList", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            products: "=",
            size: "=?",
            type: "@",
            includeInvisible: "=?",
            template: "@?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/associations/relatedProductsList.html", "ui"),
        controller: RelatedProductsListController,
        controllerAs: "rplCtrl",
        bindToController: true
    }));
}
