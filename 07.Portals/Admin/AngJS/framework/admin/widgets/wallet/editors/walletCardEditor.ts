/**
 * @file framework/admin/widgets/wallet/editors/walletCardEditor.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Wallet Card Editor controller class
 */
module cef.admin.userDashboard.controls.wallet {
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
        confirmDeactivateKey = "ui.admin.common.wallet.AreYouSureYouWantToDeactivateThisCard.Question";
        // Constructor
        constructor(
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $q: ng.IQService,
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvAuthenticationService: services.IAuthenticationService,
                protected readonly cvWalletService: services.IWalletService,
                protected readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super($rootScope, $q, $translate, cefConfig, cvServiceStrings,
                cvAuthenticationService, cvWalletService, cvConfirmModalFactory);
            // TODO: tie startup type to variable
            this.viewstate.createMode = true;
        }
    }

    adminApp.directive("cefWalletCardEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            entry: "=?",
            hideHeader: "=?",
            hideFooter: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/wallet/editors/walletCardEditor.html", "ui"),
        controller: WalletCardEditorController,
        controllerAs: "wceCtrl",
        bindToController: true
    }));
}
