// <copyright file="AccountWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account workflow tests class</summary>
// ReSharper disable InconsistentNaming, ObjectCreationAsStatement, ReturnValueOfPureMethodIsNotUsed, UnusedMember.Global
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.IO;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Accounts.Accounts.Special")]
    public class Accounts_Accounts_SpecialWorkflowTests : Accounts_Accounts_WorkflowTestsBase
    {
        public Accounts_Accounts_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public Task Verify_Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataException()
        {
            return Task.WhenAll(
                Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(500),
                Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(1000),
                Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(10000),
                Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(100000),
                Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int.MaxValue - 1));
        }

        private static async Task Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int accountTypeID)
        {
            // Arrange
            var contextProfileName = $"Accounts_Accounts_SpecialWorkflowTests|Verify_Create_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataException|{accountTypeID}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccountTypeTable = true, DoAccountStatusTable = true };
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var model = new AccountModel { Name = "TEST", TypeID = accountTypeID, StatusID = 1 };
                // Act/Assert
                await Assert.ThrowsAsync<InvalidDataException>(
                        () => new AccountWorkflow().CreateAsync(model, contextProfileName))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public Task Verify_Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataException()
        {
            return Task.WhenAll(
                Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(500),
                Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(1000),
                Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(10000),
                Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(100000),
                Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int.MaxValue - 1));
        }

        private static async Task Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int accountStatusID)
        {
            // Arrange
            var contextProfileName = $"Accounts_Accounts_SpecialWorkflowTests|Verify_Create_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataException|{accountStatusID}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccountTypeTable = true, DoAccountStatusTable = true };
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var model = new AccountModel { Name = "TEST", TypeID = 1, StatusID = accountStatusID };
                // Act/Assert
                await Assert.ThrowsAsync<InvalidDataException>(() => new AccountWorkflow().CreateAsync(model, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public Task Verify_Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataException()
        {
            return Task.WhenAll(
                Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(500),
                Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(1000),
                Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(10000),
                Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(100000),
                Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int.MaxValue - 1));
        }

        private static async Task Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int accountTypeID)
        {
            // Arrange
            var contextProfileName = $"Accounts_Accounts_SpecialWorkflowTests|Verify_Update_WithAnAccountTypeIDNotInTheData_Should_ThrowAnInvalidDataException|{accountTypeID}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccountTable = true, DoAccountTypeTable = true, DoAccountStatusTable = true };
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var model = new AccountModel { ID = 1, Name = "Test", TypeID = accountTypeID, StatusID = 1 };
                // Act/Assert
                await Assert.ThrowsAsync<InvalidDataException>(() => new AccountWorkflow().UpdateAsync(model, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public Task Verify_Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataException()
        {
            return Task.WhenAll(
                Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(500),
                Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(1000),
                Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(10000),
                Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(100000),
                Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int.MaxValue - 1));
        }

        private static async Task Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataExceptionAsync(int accountStatusID)
        {
            // Arrange
            var contextProfileName = $"Accounts_Accounts_SpecialWorkflowTests|Verify_Update_WithAnAccountStatusIDNotInTheData_Should_ThrowAnInvalidDataException|{accountStatusID}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccountTable = true, DoAccountTypeTable = true, DoAccountStatusTable = true };
                await DoMockingSetupAsync(mockingSetup, contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var model = new AccountModel { ID = 1, Name = "Test", TypeID = 1, StatusID = accountStatusID };
                // Act/Assert
                await Assert.ThrowsAsync<InvalidDataException>(() => new AccountWorkflow().UpdateAsync(model, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
