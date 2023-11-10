// <copyright file="DiscountValidatorTestsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount validator tests base class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Moq;
    using Xunit;

    public abstract class DiscountValidatorTestsBase : XUnitLogHelper
    {
        /// <summary>Initializes a new instance of the <see cref="DiscountValidatorTestsBase"/> class.</summary>
        /// <param name="testOutputHelper">The test output helper.</param>
        protected DiscountValidatorTestsBase(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        /// <summary>Verify CEFAR failed with single message.</summary>
        /// <param name="result">       The result.</param>
        /// <param name="expectMessage">Message describing the expect.</param>
        protected static void Verify_CEFAR_Failed_WithSingleMessage(CEFActionResponse result, string expectMessage)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Single(result.Messages);
            Assert.Equal(expectMessage, result.Messages[0]);
        }

        /// <summary>Verify CEFAR failed with multiple messages.</summary>
        /// <param name="result">        The result.</param>
        /// <param name="expectMessages">A variable-length parameters list containing expect messages.</param>
        protected static void Verify_CEFAR_Failed_WithMultipleMessages(
            CEFActionResponse result,
            params string[] expectMessages)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Equal(expectMessages.Length, result.Messages.Count);
            var counter = 0;
            foreach (var expectMessage in expectMessages)
            {
                Assert.Equal(expectMessage, result.Messages[counter]);
                counter++;
            }
        }

        /// <summary>Verify CEFAR passed with no messages.</summary>
        /// <param name="result">The result.</param>
        protected static void Verify_CEFAR_Passed_WithNoMessages(CEFActionResponse result)
        {
            Assert.NotNull(result);
            Assert.True(
                result.ActionSucceeded,
                result.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => c + "\r\n" + n));
            Assert.Empty(result.Messages);
        }

        /// <summary>Base validate discount invalid.</summary>
        /// <param name="validator">      The validator.</param>
        /// <param name="thresholdAmount">The threshold amount.</param>
        /// <returns>A Task.</returns>
        protected async Task Base_ValidateDiscount_InvalidAsync(IDiscountValidator validator, decimal thresholdAmount)
        {
            // Arrange
            var (discount, currentDiscounts, context, _, _, _, _, _, _, cart) = Setup();
            discount.Object.ThresholdAmount = thresholdAmount;
            discount.Object.DiscountCompareOperator = (int)Enums.CompareOperator.GreaterThan;
            // Act
            var validateDiscount = await validator.ValidateDiscountAsync(
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
            Verify_CEFAR_Failed_WithSingleMessage(validateDiscount, "Unknown Error TODO");
        }

        /// <summary>Sets up this DiscountValidatorFacts per the overridden function actions.</summary>
        protected abstract (
            Mock<IDiscount> discount,
            List<IAppliedDiscountBaseModel> currentDiscounts,
            Mock<IClarityEcommerceEntities> context,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<DbSet<AppliedCartItemDiscount>> appliedCartItemDiscounts,
            Mock<DbSet<AppliedSalesOrderDiscount>> appliedSalesOrderDiscounts,
            Mock<DbSet<AppliedSalesOrderItemDiscount>> appliedSalesOrderItemDiscounts,
            DiscountProduct product,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            Mock<ICartModel> cart) Setup();

        /// <summary>Sets up the context.</summary>
        /// <param name="context">                       The context.</param>
        /// <param name="appliedCartDiscounts">          The applied cart discounts.</param>
        /// <param name="appliedCartItemDiscounts">      The applied cart item discounts.</param>
        /// <param name="appliedSalesOrderDiscounts">    The applied sales order discounts.</param>
        /// <param name="appliedSalesOrderItemDiscounts">The applied sales order item discounts.</param>
        protected virtual void SetupContext(
            Mock<IClarityEcommerceEntities> context,
            Mock<DbSet<AppliedCartDiscount>> appliedCartDiscounts,
            Mock<DbSet<AppliedCartItemDiscount>> appliedCartItemDiscounts,
            Mock<DbSet<AppliedSalesOrderDiscount>> appliedSalesOrderDiscounts,
            Mock<DbSet<AppliedSalesOrderItemDiscount>> appliedSalesOrderItemDiscounts)
        {
            context.Setup(x => x.Set<AppliedCartDiscount>()).Returns(appliedCartDiscounts.Object);
            context.Setup(x => x.Set<AppliedCartItemDiscount>()).Returns(appliedCartItemDiscounts.Object);
            context.Setup(x => x.Set<AppliedSalesOrderDiscount>()).Returns(appliedSalesOrderDiscounts.Object);
            context.Setup(x => x.Set<AppliedSalesOrderItemDiscount>()).Returns(appliedSalesOrderItemDiscounts.Object);
        }

        /// <summary>Sets up the discount.</summary>
        /// <param name="salesItems">The sales items.</param>
        /// <param name="product">   The product.</param>
        /// <param name="discount">  The discount.</param>
        /// <param name="idOffset">  The identifier offset.</param>
        protected virtual void SetupDiscount(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> salesItems,
            DiscountProduct product,
            Mock<IDiscount> discount,
            int idOffset = 0)
        {
            discount.SetupAllProperties();
            discount.Object.ID = 1_000 + idOffset;
            discount.Object.Active = true;
            discount.Object.StartDate = DateExtensions.GenDateTime;
            discount.Object.EndDate = discount.Object.StartDate.Value.AddDays(1);
            discount.Object.CanCombine = true;
            discount.Object.UsageLimitByUser = false;
        }
    }
}
