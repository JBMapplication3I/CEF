module cef.store.userDashboard.controls.shoppingLists {
    class CreateShoppingListModalController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        // <None>
        // Properties
        newName: string = null;
        createdMessage: string = null;
        cartTypes: api.CartTypeModel[] = [];
        // Functions
        private getCartTypes(): void {
            this.cvApi.shopping.GetCurrentUserCartTypes({ IncludeNotCreated: false })
                .then(r => this.cartTypes = r.data.Results);
        }
        private createCart(displayName: string): ng.IPromise<any> {
            return this.cvApi.shopping.CreateCartTypeForCurrentUser({
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                DisplayName: displayName,
                BrandID: this.$rootScope["globalBrandID"]
            });
        }
        ok(): void {
            this.setRunning();
            this.createdMessage = this.newName;
            this.createCart(this.newName).then(() => {
                this.newName = null;
                this.$rootScope.$broadcast(this.cvServiceStrings.events.carts.updateShoppingLists);
                this.close();
            });
        }
        close(): void {
            this.$uibModalInstance.close(true);
        }
        cancel(): void {
            this.$uibModalInstance.dismiss("cancel");
        }
        // Events
        inputKeyPress(event: JQueryKeyEventObject): void { // Angular returns this type of object per their docs
            if (event.key !== "Enter") { return; } // Only do anything if it was the enter key
            if (!this.newName) { return; } // And only if the user actually entered something
            event.preventDefault();
            event.stopPropagation();
            this.ok();
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvMessageModalFactory: store.modals.IMessageModalFactory) {
            super(cefConfig);
            this.getCartTypes();
        }
    }

    class AddToShoppingListButtonController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        text: string;
        classes: string;
        index: string;
        product: api.ProductModel;
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        onClick(): void {
            this.cvLoginModalFactory(null, null, false, true, this.cvServiceStrings.modalSizes.md).then(success => {
                if (!success) { return; }
                this.$uibModal.open({
                    size: this.cvServiceStrings.modalSizes.sm,
                    templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.addTo.modal.html", "ui"),
                    controller: CreateShoppingListModalController,
                    controllerAs: "shoppingListAddToModalCtrl",
                    resolve: {
                        product: () => this.product
                    }
                }).result.then(success2 => {
                    if (!success2) { return; }
                });
            });
        }
        // Constructor
        constructor(
                protected readonly $filter: ng.IFilterService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvLoginModalFactory: user.ILoginModalFactory) {
            super(cefConfig);
        }
    }

    class AddToShoppingListButtonProductController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        text: string;
        classes: string;
        index: string;
        product: api.ProductModel;
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        onClick(): void {
            this.cvLoginModalFactory(null, null, false, true, this.cvServiceStrings.modalSizes.md).then(success => {
                if (!success) { return; }
                this.$uibModal.open({
                    size: this.cvServiceStrings.modalSizes.sm,
                    templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.addTo.modal.html", "ui"),
                    controller: CreateShoppingListModalController,
                    controllerAs: "shoppingListAddToModalCtrl",
                    resolve: {
                        product: () => this.product
                    }
                }).result.then(success2 => {
                    if (!success2) { return; }
                });
            });
        }
        // Constructor
        constructor(
                protected readonly $filter: ng.IFilterService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvLoginModalFactory: user.ILoginModalFactory) {
            super(cefConfig);
        }
    }

    class AddToShoppingListButtonCatalogController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        text: string;
        classes: string;
        index: string;
        product: api.ProductModel;
        // Properties
        // <None>
        // Functions
        // <None>
        // Events
        onClick(): void {
            this.cvLoginModalFactory(null, null, false, true, this.cvServiceStrings.modalSizes.md).then(success => {
                if (!success) { return; }
                this.$uibModal.open({
                    size: this.cvServiceStrings.modalSizes.sm,
                    templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.addTo.modal.html", "ui"),
                    controller: CreateShoppingListModalController,
                    controllerAs: "shoppingListAddToModalCtrl",
                    resolve: {
                        product: () => this.product
                    }
                }).result.then(success2 => {
                    if (!success2) { return; }
                });
            });
        }
        // Constructor
        constructor(
                protected readonly $filter: ng.IFilterService,
                protected readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvLoginModalFactory: user.ILoginModalFactory) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefShoppingListButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            text: "@",
            product: "@",
            name: "@?",
            classes: "@?",
            index: "@?"
        },
        replace: true, // Required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.addTo.button.html", "ui"),
        controller: AddToShoppingListButtonController,
        controllerAs: "shoppingListAddToButtonCtrl",
        bindToController: true
    }));

    cefApp.directive("cefAddToShoppingListProduct", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            text: "@",
            product: "@",
            name: "@?",
            classes: "@?",
            index: "@?"
        },
        replace: true, // Required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.addTo.button.product.html", "ui"),
        controller: AddToShoppingListButtonProductController,
        controllerAs: "shoppingListAddToButtonProductCtrl",
        bindToController: true
    }));

    cefApp.directive("cefAddToShoppingListCatalog", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            text: "@",
            product: "@",
            name: "@?",
            classes: "@?",
            index: "@?"
        },
        replace: true, // Required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.addTo.button.catalog.html", "ui"),
        controller: AddToShoppingListButtonCatalogController,
        controllerAs: "shoppingListAddToButtonCatalogCtrl",
        bindToController: true
    }));
}
