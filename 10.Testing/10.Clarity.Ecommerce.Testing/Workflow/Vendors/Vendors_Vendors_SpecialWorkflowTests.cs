// <copyright file="VendorWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Vendors.Vendors.Special")]
    public class Vendors_Vendors_SpecialWorkflowTests : Vendors_Vendors_WorkflowTestsBase
    {
        public Vendors_Vendors_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_GetVendorsByProducts_Should_ReturnAListOfModels()
        {
            // Arrange
            const string contextProfileName = "Vendors_Vendors_SpecialWorkflowTests|Verify_GetVendorsByProducts_Should_ReturnAListOfModels";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoVendorTable = true,
                    DoVendorProductTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var results = await new VendorWorkflow().GetVendorsByProductAsync(1151, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IVendorProductModel>>(results);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
