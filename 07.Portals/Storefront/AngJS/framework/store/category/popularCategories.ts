// <copyright file="popularCategories.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>popular categories class</summary>
module cef.store.category {
    class PopulateCategoriesController extends core.TemplatedControllerBase {
        // Properties
        categories: api.CategoryModel[] = [];
        // Functions
        load(): void {
            var getCategories = {
                Active: true,
                BrandID: this.$rootScope["globalBrandID"],
                AsListing: true,
                JsonAttributes: { "Popular Category": ["true"] },
                IncludeChildrenInResults: false,
                noCache: 1
            };
            this.cvApi.categories.GetCategories(getCategories).then(r => {
                console.log(r.data.Results, "popular categories call")
                this.categories = r.data.Results
            });
        }
        setupWatchers(): void {
            if (angular.isDefined(this.$rootScope["globalBrandID"])) {
                return this.load();
            }
            let unbind1 = this.$rootScope.$on(this.cvServiceStrings.events.brands.globalBrandSiteDomainPopulated, () => this.load());
            this.$rootScope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            if (!this.cefConfig.featureSet.brands.enabled) {
                this.load();
            }
            this.setupWatchers();
        }
    }

    cefApp.directive("cefPopularCategories", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/category/popularCategories.html", "ui"),
        controller: PopulateCategoriesController,
        controllerAs: "popularCategoriesCtrl"
    }));
}