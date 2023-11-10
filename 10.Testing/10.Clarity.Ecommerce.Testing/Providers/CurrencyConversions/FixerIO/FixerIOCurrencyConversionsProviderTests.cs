// <copyright file="FixerIOCurrencyConversionsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fixer i/o currency conversions provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.CurrencyConversions.FixerIO;
    using Moq;
    using Xunit;

    [Trait("Category", "Providers.CurrencyConversions.FixerIO")]
    public class FixerIOCurrencyConversionsProviderTests : XUnitLogHelper
    {
        public FixerIOCurrencyConversionsProviderTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_Convert_ReturnsAValue()
        {
            return Verify_Convert_ReturnsAValueInnerAsync("USD", "GBP", 100.00d, 82.5716461963248d);
            // TODO@BE: Add other conversions to test
        }

        private async Task Verify_Convert_ReturnsAValueInnerAsync(string keyA, string keyB, double value, double converted)
        {
            // Arrange
            var provider = new FixerIOCurrencyConversionsProvider();
            var mockingSetup = new MockingSetup { DoCurrencies = true, SaveChangesResult = 1, DoInactives = true };
            await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
            var mockContext = mockingSetup.MockContext;
            RegistryLoader.ContainerInstance.Inject(mockContext.Object);
            // Doubles are funny in that the same number to decimal places
            // in memory may not match, so have to use a margin for error
            const double MarginForError = 2d;
            // Act
            var result = await provider.ConvertAsync(keyA, keyB, value, null).ConfigureAwait(false);
            TestOutputHelper.WriteLine(result.ToString(CultureInfo.InvariantCulture));
            // Assert
            Assert.True(result > 0);
            Assert.True(Math.Abs((converted - result) / converted) <= MarginForError);
        }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_Convert_WithAHistoricalDate_ReturnsAValue()
        {
            return Task.WhenAll(
                Verify_Convert_WithAHistoricalDate_ReturnsAValueInnerAsync("USD", "GBP", 100.00d, 2018, 1, 1, 81.223792809031409d),
                Verify_Convert_WithAHistoricalDate_ReturnsAValueInnerAsync("USD", "GBP", 100.00d, 2018, 1, 2, 81.3569039655996d));
            // TODO@BE: Add other conversions to test
        }

        private async Task Verify_Convert_WithAHistoricalDate_ReturnsAValueInnerAsync(
            string keyA,
            string keyB,
            double value,
            int year,
            int month,
            int day,
            double converted)
        {
            // Arrange
            var provider = new FixerIOCurrencyConversionsProvider();
            var mockingSetup = new MockingSetup { DoCurrencies = true, SaveChangesResult = 1, DoInactives = true };
            await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
            var mockContext = mockingSetup.MockContext;
            RegistryLoader.ContainerInstance.Inject(mockContext.Object);
            // Doubles are funny in that the same number to decimal places
            // in memory may not match, so have to use a margin for error
            const double MarginForError = 0.01d;
            // Act
            var result = await provider.ConvertAsync(keyA, keyB, value, new DateTime(year, month, day), null).ConfigureAwait(false);
            TestOutputHelper.WriteLine(result.ToString(CultureInfo.InvariantCulture));
            // Assert
            Assert.True(result > 0);
            Assert.True(Math.Abs((converted - result) / converted) <= MarginForError);
        }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_Convert_WithAHistoricalDate_StoresTheRate()
        {
            return Task.WhenAll(
                Verify_Convert_WithAHistoricalDate_StoresTheRateInnerAsync("USD", "GBP", 100.00d, 2018, 2, 1),
                Verify_Convert_WithAHistoricalDate_StoresTheRateInnerAsync("USD", "GBP", 100.00d, 2018, 2, 2));
            // TODO@BE: Add other conversions to test
        }

        private async Task Verify_Convert_WithAHistoricalDate_StoresTheRateInnerAsync(
            string keyA,
            string keyB,
            double value,
            int year,
            int month,
            int day)
        {
            // Arrange
            var provider = new FixerIOCurrencyConversionsProvider();
            var mockingSetup = new MockingSetup { DoCurrencies = true, SaveChangesResult = 1 };
            await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
            var mockContext = mockingSetup.MockContext;
            RegistryLoader.ContainerInstance.Inject(mockContext.Object);
            // Act
            var result = await provider.ConvertAsync(keyA, keyB, value, new DateTime(year, month, day), null).ConfigureAwait(false);
            TestOutputHelper.WriteLine(result.ToString(CultureInfo.InvariantCulture));
            // Assert
            mockingSetup.HistoricalCurrencyRates.Verify(m => m.Add(It.IsAny<HistoricalCurrencyRate>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
