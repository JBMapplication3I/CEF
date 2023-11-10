module cef.store.userDashboard.controls.shoppingLists {
    class ShoppingListsTableController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        cartTypes: api.CartTypeModel[];
        // Functions
        getCurrentUserCartTypes(): void {
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    return;
                }
                this.cvApi.shopping.GetCurrentUserCartTypes({ IncludeNotCreated: false })
                    .then(r => this.cartTypes = r.data.Results);
            });
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $filter: ng.IFilterService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvLoginModalFactory: user.ILoginModalFactory) {
            super(cefConfig);
            this.getCurrentUserCartTypes();
        }
    }

    cefApp.directive("cefShoppingListsTable", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.table.html", "ui"),
        controller: ShoppingListsTableController,
        controllerAs: "shoppingListsTableCtrl"
    }));
}
