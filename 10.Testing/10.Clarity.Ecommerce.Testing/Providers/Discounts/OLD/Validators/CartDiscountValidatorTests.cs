// <copyright file="CartDiscountValidatorTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart discount validator tests class</summary>
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
    using Mapper;
    using Models;
    using Moq;
    using Xunit;

    [Trait("Category", "Providers.Discounts.Validators.Carts")]
    public class CartDiscountValidatorTests : DiscountValidatorTestsBase
    {
        public CartDiscountValidatorTests(Xunit.Abstractions.ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public async Task ValidateDiscount_Invalid()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ThresholdAmount = cart.Object.Totals.SubTotal;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Failed_WithMultipleMessages(
                validateDiscount,
                "Order amount should be greater than $99.99 (actual: $99.99)",
                "Discount Removed");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var cartDiscount = new Mock<IAppliedCartDiscountModel>();
            cartDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            cart.Object.Discounts.Add(cartDiscount.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var cartDiscount = new Mock<IAppliedCartDiscountModel>();
            cartDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            cart.Object.Discounts.Add(cartDiscount.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_NoItemsInCart()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            cart.Object.Totals.SubTotal = 0m;
            cart.Object.SalesItems = new List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(validateDiscount, "No Items in Cart");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_ZeroAmount()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            discount.Object.Value = 0;
            var cartDiscount = new Mock<IAppliedCartDiscountModel>();
            cartDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            cart.Object.Discounts.Add(cartDiscount.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_ZeroAmount()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            discount.Object.Value = 0;
            var cartDiscount = new Mock<IAppliedCartDiscountModel>();
            cartDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            cart.Object.Discounts.Add(cartDiscount.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var currentCartDiscounts = new List<AppliedCartDiscount>
            {
                new AppliedCartDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = cart.Object.ID.Value,
                    Active = false
                },
            };
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var cartDiscount = new Mock<IAppliedCartDiscountModel>();
            cartDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            cart.Object.Discounts.Add(cartDiscount.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForAmountValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var currentCartDiscounts = new List<AppliedCartDiscount>
            {
                new AppliedCartDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = cart.Object.ID.Value,
                    Active = false
                },
            };
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var cartDiscount = new Mock<IAppliedCartDiscountModel>();
            cartDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            cart.Object.Discounts.Add(cartDiscount.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: false,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForPercentValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, appliedCartDiscounts, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: true,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertAddsForAmountValueType(cart, appliedCartDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, appliedCartDiscounts, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: true,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertAddsForPercentValueType(cart, appliedCartDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var currentCartDiscounts = new List<AppliedCartDiscount>
            {
                new AppliedCartDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = cart.Object.ID.Value,
                    Active = false
                },
            };
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: true,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForAmountValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var currentCartDiscounts = new List<AppliedCartDiscount>
            {
                new AppliedCartDiscount
                {
                    SlaveID = discount.Object.ID,
                    MasterID = cart.Object.ID.Value,
                    Active = false
                },
            };
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: true,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForPercentValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_DiscountExists()
        {
            // Arrange
            var (discount, _, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ThresholdAmount = cart.Object.Totals.SubTotal;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            var currentCartDiscounts = new List<AppliedCartDiscount>
            {
                new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                },
            };
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var currentDiscounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList<IAppliedDiscountBaseModel>();
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: true,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(validateDiscount, "Discount has already been added");
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ThresholdAmount = cart.Object.Totals.SubTotal;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            // Act
            var validateDiscount = await new CartDiscountValidator().ValidateDiscountAsync(
                    isAdd: true,
                    userID: 0,
                    cartID: 0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "Order amount should be greater than $99.99 (actual: $99.99)");
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
            SetupSalesCollection(cart);
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

        protected override void SetupContext(
            Mock<IClarityEcommerceEntities> context,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<DbSet<AppliedCartItemDiscount>> appliedCartItemDiscounts,
            Mock<DbSet<AppliedSalesOrderDiscount>> appliedSalesOrderDiscounts,
            Mock<DbSet<AppliedSalesOrderItemDiscount>> appliedSalesOrderItemDiscounts)
        {
            base.SetupContext(
                context,
                appliedCartDiscounts,
                appliedCartItemDiscounts,
                appliedSalesOrderDiscounts,
                appliedSalesOrderItemDiscounts);
            appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet<AppliedCartDiscount>();
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
        }

        protected override void SetupDiscount(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            DiscountProduct product,
            Mock<IDiscount> discount,
            int idOffset = 0)
        {
            base.SetupDiscount(salesItems, product, discount, idOffset);
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Order;
            discount.Object.Value = 25.00m;
        }

        private void SetupSalesCollection(
            Mock<ICartModel> cart,
            int idOffset = 0)
        {
            cart.SetupAllProperties();
            cart.Object.ID = 40_000 + idOffset;
            cart.Object.Discounts = new List<IAppliedCartDiscountModel>();
            cart.Object.Totals = new CartTotals { SubTotal = 99.99m };
        }

        private void AssertAddsForAmountValueType(
            Mock<ICartModel> cart,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            appliedCartDiscounts.Verify(
                m => m.Add(
                    It.Is<AppliedCartDiscount>(
                        x => x.Active
                          && x.MasterID == cart.Object.ID.Value
                          && x.SlaveID == discount.Object.ID)),
                Times.Once);
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        private void AssertUpdatesForAmountValueType(
            Mock<ICartModel> cart,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            Assert.NotNull(
                context.Object.Set<AppliedCartDiscount>()
                    .SingleOrDefault(x => x.Active
                                       && x.MasterID == cart.Object.ID.Value
                                       && x.SlaveID == discount.Object.ID));
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private void AssertAddsForPercentValueType(
            Mock<ICartModel> cart,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            appliedCartDiscounts.Verify(
                m => m.Add(
                    It.Is<AppliedCartDiscount>(
                        x => x.Active
                          && x.MasterID == cart.Object.ID.Value
                          && x.SlaveID == discount.Object.ID)),
                Times.Once);
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        private void AssertUpdatesForPercentValueType(
            Mock<ICartModel> cart,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            Assert.NotNull(
                context.Object.Set<AppliedCartDiscount>()
                    .SingleOrDefault(x => x.Active
                                       && x.MasterID == cart.Object.ID.Value
                                       && x.SlaveID == discount.Object.ID));
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}
