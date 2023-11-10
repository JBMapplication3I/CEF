// <copyright file="DoMockingSetupForContextRunnerDiscounts.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner discounts class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerDiscountsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Applied Cart Discounts
            if (DoAll || DoDiscounts || DoAppliedCartDiscountTable)
            {
                var index = 0;
                RawAppliedCartDiscounts = new()
                {
                    await CreateADummyAppliedCartDiscountAsync(id: ++index, key: "APPLIED-CART-DISCOUNT-1",           discountTotal: 1.25m, applicationsUsed: 1, masterID: 01, slaveID: 01).ConfigureAwait(false),
                    await CreateADummyAppliedCartDiscountAsync(id: ++index, key: "APPLIED-CART-DISCOUNT-EMPTY-CART",  discountTotal: 1.25m, applicationsUsed: 1, masterID: 05, slaveID: 01).ConfigureAwait(false),
                    await CreateADummyAppliedCartDiscountAsync(id: ++index, key: "APPLIED-CART-DISCOUNT-ALREADY",     discountTotal: 1.25m, applicationsUsed: 1, masterID: 08, slaveID: 03).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedCartDiscountsAsync(mockContext, RawAppliedCartDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Cart Item Discounts
            if (DoAll || DoDiscounts || DoAppliedCartItemDiscountTable)
            {
                var index = 0;
                RawAppliedCartItemDiscounts = new()
                {
                    await CreateADummyAppliedCartItemDiscountAsync(id: ++index, key: "APPLIED-CART-ITEM-DISCOUNT-1",           discountTotal: 1.25m, applicationsUsed: 1, masterID: 01, slaveID: 01).ConfigureAwait(false),
                    await CreateADummyAppliedCartItemDiscountAsync(id: ++index, key: "APPLIED-CART-ITEM-DISCOUNT-ALREADY-ITM", discountTotal: 1.25m, applicationsUsed: 1, masterID: 81, slaveID: 04).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedCartItemDiscountsAsync(mockContext, RawAppliedCartItemDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Purchase Order Discounts
            if (DoAll || DoDiscounts || DoAppliedPurchaseOrderDiscountTable)
            {
                RawAppliedPurchaseOrderDiscounts = new()
                {
                    await CreateADummyAppliedPurchaseOrderDiscountAsync(id: 1, key: "APPLIED-PURCHASE-ORDER-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedPurchaseOrderDiscountsAsync(mockContext, RawAppliedPurchaseOrderDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Purchase Order Item Discounts
            if (DoAll || DoDiscounts || DoAppliedPurchaseOrderItemDiscountTable)
            {
                RawAppliedPurchaseOrderItemDiscounts = new()
                {
                    await CreateADummyAppliedPurchaseOrderItemDiscountAsync(id: 1, key: "APPLIED-PURCHASE-ORDER-ITEM-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedPurchaseOrderItemDiscountsAsync(mockContext, RawAppliedPurchaseOrderItemDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Invoice Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesInvoiceDiscountTable)
            {
                RawAppliedSalesInvoiceDiscounts = new()
                {
                    await CreateADummyAppliedSalesInvoiceDiscountAsync(id: 1, key: "APPLIED-SALES-INVOICE-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesInvoiceDiscountsAsync(mockContext, RawAppliedSalesInvoiceDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Invoice Item Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesInvoiceItemDiscountTable)
            {
                RawAppliedSalesInvoiceItemDiscounts = new()
                {
                    await CreateADummyAppliedSalesInvoiceItemDiscountAsync(id: 1, key: "APPLIED-SALES-INVOICE-ITEM-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesInvoiceItemDiscountsAsync(mockContext, RawAppliedSalesInvoiceItemDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Order Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesOrderDiscountTable)
            {
                var index = 0;
                RawAppliedSalesOrderDiscounts = new()
                {
                    await CreateADummyAppliedSalesOrderDiscountAsync(id: ++index, key: "APPLIED-SALES-ORDER-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                    await CreateADummyAppliedSalesOrderDiscountAsync(id: ++index, key: "APPLIED-SALES-ORDER-DISCOUNT-2", discountTotal: 1.25m, applicationsUsed: 1, slaveID: 011).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesOrderDiscountsAsync(mockContext, RawAppliedSalesOrderDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Order Item Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesOrderItemDiscountTable)
            {
                RawAppliedSalesOrderItemDiscounts = new()
                {
                    await CreateADummyAppliedSalesOrderItemDiscountAsync(id: 1, key: "APPLIED-SALES-ORDER-ITEM-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesOrderItemDiscountsAsync(mockContext, RawAppliedSalesOrderItemDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Quote Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesQuoteDiscountTable)
            {
                RawAppliedSalesQuoteDiscounts = new()
                {
                    await CreateADummyAppliedSalesQuoteDiscountAsync(id: 1, key: "APPLIED-SALES-QUOTE-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesQuoteDiscountsAsync(mockContext, RawAppliedSalesQuoteDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Quote Item Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesQuoteItemDiscountTable)
            {
                RawAppliedSalesQuoteItemDiscounts = new()
                {
                    await CreateADummyAppliedSalesQuoteItemDiscountAsync(id: 1, key: "APPLIED-SALES-QUOTE-ITEM-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesQuoteItemDiscountsAsync(mockContext, RawAppliedSalesQuoteItemDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Return Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesReturnDiscountTable)
            {
                RawAppliedSalesReturnDiscounts = new()
                {
                    await CreateADummyAppliedSalesReturnDiscountAsync(id: 1, key: "APPLIED-SALES-RETURN-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesReturnDiscountsAsync(mockContext, RawAppliedSalesReturnDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sales Return Item Discounts
            if (DoAll || DoDiscounts || DoAppliedSalesReturnItemDiscountTable)
            {
                RawAppliedSalesReturnItemDiscounts = new()
                {
                    await CreateADummyAppliedSalesReturnItemDiscountAsync(id: 1, key: "APPLIED-SALES-RETURN-ITEM-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSalesReturnItemDiscountsAsync(mockContext, RawAppliedSalesReturnItemDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sample Request Discounts
            if (DoAll || DoDiscounts || DoAppliedSampleRequestDiscountTable)
            {
                RawAppliedSampleRequestDiscounts = new()
                {
                    await CreateADummyAppliedSampleRequestDiscountAsync(id: 1, key: "APPLIED-SAMPLE-REQUEST-ITEM-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSampleRequestDiscountsAsync(mockContext, RawAppliedSampleRequestDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Applied Sample Request Item Discounts
            if (DoAll || DoDiscounts || DoAppliedSampleRequestItemDiscountTable)
            {
                RawAppliedSampleRequestItemDiscounts = new()
                {
                    await CreateADummyAppliedSampleRequestItemDiscountAsync(id: 1, key: "APPLIED-SAMPLE-REQUEST-ITEM-DISCOUNT-1", discountTotal: 1.25m, applicationsUsed: 1).ConfigureAwait(false),
                };
                await InitializeMockSetAppliedSampleRequestItemDiscountsAsync(mockContext, RawAppliedSampleRequestItemDiscounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Accounts
            if (DoAll || DoDiscounts || DoDiscountAccountTable)
            {
                RawDiscountAccounts = new()
                {
                    await CreateADummyDiscountAccountAsync(id: 1, key: "DISCOUNT-ACCOUNT-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountAccountsAsync(mockContext, RawDiscountAccounts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Account Types
            if (DoAll || DoDiscounts || DoDiscountAccountTypeTable)
            {
                RawDiscountAccountTypes = new()
                {
                    await CreateADummyDiscountAccountTypeAsync(id: 1, key: "DISCOUNT-ACCOUNT-TYPE-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountAccountTypesAsync(mockContext, RawDiscountAccountTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Brands
            if (DoAll || DoDiscounts || DoDiscountBrandTable)
            {
                RawDiscountBrands = new()
                {
                    await CreateADummyDiscountBrandAsync(id: 1, key: "DISCOUNT-BRAND-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountBrandsAsync(mockContext, RawDiscountBrands).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Codes
            if (DoAll || DoDiscounts || DoDiscountCodeTable)
            {
                RawDiscountCodes = new()
                {
                    await CreateADummyDiscountCodeAsync(id: 001, key: "DISCOUNT-CODE-001", code: "ABCD1234",                     discountID: 001).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 002, key: "DISCOUNT-CODE-002", code: "NO-EFFECT",                    discountID: 002).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 003, key: "DISCOUNT-CODE-003", code: "ALREADY",                      discountID: 003).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 004, key: "DISCOUNT-CODE-004", code: "ALREADY-ITM",                  discountID: 004).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 005, key: "DISCOUNT-CODE-005", code: "EXCL-NOT-ADDABLE",             discountID: 005).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 006, key: "DISCOUNT-CODE-006", code: "EXCL-NOT-ADDABLE-ITM",         discountID: 006).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 007, key: "DISCOUNT-CODE-007", code: "DATE-NOT-ADDABLE-EXP",         discountID: 007).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 008, key: "DISCOUNT-CODE-008", code: "DATE-NOT-ADDABLE-FUT",         discountID: 008).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 009, key: "DISCOUNT-CODE-009", code: "NOT-ADDABLE-DEACTIVATED",      discountID: 009).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 010, key: "DISCOUNT-CODE-010", code: "NOT-ADDABLE-NO-AUTH",          discountID: 010).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 011, key: "DISCOUNT-CODE-011", code: "NOT-ADDABLE-USAGE-LIMIT-USER", discountID: 011).ConfigureAwait(false),

                    await CreateADummyDiscountCodeAsync(id: 101, key: "DISCOUNT-CODE-101", code: "$-ORDER-ADDABLE",              discountID: 101).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 102, key: "DISCOUNT-CODE-102", code: "$-ORDER-NOT-ADDABLE-GT",       discountID: 102).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 103, key: "DISCOUNT-CODE-103", code: "$-ORDER-NOT-ADDABLE-GTE",      discountID: 103).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 104, key: "DISCOUNT-CODE-104", code: "$-ORDER-NOT-ADDABLE-LT",       discountID: 104).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 105, key: "DISCOUNT-CODE-105", code: "$-ORDER-NOT-ADDABLE-LTE",      discountID: 105).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 106, key: "DISCOUNT-CODE-106", code: "$-SHIP-ADDABLE",               discountID: 106).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 107, key: "DISCOUNT-CODE-107", code: "$-SHIP-NOT-ADDABLE-GT",        discountID: 107).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 108, key: "DISCOUNT-CODE-108", code: "$-SHIP-NOT-ADDABLE-GTE",       discountID: 108).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 109, key: "DISCOUNT-CODE-109", code: "$-SHIP-NOT-ADDABLE-LT",        discountID: 109).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 110, key: "DISCOUNT-CODE-110", code: "$-SHIP-NOT-ADDABLE-LTE",       discountID: 110).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 111, key: "DISCOUNT-CODE-111", code: "$-PRODUCT-ADDABLE",            discountID: 111).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 112, key: "DISCOUNT-CODE-112", code: "$-PRODUCT-NOT-ADDABLE-GT",     discountID: 112).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 113, key: "DISCOUNT-CODE-113", code: "$-PRODUCT-NOT-ADDABLE-GTE",    discountID: 113).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 114, key: "DISCOUNT-CODE-114", code: "$-PRODUCT-NOT-ADDABLE-LT",     discountID: 114).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 115, key: "DISCOUNT-CODE-115", code: "$-PRODUCT-NOT-ADDABLE-LTE",    discountID: 115).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 116, key: "DISCOUNT-CODE-116", code: "$-BXGY-ADDABLE",               discountID: 116).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 117, key: "DISCOUNT-CODE-117", code: "$-BXGY-NOT-ADDABLE-GT",        discountID: 117).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 118, key: "DISCOUNT-CODE-118", code: "$-BXGY-NOT-ADDABLE-GTE",       discountID: 118).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 119, key: "DISCOUNT-CODE-119", code: "$-BXGY-NOT-ADDABLE-LT",        discountID: 119).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 120, key: "DISCOUNT-CODE-120", code: "$-BXGY-NOT-ADDABLE-LTE",       discountID: 120).ConfigureAwait(false),

                    await CreateADummyDiscountCodeAsync(id: 201, key: "DISCOUNT-CODE-201", code: "%-ORDER-ADDABLE",              discountID: 201).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 202, key: "DISCOUNT-CODE-202", code: "%-ORDER-NOT-ADDABLE-GT",       discountID: 202).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 203, key: "DISCOUNT-CODE-203", code: "%-ORDER-NOT-ADDABLE-GTE",      discountID: 203).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 204, key: "DISCOUNT-CODE-204", code: "%-ORDER-NOT-ADDABLE-LT",       discountID: 204).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 205, key: "DISCOUNT-CODE-205", code: "%-ORDER-NOT-ADDABLE-LTE",      discountID: 205).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 206, key: "DISCOUNT-CODE-206", code: "%-SHIP-ADDABLE",               discountID: 206).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 207, key: "DISCOUNT-CODE-207", code: "%-SHIP-NOT-ADDABLE-GT",        discountID: 207).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 208, key: "DISCOUNT-CODE-208", code: "%-SHIP-NOT-ADDABLE-GTE",       discountID: 208).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 209, key: "DISCOUNT-CODE-209", code: "%-SHIP-NOT-ADDABLE-LT",        discountID: 209).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 210, key: "DISCOUNT-CODE-210", code: "%-SHIP-NOT-ADDABLE-LTE",       discountID: 210).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 211, key: "DISCOUNT-CODE-211", code: "%-PRODUCT-ADDABLE",            discountID: 211).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 212, key: "DISCOUNT-CODE-212", code: "%-PRODUCT-NOT-ADDABLE-GT",     discountID: 212).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 213, key: "DISCOUNT-CODE-213", code: "%-PRODUCT-NOT-ADDABLE-GTE",    discountID: 213).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 214, key: "DISCOUNT-CODE-214", code: "%-PRODUCT-NOT-ADDABLE-LT",     discountID: 214).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 215, key: "DISCOUNT-CODE-215", code: "%-PRODUCT-NOT-ADDABLE-LTE",    discountID: 215).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 216, key: "DISCOUNT-CODE-216", code: "%-BXGY-ADDABLE",               discountID: 216).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 217, key: "DISCOUNT-CODE-217", code: "%-BXGY-NOT-ADDABLE-GT",        discountID: 217).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 218, key: "DISCOUNT-CODE-218", code: "%-BXGY-NOT-ADDABLE-GTE",       discountID: 218).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 219, key: "DISCOUNT-CODE-219", code: "%-BXGY-NOT-ADDABLE-LT",        discountID: 219).ConfigureAwait(false),
                    await CreateADummyDiscountCodeAsync(id: 220, key: "DISCOUNT-CODE-220", code: "%-BXGY-NOT-ADDABLE-LTE",       discountID: 220).ConfigureAwait(false),
                };
                await InitializeMockSetDiscountCodesAsync(mockContext, RawDiscountCodes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Categories
            if (DoAll || DoDiscounts || DoDiscountCategoryTable)
            {
                RawDiscountCategories = new()
                {
                    await CreateADummyDiscountCategoryAsync(id: 1, key: "DISCOUNT-CATEGORY-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountCategoriesAsync(mockContext, RawDiscountCategories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Countries
            if (DoAll || DoDiscounts || DoDiscountCountryTable)
            {
                RawDiscountCountries = new()
                {
                    await CreateADummyDiscountCountryAsync(id: 1, key: "DISCOUNT-COUNTRY-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountCountriesAsync(mockContext, RawDiscountCountries).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Franchises
            if (DoAll || DoDiscounts || DoDiscountFranchiseTable)
            {
                RawDiscountFranchises = new()
                {
                    await CreateADummyDiscountFranchiseAsync(id: 1, key: "DISCOUNT-FRANCHISE-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountFranchisesAsync(mockContext, RawDiscountFranchises).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Manufacturers
            if (DoAll || DoDiscounts || DoDiscountManufacturerTable)
            {
                RawDiscountManufacturers = new()
                {
                    await CreateADummyDiscountManufacturerAsync(id: 1, key: "DISCOUNT-MANUFACTURER-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountManufacturersAsync(mockContext, RawDiscountManufacturers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Products
            if (DoAll || DoDiscounts || DoDiscountProductTable)
            {
                RawDiscountProducts = new()
                {
                    await CreateADummyDiscountProductAsync(id: 1, key: "DISCOUNT-PRODUCT-1", slaveID: 1151).ConfigureAwait(false),
                };
                await InitializeMockSetDiscountProductsAsync(mockContext, RawDiscountProducts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Product Types
            if (DoAll || DoDiscounts || DoDiscountProductTypeTable)
            {
                RawDiscountProductTypes = new()
                {
                    await CreateADummyDiscountProductTypeAsync(id: 1, key: "DISCOUNT-PRODUCT-TYPE-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountProductTypesAsync(mockContext, RawDiscountProductTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Ship Carrier Methods
            if (DoAll || DoDiscounts || DoDiscountShipCarrierMethodTable)
            {
                RawDiscountShipCarrierMethods = new()
                {
                    await CreateADummyDiscountShipCarrierMethodAsync(id: 1, key: "DISCOUNT-SHIP-CARRIER-METHOD-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountShipCarrierMethodsAsync(mockContext, RawDiscountShipCarrierMethods).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Stores
            if (DoAll || DoDiscounts || DoDiscountStoreTable)
            {
                RawDiscountStores = new()
                {
                    await CreateADummyDiscountStoreAsync(id: 1, key: "DISCOUNT-STORE-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountStoresAsync(mockContext, RawDiscountStores).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Users
            if (DoAll || DoDiscounts || DoDiscountUserTable)
            {
                RawDiscountUsers = new()
                {
                    await CreateADummyDiscountUserAsync(id: 1, key: "DISCOUNT-001-USER-1").ConfigureAwait(false),
                    await CreateADummyDiscountUserAsync(id: 2, key: "DISCOUNT-010-USER-NotReal", masterID: 010, slaveID: 500).ConfigureAwait(false),
                };
                await InitializeMockSetDiscountUsersAsync(mockContext, RawDiscountUsers).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount User Roles
            if (DoAll || DoDiscounts || DoDiscountUserRoleTable)
            {
                RawDiscountUserRoles = new()
                {
                    await CreateADummyDiscountUserRoleAsync(id: 1, key: "DISCOUNT-USER-ROLE-1", roleName: "CEF Global Administrator").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountUserRolesAsync(mockContext, RawDiscountUserRoles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discount Vendors
            if (DoAll || DoDiscounts || DoDiscountVendorTable)
            {
                RawDiscountVendors = new()
                {
                    await CreateADummyDiscountVendorAsync(id: 1, key: "DISCOUNT-VENDOR-1").ConfigureAwait(false),
                };
                await InitializeMockSetDiscountVendorsAsync(mockContext, RawDiscountVendors).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Discounts
            if (DoAll || DoDiscounts || DoDiscountTable)
            {
                RawDiscounts = new()
                {
                    await CreateADummyDiscountAsync(id: 001, key: "DISCOUNT-SHIP-CARRIER-METHOD-1",     name: "Discount 01",                              desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 002, key: "DISCOUNT-NO-EFFECT",                 name: "Discount No Effect",                       desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.00m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 003, key: "DISCOUNT-ALREADY",                   name: "Discount Already",                         desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 004, key: "DISCOUNT-ALREADY-ITM",               name: "Discount Already (Item)",                  desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 005, key: "DISCOUNT-NOT-ADDABLE-EXCL",          name: "Discount Not Addable Excl.",               desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: false, priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 006, key: "DISCOUNT-NOT-ADDABLE-EXCL-ITM",      name: "Discount Not Addable Excl. (Item)",        desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: false, priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 007, key: "DISCOUNT-NOT-ADDABLE-EXP",           name: "Discount Not Addable Exp.",                desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: DateTime.Today.AddDays(-4)).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 008, key: "DISCOUNT-NOT-ADDABLE-FUT",           name: "Discount Not Addable Fut.",                desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: DateTime.Today.AddDays(4), endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 009, key: "DISCOUNT-NOT-ADDABLE-DEACTIVATED",   name: "Discount Not Addable Deactivated",         desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null, active: false).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 010, key: "DISCOUNT-NOT-ADDABLE-NO-AUTH",       name: "Discount Not Addable No Auth",             desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 011, key: "DISCOUNT-NOT-ADDABLE-USG-LMT-USR",   name: "Discount Not Addable Usage Limit (Users)", desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 1, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),

                    await CreateADummyDiscountAsync(id: 101, key: "DISCOUNT-$-ORDER-ADDABLE",           name: "Discount $ Order Addable",                 desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 102, key: "DISCOUNT-$-ORDER-NOT-ADDABLE-GT",    name: "Discount $ Order Not Addable GT",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 103, key: "DISCOUNT-$-ORDER-NOT-ADDABLE-GTE",   name: "Discount $ Order Not Addable GTE",         desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 104, key: "DISCOUNT-$-ORDER-NOT-ADDABLE-LT",    name: "Discount $ Order Not Addable LT",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_205.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 105, key: "DISCOUNT-$-ORDER-NOT-ADDABLE-LTE",   name: "Discount $ Order Not Addable LTE",         desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_204.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 106, key: "DISCOUNT-$-SHIP-ADDABLE",            name: "Discount $ Ship Addable",                  desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 107, key: "DISCOUNT-$-SHIP-NOT-ADDABLE-GT",     name: "Discount $ Ship Not Addable GT",           desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 108, key: "DISCOUNT-$-SHIP-NOT-ADDABLE-GTE",    name: "Discount $ Ship Not Addable GTE",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 109, key: "DISCOUNT-$-SHIP-NOT-ADDABLE-LT",     name: "Discount $ Ship Not Addable LT",           desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 00_008.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 110, key: "DISCOUNT-$-SHIP-NOT-ADDABLE-LTE",    name: "Discount $ Ship Not Addable LTE",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 00_007.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 111, key: "DISCOUNT-$-PRODUCT-ADDABLE",         name: "Discount $ Product Addable",               desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 112, key: "DISCOUNT-$-PRODUCT-NOT-ADDABLE-GT",  name: "Discount $ Product Not Addable GT",        desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 113, key: "DISCOUNT-$-PRODUCT-NOT-ADDABLE-GTE", name: "Discount $ Product Not Addable GTE",       desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 114, key: "DISCOUNT-$-PRODUCT-NOT-ADDABLE-LT",  name: "Discount $ Product Not Addable LT",        desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 00_008.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 115, key: "DISCOUNT-$-PRODUCT-NOT-ADDABLE-LTE", name: "Discount $ Product Not Addable LTE",       desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 00_007.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 116, key: "DISCOUNT-$-BXGY-ADDABLE",            name: "Discount $ Buy X Get Y Addable",           desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 117, key: "DISCOUNT-$-BXGY-NOT-ADDABLE-GT",     name: "Discount $ Buy X Get Y Not Addable GT",    desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 118, key: "DISCOUNT-$-BXGY-NOT-ADDABLE-GTE",    name: "Discount $ Buy X Get Y Not Addable GTE",   desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 119, key: "DISCOUNT-$-BXGY-NOT-ADDABLE-LT",     name: "Discount $ Buy X Get Y Not Addable LT",    desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 00_008.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 120, key: "DISCOUNT-$-BXGY-NOT-ADDABLE-LTE",    name: "Discount $ Buy X Get Y Not Addable LTE",   desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 00_007.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 1.25m, valueType: (int)Enums.DiscountValueType.Amount,  roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),

                    await CreateADummyDiscountAsync(id: 201, key: "DISCOUNT-%-ORDER-ADDABLE",           name: "Discount % Order Addable",                 desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 202, key: "DISCOUNT-%-ORDER-NOT-ADDABLE-GT",    name: "Discount % Order Not Addable GT",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 203, key: "DISCOUNT-%-ORDER-NOT-ADDABLE-GTE",   name: "Discount % Order Not Addable GTE",         desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 204, key: "DISCOUNT-%-ORDER-NOT-ADDABLE-LT",    name: "Discount % Order Not Addable LT",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_205.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 205, key: "DISCOUNT-%-ORDER-NOT-ADDABLE-LTE",   name: "Discount % Order Not Addable LTE",         desc: "desc", discountTypeID: (int)Enums.DiscountType.Order,    canCombine: true,  priority: 0, thresholdAmount: 00_204.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 206, key: "DISCOUNT-%-SHIP-ADDABLE",            name: "Discount % Ship Addable",                  desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 207, key: "DISCOUNT-%-SHIP-NOT-ADDABLE-GT",     name: "Discount % Ship Not Addable GT",           desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 208, key: "DISCOUNT-%-SHIP-NOT-ADDABLE-GTE",    name: "Discount % Ship Not Addable GTE",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 209, key: "DISCOUNT-%-SHIP-NOT-ADDABLE-LT",     name: "Discount % Ship Not Addable LT",           desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 00_008.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 210, key: "DISCOUNT-%-SHIP-NOT-ADDABLE-LTE",    name: "Discount % Ship Not Addable LTE",          desc: "desc", discountTypeID: (int)Enums.DiscountType.Shipping, canCombine: true,  priority: 0, thresholdAmount: 00_007.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 211, key: "DISCOUNT-%-PRODUCT-ADDABLE",         name: "Discount % Product Addable",               desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 212, key: "DISCOUNT-%-PRODUCT-NOT-ADDABLE-GT",  name: "Discount % Product Not Addable GT",        desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 213, key: "DISCOUNT-%-PRODUCT-NOT-ADDABLE-GTE", name: "Discount % Product Not Addable GTE",       desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 214, key: "DISCOUNT-%-PRODUCT-NOT-ADDABLE-LT",  name: "Discount % Product Not Addable LT",        desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 00_008.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 215, key: "DISCOUNT-%-PRODUCT-NOT-ADDABLE-LTE", name: "Discount % Product Not Addable LTE",       desc: "desc", discountTypeID: (int)Enums.DiscountType.Product,  canCombine: true,  priority: 0, thresholdAmount: 00_007.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: null, getYValue: null, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 216, key: "DISCOUNT-%-BXGY-ADDABLE",            name: "Discount % Buy X Get Y Addable",           desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 00_000.00m, discountCompareOperator: (int)Enums.CompareOperator.Undefined,            usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 217, key: "DISCOUNT-%-BXGY-NOT-ADDABLE-GT",     name: "Discount % Buy X Get Y Not Addable GT",    desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThan,          usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 218, key: "DISCOUNT-%-BXGY-NOT-ADDABLE-GTE",    name: "Discount % Buy X Get Y Not Addable GTE",   desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 10_000.00m, discountCompareOperator: (int)Enums.CompareOperator.GreaterThanOrEqualTo, usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 219, key: "DISCOUNT-%-BXGY-NOT-ADDABLE-LT",     name: "Discount % Buy X Get Y Not Addable LT",    desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 00_008.00m, discountCompareOperator: (int)Enums.CompareOperator.LessThan,             usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                    await CreateADummyDiscountAsync(id: 220, key: "DISCOUNT-%-BXGY-NOT-ADDABLE-LTE",    name: "Discount % Buy X Get Y Not Addable LTE",   desc: "desc", discountTypeID: (int)Enums.DiscountType.BuyXGetY, canCombine: true,  priority: 0, thresholdAmount: 00_007.99m, discountCompareOperator: (int)Enums.CompareOperator.LessThanOrEqualTo,    usageLimitGlobally: 0, usageLimitPerUser: 0, value: 0.95m, valueType: (int)Enums.DiscountValueType.Percent, roundingOperation: 0, buyXValue: 0005, getYValue: 0001, startDate: null,                      endDate: null).ConfigureAwait(false),
                };
                await InitializeMockSetDiscountsAsync(mockContext, RawDiscounts).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
