// <copyright file="MFATests.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mfa tests class</summary>
namespace Clarity.Ecommerce.Testing.MultiFactorAuthentication
{
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Microsoft.AspNet.Identity;
    using Moq;
    using Utilities;
    using Xunit;

    [Trait("Category", "Multi-Factor Authentication")]
    public class MFATests : XUnitLogHelper
    {
        public MFATests(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            // BaseModelMapper.Initialize();
        }

        [Fact(Skip = "Sending SMS has a cost associated, do not run automatically")]
        public async Task Verify_TwilioIntegration_WorksAsync()
        {
            // Arrange
            var service = new CEFSMSService();
            var message = new IdentityMessage
            {
                Destination = "+15128254669",
                Body = "[Test] The code is 123456",
            };
            // Act
            await service.SendAsync(message).ConfigureAwait(false);
            // Assert
            Assert.NotEmpty(CEFSMSService.RecentResults);
            foreach (var result in CEFSMSService.RecentResults)
            {
                Assert.NotNull(result);
                Assert.IsType<Twilio.Rest.Api.V2010.Account.MessageResource>(result);
                Assert.Null(result.ErrorCode);
                Assert.Null(result.ErrorMessage);
                Assert.Equal(ConfigurationManager.AppSettings["Clarity.SMS.Twilio.AccountSID"], result.AccountSid);
                Assert.Equal("2010-04-01", result.ApiVersion);
                Assert.Equal("Sent from your Twilio trial account - " + message.Body, result.Body);
                Assert.Equal(Twilio.Rest.Api.V2010.Account.MessageResource.DirectionEnum.OutboundApi, result.Direction);
                Assert.Equal(Twilio.Rest.Api.V2010.Account.MessageResource.StatusEnum.Queued, result.Status);
                Assert.Equal("+19313454324", result.From.ToString());
                Assert.Equal(message.Destination, result.To);
                Assert.Equal("0", result.NumMedia);
                Assert.Equal("1", result.NumSegments);
                Assert.Null(result.Price);
                Assert.Equal("USD", result.PriceUnit);
                // "SMeff6597f0ba44833997aeccde0a830bb"
                Assert.NotNull(result.Sid);
                // "/2010-04-01/Accounts/ACb3fd416de76d235288de8f28fdfb331a/Messages/SMeff6597f0ba44833997aeccde0a830bb.json"
                Assert.NotNull(result.Uri);
            }
        }

