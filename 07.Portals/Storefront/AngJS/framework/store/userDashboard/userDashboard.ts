/**
 * @file framework/store/userDashboard/userDashboard.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc User dashboard class
 */
module cef.store.userDashboard {
    class UserDashboardController extends core.TemplatedControllerBase {
        // Properties
        active: string;
        get currentUser(): api.UserModel { return this.cvAuthenticationService["currentUser"] };;
        get currentAccount(): api.AccountModel { return this.cvAuthenticationService["currentAccount"] };
        get isStoreAdmin(): boolean { return this.cvSecurityService.hasRole("CEF Store Administrator") };
        generalAttributes: Array<api.GeneralAttributeModel>;
        // Functions
        load(): void {
            this.setRunning();
            this.cvAuthenticationService.preAuth().finally(() => {
                if (!this.cvAuthenticationService.isAuthenticated()) {
                    this.redirectUnauthorizedUser();
                    this.finishRunning();
                    return;
                }
                this.cvAuthenticationService.getCurrentUserPromise()
                    .then(() => this.cvAuthenticationService.getCurrentAccountPromise().then(() => { }))
                    .finally(() => {
                        this.cvSecurityService.hasRolePromise("CEF Store Administrator").then(() => { });
                        this.cvAttributeService.search({ Active: true, AsListing: true, TypeName: "User" })
                            .then(attrs => {
                                this.generalAttributes = attrs;
                                this.setSerializableAttributes();
                            });
                        this.finishRunning();
                    });
            });
        }
        showSideMenu() : boolean {
            // NOTE: Change this function with client specific functionality
            return true;
        }
        redirectUnauthorizedUser(): void {
            // Redirect to login page
            if (this.cefConfig.authProvider.toLowerCase().split(",")
                    .indexOf(this.cvServiceStrings.auth.providers.openIdConnect.toLowerCase()) >= 0) {
                const url = `${this.cefConfig.authProviderAuthorizeUrl
                    }?response_type=code&client_id=${encodeURI(this.cefConfig.authProviderClientId)
                    }&redirect_uri=${encodeURI(this.cefConfig.authProviderRedirectUri)
                    }&scope=${encodeURI(this.cefConfig.authProviderScope)
                    }&nonce=${"N" + Math.random() + "" + Date.now()
                    }&response_mode=form_post&grant_type=authorization_code`;
                window.location.href = url;
                return;
            }
            if (this.cefConfig.authProvider.toLowerCase().split(",")
                    .indexOf(this.cvServiceStrings.auth.providers.cobalt.toLowerCase()) >= 0) {
                // No redirect
                return;
            }
            this.$filter("goToCORSLink")("#!?returnUrl=[ReturnUrl]", "login", undefined, undefined, this.$state.params, this.$state.current);
        }
        setSerializableAttributes(): void {
            if (!this.currentAccount) { return; }
            this.currentAccount.AccountContacts.forEach(ac => {
                this.generalAttributes.forEach(attr => {
                    if (ac.Slave.SerializableAttributes &&
                        ac.Slave.SerializableAttributes[attr.CustomKey]) {
                        ac.Slave[attr.CustomKey] = ac.Slave.SerializableAttributes[attr.CustomKey];
                        return;
                    }
                    ac.Slave[attr.CustomKey] = {
                        ID: attr.ID,
                        Group: attr.Group,
                        Key: attr.CustomKey,
                        SortOrder: attr.SortOrder,
                        Value: undefined,
                        AllowedValues: new Array<string>()
                    } as api.SerializableAttributeObject;
                    if (!attr.IsPredefined) {
                        return;
                    }
                    if (!attr.GeneralAttributePredefinedOptions) {
                        attr.GeneralAttributePredefinedOptions = [];
                    }
                    this.cvApi.attributes.GetGeneralAttributePredefinedOptions({
                        Active: true,
                        AsListing: true,
                        AttributeID: attr.ID
                    }).then(r => {
                        r.data.Results.forEach(option => {
                            ac.Slave[attr.CustomKey].AllowedValues.push(option.Value);
                            if (attr.GeneralAttributePredefinedOptions.filter(
                                    predefinedOption => predefinedOption.Value === option.Value).length === 0) {
                                attr.GeneralAttributePredefinedOptions.push(option);
                            }
                        });
                    });
                });
            });
        }
        // Constructor
        constructor(
                private readonly $state: ng.ui.IStateService, // Used by UI
                private readonly $title: ng.ui.ITitleService, // Used by UI
                private readonly $filter: ng.IFilterService,
                private readonly cvApi: api.ICEFAPI,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly $uibModal: ng.ui.bootstrap.IModalService,
                private readonly cvSecurityService: services.ISecurityService,
                private readonly cvAttributeService: services.IAttributeService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp.directive("cefUserDashboard", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/store/userDashboard/userDashboard.html", "ui"),
        controller: UserDashboardController,
        controllerAs: "udCtrl"
    }));
}
