// <copyright file="BasicMembershipProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the basic membership provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Memberships;
    using Ecommerce.Providers.Memberships.Basic;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Moq;
    using StructureMap.Pipeline;
    using Xunit;
    using IContainer = StructureMap.IContainer;

    [Trait("Category", "Providers.Memberships.Basic")]
    public class BasicMembershipProviderTests : XUnitLogHelper
    {
        public BasicMembershipProviderTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAKit_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAdZoneAccessTable = true,
                    DoMembershipAdZoneAccessByLevelTable = true,
                    DoMembershipAdZoneAccessTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipRepeatTypeTable = true,
                    DoMembershipTable = true,
                    DoPackageTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductMembershipLevelTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRepeatTypeTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                    DoZoneTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.UserID = null;
                salesOrderItem.ProductID = 500000;
                salesOrderItem.ProductTypeID = 2;
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                Assert.Equal(Enums.ProviderType.Memberships, provider.ProviderType);
                Assert.True(provider.HasDefaultProvider);
                Assert.True(provider.IsDefaultProvider);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    new PricingFactoryContextModel { UserID = 1 },
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAKit_WithNoUniqueAdLimitThresholds_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAdZoneAccessTable = true,
                    ////DoMembershipAdZoneAccessByLevelTable = true,
                    DoMembershipAdZoneAccessTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipRepeatTypeTable = true,
                    DoMembershipTable = true,
                    DoPackageTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductMembershipLevelTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRepeatTypeTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                    DoZoneTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.UserID = null;
                salesOrderItem.ProductID = 700000;
                salesOrderItem.ProductTypeID = 2;
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    new PricingFactoryContextModel { UserID = 1 },
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAMembership_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipRepeatTypeTable = true,
                    DoPackageTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductMembershipLevelTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRepeatTypeTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.UserID = null;
                salesOrderItem.ProductID = 600000;
                salesOrderItem.ProductTypeID = 6;
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    new PricingFactoryContextModel { UserID = 1 },
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAProductThatDoesntExist_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.ProductID = int.MaxValue - 1;
                salesOrderItem.ProductTypeID = mockingSetup.RawProductTypes.Single(x => x.CustomKey == "MEMBERSHIP").ID;
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    null,
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Could not locate product",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAProductThatHasNoMembershipLevels_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoPriceRuleAccountTable = true,
                    DoPriceRuleAccountTypeTable = true,
                    DoPriceRuleCategoryTable = true,
                    DoPriceRuleCountryTable = true,
                    DoPriceRuleManufacturerTable = true,
                    DoPriceRuleProductTable = true,
                    DoPriceRuleProductTypeTable = true,
                    DoPriceRuleStoreTable = true,
                    DoPriceRuleTable = true,
                    DoPriceRuleUserRoleTable = true,
                    DoPriceRuleVendorTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.ProductID = 600002;
                salesOrderItem.ProductTypeID = mockingSetup.ProductTypes.Object.Single(x => x.CustomKey == "MEMBERSHIP").ID;
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    new PricingFactoryContextModel { UserID = 1 },
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Product does not contain membership definitions",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAProductThatHasNoMembershipAdZoneAccessByLevels_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipRepeatTypeTable = true,
                    DoProductMembershipLevelTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRepeatTypeTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.ProductID = 600002;
                salesOrderItem.ProductTypeID = mockingSetup.ProductTypes.Object.Single(x => x.CustomKey == "MEMBERSHIP").ID;
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    new PricingFactoryContextModel { UserID = 1 },
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Empty(response.Messages);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAProductThatDoesntQuality_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var contextProfileName = await SetupContainerAsync(new MockingSetup { SaveChangesResult = 1, DoProductTypeTable = true, }, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.ProductID = 1152;
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    null,
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "No action taken as product is not a membership or kit that contains a membership",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_WithNoSalesOrderItem_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var contextProfileName = await SetupContainerAsync(new MockingSetup(), childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    null,
                    null,
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "No Product to read an ID from",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAMembership_ThatShouldUpgrade_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAdZoneAccessTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipRepeatTypeTable = true,
                    DoProductMembershipLevelTable = true,
                    DoProductTable = true,
                    DoRepeatTypeTable = true,
                    DoSalesInvoiceTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                    DoZoneTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.UserID = null;
                salesOrderItem.ProductID = 600000;
                salesOrderItem.ProductTypeID = 6;
                salesOrderItem.SerializableAttributes = new SerializableAttributesDictionary
                {
                    ["upgrade_membership"] = new SerializableAttributeObject
                    {
                        Key = "upgrade_membership",
                        Value = 2.ToString() // Previous Subscription ID
                    }
                };
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                // Act
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>(),
                    1,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAMembership_ThatCantParseTheOldID_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.UserID = null;
                salesOrderItem.ProductID = 600000;
                salesOrderItem.ProductTypeID = 6;
                salesOrderItem.SerializableAttributes = new SerializableAttributesDictionary
                {
                    ["upgrade_membership"] = new SerializableAttributeObject
                    {
                        Key = "upgrade_membership",
                        Value = "two" // Previous Subscription ID which can't parse
                    },
                };
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                // Act
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>(),
                    1,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Could not read old subscription ID to mark for upgrade.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_ImplementProductMembershipFromOrderItem_ForAMembership_ThatShouldUpgrade_ButDoesntMatchASubscription_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var salesOrderItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel>>();
                salesOrderItem.UserID = null;
                salesOrderItem.ProductID = 600000;
                salesOrderItem.ProductTypeID = 6;
                salesOrderItem.SerializableAttributes = new SerializableAttributesDictionary
                {
                    ["upgrade_membership"] = new SerializableAttributeObject
                    {
                        Key = "upgrade_membership",
                        Value = 40.ToString() // Previous Subscription ID
                    }
                };
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.ImplementProductMembershipFromOrderItemAsync(
                    1,
                    1,
                    salesOrderItem,
                    null,
                    null,
                    DateTime.Today,
                    contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Must supply the identifier of an existing subscription to upgrade",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInUpgradePeriod_WithAnIDThatDoesntExist_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.Today.AddDays(CEFConfigDictionary.MembershipsUpgradePeriodBlackout * -1 - 1);
                var response = await provider.IsSubscriptionInUpgradePeriodAsync(1000000, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "ERROR! Unable to locate subscription",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInUpgradePeriod_WithAnEndDateBeyondTheMax_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.Today.AddDays(CEFConfigDictionary.MembershipsUpgradePeriodBlackout * -1 - 1);
                var response = await provider.IsSubscriptionInUpgradePeriodAsync(1, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    $"The subscription cannot be changed within {CEFConfigDictionary.MembershipsUpgradePeriodBlackout} days of the end.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInUpgradePeriod_WithAnEndDateBeforeTheMax_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.Today.AddDays(CEFConfigDictionary.MembershipsUpgradePeriodBlackout * -1 - 1);
                var response = await provider.IsSubscriptionInUpgradePeriodAsync(9, contextProfileName).ConfigureAwait(false);
                Assert.True(response.ActionSucceeded, response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Empty(response.Messages);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInUpgradePeriod_WithANullEndDate_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = null;
                var response = await provider.IsSubscriptionInUpgradePeriodAsync(6, contextProfileName).ConfigureAwait(false);
                Assert.True(response.ActionSucceeded, response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Empty(response.Messages);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInUpgradePeriod_WithAMinValueEndDate_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.MinValue;
                var response = await provider.IsSubscriptionInUpgradePeriodAsync(8, contextProfileName).ConfigureAwait(false);
                Assert.True(response.ActionSucceeded, response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Empty(response.Messages);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInUpgradePeriod_WithAMaxValueEndDate_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.MaxValue;
                var response = await provider.IsSubscriptionInUpgradePeriodAsync(7, contextProfileName).ConfigureAwait(false);
                Assert.True(response.ActionSucceeded, response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Empty(response.Messages);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInRenewalPeriod_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.Today;
                var response = await provider.IsSubscriptionInRenewalPeriodAsync(5, contextProfileName).ConfigureAwait(false);
                Assert.True(response.ActionSucceeded, response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Empty(response.Messages);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInRenewalPeriod_WithAnIDThatDoesntExist_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.IsSubscriptionInRenewalPeriodAsync(int.MaxValue - 1, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "ERROR! Unable to locate subscription",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInRenewalPeriod_WithAnEndDateBeyondTheMax_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.Today.AddDays(CommonMembershipProviderConfig.RenewalPeriodBefore * -1 - 1);
                var response = await provider.IsSubscriptionInRenewalPeriodAsync(1, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "This subscription is outside the renewal period and cannot be modified.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInRenewalPeriod_WithAnEndDateBeyondTheMin_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.Today.AddDays(CommonMembershipProviderConfig.RenewalPeriodAfter + 1);
                var response = await provider.IsSubscriptionInRenewalPeriodAsync(1, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "This subscription is outside the renewal period and cannot be modified.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInRenewalPeriod_WithANullEndDate_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = null;
                var response = await provider.IsSubscriptionInRenewalPeriodAsync(6, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Equal(
                    "No End Date specified for subscription, cannot renew.",
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInRenewalPeriod_WithAMinValueEndDate_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                ////var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
                ////subscription.EndsOn = DateTime.MinValue;
                var response = await provider.IsSubscriptionInRenewalPeriodAsync(8, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Equal(
                    "No End Date specified for subscription, cannot renew.",
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_IsSubscriptionInRenewalPeriod_WithAMaxValueEndDate_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.IsSubscriptionInRenewalPeriodAsync(7, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "No End Date specified for subscription, cannot renew.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_RenewMembership_WithAFee_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAdZoneAccessTable = true,
                    DoMembershipAdZoneAccessByLevelTable = true,
                    DoMembershipAdZoneAccessTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipRepeatTypeTable = true,
                    DoMembershipTable = true,
                    DoPackageTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductMembershipLevelTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRepeatTypeTable = true,
                    DoSalesInvoiceTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                    DoZoneTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.RenewMembershipAsync(
                    2,
                    600000,
                    1,
                    new DateTime(2020, 1, 1),
                    5.99m,
                    contextProfileName).ConfigureAwait(false);
                Assert.True(response.ActionSucceeded, response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Set up ad zone: Zone 1",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_RenewMembership_WithAPricingFactory_Returns_APassingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAdZoneAccessTable = true,
                    DoMembershipAdZoneAccessByLevelTable = true,
                    DoMembershipAdZoneAccessTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipRepeatTypeTable = true,
                    DoMembershipTable = true,
                    DoPackageTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductMembershipLevelTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRepeatTypeTable = true,
                    DoSalesInvoiceTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                    DoZoneTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.RenewMembershipAsync(
                    2,
                    600000,
                    1,
                    new DateTime(2020, 1, 1),
                    new PricingFactoryContextModel { UserID = 1 },
                    contextProfileName).ConfigureAwait(false);
                Assert.True(response.ActionSucceeded, response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Set up ad zone: Zone 1",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_RenewMembership_WithAFee_ForASubscriptionThatDoesntExist_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.RenewMembershipAsync(
                    int.MaxValue - 1,
                    600000,
                    1,
                    new DateTime(2020, 1, 1),
                    5.99m,
                    contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Could not locate previous subscription.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_RenewMembership_WithAPricingFactory_ForASubscriptionThatDoesntExist_Returns_AFailingCEFAR()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                Assert.True(provider.HasValidConfiguration);
                var response = await provider.RenewMembershipAsync(
                    int.MaxValue - 1,
                    600000,
                    1,
                    new DateTime(2020, 1, 1),
                    new PricingFactoryContextModel { UserID = 1 },
                    contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "Could not locate previous subscription.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public void Verify_MembershipCalculateDate_ProcessesDataCorrectly()
        {
            var timestamp = DateExtensions.GenDateTime;
            var testData = new[]
            {
                new { RepeatTypeKey = "13 Months", BillingPeriods = 1, Result = timestamp.AddMonths(13) },
                new { RepeatTypeKey = "13Months", BillingPeriods = 1, Result = timestamp.AddMonths(13) },
                new { RepeatTypeKey = "13", BillingPeriods = 1, Result = timestamp.AddMonths(13) },
                new { RepeatTypeKey = "13-months", BillingPeriods = 1, Result = timestamp.AddMonths(13) },
                new { RepeatTypeKey = "13-month", BillingPeriods = 1, Result = timestamp.AddMonths(13) },
                new { RepeatTypeKey = "thirteen-month", BillingPeriods = 1, Result = timestamp.AddMonths(13) },
                new { RepeatTypeKey = "Yearly", BillingPeriods = 1, Result = timestamp.AddYears(1) },
                new { RepeatTypeKey = "1 year", BillingPeriods = 1, Result = timestamp.AddYears(1) },
                new { RepeatTypeKey = "2 years", BillingPeriods = 1, Result = timestamp.AddMonths(24) },
                new { RepeatTypeKey = "Monthly", BillingPeriods = 1, Result = timestamp.AddMonths(1) },
                new { RepeatTypeKey = "quarterly", BillingPeriods = 1, Result = timestamp.AddMonths(3) },
                new { RepeatTypeKey = "semi-annual", BillingPeriods = 1, Result = timestamp.AddMonths(6) },
                new { RepeatTypeKey = "semiannual", BillingPeriods = 1, Result = timestamp.AddMonths(6) },
                new { RepeatTypeKey = "semiyearly", BillingPeriods = 1, Result = timestamp.AddMonths(6) },
                new { RepeatTypeKey = "annual", BillingPeriods = 1, Result = timestamp.AddYears(1) },
                new { RepeatTypeKey = "annually.", BillingPeriods = 1, Result = timestamp.AddYears(1) },
                new { RepeatTypeKey = "bimonthly", BillingPeriods = 1, Result = timestamp.AddMonths(2) },
                new { RepeatTypeKey = "bimonthly", BillingPeriods = 0, Result = timestamp.AddMonths(2) },
                new { RepeatTypeKey = "1", BillingPeriods = 1, Result = timestamp.AddMonths(1) },
                new { RepeatTypeKey = "semimonthly", BillingPeriods = 1, Result = timestamp },
            };
            foreach (var test in testData)
            {
                Assert.Equal(
                    test.Result,
                    MembershipProviderBase.MembershipCalculateDate(timestamp, test.RepeatTypeKey, test.BillingPeriods));
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_CancelMembership_WithASubscriptionIDNotInTheData_Returns_AFailingCEFAR()
        {
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                var response = await provider.CancelMembershipAsync(500, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "ERROR! Could not find the subscription to cancel.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_CancelMembership_WithAnInvalidSubscriptionID_Returns_AFailingCEFAR()
        {
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                var response = await provider.CancelMembershipAsync(0, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "ERROR! Invalid subscription identifier.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_CancelMembership_WithAnExpiredSubscription_Returns_AFailingCEFAR()
        {
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSubscriptionTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                var response = await provider.CancelMembershipAsync(3, contextProfileName).ConfigureAwait(false);
                Assert.False(response.ActionSucceeded);
                Assert.Single(response.Messages);
                Assert.Equal(
                    "ERROR! Cannot cancel a subscription that has already expired.",
                    response.Messages.Single());
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        [Fact(Skip = "Don't run automatically.")]
        public async Task Verify_CancelMembership_Returns_APassingCEFAR()
        {
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAccountTable = true,
                    DoAdZoneAccessTable = true,
                    DoAdZoneTable = true,
                    DoMembershipLevelTable = true,
                    DoMembershipAdZoneAccessTable = true,
                    DoMembershipAdZoneAccessByLevelTable = true,
                    DoProductMembershipLevelTable = true,
                    DoRepeatTypeTable = true,
                    DoRoleUserTable = true,
                    DoSalesInvoiceTable = true,
                    DoSubscriptionStatusTable = true,
                    DoSubscriptionTable = true,
                    DoSubscriptionTypeTable = true,
                    DoUserRoleTable = true,
                    DoUserTable = true,
                    DoZoneTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new BasicMembershipProvider();
                var response = await provider.CancelMembershipAsync(1, contextProfileName).ConfigureAwait(false);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.Empty(response.Messages);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
            }
        }

        private async Task<string> SetupContainerAsync(
            MockingSetup mockingSetup,
            IContainer childContainer,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string memberName = "")
        {
            var contextProfileName = $"{sourceFilePath}|{memberName}";
            RegistryLoader.RootContainer.Configure(x => x.For<ILogger>().UseInstance(
                new ObjectInstance(new Logger { ExtraLogger = s => TestOutputHelper.WriteLine(s) })));
            await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
            var mockRoleManager = new Mock<ICEFRoleManager>();
            mockRoleManager.Setup(m => m.Roles).Returns(() => mockingSetup.MockContext.Object.Roles);
            var mockUserStore = new Mock<ICEFUserStore>();
            var mockUserManager = new Mock<ICEFUserManager>().SetupAllProperties();
            mockUserManager.Setup(m => m.GetUserRolesAsync(It.IsAny<int>())).Returns(
                (int id) => mockingSetup.MockContext.Object.RoleUsers
                    .Where(q => q.UserId == id)
                    .Select(q => q.Role.Name)
                    .ToListAsync());
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                x.For<ICEFRoleManager>().Use(() => mockRoleManager.Object);
                x.For<ICEFUserManager>().Use(() => mockUserManager.Object);
                x.For<ICEFUserStore>().Use(() => mockUserStore.Object);
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
            return contextProfileName;
        }
    }
}