        [Fact]
        public async Task Verify_CodeSend_Works()
        {
            // Arrange
            const string? contextProfileName = "MFATests|Verify_CodeSend_Works";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoUserTable = true,
                    DoUserRoleTable = true,
                    DoRoleUserTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var userStoreMock = new Mock<CEFUserStore>(context);
                userStoreMock.Setup(m => m.FindByIdAsync(It.IsAny<int>()))
                    .Returns<int>(id => Task.FromResult(mockingSetup.RawUsers!.SingleOrDefault(x => x.Object.ID == id)?.Object));
                userStoreMock.Setup(m => m.SetSecurityStampAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns<User, string>((u, s) =>
                    {
                        u.SecurityStamp = s;
                        return Task.CompletedTask;
                    });
                userStoreMock.Setup(m => m.UpdateAsync(It.IsAny<User>())).Returns<User>(
                    u =>
                    {
                        var obj = mockingSetup.RawUsers!.Single(x => x.Object.ID == u.ID);
                        obj.Object.SecurityStamp = u.SecurityStamp;
                        return Task.CompletedTask;
                    });
                userStoreMock.Setup(m => m.GetEmailAsync(It.IsAny<User>()))
                    .Returns<User>(u => Task.FromResult(u.Email!));
                userStoreMock.Setup(m => m.GetPhoneNumberAsync(It.IsAny<User>()))
                    .Returns<User>(u => Task.FromResult(u.PhoneNumber!));
                userStoreMock.Setup(m => m.GetEmailConfirmedAsync(It.IsAny<User>()))
                    .Returns<User>(_ => Task.FromResult(true));
                userStoreMock.Setup(m => m.GetPhoneNumberConfirmedAsync(It.IsAny<User>()))
                    .Returns<User>(_ => Task.FromResult(true));
                userStoreMock.Setup(m => m.GetTwoFactorEnabledAsync(It.IsAny<User>()))
                    .Returns<User>(_ => Task.FromResult(true));
                userStoreMock.Setup(m => m.GetSecurityStampAsync(It.IsAny<User>()))
                    .Returns<User>(u => Task.FromResult(u.SecurityStamp!));
                using var userManager = new CEFUserManager(userStoreMock.Object);
                const int userID = 1;
                var mockEmailService = new Mock<IIdentityMessageService>();
                var mockSMSService = new Mock<IIdentityMessageService>();
                mockEmailService.SetupAllProperties();
                mockEmailService.Setup(m => m.SendAsync(It.IsAny<IdentityMessage>()));
                mockSMSService.SetupAllProperties();
                mockSMSService.Setup(m => m.SendAsync(It.IsAny<IdentityMessage>()));
                userManager.EmailService = mockEmailService.Object;
                userManager.SmsService = mockSMSService.Object;
                var emailSubject = ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.ByEmail.Subject"];
                var emailBody = ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.ByEmail.Body"];
                var smsBody = ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.BySMS.Body"];
                // Assert
                Contract.RequiresAllValidKeys(emailSubject, emailBody);
                Assert.True(userManager.SupportsUserTwoFactor);
                // Act
                var setEnabledResult = await userManager.SetTwoFactorEnabledAsync(
                        userId: userID,
                        enabled: true)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(
                    setEnabledResult.Succeeded,
                    setEnabledResult.Errors.DefaultIfEmpty("no errors reported").Aggregate((c, n) => c + "\r\n" + n));
                Assert.Empty(setEnabledResult.Errors);
                // Act
                var isEnabledResult = await userManager.GetTwoFactorEnabledAsync(
                        userId: userID)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(isEnabledResult);
                // Act
                var validProvidersResult = await userManager.GetValidTwoFactorProvidersAsync(
                        userId: userID)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotEmpty(validProvidersResult);
                await Verify_MFA_ByEmail_WorksAsync(userManager, userID, setEnabledResult, mockEmailService, emailSubject, emailBody).ConfigureAwait(false);
                await Verify_MFA_BySMS_WorksAsync(userManager, userID, setEnabledResult, mockSMSService, smsBody).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        private async Task Verify_MFA_ByEmail_WorksAsync(
            CEFUserManager userManager,
            int userID,
            IdentityResult setEnabledResult,
            Mock<IIdentityMessageService> mockEmailService,
            string emailSubject,
            string emailBody)
        {
            // Act
            var token = await userManager.GenerateTwoFactorTokenAsync(
                    userId: userID,
                    twoFactorProvider: CEFUserManager.DataProtectionProviderNameForEmail)
                .ConfigureAwait(false);
            this.TestOutputHelper.WriteLine("Token: " + token);
            // Assert
            Assert.NotNull(token);
            // Act
            var notifyResult = await userManager.NotifyTwoFactorTokenAsync(
                    userId: userID,
                    twoFactorProvider: CEFUserManager.DataProtectionProviderNameForEmail,
                    token: token)
                .ConfigureAwait(false);
            // Assert
            Assert.True(
                notifyResult.Succeeded,
                setEnabledResult.Errors.DefaultIfEmpty("no errors reported").Aggregate((c, n) => c + "\r\n" + n));
            Assert.Empty(notifyResult.Errors);
            mockEmailService.Verify(m => m.SendAsync(It.IsAny<IdentityMessage>()), Times.Once);
            var index = 0;
            foreach (var invocation in mockEmailService.Invocations)
            {
                this.TestOutputHelper.WriteLine("Invocation: " + ++index);
                foreach (var arg in invocation.Arguments)
                {
                    this.TestOutputHelper.WriteLine("Argument:");
                    var asMsg = (IdentityMessage)arg;
                    this.TestOutputHelper.WriteLine("To");
                    this.TestOutputHelper.WriteLine(asMsg.Destination);
                    Assert.Equal("jothay@email.com", asMsg.Destination);
                    this.TestOutputHelper.WriteLine("Subject");
                    this.TestOutputHelper.WriteLine(asMsg.Subject);
                    Assert.Equal(emailSubject, asMsg.Subject);
                    this.TestOutputHelper.WriteLine("Body");
                    this.TestOutputHelper.WriteLine(asMsg.Body);
                    Assert.Equal(string.Format(emailBody, token), asMsg.Body);
                }
            }
            // Act
            var verifyResult = await userManager.VerifyTwoFactorTokenAsync(
                    userId: userID,
                    twoFactorProvider: CEFUserManager.DataProtectionProviderNameForEmail,
                    token: token)
                .ConfigureAwait(false);
            // Assert
            Assert.True(verifyResult);
        }

        private async Task Verify_MFA_BySMS_WorksAsync(
            CEFUserManager userManager,
            int userID,
            IdentityResult setEnabledResult,
            Mock<IIdentityMessageService> mockSMSService,
            string smsBody)
        {
            // Act
            var token = await userManager.GenerateTwoFactorTokenAsync(
                    userId: userID,
                    twoFactorProvider: CEFUserManager.DataProtectionProviderNameForSMS)
                .ConfigureAwait(false);
            this.TestOutputHelper.WriteLine("Token: " + token);
            // Assert
            Assert.NotNull(token);
            // Act
            var notifyResult = await userManager.NotifyTwoFactorTokenAsync(
                    userId: userID,
                    twoFactorProvider: CEFUserManager.DataProtectionProviderNameForSMS,
                    token: token)
                .ConfigureAwait(false);
            // Assert
            Assert.True(
                notifyResult.Succeeded,
                setEnabledResult.Errors.DefaultIfEmpty("no errors reported").Aggregate((c, n) => c + "\r\n" + n));
            Assert.Empty(notifyResult.Errors);
            mockSMSService.Verify(m => m.SendAsync(It.IsAny<IdentityMessage>()), Times.Once);
            var index = 0;
            foreach (var invocation in mockSMSService.Invocations)
            {
                this.TestOutputHelper.WriteLine("Invocation: " + ++index);
                foreach (var arg in invocation.Arguments)
                {
                    this.TestOutputHelper.WriteLine("Argument:");
                    var asMsg = (IdentityMessage)arg;
                    this.TestOutputHelper.WriteLine("To");
                    this.TestOutputHelper.WriteLine(asMsg.Destination);
                    Assert.Equal("555-555-0192", asMsg.Destination);
                    this.TestOutputHelper.WriteLine("Body");
                    this.TestOutputHelper.WriteLine(asMsg.Body);
                    Assert.Equal(string.Format(smsBody, token), asMsg.Body);
                }
            }
            // Act
            var verifyResult = await userManager.VerifyTwoFactorTokenAsync(
                    userId: userID,
                    twoFactorProvider: CEFUserManager.DataProtectionProviderNameForSMS,
                    token: token)
                .ConfigureAwait(false);
            // Assert
            Assert.True(verifyResult);
        }
    }
}
