module cef.store.userDashboard.controls.orderRequests {
    class CreateOrderRequestButtonController extends core.TemplatedControllerBase {
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
                    templateUrl: this.$filter("corsLink")("/framework/store/userDashboard/controls/orderRequests/createOrderRequestModal.html", "ui"),
                    controller: CreateOrderRequestModalController,
                    controllerAs: "orderRequestCreateModalCtrl"
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

    cefApp.directive("cefOrderRequestCreateButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            text: "@",
            classes: "@?",
            index: "@?"
        },
        replace: true, // Required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/orderRequests/createOrderRequestButton.html", "ui"),
        controller: CreateOrderRequestButtonController,
        controllerAs: "orderRequestCreateButtonCtrl",
        bindToController: true
    }));
}