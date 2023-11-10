// <copyright file="BuyXGetYDiscountValidatorTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the buy x coordinate get y discount validator tests class</summary>
// ReSharper disable PossibleInvalidOperationException
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Discounts;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Moq;
    using Xunit;

    [Trait("Category", "Providers.Discounts.Validators.BuyXGetY")]
    public class BuyXGetYDiscountValidatorTests : DiscountValidatorTestsBase
    {
        public BuyXGetYDiscountValidatorTests(Xunit.Abstractions.ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public Task ValidateDiscount_Invalid()
        {
            // Arrange
            var (_, _, _, _, _, _, _, _, salesItems, _) = Setup();
            var thresholdAmount = salesItems.Sum(x => x.UnitCorePrice * x.Quantity);
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act/Assert
            return Base_ValidateDiscount_InvalidAsync(buyXGetYDiscountValidator, thresholdAmount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var cartItemDiscount = new Mock<IAppliedCartItemDiscountModel>();
            cartItemDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            foreach (var salesItem in salesItems)
            {
                salesItem.Discounts.Add(cartItemDiscount.Object);
            }
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var cartItemDiscount = new Mock<IAppliedCartItemDiscountModel>();
            cartItemDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            foreach (var salesItem in salesItems)
            {
                salesItem.Discounts.Add(cartItemDiscount.Object);
            }
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_ZeroBuyXAmount()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.BuyXValue = 0;
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.False(response.ActionSucceeded);
            Assert.Equal("Invalid BuyXGetY values", response.Messages.First());
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_ZeroGetYAmount()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.GetYValue = 0;
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.False(response.ActionSucceeded);
            Assert.Equal("Invalid BuyXGetY values", response.Messages.First());
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount>(
                salesItems.Select(x => new AppliedCartItemDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = x.ID.Value,
                    Active = false,
                }));
            var appliedCartItemDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts.Object);
            var cartItemDiscount = new Mock<IAppliedCartItemDiscountModel>();
            cartItemDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            foreach (var salesItem in salesItems)
            {
                salesItem.Discounts.Add(cartItemDiscount.Object);
            }
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
            AssertUpdatesForAmountValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount>(
                salesItems.Select(x => new AppliedCartItemDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = x.ID.Value,
                    Active = false,
                }));
            var appliedCartItemDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts.Object);
            var cartItemDiscount = new Mock<IAppliedCartItemDiscountModel>();
            cartItemDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            foreach (var salesItem in salesItems)
            {
                salesItem.Discounts.Add(cartItemDiscount.Object);
            }
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
            AssertUpdatesForPercentValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, appliedCartItemDiscounts, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
            AssertAddsForAmountValueType(salesItems, appliedCartItemDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, appliedCartItemDiscounts, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
            AssertAddsForPercentValueType(salesItems, appliedCartItemDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount>(
                salesItems.Select(x => new AppliedCartItemDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = x.ID.Value,
                    Active = false,
                }));
            var appliedCartItemDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts.Object);
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
            AssertUpdatesForAmountValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount>(
                salesItems.Select(x => new AppliedCartItemDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = x.ID.Value,
                    DiscountTotal = 0,
                    Active = false,
                }));
            var appliedCartItemDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts.Object);
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.True(response.ActionSucceeded, response.Messages.FirstOrDefault());
            AssertUpdatesForPercentValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ThresholdAmount = salesItems.Sum(x => x.UnitCorePrice * x.Quantity);
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount>(
                salesItems.Select(x => new AppliedCartItemDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = x.ID.Value,
                    Active = true,
                }));
            var appliedCartItemDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts.Object);
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.False(response.ActionSucceeded);
            foreach (var appliedDiscount in context.Object.Set<AppliedCartItemDiscount>())
            {
                Assert.False(appliedDiscount.Active);
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, _) = Setup();
            discount.Object.ThresholdAmount = salesItems.Sum(x => x.UnitCorePrice * x.Quantity);
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            var buyXGetYDiscountValidator = new BuyXGetYDiscountValidator();
            // Act
            var validateDiscount = await buyXGetYDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    null,
                    discount.Object,
                    currentDiscounts,
                    salesItems,
                    null,
                    context.Object)
                .ConfigureAwait(false);
            // Assert
            var response = Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Assert.False(response.ActionSucceeded);
        }

        protected override (
            Mock<IDiscount> discount,
            List<IAppliedDiscountBaseModel> currentDiscounts,
            Mock<IClarityEcommerceEntities> context,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<DbSet<AppliedCartItemDiscount>> appliedCartItemDiscounts,
            Mock<DbSet<AppliedSalesOrderDiscount>> appliedSalesOrderDiscounts,
            Mock<DbSet<AppliedSalesOrderItemDiscount>> appliedSalesOrderItemDiscounts,
            DiscountProduct product,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            Mock<ICartModel> cart) Setup()
        {
            var discount = new Mock<IDiscount>();
            var currentDiscounts = new List<IAppliedDiscountBaseModel>();
            var context = new Mock<IClarityEcommerceEntities>();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet<AppliedCartDiscount>();
            var appliedCartItemDiscounts = SetupUtilities.GetMockAsyncDbSet<AppliedCartItemDiscount>();
            var appliedSalesOrderDiscounts = SetupUtilities.GetMockAsyncDbSet<AppliedSalesOrderDiscount>();
            var appliedSalesOrderItemDiscounts = SetupUtilities.GetMockAsyncDbSet<AppliedSalesOrderItemDiscount>();
            var salesItems = new List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            var product = new DiscountProduct();
            var cart = new Mock<ICartModel>();
            cart.SetupAllProperties();
            cart.Object.Totals = new CartTotals();
            SetupSalesItems(salesItems);
            SetupDiscount(salesItems, product, discount);
            SetupContext(
                context,
                appliedCartDiscounts,
                appliedCartItemDiscounts,
                appliedSalesOrderDiscounts,
                appliedSalesOrderItemDiscounts);
            return (discount,
                currentDiscounts,
                context,
                appliedCartDiscounts,
                appliedCartItemDiscounts,
                appliedSalesOrderDiscounts,
                appliedSalesOrderItemDiscounts,
                product,
                salesItems,
                cart);
        }

        protected override void SetupDiscount(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            DiscountProduct product,
            Mock<IDiscount> discount,
            int idOffset = 0)
        {
            base.SetupDiscount(salesItems, product, discount, idOffset);
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            discount.Object.Value = 25.00m;
            discount.Object.UsageLimit = salesItems.Count;
            discount.Object.BuyXValue = 4;
            discount.Object.GetYValue = 1;
        }

        private void SetupSalesItems(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            int idOffset = 0)
        {
            var salesItem1 = new Mock<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            var salesItem2 = new Mock<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            var salesItem3 = new Mock<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            var existingDiscount1 = new Mock<IAppliedCartItemDiscountModel>();
            var existingDiscount2 = new Mock<IAppliedCartItemDiscountModel>();
            var existingDiscount3 = new Mock<IAppliedCartItemDiscountModel>();
            salesItem1.SetupAllProperties();
            salesItem2.SetupAllProperties();
            salesItem3.SetupAllProperties();
            existingDiscount1.SetupAllProperties();
            existingDiscount2.SetupAllProperties();
            existingDiscount3.SetupAllProperties();
            salesItem1.Object.Discounts = new List<IAppliedCartItemDiscountModel>();
            salesItem2.Object.Discounts = new List<IAppliedCartItemDiscountModel>();
            salesItem3.Object.Discounts = new List<IAppliedCartItemDiscountModel>();
            salesItem1.Object.ID = 5_000 + idOffset;
            salesItem2.Object.ID = 5_001 + idOffset;
            salesItem3.Object.ID = 5_002 + idOffset;
            existingDiscount1.Object.DiscountID = 1_100 + idOffset;
            existingDiscount2.Object.DiscountID = 1_101 + idOffset;
            existingDiscount3.Object.DiscountID = 1_102 + idOffset;
            existingDiscount1.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            existingDiscount2.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            existingDiscount3.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            salesItem1.Object.Quantity = 05;
            salesItem2.Object.Quantity = 10;
            salesItem3.Object.Quantity = 15;
            salesItem1.Object.UnitCorePrice = 14.99m;
            salesItem2.Object.UnitCorePrice = 24.99m;
            salesItem3.Object.UnitCorePrice = 49.99m;
            salesItem1.Object.UnitSoldPrice = salesItem1.Object.UnitCorePrice;
            salesItem2.Object.UnitSoldPrice = salesItem2.Object.UnitCorePrice;
            salesItem3.Object.UnitSoldPrice = salesItem3.Object.UnitCorePrice;
            existingDiscount1.Object.DiscountTotal = salesItem1.Object.UnitSoldPrice.Value * 0.50m;
            existingDiscount2.Object.DiscountTotal = salesItem2.Object.UnitSoldPrice.Value * 0.25m;
            existingDiscount3.Object.DiscountTotal = salesItem3.Object.UnitSoldPrice.Value * salesItem3.Object.Quantity;
            salesItem1.Object.Discounts.Add(existingDiscount1.Object);
            salesItem2.Object.Discounts.Add(existingDiscount2.Object);
            salesItem3.Object.Discounts.Add(existingDiscount3.Object);
            salesItems.Add(salesItem1.Object);
            salesItems.Add(salesItem2.Object);
            salesItems.Add(salesItem3.Object);
        }

        private void AssertAddsForAmountValueType(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            Mock<DbSet<AppliedCartItemDiscount>> appliedCartItemDiscounts,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var salesItem in salesItems.Where(x => x.UnitSoldPrice < x.UnitCorePrice))
            {
                appliedCartItemDiscounts.Verify(
                    m => m.Add(
                        It.Is<AppliedCartItemDiscount>(
                            x => x.Active
                              && x.MasterID == salesItem.ID.Value
                              && x.SlaveID == discount.Object.ID)),
                    Times.Once);
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private void AssertUpdatesForAmountValueType(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var salesItem in salesItems.Where(x => x.UnitSoldPrice < x.UnitCorePrice))
            {
                Assert.NotNull(
                    context.Object.Set<AppliedCartItemDiscount>()
                        .SingleOrDefault(x => x.Active
                                           && x.MasterID == salesItem.ID.Value
                                           && x.SlaveID == discount.Object.ID));
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private void AssertAddsForPercentValueType(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            Mock<DbSet<AppliedCartItemDiscount>> appliedCartItemDiscounts,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var salesItem in salesItems.Where(x => x.UnitSoldPrice < x.UnitCorePrice))
            {
                appliedCartItemDiscounts.Verify(
                    m => m.Add(
                        It.Is<AppliedCartItemDiscount>(
                            x => x.Active
                              && x.MasterID == salesItem.ID.Value
                              && x.SlaveID == discount.Object.ID)),
                    Times.Once);
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private void AssertUpdatesForPercentValueType(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var salesItem in salesItems.Where(x => x.UnitSoldPrice < x.UnitCorePrice))
            {
                Assert.NotNull(
                    context.Object.Set<AppliedCartItemDiscount>()
                        .SingleOrDefault(x => x.Active
                                           && x.MasterID == salesItem.ID.Value
                                           && x.SlaveID == discount.Object.ID));
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}
