// <copyright file="Categories_Categories_SpecialWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country workflow tests class</summary>
// ReSharper disable InconsistentNaming, ObjectCreationAsStatement, ReturnValueOfPureMethodIsNotUsed
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Categories.Categories.Special")]
    public class Categories_Categories_SpecialWorkflowTests : Categories_Categories_WorkflowTestsBase
    {
        public Categories_Categories_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_GetCategoriesThreeLevels_Should_ReturnAListOfModelsWithLiteMapping()
        {
            // Arrange
            const string contextProfileName = "Categories_Categories_SpecialWorkflowTests|Verify_GetCategoriesThreeLevels_Should_ReturnAListOfModelsWithLiteMapping";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoCategories = true, DoGeneralAttributeTable = true };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var results = await new CategoryWorkflow().GetCategoriesThreeLevelsAsync(new CategorySearchModel(), null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotEmpty(results!);
                Assert.IsType<List<ICategoryModel>>(results);
                Assert.Equal(7, results!.Count);
                Assert.Equal("GLASS", results[0]!.CustomKey);
                Assert.True(results[0]!.Active);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
