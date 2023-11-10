/**
 * @file framework/store/menu/miniMenu.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Mini-Menu directive class
 */
module cef.store.menu {
    class MiniMenuController extends core.TemplatedControllerBase {
        // Properties
        get displayName(): string {
            return this.cvAuthenticationService["currentUser"]
                ? this.cvAuthenticationService["currentUser"].DisplayName
                : undefined;
        }
        get userName(): string {
            return this.cvAuthenticationService["currentUser"]
                ? this.cvAuthenticationService["currentUser"].UserName
                : undefined;
        }
        get accountName(): string {
            if (!this.accessibleAccounts || !this.accessibleAccounts.length) {
                return undefined;
            }
            this.selectedAccountKey = this.$cookies.get("cefSelectedAffiliateAccountKey");
            const found = _.find(this.accessibleAccounts, x => x.CustomKey === this.selectedAccountKey);
            return found && found.Name || undefined;
        }
        sections: Array<core.IDashboardSettings> = [];
        accessibleAccounts: Array<api.AccountModel> = [];
        languageMenuWasBuiltWith: string;
        buildingMenu = true;
        selectedAccountKey: string;
        // Functions
        isActive(state: string): boolean {
            return this.$window.location.pathname.toLowerCase()
                .startsWith(this.cefConfig.routes.dashboard.root.toLowerCase())
                && this.$state.includes(state);
        }
        private addWatchers(): void {
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.carts.itemAdded, (item, type, dto) => {
                if (type == this.cvServiceStrings.carts.types.wishList) {
                    this.$translate(
                        "ui.storefront.wishlist.messages.added",
                        null,
                        null,
                        "Item added to wishlist."
                    ).then(translated => {
                        $('#btnMiniMenuUserName').attr("data-content", translated);
                        $('#btnMiniMenuUserName')["popover"]('show');
                        this.$timeout(() => {
                            $('#btnMiniMenuUserName')["popover"]('hide');
                        }, 5000);
                    });
                } else if (type == this.cvServiceStrings.carts.types.quote) {
                    this.$translate(
                        "ui.storefront.quotes.messages.added",
                        null,
                        null,
                        "Successfully quoted"
                    ).then(translated => {
                        $('#btnMiniMenuUserName').attr("data-content", translated);
                        $('#btnMiniMenuUserName')["popover"]('show');
                        this.$timeout(() => {
                            $('#btnMiniMenuUserName')["popover"]('hide');
                        }, 5000);
                    });
                }
            });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
        private buildMenuOuter(): void {
            this.cvLanguageService.getCurrentLanguagePromise().then(language => {
                if (!language.CustomKey || this.languageMenuWasBuiltWith === language.CustomKey) {
                    return;
                }
                this.languageMenuWasBuiltWith = language.CustomKey;
                this.sections = [];
                this.buildMenu(angular.fromJson(angular.toJson(this.cefConfig.dashboard)));
            });
        }
        private buildMenu(settings: Array<core.IDashboardSettings>): void {
            settings.filter(x => x.enabled).forEach(section => {
                if (section.reqAnyRoles && section.reqAnyRoles.length) {
                    this.$q.all(
                        section.reqAnyRoles.map(role => this.cvSecurityService.hasRolePromise(role))
                    ).then(arr => {
                        if (!arr.some(x => x === true)) {
                            // Stop processing, we don't have the required role(s)
                            return;
                        }
                        // Continue processing
                        this.buildMenuInner(section);
                    });
                    return; // Processing was continued inside the promise
                }
                if (section.reqAnyPerms && section.reqAnyPerms.length) {
                    this.$q.all(
                        section.reqAnyPerms.map(perm => this.cvSecurityService.hasPermissionPromise(perm))
                    ).then(arr => {
                        if (!arr.some(x => x === true)) {
                            // Stop processing, we don't have the required permission(s)
                            return;
                        }
                        // Continue processing
                        this.buildMenuInner(section);
                    });
                    return; // Processing was continued inside the promise
                }
                // Continue Processing
                this.buildMenuInner(section);
            });
            this.buildingMenu = false;
        }
        private buildMenuInner(section: core.IDashboardSettings): void {
            this.$translate(section.titleKey).then(translated => {
                section.title = translated;
            }).finally(() => {
                if (section.sref) {
                    this.sections.push(section);
                }
                if (section.children && section.children.length) {
                    this.buildMenu(section.children);
                }
            });
        }
        private loadAccounts(): void {
            this.cvSecurityService.hasRolePromise("CEF Affiliate Administrator").then(hasRole => {
                if (!hasRole) { return; }
                this.cvApi.accounts.GetAccountsForCurrentAccount({
                    Active: true,
                    AsListing: true
                }).then(r => {
                    if (!r || !r.data) {
                        return;
                    }
                    this.accessibleAccounts = r.data.Results;
                });
            });
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $window: ng.IWindowService,
                private readonly $state: ng.ui.IStateService,
                private readonly $cookies: ng.cookies.ICookiesService,
                private readonly $translate: ng.translate.ITranslateService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvLanguageService: services.ILanguageService) {
            super(cefConfig);
            this.cvAuthenticationService.preAuth().finally(() => {
                this.buildMenuOuter();
                this.loadAccounts();
                this.addWatchers();
            });
        }
    }

    cefApp.directive("cefMiniMenu", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/menu/miniMenu.html", "ui"),
        controller: MiniMenuController,
        controllerAs: "miniMenuCtrl"
    }));
}
