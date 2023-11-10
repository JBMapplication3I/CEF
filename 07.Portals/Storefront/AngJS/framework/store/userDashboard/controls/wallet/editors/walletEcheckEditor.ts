/**
 * @file framework/store/userDashboard/controls/wallet/walletEcheck.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet eCheck Editor controller class
 */
module cef.store.userDashboard.controls.wallet {
    class WalletEcheckEditorController extends WalletEditorControllerBase {
        // Properties
        get eCheckAccountTypes() {
            return this.cvWalletService.eCheckAccountTypes;
        }
        // Overridable strings
        formName = "Echeck";
        confirmDeactivateKey = "ui.storefront.userDashboard.wallet.AreYouSureYouWantToDeactivateThisEcheck.Question";
        // Functions
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvConfirmModalFactory: store.modals.IConfirmModalFactory) {
            super($rootScope, $q, $translate, cefConfig, cvServiceStrings,
                cvAuthenticationService, cvWalletService, cvConfirmModalFactory);
            // TODO: tie startup type to variable
            this.viewstate.createMode = true;
        }
    }

    cefApp.directive("cefWalletEcheckEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            entry: "=?",
            hideHeader: "=?",
            hideFooter: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/wallet/editors/walletEcheck.html", "ui"),
        controller: WalletEcheckEditorController,
        controllerAs: "weeCtrl",
        bindToController: true
    }));
}
