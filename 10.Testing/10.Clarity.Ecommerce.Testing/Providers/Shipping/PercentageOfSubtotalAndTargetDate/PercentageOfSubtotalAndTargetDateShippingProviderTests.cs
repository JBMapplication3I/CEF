// <copyright file="RedemptionPlusShippingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the RedemptionPlus shipping provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Shipping.PercentageOfSubtotalAndTargetDate;
    using Interfaces.DataModel;
    using Models;
    using Moq;
    using StructureMap;
    using StructureMap.Pipeline;
    using Utilities;
    using Xunit;

    [Trait("Category", "Providers.Shipping.RedemptionPlus")]
    public class PercentageOfSubtotalAndTargetDateShippingProviderTests : XUnitLogHelper
    {
        private readonly AddressModel Origin = new AddressModel
        {
            Company = "Clarity Ventures",
            Street1 = "6805 N Capital of Texas Hwy",
            City = "Austin",
            RegionCode = "TX",
            PostalCode = "78759",
            CountryCode = "USA"
        };

        private readonly AddressModel Destination = new AddressModel
        {
            Company = "Clarity Ventures",
            Street1 = "6805 N Capital of Texas Hwy",
            City = "Austin",
            RegionCode = "TX",
            PostalCode = "78759",
            CountryCode = "USA"
        };

        public PercentageOfSubtotalAndTargetDateShippingProviderTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Do Not Run Automatically")]
        public async Task Test_GetRateQuotes()
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartItemTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoDiscountTable = true,
                    DoManufacturerProductTable = true,
                    DoPackageTable = true,
                    DoProductCategoryTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                    DoSettingGroupTable = true,
                    DoSettingTable = true,
                    DoSettingTypeTable = true,
                    DoShipCarrierMethodTable = true,
                    DoShipCarrierTable = true,
                    DoStoreProductTable = true,
                    DoUserTable = true,
                    DoVendorProductTable = true,
                };
                var contextProfileName = await SetupContainerAsync(mockingSetup, childContainer).ConfigureAwait(false);
                var provider = new PercentageOfSubtotalAndTargetDateShippingProvider();
                Assert.True(provider.HasValidConfiguration);
                const int cartID = 1;
                var cartItems = new List<int> { 11, 12, 13 };
                var originContact = new ContactModel { Address = Origin };
                var destinationContact = new ContactModel { Address = Destination };
                // Act
                var rateQuotes = await provider.GetRateQuotesAsync(
                        cartID,
                        cartItems,
                        originContact,
                        destinationContact,
                        contextProfileName)
                    .ConfigureAwait(false);
                RegistryLoader.RemoveOverrideContainer(contextProfileName);
                // Assert
                Assert.NotNull(rateQuotes);
                Assert.True(rateQuotes.ActionSucceeded, rateQuotes.Messages.DefaultIfEmpty("No error message supplied").Aggregate((c, n) => c + "\r\n" + n));
                Assert.Empty(rateQuotes.Messages);
                Assert.NotNull(rateQuotes.Result);
                Assert.Equal(4, rateQuotes.Result.Count);
                // Rate 0
                var index = 0;
                Assert.Equal("Standard US Continental (3 Day) - 9%", rateQuotes.Result[index].Name);
                Assert.Equal(18.45m, rateQuotes.Result[index].Rate);
                Assert.NotNull(rateQuotes.Result[index].TargetShippingDate);
                Assert.NotNull(rateQuotes.Result[index].EstimatedDeliveryDate);
                Assert.Equal(new DateTime(2019, 12, 05).ToUniversalTime(), rateQuotes.Result[index].TargetShippingDate.Value.Date.ToUniversalTime());
                Assert.Equal(new DateTime(2019, 12, 09).ToUniversalTime(), rateQuotes.Result[index].EstimatedDeliveryDate.Value.Date.ToUniversalTime());
                // Rate 1
                index = 1;
                Assert.Equal("Standard US Continental (3 Day) - 9% - PREFERRED DELIVERY DAY", rateQuotes.Result[index].Name);
                Assert.Equal(18.45m, rateQuotes.Result[index].Rate);
                Assert.NotNull(rateQuotes.Result[index].TargetShippingDate);
                Assert.NotNull(rateQuotes.Result[index].EstimatedDeliveryDate);
                Assert.Equal(new DateTime(2019, 12, 06).ToUniversalTime(), rateQuotes.Result[index].TargetShippingDate.Value.Date.ToUniversalTime());
                Assert.Equal(new DateTime(2019, 12, 10).ToUniversalTime(), rateQuotes.Result[index].EstimatedDeliveryDate.Value.Date.ToUniversalTime());
                // Rate 2
                index = 2;
                Assert.Equal("Expedited US (2 Day) - 50%", rateQuotes.Result[index].Name);
                Assert.Equal(102.50m, rateQuotes.Result[index].Rate);
                Assert.NotNull(rateQuotes.Result[index].TargetShippingDate);
                Assert.NotNull(rateQuotes.Result[index].EstimatedDeliveryDate);
                Assert.Equal(new DateTime(2019, 12, 05).ToUniversalTime(), rateQuotes.Result[index].TargetShippingDate.Value.Date.ToUniversalTime());
                Assert.Equal(new DateTime(2019, 12, 07).ToUniversalTime(), rateQuotes.Result[index].EstimatedDeliveryDate.Value.Date.ToUniversalTime());
                // Rate 3
                index = 3;
                Assert.Equal("Rush US (1 Day) - 100%", rateQuotes.Result[index].Name);
                Assert.Equal(205.00m, rateQuotes.Result[index].Rate);
                Assert.NotNull(rateQuotes.Result[index].TargetShippingDate);
                Assert.NotNull(rateQuotes.Result[index].EstimatedDeliveryDate);
                Assert.Equal(new DateTime(2019, 12, 05).ToUniversalTime(), rateQuotes.Result[index].TargetShippingDate.Value.Date.ToUniversalTime());
                Assert.Equal(new DateTime(2019, 12, 06).ToUniversalTime(), rateQuotes.Result[index].EstimatedDeliveryDate.Value.Date.ToUniversalTime());
            }
        }

        [Fact(Skip = "Do Not Run Automatically")]
        public void SeedDatabase()
        {
            using (var context = new ClarityEcommerceEntities())
            {
                var now = DateExtensions.GenDateTime;
                var attributeTypeID = Contract.RequiresValidID(
                    context.AttributeTypes
                        .AsNoTracking()
                        .Where(x => x.Active && x.CustomKey == "ACCOUNT")
                        .Select(x => x.ID)
                        .SingleOrDefault(),
                    "Unable to retrieve AttributeType with CustomKey 'ACCOUNT'");
                var attributeID = context.GeneralAttributes
                    .AsNoTracking()
                    .Where(x => x.Active && x.CustomKey == "PreferredDeliveryDay")
                    .Select(x => x.ID)
                    .FirstOrDefault();
                if (Contract.CheckInvalidID(attributeID))
                {
                    var attribute = new GeneralAttribute
                    {
                        Active = true,
                        CreatedDate = now,
                        CustomKey = "PreferredDeliveryDay",
                        Name = "PreferredDeliveryDay",
                        DisplayName = "Preferred Delivery Day",
                        Description = "Defines a day of the week that is the preferred delivery day.",
                        IsPredefined = true,
                        TypeID = attributeTypeID,
                    };
                    context.GeneralAttributes.Add(attribute);
                    context.SaveUnitOfWork(true);
                    attributeID = attribute.ID;
                }
                AddDayOfWeekPredefinedOption(DayOfWeek.Sunday, attributeID, now, context);
                AddDayOfWeekPredefinedOption(DayOfWeek.Monday, attributeID, now, context);
                AddDayOfWeekPredefinedOption(DayOfWeek.Tuesday, attributeID, now, context);
                AddDayOfWeekPredefinedOption(DayOfWeek.Wednesday, attributeID, now, context);
                AddDayOfWeekPredefinedOption(DayOfWeek.Thursday, attributeID, now, context);
                AddDayOfWeekPredefinedOption(DayOfWeek.Friday, attributeID, now, context);
                AddDayOfWeekPredefinedOption(DayOfWeek.Saturday, attributeID, now, context);
            }
        }

        private static void AddDayOfWeekPredefinedOption(
            DayOfWeek dayOfWeek,
            int attributeID,
            DateTime now,
            IClarityEcommerceEntities context)
        {
            var dayOfWeekName = dayOfWeek.ToString();
            if (context.GeneralAttributePredefinedOptions.Any(x => x.CustomKey == dayOfWeekName))
            {
                return;
            }
            context.GeneralAttributePredefinedOptions.Add(new GeneralAttributePredefinedOption
            {
                Active = true,
                CreatedDate = now,
                CustomKey = dayOfWeekName,
                Value = dayOfWeekName,
                SortOrder = (int)dayOfWeek,
                AttributeID = attributeID
            });
            context.SaveUnitOfWork(true);
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
