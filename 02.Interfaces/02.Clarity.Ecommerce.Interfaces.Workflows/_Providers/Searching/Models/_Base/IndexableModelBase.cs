// <copyright file="IndexableModelBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexable model base class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System;
    using System.Collections.Generic;

    /// <summary>An indexable model base.</summary>
    public abstract class IndexableModelBase
    {
        #region Suggestions
        /// <summary>Gets or sets the suggest.</summary>
        /// <value>The suggest.</value>
        public object? Suggest { get; set; }

        /// <summary>Gets or sets the name of the suggested by.</summary>
        /// <value>The name of the suggested by.</value>
        public object? SuggestedByName { get; set; }

        /// <summary>Gets or sets the suggested by long description.</summary>
        /// <value>The suggested by long description.</value>
        public object? SuggestedByDescription { get; set; }

        /// <summary>Gets or sets the suggested by custom key.</summary>
        /// <value>The suggested by custom key.</value>
        public object? SuggestedByCustomKey { get; set; }

        /// <summary>Gets or sets the suggestion field for serializable attributes.</summary>
        /// <value>The suggestion field for serializable attributes.</value>
        public object? SuggestedByQueryableSerializableAttributes { get; set; }

        /// <summary>Gets or sets the brand the record belongs to.</summary>
        /// <value>The suggested by brand.</value>
        public object? SuggestedByBrand { get; set; }

        /// <summary>Gets or sets the category the record belongs to.</summary>
        /// <value>The suggested by category.</value>
        public object? SuggestedByCategory { get; set; }

        /// <summary>Gets or sets the franchise the record belongs to.</summary>
        /// <value>The suggested by franchise.</value>
        public object? SuggestedByFranchise { get; set; }

        /// <summary>Gets or sets the manufacturer the record belongs to.</summary>
        /// <value>The suggested by manufacturer.</value>
        public object? SuggestedByManufacturer { get; set; }

        /// <summary>Gets or sets the product the record belongs to.</summary>
        /// <value>The suggested by product.</value>
        public object? SuggestedByProduct { get; set; }

        /// <summary>Gets or sets the store the record belongs to.</summary>
        /// <value>The suggested by store.</value>
        public object? SuggestedByStore { get; set; }

        /// <summary>Gets or sets the vendor the record belongs to.</summary>
        /// <value>The suggested by vendor.</value>
        public object? SuggestedByVendor { get; set; }
        #endregion

        #region Base Properties
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        public string? CustomKey { get; set; }

        /// <summary>Gets or sets a value indicating whether the active.</summary>
        /// <value>True if active, false if not.</value>
        public bool Active { get; set; }

        /// <summary>Gets or sets the created date.</summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>Gets or sets the updated date.</summary>
        /// <value>The updated date.</value>
        public DateTime? UpdatedDate { get; set; }
        #endregion

        #region NameableBase Properties
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string? Description { get; set; }
        #endregion

        #region Attributes
        /// <summary>Gets or sets the JSON attributes.</summary>
        /// <value>The JSON attributes.</value>
        public string? FilterableJsonAttributes { get; set; }

        /// <summary>Gets or sets the filterable JSON attributes.</summary>
        /// <value>The queryable JSON attributes.</value>
        public string? QueryableJsonAttributes { get; set; }

        /// <summary>Gets or sets the filterable attributes.</summary>
        /// <value>The filterable attributes.</value>
        public IEnumerable<IndexableAttributeObjectFilter>? FilterableAttributes { get; set; }

        /// <summary>Gets or sets the queryable attributes.</summary>
        /// <value>The queryable attributes.</value>
        public IEnumerable<IndexableAttributeObjectFilter>? QueryableAttributes { get; set; }

        /// <summary>Gets or sets the serializable attribute values that are queryable as an aggregate string.</summary>
        /// <value>The serializable attribute values.</value>
        public string? QueryableSerializableAttributeValuesAggregate { get; set; }
        #endregion

        #region Brands
        /// <summary>Gets or sets a value indicating whether this IndexableModelBase has brands.</summary>
        /// <value>True if this IndexableModelBase has brands, false if not.</value>
        public bool HasBrands { get; set; }

        /// <summary>Gets or sets the brands.</summary>
        /// <value>The brands.</value>
        public IEnumerable<IndexableBrandFilter>? Brands { get; set; }
        #endregion

        #region Categories
        /// <summary>Gets or sets a value indicating whether this record has categories.</summary>
        /// <value>True if this record has categories, false if not.</value>
        public bool HasCategories { get; set; }

        /// <summary>Gets or sets the category name primary.</summary>
        /// <value>The category name primary.</value>
        public string? CategoryNamePrimary { get; set; }

        /// <summary>Gets or sets the category name secondary.</summary>
        /// <value>The category name secondary.</value>
        public string? CategoryNameSecondary { get; set; }

        /// <summary>Gets or sets the category name tertiary.</summary>
        /// <value>The category name tertiary.</value>
        public string? CategoryNameTertiary { get; set; }

        /// <summary>Gets or sets the categories the product belongs to.</summary>
        /// <value>The product categories.</value>
        public IEnumerable<IndexableCategoryFilter>? Categories { get; set; }
        #endregion

        #region Franchises
        /// <summary>Gets or sets a value indicating whether this IndexableModelBase has franchises.</summary>
        /// <value>True if this IndexableModelBase has franchises, false if not.</value>
        public bool HasFranchises { get; set; }

        /// <summary>Gets or sets the franchises.</summary>
        /// <value>The franchises.</value>
        public IEnumerable<IndexableFranchiseFilter>? Franchises { get; set; }
        #endregion

        #region Location
        /// <summary>Gets or sets the identifier of the contact.</summary>
        /// <value>The identifier of the contact.</value>
        public int? ContactID { get; set; }

        /// <summary>Gets or sets the contact key.</summary>
        /// <value>The contact key.</value>
        public string? ContactKey { get; set; }

        /// <summary>Gets or sets the contact phone.</summary>
        /// <value>The contact phone.</value>
        public string? ContactPhone { get; set; }

        /// <summary>Gets or sets the contact fax.</summary>
        /// <value>The contact fax.</value>
        public string? ContactFax { get; set; }

        /// <summary>Gets or sets the contact email.</summary>
        /// <value>The contact email.</value>
        public string? ContactEmail { get; set; }

        /// <summary>Gets or sets the name of the contact first.</summary>
        /// <value>The name of the contact first.</value>
        public string? ContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the contact last.</summary>
        /// <value>The name of the contact last.</value>
        public string? ContactLastName { get; set; }

        /// <summary>Gets or sets the name of the contact city.</summary>
        /// <value>The name of the contact city.</value>
        public string? ContactCity { get; set; }

        /// <summary>Gets or sets the identifier of the contact country.</summary>
        /// <value>The identifier of the contact country.</value>
        public int? ContactCountryID { get; set; }

        /// <summary>Gets or sets the identifier of the contact region.</summary>
        /// <value>The identifier of the contact region.</value>
        public int? ContactRegionID { get; set; }

        /// <summary>Gets or sets the name of the contact region.</summary>
        /// <value>The name of the contact region.</value>
        public string? ContactRegionName { get; set; }

        /// <summary>Gets or sets a value indicating whether this IndexableModelBase has districts.</summary>
        /// <value>True if this IndexableModelBase has districts, false if not.</value>
        public bool HasDistricts { get; set; }

        /// <summary>Gets or sets the districts.</summary>
        /// <value>The districts.</value>
        public IEnumerable<IndexableDistrictFilter>? Districts { get; set; }

        /// <summary>Gets or sets the identifier of the contact district.</summary>
        /// <value>The identifier of the contact district.</value>
        public int? ContactDistrictID { get; set; }

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        public decimal? Latitude { get; set; }

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        public decimal? Longitude { get; set; }
        #endregion

        #region Manufacturers
        /// <summary>Gets or sets a value indicating whether this record has manufacturers.</summary>
        /// <value>True if this record has manufacturers, false if not.</value>
        public bool HasManufacturers { get; set; }

        /// <summary>Gets or sets the manufacturers.</summary>
        /// <value>The manufacturers.</value>
        public IEnumerable<IndexableManufacturerFilter>? Manufacturers { get; set; }
        #endregion

        #region Pricings
        /// <summary>Gets or sets the pricing to index as.</summary>
        /// <value>The pricing to index as.</value>
        public decimal PricingToIndexAs { get; set; }
        #endregion

        #region Products
        /// <summary>Gets or sets a value indicating whether this record has products.</summary>
        /// <value>True if this record has products, false if not.</value>
        public bool HasProducts { get; set; }

        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        public IEnumerable<IndexableProductFilter>? Products { get; set; }

        /// <summary>Gets or sets the stock quantity.</summary>
        /// <value>The stock quantity.</value>
        public decimal? StockQuantity { get; set; }
        #endregion

        #region Ratings
        /// <summary>Gets or sets the rating to index as.</summary>
        /// <value>The rating to index as.</value>
        public decimal RatingToIndexAs { get; set; }
        #endregion

        #region Roles
        /// <summary>Gets or sets the requires roles.</summary>
        /// <value>The requires roles.</value>
        public string? RequiresRoles { get; set; }

        /// <summary>Gets or sets a list of requires roles.</summary>
        /// <value>A List of requires roles.</value>
        public IEnumerable<IndexableRoleFilter>? RequiresRolesList { get; set; }

        /// <summary>Gets or sets the requires roles alternate.</summary>
        /// <value>The requires roles alternate.</value>
        public string? RequiresRolesAlt { get; set; }

        /// <summary>Gets or sets the requires roles list alternate.</summary>
        /// <value>The requires roles list alternate.</value>
        public IEnumerable<IndexableRoleFilter>? RequiresRolesListAlt { get; set; }
        #endregion

        #region SEO Data
        /// <summary>Gets or sets the seo keywords.</summary>
        /// <value>The seo keywords.</value>
        public string? SeoKeywords { get; set; }

        /// <summary>Gets or sets URL of the seo.</summary>
        /// <value>The seo URL.</value>
        public string? SeoUrl { get; set; }

        /// <summary>Gets or sets information describing the seo.</summary>
        /// <value>Information describing the seo.</value>
        public string? SeoDescription { get; set; }

        /// <summary>Gets or sets information describing the seo meta.</summary>
        /// <value>Information describing the seo meta.</value>
        public string? SeoMetaData { get; set; }

        /// <summary>Gets or sets the seo page title.</summary>
        /// <value>The seo page title.</value>
        public string? SeoPageTitle { get; set; }
        #endregion

        #region Stores
        /// <summary>Gets or sets a value indicating whether this record has stores.</summary>
        /// <value>True if this record has stores, false if not.</value>
        public bool HasStores { get; set; }

        /// <summary>Gets or sets the stores.</summary>
        /// <value>The stores.</value>
        public IEnumerable<IndexableStoreFilter>? Stores { get; set; }
        #endregion

        #region Type
        /// <summary>Gets or sets the identifier of the type.</summary>
        /// <value>The identifier of the type.</value>
        public int? TypeID { get; set; }

        /// <summary>Gets or sets the type key.</summary>
        /// <value>The type key.</value>
        public string? TypeKey { get; set; }

        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        public string? TypeName { get; set; }

        /// <summary>Gets or sets the type sort order.</summary>
        /// <value>The type sort order.</value>
        public int TypeSortOrder { get; set; }
        #endregion

        #region Vendors
        /// <summary>Gets or sets a value indicating whether this record has vendors.</summary>
        /// <value>True if this record has vendors, false if not.</value>
        public bool HasVendors { get; set; }

        /// <summary>Gets or sets the vendors.</summary>
        /// <value>The vendors.</value>
        public IEnumerable<IndexableVendorFilter>? Vendors { get; set; }
        #endregion

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        public int? SortOrder { get; set; }
    }
}
