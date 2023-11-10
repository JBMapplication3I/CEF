// <copyright file="DefaultData.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the default data class</summary>
#nullable enable
#pragma warning disable format
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Interfaces.DataModel;
    using Utilities;

    public class DefaultData
    {
#if ORACLE
        private readonly OracleClarityEcommerceEntities context;
#else
        private readonly ClarityEcommerceEntities context;
#endif

#if ORACLE
        public DefaultData(OracleClarityEcommerceEntities context)
#else
        public DefaultData(ClarityEcommerceEntities context)
#endif
        {
            this.context = context;
        }

        private static DateTime CreatedDate { get; } = new(2020, 1, 1);

        public void Populate()
        {
            AddAccountStatuses();
            AddAccountTypes();
            AddAccountProductTypes();
            AddAccountAssociationTypes();
            AddAdStatuses();
            AddAdTypes();
            AddAttributeTypes();
            AddAuctionTypes();
            AddAuctionStatuses();
            AddLotStatuses();
            AddBidStatuses();
            AddCalendarEventTypes();
            AddCalendarEventStatuses();
            AddCalendarEventUserAttendanceTypes();
            AddCampaignStatuses();
            AddCampaignTypes();
            AddCartStatuses();
            AddCartStates();
            AddCartTypes();
            AddCategoryTypes();
            AddContactTypes();
            AddCounterTypes();
            AddCounterLogTypes();
            AddCountries();
            AddCurrencies();
            AddEmailStatuses();
            AddEmailTypes();
            AddEventStatuses();
            AddEventTypes();
            AddFranchiseTypes();
            AddFutureImportStatuses();
            AddLanguages();
            AddGroupStatuses();
            AddGroupTypes();
            AddManufacturerTypes();
            AddNoteTypes();
            AddPackageTypes();
            AddPackages();
            AddPageViewStatuses();
            AddPageViewTypes();
            AddPaymentMethods();
            AddPaymentStatuses();
            AddPaymentTypes();
            AddProductAssociationTypes();
            AddProductDownloadTypes();
            AddProductStatuses();
            AddProductTypes();
            AddPurchaseOrderStatuses();
            AddPurchaseOrderStates();
            AddPurchaseOrderTypes();
            AddRegions();
            AddRecordVersionTypes();
            AddRepeatTypes();
            AddMemberships();
            AddReviewTypes();
            AddSalesItemTargetTypes();
            AddSalesInvoiceStatuses();
            AddSalesInvoiceTypes();
            AddSalesInvoiceStates();
            AddSalesOrderStatuses();
            AddSalesOrderTypes();
            AddSalesOrderStates();
            AddSalesQuoteStatuses();
            AddSalesQuoteTypes();
            AddSalesQuoteStates();
            AddSalesReturnReasons();
            AddSalesReturnStates();
            AddSalesReturnStatuses();
            AddSalesReturnTypes();
            AddSampleRequestStatuses();
            AddSampleRequestStates();
            AddSampleRequestTypes();
            AddDefaultSalesQuoteSettings();
            AddSettingTypes();
            AddShipCarriers();
            AddShipmentStatuses();
            AddShipmentTypes();
            AddStoreTypes();
            AddSubscriptionStatuses();
            AddSubscriptionTypes();
            AddUserStatuses();
            AddUserTypes();
            AddVendorTypes();
            AddVisitStatuses();
            AddZoneStatuses();
            AddZoneTypes();
            // SalesEvent Types
            AddGenericType<CartEventType>();
            AddGenericType<SalesInvoiceEventType>();
            AddGenericType<SalesOrderEventType>();
            AddGenericType<SalesQuoteEventType>();
            AddGenericType<SalesReturnEventType>();
            AddGenericType<SampleRequestEventType>();
            AddGenericType<PurchaseOrderEventType>();
            // Image Types
            AddProductImageTypes();
            AddGenericType<AccountImageType>();
            AddGenericType<AdImageType>();
            AddGenericType<BadgeImageType>();
            AddGenericType<BrandImageType>();
            AddGenericType<CalendarEventImageType>();
            AddGenericType<CategoryImageType>();
            AddGenericType<ContactImageType>();
            AddGenericType<CurrencyImageType>();
            AddGenericType<CountryImageType>();
            AddGenericType<FranchiseImageType>();
            AddGenericType<LanguageImageType>();
            AddGenericType<ManufacturerImageType>();
            AddGenericType<RegionImageType>();
            AddGenericType<StoreImageType>();
            AddGenericType<UserImageType>();
            AddGenericType<VendorImageType>();
            context.SaveUnitOfWork(true);
            // Custom
            AddHangfireSchemaVersion();
            AddDefaultAvalaraSettings();
        }

        private static void CreateAndAddTypeEntity<TDbSet>(
                TDbSet dbSet,
                string key,
                string name,
                string displayName,
                bool isRestockingFeeApplicable,
                string? translationKey = null)
            where TDbSet : class, IDbSet<SalesReturnReason>
        {
            if (dbSet.Any(x => x.Active && x.Name == name))
            {
                return;
            }
            dbSet.Add(new()
            {
                CreatedDate = CreatedDate,
                Active = true,
                CustomKey = key,
                Name = name,
                DisplayName = displayName,
                TranslationKey = translationKey,
                IsRestockingFeeApplicable = isRestockingFeeApplicable,
            });
        }

        private static void CreateAndAddTypeEntity<TEntity>(
                IDbContext context,
                string key,
                string name,
                string? displayName,
                int? sortOrder,
                string? translationKey = null,
                string jsonAttributes = "{}")
            where TEntity : class, ITypableBase, new()
        {
            if (context.Set<TEntity>().Any(x => x.Name == name))
            {
                return;
            }
            context.Set<TEntity>()
                .Add(new()
                {
                    CreatedDate = CreatedDate,
                    Active = true,
                    CustomKey = key,
                    Name = name,
                    DisplayName = displayName,
                    SortOrder = sortOrder,
                    TranslationKey = translationKey,
                    JsonAttributes = jsonAttributes,
                });
        }

        private static void CreateAndAddStatusEntity<TEntity>(IDbContext context, string key, string name, string displayName, int? sortOrder, string? translationKey = null, string jsonAttributes = "{}")
            where TEntity : class, IStatusableBase, new()
        {
            if (context.Set<TEntity>().Any(x => x.Name == name))
            {
                return;
            }
            context.Set<TEntity>()
                .Add(new()
                {
                    CreatedDate = CreatedDate,
                    Active = true,
                    CustomKey = key,
                    Name = name,
                    DisplayName = displayName,
                    SortOrder = sortOrder,
                    TranslationKey = translationKey,
                    JsonAttributes = jsonAttributes,
                });
        }

        private static void CreateAndAddStateEntity<TEntity>(IDbContext context, string key, string name, string displayName, int? sortOrder, string? translationKey = null, string jsonAttributes = "{}")
            where TEntity : class, IStateableBase, new()
        {
            if (context.Set<TEntity>().Any(x => x.Name == name))
            {
                return;
            }
            context.Set<TEntity>()
                .Add(new()
                {
                    CreatedDate = CreatedDate,
                    Active = true,
                    CustomKey = key,
                    Name = name,
                    DisplayName = displayName,
                    SortOrder = sortOrder,
                    TranslationKey = translationKey,
                    JsonAttributes = jsonAttributes,
                });
        }

        private void AddGenericType<TEntity>()
            where TEntity : class, ITypableBase, new()
        {
            CreateAndAddTypeEntity<TEntity>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
        }

        #region Accounts
        private void AddAccountTypes()
        {
            CreateAndAddTypeEntity<AccountType>(context, key: "VENDOR", name: "Vendor", displayName: "Vendor", sortOrder: 0);
            CreateAndAddTypeEntity<AccountType>(context, key: "CUSTOMER", name: "Customer", displayName: "Customer", sortOrder: 1);
            CreateAndAddTypeEntity<AccountType>(context, key: "AFFILIATE", name: "Affiliate", displayName: "Affiliate", sortOrder: 2);
            CreateAndAddTypeEntity<AccountType>(context, key: "ORGANIZATION", name: "Organization", displayName: "Organization", sortOrder: 3);
            context.SaveUnitOfWork(true);
        }

        private void AddAccountAssociationTypes()
        {
            CreateAndAddTypeEntity<AccountAssociationType>(context, key: "Affiliate", name: "Affiliate", displayName: "Affiliate", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddAccountProductTypes()
        {
            CreateAndAddTypeEntity<AccountProductType>(context, key: "Customized", name: "Customized", displayName: "Customized", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddAccountStatuses()
        {
            CreateAndAddStatusEntity<AccountStatus>(context, key: "NORMAL", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Advertising
        private void AddAdTypes()
        {
            CreateAndAddTypeEntity<AdType>(context, key: "GENERAL", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddAdStatuses()
        {
            CreateAndAddStatusEntity<AdStatus>(context, key: "NORMAL", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddZoneTypes()
        {
            CreateAndAddTypeEntity<ZoneType>(context, key: "GENERAL", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddZoneStatuses()
        {
            CreateAndAddStatusEntity<ZoneStatus>(context, key: "NORMAL", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Attributes
        private void AddAttributeTypes()
        {
            CreateAndAddTypeEntity<AttributeType>(context, key: "GENERAL", name: "General", displayName: "General", sortOrder: 0);
            CreateAndAddTypeEntity<AttributeType>(context, key: "SALES-ORDER", name: "Sales Order", displayName: "Order", sortOrder: 1);
            CreateAndAddTypeEntity<AttributeType>(context, key: "CART", name: "Cart", displayName: "Cart", sortOrder: 2);
            CreateAndAddTypeEntity<AttributeType>(context, key: "PRODUCT", name: "Product", displayName: "Product", sortOrder: 3);
            CreateAndAddTypeEntity<AttributeType>(context, key: "CART-ITEM", name: "Cart Item", displayName: "Cart Item", sortOrder: 4);
            CreateAndAddTypeEntity<AttributeType>(context, key: "CATEGORY", name: "Category", displayName: "Category", sortOrder: 5);
            CreateAndAddTypeEntity<AttributeType>(context, key: "ACCOUNT", name: "Account", displayName: "Account", sortOrder: 6);
            CreateAndAddTypeEntity<AttributeType>(context, key: "SALES-INVOICE", name: "Sales Invoice", displayName: "Invoice", sortOrder: 7);
            CreateAndAddTypeEntity<AttributeType>(context, key: "SALES-INVOICE-ITEM", name: "Sales Invoice Item", displayName: "Invoice Item", sortOrder: 8);
            CreateAndAddTypeEntity<AttributeType>(context, key: "SALES-QUOTE", name: "Sales Quote", displayName: "Quote", sortOrder: 9);
            CreateAndAddTypeEntity<AttributeType>(context, key: "SALES-QUOTE-ITEM", name: "Sales Quote Item", displayName: "Quote Item", sortOrder: 10);
            CreateAndAddTypeEntity<AttributeType>(context, key: "PURCHASE-ORDER", name: "Purchase Order", displayName: "Purchase Order", sortOrder: 11);
            CreateAndAddTypeEntity<AttributeType>(context, key: "PURCHASE-ORDER-ITEM", name: "Purchase Order Item", displayName: "Purchase Order Item", sortOrder: 12);
            CreateAndAddTypeEntity<AttributeType>(context, key: "USER", name: "User", displayName: "User", sortOrder: 13);
            CreateAndAddTypeEntity<AttributeType>(context, key: "PRODUCT-ASSOCIATION", name: "Product Association", displayName: "Product Association", sortOrder: 14);
            CreateAndAddTypeEntity<AttributeType>(context, key: "PRODUCT-CATEGORY", name: "Product Category", displayName: "Product Category", sortOrder: 15);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Auctions
        private void AddAuctionTypes()
        {
            CreateAndAddTypeEntity<AuctionType>(context, key: "In Person - Called", name: "In Person - Called", displayName: "In Person - Called", sortOrder: 0);
            CreateAndAddTypeEntity<AuctionType>(context, key: "In Person - Silent", name: "In Person - Silent", displayName: "In Person - Silent", sortOrder: 1);
            CreateAndAddTypeEntity<AuctionType>(context, key: "Online", name: "Online", displayName: "Online", sortOrder: 2);
            context.SaveUnitOfWork(true);
        }

        private void AddAuctionStatuses()
        {
            CreateAndAddStatusEntity<AuctionStatus>(context, key: "Pending", name: "Pending", displayName: "Pending", sortOrder: 0);
            CreateAndAddStatusEntity<AuctionStatus>(context, key: "Open", name: "Open", displayName: "Open", sortOrder: 0);
            CreateAndAddStatusEntity<AuctionStatus>(context, key: "Closed", name: "Closed", displayName: "Closed", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddLotStatuses()
        {
            CreateAndAddStatusEntity<LotStatus>(context, key: "Pending", name: "Pending", displayName: "Pending", sortOrder: 0);
            CreateAndAddStatusEntity<LotStatus>(context, key: "Open", name: "Open", displayName: "Open", sortOrder: 1);
            CreateAndAddStatusEntity<LotStatus>(context, key: "Closed", name: "Closed", displayName: "Closed", sortOrder: 2);
            context.SaveUnitOfWork(true);
        }

        private void AddBidStatuses()
        {
            CreateAndAddStatusEntity<BidStatus>(context, key: "Losing", name: "Losing", displayName: "Losing", sortOrder: 0);
            CreateAndAddStatusEntity<BidStatus>(context, key: "Winning", name: "Winning", displayName: "Winning", sortOrder: 1);
            CreateAndAddStatusEntity<BidStatus>(context, key: "Lost", name: "Lost", displayName: "Lost", sortOrder: 2);
            CreateAndAddStatusEntity<BidStatus>(context, key: "Won", name: "Won", displayName: "Won", sortOrder: 3);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Calendar Events
        private void AddCalendarEventTypes()
        {
            CreateAndAddTypeEntity<CalendarEventType>(context, key: "GENERAL", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddCalendarEventStatuses()
        {
            CreateAndAddStatusEntity<CalendarEventStatus>(context, key: "NORMAL", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddCalendarEventUserAttendanceTypes()
        {
            CreateAndAddTypeEntity<UserEventAttendanceType>(context, key: "GENERAL", name: "General", displayName: "General", sortOrder: 0);
            CreateAndAddTypeEntity<UserEventAttendanceType>(context, key: "ORGANIZER", name: "Organizer", displayName: "Organizer", sortOrder: 1);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Categories
        private void AddCategoryTypes()
        {
            CreateAndAddTypeEntity<CategoryType>(context, key: "GENERAL", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Contacts
        private void AddContactTypes()
        {
            CreateAndAddTypeEntity<ContactType>(context, key: "User", name: "User", displayName: "User", sortOrder: 0);
            CreateAndAddTypeEntity<ContactType>(context, key: "Billing", name: "Billing", displayName: "Billing", sortOrder: 1);
            CreateAndAddTypeEntity<ContactType>(context, key: "Shipping", name: "Shipping", displayName: "Shipping", sortOrder: 2);
            CreateAndAddTypeEntity<ContactType>(context, key: "Account Address", name: "Account Address", displayName: "Account Address", sortOrder: 3);
            CreateAndAddTypeEntity<ContactType>(context, key: "Store", name: "Store", displayName: "Store", sortOrder: 4);
            CreateAndAddTypeEntity<ContactType>(context, key: "Returning", name: "Returning", displayName: "Returning", sortOrder: 5);
            CreateAndAddTypeEntity<ContactType>(context, key: "Warehouse", name: "Warehouse", displayName: "Warehouse", sortOrder: 6);
            CreateAndAddTypeEntity<ContactType>(context, key: "Manufacturer", name: "Manufacturer", displayName: "Manufacturer", sortOrder: 7);
            CreateAndAddTypeEntity<ContactType>(context, key: "Vendor", name: "Vendor", displayName: "Vendor", sortOrder: 8);
            context.SaveUnitOfWork(true);
        }

        private void AddUserStatuses()
        {
            CreateAndAddStatusEntity<UserStatus>(context, key: "Registered", name: "Registered", displayName: "Registered", sortOrder: 0);
            CreateAndAddStatusEntity<UserStatus>(context, key: "Email Sent", name: "Email Sent", displayName: "Email Sent", sortOrder: 1);
            CreateAndAddStatusEntity<UserStatus>(context, key: "Email Verified", name: "Email Verified", displayName: "Email Verified", sortOrder: 2);
            context.SaveUnitOfWork(true);
        }

        private void AddUserTypes()
        {
            CreateAndAddTypeEntity<UserType>(context, key: "Customer", name: "Customer", displayName: "Customer", sortOrder: 0);
            CreateAndAddTypeEntity<UserType>(context, key: "Partner / Affiliate", name: "Partner / Affiliate", displayName: "Partner / Affiliate", sortOrder: 1);
            CreateAndAddTypeEntity<UserType>(context, key: "Administrator", name: "Administrator", displayName: "Administrator", sortOrder: 2);
            CreateAndAddTypeEntity<UserType>(context, key: "No Access", name: "No Access", displayName: "No Access", sortOrder: 3);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Counters
        private void AddCounterTypes()
        {
            CreateAndAddTypeEntity<CounterType>(context, key: "COUNTER", name: "Counter", displayName: "General Counter", sortOrder: 0);
            CreateAndAddTypeEntity<CounterType>(context, key: "AD-IMPRESSION", name: "AdImpression", displayName: "Ad Impression", sortOrder: 2);
            CreateAndAddTypeEntity<CounterType>(context, key: "AD-CLICK", name: "AdClick", displayName: "Ad Click", sortOrder: 3);
            context.SaveUnitOfWork(true);
        }

        private void AddCounterLogTypes()
        {
            CreateAndAddTypeEntity<CounterLogType>(context, key: "COUNTER-CREATED", name: "CounterCreated", displayName: "Counter Created", sortOrder: 0);
            CreateAndAddTypeEntity<CounterLogType>(context, key: "COUNTER-UPDATED", name: "CounterUpdated", displayName: "Counter Updated", sortOrder: 1);
            CreateAndAddTypeEntity<CounterLogType>(context, key: "AD-IMPRESSION-COUNTER-CREATED", name: "AdImpressionCounterCreated", displayName: "Ad Impression Counter Created", sortOrder: 2);
            CreateAndAddTypeEntity<CounterLogType>(context, key: "AD-IMPRESSION-COUNTER-UPDATED", name: "AdImpressionCounterUpdated", displayName: "Ad Impression Counter Updated", sortOrder: 3);
            CreateAndAddTypeEntity<CounterLogType>(context, key: "AD-CLICK-COUNTER-CREATED", name: "AdClickCounterCreated", displayName: "Ad Click Counter Created", sortOrder: 4);
            CreateAndAddTypeEntity<CounterLogType>(context, key: "AD-CLICK-COUNTER-UPDATED", name: "AdClickCounterUpdated", displayName: "Ad Click Counter Updated", sortOrder: 5);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Currencies
        private void AddCurrencies()
        {
            if (context.Currencies.Any())
            {
                return;
            }
            context.Currencies.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "USD", Name = "US Dollar", UnicodeSymbolValue = 24 });
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Franchises
        private void AddFranchiseTypes()
        {
            CreateAndAddTypeEntity<FranchiseType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Geography
        private void AddCountries()
        {
            if (context.Countries.Any())
            {
                return;
            }
            context.Countries.Add(new() { Code = "USA", CreatedDate = CreatedDate, Active = true, CustomKey = "United States of America", Name = "United States of America" });
            // context.Countries.Add(new() { Code = "CA", CreatedDate = CreatedDate, Active = true, CustomKey = "Canada", Name = "Canada" });
            context.SaveUnitOfWork(true);
        }

        private void AddRegions()
        {
            if (context.Regions.Any())
            {
                return;
            }
            context.Regions.Add(new() { CountryID = 1, Code = "AL", CreatedDate = CreatedDate, Active = true, CustomKey = "Alabama", Name = "Alabama" });
            context.Regions.Add(new() { CountryID = 1, Code = "AK", CreatedDate = CreatedDate, Active = true, CustomKey = "Alaska", Name = "Alaska" });
            context.Regions.Add(new() { CountryID = 1, Code = "AZ", CreatedDate = CreatedDate, Active = true, CustomKey = "Arizona", Name = "Arizona" });
            context.Regions.Add(new() { CountryID = 1, Code = "AR", CreatedDate = CreatedDate, Active = true, CustomKey = "Arkansas", Name = "Arkansas" });
            context.Regions.Add(new() { CountryID = 1, Code = "CA", CreatedDate = CreatedDate, Active = true, CustomKey = "California", Name = "California" });
            context.Regions.Add(new() { CountryID = 1, Code = "CO", CreatedDate = CreatedDate, Active = true, CustomKey = "Colorado", Name = "Colorado" });
            context.Regions.Add(new() { CountryID = 1, Code = "CT", CreatedDate = CreatedDate, Active = true, CustomKey = "Connecticut", Name = "Connecticut" });
            context.Regions.Add(new() { CountryID = 1, Code = "DE", CreatedDate = CreatedDate, Active = true, CustomKey = "Delaware", Name = "Delaware" });
            context.Regions.Add(new() { CountryID = 1, Code = "FL", CreatedDate = CreatedDate, Active = true, CustomKey = "Florida", Name = "Florida" });
            context.Regions.Add(new() { CountryID = 1, Code = "GA", CreatedDate = CreatedDate, Active = true, CustomKey = "Georgia", Name = "Georgia" });
            context.Regions.Add(new() { CountryID = 1, Code = "HI", CreatedDate = CreatedDate, Active = true, CustomKey = "Hawaii", Name = "Hawaii" });
            context.Regions.Add(new() { CountryID = 1, Code = "ID", CreatedDate = CreatedDate, Active = true, CustomKey = "Idaho", Name = "Idaho" });
            context.Regions.Add(new() { CountryID = 1, Code = "IL", CreatedDate = CreatedDate, Active = true, CustomKey = "Illinois", Name = "Illinois" });
            context.Regions.Add(new() { CountryID = 1, Code = "IN", CreatedDate = CreatedDate, Active = true, CustomKey = "Indiana", Name = "Indiana" });
            context.Regions.Add(new() { CountryID = 1, Code = "IA", CreatedDate = CreatedDate, Active = true, CustomKey = "Iowa", Name = "Iowa" });
            context.Regions.Add(new() { CountryID = 1, Code = "KS", CreatedDate = CreatedDate, Active = true, CustomKey = "Kansas", Name = "Kansas" });
            context.Regions.Add(new() { CountryID = 1, Code = "KY", CreatedDate = CreatedDate, Active = true, CustomKey = "Kentucky", Name = "Kentucky" });
            context.Regions.Add(new() { CountryID = 1, Code = "LA", CreatedDate = CreatedDate, Active = true, CustomKey = "Louisiana", Name = "Louisiana" });
            context.Regions.Add(new() { CountryID = 1, Code = "ME", CreatedDate = CreatedDate, Active = true, CustomKey = "Maine", Name = "Maine" });
            context.Regions.Add(new() { CountryID = 1, Code = "MD", CreatedDate = CreatedDate, Active = true, CustomKey = "Maryland", Name = "Maryland" });
            context.Regions.Add(new() { CountryID = 1, Code = "MA", CreatedDate = CreatedDate, Active = true, CustomKey = "Massachusetts", Name = "Massachusetts" });
            context.Regions.Add(new() { CountryID = 1, Code = "MI", CreatedDate = CreatedDate, Active = true, CustomKey = "Michigan", Name = "Michigan" });
            context.Regions.Add(new() { CountryID = 1, Code = "MN", CreatedDate = CreatedDate, Active = true, CustomKey = "Minnesota", Name = "Minnesota" });
            context.Regions.Add(new() { CountryID = 1, Code = "MS", CreatedDate = CreatedDate, Active = true, CustomKey = "Mississippi", Name = "Mississippi" });
            context.Regions.Add(new() { CountryID = 1, Code = "MO", CreatedDate = CreatedDate, Active = true, CustomKey = "Missouri", Name = "Missouri" });
            context.Regions.Add(new() { CountryID = 1, Code = "MT", CreatedDate = CreatedDate, Active = true, CustomKey = "Montana", Name = "Montana" });
            context.Regions.Add(new() { CountryID = 1, Code = "NE", CreatedDate = CreatedDate, Active = true, CustomKey = "Nebraska", Name = "Nebraska" });
            context.Regions.Add(new() { CountryID = 1, Code = "NV", CreatedDate = CreatedDate, Active = true, CustomKey = "Nevada", Name = "Nevada" });
            context.Regions.Add(new() { CountryID = 1, Code = "NH", CreatedDate = CreatedDate, Active = true, CustomKey = "New Hampshire", Name = "New Hampshire" });
            context.Regions.Add(new() { CountryID = 1, Code = "NJ", CreatedDate = CreatedDate, Active = true, CustomKey = "New Jersey", Name = "New Jersey" });
            context.Regions.Add(new() { CountryID = 1, Code = "NM", CreatedDate = CreatedDate, Active = true, CustomKey = "New Mexico", Name = "New Mexico" });
            context.Regions.Add(new() { CountryID = 1, Code = "NY", CreatedDate = CreatedDate, Active = true, CustomKey = "New York", Name = "New York" });
            context.Regions.Add(new() { CountryID = 1, Code = "NC", CreatedDate = CreatedDate, Active = true, CustomKey = "North Carolina", Name = "North Carolina" });
            context.Regions.Add(new() { CountryID = 1, Code = "ND", CreatedDate = CreatedDate, Active = true, CustomKey = "North Dakota", Name = "North Dakota" });
            context.Regions.Add(new() { CountryID = 1, Code = "OH", CreatedDate = CreatedDate, Active = true, CustomKey = "Ohio", Name = "Ohio" });
            context.Regions.Add(new() { CountryID = 1, Code = "OK", CreatedDate = CreatedDate, Active = true, CustomKey = "Oklahoma", Name = "Oklahoma" });
            context.Regions.Add(new() { CountryID = 1, Code = "OR", CreatedDate = CreatedDate, Active = true, CustomKey = "Oregon", Name = "Oregon" });
            context.Regions.Add(new() { CountryID = 1, Code = "PA", CreatedDate = CreatedDate, Active = true, CustomKey = "Pennsylvania", Name = "Pennsylvania" });
            context.Regions.Add(new() { CountryID = 1, Code = "RI", CreatedDate = CreatedDate, Active = true, CustomKey = "Rhode Island", Name = "Rhode Island" });
            context.Regions.Add(new() { CountryID = 1, Code = "SC", CreatedDate = CreatedDate, Active = true, CustomKey = "South Carolina", Name = "South Carolina" });
            context.Regions.Add(new() { CountryID = 1, Code = "SD", CreatedDate = CreatedDate, Active = true, CustomKey = "South Dakota", Name = "South Dakota" });
            context.Regions.Add(new() { CountryID = 1, Code = "TN", CreatedDate = CreatedDate, Active = true, CustomKey = "Tennessee", Name = "Tennessee" });
            context.Regions.Add(new() { CountryID = 1, Code = "TX", CreatedDate = CreatedDate, Active = true, CustomKey = "Texas", Name = "Texas" });
            context.Regions.Add(new() { CountryID = 1, Code = "UT", CreatedDate = CreatedDate, Active = true, CustomKey = "Utah", Name = "Utah" });
            context.Regions.Add(new() { CountryID = 1, Code = "VT", CreatedDate = CreatedDate, Active = true, CustomKey = "Vermont", Name = "Vermont" });
            context.Regions.Add(new() { CountryID = 1, Code = "VA", CreatedDate = CreatedDate, Active = true, CustomKey = "Virginia", Name = "Virginia" });
            context.Regions.Add(new() { CountryID = 1, Code = "WA", CreatedDate = CreatedDate, Active = true, CustomKey = "Washington", Name = "Washington" });
            context.Regions.Add(new() { CountryID = 1, Code = "WV", CreatedDate = CreatedDate, Active = true, CustomKey = "West Virginia", Name = "West Virginia" });
            context.Regions.Add(new() { CountryID = 1, Code = "WI", CreatedDate = CreatedDate, Active = true, CustomKey = "Wisconsin", Name = "Wisconsin" });
            context.Regions.Add(new() { CountryID = 1, Code = "WY", CreatedDate = CreatedDate, Active = true, CustomKey = "Wyoming", Name = "Wyoming" });
            context.Regions.Add(new() { CountryID = 1, Code = "DC", CreatedDate = CreatedDate, Active = true, Name = "Washington D.C." });
            context.Regions.Add(new() { CountryID = 1, Code = "PR", CreatedDate = CreatedDate, Active = true, CustomKey = "Puerto Rico", Name = "Puerto Rico" });
            context.Regions.Add(new() { CountryID = 1, Code = "VI", CreatedDate = CreatedDate, Active = true, CustomKey = "Virgin Islands", Name = "Virgin Islands" });
            // context.Regions.Add(new() { CountryID = 2, Code = "AB", CreatedDate = CreatedDate, Active = true, CustomKey = "Alberta", Name = "Alberta" });
            // context.Regions.Add(new() { CountryID = 2, Code = "BC", CreatedDate = CreatedDate, Active = true, CustomKey = "British Columbia", Name = "British Columbia" });
            // context.Regions.Add(new() { CountryID = 2, Code = "MB", CreatedDate = CreatedDate, Active = true, CustomKey = "Manitoba", Name = "Manitoba" });
            // context.Regions.Add(new() { CountryID = 2, Code = "NB", CreatedDate = CreatedDate, Active = true, CustomKey = "New Brunswick", Name = "New Brunswick" });
            // context.Regions.Add(new() { CountryID = 2, Code = "NL", CreatedDate = CreatedDate, Active = true, CustomKey = "Newfoundland and Labrador", Name = "Newfoundland and Labrador" });
            // context.Regions.Add(new() { CountryID = 2, Code = "NT", CreatedDate = CreatedDate, Active = true, CustomKey = "Northwest Territories", Name = "Northwest Territories" });
            // context.Regions.Add(new() { CountryID = 2, Code = "NS", CreatedDate = CreatedDate, Active = true, CustomKey = "Nova Scotia", Name = "Nova Scotia" });
            // context.Regions.Add(new() { CountryID = 2, Code = "NU", CreatedDate = CreatedDate, Active = true, CustomKey = "Nunavut", Name = "Nunavut" });
            // context.Regions.Add(new() { CountryID = 2, Code = "ON", CreatedDate = CreatedDate, Active = true, CustomKey = "Ontario", Name = "Ontario" });
            // context.Regions.Add(new() { CountryID = 2, Code = "PE", CreatedDate = CreatedDate, Active = true, CustomKey = "Prince Edward Island", Name = "Prince Edward Island" });
            // context.Regions.Add(new() { CountryID = 2, Code = "QC", CreatedDate = CreatedDate, Active = true, CustomKey = "Quebec", Name = "Quebec" });
            // context.Regions.Add(new() { CountryID = 2, Code = "SK", CreatedDate = CreatedDate, Active = true, CustomKey = "Saskatchewan", Name = "Saskatchewan" });
            // context.Regions.Add(new() { CountryID = 2, Code = "YT", CreatedDate = CreatedDate, Active = true, CustomKey = "Yukon", Name = "Yukon" });
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Globalization
        private void AddLanguages()
        {
            if (context.Languages.Any())
            {
                return;
            }
            context.Languages.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "en_US", Locale = "en_US", UnicodeName = "English (US)" });
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Hangfire
        private void AddHangfireSchemaVersion()
        {
            const int CurrentVersion = 5;
            if (context.HangfireSchemas.Any())
            {
                return;
            }
            context.HangfireSchemas.Add(new() { Version = CurrentVersion });
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Invoicing
        private void AddSalesInvoiceStatuses()
        {
            CreateAndAddStatusEntity<SalesInvoiceStatus>(context, sortOrder: 0, key: "Unpaid",         name: "Unpaid",         displayName: "Unpaid",         translationKey: "ui.storefront.SalesInvoiceStatuses.Unpaid");
            CreateAndAddStatusEntity<SalesInvoiceStatus>(context, sortOrder: 1, key: "Partially Paid", name: "Partially Paid", displayName: "Partially Paid", translationKey: "ui.storefront.SalesInvoiceStatuses.PartiallyPaid");
            CreateAndAddStatusEntity<SalesInvoiceStatus>(context, sortOrder: 2, key: "Paid",           name: "Paid",           displayName: "Paid",           translationKey: "ui.storefront.SalesInvoiceStatuses.Paid");
            CreateAndAddStatusEntity<SalesInvoiceStatus>(context, sortOrder: 3, key: "Void",           name: "Void",           displayName: "Void",           translationKey: "ui.storefront.SalesInvoiceStatuses.Void");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesInvoiceStates()
        {
            CreateAndAddStateEntity<SalesInvoiceState>(context, sortOrder: 0, key: "WORK",    name: "Work",    displayName: "Work");
            CreateAndAddStateEntity<SalesInvoiceState>(context, sortOrder: 1, key: "HISTORY", name: "History", displayName: "History");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesInvoiceTypes()
        {
            CreateAndAddTypeEntity<SalesInvoiceType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Group
        private void AddGroupStatuses()
        {
            CreateAndAddStatusEntity<GroupStatus>(context, key: "NORMAL", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddGroupTypes()
        {
            CreateAndAddTypeEntity<GroupType>(context, key: "VILLAGE", name: "Village", displayName: "Village", sortOrder: 0);
            CreateAndAddTypeEntity<GroupType>(context, key: "FAMILY", name: "Family", displayName: "Family", sortOrder: 1);
            CreateAndAddTypeEntity<GroupType>(context, key: "FRIENDSHIP", name: "Friendship", displayName: "Friendship", sortOrder: 2);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Manufacturers
        private void AddManufacturerTypes()
        {
            CreateAndAddTypeEntity<ManufacturerType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Messaging
        private void AddEmailStatuses()
        {
            CreateAndAddStatusEntity<EmailStatus>(context, key: "Pending", name: "Pending", displayName: "Pending", sortOrder: 0);
            CreateAndAddStatusEntity<EmailStatus>(context, key: "Retrying", name: "Send Failed - Retrying", displayName: "Send Failed - Retrying", sortOrder: 1);
            CreateAndAddStatusEntity<EmailStatus>(context, key: "Retries Failed", name: "Send Failed - Retries Failed", displayName: "Send Failed - Retries Failed", sortOrder: 2);
            CreateAndAddStatusEntity<EmailStatus>(context, key: "Sent", name: "Sent", displayName: "Sent", sortOrder: 3);
            CreateAndAddStatusEntity<EmailStatus>(context, key: "Cancelled", name: "Cancelled", displayName: "Cancelled", sortOrder: 4);
            context.SaveUnitOfWork(true);
        }

        private void AddEmailTypes()
        {
            CreateAndAddTypeEntity<EmailType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Ordering
        private void AddSalesOrderStatuses()
        {
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 00, key: "Pending",                  name: "Pending",                  displayName: "Pending",                  translationKey: "ui.storefront.SalesOrderStatuses.Pending");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 05, key: "On Hold",                  name: "On Hold",                  displayName: "On Hold",                  translationKey: "ui.storefront.SalesOrderStatuses.OnHold");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 10, key: "Confirmed",                name: "Confirmed",                displayName: "Confirmed",                translationKey: "ui.storefront.SalesOrderStatuses.Confirmed");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 11, key: "Backordered",              name: "Backordered",              displayName: "Backordered",              translationKey: "ui.storefront.SalesOrderStatuses.Backordered");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 12, key: "Split",                    name: "Split",                    displayName: "Split",                    translationKey: "ui.storefront.SalesOrderStatuses.Split"); // Closed
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 20, key: "Partial Payment Received", name: "Partial Payment Received", displayName: "Partial Payment Received", translationKey: "ui.storefront.SalesOrderStatuses.PartialPaymentReceived");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 21, key: "Full Payment Received",    name: "Full Payment Received",    displayName: "Full Payment Received",    translationKey: "ui.storefront.SalesOrderStatuses.FullPaymentReceived");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 30, key: "Processing",               name: "Processing",               displayName: "Processing",               translationKey: "ui.storefront.SalesOrderStatuses.Processing"); // Create Pick ticket
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 40, key: "Shipped",                  name: "Shipped",                  displayName: "Shipped",                  translationKey: "ui.storefront.SalesOrderStatuses.Shipped");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 41, key: "Shipped from Vendor",      name: "Shipped from Vendor",      displayName: "Shipped from Vendor",      translationKey: "ui.storefront.SalesOrderStatuses.ShippedFromVendor"); // Drop Ship
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 42, key: "Ready for Pickup",         name: "Ready for Pickup",         displayName: "Ready for Pickup",         translationKey: "ui.storefront.SalesOrderStatuses.ReadyForPickup");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 98, key: "Completed",                name: "Completed",                displayName: "Completed",                translationKey: "ui.storefront.SalesOrderStatuses.Completed");
            CreateAndAddStatusEntity<SalesOrderStatus>(context, sortOrder: 99, key: "Void",                     name: "Void",                     displayName: "Void",                     translationKey: "ui.storefront.SalesOrderStatuses.Void");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesOrderStates()
        {
            CreateAndAddStateEntity<SalesOrderState>(context, sortOrder: 0, key: "WORK", name: "Work", displayName: "Work");
            CreateAndAddStateEntity<SalesOrderState>(context, sortOrder: 1, key: "HISTORY", name: "History", displayName: "History");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesOrderTypes()
        {
            CreateAndAddTypeEntity<SalesOrderType>(context, sortOrder: 0, key: "Web", name: "Web", displayName: "Web");
            CreateAndAddTypeEntity<SalesOrderType>(context, sortOrder: 1, key: "Phone", name: "Phone", displayName: "Phone");
            CreateAndAddTypeEntity<SalesOrderType>(context, sortOrder: 2, key: "On Site", name: "On Site", displayName: "On Site");
            CreateAndAddTypeEntity<SalesOrderType>(context, sortOrder: 3, key: "Sales Order Child", name: "Sales Order Child", displayName: "Sales Order Child");
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Payments
        private void AddPaymentMethods()
        {
            if (context.PaymentMethods.Any())
            {
                return;
            }
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Credit Card", Name = "Credit Card" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "eCheck", Name = "eCheck" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "ACH", Name = "ACH" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Free", Name = "Free" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Invoice", Name = "Invoice" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Quote Me", Name = "Quote Me" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Payoneer", Name = "Payoneer" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "PayPal", Name = "PayPal" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Store Credit", Name = "Store Credit" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Wire Transfer", Name = "Wire Transfer" });
            context.PaymentMethods.Add(new() { CreatedDate = CreatedDate, Active = true, CustomKey = "Custom", Name = "Custom" });
            context.SaveUnitOfWork(true);
        }

        private void AddPaymentStatuses()
        {
            CreateAndAddStatusEntity<PaymentStatus>(context, key: "No Payment Received", name: "No Payment Received", displayName: "No Payment Received", sortOrder: 0);
            CreateAndAddStatusEntity<PaymentStatus>(context, key: "Payment Received", name: "Payment Received", displayName: "Payment Received", sortOrder: 1);
            context.SaveUnitOfWork(true);
        }

        private void AddPaymentTypes()
        {
            CreateAndAddTypeEntity<PaymentType>(context, key: "Mastercard", name: "Mastercard", displayName: "Mastercard", sortOrder: 0);
            CreateAndAddTypeEntity<PaymentType>(context, key: "VISA", name: "VISA", displayName: "VISA", sortOrder: 1);
            CreateAndAddTypeEntity<PaymentType>(context, key: "American Express", name: "American Express", displayName: "American Express", sortOrder: 2);
            CreateAndAddTypeEntity<PaymentType>(context, key: "Discover", name: "Discover", displayName: "Discover", sortOrder: 3);
            CreateAndAddTypeEntity<PaymentType>(context, key: "Other", name: "Other", displayName: "Other", sortOrder: 4);
            context.SaveUnitOfWork(true);
        }

        // ReSharper disable once CognitiveComplexity
        private void AddRepeatTypes()
        {
            if (context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Monthly" && x.SortOrder != 100))
            {
                var entity = context.RepeatTypes.First(x => x.Active && x.CustomKey == "Monthly" && x.SortOrder != 100);
                entity.SortOrder = 100;
                entity.InitialBonusBillingPeriods = 0;
                entity.RepeatableBillingPeriods = 1;
            }
            else if (!context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Monthly"))
            {
                context.RepeatTypes.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "Monthly",
                    Name = "Monthly",
                    DisplayName = "Monthly",
                    InitialBonusBillingPeriods = 0,
                    RepeatableBillingPeriods = 1,
                    SortOrder = 100,
                });
            }
            if (context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Quarterly" && x.SortOrder != 200))
            {
                var entity = context.RepeatTypes.First(x => x.Active && x.CustomKey == "Quarterly" && x.SortOrder != 200);
                entity.SortOrder = 200;
                entity.InitialBonusBillingPeriods = 0;
                entity.RepeatableBillingPeriods = 3;
            }
            else if (!context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Quarterly"))
            {
                context.RepeatTypes.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "Quarterly",
                    Name = "Quarterly",
                    DisplayName = "Quarterly",
                    InitialBonusBillingPeriods = 0,
                    RepeatableBillingPeriods = 3,
                    SortOrder = 200,
                });
            }
            if (context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Semi-Annually" && x.SortOrder != 300))
            {
                var entity = context.RepeatTypes.First(x => x.Active && x.CustomKey == "Semi-Annually" && x.SortOrder != 300);
                entity.SortOrder = 300;
                entity.InitialBonusBillingPeriods = 0;
                entity.RepeatableBillingPeriods = 6;
            }
            else if (!context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Semi-Annually"))
            {
                context.RepeatTypes.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "Semi-Annually",
                    Name = "Semi-Annually",
                    DisplayName = "Semi-Annually",
                    InitialBonusBillingPeriods = 0,
                    RepeatableBillingPeriods = 6,
                    SortOrder = 300,
                });
            }
            if (context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Annually" && x.SortOrder != 400))
            {
                var entity = context.RepeatTypes.First(x => x.Active && x.CustomKey == "Annually" && x.SortOrder != 400);
                entity.SortOrder = 400;
                entity.InitialBonusBillingPeriods = 0;
                entity.RepeatableBillingPeriods = 12;
            }
            else if (!context.RepeatTypes.Any(x => x.Active && x.CustomKey == "Annually"))
            {
                context.RepeatTypes.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "Annually",
                    Name = "Annually",
                    DisplayName = "Annually",
                    InitialBonusBillingPeriods = 0,
                    RepeatableBillingPeriods = 12,
                    SortOrder = 400,
                });
            }
            context.SaveUnitOfWork(true);
        }

        private void AddSubscriptionStatuses()
        {
            CreateAndAddStatusEntity<SubscriptionStatus>(context, key: "Current", name: "Current", displayName: "Current", sortOrder: 0);
            CreateAndAddStatusEntity<SubscriptionStatus>(context, key: "Expired", name: "Expired", displayName: "Expired", sortOrder: 1);
            CreateAndAddStatusEntity<SubscriptionStatus>(context, key: "Suspended", name: "Suspended", displayName: "Suspended", sortOrder: 2);
            CreateAndAddStatusEntity<SubscriptionStatus>(context, key: "Cancelled", name: "Cancelled", displayName: "Cancelled", sortOrder: 3);
            context.SaveUnitOfWork(true);
        }

        private void AddMemberships()
        {
            if (context.Memberships.Any(x => x.CustomKey == "SiteMembership"))
            {
                return;
            }
            context.Memberships.Add(new()
            {
                Active = true,
                CreatedDate = CreatedDate,
                CustomKey = "SiteMembership",
                Name = "Site Membership",
                SortOrder = 0,
                MembershipLevels = new HashSet<MembershipLevel>
                {
                    new()
                    {
                        Active = true,
                        CreatedDate = CreatedDate,
                        CustomKey = "SiteMembership|Bronze",
                        SortOrder = 10,
                        Name = "Bronze",
                        RolesApplied = "Bronze Site Membership",
                    },
                    new()
                    {
                        Active = true,
                        CreatedDate = CreatedDate,
                        CustomKey = "SiteMembership|Gold",
                        SortOrder = 20,
                        Name = "Gold",
                        RolesApplied = "Gold Site Membership",
                    },
                    new()
                    {
                        Active = true,
                        CreatedDate = CreatedDate,
                        CustomKey = "SiteMembership|Platinum",
                        SortOrder = 30,
                        Name = "Platinum",
                        RolesApplied = "Platinum Site Membership",
                    },
                },
                MembershipRepeatTypes = new HashSet<MembershipRepeatType>
                {
                    new()
                    {
                        Active = true,
                        CreatedDate = CreatedDate,
                        CustomKey = "SiteMembership|Monthly",
                        SlaveID = context.RepeatTypes.Single(x => x.Name == "Monthly").ID,
                    },
                    new()
                    {
                        Active = true,
                        CreatedDate = CreatedDate,
                        CustomKey = "SiteMembership|Quarterly",
                        SlaveID = context.RepeatTypes.Single(x => x.Name == "Quarterly").ID,
                    },
                    new()
                    {
                        Active = true,
                        CreatedDate = CreatedDate,
                        CustomKey = "SiteMembership|Semi-Annually",
                        SlaveID = context.RepeatTypes.Single(x => x.Name == "Semi-Annually").ID,
                    },
                    new()
                    {
                        Active = true,
                        CreatedDate = CreatedDate,
                        CustomKey = "SiteMembership|Annually",
                        SlaveID = context.RepeatTypes.Single(x => x.Name == "Annually").ID,
                    },
                },
            });
            context.SaveUnitOfWork(true);
        }

        private void AddSubscriptionTypes()
        {
            CreateAndAddTypeEntity<SubscriptionType>(context, sortOrder: 0, key: "Subscription", name: "Subscription", displayName: "Subscription");
            CreateAndAddTypeEntity<SubscriptionType>(context, sortOrder: 1, key: "Membership", name: "Membership", displayName: "Membership");
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Products
        private void AddFutureImportStatuses()
        {
            CreateAndAddStatusEntity<FutureImportStatus>(context, key: "Pending", name: "Pending", displayName: "Pending", sortOrder: 0);
            CreateAndAddStatusEntity<FutureImportStatus>(context, key: "Retrying", name: "Import Failed - Retrying", displayName: "Import Failed - Retrying", sortOrder: 1);
            CreateAndAddStatusEntity<FutureImportStatus>(context, key: "Retries Failed", name: "Import Failed - Retries Failed", displayName: "Import Failed - Retries Failed", sortOrder: 2);
            CreateAndAddStatusEntity<FutureImportStatus>(context, key: "Sent", name: "Sent", displayName: "Sent", sortOrder: 3);
            CreateAndAddStatusEntity<FutureImportStatus>(context, key: "Cancelled", name: "Cancelled", displayName: "Cancelled", sortOrder: 4);
            context.SaveUnitOfWork(true);
        }

        private void AddProductImageTypes()
        {
            CreateAndAddTypeEntity<ProductImageType>(context, key: "PRODUCT-IMAGE", name: "Product Image", displayName: "Product Image", sortOrder: 0);
            CreateAndAddTypeEntity<ProductImageType>(context, key: "PRODUCT-DRAWING", name: "Product Drawing", displayName: "Product Drawing", sortOrder: 1);
            CreateAndAddTypeEntity<ProductImageType>(context, key: "PRODUCT-SWATCH", name: "Product Swatch", displayName: "Product Swatch", sortOrder: 2);
        }

        private void AddProductAssociationTypes()
        {
            CreateAndAddTypeEntity<ProductAssociationType>(context, key: "RELATED-PRODUCT", name: "Related Product", displayName: "Related Product", sortOrder: 0);
            CreateAndAddTypeEntity<ProductAssociationType>(context, key: "KIT-COMPONENT", name: "Kit Component", displayName: "Kit Component", sortOrder: 1);
            CreateAndAddTypeEntity<ProductAssociationType>(context, key: "VARIANT-OF-MASTER", name: "Variant of Master", displayName: "Variant of Master", sortOrder: 2);
            context.SaveUnitOfWork(true);
        }

        private void AddProductDownloadTypes()
        {
            CreateAndAddTypeEntity<ProductDownloadType>(context, key: "Brochure", name: "Brochure", displayName: null, sortOrder: 00);
            CreateAndAddTypeEntity<ProductDownloadType>(context, key: "Disk ISO", name: "Disk ISO", displayName: null, sortOrder: 01);
            CreateAndAddTypeEntity<ProductDownloadType>(context, key: "Other",    name: "Other",    displayName: null, sortOrder: 99);
            context.SaveUnitOfWork(true);
        }

        private void AddProductTypes()
        {
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 0, key: "GENERAL", name: "General", displayName: "General");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 1, key: "KIT", name: "Kit", displayName: "Kit");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 2, key: "VARIANT-MASTER", name: "Variant Master", displayName: "Variant Master");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 2, key: "VARIANT", name: "Variant", displayName: "Variant");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 2, key: "VARIANT-KIT", name: "Variant Kit", displayName: "Variant Kit");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 3, key: "MEMBERSHIP", name: "Membership", displayName: "Membership");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 3, key: "SUBSCRIPTION", name: "Subscription", displayName: "Subscription");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 4, key: "AD-ZONE-ACCESS", name: "Ad Zone Access", displayName: "Ad Zone Access");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 5, key: "FREE-SAMPLE", name: "Free Sample", displayName: "Free Sample");
            CreateAndAddTypeEntity<ProductType>(context, sortOrder: 6, key: "PAID-SAMPLE", name: "Paid Sample", displayName: "Paid Sample");
            context.SaveUnitOfWork(true);
        }

        private void AddProductStatuses()
        {
            CreateAndAddStatusEntity<ProductStatus>(context, key: "NORMAL", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Purchasing
        private void AddPurchaseOrderStatuses()
        {
            CreateAndAddStatusEntity<PurchaseOrderStatus>(context, sortOrder: 0, key: "Submitted", name: "Submitted", displayName: "Submitted");
            CreateAndAddStatusEntity<PurchaseOrderStatus>(context, sortOrder: 1, key: "In Progress", name: "In Progress", displayName: "In Progress");
            CreateAndAddStatusEntity<PurchaseOrderStatus>(context, sortOrder: 2, key: "Fulfilled", name: "Fulfilled", displayName: "Fulfilled");
            context.SaveUnitOfWork(true);
        }

        private void AddPurchaseOrderStates()
        {
            CreateAndAddStateEntity<PurchaseOrderState>(context, sortOrder: 0, key: "WORK", name: "Work", displayName: "Work");
            CreateAndAddStateEntity<PurchaseOrderState>(context, sortOrder: 1, key: "HISTORY", name: "History", displayName: "History");
            context.SaveUnitOfWork(true);
        }

        private void AddPurchaseOrderTypes()
        {
            CreateAndAddTypeEntity<PurchaseOrderType>(context, sortOrder: 0, key: "General", name: "General", displayName: "General");
            CreateAndAddTypeEntity<PurchaseOrderType>(context, sortOrder: 1, key: "Drop-Ship", name: "Drop-Ship", displayName: "Drop-Ship");
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Quoting
        private void AddSalesQuoteStatuses()
        {
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 0, key: "Submitted",  name: "Submitted",  displayName: "Submitted",  translationKey: "ui.storefront.SalesQuoteStatuses.Submitted");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 1, key: "In Process", name: "In Process", displayName: "In Process", translationKey: "ui.storefront.SalesQuoteStatuses.InProcess");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 2, key: "Processed",  name: "Processed",  displayName: "Processed",  translationKey: "ui.storefront.SalesQuoteStatuses.Processed");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 3, key: "Approved",   name: "Approved",   displayName: "Approved",   translationKey: "ui.storefront.SalesQuoteStatuses.Approved");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 4, key: "Confirmed",  name: "Confirmed",  displayName: "Confirmed",  translationKey: "ui.storefront.SalesQuoteStatuses.Confirmed");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 5, key: "Denied",     name: "Denied",     displayName: "Denied",     translationKey: "ui.storefront.SalesQuoteStatuses.Denied");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 6, key: "Expired",    name: "Expired",    displayName: "Expired",    translationKey: "ui.storefront.SalesQuoteStatuses.Expired");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 7, key: "Rejected",   name: "Rejected",   displayName: "Rejected",   translationKey: "ui.storefront.SalesQuoteStatuses.Rejected");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 8, key: "Void",       name: "Void",       displayName: "Void",       translationKey: "ui.storefront.SalesQuoteStatuses.Void");
            CreateAndAddStatusEntity<SalesQuoteStatus>(context, sortOrder: 8, key: "Confirmed", name: "Confirmed",   displayName: "Confirmed",  translationKey: "ui.storefront.SalesQuoteStatuses.Confirmed");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesQuoteStates()
        {
            CreateAndAddStateEntity<SalesQuoteState>(context, sortOrder: 0, key: "WORK", name: "Work", displayName: "Work");
            CreateAndAddStateEntity<SalesQuoteState>(context, sortOrder: 1, key: "HISTORY", name: "History", displayName: "History");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesQuoteTypes()
        {
            CreateAndAddTypeEntity<SalesQuoteType>(context, sortOrder: 0, key: "General", name: "General", displayName: "General");
            context.SaveUnitOfWork(true);
        }

        private void AddDefaultSalesQuoteSettings()
        {
            if (context.SettingGroups.Any(x => x.Name == "SalesQuotes"))
            {
                return;
            }
            var settingGroup = new SettingGroup
            {
                CreatedDate = CreatedDate,
                CustomKey = "SalesQuotes",
                Active = true,
                DisplayName = "SalesQuote Settings",
                Name = "SalesQuotes",
            };
            if (!context.Settings.Any(x => x.CustomKey == "SalesQuoteDaysToExpire"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "SalesQuoteDaysToExpire",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "DaysToExpire",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "DaysToExpire",
                    },
                    Value = "30",
                });
            }
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Reporting
        ////private void AddReportTypes()
        ////{
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.SalesOrderListReport", "Clarity.Ecommerce.Models.Reports.SalesOrderListReport", "Clarity.Ecommerce.Models.Reports.SalesOrderListReport");
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.OrdersByCustomerReport", "Clarity.Ecommerce.Models.Reports.OrdersByCustomerReport", "Clarity.Ecommerce.Models.Reports.OrdersByCustomerReport");
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.OrdersByProductReport", "Clarity.Ecommerce.Models.Reports.OrdersByProductReport", "Clarity.Ecommerce.Models.Reports.OrdersByProductReport");
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.OrdersByCategoryReport", "Clarity.Ecommerce.Models.Reports.OrdersByCategoryReport", "Clarity.Ecommerce.Models.Reports.OrdersByCategoryReport");
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.OrdersByOrderedItemReport", "Clarity.Ecommerce.Models.Reports.OrdersByOrderedItemReport", "Clarity.Ecommerce.Models.Reports.OrdersByOrderedItemReport");
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.OrdersByVendorReport", "Clarity.Ecommerce.Models.Reports.OrdersByVendorReport", "Clarity.Ecommerce.Models.Reports.OrdersByVendorReport");
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.OrdersByAgingSoldProductReport", "Clarity.Ecommerce.Models.Reports.OrdersByAgingSoldProductReport", "Clarity.Ecommerce.Models.Reports.OrdersByAgingSoldProductReport");
        ////    CreateAndAddTypeEntity<ReportType>(context, "Clarity.Ecommerce.Models.Reports.OrdersByAgingUnsoldProductReport", "Clarity.Ecommerce.Models.Reports.OrdersByAgingUnsoldProductReport", "Clarity.Ecommerce.Models.Reports.OrdersByAgingUnsoldProductReport");
        ////    context.SaveUnitOfWork(true);
        ////}
        #endregion

        #region Returning
        private void AddSalesReturnStatuses()
        {
            CreateAndAddStatusEntity<SalesReturnStatus>(context, sortOrder: 00, key: "Pending",      name: "Pending",      displayName: "Pending",      translationKey: "ui.storefront.SalesReturnStatuses.Pending");
            CreateAndAddStatusEntity<SalesReturnStatus>(context, sortOrder: 10, key: "Confirmed",    name: "Confirmed",    displayName: "Confirmed",    translationKey: "ui.storefront.SalesReturnStatuses.Confirmed");
            CreateAndAddStatusEntity<SalesReturnStatus>(context, sortOrder: 20, key: "Shipped",      name: "Shipped",      displayName: "Shipped",      translationKey: "ui.storefront.SalesReturnStatuses.Shipped");
            CreateAndAddStatusEntity<SalesReturnStatus>(context, sortOrder: 30, key: "Payment Sent", name: "Payment Sent", displayName: "Payment Sent", translationKey: "ui.storefront.SalesReturnStatuses.PaymentSent");
            CreateAndAddStatusEntity<SalesReturnStatus>(context, sortOrder: 97, key: "Rejected",     name: "Rejected",     displayName: "Rejected",     translationKey: "ui.storefront.SalesReturnStatuses.Rejected");
            CreateAndAddStatusEntity<SalesReturnStatus>(context, sortOrder: 98, key: "Completed",    name: "Completed",    displayName: "Completed",    translationKey: "ui.storefront.SalesReturnStatuses.Completed");
            CreateAndAddStatusEntity<SalesReturnStatus>(context, sortOrder: 99, key: "Void",         name: "Void",         displayName: "Void",         translationKey: "ui.storefront.SalesReturnStatuses.Void");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesReturnStates()
        {
            CreateAndAddStateEntity<SalesReturnState>(context, sortOrder: 0, key: "WORK", name: "Work", displayName: "Work");
            CreateAndAddStateEntity<SalesReturnState>(context, sortOrder: 1, key: "HISTORY", name: "History", displayName: "History");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesReturnTypes()
        {
            CreateAndAddTypeEntity<SalesReturnType>(context, sortOrder: 0, key: "Web", name: "Web", displayName: "Web");
            CreateAndAddTypeEntity<SalesReturnType>(context, sortOrder: 1, key: "Phone", name: "Phone", displayName: "Phone");
            CreateAndAddTypeEntity<SalesReturnType>(context, sortOrder: 2, key: "On Site", name: "On Site", displayName: "On Site");
            CreateAndAddTypeEntity<SalesReturnType>(context, sortOrder: 3, key: "Sales Return Child", name: "Sales Return Child", displayName: "Sales Return Child");
            context.SaveUnitOfWork(true);
        }

        private void AddSalesReturnReasons()
        {
            CreateAndAddTypeEntity(context.SalesReturnReasons, key: "Bought by mistake", name: "Bought by mistake", displayName: "Bought by mistake", isRestockingFeeApplicable: true);
            CreateAndAddTypeEntity(context.SalesReturnReasons, key: "Better price available", name: "Better price available", displayName: "Better price available", isRestockingFeeApplicable: true);
            CreateAndAddTypeEntity(context.SalesReturnReasons, key: "Product used by customer", name: "Product used by customer", displayName: "Product used by customer", isRestockingFeeApplicable: true);
            CreateAndAddTypeEntity(context.SalesReturnReasons, key: "Product and shipping box both damage", name: "Product and shipping box both damage", displayName: "Product and shipping box both damage", isRestockingFeeApplicable: false);
            CreateAndAddTypeEntity(context.SalesReturnReasons, key: "Missing parts or accessories", name: "Missing parts or accessories", displayName: "Missing parts or accessories", isRestockingFeeApplicable: false);
            CreateAndAddTypeEntity(context.SalesReturnReasons, key: "Wrong item was sent", name: "Wrong item was sent", displayName: "Wrong item was sent", isRestockingFeeApplicable: false);
            CreateAndAddTypeEntity(context.SalesReturnReasons, key: "Item defective or does not work", name: "Item defective or does not work", displayName: "Item defective or does not work", isRestockingFeeApplicable: false);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Reviews
        private void AddReviewTypes()
        {
            CreateAndAddTypeEntity<ReviewType>(context, sortOrder: 0, key: "Overall", name: "Overall", displayName: "Overall");
            CreateAndAddTypeEntity<ReviewType>(context, sortOrder: 1, key: "Category", name: "Category", displayName: "Category");
            CreateAndAddTypeEntity<ReviewType>(context, sortOrder: 2, key: "Manufacturer", name: "Manufacturer", displayName: "Manufacturer");
            CreateAndAddTypeEntity<ReviewType>(context, sortOrder: 3, key: "Product", name: "Product", displayName: "Product");
            CreateAndAddTypeEntity<ReviewType>(context, sortOrder: 4, key: "Store", name: "Store", displayName: "Store");
            CreateAndAddTypeEntity<ReviewType>(context, sortOrder: 5, key: "User", name: "User", displayName: "User");
            CreateAndAddTypeEntity<ReviewType>(context, sortOrder: 6, key: "Vendor", name: "Vendor", displayName: "Vendor");
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Sales
        private void AddSalesItemTargetTypes()
        {
            CreateAndAddTypeEntity<SalesItemTargetType>(context, sortOrder: 0, key: "ShipToHome", name: "Ship to Home", displayName: "Ship to Home");
            CreateAndAddTypeEntity<SalesItemTargetType>(context, sortOrder: 1, key: "ShipToStore", name: "Ship to Store", displayName: "Ship to Store");
            CreateAndAddTypeEntity<SalesItemTargetType>(context, sortOrder: 2, key: "PickupInStore", name: "Pickup in Store", displayName: "Pickup in Store");
            CreateAndAddTypeEntity<SalesItemTargetType>(context, sortOrder: 3, key: "ShipToWarehouse", name: "Ship to Warehouse", displayName: "Ship to Warehouse");
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Sampling
        private void AddSampleRequestStatuses()
        {
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 00, key: "Pending",                  name: "Pending",                  displayName: "Pending",                  translationKey: "ui.storefront.SalesOrderStatuses.Pending");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 05, key: "On Hold",                  name: "On Hold",                  displayName: "On Hold",                  translationKey: "ui.storefront.SalesOrderStatuses.OnHold");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 10, key: "Confirmed",                name: "Confirmed",                displayName: "Confirmed",                translationKey: "ui.storefront.SalesOrderStatuses.Confirmed");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 11, key: "Backordered",              name: "Backordered",              displayName: "Backordered",              translationKey: "ui.storefront.SalesOrderStatuses.Backordered");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 12, key: "Split",                    name: "Split",                    displayName: "Split",                    translationKey: "ui.storefront.SalesOrderStatuses.Split"); // Closed
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 20, key: "Partial Payment Received", name: "Partial Payment Received", displayName: "Partial Payment Received", translationKey: "ui.storefront.SalesOrderStatuses.PartialPaymentReceived");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 21, key: "Full Payment Received",    name: "Full Payment Received",    displayName: "Full Payment Received",    translationKey: "ui.storefront.SalesOrderStatuses.FullPaymentReceived");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 30, key: "Processing",               name: "Processing",               displayName: "Processing",               translationKey: "ui.storefront.SalesOrderStatuses.Processing"); // Create Pick ticket
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 40, key: "Shipped",                  name: "Shipped",                  displayName: "Shipped",                  translationKey: "ui.storefront.SalesOrderStatuses.Shipped");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 41, key: "Shipped from Vendor",      name: "Shipped from Vendor",      displayName: "Shipped from Vendor",      translationKey: "ui.storefront.SalesOrderStatuses.ShippedFromVendor"); // Drop Ship
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 42, key: "Ready for Pickup",         name: "Ready for Pickup",         displayName: "Ready for Pickup",         translationKey: "ui.storefront.SalesOrderStatuses.ReadyForPickup");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 98, key: "Completed",                name: "Completed",                displayName: "Completed",                translationKey: "ui.storefront.SalesOrderStatuses.Completed");
            CreateAndAddStatusEntity<SampleRequestStatus>(context, sortOrder: 99, key: "Void",                     name: "Void",                     displayName: "Void",                     translationKey: "ui.storefront.SalesOrderStatuses.Void");
            context.SaveUnitOfWork(true);
        }

        private void AddSampleRequestStates()
        {
            CreateAndAddStateEntity<SampleRequestState>(context, sortOrder: 0, key: "WORK", name: "Work", displayName: "Work");
            CreateAndAddStateEntity<SampleRequestState>(context, sortOrder: 1, key: "HISTORY", name: "History", displayName: "History");
            context.SaveUnitOfWork(true);
        }

        private void AddSampleRequestTypes()
        {
            CreateAndAddTypeEntity<SampleRequestType>(context, sortOrder: 0, key: "Web", name: "Web", displayName: "Web");
            CreateAndAddTypeEntity<SampleRequestType>(context, sortOrder: 1, key: "Phone", name: "Phone", displayName: "Phone");
            CreateAndAddTypeEntity<SampleRequestType>(context, sortOrder: 2, key: "On Site", name: "On Site", displayName: "On Site");
            CreateAndAddTypeEntity<SampleRequestType>(context, sortOrder: 3, key: "Sales Order Child", name: "Sales Order Child", displayName: "Sales Order Child");
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Shipping
        private void AddPackages()
        {
            if (context.Packages.Any())
            {
                return;
            }
            context.Packages.Add(new() { CustomKey = "Default",  Name = "Default",  Width = 9, Depth = 12, Height = 12, Weight = 1, IsCustom = false, CreatedDate = CreatedDate, Active = true, WeightUnitOfMeasure = "lbs", WidthUnitOfMeasure = "in", DepthUnitOfMeasure = "in", HeightUnitOfMeasure = "in", DimensionalWeight = 1, DimensionalWeightUnitOfMeasure = "lbs", TypeID = 1, JsonAttributes = "{}" });
            context.Packages.Add(new() { CustomKey = "Download", Name = "Download", Width = 0, Depth = 00, Height = 00, Weight = 0, IsCustom = false, CreatedDate = CreatedDate, Active = true, WeightUnitOfMeasure = "lbs", WidthUnitOfMeasure = "in", DepthUnitOfMeasure = "in", HeightUnitOfMeasure = "in", DimensionalWeight = 0, DimensionalWeightUnitOfMeasure = "lbs", TypeID = 4, JsonAttributes = "{}" });
            context.SaveUnitOfWork(true);
        }

        private void AddPackageTypes()
        {
            CreateAndAddTypeEntity<PackageType>(context, sortOrder: 0, key: "Package", name: "Package", displayName: "Package");
            CreateAndAddTypeEntity<PackageType>(context, sortOrder: 1, key: "Master Pack", name: "Master Pack", displayName: "Master Pack");
            CreateAndAddTypeEntity<PackageType>(context, sortOrder: 2, key: "Pallet", name: "Pallet", displayName: "Pallet");
            CreateAndAddTypeEntity<PackageType>(context, sortOrder: 2, key: "Non-Physical", name: "Non-Physical", displayName: "Non-Physical");
            context.SaveUnitOfWork(true);
        }

        private void AddShipCarriers()
        {
            if (context.ShipCarriers.Any())
            {
                return;
            }
            var upsCarrier = context.ShipCarriers.Add(new()
            {
                Name = "UPS",
                CustomKey = "UPS",
                IsInbound = true,
                IsOutbound = true,
                CreatedDate = CreatedDate,
                Active = true,
                JsonAttributes = "{}",
            });
            var fedExCarrier = context.ShipCarriers.Add(new()
            {
                Name = "FedEx",
                CustomKey = "FedEx",
                IsInbound = true,
                IsOutbound = true,
                CreatedDate = CreatedDate,
                Active = true,
                JsonAttributes = "{}",
            });
            context.ShipCarriers.Add(new()
            {
                Name = "USPS",
                CustomKey = "USPS",
                IsInbound = true,
                IsOutbound = true,
                CreatedDate = CreatedDate,
                Active = true,
                JsonAttributes = "{}",
            });
            context.ShipCarrierMethods.Add(new() { Active = true, CreatedDate = CreatedDate, CustomKey = "01", Name = "UPS Next Day Air", ShipCarrier = upsCarrier, JsonAttributes = "{}" });
            context.ShipCarrierMethods.Add(new() { Active = true, CreatedDate = CreatedDate, CustomKey = "02", Name = "UPS 2nd Day Air", ShipCarrier = upsCarrier, JsonAttributes = "{}" });
            context.ShipCarrierMethods.Add(new() { Active = true, CreatedDate = CreatedDate, CustomKey = "12", Name = "UPS 3 Day Select", ShipCarrier = upsCarrier, JsonAttributes = "{}" });
            context.ShipCarrierMethods.Add(new() { Active = true, CreatedDate = CreatedDate, CustomKey = "03", Name = "UPS Ground", ShipCarrier = upsCarrier, JsonAttributes = "{}" });
            context.ShipCarrierMethods.Add(new() { Active = true, CreatedDate = CreatedDate, CustomKey = "STANDARD_OVERNIGHT", Name = "FedEx Standard Overnight", ShipCarrier = fedExCarrier, JsonAttributes = "{}" });
            context.ShipCarrierMethods.Add(new() { Active = true, CreatedDate = CreatedDate, CustomKey = "FEDEX_2_DAY", Name = "FedEx 2 Day", ShipCarrier = fedExCarrier, JsonAttributes = "{}" });
            context.ShipCarrierMethods.Add(new() { Active = true, CreatedDate = CreatedDate, CustomKey = "FEDEX_GROUND", Name = "FedEx Ground", ShipCarrier = fedExCarrier, JsonAttributes = "{}" });
            context.SaveUnitOfWork(true);
        }

        private void AddShipmentStatuses()
        {
            CreateAndAddStatusEntity<ShipmentStatus>(context, sortOrder: 0, key: "Delivered", name: "Delivered", displayName: "Delivered");
            CreateAndAddStatusEntity<ShipmentStatus>(context, sortOrder: 1, key: "Billing Information Received", name: "Billing Information Received", displayName: "Billing Information Received");
            CreateAndAddStatusEntity<ShipmentStatus>(context, sortOrder: 2, key: "Arrival Scan", name: "Arrival Scan", displayName: "Arrival Scan");
            CreateAndAddStatusEntity<ShipmentStatus>(context, sortOrder: 3, key: "Departure Scan", name: "Departure Scan", displayName: "Departure Scan");
            CreateAndAddStatusEntity<ShipmentStatus>(context, sortOrder: 4, key: "Investigation Requested", name: "Investigation Requested", displayName: "Investigation Requested");
            CreateAndAddStatusEntity<ShipmentStatus>(context, sortOrder: 5, key: "Severe Weather Conditions Have Delayed Delivery", name: "Severe Weather Conditions Have Delayed Delivery", displayName: "Severe Weather Conditions Have Delayed Delivery");
            CreateAndAddStatusEntity<ShipmentStatus>(context, sortOrder: 6, key: "Reported Damaged", name: "Reported Damaged", displayName: "Reported Damaged");
            context.SaveUnitOfWork(true);
        }

        private void AddShipmentTypes()
        {
            CreateAndAddTypeEntity<ShipmentType>(context, key: "Inbound", name: "Inbound", displayName: "Inbound", sortOrder: 0);
            CreateAndAddTypeEntity<ShipmentType>(context, key: "Outbound", name: "Outbound", displayName: "Outbound", sortOrder: 1);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Shopping
        private void AddCartStatuses()
        {
            CreateAndAddStatusEntity<CartStatus>(context, sortOrder: 0, key: "New", name: "New", displayName: "New");
            CreateAndAddStatusEntity<CartStatus>(context, sortOrder: 1, key: "Abandoned", name: "Abandoned", displayName: "Abandoned");
            CreateAndAddStatusEntity<CartStatus>(context, sortOrder: 2, key: "Converted To Order", name: "Converted To Order", displayName: "Converted To Order");
            context.SaveUnitOfWork(true);
        }

        private void AddCartStates()
        {
            CreateAndAddStateEntity<CartState>(context, sortOrder: 0, key: "WORK", name: "Work", displayName: "Work");
            CreateAndAddStateEntity<CartState>(context, sortOrder: 1, key: "HISTORY", name: "History", displayName: "History");
            context.SaveUnitOfWork(true);
        }

        private void AddCartTypes()
        {
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 00, key: "Cart", name: "Cart", displayName: "Cart");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 01, key: "Favorites List", name: "Favorites List", displayName: "Favorites List");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 02, key: "Wish List", name: "Wish List", displayName: "Wish List");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 03, key: "Request For Quote", name: "Request For Quote", displayName: "Request For Quote");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 04, key: "Bookmark", name: "Bookmark", displayName: "Bookmark");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 05, key: "Watch List", name: "Watch List", displayName: "Watch List");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 06, key: "Request For Sample", name: "Request For Sample", displayName: "Request For Sample");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 07, key: "Notify Me When In Stock", name: "Notify Me When In Stock", displayName: "Notify Me When In Stock");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 08, key: "Quote Cart", name: "Quote Cart", displayName: "Quote Cart");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 09, key: "Samples Cart", name: "Samples Cart", displayName: "Samples Cart");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 10, key: "Temp Cart", name: "Temp Cart", displayName: "Temp Cart");
            CreateAndAddTypeEntity<CartType>(context, sortOrder: 11, key: "Compare Cart", name: "Compare Cart", displayName: "Compare Cart");
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Stores
        private void AddStoreTypes()
        {
            CreateAndAddTypeEntity<StoreType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Structure
        private void AddNoteTypes()
        {
            if (context.NoteTypes.Any())
            {
                return;
            }
            context.NoteTypes.Add(new() { CustomKey = "Order Note", Name = "Order Note", DisplayName = "Order Note", IsPublic = true, IsCustomer = false, CreatedDate = CreatedDate, Active = true });
            context.NoteTypes.Add(new() { CustomKey = "Private Note", Name = "Private Note", DisplayName = "Private Note", IsPublic = false, IsCustomer = false, CreatedDate = CreatedDate, Active = true });
            context.NoteTypes.Add(new() { CustomKey = "Customer Note", Name = "Customer Note", DisplayName = "Customer Note", IsPublic = true, IsCustomer = true, CreatedDate = CreatedDate, Active = true });
            context.NoteTypes.Add(new() { CustomKey = "Sales Return Note", Name = "Sales Return Note", DisplayName = "Return Note", IsPublic = true, IsCustomer = true, CreatedDate = CreatedDate, Active = true });
            context.SaveUnitOfWork(true);
        }

        private void AddRecordVersionTypes()
        {
            CreateAndAddTypeEntity<RecordVersionType>(context, key: "Product", name: "Product", displayName: null, sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddSettingTypes()
        {
            CreateAndAddTypeEntity<SettingType>(context, key: "Synonyms", name: "Synonyms", displayName: "Synonyms", sortOrder: 1);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Taxes
        private void AddDefaultAvalaraSettings()
        {
            if (context.SettingGroups.Any(x => x.Name == "Avalara"))
            {
                return;
            }
            var settingGroup = new SettingGroup
            {
                CreatedDate = CreatedDate,
                CustomKey = "Avalara",
                Active = true,
                DisplayName = "Avalara Settings",
                Name = "Avalara",
            };
            if (!context.Settings.Any(x => x.CustomKey == "AccountNumber"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "AccountNumber",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraAccountNumber",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "AccountNumber",
                    },
                    Value = "1100110323",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "LicenseKey"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "LicenseKey",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraLicenseKey",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "LicenseKey",
                    },
                    Value = "<no key>",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "ServiceURL"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "ServiceURL",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraServiceURL",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "ServiceURL",
                    },
                    Value = "https://development.avalara.net/",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "CompanyCode"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "CompanyCode",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraCompanyCode",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "CompanyCode",
                    },
                    Value = "01",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "TaxServiceEnabled"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "TaxServiceEnabled",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraTaxServiceEnabled",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "TaxServiceEnabled",
                    },
                    Value = "false",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "AddressServiceEnabled"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "AddressServiceEnabled",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraAddressServiceEnabled",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "AddressServiceEnabled",
                    },
                    Value = "false",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "AddressServiceCountries"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "AddressServiceCountries",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraAddressServiceCountries",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "AddressServiceCountries",
                    },
                    Value = "USA",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "DocumentCommitingEnabled"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "DocumentCommitingEnabled",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraDocumentCommitingEnabled",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "DocumentCommitingEnabled",
                    },
                    Value = "true",
                });
            }
            if (!context.Settings.Any(x => x.CustomKey == "LoggingEnabled"))
            {
                context.Settings.Add(new()
                {
                    Active = true,
                    CreatedDate = CreatedDate,
                    CustomKey = "LoggingEnabled",
                    SettingGroup = settingGroup,
                    Type = new()
                    {
                        CustomKey = "AvalaraLoggingEnabled",
                        CreatedDate = CreatedDate,
                        Active = true,
                        Name = "LoggingEnabled",
                    },
                    Value = "false",
                });
            }
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Tracking
        private void AddCampaignStatuses()
        {
            CreateAndAddStatusEntity<CampaignStatus>(context, key: "Normal", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddCampaignTypes()
        {
            CreateAndAddTypeEntity<CampaignType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddEventStatuses()
        {
            CreateAndAddStatusEntity<EventStatus>(context, key: "Normal", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddEventTypes()
        {
            CreateAndAddTypeEntity<EventType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            CreateAndAddTypeEntity<EventType>(context, key: "Product Catalog Search", name: "Product Catalog Search", displayName: "Product Catalog Search", sortOrder: 1);
            context.SaveUnitOfWork(true);
        }

        private void AddPageViewStatuses()
        {
            CreateAndAddStatusEntity<PageViewStatus>(context, key: "Normal", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddPageViewTypes()
        {
            CreateAndAddTypeEntity<PageViewType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }

        private void AddVisitStatuses()
        {
            CreateAndAddStatusEntity<VisitStatus>(context, key: "Normal", name: "Normal", displayName: "Normal", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion

        #region Vendors
        private void AddVendorTypes()
        {
            CreateAndAddTypeEntity<VendorType>(context, key: "General", name: "General", displayName: "General", sortOrder: 0);
            context.SaveUnitOfWork(true);
        }
        #endregion
    }
}
