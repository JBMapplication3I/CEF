// <copyright file="OrbitalPaymentechPaymentsProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the orbital paymentech payments provider tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Payments.OrbitalPaymentech;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Providers.Payments.OrbitalPaymentech")]
    public class OrbitalPaymentechPaymentsProviderTests : BasePaymentTest
    {
        public OrbitalPaymentechPaymentsProviderTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private IPaymentModel OrbitalPaymentechTestPayment(string token = null)
        {
            // This must change to create a unique CustomerRefNumber for Test_Wallet_CreateCustomerProfile
            Payment.CardHolderName = $"Test User{DateTime.UtcNow.Ticks}";
            // Override Test CC Number for OrbitalPaymentech
            //Payment.CardNumber = "4011361100000012";
            //Payment.CardNumber = "371449635398431";
            Payment.CardNumber = "341134113411347";
            // Override Test CVV for OrbitalPaymentech
            Payment.CVV = "2233";
            Payment.Token = token;
            return Payment;
        }

        [Fact(Skip = "Don't Run Automatically.")]
        public async Task Test_AuthorizeWithDelayedCapture()
        {
            // Arrange
            var contextProfileName = $"{nameof(OrbitalPaymentechPaymentsProviderTests)}|{nameof(Test_AuthorizeWithDelayedCapture)}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new OrbitalPaymentechPaymentsProvider();
                await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
                Assert.True(provider.HasValidConfiguration);
                // Act
                var result1 = await provider.AuthorizeAsync(
                    OrbitalPaymentechTestPayment(),
                    BillingModel,
                    ShippingModel,
                    contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result1.Approved, result1.ResponseCode);
                Assert.Equal(42, result1.Amount);
                Assert.NotNull(result1.TransactionID);
                // Act/Assert
                await Assert.ThrowsAsync<NotImplementedException>(() => provider.CaptureAsync(
                     result1.AuthorizationCode,
                     OrbitalPaymentechTestPayment(),
                     contextProfileName)).ConfigureAwait(false);
                // // Act
                // var result2 = provider.Capture(
                //      result1.AuthorizationCode,
                //      OrbitalPaymentechTestPayment(),
                //      null);
                // // Assert
                // Assert.True(result2.Approved, result2.ResponseCode);
                // Assert.Equal(42, result2.Amount);
                // Assert.NotNull(result2.TransactionID);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Don't Run Automatically.")]
        public async Task Verify_AuthorizeAndCapture()
        {
            // Arrange
            var provider = new OrbitalPaymentechPaymentsProvider();
            await provider.InitConfigurationAsync(contextProfileName: null).ConfigureAwait(false);
            Assert.True(provider.HasValidConfiguration);
            // Act
            var result = await provider.AuthorizeAndACaptureAsync(
                OrbitalPaymentechTestPayment(),
                BillingModel,
                ShippingModel,
                null).ConfigureAwait(false);
            // Assert
            Assert.True(result.Approved, result.ResponseCode);
            Assert.Equal(42, result.Amount);
            Assert.NotNull(result.TransactionID);
        }

        [Fact(Skip = "Don't Run Automatically.")]
        public async Task Verify_WalletProcess_Works()
        {
            var contextProfileName = $"{nameof(OrbitalPaymentechPaymentsProviderTests)}.{nameof(Verify_WalletProcess_Works)}";
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new OrbitalPaymentechPaymentsProvider();
                await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
                Assert.True(provider.HasValidConfiguration);
                // 1. Test_Wallet_CreateCustomerProfile
                // Act
                var result1 = await provider.CreateCustomerProfileAsync(OrbitalPaymentechTestPayment(),
                    BillingModel,
                    contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result1.Approved, result1.ResponseCode);
                Assert.True(
                    OrbitalPaymentechPaymentsProvider.TryParseToken(
                        result1.Token,
                        out _,
                        out _),
                    "Token was null or not valid.");
                // 2. Test_Wallet_GetCustomerProfile
                // Act
                var result2 = await provider.GetCustomerProfileAsync(OrbitalPaymentechTestPayment(result1.Token),
                    contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result2.Approved, result2.ResponseCode);
                // 3. Test_AuthorizeAndCaptureWithToken
                // Act
                var result3 = await provider.AuthorizeAndACaptureAsync(OrbitalPaymentechTestPayment(result1.Token),
                    BillingModel,
                    ShippingModel,
                    contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result3.Approved, result3.ResponseCode);
                Assert.Equal(42, result3.Amount);
                Assert.NotNull(result3.TransactionID);
                // 4. Test_Wallet_UpdateCustomerProfile
                // Act
                var result4 = await provider.UpdateCustomerProfileAsync(OrbitalPaymentechTestPayment(result1.Token),
                    BillingModel,
                    null).ConfigureAwait(false);
                // Assert
                Assert.True(result4.Approved, result4.ResponseCode);
                // 5. Test_Wallet_DeleteCustomerProfile
                // Act
                var result5 = await provider.DeleteCustomerProfileAsync(OrbitalPaymentechTestPayment(result1.Token),
                    contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result5.Approved, result5.ResponseCode);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Don't Run Automatically.")]
        public async Task Verify_SubscriptionProcess_Works()
        {
            var contextProfileName = $"{nameof(OrbitalPaymentechPaymentsProviderTests)}.{nameof(Verify_SubscriptionProcess_Works)}";
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new OrbitalPaymentechPaymentsProvider();
                await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
                Assert.True(provider.HasValidConfiguration);
                var model = RegistryLoaderWrapper.GetInstance<ISubscriptionPaymentModel>();
                model.Payment = OrbitalPaymentechTestPayment();
                model.Payment.BillingContact = BillingModel;
                // Act
                var result1 = await provider.CreateSubscriptionAsync(model, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result1);
                Assert.True(result1.Approved, result1.ResponseCode);
                Assert.NotNull(result1.AuthorizationCode);
                Assert.NotNull(result1.TransactionID);
                // Act/Assert
                await Assert.ThrowsAsync<NotImplementedException>(() => provider.GetSubscriptionAsync(null, contextProfileName));
                // Act/Assert
                await Assert.ThrowsAsync<NotImplementedException>(() => provider.UpdateSubscriptionAsync(null, contextProfileName));
                // Act/Assert
                await Assert.ThrowsAsync<NotImplementedException>(() => provider.CancelSubscriptionAsync(null, contextProfileName));
            }
        }

        ////[Fact]
        ////public async Task Verify_MSDKExamples_Run()
        ////{
        ////    NCMOnlineAuth.NCMOnlineAuth.Main(new string[0]);
        ////
        ////    ORBOnlineAuth.ORBOnlineAuth.Main(new string[0]);
        ////
        ////    PNSOnlineAuth.PNSOnlineAuth.Main(new string[0]);
        ////    PNSOnlineChipCard.PNSOnlineChipCard.Main(new string[0]);
        ////    PNSOnlineEcho.PNSOnlineEcho.Main(new string[0]);
        ////
        ////    PNSUpload.PNSUpload.Main(new string[0]);
        ////    PNSUploadArrayReq.PNSUploadArrayReq.Main(new string[0]);
        ////    PNSUploadQryArrayResp.PNSUploadQryArrayResp.Main(new string[0]);
        ////
        ////    SLMBatchDFRSFTP.SLMBatchDFRSFTP.Main(new string[0]);
        ////    SLMBatchDFRTCP.SLMBatchDFRTCP.Main(new string[0]);
        ////    SLMBatchRespSFTP.SLMBatchRespSFTP.Main(new string[0]);
        ////    SLMBatchRespTCP.SLMBatchRespTCP.Main(new string[0]);
        ////    SLMBatchRFS.SLMBatchRFS.Main(new string[0]);
        ////    SLMBatchSubmit.SLMBatchSubmit.Main(new string[0]);
        ////
        ////    SLMOnlineAuth.SLMOnlineAuth.Main(new string[0]);
        ////    SLMOnlineAuthChaseNet.SLMOnlineAuthChaseNet.Main(new string[0]);
        ////    SLMOnlineHeartBeat.SLMOnlineHeartBeat.Main(new string[0]);
        ////}
    }
}
