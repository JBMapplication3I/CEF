// <copyright file="Geography_Districts_SpecialWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the districts workflows tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Models;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Workflows.Geography.Districts.Special")]
    public class Geography_Districts_SpecialWorkflowTests : Geography_Districts_WorkflowTestsBase
    {
        public Geography_Districts_SpecialWorkflowTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_Get_ByNameAndRegionID_Should_ReturnAModelObjectWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Geography_Districts_SpecialWorkflowTests|Verify_Get_ByNameAndRegionID_Should_ReturnAModelObjectWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoDistrictTable = true, DoRegionTable = true };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await new DistrictWorkflow().GetDistrictByNameAndRegionIDAsync(1, "Williamson County", contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<DistrictModel>(result);
                Assert.Equal(1, result!.ID);
                Assert.Equal("WILCO", result.Code);
                Assert.Equal("WILCO", result.CustomKey);
                Assert.Equal(1, result.Region!.ID);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
