// <copyright file="WalletWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the wallet workflow tests class</summary>
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Payments;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Mapper;
    using Models;
    using Moq;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Payments.Wallets.Special")]
    public class Payments_Wallets_SpecialWorkflowTests : XUnitLogHelper
    {
        public Payments_Wallets_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        // Mock PaymentWallet that returns PaymentWalletResponse which is approved
        private static IWalletProviderBase GetMockPaymentWallet()
        {
            var mockPaymentWallet = new Mock<IWalletProviderBase>();
            mockPaymentWallet
                .Setup(m => m.CreateCustomerProfileAsync(It.IsAny<Interfaces.Providers.IProviderPayment>(), It.IsAny<IContactModel>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult<IPaymentWalletResponse>(new PaymentWalletResponse { Approved = true, Token = "TESTING" }));
            mockPaymentWallet
                .Setup(m => m.UpdateCustomerProfileAsync(It.IsAny<Interfaces.Providers.IProviderPayment>(), It.IsAny<IContactModel>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult<IPaymentWalletResponse>(new PaymentWalletResponse { Approved = true, Token = "TESTING" }));
            mockPaymentWallet
                .Setup(m => m.DeleteCustomerProfileAsync(It.IsAny<Interfaces.Providers.IProviderPayment>(), It.IsAny<string>()))
                .Returns(() => Task.FromResult<IPaymentWalletResponse>(new PaymentWalletResponse { Approved = true, Token = "TESTING" }));
            return mockPaymentWallet.Object;
        }

        [Fact]
        public async Task Verify_GetAccountWallet_ByUserID_ReturnsAListOfModelsWithFullMapping()
        {
            // Arrange
            const string contextProfileName = "Payments_Wallets_SpecialWorkflowTests|Verify_GetAccountWallet_ByUserID_ReturnsAListOfModelsWithFullMapping";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                BaseModelMapper.Initialize();
                var mockingSetup = new MockingSetup { DoContacts = true, DoPayments = true };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var results = await new WalletWorkflow().GetWalletForUserAsync(
                        1,
                        GetMockPaymentWallet(),
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(results.ActionSucceeded);
                Assert.IsType<List<IWalletModel>>(results.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CreateCreditCard_ReturnsAModelWithFullMapping()
        {
            // Arrange
            const string contextProfileName = "Payments_Wallets_SpecialWorkflowTests|Verify_CreateCreditCard_ReturnsAModelWithFullMapping";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                BaseModelMapper.Initialize();
                var mockingSetup = new MockingSetup { DoContacts = true, DoPayments = true, };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = new WalletWorkflow();
                var walletModel = new WalletModel
                {
                    CardHolderName = "John Smith",
                    CreditCardNumber = "4111111111111111",
                    ExpirationMonth = 12,
                    ExpirationYear = DateTime.Today.Year + 1,
                    UserID = 1,
                };
                // Act
                var result = await workflow.CreateWalletEntryAsync(walletModel, GetMockPaymentWallet(), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded, result.Messages.DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + "\r\n" + n));
                Assert.IsType<WalletModel>(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_UpdateCreditCard_ReturnsAModelWithFullMapping()
        {
            // Arrange
            const string contextProfileName = "Payments_Wallets_SpecialWorkflowTests|Verify_UpdateCreditCard_ReturnsAModelWithFullMapping";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                BaseModelMapper.Initialize();
                var mockingSetup = new MockingSetup
                {
                    DoAccounts = true,
                    DoPayments = true,
                    DoContactTable = true,
                    DoContactTypeTable = true,
                    DoAddressTable = true,
                    DoRegionTable = true,
                    DoCountryTable = true,
                    DoAccountTypeTable = true,
                    DoAccountStatusTable = true,
                    DoStoreTable = true,
                    DoUserTable = true,
                    DoUserTypeTable = true,
                    DoUserStatusTable = true,
                    DoStoreUserTable = true,
                    DoFavoriteCategoryTable = true,
                    DoFavoriteVendorTable = true,
                    DoFavoriteStoreTable = true,
                    DoFavoriteManufacturerTable = true,
                };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = new WalletWorkflow();
                var walletModel = new WalletModel { ID = 1, CreditCardNumber = "4111111111111111", UserID = 1 };
                // Act
                var result = await workflow.UpdateWalletEntryAsync(walletModel, GetMockPaymentWallet(), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded, result.Messages.DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + "\r\n" + n));
                Assert.IsType<WalletModel>(result.Result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_DeactivateCreditCard_ReturnsTrue()
        {
            // Arrange
            const string contextProfileName = "Payments_Wallets_SpecialWorkflowTests|Verify_DeactivateCreditCard_ReturnsTrue";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoAccounts = true, DoPayments = true };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = new WalletWorkflow();
                // Act
                var result = await workflow.DeactivateWalletEntryAsync(1, GetMockPaymentWallet(), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded, result.Messages.DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + "\r\n" + n));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
