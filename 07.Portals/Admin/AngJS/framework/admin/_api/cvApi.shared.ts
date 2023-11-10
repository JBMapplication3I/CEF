/**
 * @file framework/admin/_api/clarityEcomService_shared.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc Clarity eCommerce API/service shared class
 */
module cef.admin.api {
    export abstract class ServiceStackRoute {
        // The root URL for making RESTful calls
        get rootUrl(): string { return this.service.rootUrl; }
        get $http(): ng.IHttpService { return this.service.$http; }
        constructor(public readonly service: ICEFAPI) { }
    }

    /*
    type Guid = string & { isGuid: true };

    export class Guid {
        private static validator = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
        private _value: string;
        get value(): string {
            return this._value;
        }
        set value(newValue: string) {
            if (!Guid.validator.test(newValue)) {
                throw new Error(`ERROR! The provided value: '${newValue}' is not a valid Guid format.`);
            }
            this._value = newValue;
        }
        constructor();
        constructor(source?: string) {
            if (source && source.length > 0) {
                this.value = source;
                return;
            }
            this.value = Guid.newGuid();
        }
        toString(): string {
            return this.value;
        }
        static empty(): string {
            return "00000000-0000-0000-0000-000000000000";
        }
        static newGuid(): string {
            return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, c => {
                const r = Math.random() * 16 | 0, v = c === "x" ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
        }
    }
    */

    export class Guid {
        public static validator = new RegExp("^[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}$", "i");
        public static EMPTY = "00000000-0000-0000-0000-000000000000";
        public static isGuid(guid: any) {
            const value: string = guid.toString();
            return guid && (guid instanceof Guid || Guid.validator.test(value));
        }
        public static create(): Guid {
            return new Guid([Guid.gen(2), Guid.gen(1), Guid.gen(1), Guid.gen(1), Guid.gen(3)].join("-"));
        }
        public static createEmpty(): Guid {
            return new Guid("emptyguid");
        }
        public static parse(guid: string): Guid {
            return new Guid(guid);
        }
        public static raw(): string {
            return [Guid.gen(2), Guid.gen(1), Guid.gen(1), Guid.gen(1), Guid.gen(3)].join("-");
        }
        private static gen(count: number) {
            let out: string = "";
            for (let i: number = 0; i < count; i++) {
                // tslint:disable-next-line:no-bitwise
                out += (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            }
            return out;
        }
        private value: string;
        private constructor(guid: string) {
            if (!guid) { throw new TypeError("Invalid argument; `value` has no value."); }
            this.value = Guid.EMPTY;
            if (guid && Guid.isGuid(guid)) {
                this.value = guid;
            }
        }
        public equals(other: Guid): boolean {
            // Comparing string `value` against provided `guid` will auto-call
            // toString on `guid` for comparison
            return Guid.isGuid(other) && this.value === other.toString();
        }
        public isEmpty(): boolean {
            return this.value === Guid.EMPTY;
        }
        public toString(): string {
            return this.value;
        }
        public toJSON(): any {
            return {
                value: this.value,
            };
        }
    }

    /**
     * The key is actually whatever type you provided, but
     * this fails the linter
     */
    export class KeyValuePair<TKey,TValue> {
        [key: /*TKey*/string]: TValue;
    }

    /**
     * @name CEFActionResponse
     */
    export interface CEFActionResponse {
        // ActionSucceeded: boolean; // Generated
        // Messages?: Array<string>; // Generated
    }

    /**
     * @name CEFActionResponseT<TResult>
     * @property {TResult} Result
     * @inheritdoc {CEFActionResponse}
     */
    export interface CEFActionResponseT<TResult> extends CEFActionResponse {
        Result: TResult;
    }

    /**
     * @public
     */
    export interface ImplementsDisplayNameBase {
        noCache?: number;
    }

    /**
     * @public
     */
    export interface ImplementsIDBase {
        noCache?: number;
    }

    /**
     * @public
     */
    export interface ImplementsIDOnQueryBase {
        ID: number;
        noCache?: number;
    }

    /**
     * @public
     */
    export interface ImplementsKeyBase {
        noCache?: number;
    }

    /**
     * @public
     */
    export interface ImplementsNameBase {
        noCache?: number;
    }

    /**
     * @public
     */
    export interface ImplementsTypeNameBase {
        noCache?: number;
    }

    export interface BaseModel {
        __caller?: string;
    }

    export interface HaveAParentBaseModel<TModel> {
        ParentID?: number;
        Parent?: TModel;
        Children?: Array<TModel>;
        /** Whether or not the object has child objects (read-only) */
        HasChildren: boolean;
    }

    export interface HaveJsonAttributesBaseModel {
        /** A list of extending Attributes for the this object */
        SerializableAttributes?: SerializableAttributesDictionary;
    }

    export interface HaveATypeSearchModel {
        /** The Type ID for objects */
        TypeID?: number;
        /** The Type ID for objects to specifically exclude */
        ExcludedTypeID?: number;
        /** The Type IDs for objects to specifically exclude */
        ExcludedTypeIDs?: Array<number>;
        /** The Type Key for objects */
        TypeKey?: string;
        /** The Type Key for objects to specifically exclude */
        ExcludedTypeKey?: string;
        /** The Type Keys for objects to specifically exclude */
        ExcludedTypeKeys?: Array<string>;
        /** The Type Name for objects */
        TypeName?: string;
        /** The Type Name for objects to specifically exclude */
        ExcludedTypeName?: string;
        /** The Type Names for objects to specifically exclude */
        ExcludedTypeNames?: Array<string>;
        /** The Type Display Name for objects */
        TypeDisplayName?: string;
        /** The Type Display Name for objects to specifically exclude */
        ExcludedTypeDisplayName?: string;
        /** The Type Display Names for objects to specifically exclude */
        ExcludedTypeDisplayNames?: Array<string>;
        /** The Type Translation Key for objects */
        TypeTranslationKey?: string;
    }

    export interface HaveATypeSearchModel {
        /** The Type ID for objects */
        TypeID?: number;
        /** The Type ID for objects to specifically exclude */
        ExcludedTypeID?: number;
        /** The Type IDs for objects to specifically exclude */
        ExcludedTypeIDs?: Array<number>;
        /** The Type Key for objects */
        TypeKey?: string;
        /** The Type Key for objects to specifically exclude */
        ExcludedTypeKey?: string;
        /** The Type Keys for objects to specifically exclude */
        ExcludedTypeKeys?: Array<string>;
        /** The Type Name for objects */
        TypeName?: string;
        /** The Type Name for objects to specifically exclude */
        ExcludedTypeName?: string;
        /** The Type Names for objects to specifically exclude */
        ExcludedTypeNames?: Array<string>;
        /** The Type Display Name for objects */
        TypeDisplayName?: string;
        /** The Type Display Name for objects to specifically exclude */
        ExcludedTypeDisplayName?: string;
        /** The Type Display Names for objects to specifically exclude */
        ExcludedTypeDisplayNames?: Array<string>;
        /** The Type Translation Key for objects */
        TypeTranslationKey?: string;
    }

    export interface HaveSeoBaseModel {
        /** SEO Keywords to use in the Meta tags of the page for this object */
        SeoKeywords?: string;
        /** SEO URL to use to link to the page for this object */
        SeoUrl?: string;
        /** SEO Description to use in the Meta tags of the page for this object */
        SeoDescription?: string;
        /** SEO General Meta Data to use in the Meta tags of the page for this object */
        SeoMetaData?: string;
        /** SEO Page Title to use in the Meta tags of the page for this object */
        SeoPageTitle?: string;
    }

