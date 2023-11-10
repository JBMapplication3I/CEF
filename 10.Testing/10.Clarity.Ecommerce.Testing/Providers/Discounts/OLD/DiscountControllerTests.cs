// <copyright file="DiscountControllerTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount controller tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Discounts;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using StructureMap.Pipeline;
    using Xunit;

    [Trait("Category", "Providers.Discounts.Controllers")]
    public class DiscountControllerTests : XUnitLogHelper
    {
        public DiscountControllerTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async void Verify_AddDiscountAsync_Works()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            await RunWithSetupAndTearDownAsync(
                new MockingSetup
                {
                    DoDiscountCodeTable = true,
                    DoDiscountTable = true,
                },
                async (discountController, context, contextProfileName) =>
                {
                    var cart = new CartModel
                    {
                        ID = 1,
                        SalesItems = new List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>(),
                        Discounts = new List<AppliedCartDiscountModel>(),
                        Totals = new CartTotals(),
                    };
                    const string code = "ABCD1234";
                    // Act
                    var result = await discountController.AddDiscountAsync(
                            cart,
                            code,
                            null,
                            context)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.True(result.ActionSucceeded, result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_AddDiscountAsync_ForACodeThatDoesntExist_Returns_AFailingCEFARWithSpecificMessage()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            await RunWithSetupAndTearDownAsync(
                new MockingSetup
                {
                    DoDiscountCodeTable = true,
                },
                async (discountController, context, contextProfileName) =>
                {
                    var cart = new CartModel
                    {
                        SalesItems = new List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>(),
                        Discounts = new List<AppliedCartDiscountModel>(),
                        Totals = new CartTotals(),
                    };
                    const string code = "Doesn't Exist";
                    // Act
                    var result = await discountController.AddDiscountAsync(
                            cart,
                            code,
                            null,
                            context)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.False(result.ActionSucceeded);
                    Assert.Equal("Unable to add discount \"Doesn't Exist\". No discount found for this code", result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_AddDiscountAsync_ForAnythingThatWouldThrowAnException_Returns_AFailingCEFARWithSpecificMessage()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            await RunWithSetupAndTearDownAsync(
                new MockingSetup(),
                async (discountController, context, contextProfileName) =>
                {
                    var cart = new CartModel
                    {
                        SalesItems = new List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>(),
                        Discounts = new List<AppliedCartDiscountModel>(),
                        Totals = new CartTotals(),
                    };
                    const string code = "Doesn't Exist";
                    // Act
                    var result = await discountController.AddDiscountAsync(
                            cart,
                            code,
                            null,
                            context)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.False(result.ActionSucceeded);
                    Assert.Equal("Unable to add discount \"Doesn't Exist\". An Exception occurred", result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_VerifyCurrentDiscountsAsync_Works()
        {
            // Arrange
            await RunWithSetupAndTearDownAsync(
                new MockingSetup
                {
                    DoDiscountTable = true,
                    DoDiscountUserTable = true,
                },
                async (discountController, context, contextProfileName) =>
                {
                    var cart = new CartModel
                    {
                        ID = 1,
                        SalesItems = new List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>(),
                        Discounts = new List<AppliedCartDiscountModel>(),
                        RateQuotes = new List<RateQuoteModel>(),
                        Totals = new CartTotals(),
                    };
                    // Act
                    var result = await discountController.VerifyCurrentDiscountsAsync(
                            cart,
                            null,
                            contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.True(result.ActionSucceeded, result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_VerifyCurrentDiscountsAsync_ForAnythingThatWouldThrowAnException_Returns_AFailingCEFARWithSpecificMessage()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            await RunWithSetupAndTearDownAsync(
                new MockingSetup(),
                async (discountController, context, contextProfileName) =>
                {
                    var cart = new CartModel
                    {
                        SalesItems = new List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>(),
                        Discounts = new List<AppliedCartDiscountModel>(),
                        RateQuotes = new List<RateQuoteModel>(),
                        Totals = new CartTotals(),
                    };
                    // Act
                    var result = await discountController.VerifyCurrentDiscountsAsync(
                            cart,
                            null,
                            contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.False(result.ActionSucceeded);
                    Assert.Equal("An Exception occurred", result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_VerifyCurrentDiscountsAsync_ForACartWithDiscountsToBeRemoved_ShouldSave_ReturnsAPassingCEFAR()
        {
            // Arrange
            await RunWithSetupAndTearDownAsync(
                new MockingSetup
                {
                    DoDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoProductTable = true,
                },
                async (discountController, context, contextProfileName) =>
                {
                    var product = context.Products.Single(x => x.ID == 1151);
                    var cart = new CartModel
                    {
                        ID = 1,
                        SalesItems = new List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>
                        {
                            new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                            {
                                ID = 1,
                                Active = true,
                                CreatedDate = DateTime.Today,
                                MasterID = 1,
                                ProductID = 1151,
                                UnitCorePrice = product.PriceBase ?? 1151m,
                                UnitSoldPrice = product.PriceSale,
                                Discounts = new List<AppliedCartItemDiscountModel>
                                {
                                    new AppliedCartItemDiscountModel
                                    {
                                        ID = 1,
                                        Active = true,
                                        CreatedDate = DateTime.Today,
                                        MasterID = 1,
                                        SlaveID = 1,
                                    }
                                },
                            },
                        },
                        Discounts = new List<AppliedCartDiscountModel>
                        {
                            new AppliedCartDiscountModel
                            {
                                ID = 1,
                                Active = true,
                                CreatedDate = DateTime.Today,
                                MasterID = 1,
                                SlaveID = 1,
                            },
                        },
                        RateQuotes = new List<RateQuoteModel>(),
                        Totals = new CartTotals
                        {
                            SubTotal = product.PriceSale ?? product.PriceBase ?? 1151m
                        }
                    };
                    // Act
                    var result = await discountController.VerifyCurrentDiscountsAsync(
                            cart,
                            null,
                            contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.True(result.ActionSucceeded, result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_RemoveDiscountAsync_Works()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            await RunWithSetupAndTearDownAsync(
                new MockingSetup
                {
                    DoAppliedCartItemDiscountTable = true,
                },
                async (discountController, context, contextProfileName) =>
                {
                    var discountID = 1;
                    // Act
                    var result = await discountController.RemoveDiscountAsync<AppliedCartItemDiscount>(
                            discountID,
                            context)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.True(result.ActionSucceeded, result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_RemoveDiscountAsync_ForAnythingThatWouldThrowAnException_Returns_AFailingCEFARWithSpecificMessage()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            await RunWithSetupAndTearDownAsync(
                new MockingSetup(),
                async (discountController, context, contextProfileName) =>
                {
                    var discountID = 1;
                    // Act
                    var result = await discountController.RemoveDiscountAsync<AppliedCartItemDiscount>(
                            discountID,
                            context)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.IsAssignableFrom<CEFActionResponse>(result);
                    Assert.False(result.ActionSucceeded);
                    Assert.Equal("An Exception occurred", result.Messages.FirstOrDefault());
                })
                .ConfigureAwait(false);
        }

        [Fact]
        public async void Verify_RemoveDiscountAsync_ForADiscountThatDoesntExist_Returns_AFailingCEFAR()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            await RunWithSetupAndTearDownAsync(
                    new MockingSetup
                    {
                        DoAppliedCartItemDiscountTable = true,
                    },
                    async (discountController, context, contextProfileName) =>
                    {
                        var discountID = int.MaxValue - 1;
                        // Act
                        var result = await discountController.RemoveDiscountAsync<AppliedCartItemDiscount>(
                                discountID,
                                context)
                            .ConfigureAwait(false);
                        // Assert
                        Assert.IsAssignableFrom<CEFActionResponse>(result);
                        Assert.False(result.ActionSucceeded);
                        Assert.Equal("Discount not found", result.Messages.FirstOrDefault());
                    })
                .ConfigureAwait(false);
        }

        private async Task RunWithSetupAndTearDownAsync(
            [NotNull] MockingSetup mockingSetup,
            [NotNull] Func<IDiscountController, IClarityEcommerceEntities, string, Task> task,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string memberName = "")
        {
            var contextProfileName = $"{sourceFilePath}|{memberName}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                RegistryLoader.RootContainer.Configure(x => x.For<ILogger>().UseInstance(
                    new ObjectInstance(new Logger
                    {
                        ExtraLogger = s =>
                        {
                            try
                            {
                                TestOutputHelper.WriteLine(s);
                            }
                            catch
                            {
                                // Do nothing
                            }
                        },
                    })));
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await task(new DiscountController(), mockingSetup.MockContext.Object, contextProfileName).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
