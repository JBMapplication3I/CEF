// <copyright file="CommonDiscountValidatorTests.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base discount validator tests class</summary>
// ReSharper disable ArgumentsStyleOther, ArgumentsStyleLiteral, ArgumentsStyleNamedExpression
namespace Clarity.Ecommerce.Testing
{
    using System;
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

    [Trait(name: "Category", value: "Providers.Discounts.Validators.Base")]
    public class CommonDiscountValidatorTests : DiscountValidatorTestsBase
    {
        private class DiscountValidator : BaseDiscountValidator
        {
            /// <inheritdoc/>
            protected override Task<(bool added, List<IAppliedDiscountBaseModel> currentDiscounts)> SetDiscountTotalAsync(
                bool isAdd,
                decimal subtotal,
                IDiscount discount,
                int cartID,
                List<IAppliedDiscountBaseModel> currentDiscounts,
                List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> cartItems,
                List<IRateQuoteModel> rateQuotes,
                IClarityEcommerceEntities context)
            {
                return Task.FromResult((true, (List<IAppliedDiscountBaseModel>)null));
            }
        }

        public CommonDiscountValidatorTests(Xunit.Abstractions.ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task Verify_ValidateDiscount_WithAValidDiscount_Combinable_InDateRange_NoLimits_NoThreshold_NoReqs_Passes()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_Inactive()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.Active = false;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "Invalid Discount code");
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_TooEarly()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.StartDate = DateExtensions.GenDateTime.AddDays(1);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "Discount hasn't started yet");
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_TooLate()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.EndDate = DateExtensions.GenDateTime.AddDays(-1);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "Discount ended");
        }

        [Fact]
        public Task Verify_ValidateDiscount_With_BadDiscountType_Fails()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = Enum.GetNames(typeof(Enums.DiscountType)).Length + 1;
            discount.Object.UsageLimit = 100;
            // Act/Assert
            return Assert.ThrowsAsync<ArgumentException>(
                () => CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context));
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_AddedDiscountNotCombinable()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.CanCombine = false;
            var otherDiscount = new Mock<IAppliedDiscountBaseModel>();
            otherDiscount.Setup(x => x.ID).Returns(discount.Object.ID + 1);
            currentDiscounts.Add(otherDiscount.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "The current discounts can not be combined with other discounts");
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_CurrentDiscountsNotCombinable()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            var otherDiscount = new Mock<IAppliedDiscountBaseModel>();
            otherDiscount.Setup(x => x.ID).Returns(discount.Object.ID + 1);
            otherDiscount.Setup(x => x.DiscountCanCombine).Returns(false);
            otherDiscount.Setup(x => x.Discount.CanCombine).Returns(false);
            currentDiscounts.Add(otherDiscount.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "The current discounts can not be combined with other discounts");
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_DiscountAlreadyAdded_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            var otherDiscount = new Mock<IAppliedDiscountBaseModel>();
            otherDiscount.SetupAllProperties();
            otherDiscount.Object.Active = true;
            otherDiscount.Object.DiscountID = discount.Object.ID;
            otherDiscount.Object.SlaveID = discount.Object.ID;
            currentDiscounts.Add(otherDiscount.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "Discount has already been added");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_HasUsageLimitByUser()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.UsageLimitByUser = true;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_OrderDiscountType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Order;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_OrderDiscountType_SalesOrderDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Order;
            var currentSalesOrderDiscount = new AppliedSalesOrderDiscount
            {
                SlaveID = discount.Object.ID,
            };
            var currentSalesOrderDiscounts = new List<AppliedSalesOrderDiscount>
            {
                currentSalesOrderDiscount,
                currentSalesOrderDiscount,
            };
            discount.Object.UsageLimit = currentSalesOrderDiscounts.Count;
            var appliedSalesOrderDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentSalesOrderDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedSalesOrderDiscount>()).Returns(appliedSalesOrderDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_OrderDiscountType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Order;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_OrderDiscountType_IsAdd_CartDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Order;
            var currentCartDiscount = new AppliedCartDiscount
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime.AddHours(-1),
                SlaveID = discount.Object.ID,
            };
            var currentCartDiscounts = new List<AppliedCartDiscount> { currentCartDiscount, currentCartDiscount };
            discount.Object.UsageLimit = currentCartDiscounts.Count;
            var appliedCartDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_ShipmentDiscountType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Shipping;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_ShipmentDiscountType_SalesOrderDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Shipping;
            var currentSalesOrderDiscount = new AppliedSalesOrderDiscount
            {
                SlaveID = discount.Object.ID,
            };
            var currentSalesOrderDiscounts = new List<AppliedSalesOrderDiscount>
            {
                currentSalesOrderDiscount,
                currentSalesOrderDiscount,
            };
            discount.Object.UsageLimit = currentSalesOrderDiscounts.Count;
            var appliedSalesOrderDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentSalesOrderDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedSalesOrderDiscount>()).Returns(appliedSalesOrderDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_ShipmentDiscountType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Shipping;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_ShipmentDiscountType_IsAdd_CartDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Shipping;
            var currentCartDiscount = new AppliedCartDiscount
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime.AddHours(-1),
                SlaveID = discount.Object.ID,
            };
            var currentCartDiscounts = new List<AppliedCartDiscount> { currentCartDiscount, currentCartDiscount };
            discount.Object.UsageLimit = currentCartDiscounts.Count;
            var appliedCartDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentCartDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_ProductDiscountType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_ProductDiscountType_SalesOrderItemDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            var currentSalesOrderItemDiscount = new AppliedSalesOrderItemDiscount
            {
                SlaveID = discount.Object.ID,
            };
            var currentSalesOrderItemDiscounts = new List<AppliedSalesOrderItemDiscount>
            {
                currentSalesOrderItemDiscount,
                currentSalesOrderItemDiscount,
            };
            discount.Object.UsageLimit = currentSalesOrderItemDiscounts.Count;
            var appliedSalesOrderItemDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentSalesOrderItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedSalesOrderItemDiscount>()).Returns(appliedSalesOrderItemDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_ProductDiscountType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_ProductDiscountType_IsAdd_CartItemDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.Product;
            var currentCartItemDiscount = new AppliedCartItemDiscount
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime.AddHours(-1),
                SlaveID = discount.Object.ID,
            };
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount> { currentCartItemDiscount, currentCartItemDiscount };
            discount.Object.UsageLimit = currentCartItemDiscounts.Count;
            var appliedCartItemDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_BuyXGetYDiscountType()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.BuyXGetY;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_BuyXGetYDiscountType_SalesOrderItemDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.BuyXGetY;
            var currentSalesOrderItemDiscount = new AppliedSalesOrderItemDiscount
            {
                SlaveID = discount.Object.ID,
            };
            var currentSalesOrderItemDiscounts = new List<AppliedSalesOrderItemDiscount>
            {
                currentSalesOrderItemDiscount,
                currentSalesOrderItemDiscount,
            };
            discount.Object.UsageLimit = currentSalesOrderItemDiscounts.Count;
            var appliedSalesOrderItemDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentSalesOrderItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedSalesOrderItemDiscount>()).Returns(appliedSalesOrderItemDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: false,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public async Task ValidateDiscount_Valid_HasUsageLimit_BuyXGetYDiscountType_IsAdd()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.UsageLimit = currentDiscounts.Count + 1;
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.BuyXGetY;
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateDiscount);
        }

        [Fact]
        public async Task ValidateDiscount_Invalid_HasUsageLimit_BuyXGetYDiscountType_IsAdd_CartItemDiscountsExceeded()
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.DiscountTypeID = (int)Enums.DiscountType.BuyXGetY;
            var currentCartItemDiscount = new AppliedCartItemDiscount
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime.AddHours(-1),
                SlaveID = discount.Object.ID,
            };
            var currentCartItemDiscounts = new List<AppliedCartItemDiscount>
            {
                currentCartItemDiscount,
                currentCartItemDiscount,
            };
            discount.Object.UsageLimit = currentCartItemDiscounts.Count;
            var appliedCartItemDiscounts2 = SetupUtilities.GetMockAsyncDbSet(currentCartItemDiscounts.AsQueryable());
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts2.Object);
            // Act
            var validateDiscount = await CallValidateAsync(
                    isAdd: true,
                    cart: cart,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    context: context)
                .ConfigureAwait(false);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateDiscount,
                "This discount has been applied the maximum number of times");
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_ANullOperator_Passes()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: 10m, // Just need to pass a value
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateAmountThreshold);
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_UndefinedOperator_Fails()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.Undefined;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: 10m, // Just need to pass a value
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateAmountThreshold);
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_ABadOperator_Fails()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.DiscountCompareOperator = Enum.GetNames(typeof(Enums.CompareOperator)).Length;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: 10m, // Just need to pass a value
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateAmountThreshold,
                "Invalid comparison operator for discount");
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_LessThanOperatorAndValidAmount_Passes()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.LessThan;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount - 1, // Specifically lower
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateAmountThreshold);
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_LessThanOperatorAndInvalidAmount_Fails()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.LessThan;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount,
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateAmountThreshold,
                $"{(Enums.DiscountType)discount.Object.DiscountTypeID} amount should be less than"
                + $" {discount.Object.ThresholdAmount:c} (actual: {discount.Object.ThresholdAmount:c})");
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_LessThanOrEqualOperatorAndValidAmount_Passes()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.LessThanOrEqualTo;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount,
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateAmountThreshold);
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_LessThanOrEqualOperatorAndInvalidAmount_Fails()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.LessThanOrEqualTo;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount + 1, // Specifically Higher
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateAmountThreshold,
                $"{(Enums.DiscountType)discount.Object.DiscountTypeID} amount should be less or equal to"
                + $" {discount.Object.ThresholdAmount:c} (actual: {discount.Object.ThresholdAmount + 1:c})");
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_GreaterThanOperatorAndValidAmount_Passes()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount + 1, // Specifically Higher
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateAmountThreshold);
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_GreaterThanOperatorAndInvalidAmount_Fails()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount,
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateAmountThreshold,
                $"{(Enums.DiscountType)discount.Object.DiscountTypeID} amount should be greater than"
                + $" {discount.Object.ThresholdAmount:c} (actual: {discount.Object.ThresholdAmount:c})");
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_GreaterThanOrEqualOperatorAndValidAmount_Passes()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThanOrEqualTo;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount,
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Passed_WithNoMessages(validateAmountThreshold);
        }

        [Fact]
        public void Verify_ValidateAmountThreshold_With_GreaterThanOrEqualOperatorAndInvalidAmount_Fails()
        {
            // Arrange
            var (discount, _, _, _, _, _, _, _, _, _) = Setup();
            discount.Object.ThresholdAmount = 10_000m;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThanOrEqualTo;
            // Act
            var validateAmountThreshold = new DiscountValidator().ValidateAmountThreshold(
                compareAmount: discount.Object.ThresholdAmount - 1, // Specifically Lower
                compareOperator: discount.Object.DiscountCompareOperator,
                thresholdAmount: discount.Object.ThresholdAmount,
                discountType: discount.Object.DiscountTypeID);
            // Assert
            Verify_CEFAR_Failed_WithSingleMessage(
                validateAmountThreshold,
                $"{(Enums.DiscountType)discount.Object.DiscountTypeID} amount should be greater or equal to"
                + $" {discount.Object.ThresholdAmount:c} (actual: {discount.Object.ThresholdAmount - 1:c})");
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
            SetupContext(
                context: context,
                appliedCartDiscounts: appliedCartDiscounts,
                appliedCartItemDiscounts: appliedCartItemDiscounts,
                appliedSalesOrderDiscounts: appliedSalesOrderDiscounts,
                appliedSalesOrderItemDiscounts: appliedSalesOrderItemDiscounts);
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

        private Task<CEFActionResponse<List<IAppliedDiscountBaseModel>>> CallValidateAsync(
            bool isAdd,
            Mock<ICartModel> cart,
            Mock<IDiscount> discount,
            List<IAppliedDiscountBaseModel> currentDiscounts,
            Mock<IClarityEcommerceEntities> context)
        {
            return new DiscountValidator().ValidateDiscountAsync(
                isAdd: isAdd,
                userID: 0,
                cartID: 0,
                subtotal: cart.Object.Totals.SubTotal,
                discount: discount.Object,
                currentDiscounts: currentDiscounts,
                cartItems: cart.Object.SalesItems,
                rateQuotes: cart.Object.RateQuotes,
                context: context.Object);
        }
    }
}