    export interface HaveNotesBaseModel {
        /** Notes for the object, optional */
        Notes?: Array<NoteModel>;
    }

    export interface TreeAggregate {
        PrimaryImageFileName?: string;
        DisplayName?: string;
    }

    export interface HaveImagesBaseModel<TImageModel extends IImageBaseModel> {
        /** Images for the object, optional */
        Images?: Array<TImageModel>;
    }

    export interface IImageBaseModel
        extends
            BaseModel,
            HaveATypeModel<TypeModel> {
        TypeKey?: string;
        TypeName?: string;
        TypeDisplayName?: string;
        TypeTranslationKey?: string;
        TypeSortOrder?: number;
        SortOrder?: number;
        DisplayName?: string;
        SeoTitle?: string;
        Author?: string;
        MediaDate?: Date;
        Copyright?: string;
        Location?: string;
        Latitude?: number;
        Longitude?: number;
        IsPrimary: boolean;
        OriginalWidth?: number;
        OriginalHeight?: number;
        OriginalFileFormat?: string;
        OriginalFileName?: string;
        OriginalIsStoredInDB: boolean;
        OriginalBytes?: Array<number>;
        ThumbnailWidth?: number;
        ThumbnailHeight?: number;
        ThumbnailFileFormat?: string;
        ThumbnailFileName?: string;
        ThumbnailIsStoredInDB: boolean;
        ThumbnailBytes?: Array<number>;
        MasterID?: number;
        MasterKey?: string;
    }

    export interface AmARelationshipTableModel<TSlaveModel extends BaseModel> extends BaseModel {
        /** Gets or sets the identifier of the master. */
        MasterID?: number;
        /** Gets or sets the master key. */
        MasterKey?: string;
        /** Gets or sets the identifier of the slave. */
        SlaveID?: number;
        /** Gets or sets the slave key. */
        SlaveKey?: string;
        /** Gets or sets the slave. */
        Slave?: TSlaveModel;
    }

    export interface AmAStoredFileRelationshipTableModel extends AmARelationshipTableModel<StoredFileModel>, HaveSeoBaseModel, NameableBaseModel {
        /** Gets or sets the identifier of the file access type. */
        FileAccessTypeID: number;
        /** Gets or sets the sort order. */
        SortOrder?: number;
    }

    export interface HaveStoredFilesBaseModel<TFileModel extends AmAStoredFileRelationshipTableModel> {
        /** StoredFiles for the object, optional */
        StoredFiles?: Array<TFileModel>;
    }

    export interface HaveAStatusModel {
        /** Identifier for the Status of this object, required if no StatusModel present */
        StatusID: number;
        /** Model for Status of this object, required if no StatusID present */
        Status?: StatusModel;
        /** Key for the Status of this object, read-only */
        StatusKey?: string;
        /** Name for the Status of this object, read-only */
        StatusName?: string;
        /** The status display name */
        StatusDisplayName?: string;
        /** The status translation key */
        StatusTranslationKey?: string;
        /** The status sort order */
        StatusSortOrder?: number;
    }

    export interface HaveAStateModel {
        /** Identifier for the State of this object, required if no StateModel present */
        StateID: number;
        /** Model for State of this object, required if no StateID present */
        State?: StatusModel;
        /** Key for the State of this object, read-only */
        StateKey?: string;
        /** Name for the State of this object, read-only */
        StateName?: string;
        /** The state display name */
        StateDisplayName?: string;
        /** The state translation key */
        StateTranslationKey?: string;
        /** The state sort order */
        StateSortOrder?: number;
    }

    export interface HaveATypeModel<TTypeModel> {
        /** Identifier for the Type of this object, required if no TypeModel present */
        TypeID: number;
        /** Model for Type of this object, required if no TypeID present */
        Type?: TTypeModel;
        /** Key for the Type of this object, read-only */
        TypeKey?: string;
        /** Name for the Type of this object, read-only */
        TypeName?: string;
        /** The type display name */
        TypeDisplayName?: string;
        /** The type translation key */
        TypeTranslationKey?: string;
        /** The type sort order */
        TypeSortOrder?: number;
    }

    export interface HaveAStatusSearchModel {
        /** The Status ID for objects */
        StatusID?: number;
        /** The Status ID for objects to specifically exclude */
        ExcludedStatusID?: number;
        /** The Status IDs for objects to specifically exclude */
        ExcludedStatusIDs?: Array<number>;
        /** The Status Key for objects */
        StatusKey?: string;
        /** The Status Key for objects to specifically exclude */
        ExcludedStatusKey?: string;
        /** The Status Keys for objects to specifically exclude */
        ExcludedStatusKeys?: Array<string>;
        /** The Status Keys for objects to specifically include */
        StatusKeys?: Array<string>;
        /** The Status Name for objects */
        StatusName?: string;
        /** The Status Name for objects to specifically exclude */
        ExcludedStatusName?: string;
        /** The Status Names for objects to specifically exclude */
        ExcludedStatusNames?: Array<string>;
        /** The Status Names for objects to specifically include */
        StatusNames?: Array<string>;
        /** The Status Display Name for objects */
        StatusDisplayName?: string;
        /** The Status Display Name for objects to specifically exclude */
        ExcludedStatusDisplayName?: string;
        /** The Status Display Names for objects to specifically exclude */
        ExcludedStatusDisplayNames?: Array<string>;
        /** The Status Display Names for objects to specifically include */
        StatusDisplayNames?: Array<string>;
        /** The Status Translation Key for objects */
        StatusTranslationKey?: string;
    }

    export interface HaveAStatusBaseSearchModel {
        /** The Status ID for objects */
        StatusID?: number;
        /** The Status ID for objects to specifically exclude */
        ExcludedStatusID?: number;
        /** The Status IDs for objects to specifically exclude */
        ExcludedStatusIDs?: Array<number>;
        /** The Status Key for objects */
        StatusKey?: string;
        /** The Status Key for objects to specifically exclude */
        ExcludedStatusKey?: string;
        /** The Status Keys for objects to specifically exclude */
        ExcludedStatusKeys?: Array<string>;
        /** The Status Name for objects */
        StatusName?: string;
        /** The Status Name for objects to specifically exclude */
        ExcludedStatusName?: string;
        /** The Status Names for objects to specifically exclude */
        ExcludedStatusNames?: Array<string>;
        /** The Status Display Name for objects */
        StatusDisplayName?: string;
        /** The Status Display Name for objects to specifically exclude */
        ExcludedStatusDisplayName?: string;
        /** The Status Display Names for objects to specifically exclude */
        ExcludedStatusDisplayNames?: Array<string>;
        /** The Status Translation Key for objects */
        StatusTranslationKey?: string;
    }

    export interface HaveAStateSearchModel {
        /** The State ID for objects */
        StateID?: number;
        /** The State ID for objects to specifically exclude */
        ExcludedStateID?: number;
        /** The State IDs for objects to specifically exclude */
        ExcludedStateIDs?: Array<number>;
        /** The State Key for objects */
        StateKey?: string;
        /** The State Key for objects to specifically exclude */
        ExcludedStateKey?: string;
        /** The State Keys for objects to specifically exclude */
        ExcludedStateKeys?: Array<string>;
        /** The State Name for objects */
        StateName?: string;
        /** The State Name for objects to specifically exclude */
        ExcludedStateName?: string;
        /** The State Names for objects to specifically exclude */
        ExcludedStateNames?: Array<string>;
        /** The State Display Name for objects */
        StateDisplayName?: string;
        /** The State Display Name for objects to specifically exclude */
        ExcludedStateDisplayName?: string;
        /** The State Display Names for objects to specifically exclude */
        ExcludedStateDisplayNames?: Array<string>;
        /** The State Translation Key for objects */
        StateTranslationKey?: string;
    }

