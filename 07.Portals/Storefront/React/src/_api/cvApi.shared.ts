/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable no-unused-vars */
/**
 * @file src/_api/clarityEcomService_shared.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc Clarity eCommerce API/service shared class
 */

import {
  AccountModel,
  BrandModel,
  CalculatedInventory,
  CategoryModel,
  ContactModel,
  CurrencyModel,
  FileEntityType,
  FranchiseModel,
  ItemType,
  ManufacturerModel,
  NameableBaseModel,
  NoteModel,
  PriceRuleModel,
  SalesEventBaseModel,
  SalesReturnReasonModel,
  SearchFormBase,
  SerializableAttributesDictionary,
  StatusModel,
  StoredFileModel,
  StoreModel,
  TypeModel,
  UploadStatus,
  UserModel,
  VendorModel,
  ProductModel as DtoClassProductModel,
  CEFActionResponse as DtoClassCEFActionResponse,
  AggregateTree as DtoAggregateTree,
  AuctionSearchViewModel as DtoClassAuctionSearchViewModel,
  LotSearchViewModel as DtoClassLotSearchViewModel,
  ProductSearchViewModel as DtoClassProductSearchViewModel,
  AggregatePricingRange,
  AggregateRatingRange,
  IndexableModelBase,
  SalesItemTargetBaseModel
} from "./cvApi._DtoClasses";

/**
 * @name CEFActionResponse
 */
export interface CEFActionResponse extends DtoClassCEFActionResponse {
  ActionSucceeded: boolean;
  Messages?: Array<string>;
}

/**
 * @name CEFActionResponseT<TResult>
 * @property {TResult} Result
 * @inheritdoc {CEFActionResponse}
 */
export interface CEFActionResponseT<TResult> extends CEFActionResponse {
  Result: TResult;
}

export interface IHttpHeadersGetter {
  (): { [name: string]: string };
  (headerName: string): string;
}

export interface IHttpPromiseCallback<T> {
  (
    data: T,
    status: number,
    headers: IHttpHeadersGetter,
    // @ts-ignore
    config?
  ): void;
}

export interface IHttpPromiseCallbackArgShort {
  status?: number;
  headers?: IHttpHeadersGetter;
  // @ts-ignore
  config?;
  statusText?: string;
}

export interface IHttpPromiseCallbackArg<T> extends IHttpPromiseCallbackArgShort {
  data?: T;
}

export interface IHttpPromise<T> extends Promise<IHttpPromiseCallbackArg<T>> {}

/**
 * @public
 */
export interface ImplementsIDOnQueryBase {
  ID: number;
}

/**
 * @public
 */
export interface ImplementsKeyOnQueryBase {
  Key?: string;
}

export interface BaseModel {
  __caller?: string;
  [propName: string]: any; // wasn't in original
}

export class KeyValuePair<TKey, TValue> {
  [key: /*TKey*/ string]: TValue;
}

export class Dictionary<TValue> {
  // eslint-disable-next-line no-restricted-globals
  [name: string]: TValue;
}

export class Guid {
  static newGuid() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, (c) => {
      const r = (Math.random() * 16) | 0,
        v = c === "x" ? r : (r & 0x3) | 0x8;
      return v.toString(16);
    });
  }
}

export interface TransactionResponse {
  success: boolean;
  responseCode: number;
  statusMessage: string;
  transactions: Transaction[];
}

export interface Transaction {
  transactionID: string;
  creditCard: CreditCard;
  transactionType: string;
  description: string;
  amount: number;
  invoiceID: string;
  shippingAddress: Address;
  billingAddress: Address;
  ReceiptEmailedTo: string;
  taxAmount: string;
  customerReferenceID: string;
  approval_code: string;
  approvalMessage: string;
  avsResponse: string;
  cscResponse: string;
  statusCode: string;
  statusMessage: string;
  created: Created;
  settled: string;
  customerID: string;
  DiscretionaryData: object;
}

export interface CreditCard {
  maskedNumber: string;
  expirationMonth: string;
  expirationYear: string;
}

export interface Address {
  name: string;
  streetAddress: string;
  streetAddress2: string;
  city: string;
  state: string;
  zip: string;
  country: string;
}

