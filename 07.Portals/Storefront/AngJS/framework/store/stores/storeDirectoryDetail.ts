module cef.store.stores {
    class StoreDirectoryDetail extends core.TemplatedControllerBase {
        // Properties
        hasCompanyLogo = false;
        hasSalesRep = false;
        companyLogoFilename = "";
        salesRepFilename = "";

        seourl: string;
        store: api.StoreModel;
        user: api.UserModel;
        roleUser: api.RoleForUserModel[] = [];
        states: api.RegionModel[] = [];
        countries: api.CountryModel[] = [];
        storeImages = [];
        showPremium = false;
        weekdayStart: Date;
        weekdayEnd: Date;
        weekendStart: Date;
        weekendEnd: Date;
        addressForGoogleMaps = "";
        addressForGoogleMapsShort = "";
        googleMap;

        load(): void { this.getStore(); }

        loadStoreImages(): void {
            this.storeImages.push({ iconImageFile: "store-header-icon.jpg", customKeyText: "storeimage1", height: "400", width: "1200", uploadImage: "uploadStoreImage1", uploadedImage: "uploadedStoreImage1", iconImageAlt: "Store Header Image", description: "Store Header Image - shown at the top of the store details page, behind the company name and slogan.", has: false, filename: null, storeImageList: true, index: 0 });
            this.storeImages.push({ iconImageFile: "primary-icon.jpg", customKeyText: "storeimage2", height: "768", width: "768", uploadImage: "uploadStoreImage2", uploadedImage: "uploadedStoreImage2", iconImageAlt: "Primary Store Image", description: "Primary Store Image - Typically an image of the primary store contact, shown behind the primary contact's name and title.", has: false, filename: null, storeImageList: true, index: 1 });
            this.storeImages.push({ iconImageFile: "top-left-icon.jpg", customKeyText: "storeimage3", height: "384", width: "384", uploadImage: "uploadStoreImage3", uploadedImage: "uploadedStoreImage3", iconImageAlt: "Top Left - Secondary Store Image", description: "Top Left Store Image - shown at top left of secondary images area on the details page.", has: false, filename: null, storeImageList: true, index: 2 });
            this.storeImages.push({ iconImageFile: "top-right-icon.jpg", customKeyText: "storeimage4", height: "384", width: "384", uploadImage: "uploadStoreImage4", uploadedImage: "uploadedStoreImage4", iconImageAlt: "Top Right - Secondary Store Image", description: "Top Right Store Image - shown at top right of secondary images area on the details page.", has: false, filename: null, storeImageList: true, index: 3 });
            this.storeImages.push({ iconImageFile: "bottom-left-icon.jpg", customKeyText: "storeimage5", height: "384", width: "384", uploadImage: "uploadStoreImage5", uploadedImage: "uploadedStoreImage5", iconImageAlt: "Bottom Left - Secondary Store Image", description: "Bottom Left Store Image - shown at bottom left of secondary images area on the details page.", has: false, filename: null, storeImageList: true, index: 4 });
            this.storeImages.push({ iconImageFile: "bottom-right-icon.jpg", customKeyText: "storeimage6", height: "384", width: "384", uploadImage: "uploadStoreImage6", uploadedImage: "uploadedStoreImage6", iconImageAlt: "Bottom Right - Secondary Store Image", description: "Bottom Right Store Image - shown at bottom right of secondary images area on the details page.", has: false, filename: null, storeImageList: true, index: 5 });
        }

        convertToHHMM(time: number): Date {
            const hour = time;
            const minute = Math.round((time - hour) * 60);
            return new Date(`2020-01-01 ${hour}:${minute}`);
        }

        getStoreTimes(): void {
            this.weekdayStart = this.convertToHHMM(this.store.OperatingHoursMondayStart);
            this.weekdayEnd = this.convertToHHMM(this.store.OperatingHoursMondayEnd);
            this.weekendStart = this.convertToHHMM(this.store.OperatingHoursSaturdayStart);
            this.weekendEnd = this.convertToHHMM(this.store.OperatingHoursSaturdayEnd);
        }

        getStoreImages(): void {
            _.forEachRight(this.store.Images, file => {
                if (file.CustomKey.toLowerCase().indexOf("salesrep") > 0 && file.Active) {
                    this.salesRepFilename = file.OriginalFileName;
                    this.hasSalesRep = true;
                    return false;
                }
                return true;
            });
            _.forEachRight(this.store.Images, file => {
                if (file.CustomKey.toLowerCase().indexOf("companylogo") > 0 && file.Active) {
                    this.companyLogoFilename = file.OriginalFileName;
                    this.hasCompanyLogo = true;
                    return false;
                }
                return true;
            });
            this.getStoreImage();
        }

        getStoreImage(): void {
            this.storeImages.forEach(si => {
                _.forEachRight(this.store.Images, file => {
                    if (file.CustomKey.toLowerCase().indexOf(si.customKeyText) > 0 && file.Active) {
                        si.filename = file.OriginalFileName;
                        si.has = true;
                        return false;
                    }
                    return true;
                });
            });
        }

        getStoreMap() {
            this.googleMap = { center: { latitude: 6, longitude: -9 }, zoom: 8 };
        }

        getStore() {
            this.cvCurrentStoreService.getCurrentStorePromise(true).then(response => {
                this.store = response;
                // Build up Address for Google Maps Geocoding
                this.addressForGoogleMaps += (this.store.Contact.Address.PostalCode !== undefined && this.store.Contact.Address.PostalCode !== null) ? this.store.Contact.Address.PostalCode + " " : "";
                this.addressForGoogleMaps += (this.store.Contact.Address.Street1 !== undefined && this.store.Contact.Address.Street1 !== null) ? this.store.Contact.Address.Street1 + " " : "";
                this.addressForGoogleMaps += (this.store.Contact.Address.Street2 !== undefined && this.store.Contact.Address.Street2 !== null) ? this.store.Contact.Address.Street2 + " " : "";
                this.addressForGoogleMaps += (this.store.Contact.Address.City !== undefined && this.store.Contact.Address.City !== null) ? this.store.Contact.Address.City + " " : "";
                this.addressForGoogleMaps += (this.store.Contact.Address.Region !== undefined && this.store.Contact.Address.Region !== null) ? this.store.Contact.Address.Region.Name + " " : "";
                this.addressForGoogleMaps += (this.store.Contact.Address.Country !== undefined && this.store.Contact.Address.Country !== null) ? this.store.Contact.Address.Country.Name : "";
                this.addressForGoogleMapsShort += (this.store.Contact.Address.Region !== undefined && this.store.Contact.Address.Region !== null) ? this.store.Contact.Address.Region.Name + " " : "";
                this.addressForGoogleMapsShort += (this.store.Contact.Address.Country !== undefined && this.store.Contact.Address.Country !== null) ? this.store.Contact.Address.Country.Name : "";
                this.showPremium = this.store.MissionStatement.toLowerCase() === "directory premium";
                this.loadStoreImages();
                this.getStoreImages();
                this.getStoreTimes();
                this.getStoreMap();
            });
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvCurrentStoreService: services.ICurrentStoreService) {
            super(cefConfig);
            this.load();
        }
    }

    cefApp
        .config((uiGmapGoogleMapApiProvider, cefConfig: core.CefConfig) => {
            if (cefConfig.google.maps.enabled) {
                uiGmapGoogleMapApiProvider.configure({
                    key: cefConfig.google.maps.apiKey
                });
            }
        })
        .directive("cefStoreDirectoryDetail", ($filter: ng.IFilterService): ng.IDirective => ({
            restrict: "EA",
            templateUrl: $filter("corsLink")("/framework/store/stores/storeDirectoryDetail.html", "ui"),
            controller: StoreDirectoryDetail,
            controllerAs: "storeDirectoryDetail"
        }));
}