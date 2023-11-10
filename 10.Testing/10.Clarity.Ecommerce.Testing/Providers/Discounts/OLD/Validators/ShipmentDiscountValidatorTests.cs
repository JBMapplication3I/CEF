// <copyright file="ShipmentDiscountValidatorTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment discount validator tests class</summary>
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

    [Trait("Category", "Providers.Discounts.Validators.Shipments")]
    public class ShipmentDiscountValidatorTests : DiscountValidatorTestsBase
    {
        public ShipmentDiscountValidatorTests(Xunit.Abstractions.ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public async Task ValidateDiscount_Invalid()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            var thresholdAmount = cart.Object.RateQuotes.Sum(x => x.Rate ?? 0);
            discount.Object.ThresholdAmount = thresholdAmount;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            // Act
            var validateDiscount = await new ShipmentDiscountValidator().ValidateDiscountAsync(
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
                "Order amount should be greater than $41.00 (actual: $41.00)",
                "Discount Removed");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType()
        {
            // Arrange
            var (discount, _, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: cart.Object.Discounts.ToList<IAppliedDiscountBaseModel>(),
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_NoSelectedRateQuotes()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            foreach (var rateQuote in cart.Object.RateQuotes) { rateQuote.Selected = false; }
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Failed_WithSingleMessage(validateDiscount, "No shipment methods selected");
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_NoApplicableRateQuotes()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            foreach (var rateQuote in cart.Object.RateQuotes)
            {
                rateQuote.ShipCarrierMethodID = discount.Object.ShipCarrierMethods.First().SlaveID + 1; // Specifically different
            }
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Failed_WithSingleMessage(validateDiscount, "Discount does not apply to selected rate quotes");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_NoDiscountShipCarrierMethods()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ShipCarrierMethods = new List<DiscountShipCarrierMethod>();
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_ZeroAmount()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            discount.Object.Value = 0;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_ZeroAmount()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            discount.Object.Value = 0;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = false,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForAmountValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = false,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForPercentValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, appliedCartDiscounts, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertAddsForAmountValueType(cart, appliedCartDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, appliedCartDiscounts, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertAddsForPercentValueType(cart, appliedCartDiscounts, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_AmountValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Amount;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = false,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            cart.Object.Discounts = currentCartDiscounts
                .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                .ToList();
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForAmountValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_PercentValueType_DiscountExists_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ValueType = (int)Enums.DiscountValueType.Percent;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = false,
                    MasterID = cart.Object.ID.Value,
                    SlaveID = discount.Object.ID,
                }));
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
            AssertUpdatesForPercentValueType(cart, discount, context);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_DiscountExists()
        {
            // Arrange
            var (discount, _, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ThresholdAmount = cart.Object.RateQuotes.Sum(x => x.Rate ?? 0);
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            var currentCartDiscounts = new List<AppliedCartDiscount>(
                cart.Object.RateQuotes.Take(1).Select(x => new AppliedCartDiscount
                {
                    Active = true,
                    MasterID = cart.Object.ID.Value + 1, // Intentionally different
                    SlaveID = discount.Object.ID,
                }));
            var appliedCartDiscounts = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
                    false,
                    0,
                    0,
                    subtotal: cart.Object.Totals.SubTotal,
                    discount: discount.Object,
                    currentDiscounts: currentCartDiscounts
                        .Select(ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList)
                        .ToList<IAppliedDiscountBaseModel>(),
                    cartItems: cart.Object.SalesItems,
                    rateQuotes: cart.Object.RateQuotes,
                    context: context.Object)
                .ConfigureAwait(false);
            // Assert
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Failed_WithSingleMessage(validateDiscount, "Discount does not apply to selected rate quotes");
            foreach (var appliedDiscount in context.Object.Set<AppliedCartDiscount>())
            {
                Assert.False(appliedDiscount.Active);
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ThresholdAmount = cart.Object.RateQuotes.Sum(x => x.Rate ?? 0);
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            var shipmentDiscountValidator = new ShipmentDiscountValidator();
            // Act
            var validateDiscount = await shipmentDiscountValidator.ValidateDiscountAsync(
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
            Assert.IsAssignableFrom<CEFActionResponse>(validateDiscount);
            Verify_CEFAR_Failed_WithSingleMessage(validateDiscount, "Discount does not apply to selected rate quotes");
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
            SetupDiscount(salesItems, product, discount);
            SetupRateQuotes(discount, cart);
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
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Shipping;
            discount.Object.Value = 25.00m;
        }

        private void SetupRateQuotes(Mock<IDiscount> discount, Mock<ICartModel> cart, int idOffset = 0)
        {
            {
                cart.SetupAllProperties();
                cart.Object.ID = 40_000 + idOffset;
                cart.Object.Active = true;
                cart.Object.Totals = new CartTotals();
                cart.Object.Discounts = new List<IAppliedCartDiscountModel>();
                cart.Object.RateQuotes = new List<IRateQuoteModel>();
            }
            var shipCarrierMethod = new Mock<IShipCarrierMethodModel>();
            {
                shipCarrierMethod.SetupAllProperties();
                shipCarrierMethod.Object.ID = 2_000 + idOffset;
            }
            var rateQuote = new Mock<IRateQuoteModel>();
            {
                rateQuote.SetupAllProperties();
                rateQuote.Object.ID = 30_000 + idOffset;
                rateQuote.Object.Active = true;
                rateQuote.Object.Selected = true;
                rateQuote.Object.Rate = 34.99m;
                rateQuote.Object.ShipCarrierMethodID = shipCarrierMethod.Object.ID.Value;
                var rateQuotes = cart.Object.RateQuotes;
                rateQuotes.Add(rateQuote.Object);
                cart.Object.RateQuotes = rateQuotes;
            }
            var discountShipCarrierMethod = new DiscountShipCarrierMethod
            {
                ID = 20_000 + idOffset,
                Active = true,
                MasterID = discount.Object.ID,
                SlaveID = shipCarrierMethod.Object.ID.Value,
            };
            discount.Object.ShipCarrierMethods.Add(discountShipCarrierMethod);
        }

        private void AssertAddsForAmountValueType(
            Mock<ICartModel> cart,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var _ in cart.Object.RateQuotes)
            {
                appliedCartDiscounts.Verify(
                    m => m.Add(
                        It.Is<AppliedCartDiscount>(
                            x => x.Active
                              && x.MasterID == cart.Object.ID.Value
                              && x.SlaveID == discount.Object.ID)),
                    Times.Once);
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private void AssertUpdatesForAmountValueType(
            Mock<ICartModel> cart,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var _ in cart.Object.RateQuotes)
            {
                Assert.NotNull(context.Object.Set<AppliedCartDiscount>()
                    .SingleOrDefault(x => x.Active
                                       && x.MasterID == cart.Object.ID.Value
                                       && x.SlaveID == discount.Object.ID));
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private void AssertAddsForPercentValueType(
            Mock<ICartModel> cart,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var _ in cart.Object.RateQuotes)
            {
                appliedCartDiscounts.Verify(
                    m => m.Add(
                        It.Is<AppliedCartDiscount>(
                            x => x.Active
                              && x.MasterID == cart.Object.ID.Value
                              && x.SlaveID == discount.Object.ID)),
                    Times.Once);
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        private void AssertUpdatesForPercentValueType(
            Mock<ICartModel> cart,
            Mock<IDiscount> discount,
            Mock<IClarityEcommerceEntities> context)
        {
            foreach (var _ in cart.Object.RateQuotes)
            {
                Assert.NotNull(
                    context.Object.Set<AppliedCartDiscount>()
                        .SingleOrDefault(x => x.Active
                                           && x.MasterID == cart.Object.ID.Value
                                           && x.SlaveID == discount.Object.ID));
            }
            context.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}
