// <copyright file="PayoneerPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payoneer payments provider tests class</summary>
// ReSharper disable StringIndexOfIsCultureSpecific.1
namespace Clarity.Ecommerce.Providers.Payments.Payoneer.Testing
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Payments.Payoneer;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;

    [Trait("Category", "Providers.Payments.Payoneer")]
    public class PayoneerPaymentsProviderTests
    {
        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_HandleWebhookEvent_Works()
        {
            // Arrange
            Mapper.BaseModelMapper.Initialize();
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_HandleWebhookEvent_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoSalesOrderEventTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new PayoneerPaymentsProvider();
                await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
                Assert.True(provider.HasValidConfiguration);
                var apiKeyReturn = new ApiKeyWebhookReturn();
                var orderEventReturn = new OrderEventWebhookReturn
                {
                    OrderID = 12345678910123,
                };
                var orderReturn = new OrderWebhookReturn();
                // Act
                await PayoneerPaymentsProvider.HandleWebhookEventAsync(
                        workflows: RegistryLoaderWrapper.GetInstance<IWorkflowsController>(contextProfileName),
                        apiKeyReturn: apiKeyReturn,
                        orderEventReturn: orderEventReturn,
                        orderReturn: orderReturn,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_Authorize_ThrowsANotImplementedException()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_Authorize_ThrowsANotImplementedException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new PayoneerPaymentsProvider();
                await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
                Assert.True(provider.HasValidConfiguration);
                // Act/Assert
                await Assert.ThrowsAsync<NotImplementedException>(
                        () => provider.AuthorizeAsync(null!, null, null, false, contextProfileName))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_Capture_ThrowsANotImplementedException()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_Capture_ThrowsANotImplementedException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new PayoneerPaymentsProvider();
                await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
                Assert.True(provider.HasValidConfiguration);
                // Act/Assert
                await Assert.ThrowsAsync<NotImplementedException>(
                        () => provider.CaptureAsync(null!, null!, contextProfileName))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_AuthorizeAndACapture_ThrowsANotImplementedException()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_AuthorizeAndACapture_ThrowsANotImplementedException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new PayoneerPaymentsProvider();
                await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
                Assert.True(provider.HasValidConfiguration);
                // Act/Assert
                await Assert.ThrowsAsync<NotImplementedException>(
                        () => provider.AuthorizeAndACaptureAsync(null!, null, null, false, contextProfileName))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_CreateAPayoneerAccount_Works()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_CreateAPayoneerAccount_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var store = new StoreModel
                {
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary(),
                };
                var user = new UserModel
                {
                    Email = "fake@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary(),
                };
                // Act
                var result = provider.CreateAPayoneerAccount(store, user);
                // Assert
                Assert.True(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_AssignAccountUser_Works()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_AssignAccountUser_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var store = new StoreModel
                {
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        }
                    },
                };
                var user = new UserModel
                {
                    Email = "fake@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        }
                    },
                };
                // Act
                var result = provider.AssignAccountUser(store, user);
                // Assert
                Assert.True(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_GetAnAuthenticatedURLForStoreOwnersToSetUpPayoutAccountsForCurrentStore_Works()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_GetAnAuthenticatedURLForStoreOwnersToSetUpPayoutAccountsForCurrentStore_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var store = new StoreModel
                {
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        }
                    },
                };
                var user = new UserModel
                {
                    Email = "fake@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        },
                        ["Payoneer-User-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-User-ID",
                            Value = "1"
                        }
                    },
                };
                // Act
                var result = provider.GetAnAuthenticatedURLForStoreOwnersToSetUpPayoutAccountsForCurrentStore(store, user);
                // Assert
                Assert.NotNull(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_ValidateReady_Works()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_ValidateReady_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var store = new StoreModel
                {
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        }
                    },
                };
                var seller = new UserModel
                {
                    Email = "fake@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        },
                        ["Payoneer-User-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-User-ID",
                            Value = "1"
                        }
                    },
                };
                var buyer = new UserModel
                {
                    Email = "fake.buyer@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary(),
                };
                // Act
                var result = PayoneerPaymentsProvider.ValidateReady(store, seller, buyer, 2, 3);
                // Assert
                Assert.True(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_CreateAnEscrowOrderToFacilitateATransactionForGoods_Works()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_CreateAnEscrowOrderToFacilitateATransactionForGoods_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var store = new StoreModel
                {
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        }
                    },
                };
                var seller = new UserModel
                {
                    Email = "fake@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        },
                        ["Payoneer-User-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-User-ID",
                            Value = "1"
                        }
                    },
                };
                var buyer = new UserModel
                {
                    Email = "fake.buyer@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary(),
                };
                var order = new SalesOrder();
                var cart = new CartModel();
                // Act
                var result = provider.CreateAnEscrowOrderToFacilitateATransactionForGoods(
                    store,
                    seller,
                    buyer,
                    order,
                    cart,
                    5.00m,
                    2,
                    3);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_GetAnAuthenticatedURLForUsersToBePresentedWithPaymentInstructions_Works()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_GetAnAuthenticatedURLForUsersToBePresentedWithPaymentInstructions_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var buyer = new UserModel
                {
                    Email = "fake.buyer@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary(),
                };
                var order = new SalesOrderModel
                {
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Order-URI"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Order-URI",
                            Value = "/accounts/0/orders/12345678910123"
                        },
                    },
                };
                // Act
                var result = provider.GetAnAuthenticatedURLForUsersToBePresentedWithPaymentInstructions(
                    buyer,
                    order,
                    2,
                    3);
                // Assert
                Assert.NotNull(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_GetAnAuthenticatedURLForStoreOwnersToAddATrackingNumberToTheEscrowOrder_Works()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_GetAnAuthenticatedURLForStoreOwnersToAddATrackingNumberToTheEscrowOrder_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var store = new StoreModel
                {
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        },
                        ["Payoneer-Account-URI"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "/accounts/0"
                        }
                    },
                };
                var seller = new UserModel
                {
                    Email = "fake@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "0"
                        },
                        ["Payoneer-User-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-User-ID",
                            Value = "1"
                        },
                        ["Payoneer-User-URI"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-User-ID",
                            Value = "/accounts/0/users/1"
                        }
                    },
                };
                var order = new SalesOrderModel
                {
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Order-URI"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Order-URI",
                            Value = "/accounts/0/orders/12345678910123"
                        }
                    },
                };
                // Act
                var result = provider.GetAnAuthenticatedURLForStoreOwnersToAddATrackingNumberToTheEscrowOrder(
                    store,
                    seller,
                    order);
                // Assert
                Assert.NotNull(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact/*(Skip = "Don't run automatically")*/]
        public async Task Verify_GetAnAuthenticatedReleaseFundsURLForAnEscrowOrder_Works()
        {
            // Arrange
            const string contextProfileName = "PayoneerPaymentsProviderTests|Verify_GetAnAuthenticatedReleaseFundsURLForAnEscrowOrder_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderEventTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = await OverrideTheWebClientAsync(contextProfileName).ConfigureAwait(false);
                var buyer = new UserModel
                {
                    Email = "fake.buyer@email.com",
                    Contact = new ContactModel
                    {
                        Address = new AddressModel(),
                    },
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Account-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Account-ID",
                            Value = "2"
                        },
                        ["Payoneer-User-ID"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-User-ID",
                            Value = "3"
                        },
                        ["Payoneer-User-URI"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-User-ID",
                            Value = "/accounts/2/users/3"
                        }
                    },
                };
                var order = new SalesOrderModel
                {
                    SerializableAttributes = new SerializableAttributesDictionary
                    {
                        ["Payoneer-Order-URI"] = new SerializableAttributeObject
                        {
                            ID = 1,
                            Key = "Payoneer-Order-URI",
                            Value = "/accounts/0/orders/12345678910123"
                        }
                    },
                };
                // Act
                var result = provider.GetAnAuthenticatedReleaseFundsURLForAnEscrowOrder(buyer, order);
                // Assert
                Assert.NotNull(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        private class MockWebClientFactory : IWebClientFactory
        {
            public IWebClient Create()
            {
                var mockWebClient = new Mock<IWebClient>();
                mockWebClient.Setup(m => m.BaseAddress).Returns(() => "file://C:/Data/Projects/CEF-Testing/");
                mockWebClient.Setup(m => m.Headers)
                    .Returns(() => new WebHeaderCollection
                    {
                        ["Content-Type"] = "application/json",
                        ["x-armorpayments-apikey"] = "FAKE-API-Key",
                        ["x-armorpayments-requesttimestamp"] = DateExtensions.GenDateTime.ToString("yyyy-MM-ddThh:mm:sszzz"),
                    });
                mockWebClient
                    .Setup(m => m.UploadString(It.Is<string>(x => x == "/accounts"), It.IsAny<string>()))
                    .Returns(() => JsonConvert.SerializeObject(new CreateAccountResult { AccountID = 0 }));
                mockWebClient
                    .Setup(m => m.UploadString(It.Is<string>(x => x == "/accounts/0/users/1/authentications"), It.IsAny<string>()))
                    .Returns(() => JsonConvert.SerializeObject(new CreateAuthenticatedURLReturn
                    {
                        AccountID = 0,
                        UserID = 1,
                        Uri = "/accounts/0/backaccounts",
                        Url = "https://pay.payoneer.com/accounts/0/bankaccounts?action=create&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e6",
                        Action = "create",
                        Created = new DateTime(2018, 1, 1, 0, 0, 0),
                        Expires = new DateTime(2018, 1, 1, 0, 30, 0),
                    }));
                mockWebClient
                    .Setup(m => m.UploadString(It.Is<string>(x => x == "/accounts/0/orders"), It.IsAny<string>()))
                    .Returns(() => JsonConvert.SerializeObject(new CreateEscrowOrderReturn
                    {
                        AccountID = 0,
                        BuyerAccountID = 2,
                        BuyerUserID = 3,
                        SellerID = 1,
                        Amount = 5.00m,
                        OrderID = 12345678910123,
                        Type = (int)PayoneerOrderEventTypes.OrderCreate,
                        Status = (int)PayoneerOrderStatuses.New,
                    }));
                mockWebClient
                    .Setup(m => m.UploadString(It.Is<string>(x => x == "/accounts/0/"), It.IsAny<string>()))
                    .Returns(() => JsonConvert.SerializeObject(new CreateAccountResult { AccountID = 0 }));
                mockWebClient
                    .Setup(m => m.UploadString(It.Is<string>(x => x == "/accounts/2/users/3/authentications"), It.IsRegex(".*/paymentinstructions.*")))
                    .Returns(() => JsonConvert.SerializeObject(new CreateAuthenticatedURLReturn
                    {
                        AccountID = 2,
                        UserID = 3,
                        Uri = "/accounts/0/orders/12345678910123/paymentinstructions",
                        Url = "https://pay.payoneer.com/accounts/0/orders/12345678910123/paymentinstructions?action=create&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e6",
                    }));
                mockWebClient
                    .Setup(m => m.UploadString(It.Is<string>(x => x == "/accounts/0/users/1/authentications"), It.IsRegex(".*/shipments.*")))
                    .Returns(() => JsonConvert.SerializeObject(new CreateAuthenticatedURLReturn
                    {
                        AccountID = 2,
                        UserID = 3,
                        Uri = "/accounts/0/orders/12345678910123/shipments",
                        Url = "https://pay.payoneer.com/accounts/0/orders/12345678910123/shipments?action=create&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e6",
                    }));
                mockWebClient
                    .Setup(m => m.UploadString(It.Is<string>(x => x == "/accounts/2/users/3/authentications"), It.IsRegex(".*release.*")))
                    .Returns(() => JsonConvert.SerializeObject(new CreateAuthenticatedURLReturn
                    {
                        AccountID = 2,
                        UserID = 3,
                        Uri = "/accounts/0/orders/12345678910123/release",
                        Url = "https://pay.payoneer.com/accounts/0/orders/12345678910123/release?action=create&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e6",
                    }));
                mockWebClient
                    .Setup(m => m.DownloadString(It.Is<string>(x => x == "/accounts/0/users")))
                    .Returns(() => JsonConvert.SerializeObject(new[]
                    {
                        new UserResult
                        {
                            AccountID = "0",
                            UserID = "1",
                            Email = "fake@email.com"
                        },
                    }));
                return mockWebClient.Object;
            }
        }

        private static async Task<PayoneerPaymentsProvider> OverrideTheWebClientAsync(string contextPofileName)
        {
            var provider = new PayoneerPaymentsProvider();
            await provider.InitConfigurationAsync(contextPofileName).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            provider.WebClientFactory = new MockWebClientFactory();
            return provider;
        }
    }
}