    export interface AmFilterableByAccountsBaseModel<TAccountRelateModel> {
        /** Accounts this object is associated with */
        Accounts?: Array<TAccountRelateModel>;
    }

    export interface AmFilterableByBrandsBaseModel<TBrandRelateModel> {
        /** Brands this object is associated with */
        Brands?: Array<TBrandRelateModel>;
    }

    export interface AmFilterableByCategoriesBaseModel<TCategoryRelateModel> {
        /** Categories this object is associated with */
        Categories?: Array<TCategoryRelateModel>;
    }

    export interface AmFilterableByFranchisesBaseModel<TFranchiseRelateModel> {
        /** Franchises this object is associated with */
        Franchises?: Array<TFranchiseRelateModel>;
    }

    export interface AmFilterableByManufacturersBaseModel<TManufacturerRelateModel> {
        /** Manufacturers this object is associated with */
        Manufacturers?: Array<TManufacturerRelateModel>;
    }

    export interface AmFilterableByProductsBaseModel<TProductRelateModel> {
        /** Products this object is associated with */
        Products?: Array<TProductRelateModel>;
    }

    export interface AmFilterableByStoreBaseModelT<TOther> {
        /** Gets or sets the stores. */
        Stores?: Array<TOther>;
    }

    export interface AmFilterableByStoresBaseModel<TStoreRelateModel> {
        /** Stores this object is associated with */
        Stores?: Array<TStoreRelateModel>;
    }

    export interface AmFilterableByUsersBaseModel<TUserRelateModel> {
        /** Users this object is associated with */
        Users?: Array<TUserRelateModel>;
    }

    export interface AmFilterableByVendorsBaseModel<TVendorRelateModel> {
        /** Vendors this object is associated with */
        Vendors?: Array<TVendorRelateModel>;
    }

    export interface AmFilterableByAccountBaseModel {
        /** Gets or sets the identifier of the Account. */
        AccountID?: number;
        /** Gets or sets the Account. */
        Account?: AccountModel;
        /** The Account key */
        StorAccountKey?: string;
        /** The Account name */
        AccountName?: string;
        /** The Account seo url */
        AccountSeoUrl?: string;
    }

    export interface AmFilterableByBrandBaseModel {
        /** Gets or sets the identifier of the Brand. */
        BrandID?: number;
        /** Gets or sets the Brand. */
        Brand?: BrandModel;
        /** The Brand key */
        BrandKey?: string;
        /** The Brand name */
        BrandName?: string;
    }

    export interface AmFilterableByCategoryBaseModel {
        /** Gets or sets the identifier of the Category. */
        CategoryID?: number;
        /** Gets or sets the Category. */
        Category?: CategoryModel;
        /** The Category key */
        CategoryKey?: string;
        /** The Category name */
        CategoryName?: string;
        /** The Category seo url */
        CategorySeoUrl?: string;
    }

    export interface AmFilterableByFranchiseBaseModel {
        /** Gets or sets the identifier of the Franchise. */
        FranchiseID?: number;
        /** Gets or sets the Franchise. */
        Franchise?: FranchiseModel;
        /** The Franchise key */
        FranchiseKey?: string;
        /** The Franchise name */
        FranchiseName?: string;
    }

    export interface AmFilterableByManufacturerBaseModel {
        /** Gets or sets the identifier of the Manufacturer. */
        ManufacturerID?: number;
        /** Gets or sets the Manufacturer. */
        Manufacturer?: ManufacturerModel;
        /** The Manufacturer key */
        ManufacturerKey?: string;
        /** The Manufacturer name */
        ManufacturerName?: string;
        /** The Manufacturer seo url */
        ManufacturerSeoUrl?: string;
    }

    export interface AmFilterableByProductBaseModel {
        /** Gets or sets the identifier of the Product. */
        ProductID?: number;
        /** Gets or sets the Product. */
        Product?: ProductModel;
        /** The Product key */
        ProductKey?: string;
        /** The Product name */
        ProductName?: string;
        /** The Product seo url */
        ProductSeoUrl?: string;
    }

    export interface AmFilterableByStoreBaseModel {
        /** Gets or sets the identifier of the Store. */
        StoreID?: number;
        /** Gets or sets the Store. */
        Store?: StoreModel;
        /** The Store key */
        StoreKey?: string;
        /** The Store name */
        StoreName?: string;
        /** The Store seo url */
        StoreSeoUrl?: string;
    }

    export interface AmFilterableByUserBaseModel {
        /** Gets or sets the identifier of the User. */
        UserID?: number;
        /** Gets or sets the User. */
        User?: UserModel;
        /** The User key */
        UserKey?: string;
        /** The User name */
        UserName?: string;
    }

    export interface AmFilterableByVendorBaseModel {
        /** Gets or sets the identifier of the Vendor. */
        VendorID?: number;
        /** Gets or sets the Vendor. */
        Vendor?: VendorModel;
        /** The Vendor key */
        VendorKey?: string;
        /** The Vendor name */
        VendorName?: string;
        /** The Vendor seo url */
        VendorSeoUrl?: string;
    }

    export interface AmFilterableByNullableAccountBaseModel {
        /** Gets or sets the identifier of the Account. */
        AccountID?: number;
        /** Gets or sets the Account. */
        Account?: AccountModel;
        /** The Account key */
        AccountKey?: string;
        /** The Account name */
        AccountName?: string;
        /** The Account seo url */
        AccountSeoUrl?: string;
    }

    export interface AmFilterableByNullableBrandBaseModel {
        /** Gets or sets the identifier of the Brand. */
        BrandID?: number;
        /** Gets or sets the Brand. */
        Brand?: BrandModel;
        /** The Brand key */
        BrandKey?: string;
        /** The Brand name */
        BrandName?: string;
    }

    export interface AmFilterableByNullableFranchiseBaseModel {
        /** Gets or sets the identifier of the Franchise. */
        FranchiseID?: number;
        /** Gets or sets the Franchise. */
        Franchise?: FranchiseModel;
        /** The Franchise key */
        FranchiseKey?: string;
        /** The Franchise name */
        FranchiseName?: string;
    }

    export interface AmFilterableByNullableCategoryBaseModel {
        /** Gets or sets the identifier of the Category. */
        CategoryID?: number;
        /** Gets or sets the Category. */
        Category?: CategoryModel;
        /** The Category key */
        CategoryKey?: string;
        /** The Category name */
        CategoryName?: string;
        /** The Category seo url */
        CategorySeoUrl?: string;
    }

    export interface AmFilterableByNullableManufacturerBaseModel {
        /** Gets or sets the identifier of the Manufacturer. */
        ManufacturerID?: number;
        /** Gets or sets the Manufacturer. */
        Manufacturer?: ManufacturerModel;
        /** The Manufacturer key */
        ManufacturerKey?: string;
        /** The Manufacturer name */
        ManufacturerName?: string;
        /** The Manufacturer seo url */
        ManufacturerSeoUrl?: string;
    }

    export interface AmFilterableByNullableProductBaseModel {
        /** Gets or sets the identifier of the Product. */
        ProductID?: number;
        /** Gets or sets the Product. */
        Product?: ProductModel;
        /** The Product key */
        ProductKey?: string;
        /** The Product name */
        ProductName?: string;
        /** The Product seo url */
        ProductSeoUrl?: string;
    }

