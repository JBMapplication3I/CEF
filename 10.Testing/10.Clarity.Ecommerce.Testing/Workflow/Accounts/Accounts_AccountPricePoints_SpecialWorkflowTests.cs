// <copyright file="Accounts_AccountPricePoints_SpecialWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account types workflows tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Moq;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Accounts.AccountPricePoints.Special")]
    public class Accounts_AccountPricePoints_SpecialWorkflowTests : Accounts_AccountPricePoints_WorkflowTestsBase
    {
        public Accounts_AccountPricePoints_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public Task Verify_Deactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            return Task.WhenAll(
                Verify_Deactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync(("MIBSF49682", "LTL")),
                Verify_Deactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync(("MIWSF49682", "WEB")));
        }

        [Fact]
        public Task Verify_Reactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChanges()
        {
            return Task.WhenAll(
                Verify_Reactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync(("MIBSF49682", "LTL")),
                Verify_Reactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync(("MIWSF49682", "WEB")));
        }

        [Fact]
        public Task Verify_Delete_ByKeys_Should_RemoveAndSaveChanges()
        {
            return Task.WhenAll(
                Verify_Delete_ByKeys_Should_RemoveAndSaveChangesInnerAsync(("MIBSF49682", "LTL")),
                Verify_Delete_ByKeys_Should_RemoveAndSaveChangesInnerAsync(("MIWSF49682", "WEB")));
        }

        private async Task Verify_Deactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync((string key1, string key2) keys)
        {
            // Arrange
            var contextProfileName = GenContextProfileName($"Verify_Deactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync|{keys}");
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccountPricePointTable = true, DoAccountTable = true, DoPricePointTable = true, SaveChangesResult = 1 };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = new AccountPricePointWorkflow();
                // Act
                await workflow.DeactivateAsync(keys, contextProfileName).ConfigureAwait(false);
                var result = await workflow.GetAsync(keys, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.Null(result);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        private async Task Verify_Reactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync((string key1, string key2) keys)
        {
            // Arrange
            var contextProfileName = GenContextProfileName($"Verify_Reactivate_ByKeys_Should_UpdateActiveAndUpdatedDateValuesAndSaveChangesInnerAsync|{keys}");
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccountPricePointTable = true, DoAccountTable = true, DoPricePointTable = true, DoInactives = true, SaveChangesResult = 1 };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = new AccountPricePointWorkflow();
                // Act
                await workflow.ReactivateAsync(keys, contextProfileName).ConfigureAwait(false);
                var result = await workflow.GetAsync(keys, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        private async Task Verify_Delete_ByKeys_Should_RemoveAndSaveChangesInnerAsync((string key1, string key2) keys)
        {
            // Arrange
            var contextProfileName = GenContextProfileName($"Verify_Delete_ByKeys_Should_RemoveAndSaveChangesInnerAsync|{keys}");
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccountPricePointTable = true, DoAccountTable = true, DoPricePointTable = true, SaveChangesResult = 1 };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                await new AccountPricePointWorkflow().DeleteAsync(keys, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.AccountPricePoints!.Verify(m => m.Remove(It.IsAny<AccountPricePoint>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
