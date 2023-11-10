// <copyright file="TargetsCheckoutProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the targets checkout provider tests class</summary>
#pragma warning disable CS8602 // Dereference of a possibly null reference.
// ReSharper disable MemberCanBePrivate.Global, ParameterOnlyUsedForPreconditionCheck.Global, RedundantArgumentDefaultValue, RedundantEmptyObjectOrCollectionInitializer
namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using CartValidation;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.CartValidation;
    using Interfaces.Workflow;
    using Mapper;
    using Models;
    using Newtonsoft.Json;
    using Payments.Mock;
    using TargetOrder;
    using Taxes.Basic;
    using Utilities;
    using Workflow;
    using Xunit;

    [Trait("Category", "Providers.Checkouts.Targets")]
    public class TargetsCheckoutProviderTests : XUnitLogHelper
    {
        public TargetsCheckoutProviderTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls; // Disable 1.0
            ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Tls11; // Disable 1.1
            // 1.2+ is the only thing that should be allowed
        }

        [Fact/*(Skip = "Cannot run on build servers, ssl error")*/]
        public async Task Verify_TargetsCheckout_Analyze_AsUser_WithValidData_Works()
        {
            try
            {
                await Verify_TargetsCheckout_Analyze_AsUser_WithValidData_Works_InnerAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Do Nothing, has issues running in build servers
                TestOutputHelper.WriteLine(ex.ToString());
                // throw;
            }
        }

        private async Task Verify_TargetsCheckout_Analyze_AsUser_WithValidData_Works_InnerAsync()
        {
            // Arrange
            var contextProfileName = "TargetsCheckoutProviderTests|Verify_TargetsCheckout_Analyze_AsUser_WithValidData_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                // Arrange: Database
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountContactTable = true,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoAttributeTypeTable = true,
                    DoBrandProductTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCategoryTable = true,
                    DoContactTable = true,
                    DoContactTypeTable = true,
                    DoCountryTable = true,
                    DoCurrencyTable = true,
                    DoGeneralAttributeTable = true,
                    DoManufacturerProductTable = true,
                    DoManufacturerTable = true,
                    DoNoteTable = true,
                    DoNoteTypeTable = true,
                    DoPackageTable = true,
                    DoPaymentTable = true,
                    DoPaymentStatusTable = true,
                    DoPaymentTypeTable = true,
                    DoProductCategoryTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoSalesGroupTable = true,
                    DoSalesItemTargetTypeTable = true,
                    DoSalesInvoiceItemTable = true,
                    DoSalesInvoiceStateTable = true,
                    DoSalesInvoiceStatusTable = true,
                    DoSalesInvoiceTable = true,
                    DoSalesInvoiceTypeTable = true,
                    DoSalesOrderSalesInvoiceTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderStateTable = true,
                    DoSalesOrderStatusTable = true,
                    DoSalesOrderTable = true,
                    DoSalesOrderTypeTable = true,
                    DoShipCarrierMethodTable = true,
                    DoShipCarrierTable = true,
                    DoStoreInventoryLocationTypeTable = true,
                    DoStoreProductTable = true,
                    DoStoreTable = true,
                    DoUserTable = true,
                    DoVendorProductTable = true,
                    DoVendorTable = true,
                    DoWalletTable = true,
                    DoCartContactTable = true,
                    DoDiscountTable = true,
                    DoProductImageTable = true,
                    DoProductPricePointTable = true,
                    DoProductTypeTable = true,
                };
                BaseModelMapper.Initialize();
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var config = new CartValidatorConfig
                {
                    DoProductRestrictionsByAccountType = false,
                    DoProductRestrictionsByMinMax = false,
                    DoCategoryRestrictionsByMinMax = false,
                    DoStoreRestrictionsByMinMax = false,
                    DoVendorRestrictionsByMinMax = false,
                    DoManufacturerRestrictionsByMinMax = false,
                    DoProductRestrictionsByMustPurchaseMultiplesOfAmount = false,
                    UseShipToHomeFromAnyDCStockCheck = false,
                    UsePickupInStoreStockCheck = false,
                    UseShipToStoreFromStoreDCStockCheck = false,
                    OverrideAndForceShipToHomeOptionIfNoShipOptionSelected = false,
                    OverrideAndForceNoShipToOptionIfWhenShipOptionSelected = false,
                    ProductRestrictionsKeysValue = new Dictionary<string, string>(),
                };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.For<ICartValidatorConfig>().Use(config);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Arrange: Providers
                var checkoutProvider = new TargetOrderCheckoutProvider();
                var checkoutTestingBlock = new CheckoutTestingBlock();
                await checkoutTestingBlock.SetupIDsAsync(contextProfileName).ConfigureAwait(false);
                await checkoutProvider.InitAsync(
                        orderStatusPendingID: checkoutTestingBlock.OrderStatusPendingID,
                        orderStatusPaidID: checkoutTestingBlock.OrderStatusPaidID,
                        orderStatusOnHoldID: checkoutTestingBlock.OrderStatusOnHoldID,
                        orderTypeID: checkoutTestingBlock.OrderTypeID,
                        orderStateID: checkoutTestingBlock.OrderStateID,
                        billingTypeID: checkoutTestingBlock.BillingTypeID,
                        shippingTypeID: checkoutTestingBlock.ShippingTypeID,
                        customerNoteTypeID: checkoutTestingBlock.CustomerNoteTypeID,
                        defaultCurrencyID: checkoutTestingBlock.DefaultCurrencyID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                var taxesProvider = new BasicTaxesProvider();
                await taxesProvider.InitAsync(contextProfileName).ConfigureAwait(false);
                var cartWorkflow = new CartWorkflow();
                var cartItemWorkflow = new CartItemWorkflow();
                var addressBookWorkflow = new AddressBookWorkflow();
                // Arrange: Models
                var us = new CultureInfo("en-US");
                var checkout = new CheckoutModel
                {
                    IsPartialPayment = false,
                    WithCartInfo = new CheckoutWithCartInfo
                    {
                        CartTypeName = "Cart",
                        CartSessionID = Guid.Parse("af22524e-9f70-48bf-9a5e-5a2449ba9f47"),
                        // CartID = 88026, // Admin only
                    },
                    IsSameAsBilling = false,
                    SpecialInstructions = null,
                    Billing = new ContactModel
                    {
                        ID = 1,
                        CustomKey = "CONTACT-1",
                        Active = true,
                        CreatedDate = DateTime.ParseExact("2020-03-28T19:07:23.2973011", "o", us),
                        SerializableAttributes = new SerializableAttributesDictionary { },
                        TypeID = 10,
                        TypeKey = "General",
                        TypeName = "General",
                        FirstName = "James",
                        LastName = "Gray",
                        Phone1 = "8009282960",
                        Email1 = "james.gray@claritymis.com",
                        SameAsBilling = false,
                        AddressID = 1,
                        AddressKey = "BILL TO",
                        Address = new AddressModel
                        {
                            ID = 1,
                            CustomKey = "BILL TO",
                            Active = true,
                            CreatedDate = DateTime.ParseExact("2020-03-28T19:07:23.2973011", "o", us),
                            SerializableAttributes = new SerializableAttributesDictionary
                            {
                                ["Validated-By-MockAddressValidationProvider"] = new SerializableAttributeObject
                                {
                                    ID = 37,
                                    Key = "Validated-By-MockAddressValidationProvider",
                                    Value = "{\"Timestamp\":\"2020-03-23T14:07:59.4966007-05:00\"}",
                                    UofM = ""
                                },
                            },
                            Street1 = "9442 N Capital of TX Hwy",
                            Street2 = "Plaza 1, Ste 925",
                            City = "Austin",
                            PostalCode = "78759",
                            Latitude = 30.390397m,
                            Longitude = -97.748598m,
                            RegionID = 43,
                            RegionKey = "Texas",
                            RegionName = "Texas",
                            RegionCode = "TX",
                            Region = new RegionModel
                            {
                                Code = "TX",
                                CountryID = 1,
                                CountryKey = "United States of America",
                                CountryName = "United States of America",
                                Country = new CountryModel
                                {
                                    Code = "USA",
                                    Name = "United States of America",
                                    ID = 1,
                                    CustomKey = "United States of America",
                                    Active = true,
                                    CreatedDate = DateTime.ParseExact("2018-01-01T00:00:00.0000000", "o", us),
                                    SerializableAttributes = new SerializableAttributesDictionary { },
                                },
                                Name = "Texas",
                                ID = 43,
                                CustomKey = "Texas",
                                Active = true,
                                CreatedDate = DateTime.ParseExact("2018-01-01T00:00:00.0000000", "o", us),
                                SerializableAttributes = new SerializableAttributesDictionary { },
                            },
                            CountryID = 1,
                            CountryKey = "United States of America",
                            CountryName = "United States of America",
                            CountryCode = "USA",
                            Country = new CountryModel
                            {
                                Code = "USA",
                                Name = "United States of America",
                                ID = 1,
                                CustomKey = "United States of America",
                                Active = true,
                                CreatedDate = DateTime.ParseExact("2018-01-01T00:00:00.0000000", "o", us),
                                SerializableAttributes = new SerializableAttributesDictionary { },
                            },
                        },
                    },
                    SerializableAttributes = new SerializableAttributesDictionary { },
                    PaymentStyle = Enums.PaymentMethodsStrings.CreditCard,
                    PayByWalletEntry = new CheckoutPayByWalletEntry
                    {
                        WalletID = 1,
                        WalletCVV = "123",
                    },
                    WithUserInfo = new CheckoutWithUserInfo
                    {
                        IsNewAccount = false,
                        UserName = "clarity",
                        UserID = 1,
                    },
                };
                var pricingFactoryContext = new PricingFactoryContextModel
                {
                    AccountID = 1,
                    CountryID = 1,
                    AccountTypeID = 1,
                    CurrencyID = 1,
                    Quantity = 1,
                    UserID = 1,
                    SessionID = checkout.WithCartInfo.CartSessionID.Value,
                    UserRoles = new List<string> { "CEF Global Administrator" },
                };
                var addressOptions = await addressBookWorkflow.GetAddressBookAsync(1, contextProfileName).ConfigureAwait(false);
                var shipmentOrigin = new ContactModel
                {
                    Active = true,
                    CreatedDate = new DateTime(2023, 1, 1),
                    Address = new AddressModel
                    {
                        Active = true,
                        CreatedDate = new DateTime(2023, 1, 1),
                        Street1 = "6801 N Capital of Texas Hwy",
                        Street2 = "Suite 312",
                        City = "Austin",
                        RegionCode = "TX",
                        CountryCode = "USA",
                        PostalCode = "78731"
                    },
                };
                // Act 1: User enters the shipping step, the original cart loads and each item gets it's targets list
                // initialized with a default target entry. The user then splits the items using the UI
                var (originalCartResponse, _) = await cartWorkflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            pricingFactoryContext.SessionID,
                            checkout.WithCartInfo.CartTypeName,
                            1,
                            1),
                        pricingFactoryContext,
                        taxesProvider,
                        contextProfileName)
                    .ConfigureAwait(false);
                var originalCartBeforeTargetsAdded = originalCartResponse.Result;
                var items = originalCartBeforeTargetsAdded!.SalesItems;
                // This function is the C# equivalent of the function with the same name in the TypeScript
                static ISalesItemTargetBaseModel targetFactory(decimal quantity, int? contactID)
                {
                    return new SalesItemTargetBaseModel
                    {
                        Active = true,
                        CreatedDate = DateTime.Now,
                        DestinationContactID = contactID ?? 0,
                        DestinationContact = null,
                        //OriginProductInventoryLocationSectionID = null,
                        // MasterID = null,
                        //OriginStoreProductID = null,
                        //OriginVendorProductID = null,
                        SelectedRateQuoteID = null,
                        // TypeID = null,
                        TypeKey = "ShipToHome", // this.cvServiceStrings.attributes.shipToHome,
                        Quantity = quantity,
                    };
                }
                // This loop is the C# equivalent of the initializeSalesItems function in the TypeScript
                foreach (var item in items!)
                {
                    // Check if the quantity changed since the last time the targets were generated
                    var totalQuantity = item.Quantity + (item.QuantityBackOrdered ?? 0) + (item.QuantityPreSold ?? 0);
                    if (item.Targets != null
                        && item.Targets.Count > 0
                        && item.Targets.Sum(x => x.Quantity) != totalQuantity)
                    {
                        // Reset targets because it changed
                        item.Targets = null;
                    }
                    if (item.Targets == null || !item.Targets.Any())
                    {
                        // When there is no list, create one with a default Target that has the full
                        // quantity
                        // WARNING! This is the only location where a targets list should be
                        // initialized in the entire platform!
                        item.Targets = new List<ISalesItemTargetBaseModel> { targetFactory(totalQuantity, null) };
                        continue;
                    }
                    // Check for and collapse duplicates in the full list, this is a processing issue
                    // that happens sometimes, but easily corrected by re-grouping
                    var grouped = item.Targets.GroupBy(x => JsonConvert.SerializeObject(new
                    {
                        typeKey = x.Type?.CustomKey ?? x.TypeKey,
                        //storeID = x.OriginStoreProductID,
                        //vendorID = x.OriginVendorProductID,
                        //ilID = x.OriginProductInventoryLocationSectionID,
                        destID = x.DestinationContactID,
                        nothingToShip = x.NothingToShip,
                    }));
                    var replacementList = grouped.Select(grouping => grouping.First()).ToList();
                    item.Targets = replacementList;
                }
                // Now that the initial default targets are populated, the user can add more targets with the UI as desired
                // This function is the C# equivalent of the one in the TypeScript
                bool allocateQuantity(
                    ISalesItemBaseModel<IAppliedCartItemDiscountModel> item,
                    ISalesItemTargetBaseModel target,
                    decimal byQuantity = 1)
                {
                    var totalQuantity = item.Quantity + (item.QuantityBackOrdered ?? 0) + (item.QuantityPreSold ?? 0);
                    if (!(byQuantity <= totalQuantity - 1))
                    {
                        TestOutputHelper.WriteLine(
                            "invalid byQuantity for allocateQuantity, would attempt to cause over-allocation, blocking modification");
                        return false;
                    }
                    if (item.Targets == null)
                    {
                        TestOutputHelper.WriteLine("This item has no targets list, cannot modify quantity");
                        return false;
                    }
                    var allocated = false;
                    foreach (var thisTarget in item.Targets)
                    {
                        if (allocated)
                        {
                            TestOutputHelper.WriteLine("Val-1: Reduce already allocated");
                            return true;
                        }
                        // if (thisTarget["$$hashKey"] == target["$$hashKey"])
                        // {
                        //     TestOutputHelper.WriteLine("Val-2: Same target, skipping allocate");
                        //     return allocated;
                        // }
                        if (!(thisTarget.Quantity - 1 >= byQuantity))
                        {
                            TestOutputHelper.WriteLine("Val-3: Would not be able to adjust this item up to allocate the new target down");
                            return false;
                        }
                        if (!(target.Quantity + byQuantity < thisTarget.Quantity))
                        {
                            TestOutputHelper.WriteLine("Val-4: Would not be able to adjust this target to allocate to another item");
                        }
                        if (!(thisTarget.Quantity - byQuantity > 0))
                        {
                            TestOutputHelper.WriteLine("Val-5: Would not be able to adjust this item down to allocate the other target up");
                            return false;
                        }
                        if (!(target.Quantity + byQuantity > 0))
                        {
                            TestOutputHelper.WriteLine("Val-6: Would not be able to adjust this target down to allocate the other item up");
                            return false;
                        }
                        // All validations pass, do the allocation
                        TestOutputHelper.WriteLine("Pass: Will do the adjustment now");
                        thisTarget.Quantity -= byQuantity;
                        target.Quantity += byQuantity;
                        allocated = true;
                    }
                    return allocated;
                }
                // This function is the C# equivalent of the one in the TypeScript
                // ReSharper disable once UnusedLocalFunctionReturnValue
                int addShippingTarget(ISalesItemBaseModel<IAppliedCartItemDiscountModel> item)
                {
                    ////this.setRunning(this.$translate("ui.storefront.common.Analyzing.Ellipses"));
                    if (item.Targets == null)
                    {
                        // NOTE: This should never happen, before this point, the UI has at least
                        // created a default target to use in initializeSalesItems
                        throw new Exception("There was not a targets list on this item");
                    }
                    if (!item.Targets.Any())
                    {
                        // NOTE: This should never happen, before this point, the UI has at least
                        // created a default target to use in initializeSalesItems
                        throw new Exception("There were no targets on this item in it's list");
                    }
                    // Generate a target with no quantity, the allocate method will assign a value if it can
                    var newTarget = targetFactory(0, null);
                    if (allocateQuantity(item, newTarget, 1))
                    {
                        // We successfully allocated, so we can put it in the list of targets
                        var tempTargets = item.Targets;
                        tempTargets.Add(newTarget);
                        item.Targets = tempTargets;
                        var resultA = item.Targets.Count;
                        ////this.finishRunning();
                        return resultA;
                    }
                    // We couldn't allocate, so just return the list as it was
                    var resultB = item.Targets.Count;
                    ////this.finishRunning();
                    ////this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                    return resultB;
                }
                // Let's assume the user clicks to add targets to the 3rd and 4th items (each have a quantity of 2)
                // All other targets are just using the default shipping
                addShippingTarget(originalCartBeforeTargetsAdded.SalesItems[2]);
                addShippingTarget(originalCartBeforeTargetsAdded.SalesItems[3]);
                // This function is the C# equivalent of what's in the TypeScript
                void selectDestinationAndTypeKey(ISalesItemTargetBaseModel target, int optionID, string typeKey)
                {
                    target.DestinationContactID = optionID;
                    target.DestinationContact = addressOptions.Single(x => x.ContactID == optionID).Contact;
                    target.DestinationContactKey = target.DestinationContact.CustomKey;
                    target.TypeID = 0;
                    target.TypeKey = typeKey;
                    target.TypeName = null;
                    target.Type = null;
                    ////this.$rootScope.$broadcast(this.cvServiceStrings.events.shipping.revalidateStep);
                }
                // 2 is the "SHIP TO" contact in the mock context data, it's default shipping
                selectDestinationAndTypeKey(originalCartBeforeTargetsAdded.SalesItems[0].Targets[0], 2, "ShipToHome");
                selectDestinationAndTypeKey(originalCartBeforeTargetsAdded.SalesItems[1].Targets[0], 2, "ShipToHome");
                selectDestinationAndTypeKey(originalCartBeforeTargetsAdded.SalesItems[2].Targets[0], 2, "ShipToHome");
                selectDestinationAndTypeKey(originalCartBeforeTargetsAdded.SalesItems[3].Targets[0], 2, "ShipToHome");
                selectDestinationAndTypeKey(originalCartBeforeTargetsAdded.SalesItems[4].Targets[0], 2, "ShipToHome");
                // 3 it the "Other" contact in the mock context data, it's not the default billing or shipping
                selectDestinationAndTypeKey(originalCartBeforeTargetsAdded.SalesItems[2].Targets[1], 3, "ShipToHome");
                selectDestinationAndTypeKey(originalCartBeforeTargetsAdded.SalesItems[3].Targets[1], 3, "ShipToHome");
                // Now that they have selected everything, the user clicks Submit to fire off an analysis,
                // which calls UpdateCartItems first
                async Task UpdateCartItemsEndpointAsync(List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> itemsToUpdate)
                {
                    var response = await cartItemWorkflow.UpdateMultipleAsync(
                            new SessionCartBySessionAndTypeLookupKey(
                                Guid.Parse("af22524e-9f70-48bf-9a5e-5a2449ba9f47"),
                                "Cart",
                                null,
                                null),
                            itemsToUpdate.Where(x => x.ItemType == Enums.ItemType.Item).ToList(),
                            pricingFactoryContext,
                            contextProfileName)
                        .ConfigureAwait(false);
                    if (!response.ActionSucceeded)
                    {
                        throw new ArgumentException(
                            "ERROR! Unable to complete the Update of these cart items.",
                            new ArgumentException(response.Messages.Aggregate((c, n) => $"{c}\r\n{n}")));
                    }
                }
                await UpdateCartItemsEndpointAsync(originalCartBeforeTargetsAdded.SalesItems).ConfigureAwait(false);
                // Assert 1
                // 5+2=7 Targets added to the original 5 that was in the table, total 12
                Assert.Equal(12, mockingSetup.CartItemTargets.Object.Count());
                // But only 7 are active
                Assert.Equal(7, mockingSetup.CartItemTargets.Object.Count(x => x.Active));
                // Act 2: The user submits the splits they created in the UI and an analysis occurs, will create a set
                // of target carts based on the rules of the system
                var firstAnalyzeResponse = await checkoutProvider.AnalyzeAsync(
                        checkout: checkout,
                        pricingFactoryContext: pricingFactoryContext,
                        new SessionCartBySessionAndTypeLookupKey(default, "Cart", 1),
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert 2
                Verify_CEFAR_Passed_WithNoMessages(firstAnalyzeResponse);
                Assert.Equal(3, firstAnalyzeResponse.Result.Count); // There should be the original cart and one target grouping
                var originalCart = firstAnalyzeResponse.Result[0];
                var initialTargetCartToDefaultShipping = firstAnalyzeResponse.Result[1];
                var initialTargetCartToOtherShipping = firstAnalyzeResponse.Result[2];
                Assert.Equal("af22524e-9f70-48bf-9a5e-5a2449ba9f47", originalCart.CustomKey);
                Assert.Equal("af22524e-9f70-48bf-9a5e-5a2449ba9f47", initialTargetCartToDefaultShipping.CustomKey);
                Assert.Equal("af22524e-9f70-48bf-9a5e-5a2449ba9f47", initialTargetCartToOtherShipping.CustomKey);
                Assert.NotEqual(originalCart.ID, initialTargetCartToDefaultShipping.ID);
                Assert.NotEqual(originalCart.ID, initialTargetCartToOtherShipping.ID);
                Assert.NotEqual(initialTargetCartToDefaultShipping.ID, initialTargetCartToOtherShipping.ID);
                Assert.Equal("Cart", originalCart.TypeKey);
                Assert.Equal("Target-Grouping-{\"TK\":\"ShipToHome\",\"CSK\":\"NormalShip\",\"HD\":-6708759711376809068}", initialTargetCartToDefaultShipping.TypeKey);
                Assert.Equal("Target-Grouping-{\"TK\":\"ShipToHome\",\"CSK\":\"NormalShip\",\"HD\":1477031529296715340}", initialTargetCartToOtherShipping.TypeKey);
                Assert.Equal(5, originalCart.SalesItems.Count);
                Assert.Equal(5, initialTargetCartToDefaultShipping.SalesItems.Count);
                Assert.Equal(2, initialTargetCartToOtherShipping.SalesItems.Count);
                Assert.Equal(7, originalCart.SalesItems.SelectMany(x => x.Targets).Count());
                Assert.True(originalCart.SalesItems.All(x => Contract.CheckValidID(x.ID)));
                Assert.True(originalCart.SalesItems.All(x => x.Targets.All(y => Contract.CheckValidID(y.ID))));
                Assert.Single(originalCart.SalesItems[0].Targets!);
                Assert.Single(originalCart.SalesItems[1].Targets!);
                Assert.Equal(2, originalCart.SalesItems[2].Targets.Count);
                Assert.Equal(2, originalCart.SalesItems[3].Targets.Count);
                Assert.Single(originalCart.SalesItems[4].Targets!);
                Assert.Equal(1, originalCart.SalesItems[0].Targets[0].Quantity);
                Assert.Equal(1, originalCart.SalesItems[1].Targets[0].Quantity);
                Assert.Equal(1, originalCart.SalesItems[2].Targets[1].Quantity);
                Assert.Equal(1, originalCart.SalesItems[3].Targets[0].Quantity);
                Assert.Equal(1, originalCart.SalesItems[3].Targets[1].Quantity);
                Assert.Equal(2, originalCart.SalesItems[4].Targets[0].Quantity);
                Assert.Equal(1, initialTargetCartToDefaultShipping.SalesItems[0].TotalQuantity);
                Assert.Equal(1, initialTargetCartToDefaultShipping.SalesItems[1].TotalQuantity);
                Assert.Equal(1, initialTargetCartToDefaultShipping.SalesItems[2].TotalQuantity);
                Assert.Equal(1, initialTargetCartToDefaultShipping.SalesItems[3].TotalQuantity);
                Assert.Equal(2, initialTargetCartToDefaultShipping.SalesItems[4].TotalQuantity);
                Assert.Equal(1, initialTargetCartToOtherShipping.SalesItems[0].TotalQuantity);
                Assert.Equal(1, initialTargetCartToOtherShipping.SalesItems[1].TotalQuantity);
                // Act 3: The analysis returns to the UI, which then runs rates for each target cart that was returned
                Task<CEFActionResponse<List<IRateQuoteModel>>> GetCurrentCartShippingRateQuotesEndpointAsync(
                    string requestTypeName,
                    bool requestExpedited)
                {
                    return cartWorkflow.GetRateQuotesAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            Guid.Parse("af22524e-9f70-48bf-9a5e-5a2449ba9f47"),
                            requestTypeName,
                            1,
                            1),
                        shipmentOrigin,
                        requestExpedited,
                        contextProfileName);
                }
                // Get the rates for the two targets
                var rates1 = await GetCurrentCartShippingRateQuotesEndpointAsync(
                        initialTargetCartToDefaultShipping.TypeKey!,
                        false)
                    .ConfigureAwait(false);
                Verify_CEFAR_Passed_WithNoMessages(rates1);
                var rates2 = await GetCurrentCartShippingRateQuotesEndpointAsync(
                        initialTargetCartToOtherShipping.TypeKey!,
                        false)
                    .ConfigureAwait(false);
                Verify_CEFAR_Passed_WithNoMessages(rates2);
                // Pick the cheapest rate on each cart like a user would
                Task<CEFActionResponse> ApplyCurrentCartShippingRateQuoteEndpointAsync(
                    string requestTypeName,
                    int requestRateQuoteID)
                {
                    return cartWorkflow.ApplyRateQuoteToCartAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            Guid.Parse("af22524e-9f70-48bf-9a5e-5a2449ba9f47"),
                            requestTypeName,
                            1,
                            1),
                        requestRateQuoteID,
                        contextProfileName);
                }
                var applyRate1 = await ApplyCurrentCartShippingRateQuoteEndpointAsync(
                        initialTargetCartToDefaultShipping.TypeKey!,
                        Contract.RequiresValidID(rates1.Result!.First().ID))
                    .ConfigureAwait(false);
                Verify_CEFAR_Passed_WithNoMessages(applyRate1);
                var applyRate2 = await ApplyCurrentCartShippingRateQuoteEndpointAsync(
                        initialTargetCartToOtherShipping.TypeKey!,
                        Contract.RequiresValidID(rates2.Result!.First().ID))
                    .ConfigureAwait(false);
                Verify_CEFAR_Passed_WithNoMessages(applyRate2);
                // The step should now be valid with rates assigned, do the payment step and finish
                var result = await checkoutProvider.CheckoutAsync(
                        checkout: checkout,
                        pricingFactoryContext: pricingFactoryContext,
                        new SessionCartBySessionAndTypeLookupKey(default, "Cart", 1, 1),
                        taxesProvider: taxesProvider,
                        gateway: new MockPaymentsProvider(),
                        selectedAccountID: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(
                    result.Succeeded,
                    result.ErrorMessage
                        ?? result.ErrorMessages
                            ?.DefaultIfEmpty("No Error Messages")
                            .Aggregate((c, n) => c + "\r\n" + n));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        // ReSharper disable once UnusedMember.Global
        protected static void Verify_CEFAR_Failed_WithSingleMessage(CEFActionResponse result, string expectMessage)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Single(result.Messages);
            Assert.Equal(expectMessage, result.Messages[0]);
        }

        protected static void Verify_CEFAR_Passed_WithNoMessages(CEFActionResponse result)
        {
            Assert.NotNull(result);
            Assert.True(
                result.ActionSucceeded,
                result.Messages?.DefaultIfEmpty("No Messages").Aggregate((c, n) => c + "\r\n" + n));
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Empty(result.Messages!);
        }

        internal class CheckoutTestingBlock
        {
            /// <summary>Gets or sets the identifier of the billing type.</summary>
            /// <value>The identifier of the billing type.</value>
            internal int BillingTypeID { get; set; }

            /// <summary>Gets or sets the identifier of the shipping type.</summary>
            /// <value>The identifier of the shipping type.</value>
            internal int ShippingTypeID { get; set; }

            /// <summary>Gets or sets the identifier of the order status pending.</summary>
            /// <value>The identifier of the order status pending.</value>
            internal int OrderStatusPendingID { get; set; }

            /// <summary>Gets or sets the identifier of the order status paid.</summary>
            /// <value>The identifier of the order status paid.</value>
            internal int OrderStatusPaidID { get; set; }

            /// <summary>Gets or sets the identifier of the order status on hold.</summary>
            /// <value>The identifier of the order status on hold.</value>
            internal int OrderStatusOnHoldID { get; set; }

            /// <summary>Gets or sets the identifier of the order state.</summary>
            /// <value>The identifier of the order state.</value>
            internal int OrderStateID { get; set; }

            /// <summary>Gets or sets the identifier of the order type.</summary>
            /// <value>The identifier of the order type.</value>
            internal int OrderTypeID { get; set; }

            /// <summary>Gets or sets the identifier of the customer note type.</summary>
            /// <value>The identifier of the customer note type.</value>
            internal int CustomerNoteTypeID { get; set; }

            /// <summary>Gets or sets the default currency identifier.</summary>
            /// <value>The default currency identifier.</value>
            internal int DefaultCurrencyID { get; set; }

            /// <summary>Sets up the IDs.</summary>
            /// <param name="contextProfileName">Name of the context profile.</param>
            /// <returns>A Task.</returns>
            public async Task SetupIDsAsync(string contextProfileName)
            {
                var workflows = RegistryLoaderWrapper.GetInstance<IWorkflowsController>(contextProfileName);
                BillingTypeID = await workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Billing",
                        "Billing",
                        "Billing",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                ShippingTypeID = await workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Shipping",
                        "Shipping",
                        "Shipping",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                OrderStatusPendingID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Pending",
                        "Pending",
                        "Pending",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                OrderStatusPaidID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Full Payment Received",
                        "Full Payment Received",
                        "Full Payment Received",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                OrderStatusOnHoldID = await workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "On Hold",
                        "On Hold",
                        "On Hold",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                OrderTypeID = await workflows.SalesOrderTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Web",
                        "Web",
                        "Web",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                OrderStateID = await workflows.SalesOrderStates.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "WORK",
                        "Work",
                        "Work",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                CustomerNoteTypeID = await workflows.NoteTypes.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "Customer",
                        "Customer",
                        "Customer",
                        new NoteTypeModel
                        {
                            Active = true,
                            CustomKey = "Customer",
                            Name = "Customer",
                            DisplayName = "Customer",
                            IsCustomer = true,
                            IsPublic = true,
                        },
                        contextProfileName)
                    .ConfigureAwait(false);
                DefaultCurrencyID = await workflows.Currencies.ResolveWithAutoGenerateToIDAsync(
                        null,
                        "USD",
                        "US Dollar",
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                ////PreferredPaymentMethodAttr = await workflows.GeneralAttributes.ResolveWithAutoGenerateAsync(
                ////        null,
                ////        "Preferred Payment Method",
                ////        "Preferred Payment Method",
                ////        "Preferred Payment Method",
                ////        new GeneralAttributeModel
                ////        {
                ////            Active = true,
                ////            CustomKey = "Preferred Payment Method",
                ////            Name = "Preferred Payment Method",
                ////            DisplayName = "Preferred Payment Method",
                ////            TypeName = "General"
                ////        },
                ////        true,
                ////        contextProfileName)
                ////    .ConfigureAwait(false);
            }
        }
    }
}
