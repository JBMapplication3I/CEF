// <copyright file="RegionWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region workflow tests class</summary>
// ReSharper disable CheckNamespace, InconsistentNaming, ObjectCreationAsStatement, ReturnValueOfPureMethodIsNotUsed
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Geography.Regions.Special")]
    public class Geography_Regions_SpecialWorkflowTests : Geography_Regions_WorkflowTestsBase
    {
        public Geography_Regions_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_Get_ByCode_Should_ReturnAModelWithFullMap()
        {
            const string contextProfileName = "Geography_Regions_SpecialWorkflowTests|Verify_Get_ByCode_Should_ReturnAModelWithFullMap";
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoRegionTable = true };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await new RegionWorkflow().GetByCodeAsync("TX", contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.Equal(43, result!.ID);
                Assert.Equal("TX", result.Code);
                Assert.Equal("Texas", result.Name);
                Assert.Equal(1, result.CountryID);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
