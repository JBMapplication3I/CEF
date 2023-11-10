// <copyright file="CountryWorkflowsTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country workflow tests class</summary>
// ReSharper disable InconsistentNaming, ObjectCreationAsStatement, ReturnValueOfPureMethodIsNotUsed
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Geography.Countries.Special")]
    public class Geography_Countries_SpecialWorkflowTests : Geography_Countries_WorkflowTestsBase
    {
        public Geography_Countries_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_Get_ByCode_Should_ReturnAModelWithFullMap()
        {
            const string contextProfileName = "Geography_Countries_SpecialWorkflowTests|Verify_Get_ByCode_Should_ReturnAModelWithFullMap";
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoCountryTable = true };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await new CountryWorkflow().GetByCodeAsync("USA", contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.Equal(1, result!.ID);
                Assert.Equal("USA", result.Code);
                Assert.Equal("United States of America", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
