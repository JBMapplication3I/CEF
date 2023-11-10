/**
 * @file framework/store/personalization/personalizedCategoriesQuickNav.ts
 * @desc Personalized categories quick navigation class.
 */
module cef.store.personalization {
    class PersonalizedCategoriesQuickNavController extends core.TemplatedControllerBase {
        // Properties
        usersSelectedStore: api.StoreModel = null; // Populated by Angular Service
        categories: Array<api.CategoryModel> = []; // Populated by Web Service
        size: number; // Populated by Scope
        // Constructors
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
            this.load();
        }
        // Functions
        load(): void {
            this.cvAuthenticationService.preAuth().finally(() => {
                this.cvApi.categories.GetPersonalizedCategoriesForCurrentUser().then(response => {
                    if (!response || !response.data) { return; }
                    this.categories = response.data;
                });
            });
        }
    }

    cefApp.directive("cefPersonalizedCategoriesQuickNav", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { size: "=" },
        templateUrl: $filter("corsLink")("/framework/store/personalization/personalizedCategoriesQuickNav.html", "ui"),
        controller: PersonalizedCategoriesQuickNavController,
        controllerAs: "personalizedCategoriesQuickNavCtrl",
        bindToController: true
    }));
}
