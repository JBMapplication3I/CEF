/**
 * @file framework/store/userDashboard/controls/wallet/walletCardEditor.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet Card Editor controller class
 */
module cef.store.userDashboard.controls.wallet {
    class WalletCardEditorController extends WalletEditorControllerBase {
        // Properties
        get expirationMonths() {
            return this.cvWalletService.expirationMonths;
        }
        get expirationYears() {
            return this.cvWalletService.expirationYears;
        }
        // Overridable strings
        formName = "Card";
        confirmDeactivateKey = "ui.storefront.userDashboard.wallet.AreYouSureYouWantToDeactivateThisCard.Question";
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

    cefApp.directive("cefWalletCardEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            entry: "=?",
            hideHeader: "=?",
            hideFooter: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/controls/wallet/editors/walletCardEditor.html", "ui"),
        controller: WalletCardEditorController,
        controllerAs: "wceCtrl",
        bindToController: true
    }));
}
