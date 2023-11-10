// <copyright file="CartItemsDiscountValidatorTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart items discount validator tests class</summary>
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

    [Trait("Category", "Providers.Discounts.Validators.CartItems")]
    public class CartItemsDiscountValidatorTests : DiscountValidatorTestsBase
    {
        public CartItemsDiscountValidatorTests(Xunit.Abstractions.ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public async Task ValidateDiscount_Invalid()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
            discount.Object.ThresholdAmount = salesItems.Sum(x => x.UnitCorePrice * x.Quantity);
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            // Act
            var validateDiscount = await new CartItemsDiscountValidator().ValidateDiscountAsync(
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
            Verify_CEFAR_Failed_WithMultipleMessages(
                validateDiscount,
                "Product amount should be greater than $1,074.70 (actual: $0.00)",
                "Discount Removed");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var cartItemDiscount = new Mock<IAppliedCartItemDiscountModel>();
            cartItemDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            foreach (var salesItem in salesItems)
            {
                salesItem.Discounts.Add(cartItemDiscount.Object);
            }
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
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
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var cartItemDiscount = new Mock<IAppliedCartItemDiscountModel>();
            cartItemDiscount.Setup(x => x.DiscountID).Returns(discount.Object.ID);
            foreach (var salesItem in salesItems)
            {
                salesItem.Discounts.Add(cartItemDiscount.Object);
            }
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
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
        public async Task ValidateDiscount_Valid_AmountValueType_ZeroAmount()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var counter = 0;
            foreach (var salesItem in salesItems)
            {
                var cartItemDiscount = new Mock<IAppliedCartItemDiscountModel>();
                cartItemDiscount.Setup(x => x.DiscountID).Returns(1_000 + ++counter);
                cartItemDiscount.Setup(x => x.DiscountTotal).Returns(salesItem.UnitSoldPrice.Value * -1);
                salesItem.Quantity = 1;
                salesItem.Discounts = new List<IAppliedCartItemDiscountModel> { cartItemDiscount.Object };
            }
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            foreach (var salesItem in salesItems)
            {
                Assert.Null(salesItem.Discounts.SingleOrDefault(x => x.DiscountID == discount.Object.ID));
            }
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
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
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForAmountValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
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
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForPercentValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, appliedCartItemDiscounts, _, _, _, salesItems, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertAddsForAmountValueType(salesItems, appliedCartItemDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, appliedCartItemDiscounts, _, _, _, salesItems, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertAddsForPercentValueType(salesItems, appliedCartItemDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
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
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForAmountValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
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
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentDiscounts,
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForPercentValueType(salesItems, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_DiscountExists()
        {
            // Arrange
            var (discount, _, context, _, _, _, _, _, salesItems, cart) = Setup();
            discount.Object.ThresholdAmount = salesItems.Sum(x => x.UnitCorePrice * x.Quantity);
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount>();
            foreach (var salesItem in salesItems)
            {
                var applied = new AppliedCartItemDiscount
                {
                    Active = true,
                    MasterID = salesItem.ID.Value,
                    SlaveID = discount.Object.ID,
                };
                currentCartItemDiscounts.Add(applied);
                salesItem.Discounts.Add(applied.CreateAppliedCartItemDiscountModelFromEntityFull());
            }
            var appliedCartItemDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts.Object);
            var currentDiscounts = context.Object.Set<AppliedCartItemDiscount>()
                .Select(ModelMapperForAppliedCartItemDiscount.MapAppliedCartItemDiscountModelFromEntityList)
                .ToList<IAppliedDiscountBaseModel>();
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
                    true,
                    0,
                    0,
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
                "Discount has already been added");
            foreach (var appliedDiscount in context.Object.Set<AppliedCartItemDiscount>())
            {
                Assert.False(appliedDiscount.Active);
            }
            ////context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, salesItems, cart) = Setup();
            discount.Object.ThresholdAmount = salesItems.Sum(x => x.UnitCorePrice * x.Quantity);
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            var cartItemsDiscountValidator = new CartItemsDiscountValidator();
            // Act
            var validateDiscount = await cartItemsDiscountValidator.ValidateDiscountAsync(
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
                "Product amount should be greater than $1,074.70 (actual: $0.00)");
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
            SetupProduct(product);
            SetupSalesItems(salesItems);
            cart.Object.SalesItems = salesItems;
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

        /// <inheritdoc/>
        protected override void SetupDiscount(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            DiscountProduct product,
            Mock<IDiscount> discount,
            int idOffset = 0)
        {
            base.SetupDiscount(salesItems, product, discount, idOffset);
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            discount.Object.Products = new List<DiscountProduct> { product };
            discount.Object.Value = 25.00m;
            discount.Object.UsageLimit = salesItems.Count;
        }

        private void SetupProduct(IDiscountProduct product)
        {
            product.SlaveID = 1151;
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
                                           && x.SlaveID == discount.Object.ID
                                           && x.MasterID == salesItem.ID.Value));
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
                                           && x.SlaveID == discount.Object.ID
                                           && x.MasterID == salesItem.ID.Value));
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}
