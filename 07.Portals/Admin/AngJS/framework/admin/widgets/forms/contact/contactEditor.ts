/**
 * @file framework/admin/widgets/forms/contact/contactWidget.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Contact Widget (edits contacts) class
 */
module cef.admin.widgets.forms.contact {
    export class ContactEditorWidgetController extends ContactEditorControllerBase {
        // Bound Scope Properties
        addressTitle: string;
        account: api.AccountModel;
        toMirror: api.ContactModel;
        doMirror: boolean;
        idSuffix: string;
        enforceUniqueEmail: boolean;
        usernameIsEmail: boolean;
        username: string;
        isBilling: boolean;
        oneCol: boolean;
        // Properties
        emailValidityState = "Empty";
        onInputChange: Function;
        regions: api.RegionModel[] = [];
        countries: api.CountryModel[] = [];
        // Functions
        loadAdditional(): void {
            // Show the inputs if there is data already in them
            if (this.contact) {
                this.setupContact();
                return;
            }
            const unbind1 = this.$scope.$on(this.cvServiceStrings.events.addressBook.resetRegionDropdownNeeded,
                () => this.propagateCountryChange());
            const unbind2 = this.$scope.$watch(
                () => this.contact,
                newValue => {
                    if (newValue && newValue.Address) {
                        unbind2();
                        this.setupContact();
                    }
                });
            this.$scope.$on(this.cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
                if (angular.isFunction(unbind2)) { unbind2(); }
            });
        }
        setupContact(): void {
            if (this.contact && this.contact.Address && this.contact.Address.Street3 && !this.showStreet3) {
                this.showStreet2 = this.showStreet3 = true;
            }
            if (this.contact && this.contact.Address && this.contact.Address.Street2 && !this.showStreet2) {
                this.showStreet2 = true;
            }
            if (this.contact && this.contact.Phone3 && !this.showPhone3) {
                this.showPhone2 = this.showPhone3 = true;
            }
            if (this.contact && this.contact.Phone2 && !this.showPhone2) {
                this.showPhone2 = true;
            }
            if (this.contact && this.contact.Address && !this.contact.Address.CountryID) {
                // Assign the default country for the site to the new contact
                this.cvCountryService.get({ code: this.cefConfig.countryCode || "USA" }).then(c => {
                    if (!c) {
                        this.updateRegions();
                        return;
                    }
                    this.selectCountry(c);
                });
                return;
            }
            this.updateRegions();
        }
        private setEmailValidity(control: ng.INgModelController, success: boolean, becauseUniqueness: boolean): void {
            if (this.usernameIsEmail) {
                this.username = success ? control.$modelValue : null;
            }
            this.emailValidityState = success ? "Valid" : "Invalid";
            if (becauseUniqueness) {
                control.$setValidity("unique", success);
            } else {
                // Don't show uniqueness fail when it hasn't even been run
                control.$setValidity("unique", true);
            }
            this.callOnChangeIfSet();
        }
        updateRegions(): void {
            this.cvApi.geography.GetRegions({
                Active: true,
                AsListing: true,
                CountryID: this.contact
                    && this.contact.Address
                    && this.contact.Address.CountryID
                    ? this.contact.Address.CountryID
                    : null
            }).then(r => this.regions = r.data.Results);
        }
        // Events
        private propertyChanged(property: string): void {
            switch (property) {
                case "CountryID": {
                    this.updateRegions();
                    if (this.contact.Address.CountryID) {
                        const country = _.find(this.countries, x => x.ID === this.contact.Address.CountryID);
                        this.contact.Address.CountryCode = country.Code;
                        this.contact.Address.CountryKey = country.CustomKey;
                        this.contact.Address.CountryName = country.Name;
                        this.contact.Address.Country = country;
                    } else {
                        this.contact.Address.CountryCode = null;
                        this.contact.Address.CountryKey = null;
                        this.contact.Address.CountryName = null;
                        this.contact.Address.Country = null;
                    }
                    break;
                }
                case "RegionID": {
                    this.updateRegions();
                    if (this.contact.Address.RegionID) {
                        const region = _.find(this.regions, x => x.ID === this.contact.Address.RegionID);
                        this.contact.Address.RegionCode = region.Code;
                        this.contact.Address.RegionKey = region.CustomKey;
                        this.contact.Address.RegionName = region.Name;
                        this.contact.Address.Region = region;
                    } else {
                        this.contact.Address.RegionCode = null;
                        this.contact.Address.RegionKey = null;
                        this.contact.Address.RegionName = null;
                        this.contact.Address.Region = null;
                    }
                    break;
                }
            }
            if (this.doMirror && this.toMirror) {
                this.contact = angular.fromJson(angular.toJson(this.toMirror));
            } else {
                this.$rootScope.$broadcast(this.cvServiceStrings.events.contacts.propertyChanged, property, this.contact);
            }
            if (angular.isFunction(this.onInputChange)) {
                this.onInputChange();
            }
        }
        emailChanged($event: ng.IAngularEvent, control: ng.INgModelController): void {
            // control.$modelValue has the new value, before it's been assigned to this.contact.Email1
            if (!control.$modelValue) {
                this.setEmailValidity(control, false, false);
                return;
            }
            if (!this.enforceUniqueEmail) {
                this.setEmailValidity(control, true, false);
                return;
            }
            this.cvAuthenticationService.validateEmailIsUnique({ Email: control.$modelValue })
                .then(r => this.setEmailValidity(control, r.data.ActionSucceeded, true))
                .catch(() => this.setEmailValidity(control, false, false));
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                protected readonly $rootScope: ng.IRootScopeService,
                protected readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCountryService: services.ICountryService,
                protected readonly cvRegionService: services.IRegionService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super($timeout, cefConfig, cvApi, cvCountryService, cvRegionService);
            this.load();
            const unbind1 = $scope.$on(cvServiceStrings.events.contacts.propertyChanged,
                (event: ng.IAngularEvent, property: string, contact: api.ContactModel) => {
                    if (this.doMirror && this.toMirror) {
                        this.contact = angular.fromJson(angular.toJson(this.toMirror));
                        event.stopPropagation && event.stopPropagation();
                    }
                });
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            });
        }
    }

    adminApp.directive("cefContactEditorWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            // contact to be bound to from outside template
            contact: "=",
            // optional address title from parent directive to use (ex: Billing Address,
            // Shipping Address, etc... false passes no title
            addressTitle: "=?",
            // Optional, the account to read address book entries from
            account: "=?",
            usernameIsEmail: "=?",
            idSuffix: "@?",
            enforceUniqueEmail: "=?",
            // Optional, when a change occurs, fire this callback
            onInputChange: "&?",
            // True to hide the address key input
            hideKey: "=?",
            // Trye to hide the "Need to enter more phone numbers?" UI
            hideMorePhones: "=?",
            // True to hide the fax input
            hideFax: "=?",
            // True to hide the address inputs (basically the right side of the form)
            hideAddress: "=?",
            // Changes which autocomplete text value to use on address inputs
            isBilling: "=?",
            // For two-way binding the username
            username: "=?",
            // Optional, External variable to determine if the mirroring should be active
            doMirror: "=?",
            // Optional, when a value is provided, a checkbox will be available stating to mirror this other contact's properties and disable editing
            toMirror: "=?",
            // Optional, when true, the main left and right sides will always stack instead
            oneCol: "=?",
            hidePersonalDetails: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/forms/contact/contactEditor.html", "ui"),
        controller: ContactEditorWidgetController,
        controllerAs: "cewCtrl",
        bindToController: true
    }));
}
