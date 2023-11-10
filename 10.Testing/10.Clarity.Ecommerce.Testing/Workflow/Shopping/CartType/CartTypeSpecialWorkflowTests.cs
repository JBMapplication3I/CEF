// <copyright file="CartTypeSpecialWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart type special workflow tests class</summary>
// ReSharper disable UnusedType.Global
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.Models;
    using Models;
    using Moq;
    using Xunit;

    [Trait("Category", "Workflows.Shopping.CartTypes.Special")]
    public class Shopping_CartTypes_SpecialWorkflowTests : Shopping_CartTypes_WorkflowTestsBase
    {
        public Shopping_CartTypes_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_GetTypesForUser_Works()
        {
            var contextProfileName = GenContextProfileName("Verify_GetTypesForUser_Works");
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartTypeWorkflow().GetTypesForUserAsync(
                        userID: 1,
                        includeNotCreatedByUser: true,
                        filterCartsByOrderRequest: false,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<List<ICartTypeModel>>(result);
                Assert.NotEmpty(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        /// <inheritdoc/>
        protected override MockingSetup GetMockingSetupWithExistingDataForThisTable()
        {
            return new() { DoCartTypeTable = true };
        }

        /// <inheritdoc/>
        protected override MockingSetup GetMockingSetupWithExistingDataForThisTableAndExpandedTables()
        {
            return new()
            {
                DoBrandTable = true,
                DoCartTable = true,
                DoCartTypeTable = true,
                DoStoreTable = true,
                DoUserTable = true,
            };
        }

        /// <inheritdoc/>
        protected override Mock<DbSet<CartType>>? GetMockSet(MockingSetup mockingSetup)
        {
            return mockingSetup.CartTypes;
        }

        /// <inheritdoc/>
        protected override List<Mock<CartType>>? GetRawSet(MockingSetup mockingSetup)
        {
            return mockingSetup.RawCartTypes;
        }

        /// <inheritdoc/>
        protected override async Task<CartTypeModel> GenNewModelAsync(MockingSetup mockingSetup, string contextProfileName)
        {
            return new()
            {
                Active = true,
                CustomKey = await GetCustomKeyAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                Name = await GetNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                DisplayName = await GetDisplayNameAsync(mockingSetup, contextProfileName).ConfigureAwait(false),
                // Required Related Properties
                // Optional Related Properties
                StoreID = null,
                BrandID = null,
                CreatedByUserID = null,
            };
        }
    }
}
