// <copyright file="DoMockingSetupForContextRunnerShopping.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner shopping class</summary>
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
        private async Task DoMockingSetupForContextRunnerShoppingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Carts
            if (DoAll || DoShopping || DoCartTable)
            {
                RawCarts = new()
                {
                    // Shopping Cart
                    await CreateADummyCartAsync(id: 01, key: "344016cd-4149-49e4-b4c0-fce3c621701d", sessionID: new Guid("344016cd-4149-49e4-b4c0-fce3c621701d"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001, billingContactID: 1, shippingContactID: 1).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 02, key: "0819541c-b350-4f41-9ac3-b394e8d0d49e", sessionID: new Guid("0819541c-b350-4f41-9ac3-b394e8d0d49e"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 03, key: "055dca7c-ba8d-46fd-a92f-dac53e900056", sessionID: new Guid("055dca7c-ba8d-46fd-a92f-dac53e900056"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: null, billingContactID: 1, shippingContactID: 1).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 04, key: "be1f5f36-a3f1-4eb0-9b2a-36f225afbea6", sessionID: new Guid("be1f5f36-a3f1-4eb0-9b2a-36f225afbea6"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 05, key: "771C8861-95BC-4437-9918-A779617DDBA2", sessionID: new Guid("771C8861-95BC-4437-9918-A779617DDBA2"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 06, key: "A91A9255-1FBA-4389-9B1B-E3B6A9A79C27", sessionID: new Guid("A91A9255-1FBA-4389-9B1B-E3B6A9A79C27"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 07, key: "E228905D-5019-43E1-A6ED-F57C02828DA3", sessionID: new Guid("E228905D-5019-43E1-A6ED-F57C02828DA3"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 08, key: "AF22524E-9F70-48BF-9A5E-5A2449BA9F47", sessionID: new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F47"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: null).ConfigureAwait(false),
                    await CreateADummyCartAsync(id: 11, key: "AF22524E-9F70-48BF-9A5E-5A2449BA9F50", sessionID: new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F50"), typeID: 01, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true).ConfigureAwait(false),
                    // Compare Cart
                    await CreateADummyCartAsync(id: 09, key: "AF22524E-9F70-48BF-9A5E-5A2449BA9F48", sessionID: new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F48"), typeID: 12, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001).ConfigureAwait(false),
                    // Wish List
                    await CreateADummyCartAsync(id: 10, key: "AF22524E-9F70-48BF-9A5E-5A2449BA9F49", sessionID: new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F49"), typeID: 04, subtotalItems: 0m, subtotalShipping: 0m, subtotalTaxes: 0m, subtotalFees: 0m, subtotalHandling: 0m, subtotalDiscounts: 0m, total: 0m, shippingSameAsBilling: true, accountID: 0001, userID: 0001).ConfigureAwait(false),
                };
                await InitializeMockSetCartsAsync(mockContext, RawCarts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart Contacts
            if (DoAll || DoShopping || DoCartContactTable)
            {
                RawCartContacts = new()
                {
                    await CreateADummyCartContactAsync(id: 1, key: "CART-CONTACT-1").ConfigureAwait(false),
                };
                await InitializeMockSetCartContactsAsync(mockContext, RawCartContacts).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart Events
            if (DoAll || DoShopping || DoCartEventTable)
            {
                var index = 0;
                RawCartEvents = new()
                {
                    await CreateADummyCartEventAsync(id: ++index, key: "Event-1", name: "Event 1", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetCartEventsAsync(mockContext, RawCartEvents).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Quote Event Types
            if (DoAll || DoShopping || DoCartEventTypeTable)
            {
                var index = 0;
                RawCartEventTypes = new()
                {
                    await CreateADummyCartEventTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetCartEventTypesAsync(mockContext, RawCartEventTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart Files
            if (DoAll || DoShopping || DoCartFileTable)
            {
                RawCartFiles = new()
                {
                    await CreateADummyCartFileAsync(id: 1, key: "CART-FILE-NEW-1", name: "Cart File New 1", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetCartFilesAsync(mockContext, RawCartFiles).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart Items
            if (DoAll || DoShopping || DoCartItemTable)
            {
                RawCartItems = new()
                {
                    // Cart 1
                    await CreateADummyCartItemAsync(id: 11, key: "CART-ITEM-11", name: "Cart Item 11", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000969, masterID: 1).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 12, key: "CART-ITEM-12", name: "Cart Item 12", desc: "desc", quantity: 00120, unitCorePrice: 0, productID: 001151, masterID: 1).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 13, key: "CART-ITEM-13", name: "Cart Item 13", desc: "desc", quantity: 01440, unitCorePrice: 0, productID: 001152, masterID: 1).ConfigureAwait(false),
                    // Cart 2
                    await CreateADummyCartItemAsync(id: 21, key: "CART-ITEM-21", name: "Cart Item 21", desc: "desc", quantity: 10000, unitCorePrice: 0, productID: 000970, masterID: 2).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 22, key: "CART-ITEM-22", name: "Cart Item 22", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000971, masterID: 2).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 23, key: "CART-ITEM-23", name: "Cart Item 23", desc: "desc", quantity: 00006, unitCorePrice: 0, productID: 000972, masterID: 2).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 24, key: "CART-ITEM-24", name: "Cart Item 24", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000973, masterID: 2).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 25, key: "CART-ITEM-25", name: "Cart Item 25", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000974, masterID: 2).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 26, key: "CART-ITEM-26", name: "Cart Item 26", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000975, masterID: 2).ConfigureAwait(false),
                    // Cart 3
                    await CreateADummyCartItemAsync(id: 31, key: "CART-ITEM-31", name: "Cart Item 31", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 3).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 32, key: "CART-ITEM-32", name: "Cart Item 32", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 3).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 33, key: "CART-ITEM-33", name: "Cart Item 33", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 3).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 34, key: "CART-ITEM-34", name: "Cart Item 34", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 3).ConfigureAwait(false),
                    // Cart 4
                    await CreateADummyCartItemAsync(id: 41, key: "CART-ITEM-41", name: "Cart Item 41", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 4).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 42, key: "CART-ITEM-42", name: "Cart Item 42", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 4).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 43, key: "CART-ITEM-43", name: "Cart Item 43", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 4).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 44, key: "CART-ITEM-44", name: "Cart Item 44", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000100, masterID: 4).ConfigureAwait(false),
                    // Cart 5 - leave empty !
                    // Cart 6 - single vendor products
                    await CreateADummyCartItemAsync(id: 61, key: "CART-ITEM-61", name: "Cart Item 61", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001151, masterID: 6).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 62, key: "CART-ITEM-62", name: "Cart Item 62", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001152, masterID: 6).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 63, key: "CART-ITEM-63", name: "Cart Item 63", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001153, masterID: 6).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 64, key: "CART-ITEM-64", name: "Cart Item 64", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001154, masterID: 6).ConfigureAwait(false),
                    // Cart 7 - multiple vendor products
                    await CreateADummyCartItemAsync(id: 71, key: "CART-ITEM-71", name: "Cart Item 71", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001151, masterID: 7).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 72, key: "CART-ITEM-72", name: "Cart Item 72", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001152, masterID: 7, forceUniqueLineItemKey: "a").ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 73, key: "CART-ITEM-73", name: "Cart Item 73", desc: "desc", quantity: 00002, unitCorePrice: 0, productID: 001152, masterID: 7, forceUniqueLineItemKey: "b").ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 74, key: "CART-ITEM-74", name: "Cart Item 74", desc: "desc", quantity: 00002, unitCorePrice: 0, productID: 001153, masterID: 7).ConfigureAwait(false),
                    // Cart 8 - multiple vendor products, with null vendor
                    await CreateADummyCartItemAsync(id: 81, key: "CART-ITEM-81", name: "Cart Item 81", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001151, masterID: 8).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 82, key: "CART-ITEM-82", name: "Cart Item 82", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001152, masterID: 8, forceUniqueLineItemKey: "a").ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 83, key: "CART-ITEM-83", name: "Cart Item 83", desc: "desc", quantity: 00002, unitCorePrice: 0, productID: 001152, masterID: 8, forceUniqueLineItemKey: "b").ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 84, key: "CART-ITEM-84", name: "Cart Item 84", desc: "desc", quantity: 00002, unitCorePrice: 0, productID: 001153, masterID: 8).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 85, key: "CART-ITEM-85", name: "Cart Item 85", desc: "desc", quantity: 00002, unitCorePrice: 0, productID: 001154, masterID: 8).ConfigureAwait(false),
                    // Cart 9 - Compare Cart
                    await CreateADummyCartItemAsync(id: 91, key: "COMPARE-CART-ITEM-91", name: "Compare Cart Item 91", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001151, masterID: 9).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 92, key: "COMPARE-CART-ITEM-92", name: "Compare Cart Item 92", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001152, masterID: 9).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 93, key: "COMPARE-CART-ITEM-93", name: "Compare Cart Item 93", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001153, masterID: 9).ConfigureAwait(false),
                    // Cart 10 - Wish List
                    await CreateADummyCartItemAsync(id: 101, key: "WISH-LIST-CART-ITEM-91", name: "Wish List Item 101", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001151, masterID: 10).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 102, key: "WISH-LIST-CART-ITEM-92", name: "Wish List Item 102", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001152, masterID: 10).ConfigureAwait(false),
                    await CreateADummyCartItemAsync(id: 103, key: "WISH-LIST-CART-ITEM-93", name: "Wish List Item 103", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 001153, masterID: 10).ConfigureAwait(false),
                    // Cart 11 - Just to assign a user to the cart
                    await CreateADummyCartItemAsync(id: 111, key: "CART-ITEM-11-1", name: "Cart Item 11-1", desc: "desc", quantity: 00001, unitCorePrice: 0, productID: 000969, masterID: 11).ConfigureAwait(false),
                };
                await InitializeMockSetCartItemsAsync(mockContext, RawCartItems).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart Item Targets
            if (DoAll || DoShopping || DoCartItemTargetTable)
            {
                var index = 0;
                RawCartItemTargets = new()
                {
                    await CreateADummyCartItemTargetAsync(id: ++index, key: "CART-ITEM-81-TARGET-1", masterID: 81, quantity: 1, selectedRateQuoteID: 1).ConfigureAwait(false),
                    await CreateADummyCartItemTargetAsync(id: ++index, key: "CART-ITEM-82-TARGET-1", masterID: 82, quantity: 1, selectedRateQuoteID: 1).ConfigureAwait(false),
                    await CreateADummyCartItemTargetAsync(id: ++index, key: "CART-ITEM-83-TARGET-1", masterID: 83, quantity: 2, selectedRateQuoteID: 1).ConfigureAwait(false),
                    await CreateADummyCartItemTargetAsync(id: ++index, key: "CART-ITEM-84-TARGET-1", masterID: 84, quantity: 2, selectedRateQuoteID: 1).ConfigureAwait(false),
                    await CreateADummyCartItemTargetAsync(id: ++index, key: "CART-ITEM-85-TARGET-1", masterID: 85, quantity: 2, selectedRateQuoteID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetCartItemTargetsAsync(mockContext, RawCartItemTargets).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart States
            if (DoAll || DoShopping || DoCartStateTable)
            {
                var index = 0;
                RawCartStates = new()
                {
                    await CreateADummyCartStateAsync(id: ++index, key: "WORK", name: "Work", desc: "desc", sortOrder: 1, displayName: "Work").ConfigureAwait(false),
                    await CreateADummyCartStateAsync(id: ++index, key: "HISTORY", name: "History", desc: "desc", sortOrder: 2, displayName: "History").ConfigureAwait(false),
                };
                await InitializeMockSetCartStatesAsync(mockContext, RawCartStates).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart Statuses
            if (DoAll || DoShopping || DoCartStatusTable)
            {
                var index = 0;
                RawCartStatuses = new()
                {
                    await CreateADummyCartStatusAsync(id: ++index, key: "New", name: "New", desc: "desc", displayName: "New").ConfigureAwait(false),
                    await CreateADummyCartStatusAsync(id: ++index, key: "Abandoned", name: "Abandoned", desc: "desc", displayName: "Abandoned").ConfigureAwait(false),
                    await CreateADummyCartStatusAsync(id: ++index, key: "Converted To Order", name: "Converted To Order", desc: "desc", displayName: "Converted To Order").ConfigureAwait(false),
                };
                await InitializeMockSetCartStatusesAsync(mockContext, RawCartStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Cart Types
            if (DoAll || DoShopping || DoCartTypeTable)
            {
                RawCartTypes = new()
                {
                    await CreateADummyCartTypeAsync(id: 01, key: "Cart", name: "Cart", desc: "desc", sortOrder: 1, displayName: "Cart").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 02, key: "Favorites List", name: "Favorites List", desc: "desc", sortOrder: 7, displayName: "Favorites List").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 03, key: "Favorite Products", name: "Favorite Products", desc: "desc", sortOrder: 7, displayName: "Favorite Products").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 04, key: "Wish List", name: "Wish List", desc: "desc", sortOrder: 4, displayName: "Wish List").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 05, key: "Request For Quote", name: "Request For Quote", desc: "desc", sortOrder: 2, displayName: "Request For Quote").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 06, key: "Bookmark", name: "Bookmark", desc: "desc", displayName: "Bookmark").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 07, key: "Watch List", name: "Watch List", desc: "desc", sortOrder: 6, displayName: "Watch List").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 08, key: "Request For Sample", name: "Request For Sample", desc: "desc", sortOrder: 3, displayName: "Request For Sample").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 09, key: "Notify Me When In Stock", name: "Notify Me When In Stock", desc: "desc", sortOrder: 5, displayName: "Notify Me When In Stock").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 10, key: "Quote Cart", name: "Quote Cart", desc: "desc", sortOrder: 1, displayName: "Quote Cart").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 11, key: "Samples Cart", name: "Samples Cart", desc: "desc", sortOrder: 1, displayName: "Samples Cart").ConfigureAwait(false),
                    await CreateADummyCartTypeAsync(id: 12, key: "Compare Cart", name: "Compare Cart", desc: "desc", sortOrder: 4, displayName: "Compare Cart").ConfigureAwait(false),
                };
                await InitializeMockSetCartTypesAsync(mockContext, RawCartTypes).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
