module cef.store.widgets.forms.contact {
    class ContactAddressBlockController extends core.TemplatedControllerBase {
        // Properties
        hideIcons: boolean;
        hidePhones: boolean;
        hideFax: boolean;
        hideEmail: boolean;
        hideAddress: boolean;
        hideCountry: boolean;
        hideName: boolean;
        hideCompany: boolean;
        hideTitle: boolean;
        smallText: boolean;
        littleSmallerText: boolean;
        noLinks: boolean;
        twoCol: boolean;
        index: string;
        private _isInService: boolean;
        get isInService(): boolean {
            return this._isInService;
        }
        private _contact: api.ContactModel;
        get contact(): api.ContactModel {
            return this._contact;
        }
        set contact(newValue: api.ContactModel) {
            this._contact = newValue;
            this._contactArray = null;
            this._addressArray = null;
        }
        private _contactArray: Array<{ fa: string; text: string }> = null;
        get contactArray(): Array<{ fa: string; text: string }> { 
            if (this._contactArray && this._contactArray.length > 0) {
                return this._contactArray;
            }
            this.getContactUIObjectsArray();
            return this._contactArray;
        }
        private _addressArray: Array<{ text: string }> = null;
        get addressArray(): Array<{ text: string }> {
            if (this._addressArray
                && this._addressArray.length) {
                return this._addressArray;
            }
            this.getContactAddressUIObjectsArray();
            return this._addressArray;
        }
        // Functions
        showContactPhone1(): boolean {
            if (this.hidePhones) { return false; }
            if (!this.contact || !this.contact.Phone1) { return false; }
            return true;
        }
        showContactPhone2(): boolean {
            if (this.hidePhones) { return false; }
            if (!this.contact || !this.contact.Phone2) { return false; }
            return true;
        }
        showContactPhone3(): boolean {
            if (this.hidePhones) { return false; }
            if (!this.contact || !this.contact.Phone3) { return false; }
            return true;
        }
        showContactFax1(): boolean {
            if (this.hideFax) { return false; }
            if (!this.contact || !this.contact.Fax1) { return false; }
            return true;
        }
        showContactEmail1(): boolean {
            if (this.hideEmail) { return false; }
            if (!this.contact || !this.contact.Email1) { return false; }
            return true;
        }
        getContactUIObjectsArray(): void {
            if (!this.contact || this.viewState.running) {
                return;
            }
            this.setRunning();
            let arr: Array<{ fa: string, text: string, tel?: boolean, mailto?: boolean }> = [];
            if (this.showContactPhone1()) { arr.push({ fa: "far fa-phone-rotary", text: this.contact.Phone1, tel: true }); }
            if (this.showContactPhone2()) { arr.push({ fa: "far fa-phone-rotary", text: this.contact.Phone2, tel: true }); }
            if (this.showContactPhone3()) { arr.push({ fa: "far fa-phone-rotary", text: this.contact.Phone3, tel: true }); }
            arr = _.uniqBy(arr, "text");
            if (this.showContactFax1()) { arr.push({ fa: "far fa-fax", text: this.contact.Fax1 }); }
            if (this.showContactEmail1()) { arr.push({ fa: "far fa-at", text: this.contact.Email1, mailto: true }); }
            this._contactArray = arr;
            this.finishRunning();
        }
        showContactCompany(): boolean {
            if (this.hideCompany) { return false; }
            if (!this.contact || !this.contact.Address || !this.contact.Address.Company) { return false; }
            return true;
        }
        showContactFullName(): boolean {
            if (this.hideName) { return false; }
            if (!this.contact || (!this.contact.FirstName && !this.contact.LastName)) { return false; }
            return true;
        }
        showContactAddressStreet1(): boolean {
            if (this.hideAddress) { return false; }
            if (!this.contact || !this.contact.Address || !this.contact.Address.Street1) { return false; }
            return true;
        }
        showContactAddressStreet2(): boolean {
            if (this.hideAddress) { return false; }
            if (!this.contact || !this.contact.Address || !this.contact.Address.Street2) { return false; }
            return true;
        }
        showContactAddressStreet3(): boolean {
            if (this.hideAddress) { return false; }
            if (!this.contact || !this.contact.Address || !this.contact.Address.Street3) { return false; }
            return true;
        }
        showContactAddressCityStateZip(): boolean {
            if (this.hideAddress) { return false; }
            if (!this.contact || !this.contact.Address
                || (!this.contact.Address.City && !this.contact.Address.RegionCode && !this.contact.Address.PostalCode))
            {
                return false;
            }
            return true;
        }
        showContactAddressCountry(): boolean {
            if (this.hideCountry) { return false; }
            if (!this.contact || !this.contact.Address
                || (!this.contact.Address.CountryName && (!this.contact.Address.Country || !this.contact.Address.Country.Name)))
            {
                return false;
            }
            return true;
        }
        getContactAddressUIObjectsArray(): void {
            if (!this.contact || this.viewState.running) {
                return;
            }
            this.setRunning();
            const arr: Array<{ text: string }> = [];
            if (this.showContactFullName()) { arr.push({ text: ((this.contact.FirstName || "") + " " + (this.contact.LastName || "")).trim() }); }
            if (this.showContactCompany()) { arr.push({ text: this.contact.Address.Company }); }
            if (this.showContactAddressStreet1()) { arr.push({ text: this.contact.Address.Street1 }); }
            if (this.showContactAddressStreet2()) { arr.push({ text: this.contact.Address.Street2 }); }
            if (this.showContactAddressStreet3()) { arr.push({ text: this.contact.Address.Street3 }); }
            if (this.showContactAddressCityStateZip()) {
                arr.push({
                    text: ((this.contact.Address.City + ", "
                        + ((this.contact.Address.RegionCode || this.contact.Address.Region && this.contact.Address.Region.Code) || "")
                        + " " + this.contact.Address.PostalCode).trim()).replace(/\s+/, " ")
                });
            }
            if (this.showContactAddressCountry()) { arr.push({ text: this.contact.Address.CountryName || this.contact.Address.Country.Name }); }
            this._addressArray = _.uniqBy(arr, "text");
            this.finishRunning();
        }
        updateIsInService(): void {
            /* NOTE: This endpoint is in a client specific library
            this.setRunning();
            this.cvApi.contractors.CheckForServiceAtAddress(this.contact.Address)
                .then(val => {
                    this._isInService = val ? true : true;
                    this.finishRunning();
                }).catch(err => {
                    this._isInService = false;
                    this.finishRunning(true, err);
                });
            */
        }
        // Constructor
        constructor(
            protected readonly cefConfig: core.CefConfig,
            protected readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.updateIsInService();
        }
    }

    cefApp.directive("cefContactAddressBlock", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            contact: "=",
            hideIcons: "=?",
            hidePhones: "=?",
            hideFax: "=?",
            hideEmail: "=?",
            hideAddress: "=?",
            hideCountry: "=?",
            hideName: "=?",
            hideCompany: "=?",
            hideTitle: "=?",
            smallText: "=?",
            littleSmallerText: "=?",
            noLinks: "=?",
            twoCol: "=?",
            index: "@?"
        },
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/contact/contactAddressBlock.html", "ui"),
        controller: ContactAddressBlockController,
        controllerAs: "wcabCtrl",
        bindToController: true
    }));
}