export interface Created {
  through: string;
  at: string;
  by: string;
  fromIP: string;
}

export interface AuctionSearchViewModel extends DtoClassAuctionSearchViewModel {
  Categories?: Array<CategoryModel>;
}
export interface LotSearchViewModel extends DtoClassLotSearchViewModel {
  Categories?: Array<CategoryModel>;
}
export interface ProductSearchViewModel extends DtoClassProductSearchViewModel {
  Categories?: Array<CategoryModel>;
}

export interface SearchViewModelBase<TSearchForm extends SearchFormBase,TIndexModel extends IndexableModelBase> {
  /** Gets or sets the form. */
  Form: TSearchForm;

  /** Gets or sets the documents. */
  Documents: Array<TIndexModel>;

  /** Gets or sets the elapsed milliseconds. */
  ElapsedMilliseconds: number;

  /** Gets or sets the number of.  */
  Total: number;

  /** Gets or sets the total number of pages. */
  TotalPages: number;

  /** Gets or sets the server error. */
  ServerError?: any;

  /** Gets or sets information describing the debug. */
  DebugInformation?: string;

  /** Gets or sets a value indicating whether this SearchViewModel is valid. */
  IsValid: boolean;

  /** Gets or sets the hits meta data maximum score. */
  HitsMetaDataMaxScore?: number;

  /** Gets or sets the hits meta data total. */
  HitsMetaDataTotal?: number;

  /** Gets or sets the hits meta data hit scores. */
  HitsMetaDataHitScores?: Dictionary<number>;

  /** Gets or sets the attributes. */
  Attributes: Array<KeyValuePair<string, Array<KeyValuePair<string, number | null>>>>;

  /** Gets or sets the brand IDs. */
  BrandIDs: Dictionary<number | null>;
  
  /** Gets or sets the brand names. */
  BrandNames: Dictionary<number | null>;

  /** Gets or sets the categories tree. */
  CategoriesTree: AggregateTree;

  /** Gets or sets the franchise IDs. */
  FranchiseIDs: Array<number>;

  /** Gets or sets the product IDs. */
  ProductIDs: Array<number>;

  /** Gets or sets the manufacturer IDs. */
  ManufacturerIDs: Array<number>;

  /** Gets or sets the store IDs. */
  StoreIDs: Array<number>;

  /** Gets or sets the type values. */
  Types: Array<IndexableTypeModel>;

  /** Gets or sets the vendor IDs. */
  VendorIDs: Array<number>;

  /** Gets or sets the pricing ranges. */
  PricingRanges: Array<AggregatePricingRange>;

  /** Gets or sets the rating ranges. */
  RatingRanges: Array<AggregateRatingRange>;

  /** Gets or sets the result IDs. */
  ResultIDs: Array<number>;
}

/**
 * @see {@link IndexableFilterBase}
 * @public
 */
export interface IndexableTypeModel extends IndexableFilterBase {
  SortOrder: number;
  Count: number;
}

/**
 * @public
 */
