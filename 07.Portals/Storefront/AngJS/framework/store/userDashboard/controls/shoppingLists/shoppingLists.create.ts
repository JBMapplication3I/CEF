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
        private createCart(name: string): ng.IPromise<any> {
            if (this.cefConfig.featureSet.carts.shoppingLists.assignNameToAndShowDisplayName) {
                return this.cvApi.shopping.CreateCartTypeForCurrentUser({
                    ID: 0,
                    Active: true,
                    CreatedDate: new Date(),
                    DisplayName: name,
                    BrandID: this.$rootScope["globalBrandID"]
                });
            }
            return this.cvApi.shopping.CreateCartTypeForCurrentUser({
                ID: 0,
                Active: true,
                CreatedDate: new Date(),
                Name: name,
                CustomKey: name,
                BrandID: this.$rootScope["globalBrandID"]
            });
        }
        ok(): void {
            this.setRunning();
            if (this.cartTypes.some(item => item.Name === this.newName)) {
                this.cvMessageModalFactory(
                    this.$translate("ui.storefront.userDashboard2.controls.shoppingLists.ShoppingListAlreadyExists"));
                this.finishRunning(true);
                return;
            }
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

    class CreateShoppingListButtonController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        text: string;
        classes: string;
        index: string;
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
                    templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.create.modal.html", "ui"),
                    controller: CreateShoppingListModalController,
                    controllerAs: "shoppingListCreateModalCtrl"
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

    cefApp.directive("cefShoppingListCreateButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            text: "@",
            classes: "@?",
            index: "@?"
        },
        replace: true, // Required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/shoppingLists/shoppingLists.create.button.html", "ui"),
        controller: CreateShoppingListButtonController,
        controllerAs: "shoppingListCreateButtonCtrl",
        bindToController: true
    }));
}
