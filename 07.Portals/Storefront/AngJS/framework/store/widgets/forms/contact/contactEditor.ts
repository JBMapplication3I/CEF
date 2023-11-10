/**
 * @file framework/store/widgets/forms/contact/contactWidget.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Contact Widget (edits contacts) class
 */
module cef.store.widgets.forms.contact {
    class ContactWidgetController extends ContactEditorControllerBase {
        // Bound Scope Properties
        addressTitle: string;
        idSuffix: string;
        enforceUniqueEmail: boolean;
        usernameIsEmail: boolean;
        // useSpecialCharInEmail = true;
        useSpecialCharInEmail: boolean;
        username: string;
        isBilling: boolean;
        // Properties
        emailValidityState = "Empty";
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
        private setEmailValidity(
            control: ng.INgModelController,
            success: boolean,
            becauseUniqueness: boolean,
            becauseSpecialChar: boolean)
            : void {
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
            if (becauseSpecialChar && !this.useSpecialCharInEmail) {
                control.$setValidity("specialChar", false);
            } else {
                control.$setValidity("specialChar", true);
            }
            this.callOnChangeIfSet();
        }
        // Events
        emailChanged($event: ng.IAngularEvent, control: ng.INgModelController): void {
            // control.$modelValue has the new value, before it's been assigned to this.contact.Email1
            if (!control.$modelValue) {
                this.setEmailValidity(control, false, false, false);
                return;
            }
            if (!(/^[A-Za-z0-9_\.@!#\$%\&'\*+/=?\^_`{|}~-]+$/.test(control.$modelValue))) {
                this.setEmailValidity(control, false, false, true);
                return;
            }
            if (!this.enforceUniqueEmail) {
                this.setEmailValidity(control, true, false, false);
                return;
            }
            this.cvAuthenticationService.validateEmailIsUnique({ Email: control.$modelValue })
                .then(r => this.setEmailValidity(control, r.data.ActionSucceeded, true, false))
                .catch(() => this.setEmailValidity(control, false, false, true));
        }
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                protected readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                protected readonly cvCountryService: services.ICountryService,
                protected readonly cvRegionService: services.IRegionService,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super($timeout, cefConfig, cvApi, cvCountryService, cvRegionService);
            this.load();
        }
    }

    cefApp.directive("cefContactWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            // contact to be bound to from outside template
            contact: "=",
            // optional address title from parent directive to use (ex: Billing Address,
            // Shipping Address, etc... false passes no title
            addressTitle: "=?",
            usernameIsEmail: "=?",
            useSpecialCharInEmail: "=?",
            idSuffix: "=?",
            enforceUniqueEmail: "=?",
            onInputChange: "&?",
            // True to hide the address key input
            hideKey: "=?",
            // True to hide the "Need to enter more phone numbers?" UI
            hideMorePhones: "=?",
            // True to hide the fax input
            hideFax: "=?",
            // True to hide the address inputs (basically the right side of the form)
            hideAddress: "=?",
            // Changes which autocomplete text value to use on address inputs
            isBilling: "=?",
            // For two-way binding the username
            username: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/contact/contactEditor.html", "ui"),
        controller: ContactWidgetController,
        controllerAs: "contactCtrl",
        bindToController: true
    }));
}
