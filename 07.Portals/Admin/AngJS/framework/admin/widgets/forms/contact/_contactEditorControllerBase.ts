/**
 * @file framework/admin/widgets/forms/contact/_contactEditorControllerBase.ts
 * @author Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
 * @desc Contact Widget (edits contacts) class
 */
module cef.admin.widgets.forms.contact {
    export abstract class ContactEditorControllerBase extends core.TemplatedControllerBase {
        // Bound Scope Properties
        private _contact: api.ContactModel;
        get contact(): api.ContactModel {
            return this._contact;
        }
        set contact(newValue: api.ContactModel) {
            this._contact = newValue;
        }
        protected existing: api.AccountContactModel;
        hideKey: boolean | string;
        hideMorePhones: boolean | string;
        hideFax: boolean | string;
        hideAddress: boolean | string;
        hidePersonalDetails: boolean | string;
        // Properties
        countries: api.CountryModel[] = [];
        regions: api.RegionModel[] = [];
        configs: core.IPersonalDetailsDisplay;
        showPersonalDetails: boolean;
        showAddress: boolean;
        showKey: boolean;
        showFirstName: boolean;
        showLastName: boolean;
        showEmail: boolean;
        showPhone: boolean;
        showPhone2: boolean;
        showPhone3: boolean;
        showMorePhones: boolean;
        showFax: boolean;
        showStreet2: boolean;
        showStreet3: boolean;
        ddlCountries: api.CountryModel[];
        selectedCountryName: string;
        duplicateKey: boolean = false;
        onInputChange: Function;
        // Functions
        protected abstract loadAdditional(): void;
        protected load(): void {
            this.configs = this.cefConfig.personalDetailsDisplay;
            this.showKey = !this.configs.hideAddressBookKeys && !Boolean(this.hideKey);
            this.showFirstName = !this.configs.hideAddressBookFirstName;
            this.showLastName = !this.configs.hideAddressBookLastName;
            this.showEmail = !this.configs.hideAddressBookEmail;
            this.showPhone = !this.configs.hideAddressBookPhone;
            this.showMorePhones = !this.configs.hideAddressBookPhone && !Boolean(this.hideMorePhones);
            this.showFax = !this.configs.hideAddressBookFax && !Boolean(this.hideFax);
            this.showPersonalDetails = (this.showFirstName
                || this.showLastName
                || this.showEmail
                || this.showPhone
                || this.showFax)
                && !Boolean(this.hidePersonalDetails);
            this.showAddress = !Boolean(this.hideAddress);
            this.cvCountryService.search({
                Active: true,
                AsListing: true,
                Sorts: [{ field: "Name", order: 0, dir: "asc" }]
            }).then(c => this.countries = this.ddlCountries = c);
            this.loadAdditional();
        }
        callOnChangeIfSet(): void {
            if (angular.isFunction(this.onInputChange)) {
                // Wait a moment so the changes can cement to the model (an angular digest cycle)
                this.$timeout(() => this.onInputChange(), 250);
            }
        }
        runPhonePrefixLookups(): void {
            this.callOnChangeIfSet();
            if (!this.cefConfig.usePhonePrefixLookups.enabled // Don't use this
                || !this.contact.Phone1 // Must have content
                || /\d+/.exec(this.contact.Phone1).length <= 0 // Must have a number in the value
            ) { return; }
            const cleanPhone = `+${this.contact.Phone1.trim().replace(/[a-zA-Z\s)(+_-]+/, "")}`;
            this.cvApi.geography.ReversePhonePrefixToCityRegionCountry({
                Prefix: cleanPhone
            }).then(r => {
                if (!r || !r.data || !r.data.Results) {
                    ////this.consoleDebug("Failed to get phone prefix lookups data.");
                    return;
                }
                if (r.data.Results.length <= 0) {
                    ////this.consoleDebug(`No results from phone prefix lookups data for "${cleanPhone}"`);
                    return;
                }
                this.contact.Address.CountryID = r.data.Results[0].CountryID;
                this.propagateCountryChange();
                this.contact.Address.RegionID = r.data.Results[0].RegionID;
                this.propagateRegionChange();
                this.contact.Address.City = r.data.Results[0].CityName;
            });
        }
        propagateCountryChange(): void {
            this.$timeout(() => {
                this.callOnChangeIfSet();
                this.contact.Address.CountryID = this.contact.Address.CountryID
                    ? this.contact.Address.CountryID
                    : null;
                if (this.contact.Address.CountryID == null) {
                    this.contact.Address.Country = null;
                    this.contact.Address.CountryKey =
                        this.contact.Address.CountryCode =
                        this.contact.Address.CountryName = null;
                    return;
                }
                this.contact.Address.Country = _.find(this.ddlCountries,
                    v => v.ID === this.contact.Address.CountryID);
                this.contact.Address.CountryKey = this.contact.Address.Country.CustomKey;
                this.contact.Address.CountryCode = this.contact.Address.Country.Code;
                this.contact.Address.CountryName = this.contact.Address.Country.Name;
                this.updateRegions();
            }, 250);
        }
        propagateRegionChange(): void {
            this.$timeout(() => {
                this.callOnChangeIfSet();
                this.contact.Address.RegionID = this.contact.Address.RegionID
                    ? this.contact.Address.RegionID
                    : null;
                if (this.contact.Address.RegionID == null) {
                    this.contact.Address.Region = null;
                    this.contact.Address.RegionKey =
                        this.contact.Address.RegionCode =
                        this.contact.Address.RegionName = null;
                    return;
                }
                this.contact.Address.Region = _.find(this.regions,
                    v => v.ID === this.contact.Address.RegionID);
                this.contact.Address.RegionKey = this.contact.Address.Region.CustomKey;
                this.contact.Address.RegionCode = this.contact.Address.Region.Code;
                this.contact.Address.RegionName = this.contact.Address.Region.Name;
            }, 250);
        }
        updateRegions(): void {
            if (!this.contact.Address.CountryID) {
                this.regions = undefined;
                return;
            }
            this.cvRegionService.search({
                Active: true,
                AsListing: true,
                Sorts: [{ field: "Name", order: 0, dir: "asc" }],
                CountryID: this.contact.Address.CountryID
            }).then(r => this.regions = r);
        }
        filterCountries(): void {
            const countryName: string = this.selectedCountryName;
            if (!countryName) {
                return;
            }
            const country = _.find(this.ddlCountries,
                (ddlCountry: api.CountryModel) =>
                    ddlCountry.Name.toLowerCase() === countryName.toLowerCase());
            if (country == null) {
                this.ddlCountries = _.filter(this.countries,
                    ddlCountry =>
                        _.includes(ddlCountry.Name.toLowerCase(), countryName.toLowerCase()));
            } else {
                this.selectCountry(country);
                this.ddlCountries = undefined;
            }
        }
        selectCountry(country: api.CountryModel): void {
            this.contact.Address.CountryID = country.ID;
            this.propagateCountryChange();
            ////this.showVatID = this.checkForVat(country, type);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                protected readonly $timeout: ng.ITimeoutService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvApi: api.ICEFAPI,
                protected readonly cvCountryService: services.ICountryService,
                protected readonly cvRegionService: services.IRegionService) {
            super(cefConfig);
            // Call load in the inherited constructors, not here
        }
    }
}