export interface IndexableFilterBase {
  ID: number;
  Key?: string;
  Name?: string;
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

export interface AggregateTree extends DtoAggregateTree {
  PrimaryImageFileName?: string;
  DisplayName?: string;
}

export interface HaveImagesBaseModel<TImageModel extends IImageBaseModel> {
  /** Images for the object, optional */
  Images?: Array<TImageModel>;
}

export interface IImageBaseModel extends BaseModel, HaveATypeModel<TypeModel> {
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

/* AccountModel extends BaseModel */
export interface AmARelationshipTableModel<TSlaveModel extends BaseModel>
  extends BaseModel {
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

export interface AmAStoredFileRelationshipTableModel
  extends AmARelationshipTableModel<StoredFileModel>,
  HaveSeoBaseModel,
  NameableBaseModel {
  /** Gets or sets the identifier of the file access type. */
  FileAccessTypeID: number;
  /** Gets or sets the sort order. */
  SortOrder?: number;
}

export interface HaveStoredFilesBaseModel<
  TFileModel extends AmAStoredFileRelationshipTableModel
  > {
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

export interface AmFilterableByNullableFranchiseBaseModel {
  /** Gets or sets the identifier of the Franchise. */
  FranchiseID?: number;
  /** Gets or sets the Franchise. */
  Franchise?: FranchiseModel;
  /** The Franchise key */
  FranchiseKey?: string;
  /** The Franchise name */
  FranchiseName?: string;
  /** The Franchise seo url */
  FranchiseSeoUrl?: string;
}

export interface AmFilterableByAccountSearchModel {
  /** Account ID For Search, Note: This will be overriden on data calls automatically */
  AccountID?: number;
  /** The Account Key for objects */
  AccountKey?: string;
  /** The Account Name for objects */
  AccountName?: string;
}
export interface AmFilterableByAccountBaseSearchModel
  extends AmFilterableByAccountSearchModel {}

export interface AmFilterableByBrandSearchModel {
  /** Brand ID For Search, Note: This will be overriden on data calls automatically */
  BrandID?: number;
  /** The Brand Key for objects */
  BrandKey?: string;
  /** The Brand Name for objects */
  BrandName?: string;
}
export interface AmFilterableByBrandBaseSearchModel
  extends AmFilterableByBrandSearchModel {}

export interface AmFilterableByFranchiseSearchModel {
  /** Gets or sets the identifier of the Franchise. */
  FranchiseID?: number;
  /** The Franchise key */
  FranchiseKey?: string;
  /** The Franchise name */
  FranchiseName?: string;
}
export interface AmFilterableByFranchiseBaseSearchModel
  extends AmFilterableByFranchiseSearchModel {}

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
export interface AmFilterableByCategoryBaseSearchModel
  extends AmFilterableByCategorySearchModel {}

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

export interface AmFilterableByManufacturerSearchModel {
  /** Manufacturer ID For Search */
  ManufacturerID?: number;
  /** The Manufacturer Key for objects */
  ManufacturerKey?: string;
  /** The Manufacturer Name for objects */
  ManufacturerName?: string;
}
export interface AmFilterableByManufacturerBaseSearchModel
  extends AmFilterableByManufacturerSearchModel {}

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
export interface AmFilterableByProductBaseSearchModel
  extends AmFilterableByProductSearchModel {}

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
export interface AmFilterableByStoreBaseSearchModel
  extends AmFilterableByStoreSearchModel {}

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
export interface AmFilterableByUserBaseSearchModel
  extends AmFilterableByUserSearchModel {}

export interface AmFilterableByVendorSearchModel {
  /** Vendor ID For Search, Note: This will be overriden on data calls automatically */
  VendorID?: number;
  /** The Vendor Key for objects */
  VendorKey?: string;
  /** The Vendor Name for objects */
  VendorName?: string;
}
export interface AmFilterableByVendorBaseSearchModel
  extends AmFilterableByVendorSearchModel {}

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
 * @sa AmFilterableByNullableStoreBaseModel.
 * @sa HaveJsonAttributesBaseModel.
 */
export interface SalesCollectionBaseModel
  extends BaseModel,
  HaveAStatusModel,
  HaveAStateModel,
  AmFilterableByNullableAccountBaseModel,
  AmFilterableByNullableBrandBaseModel,
  AmFilterableByNullableStoreBaseModel,
  AmFilterableByNullableUserBaseModel,
  HaveJsonAttributesBaseModel {
  /** The item quantity? */
  ItemQuantity?: number;

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

  // Totals?: CartTotals; // Generated
}

export interface SalesCollectionBaseModelT<
  TTypeModel,
  TContactModel,
  TSalesEventModel extends SalesEventBaseModel,
  TDiscountModel extends AppliedDiscountBaseModel,
  TItemDiscountModel extends AppliedDiscountBaseModel,
  TFileModel extends AmAStoredFileRelationshipTableModel
  > extends SalesCollectionBaseModel,
  HaveStoredFilesBaseModel<TFileModel>,
  HaveATypeModel<TTypeModel>,
  HaveAStatusModel,
  HaveAStateModel {
  Contacts?: Array<TContactModel>;
  /** The sales items. */
  SalesItems?: Array<SalesItemBaseModel<TItemDiscountModel>>;
  /** The discounts */
  Discounts?: Array<TDiscountModel>;
  /** The Sales Events */
  SalesEvents?: Array<TSalesEventModel>;
}

export interface SalesCollectionBaseSearchModel
  extends BaseSearchModel,
  HaveATypeSearchModel,
  HaveAStatusBaseSearchModel,
  AmFilterableByStoreSearchModel {}

/**
 * @see {@link NameableBaseModel}
 * @see {@link HaveJsonAttributesBaseModel}
 * @see {@link HaveNotesBaseModel}
 * @public
 */
export interface SalesItemBaseModel<
  TItemDiscountModel extends AppliedDiscountBaseModel
  > extends HasInventoryObject,
  NameableBaseModel,
  HaveJsonAttributesBaseModel,
  HaveNotesBaseModel {
  Sku?: string;
  ForceUniqueLineItemKey?: string;
  Quantity: number;
  QuantityBackOrdered?: number;
  QuantityPreSold?: number;
  TotalQuantity?: number;
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
}

export interface AppliedDiscountBaseModel {
  MasterID: number;
  MasterKey?: string;
  SlaveID: number;
  SlaveName?: string;
  DiscountID: number;
  DiscountKey?: string;
  DiscountName?: string;
  DiscountTotal: number;
  ApplicationsUsed?: number;
  DiscountTypeID: number;
  DiscountValue: number;
  DiscountPriority?: number;
  DiscountValueType?: number;
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
  // Items?: cefalt.store.Dictionary<string>;
  Items?: any;
  Provider: string;
  RefreshToken?: string;
  RefreshTokenExpiry?: Date;
  RequestToken?: string;
  RequestTokenSecret?: string;
  UserId?: string;
}

export interface DiscountModel extends NameableBaseModel {
  DisplayValue: string;
  DisplayDates: string;
  DisplayCodes: string;
}

export interface AuthUserSession {}
export interface AuthenticateResponse {}
export interface BaseSearchModel {}

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
export interface UploadResponse extends IUploadResponse { }

export interface IUpload {
  UploadID: string;
  Expires: Date;
  EntityFileType: FileEntityType;
  Name: string;
  Async: boolean;
}

export interface IUploadResult {
  ID: number;
  FileName: string;
  FilePath: string;
  FileKey: string;
  ContentType: string;
  ContentLength: number;
  PercentDone: number;
  TotalBytes: number;
  TransferredBytes: number;
  UploadFiles: Array<IUploadResult>;
  UploadFileStatus: UploadStatus;
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
  CurrentShipOption?: string;
  readInventory?: () => CalculatedInventories;
  "$_rawInventory"?: CalculatedInventories;
}
export interface CalculatedPrices {
  base?: number;
  sale?: number;
  msrp?: number;
  reduction?: number;
  haveBase?: boolean;
  haveSale?: boolean;
  haveMsrp?: boolean;
  haveReduction?: boolean;
  isSale?: boolean;
  amountOff?: number;
  percentOff?: number;
  loading: boolean;
}
export interface HasPricingObject extends HaveJsonAttributesBaseModel {
  readPrices?: () => CalculatedPrices;
  "$_rawPrices"?: CalculatedPrices;
}
export interface ProductModel
  extends HasInventoryObject,
  HasPricingObject,
  DtoClassProductModel {
  PrimaryImageFileName?: string;
  $_rawInventory?: CalculatedInventories;
  $_rawPrices?: CalculatedPrices;
  readInventory?: () => CalculatedInventories;
  readPrices?: () => CalculatedPrices;
}

export interface IPriceRuleModel extends PriceRuleModel {}

export interface AccountContactModel {
  SlavePhone?: string;
  SlaveFax?: string;
  SlaveEmail?: string;
  SlaveFirstName?: string;
  SlaveLastName?: string;
}

export interface GetCategoryByIDDto extends ImplementsIDOnQueryBase {}

export interface AuthProviderLoginDto {
  Username: string;
  Password: string;
  RememberMe?: boolean;
}

// export interface ProductCatalogSearchForm extends DtoClassProductCatalogSearchForm {
//   [key: string]: any;
// }