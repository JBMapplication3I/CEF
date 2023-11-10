/**
 * @file framework/store/personalization/personalizedFeed.ts
 * @desc Personalized feed class.
 */
module cef.store.personalization {
    class PersonalizedFeedController extends core.TemplatedControllerBase {
        // Properties
        usersSelectedStore: api.StoreModel = null; // Populated by Angular Service
        feed: Array<api.KeyValuePair<api.CategoryModel,Array<api.ProductModel>>> = []; // Populated by Web Service
        size: number; // Populated by Scope
        activeTab: number; // Populated by UI
        // Functions
        load(): void {
            this.cvAuthenticationService.preAuth().finally(() => {
                this.cvApi.products.GetPersonalizedCategoryAndProductFeedForCurrentUser().then(response => {
                    if (!response || !response.data) { return; }
                    this.feed = response.data;
                    if (this.feed[0]) {
                        this.activeTab = (this.feed[0] as any).Key.ID;
                    }
                });
                const unbind1 = this.$scope.$on(this.cvServiceStrings.events.stores.selectionUpdate, () => {
                    this.usersSelectedStore = null;
                    this.refreshUserStore();
                });
                const unbind2 = this.$scope.$on(this.cvServiceStrings.events.stores.cleared, () => {
                    this.usersSelectedStore = null;
                });
                this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                    if (angular.isFunction(unbind1)) { unbind1(); }
                    if (angular.isFunction(unbind2)) { unbind2(); }
                });
            });
        }
        refreshUserStore(): void {
            this.cvStoreLocationService.getUserSelectedStore()
                .then(store => this.usersSelectedStore = store)
                .catch(result => this.consoleLog(result));
        }
        // Constructors
        constructor(
                private readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvStoreLocationService: services.IStoreLocationService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefPersonalizedFeed", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { size: "=" },
        templateUrl: $filter("corsLink")("/framework/store/personalization/personalizedFeed.html", "ui"),
        controller: PersonalizedFeedController,
        controllerAs: "personalizedFeedCtrl",
        bindToController: true
    }));
}