    export interface AmFilterableByNullableStoreBaseModel {
        /** Gets or sets the identifier of the store. */
        StoreID?: number;
        /** Gets or sets the store. */
        Store?: StoreModel;
        /** The store key */
        StoreKey?: string;
        /** The store name */
        StoreName?: string;
        /** The store seo url */
        StoreSeoUrl?: string;
    }

    export interface AmFilterableByNullableUserBaseModel {
        /** Gets or sets the identifier of the User. */
        UserID?: number;
        /** Gets or sets the User. */
        User?: UserModel;
        /** The User key */
        UserKey?: string;
        /** The User name */
        UserName?: string;
        /** The User seo url */
        UserSeoUrl?: string;
    }

    export interface AmFilterableByNullableVendorBaseModel {
        /** Gets or sets the identifier of the Vendor. */
        VendorID?: number;
        /** Gets or sets the Vendor. */
        Vendor?: VendorModel;
        /** The Vendor key */
        VendorKey?: string;
        /** The Vendor name */
        VendorName?: string;
        /** The Vendor seo url */
        VendorSeoUrl?: string;
    }

    export interface AmFilterableByAccountSearchModel {
        /** Account ID For Search, Note: This will be overriden on data calls automatically */
        AccountID?: number;
        /** The Account Key for objects */
        AccountKey?: string;
        /** The Account Name for objects */
        AccountName?: string;
    }
    export interface AmFilterableByAccountBaseSearchModel extends AmFilterableByAccountSearchModel { }

    export interface AmFilterableByBrandSearchModel {
        /** Brand ID For Search, Note: This will be overriden on data calls automatically */
        BrandID?: number;
        /** The Brand Key for objects */
        BrandKey?: string;
        /** The Brand Name for objects */
        BrandName?: string;
    }
    export interface AmFilterableByBrandBaseSearchModel extends AmFilterableByBrandSearchModel { }

    export interface AmFilterableByCategorySearchModel {
        /** Category ID For Search, Note: This will be overriden on data calls automatically */
        CategoryID?: number;
        /** The Category Key for objects */
        CategoryKey?: string;
        /** The Category Name for objects */
        CategoryName?: string;
        /** The Category SEO URL for objects */
        CategorySeoUrl?: string;
    }
    export interface AmFilterableByCategoryBaseSearchModel extends AmFilterableByCategorySearchModel { }

    export interface AmFilterableByFranchiseSearchModel {
        /** Franchise ID For Search, Note: This will be overriden on data calls automatically */
        FranchiseID?: number;
        /** The Franchise Key for objects */
        FranchiseKey?: string;
        /** The Franchise Name for objects */
        FranchiseName?: string;
    }
    export interface AmFilterableByFranchiseBaseSearchModel extends AmFilterableByFranchiseSearchModel { }

    export interface HaveAContactBaseSearchModel {
        ContactID?: number;
        ContactKey?: string;
        ContactName?: string;
        ContactFirstName?: string;
        ContactLastName?: string;
        ContactPhone?: string;
        ContactFax?: string;
        ContactEmail?: string;
    }

    export interface AmFilterableByAccountSearchModel {
        AccountID?: number;
        /** The Account Key for objects */
        AccountKey?: string;
        /** The Account Name for objects */
        AccountName?: string;
    }

    export interface AmFilterableByBrandSearchModel {
        /** Brand ID For Search, Note: This will be overriden on data calls automatically */
        BrandID?: number;
        /** The Brand Key for objects */
        BrandKey?: string;
        /** The Brand Name for objects */
        BrandName?: string;
        BrandCategoryID?: number;
    }

    export interface AmFilterableByCategorySearchModel {
        CategoryID?: number;
        /** The Category Key for objects */
        CategoryKey?: string;
        /** The Category Name for objects */
        CategoryName?: string;
        CategorySeoUrl?: string;
    }

    export interface AmFilterableByFranchiseSearchModel {
        /** Franchise ID For Search, Note: This will be overriden on data calls automatically */
        FranchiseID?: number;
        /** The Franchise Key for objects */
        FranchiseKey?: string;
        /** The Franchise Name for objects */
        FranchiseName?: string;
        FranchiseCategoryID?: number;
    }

    export interface AmFilterableByManufacturerSearchModel {
        /** Manufacturer ID For Search */
        ManufacturerID?: number;
        /** The Manufacturer Key for objects */
        ManufacturerKey?: string;
        /** The Manufacturer Name for objects */
        ManufacturerName?: string;
    }
    export interface AmFilterableByManufacturerBaseSearchModel extends AmFilterableByManufacturerSearchModel { }

    export interface AmFilterableByProductSearchModel {
        /** Product ID For Search, Note: This will be overriden on data calls automatically */
        ProductID?: number;
        /** The Product Key for objects */
        ProductKey?: string;
        /** The Product Name for objects */
        ProductName?: string;
        /** The Product seo url */
        ProductSeoUrl?: string;
    }

    export interface AmFilterableByStoreSearchModel {
        /** Store ID For Search, Note: This will be overriden on data calls automatically */
        StoreID?: number;
        /** The Store Key for objects */
        StoreKey?: string;
        /** The Store Name for objects */
        StoreName?: string;
        /** The store seo url */
        StoreSeoUrl?: string;
        StoreCountryID?: number;
        StoreRegionID?: number;
        StoreCity?: string;
        StoreAnyCountryID?: number;
        StoreAnyRegionID?: number;
        StoreAnyCity?: string;
    }

    export interface AmFilterableByUserBaseSearchModel {
        UserID?: number;
        /** The User Key for objects */
        UserKey?: string;
        /** The User Name for objects */
        UserUsername?: string;
    }

    export interface AmFilterableByVendorSearchModel {
        /** Vendor ID For Search, Note: This will be overriden on data calls automatically */
        VendorID?: number;
        /** The Vendor Key for objects */
        VendorKey?: string;
        /** The Vendor Name for objects */
        VendorName?: string;
    }

    export interface AmFilterableByProductSearchModel {
        /** Product ID For Search, Note: This will be overriden on data calls automatically */
        ProductID?: number;
        /** The Product Key for objects */
        ProductKey?: string;
        /** The Product Name for objects */
        ProductName?: string;
        /** The Product seo url */
        ProductSeoUrl?: string;
    }
    export interface AmFilterableByProductBaseSearchModel extends AmFilterableByProductSearchModel { }

    export interface AmFilterableByStoreSearchModel {
        /** Store ID For Search, Note: This will be overriden on data calls automatically */
        StoreID?: number;
        /** The Store Key for objects */
        StoreKey?: string;
        /** The Store Name for objects */
        StoreName?: string;
        /** The store seo url */
        StoreSeoUrl?: string;
    }
    export interface AmFilterableByStoreBaseSearchModel extends AmFilterableByStoreSearchModel { }

    export interface AmFilterableByUserSearchModel {
        /** User ID For Search, Note: This will be overriden on data calls automatically */
        UserID?: number;
        /** The User Key for objects */
        UserKey?: string;
        /** The User Name for objects */
        UserName?: string;
        /** The User seo url */
        UserSeoUrl?: string;
    }
    export interface AmFilterableByUserBaseSearchModel extends AmFilterableByUserSearchModel { }

    export interface AmFilterableByVendorSearchModel {
        /** Vendor ID For Search, Note: This will be overriden on data calls automatically */
        VendorID?: number;
        /** The Vendor Key for objects */
        VendorKey?: string;
        /** The Vendor Name for objects */
        VendorName?: string;
    }
    export interface AmFilterableByVendorBaseSearchModel extends AmFilterableByVendorSearchModel { }

    export interface HaveNullableDimensionsBaseModel {
        Weight?: number;
        WeightUnitOfMeasure?: string;
        Width?: number;
        WidthUnitOfMeasure?: string;
        Depth?: number;
        DepthUnitOfMeasure?: string;
        Height?: number;
        HeightUnitOfMeasure?: string;
    }

