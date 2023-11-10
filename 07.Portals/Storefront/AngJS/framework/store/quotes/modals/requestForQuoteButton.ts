/**
 * @file framework/store/quotes/modals/requestForQuoteButton.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com All rights reserved.
 * @desc Request for quote button class.
 */
module cef.store.quotes.modals {
    export class RequestForQuoteButtonController extends core.TemplatedControllerBase {
        // Properties
        overrideClasses: string;
        overrideText: string;
        // Functions
        requireLoginForRFQ(): void {
            this.cvLoginModalFactory(() => null, null, true, false).then(() => {
                this.$uibModal.open({
                    templateUrl: this.$filter("corsLink")("/framework/store/quotes/modals/requestForQuoteModal.html", "ui"),
                    scope: this.$scope,
                    size: this.cvServiceStrings.modalSizes.lg,
                    controller: RequestForQuoteModalController,
                    controllerAs: "requestForQuoteModalCtrl"
                });
            });
        }
        // Constructors
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $filter: ng.IFilterService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvLoginModalFactory: user.ILoginModalFactory,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefRequestForQuoteButton", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { overrideText: "@?", overrideClasses: "@?" },
        templateUrl: $filter("corsLink")("/framework/store/quotes/modals/requestForQuoteButton.html", "ui"),
        controller: RequestForQuoteButtonController,
        controllerAs: "rfqbCtrl",
        bindToController: true
    }));
}
