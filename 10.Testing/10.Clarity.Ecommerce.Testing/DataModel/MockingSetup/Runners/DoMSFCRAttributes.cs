// <copyright file="DoMockingSetupForContextRunnerAttributes.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner attributes class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerAttributesAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Attribute Groups
            if (DoAll || DoAttributes || DoAttributeGroupTable)
            {
                var index = 0;
                RawAttributeGroups = new()
                {
                    await CreateADummyAttributeGroupAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetAttributeGroupsAsync(mockContext, RawAttributeGroups).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Attribute Tabs
            if (DoAll || DoAttributes || DoAttributeTabTable)
            {
                var index = 0;
                RawAttributeTabs = new()
                {
                    await CreateADummyAttributeTabAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetAttributeTabsAsync(mockContext, RawAttributeTabs).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Attribute Types
            if (DoAll || DoAttributes || DoAttributeTypeTable)
            {
                var index = 0;
                RawAttributeTypes = new()
                {
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "GENERAL", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "SALES-ORDER", name: "Sales Order", desc: "desc", sortOrder: 5, displayName: "Sales Order").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "CART", name: "Cart", desc: "desc", sortOrder: 6, displayName: "Cart").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "PRODUCT", name: "Product", desc: "desc", sortOrder: 2, displayName: "Product").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "CART-ITEM", name: "Cart Item", desc: "desc", sortOrder: 6, displayName: "Cart Item").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "CATEGORY", name: "Category", desc: "desc", sortOrder: 3, displayName: "Category").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "ACCOUNT", name: "Account", desc: "desc", sortOrder: 4, displayName: "Account").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "SALES-INVOICE", name: "Sales Invoice", desc: "desc", sortOrder: 7, displayName: "Sales Invoice").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "SALES-INVOICE-ITEM", name: "Sales Invoice Item", desc: "desc", sortOrder: 7, displayName: "Sales Invoice Item").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "SALES-QUOTE", name: "Sales Quote", desc: "desc", sortOrder: 8, displayName: "Sales Quote").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "SALES-QUOTE-ITEM", name: "Sales Quote Item", desc: "desc", sortOrder: 8, displayName: "Sales Quote Item").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "PURCHASE-ORDER", name: "Purchase Order", desc: "desc", sortOrder: 9, displayName: "Purchase Order").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "PURCHASE-ORDER-ITEM", name: "Purchase Order Item", desc: "desc", sortOrder: 9, displayName: "Purchase Order Item").ConfigureAwait(false),
                    await CreateADummyAttributeTypeAsync(id: ++index, key: "SALES-ORDER-ITEM", name: "Sales Order Item", desc: "desc", sortOrder: 5, displayName: "Sales Order Item").ConfigureAwait(false),
                };
                await InitializeMockSetAttributeTypesAsync(mockContext, RawAttributeTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: General Attributes
            if (DoAll || DoAttributes || DoGeneralAttributeTable)
            {
                RawGeneralAttributes = new()
                {
                    await CreateADummyGeneralAttributeAsync(id: 01, key: "Size", name: "Size", desc: "desc", displayName: "Size", isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 02, key: "Color", name: "Color", desc: "desc", isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 03, key: "CLASSKEY", name: "ClassKey", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 04, key: "CurrencyDecimalPlaces", name: "CurrencyDecimalPlaces", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 05, key: "WarrantyDays", name: "WarrantyDays", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 06, key: "UserCategoryList1", name: "UserCategoryList1", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 07, key: "UserCategoryList2", name: "UserCategoryList2", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 08, key: "UserCategoryList3", name: "UserCategoryList3", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 09, key: "UserCategoryList4", name: "UserCategoryList4", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 10, key: "UserCategoryList5", name: "UserCategoryList5", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 11, key: "UserCategoryList6", name: "UserCategoryList6", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 12, key: "Shipping & Returns Info", name: "Shipping & Returns Info", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 13, key: "Purchasable By Individuals", name: "Purchasable By Individuals", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 14, key: "Purchasable by Organizations", name: "Purchasable by Organizations", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 15, key: "ITMGEDSC", name: "ITMGEDSC", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 16, key: "LOCNCODE", name: "LOCNCODE", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 17, key: "ABCCODE", name: "ABCCODE", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 18, key: "ITMTSHID", name: "ITMTSHID", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 19, key: "UofMSchedule", name: "UofMSchedule", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 20, key: "PurchasingUofM", name: "PurchasingUofM", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 21, key: "TrainingCourse", name: "TrainingCourse", desc: "desc", typeID: 4).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 22, key: "DigitalDownload", name: "DigitalDownload", desc: "desc", typeID: 4, isFilter: true).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 23, key: "MIN-PURCHASE", name: "Category Minimum Purchase Quantity: Amount", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 24, key: "MIN-PURCHASE-AFTER-MET", name: "Category Minimum Purchase Quantity: If Minimum Previously Met", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 25, key: "MIN-PURCHASE-BUFFER-SKU", name: "Category Minimum Purchase Quantity: Buffer SKU", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 26, key: "MIN-PURCHASE-BUFFER-CATEGORY", name: "Category Minimum Purchase Quantity: Buffer Category SEO URL", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 27, key: "MIN-PURCHASE-WARNING-MESSAGE", name: "Category Minimum Purchase Quantity: Restriction Warning Message Format", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 28, key: "MIN-PURCHASE-DOLLAR", name: "Category Minimum Purchase Dollar: Amount", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 29, key: "MIN-PURCHASE-DOLLAR-AFTER-MET", name: "Category Minimum Purchase Dollar: If Minimum Previously Met", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 30, key: "MIN-PURCHASE-DOLLAR-BUFFER-SKU", name: "Category Minimum Purchase Dollar: Buffer SKU", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 31, key: "MIN-PURCHASE-DOLLAR-BUFFER-CATEGORY", name: "Category Minimum Purchase Dollar: Buffer Category SEO URL", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 32, key: "MIN-PURCHASE-DOLLAR-WARNING-MESSAGE", name: "Category Minimum Purchase Dollar: Restriction Warning Message Format", desc: "desc", typeID: 6).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 33, key: "User Defined 1 (GP)", name: "User Defined 1 (GP)", desc: "desc", typeID: 7).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 34, key: "User Defined 2 (GP)", name: "User Defined 2 (GP)", desc: "desc", typeID: 7).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 35, key: "USERTAB01", name: "Market Source 1", desc: "desc", typeID: 2).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 36, key: "USERTAB05", name: "Store ID", desc: "desc", typeID: 2).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 37, key: "INET7", name: "Order Email", desc: "desc", typeID: 2).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 38, key: "USERTAB03", name: "MS License Number", desc: "desc", typeID: 2).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 39, key: "USERTAB02", name: "Market Source 2/Coupon Code", desc: "desc", typeID: 2).ConfigureAwait(false),
                    await CreateADummyGeneralAttributeAsync(id: 40, key: "USERTAB04", name: "MS Authorization Number", desc: "desc", typeID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetGeneralAttributesAsync(mockContext, RawGeneralAttributes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: General Attribute Predefined Options
            if (DoAll || DoAttributes || DoGeneralAttributePredefinedOptionTable)
            {
                var index = 0;
                RawGeneralAttributePredefinedOptions = new()
                {
                    await CreateADummyGeneralAttributePredefinedOptionAsync(id: ++index, key: "Size|XS",   attributeID: 01, sortOrder: 00, value: "XS").ConfigureAwait(false),
                    await CreateADummyGeneralAttributePredefinedOptionAsync(id: ++index, key: "Size|SM",   attributeID: 01, sortOrder: 01, value: "SM").ConfigureAwait(false),
                    await CreateADummyGeneralAttributePredefinedOptionAsync(id: ++index, key: "Size|MD",   attributeID: 01, sortOrder: 02, value: "MD").ConfigureAwait(false),
                    await CreateADummyGeneralAttributePredefinedOptionAsync(id: ++index, key: "Size|LG",   attributeID: 01, sortOrder: 03, value: "LG").ConfigureAwait(false),
                    await CreateADummyGeneralAttributePredefinedOptionAsync(id: ++index, key: "Size|XL",   attributeID: 01, sortOrder: 04, value: "XL").ConfigureAwait(false),
                    await CreateADummyGeneralAttributePredefinedOptionAsync(id: ++index, key: "Size|XXL",  attributeID: 01, sortOrder: 05, value: "XXL").ConfigureAwait(false),
                    await CreateADummyGeneralAttributePredefinedOptionAsync(id: ++index, key: "Size|XXXL", attributeID: 01, sortOrder: 06, value: "XXXL").ConfigureAwait(false),
                };
                await InitializeMockSetGeneralAttributePredefinedOptionsAsync(mockContext, RawGeneralAttributePredefinedOptions).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