    export interface HaveRequiresRolesBaseModel {
        RequiresRolesList?: Array<string>;
        RequiresRolesAlt?: string;
        RequiresRolesListAlt?: Array<string>;
    }

    export interface HaveOrderMinimumsBaseModel {
        MinimumOrderDollarAmount?: number;
        MinimumOrderDollarAmountAfter?: number;
        MinimumOrderDollarAmountWarningMessage?: string;
        MinimumOrderDollarAmountOverrideFee?: number;
        MinimumOrderDollarAmountOverrideFeeIsPercent: boolean;
        MinimumOrderDollarAmountOverrideFeeWarningMessage?: string;
        MinimumOrderDollarAmountOverrideFeeAcceptedMessage?: string;
        MinimumOrderQuantityAmount?: number;
        MinimumOrderQuantityAmountAfter?: number;
        MinimumOrderQuantityAmountWarningMessage?: string;
        MinimumOrderQuantityAmountOverrideFee?: number;
        MinimumOrderQuantityAmountOverrideFeeIsPercent: boolean;
        MinimumOrderQuantityAmountOverrideFeeWarningMessage?: string;
        MinimumOrderQuantityAmountOverrideFeeAcceptedMessage?: string;
        MinimumOrderDollarAmountBufferProductID?: number;
        MinimumOrderDollarAmountBufferProductKey?: string;
        MinimumOrderDollarAmountBufferProductName?: string;
        MinimumOrderDollarAmountBufferProduct?: ProductModel;
        MinimumOrderQuantityAmountBufferProductID?: number;
        MinimumOrderQuantityAmountBufferProductKey?: string;
        MinimumOrderQuantityAmountBufferProductName?: string;
        MinimumOrderQuantityAmountBufferProduct?: ProductModel;
        MinimumOrderDollarAmountBufferCategoryID?: number;
        MinimumOrderDollarAmountBufferCategoryKey?: string;
        MinimumOrderDollarAmountBufferCategoryName?: string;
        MinimumOrderDollarAmountBufferCategory?: CategoryModel;
        MinimumOrderQuantityAmountBufferCategoryID?: number;
        MinimumOrderQuantityAmountBufferCategoryKey?: string;
        MinimumOrderQuantityAmountBufferCategoryName?: string;
        MinimumOrderQuantityAmountBufferCategory?: CategoryModel;
    }

    export interface HaveFreeShippingMinimumsBaseModel {
        MinimumForFreeShippingDollarAmount?: number;
        MinimumForFreeShippingDollarAmountAfter?: number;
        MinimumForFreeShippingDollarAmountWarningMessage?: string;
        MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage?: string;
        MinimumForFreeShippingQuantityAmount?: number;
        MinimumForFreeShippingQuantityAmountAfter?: number;
        MinimumForFreeShippingQuantityAmountWarningMessage?: string;
        MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage?: string;
        MinimumForFreeShippingDollarAmountBufferProductID?: number;
        MinimumForFreeShippingDollarAmountBufferProductKey?: string;
        MinimumForFreeShippingDollarAmountBufferProductName?: string;
        MinimumForFreeShippingDollarAmountBufferProduct?: ProductModel;
        MinimumForFreeShippingQuantityAmountBufferProductID?: number;
        MinimumForFreeShippingQuantityAmountBufferProductKey?: string;
        MinimumForFreeShippingQuantityAmountBufferProductName?: string;
        MinimumForFreeShippingQuantityAmountBufferProduct?: ProductModel;
        MinimumForFreeShippingDollarAmountBufferCategoryID?: number;
        MinimumForFreeShippingDollarAmountBufferCategoryKey?: string;
        MinimumForFreeShippingDollarAmountBufferCategoryName?: string;
        MinimumForFreeShippingDollarAmountBufferCategory?: CategoryModel;
        MinimumForFreeShippingQuantityAmountBufferCategoryID?: number;
        MinimumForFreeShippingQuantityAmountBufferCategoryKey?: string;
        MinimumForFreeShippingQuantityAmountBufferCategoryName?: string;
        MinimumForFreeShippingQuantityAmountBufferCategory?: CategoryModel;
    }

    /**
     * Interface for sales collection base model.
     * @sa HaveAStatusModel.
     * @sa HaveAStateModel.
     * @sa AmFilterableByNullableBrandBaseModel.
     * @sa AmFilterableByNullableFranchiseBaseModel.
     * @sa AmFilterableByNullableStoreBaseModel.
     * @sa HaveJsonAttributesBaseModel.
     */
    export interface SalesCollectionBaseModel
        extends
            HaveAStatusModel,
            HaveAStateModel,
            AmFilterableByNullableBrandBaseModel,
            AmFilterableByNullableFranchiseBaseModel,
            AmFilterableByNullableStoreBaseModel,
            HaveJsonAttributesBaseModel {
        /** The item quantity? */
        ItemQuantity?: number;

        /** The account id */
        AccountID?: number;
        /** The account */
        Account?: AccountModel;
        /** The account key */
        AccountKey?: string;
        /** The account name */
        AccountName?: string;

        /** The user id */
        UserID?: number;
        /** The user */
        User?: UserModel;
        /** The user key */
        UserKey?: string;
        /** The user name */
        UserName?: string;
        /** The user name */
        UserUserName?: string;
        /** The user contact first name */
        UserContactFirstName?: string;
        /** The user contact last name */
        UserContactLastName?: string;
        /** The user contact email */
        UserContactEmail?: string;

        /** The balance due */
        BalanceDue?: number;
        /** The shipping same as billing. */
        ShippingSameAsBilling: boolean;
        /** The billing contact */
        BillingContactID?: number;
        /** The billing contact */
        BillingContact?: ContactModel;
        /** The shipping contact */
        ShippingContactID?: number;
        /** The shipping contact */
        ShippingContact?: ContactModel;

        SessionID?: Guid;

        // Totals?: CartTotals; // Generated
    }

    export interface SalesCollectionBaseModelT<
            TTypeModel,
            TContactModel,
            TSalesEventModel extends SalesEventBaseModel,
            TDiscountModel extends AppliedDiscountBaseModel,
            TItemDiscountModel extends AppliedDiscountBaseModel,
            TFileModel extends AmAStoredFileRelationshipTableModel>
        extends
            SalesCollectionBaseModel,
            HaveStoredFilesBaseModel<TFileModel>,
            HaveATypeModel<TTypeModel>,
            HaveAStatusModel,
            HaveAStateModel {
        Contacts?: Array<TContactModel>;
        /** The sales items. */
        SalesItems?: Array<SalesItemBaseModel<TItemDiscountModel>>;
        /** The discounts */
        Discounts?: Array<TDiscountModel>;
        /** The sales events */
        SalesEvents?: Array<TSalesEventModel>;
    }

    export interface SalesCollectionBaseSearchModel
        extends BaseSearchModel
        , HaveATypeSearchModel
        , HaveAStatusBaseSearchModel
        , AmFilterableByStoreSearchModel { }

