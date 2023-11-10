/**
 * @file framework/store/_factories/cvContactFactory.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Contact Factory, generates new contacts with all the basic properties
 * set up for use in an contact form
 */
module cef.store.factories {
    export interface IContactFactory {
        new: () => api.ContactModel;
        upsert: (contact: api.ContactModel) => ng.IHttpPromise<api.CEFActionResponseT<number>>;
    }

    /**
     * @export
     * @class ContactFactoryFn
     * @implements {IContactFactory}
     */
    export const cvContactFactoryFn = (cvApi: api.ICEFAPI, cefConfig: core.CefConfig, cvCountryService: services.ICountryService): IContactFactory => {
        let countries: api.CountryModel[] = [];
        let defaultCountry: api.CountryModel = null;
        cvCountryService.search({ AsListing: true, Sorts: [{ field: "Name", order: 0, dir: "asc" }] }).then(c => {
            countries = c;
            defaultCountry = _.find(countries, x => x.Code === cefConfig.countryCode);
        });
        const newContact = (): api.ContactModel => {
            return <api.ContactModel>{
                // Base Properties
                ID: null,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // Contact Properties
                FirstName: null,
                MiddleName: null,
                LastName: null,
                FullName: null,
                Title: null,
                Phone1: null,
                Phone2: null,
                Phone3: null,
                Email1: null,
                Email2: null,
                Email3: null,
                Fax1: null,
                Website1: null,
                Website2: null,
                Website3: null,
                SameAsBilling: false,
                // Related Objects
                TypeID: null,
                TypeKey: "General",
                Type: null,
                TypeName: null,
                TypeDisplayName: null,
                TypeSortOrder: null,
                AddressID: null,
                AddressKey: null,
                Address: <api.AddressModel>{
                    // Base Properties
                    ID: null,
                    CustomKey: null,
                    Active: true,
                    CreatedDate: new Date(),
                    UpdatedDate: null,
                    // Address Properties
                    Company: null,
                    Street1: null,
                    Street2: null,
                    Street3: null,
                    City: null,
                    PostalCode: null,
                    IsBilling: false,
                    IsPrimary: false,
                    Latitude: null,
                    Longitude: null,
                    // Related Objects
                    Country: defaultCountry,
                    CountryID: defaultCountry ? defaultCountry.ID : null,
                    CountryKey: defaultCountry ? defaultCountry.CustomKey : null,
                    CountryCode: defaultCountry ? defaultCountry.Code : null,
                    CountryName: defaultCountry ? defaultCountry.Name : null,
                    Region: null,
                    RegionID: null,
                    RegionKey: null,
                    RegionCode: null,
                    RegionName: null,
                }
            };
        };
        return <IContactFactory>{
            defaultCountry: () => defaultCountry,
            new: newContact,
            upsert: cvApi.contacts.UpsertContact
        };
    }
}
