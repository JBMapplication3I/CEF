// <copyright file="AddressesWorkflowsTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the addresses workflows tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Geography.Addresses.Special")]
    public class Geography_Addresses_SpecialWorkflowTests : Geography_Addresses_WorkflowTestsBase
    {
        public Geography_Addresses_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_Get_ByID_Should_ReturnAModelObjectWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Geography_Addresses_SpecialWorkflowTests|Verify_Get_ByID_Should_ReturnAModelObjectWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAddressTable = true, DoCountryTable = true, DoRegionTable = true };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await new AddressWorkflow().GetAsync(1, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<AddressModel>(result);
                Assert.Equal(1, result!.ID);
                Assert.Equal("BILL TO", result.CustomKey);
                Assert.Equal("James Gray", result.Company);
                Assert.Equal("9442 N Capital of TX Hwy", result.Street1);
                Assert.Equal("Plaza 1, Ste 925", result.Street2);
                Assert.Null(result.Street3);
                Assert.Equal("Austin", result.City);
                Assert.Equal(43, result.RegionID);
                Assert.Equal("78759", result.PostalCode);
                Assert.Equal(1, result.CountryID);
                //Assert.Equal( 30.390397m, result.Latitude);
                //Assert.Equal(-97.748598m, result.Longitude);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