    /**
     * @see {@link NameableBaseModel}
     * @see {@link HaveJsonAttributesBaseModel}
     * @see {@link HaveNotesBaseModel}
     * @public
     */
    export interface SalesItemBaseModel<TItemDiscountModel extends AppliedDiscountBaseModel>
        extends
            HasInventoryObject,
            NameableBaseModel,
            HaveJsonAttributesBaseModel,
            HaveNotesBaseModel
    {
        Sku?: string;
        ForceUniqueLineItemKey?: string;
        Quantity: number;
        QuantityBackOrdered?: number;
        QuantityPreSold?: number;
        UnitCorePrice: number;
        UnitSoldPrice?: number;
        MasterID?: number;
        Discounts?: Array<TItemDiscountModel>;

        UnitSoldPriceModifier?: number;
        UnitSoldPriceModifierMode?: number;
        ExtendedPrice: number;
        ExtendedShippingAmount?: number;
        ExtendedTaxAmount?: number;
        UnitCorePriceInSellingCurrency?: number;
        UnitSoldPriceInSellingCurrency?: number;
        ExtendedPriceInSellingCurrency?: number;
        ItemType: ItemType;
        QuantityReceived?: number;
        NewQuantityReceived?: number;
        RestockingFeeAmount?: number;
        SalesReturnReasonID?: number;
        SalesReturnReasonKey?: string;
        SalesReturnReasonName?: string;
        SalesReturnReason?: SalesReturnReasonModel;
        UserKey?: string;
        UserUserName?: string;
        MasterKey?: string;
        OriginalCurrencyID?: number;
        OriginalCurrencyKey?: string;
        OriginalCurrencyName?: string;
        OriginalCurrency?: CurrencyModel;
        SellingCurrencyID?: number;
        SellingCurrencyKey?: string;
        SellingCurrencyName?: string;
        SellingCurrency?: CurrencyModel;
        Targets?: Array<SalesItemTargetBaseModel>;
        DateReceived?: Date;
        MaxAllowedInCart?: number;
        ProductDownloads?: Array<string>;
        ProductDownloadsNew?: Array<string>;

        ProductID?: number;
        ProductKey?: string;
        ProductName?: string;
        ProductPrimaryImage?: string;
        ProductDescription?: string;
        ProductSeoUrl?: string;
        ProductRequiresRoles?: string;
        ProductIsUnlimitedStock?: boolean;
        ProductAllowBackOrder?: boolean;
        ProductAllowPreSale?: boolean;
        ProductIsEligibleForReturn?: boolean;
        ProductRestockingFeePercent?: number;
        ProductRestockingFeeAmount?: number;
        ProductNothingToShip?: boolean;
        ProductIsTaxable?: boolean;
        ProductTaxCode?: string;
        ProductShortDescription?: string;
        ProductUnitOfMeasure?: string;
        ProductSerializableAttributes?: SerializableAttributesDictionary;
        ProductTypeID?: number;
        ProductTypeKey?: string;
        ProductIsDiscontinued?: boolean;
        ProductMinimumPurchaseQuantity?: number;
        ProductMinimumPurchaseQuantityIfPastPurchased?: number;
        ProductMaximumPurchaseQuantity?: number;
        ProductMaximumPurchaseQuantityIfPastPurchased?: number;
        ProductStockQuantity?: number;
        ProductStockQuantityAllocated?: number;
        ProductStockQuantityBroken?: number;
        ProductStockQuantityPreSold?: number;
    }

    export interface SalesItemTargetBaseModel {
        NothingToShip: boolean;
    }

    export interface AppliedDiscountBaseModel {
        MasterID: number;
        MasterKey?: string;
        SlaveID: number;
        SlaveKey?: string;
        SlaveName?: string;
        DiscountTotal: number;
        ApplicationsUsed?: number;
        DiscountTypeID: number;
        DiscountValue: number;
        DiscountPriority?: number;
        DiscountValueType?: number;
    }

    export interface ProductSearchViewModel {
        HitsMetaDataHitScores?: cefalt.admin.Dictionary<number>;
    }

    export interface PagedResultsBase<TResult> {
        TotalPages: number;
        TotalCount: number;
        CurrentPage: number;
        CurrentCount: number;
        Results: Array<TResult>;
    }

    export interface RoleForUserModel {
        RoleId: number;
        UserId: number;
        Name?: string;
        StartDate?: Date;
        EndDate?: Date;
    }

    export interface RoleForAccountModel {
        RoleId: number;
        AccountId: number;
        Name?: string;
        StartDate?: Date;
        EndDate?: Date;
    }

    export interface IAuthTokens {
        AccessToken?: string;
        AccessTokenSecret?: string;
        Items?: cefalt.admin.Dictionary<string>;
        Provider: string;
        RefreshToken?: string;
        RefreshTokenExpiry?: Date;
        RequestToken?: string;
        RequestTokenSecret?: string;
        UserId?: string;
    }

    /*
    export interface DiscountModel extends NameableBaseModel {
        DisplayValue: string;
        DisplayDates: string;
        DisplayCodes: string;
    }
    */

    export interface AuthUserSession { }
    export interface AuthenticateResponse { }
    export interface BaseSearchModel {
        noCache?: number;
    }

    export interface HaveANullableContactBaseModel {
        ContactID?: number;
        Contact?: ContactModel;
        ContactFirstName?: string;
        ContactLastName?: string;
        ContactEmail?: string;
        ContactPhone?: string;
        ContactFax?: string;
    }
    export interface HaveAContactBaseModel {
        ContactID: number;
        Contact?: ContactModel;
        ContactFirstName?: string;
        ContactLastName?: string;
        ContactEmail?: string;
        ContactPhone?: string;
        ContactFax?: string;
    }

    export interface IUploadResponse {
        ID: string;
        Upload: IUpload;
        UploadStatus: UploadStatus;
        UploadFiles: Array<IUploadResult>;
    }

    export interface IUpload {
        UploadID: string;
        Expires: Date;
        EntityFileType: FileEntityType;
        Name: string;
        Async: boolean;
    }

    export interface IUploadResult {
        FileName: string;
        FilePath: string;
        FileKey: string;
        ContentType: string;
        ContentLength: number;
        PercentDone: number;
        TotalBytes: number;
        TransferredBytes: number;
        UploadFileStatus: UploadStatus;
    }

    export interface InventoryObject {
        storeQty: number;
        storeDCIQty: number;
        anyDCIQty: number;
        masterDCQty: number;
        totalQty: number;
        restrictShipFlag?: boolean;
        breakablePack?: string;
    }
    export interface CalculatedInventories extends CalculatedInventory {
        // == Inherited ==
        // ProductID: number;
        // IsDiscontinued: boolean;
        // IsUnlimitedStock: boolean;
        // IsOutOfStock: boolean;
        // QuantityPresent?: number;
        // QuantityAllocated?: number;
        // QuantityOnHand?: number;
        // MaximumPurchaseQuantity?: number;
        // MaximumPurchaseQuantityIfPastPurchased?: number;
        // AllowBackOrder: boolean;
        // MaximumBackOrderPurchaseQuantity?: number;
        // MaximumBackOrderPurchaseQuantityIfPastPurchased?: number;
        // MaximumBackOrderPurchaseQuantityGlobal?: number;
        // AllowPreSale: boolean;
        // PreSellEndDate?: Date;
        // QuantityPreSellable?: number;
        // QuantityPreSold?: number;
        // MaximumPrePurchaseQuantity?: number;
        // MaximumPrePurchaseQuantityIfPastPurchased?: number;
        // MaximumPrePurchaseQuantityGlobal?: number;
        // RelevantLocations?: Array<ProductInventoryLocationSectionModel>;
        loading: boolean;
    }
    export interface HasInventoryObject extends HaveJsonAttributesBaseModel {
        /** @deprecated */
        inventoryObject?: InventoryObject;
        /** @deprecated */
        ProductAssociations?: Array<ProductAssociationModel>;

        CurrentShipOption?: string;
        readInventory?: () => CalculatedInventories;
    }
    export interface ProductCatalogItem extends HasInventoryObject { }
    export interface ProductModel extends HasInventoryObject { }
    export interface StoreModel {
        Inventory?: number;
    }

