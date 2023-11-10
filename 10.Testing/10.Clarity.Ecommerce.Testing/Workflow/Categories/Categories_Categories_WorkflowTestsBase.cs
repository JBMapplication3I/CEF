// <copyright file="Categories_Categories_WorkflowTestsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the categories workflow tests base class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Workflow;
    using Xunit;

    public partial class Categories_Categories_StandardWorkflowTests
    {
        protected override async Task Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping()
        {
            var contextProfileName = GenContextProfileName("Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var results = (await new CategoryWorkflow().SearchAsync(
                            new CategorySearchModel(),
                            true,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .results;
                // Assert
                Assert.IsType<List<ICategoryModel>>(results);
                Assert.True(results[0].Active);
                Assert.Equal(await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false), results.Count + 2);
                Verify_Search_WithAsListingTrue_Should_ReturnAListOfModelsWithListingMapping_Results(results);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected override async Task Verify_Search_Should_ReturnAListOfModelsWithLiteMapping()
        {
            var contextProfileName = GenContextProfileName("Verify_Search_Should_ReturnAListOfModelsWithLiteMapping");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var results = (await new CategoryWorkflow().SearchAsync(new CategorySearchModel(), false, contextProfileName).ConfigureAwait(false)).results;
                // Assert
                Assert.IsType<List<ICategoryModel>>(results);
                Assert.True(results[0].Active);
                Assert.Equal(await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false), results.Count + 2);
                Verify_Search_Should_ReturnAListOfModelsWithLiteMapping_Results(results);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected override async Task Verify_SearchForConnect_Should_ReturnAListOfModelsWithLiteMapping()
        {
            var contextProfileName = GenContextProfileName("Verify_SearchForConnect_Should_ReturnAListOfModelsWithLiteMapping");
            // Arrange/Act
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = GetMockingSetupWithExistingDataForThisTableAndExpandedTables();
                await DoSetupAsync(childContainer, mockingSetup, contextProfileName).ConfigureAwait(false);
                var results = (await new CategoryWorkflow().SearchForConnectAsync(
                            new CategorySearchModel(),
                            contextProfileName)
                        .ConfigureAwait(false))
                    ?.ToList();
                // Assert
                Assert.NotNull(results);
                Assert.NotEmpty(results);
                Assert.True(results![0].Active);
                Assert.Equal(await GetCacheCountAsync(mockingSetup, contextProfileName).ConfigureAwait(false), results.Count + 2);
                Verify_Search_Should_ReturnAListOfModelsWithLiteMapping_Results(results);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
