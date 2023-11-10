/**
 * @file framework/store/userDashboard/widgets/menus/userDashboardSideMenu.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc User dashboard side menu class
 */
module cef.store.userDashboard.widgets.menus {
    class UserDashboardSideMenuController extends core.TemplatedControllerBase {
        // Properties
        private readonly cookieName = "cefSelectedAffiliateAccountKey";
        private readonly userCookieName = "cefSelectedAffiliateUserKey";
        sections: Array<core.IDashboardSettings> = [];
        selectedAccountKey: string = null;
        accessibleAccounts: Array<{ value: string, label: string }> = [];
        selectedUserKey: string;
        accessibleUsers: Array<{ value: string; label: string }> = [];
        languageMenuWasBuiltWith: string;
        // Functions
        isActive(state: string): boolean {
            return this.$state.includes(state);
        }
        validateChildActive(section: core.IDashboardSettings): boolean {
            if (section.sref
                && section.sref.endsWith(".list")
                && this.$state.get(section.sref.replace(".list", ""))
                && this.$state.includes(section.sref.replace(".list", ""))) {
                return true;
            }
            return _.some(section.children, ["sref", this.$state.current.name]);
        }
        validateAlternateActive(section: core.IDashboardSettings): boolean {
            return section.srefAlt && this.$state.includes(section.srefAlt);
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
        private buildMenu(settings: Array<core.IDashboardSettings>, parent: core.IDashboardSettings = null): void {
            settings.filter(x => x.enabled).forEach(section => {
                if (section.reqAnyRoles && section.reqAnyRoles.length) {
                    this.$q.all(section.reqAnyRoles
                            .map(role => this.cvSecurityService.hasRolePromise(role)))
                        .then(arr => {
                            if (!arr.some(x => x === true)) {
                                // Stop processing, we don't have the required role(s)
                                return;
                            }
                            // Continue processing
                            this.buildMenuInner(section, parent);
                        });
                    return;
                }
                if (section.reqAnyPerms && section.reqAnyPerms.length) {
                    this.$q.all(
                        section.reqAnyPerms
                            .map(perm => this.cvSecurityService.hasPermissionPromise(perm))
                    ).then(arr => {
                        if (!arr.some(x => x === true)) {
                            // Stop processing, we don't have the required permission(s)
                            return;
                        }
                        // Continue processing
                        this.buildMenuInner(section, parent);
                    });
                    return;
                }
                // Continue Processing
                this.buildMenuInner(section, parent);
            });
        }
        private buildMenuInner(section: core.IDashboardSettings, parent: core.IDashboardSettings = null): void {
            this.$translate(section.titleKey).then(translated => {
                section.title = translated;
            }).finally(() => {
                const children = section.children;
                section.children = [];
                if (parent) {
                    parent.children.push(section);
                } else {
                    this.sections.push(section);
                }
                if (children && children.length) {
                    this.buildMenu(children, section);
                }
            });
        }
        saveSelectedAccountKey(): void {
            if (!this.selectedAccountKey) {
                // Use this user's account
                this.cvAuthenticationService.getCurrentAccountPromise()
                    .then(account => this.saveSelectedAccountKeyInner(account.CustomKey));
                return;
            }
            this.selectedUserKey = null;
            this.saveSelectedUserKey();
            // Use the other account
            this.saveSelectedAccountKeyInner(this.selectedAccountKey);
        }
        private saveSelectedAccountKeyInner(key: string): void {
            this.$cookies.put(this.cookieName, key, this.getCookiesOptions());
            // Clear the cached address book so it doesn't try to mix books
            this.cvAddressBookService.reset().finally(() => {
                // Reset the UI now that we have changed it, but we don't need
                // to do a full page reload
                this.$state.reload();
            });
        }
        private loadSelectedAccountKey(): void {
            const value = this.$cookies.get(this.cookieName);
            this.selectedAccountKey = value ? value : null;
        }
        saveSelectedUserKey(): void {
            if (!this.selectedUserKey) {
                this.cvAuthenticationService.getCurrentUserPromise()
                    .then(user => this.saveSelectedUserKeyInner(user.username));
                return;
            }
            // Use the other user
            this.saveSelectedUserKeyInner(this.selectedUserKey);
        }
        private saveSelectedUserKeyInner(key: string): void {
            this.$cookies.put(this.userCookieName, key, this.getCookiesOptions());
            this.$state.reload();
        }
        private loadSelectedUserKey(): void {
            const value = this.$cookies.get(this.userCookieName);
            this.selectedUserKey = value ? value : null;
        }
        private getCookiesOptions(): ng.cookies.ICookiesOptions {
            return <ng.cookies.ICookiesOptions>{
                // expires: never
                path: "/",
                domain: this.cefConfig.useSubDomainForCookies || !this.subdomain
                    ? this.$location.host()
                    : this.$location.host().replace(this.subdomain, "")
            };
        }
        load(): void {
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.loadSelectedAccountKey();
                    this.loadSelectedUserKey();
                    return;
                }
                this.buildMenuOuter();
                this.cvAuthenticationService.getCurrentUserPromise(true).then(user => {
                    if (user.SerializableAttributes != null && user?.SerializableAttributes["associatedAccounts"]?.Value) {
                        let accountIDArray = user?.SerializableAttributes["associatedAccounts"]?.Value.trim().split(",").map(Number);
                        this.cvApi.accounts.GetAccountsForCurrentAccount({
                            IDs: accountIDArray
                        }).then(r => {
                            if (!r || !r.data) {
                                this.loadSelectedAccountKey();
                                this.loadSelectedUserKey();
                                return;
                            }
                            let label = "My Account";
                            this.$translate("ui.storefront.userDashboard2.widgets.menus.userDashboardSideMenu.MyAccount").then(translated => {
                                label = translated;
                            }).finally(() => {
                                const values: { value: string, label: string }[] = r.data.Results.map(x => {
                                    return {
                                        value: x.CustomKey,
                                        label: x.Name,
                                    };
                                });
                                values.unshift({
                                    value: this.cvAuthenticationService["currentUser"].AccountKey,
                                    label: label
                                });
                                this.accessibleAccounts = values;
                                this.loadSelectedAccountKey();
                                if (!this.selectedAccountKey && this.accessibleAccounts.length) {
                                    this.selectedAccountKey = this.accessibleAccounts[0].value;
                                    this.$cookies.put(this.cookieName, this.selectedAccountKey, this.getCookiesOptions());
                                    this.loadSelectedAccountKey();
                                }
                                this.cvApi.accounts.GetUsersForCurrentAccount({
                                    Active: true,
                                    AsListing: true,
                                    AccessibleLevels: 0,
                                    Timestamp: new Date().getTime()
                                }).then(r2 => {
                                    if (!r2 || !r2.data) {
                                        this.loadSelectedAccountKey();
                                        this.loadSelectedUserKey();
                                        return;
                                    }
                                    const values: { value: string; label: string }[] = r2.data.Results.map(x => {
                                        return {
                                            value: x.UserName,
                                            label: x.UserName,
                                        };
                                    });
                                    this.accessibleUsers = values;
                                    this.loadSelectedUserKey();
                                    if (!this.selectedUserKey
                                        || (this.selectedUserKey == null && this.accessibleUsers.length)
                                        || !_.some(this.accessibleUsers, x => x.value == this.selectedUserKey)) {
                                            if (this.accessibleUsers.length) {
                                                this.selectedUserKey = this.accessibleUsers[0].value;
                                            }
                                        this.$cookies.put(this.userCookieName, this.selectedUserKey, this.getCookiesOptions());
                                        this.loadSelectedUserKey();
                                    }
                                    this.$rootScope.$broadcast(this.cvServiceStrings.events.wallet.updated);
                                }).catch(reason3 => this.consoleDebug(reason3));
                            });
                        }).catch(reason2 => this.consoleDebug(reason2));
                    }
                }).catch(reason => this.consoleDebug(reason));
                const unbind1 = this.$scope.$on(this.cvServiceStrings.events.lang.changeFinished,
                    () => this.buildMenuOuter());
                this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                    if (angular.isFunction(unbind1)) { unbind1(); }
                });
            });
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $q: ng.IQService,
                private readonly $location: ng.ILocationService,
                private readonly $state: ng.ui.IStateService,
                private readonly $cookies: ng.cookies.ICookiesService,
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly subdomain: string,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvAddressBookService: services.IAddressBookService,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvLanguageService: services.ILanguageService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefUserDashboardSideMenu", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/widgets/menus/userDashboardSideMenu.html", "ui"),
        controller: UserDashboardSideMenuController,
        controllerAs: "userDashboardSideMenuCtrl"
    }));
}