    export interface IPriceRuleModel extends PriceRuleModel {
    }

    export interface AccountContactModel {
        SlavePhone?: string;
        SlaveFax?: string;
        SlaveEmail?: string;
        SlaveFirstName?: string;
        SlaveLastName?: string;
    }

    export interface GetCategoryByIDDto extends ImplementsIDOnQueryBase {
    }

    export interface AuthProviderLoginDto {
        Username: string;
        Password: string;
        RememberMe?: boolean;
    }

    export class Authentication2 extends ServiceStackRoute {
        /**
         * Path: /auth/{provider}
         * Verbs: POST
         */
        ProviderLogin = (provider: string, routeParams?: AuthProviderLoginDto) => {
            return this.$http<AuthenticateResponse>({
                url: [this.rootUrl, "auth", provider].join("/"),
                method: "POST",
                data: routeParams
            });
        }

        /**
         * Path: /auth/logout
         * Verbs: GET
         */
        ProviderLogout = () => {
            return this.$http<AuthenticateResponse>({
                url: [this.rootUrl, "auth", "logout"].join("/"),
                method: "GET",
            });
        }
    }

    export interface IClarityEcomService {
        authentication2: Authentication2;
    }

    export interface IAddCartItemParams {
        SerializableAttributes?: api.SerializableAttributesDictionary;
        selectedShipOption?: string;
        ProductInventoryLocationSectionID?: number;
        currentInventoryLimit?: number;
        ForceUniqueLineItemKey?: string;
        userID?: number;
        accountID?: number;
        storeID?: number;
        franchiseID?: number;
        brandID?: number;
        forceNoModal?: boolean;
    }

    /**
     * A cart lookup key base.
     * @export
     * @abstract
     * @class CartLookupKeyBase
     */
    export abstract class CartLookupKeyBase {
        /**
         * Gets or sets the identifier of the account.
         * @type {number}
         * @optional
         * @default null
         * @memberof CartLookupKeyBase
         */
        AID?: number;
        /**
         * Gets or sets the identifier of the user.
         * @type {number}
         * @optional
         * @default null
         * @memberof CartLookupKeyBase
         */
        UID?: number;
         /**
         * Gets or sets the identifier of the brand.
         * @type {number}
         * @optional
         * @default null
         * @memberof CartLookupKeyBase
         */
        BID?: number;
        /**
         * Gets or sets the identifier of the franchise.
         * @type {number}
         * @optional
         * @default null
         * @memberof CartLookupKeyBase
         */
        FID: number;
        /**
         * Gets or sets the identifier of the store.
         * @type {number}
         * @optional
         * @default null
         * @memberof CartLookupKeyBase
         */
        SID?: number;

        protected baseIsEquivalentTo(otherLookupKey: CartLookupKeyBase): boolean {
            if (!otherLookupKey) {
                return false;
            }
            return otherLookupKey.AID === this.AID
                && otherLookupKey.UID === this.UID
                && otherLookupKey.BID === this.BID
                && otherLookupKey.FID === this.FID
                && otherLookupKey.SID === this.SID;
        }
        abstract isEquivalentTo(cvCartService: services.ICartService, otherLookupKey: CartLookupKeyBase): boolean;
        abstract isValid(): boolean;
        abstract updateFromJson(source: string);
        abstract toString(): string;
    }

    /**
     * A cart by identifier lookup key.
     * @export
     * @class CartByIDLookupKey
     * @extends {CartLookupKeyBase}
     */
    export class CartByIDLookupKey extends CartLookupKeyBase {
        /**
         * Gets or sets the identifier of the cart.
         * @type {number}
         * @required
         * @memberof CartByIDLookupKey
         */
        ID: number;

        /**
         * Initializes a new instance of the {@see CartByIDLookupKey} class.
         * @param {number} [cartID] Identifier for the cart.
         * @param {number} [userID=null] -dentifier for the user.
         * @param {number} [accountID=null] Identifier for the account.
         * @param {number} [brandID=null] Identifier for the brand.
         * @param {number} [franchiseID=null] Identifier for the franchise.
         * @param {number} [storeID=null] Identifier for the store.
         * @memberof CartByIDLookupKey
         */
        constructor(
            cartID: number,
            userID: number = null,
            accountID: number = null,
            brandID: number = null,
            franchiseID: number = null,
            storeID: number = null) {
            super();
            this.ID = cartID;
            this.UID = userID;
            this.AID = accountID;
            this.BID = brandID;
            this.FID = franchiseID;
            this.SID = storeID;
        }

        isEquivalentTo(cvCartService: services.ICartService, otherLookupKey: api.CartLookupKeyBase): boolean {
            if (!super.baseIsEquivalentTo(otherLookupKey)) {
                return false;
            }
            if (typeof(otherLookupKey) === typeof(CartByIDLookupKey)) {
                const asByID = otherLookupKey as any as CartByIDLookupKey;
                return asByID.ID === this.ID;
            }
            /* TODO@JTG
            if (typeof(otherLookupKey) === typeof(SessionCartBySessionAndTypeLookupKey)) {
                const asBySe = otherLookupKey as any as SessionCartBySessionAndTypeLookupKey;
                return cvCartService.accessCart(asBySe).ID === this.ID;
            }
            */
            return false;
        }

        isValid(): boolean {
            return this.ID > 0;
        }

        /**
         * Initializes a new instance of the {@see CartByIDLookupKey} class.
         * @param {string} source Source for the data.
         * @memberof CartByIDLookupKey
         */
        updateFromJson(source: string): void {
            const parsed = CartByIDLookupKey.newFromJson(source);
            this.ID = parsed.ID;
            this.UID = parsed.UID;
            this.AID = parsed.AID;
            this.BID = parsed.BID;
            this.FID = parsed.FID;
            this.SID = parsed.SID;
        }

        /**
         * Converts this {@see CartByIDLookupKey} to a {@see SessionCartBySessionAndTypeLookupKey}.
         * @param {string} typeKey The type key
         * @param {api.Guid} sessionID Identifier for the session.
         * @returns The given data converted to a {@see SessionCartBySessionAndTypeLookupKey}.
         */
        toSessionLookupKey(typeKey: string, sessionID: api.Guid): SessionCartBySessionAndTypeLookupKey {
            return new SessionCartBySessionAndTypeLookupKey(
                /*typeKey:*/ typeKey,
                /*sessionID:*/ sessionID,
                /*userID:*/ this.UID,
                /*accountID:*/ this.AID,
                /*brandID:*/ this.BID,
                /*franchiseID:*/ this.FID,
                /*storeID:*/ this.SID);
        }

        /**
         * Initializes a {@see CartByIDLookupKey} from the given string.
         * @param {string} source The json string to parse.
         * @returns A {@see CartByIDLookupKey}.
         */
        static newFromJson(source: string): CartByIDLookupKey {
            const parsed = JSON.parse(source) as CartByIDLookupKey;
            return new CartByIDLookupKey(
                /*cartID:*/ parsed.ID,
                /*userID:*/ parsed.UID,
                /*accountID:*/ parsed.AID,
                /*brandID:*/ parsed.BID,
                /*franchiseID:*/ parsed.FID,
                /*storeID:*/ parsed.SID);
        }

