module cef.store.widgets.affiliates {
    class AffiliateAccountSelectorController extends core.TemplatedControllerBase {
        // Properties
        private readonly cookieName = "cefSelectedAffiliateAccountKey";
        selectedAccountKey: string = null;
        accessibleAccounts: Array<{ value: string, label: string }> = [];
        // Functions
        saveSelectedAccountKey(): void {
            if (!this.selectedAccountKey) {
                // Use this user's account
                this.cvAuthenticationService.getCurrentAccountPromise().then(
                    account => this.saveSelectedAccountKeyInner(account.CustomKey));
                return;
            }
            // Use the other account
            this.saveSelectedAccountKeyInner(this.selectedAccountKey);
        }
        private saveSelectedAccountKeyInner(key: string): void {
            this.$cookies.put(this.cookieName, key, this.getCookiesOptions());
            // Clear the cached address book so it doesn't try to mix books
            this.cvAddressBookService.reset().finally(() => {
                // Reset the UI now that we have changed it
                window.location.reload();
            });
        }
        private loadSelectedAccountKey(): void {
            const value = this.$cookies.get(this.cookieName);
            this.selectedAccountKey = value ? value : null;
        }
        private getCookiesOptions(): ng.cookies.ICookiesOptions {
            const domain = this.cefConfig.useSubDomainForCookies || !this.subdomain
                ? this.$location.host()
                : this.$location.host().replace(this.subdomain, "")
            return <ng.cookies.ICookiesOptions>{
                // expires: never
                path: "/",
                domain: domain
            };
        }
        load(): void {
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.loadSelectedAccountKey();
                    return;
                }
                this.cvAuthenticationService.getCurrentUserPromise(true).then(user => {
                    if (user.SerializableAttributes != null && user?.SerializableAttributes["associatedAccounts"]?.Value) {
                        let accountIDArray = user?.SerializableAttributes["associatedAccounts"]?.Value.trim().split(",").map(Number);
                        this.cvApi.accounts.GetAccountsForCurrentAccount({
                            IDs: accountIDArray
                        }).then(r => {
                            if (!r || !r.data) {
                                this.loadSelectedAccountKey();
                                return;
                            }
                            let translated = "My Account";
                            this.$translate("ui.storefront.userDashboard2.widgets.menus.userDashboardSideMenu.MyAccount").then(trans => {
                                translated = trans;
                            }).finally(() => {
                                const values: { value: string, label: string }[] = r.data.Results.map(x => {
                                    return {
                                        value: x.CustomKey,
                                        label: x.Name,
                                    };
                                });
                                values.unshift({
                                    value: this.cvAuthenticationService["currentUser"].AccountKey,
                                    label: this.cvAuthenticationService["currentUser"].Account.Name,
                                });
                                this.accessibleAccounts = values;
                                this.loadSelectedAccountKey();
                                if (!this.selectedAccountKey && this.accessibleAccounts.length) {
                                    this.selectedAccountKey = this.accessibleAccounts[0].value;
                                    this.$cookies.put(this.cookieName, this.selectedAccountKey, this.getCookiesOptions());
                                    this.loadSelectedAccountKey();
                                }
                            });
                        });
                    }
                });
            });
        }
        // Constructor
        constructor(
                private readonly $location: ng.ILocationService,
                private readonly $state: ng.ui.IStateService,
                private readonly $cookies: ng.cookies.ICookiesService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly subdomain: string,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvAffiliateAccountSelectorModalFactory: IAffiliateAccountSelectorModalFactory) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefAffiliateAccountSelector", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/widgets/affiliates/affiliateAccountSelector.html", "ui"),
        controller: AffiliateAccountSelectorController,
        controllerAs: "affiliateAccountSelectorCtrl"
    }));
}