        /**
         * Initializes a {@see CartByIDLookupKey} from the given cart model.
         * @param {api.CartModel} cart The cart.
         * @returns A {@see CartByIDLookupKey}.
         */
        static newFromCart(cart: api.CartModel): CartByIDLookupKey {
            return new CartByIDLookupKey(
                /*cartID:*/ cart.ID,
                /*userID:*/ cart.UserID,
                /*accountID:*/ cart.AccountID,
                /*brandID:*/ cart.BrandID,
                /*franchiseID:*/ cart.FranchiseID,
                /*storeID:*/ cart.StoreID);
        }

        toString(): string {
            return JSON.stringify(this);
        }
    }

    /**
     * A cart by session lookup key.
     * @export
     * @class SessionCartBySessionAndTypeLookupKey
     * @extends {CartLookupKeyBase}
     */
    export class SessionCartBySessionAndTypeLookupKey extends CartLookupKeyBase {
        /**
         * Gets or sets the type key.
         * @type {string}
         * @required
         * @memberof SessionCartBySessionAndTypeLookupKey
         */
        TK: string;

        /**
         * Gets or sets the identifier of the session.
         * @type {api.Guid}
         * @required
         * @memberof SessionCartBySessionAndTypeLookupKey
         */
        SeID: api.Guid;

        /**
         * Initializes a new instance of the {@see SessionCartBySessionAndTypeLookupKey} class.
         * @param {string} typeKey The type key.
         * @param {api.Guid} sessionID Identifier for the session.
         * @param {number} [userID=null] Identifier for the user.
         * @param {number} [accountID=null] Identifier for the account.
         * @param {number} [brandID=null] Identifier for the brand.
         * @param {number} [franchiseID=null] Identifier for the franchise.
         * @param {number} [storeID=null] Identifier for the store.
         * @memberof SessionCartBySessionAndTypeLookupKey
         */
        constructor(
            typeKey: string,
            sessionID: api.Guid,
            userID: number = null,
            accountID: number = null,
            brandID: number = null,
            franchiseID: number = null,
            storeID: number = null) {
            super();
            this.TK = typeKey;
            this.SeID = sessionID;
            this.UID = userID;
            this.AID = accountID;
            this.BID = brandID;
            this.FID = franchiseID;
            this.SID = storeID;
        }

        isEquivalentTo(cvCartService: services.ICartService, otherLookupKey: CartLookupKeyBase): boolean {
            if (!super.baseIsEquivalentTo(otherLookupKey)) {
                return false;
            }
            if (typeof(otherLookupKey) === typeof(CartByIDLookupKey)) {
                const asByID = otherLookupKey as any as CartByIDLookupKey;
                return cvCartService.accessCart(asByID).TypeKey === this.TK
                    && cvCartService.accessCart(asByID).SessionID === this.SeID;
            }
            if (typeof(otherLookupKey) === typeof(SessionCartBySessionAndTypeLookupKey)) {
                const asBySe = otherLookupKey as any as SessionCartBySessionAndTypeLookupKey;
                return asBySe.TK === this.TK
                    && asBySe.SeID === this.SeID;
            }
            return false;
        }

        isValid(): boolean {
            return angular.isDefined(this.TK)
                && this.TK !== null
                && this.TK.length > 0
                && angular.isDefined(this.SeID)
                && this.SeID !== null;
        }

        /**
         * Updates this record with data that was serialized into a json value.
         * @param {string} source Source for the data.
         * @memberof SessionCartBySessionAndTypeLookupKey
         */
        updateFromJson(source: string): void {
            const parsed = SessionCartBySessionAndTypeLookupKey.newFromJson(source);
            this.SeID = parsed.SeID;
            this.TK = parsed.TK;
            this.UID = parsed.UID;
            this.AID = parsed.AID;
            this.BID = parsed.BID;
            this.FID = parsed.FID;
            this.SID = parsed.SID;
        }

        /**
         * Converts this {@see SessionCartBySessionAndTypeLookupKey} to a {@see CartByIDLookupKey}.
         * @param {number} cartID Identifier for the cart.
         * @returns {CartByIDLookupKey}
         * @memberof SessionCartBySessionAndTypeLookupKey
         */
        toIDLookupKey(cartID: number): CartByIDLookupKey {
            if (!cartID) {
                throw new Error("cartID is required");
            }
            return new CartByIDLookupKey(
                /*cartID:*/ cartID,
                /*userID:*/ this.UID,
                /*accountID:*/ this.AID,
                /*brandID:*/ this.BID,
                /*franchiseID:*/ this.FID,
                /*storeID:*/ this.SID);
        }

        /// <summary>Creates a copy of this lookup key, but ignores the session identifier.</summary>
        /// <returns>A <see cref="SessionCartBySessionAndTypeLookupKey"/>.</returns>
        cloneThisKeyButIgnoreSessionID(): SessionCartBySessionAndTypeLookupKey {
            return new SessionCartBySessionAndTypeLookupKey(
                /*typeKey:*/ this.TK,
                /*sessionID:*/ null,
                /*userID:*/ this.UID,
                /*accountID:*/ this.AID,
                /*brandID:*/ this.BID,
                /*franchiseID:*/ this.FID,
                /*storeID:*/ this.SID);
        }

        /// <summary>Creates a copy of this lookup key, but ignores the user and account identifiers.</summary>
        /// <returns>A <see cref="SessionCartBySessionAndTypeLookupKey"/>.</returns>
        cloneThisKeyButIgnoreUserAndAccountID(): SessionCartBySessionAndTypeLookupKey {
            return new SessionCartBySessionAndTypeLookupKey(
                /*typeKey:*/ this.TK,
                /*sessionID:*/ this.SeID,
                /*userID:*/ null,
                /*accountID:*/ null,
                /*brandID:*/ this.BID,
                /*franchiseID:*/ this.FID,
                /*storeID:*/ this.SID);
        }

        /**
         * Initializes a {@see SessionCartBySessionAndTypeLookupKey} from the given string.
         * @param {string} source Source for the data.
         * @returns {SessionCartBySessionAndTypeLookupKey}
         */
        static newFromJson(source: string): SessionCartBySessionAndTypeLookupKey {
            const parsed = JSON.parse(source) as SessionCartBySessionAndTypeLookupKey;
            return new SessionCartBySessionAndTypeLookupKey(
                /*typeKey:*/ parsed.TK,
                /*sessionID:*/ parsed.SeID,
                /*userID:*/ parsed.UID,
                /*accountID:*/ parsed.AID,
                /*brandID:*/ parsed.BID,
                /*franchiseID:*/ parsed.FID,
                /*storeID:*/ parsed.SID);
        }

        /**
         * Initializes a new {@see cref="SessionCartBySessionAndTypeLookupKey} from the given from cart model.
         * @static
         * @param {api.CartModel} cart The cart.
         * @returns {SessionCartBySessionAndTypeLookupKey}
         * @memberof SessionCartBySessionAndTypeLookupKey
         */
        static newFromCart(cart: api.CartModel): SessionCartBySessionAndTypeLookupKey {
            if (!cart.TypeName && !(cart.Type && cart.Type.Name)) {
                throw new Error("Cannot determine cart type from the provided model");
            }
            if (!cart.SessionID) {
                throw new Error("Cannot determine cart session ID from the provided model");
            }
            return new SessionCartBySessionAndTypeLookupKey(
                /*typeKey:*/ cart.TypeName || cart.Type.Name,
                /*sessionID:*/ cart.SessionID,
                /*userID:*/ cart.UserID,
                /*accountID:*/ cart.AccountID,
                /*brandID:*/ cart.BrandID,
                /*franchiseID:*/ cart.FranchiseID,
                /*storeID:*/ cart.StoreID);
        }

        toString(): string {
            return JSON.stringify(this);
        }
    }
}

module cefalt.admin {
    export class Dictionary<TValue> {
        [name: string]: TValue
    }
}
