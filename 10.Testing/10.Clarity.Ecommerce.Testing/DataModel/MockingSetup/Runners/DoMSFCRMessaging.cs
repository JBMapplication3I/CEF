// <copyright file="DoMockingSetupForContextRunnerMessaging.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner messaging class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerMessagingAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply data and set up IQueryable: Conversations
            if (DoAll || DoMessaging || DoConversationTable)
            {
                RawConversations = new()
                {
                    await CreateADummyConversationAsync(id: 1, key: "CONVERSATION-1").ConfigureAwait(false),
                };
                await InitializeMockSetConversationsAsync(mockContext, RawConversations).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Conversation Users
            if (DoAll || DoMessaging || DoConversationUserTable)
            {
                RawConversationUsers = new()
                {
                    await CreateADummyConversationUserAsync(1, "CONVERSATION-USER-1").ConfigureAwait(false),
                };
                await InitializeMockSetConversationUsersAsync(mockContext, RawConversationUsers).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Email Queues
            if (DoAll || DoMessaging || DoEmailQueueTable)
            {
                RawEmailQueues = new()
                {
                    await CreateADummyEmailQueueAsync(id: 1, key: "EMAIL-QUEUE-1", attempts: 0, addressesTo: "dev@claritymis.com", addressFrom: "dev@claritymis.com", subject: "The subject", body: "The body").ConfigureAwait(false),
                };
                await InitializeMockSetEmailQueuesAsync(mockContext, RawEmailQueues).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Email Queue Attachments
            if (DoAll || DoMessaging || DoEmailQueueAttachmentTable)
            {
                RawEmailQueueAttachments = new()
                {
                    await CreateADummyEmailQueueAttachmentAsync(id: 1, key: "EMAIL-QUEUE-ATTACHMENT-1", name: "some file", desc: "desc", fileAccessTypeID: 0).ConfigureAwait(false),
                };
                await InitializeMockSetEmailQueueAttachmentsAsync(mockContext, RawEmailQueueAttachments).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Email Statuses
            if (DoAll || DoMessaging || DoEmailStatusTable)
            {
                var index = 0;
                RawEmailStatuses = new()
                {
                    await CreateADummyEmailStatusAsync(id: ++index, key: "Pending", name: "Pending", desc: "desc", sortOrder: 1, displayName: "Pending").ConfigureAwait(false),
                    await CreateADummyEmailStatusAsync(id: ++index, key: "Retrying", name: "Send Failed - Retrying", desc: "desc", sortOrder: 1, displayName: "Send Failed - Retrying").ConfigureAwait(false),
                    await CreateADummyEmailStatusAsync(id: ++index, key: "Retries Failed", name: "Send Failed - Retries Failed", desc: "desc", sortOrder: 1, displayName: "Send Failed - Retries Failed").ConfigureAwait(false),
                    await CreateADummyEmailStatusAsync(id: ++index, key: "Sent", name: "Sent", desc: "desc", sortOrder: 1, displayName: "Sent").ConfigureAwait(false),
                    await CreateADummyEmailStatusAsync(id: ++index, key: "Cancelled", name: "Cancelled", desc: "desc", sortOrder: 1, displayName: "Cancelled").ConfigureAwait(false),
                };
                await InitializeMockSetEmailStatusesAsync(mockContext, RawEmailStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Email Templates
            if (DoAll || DoMessaging || DoEmailTemplateTable)
            {
                RawEmailTemplates = new()
                {
                    // Email Defaults
                    await CreateADummyEmailTemplateAsync(id: 01, key: "Emails.Defaults.IncludesContent", name: "Default Email Includes Content (CSS, etc)", desc: "desc", body: "<style>/* CSS Content to be included*/</style>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 02, key: "Emails.Defaults.HeaderContent", name: "Default Email Header Content (the layout of the top visible part of the email)", desc: "desc", body: "<table><tbody>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 03, key: "Emails.Defaults.FooterContent", name: "Default Email Footer Content (the layout of the bottom visible part of the email)", desc: "desc", body: "</tbody></table>").ConfigureAwait(false),
                    // Authentication
                    await CreateADummyEmailTemplateAsync(id: 04, key: "Emails.Authentication.Invitation.MainContent", name: "Email.Authentication.Invitation Main Content", desc: "desc", body: "<tr><td>Invite content {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 05, key: "Emails.Authentication.InvitationWithCode.MainContent", name: "Email.Authentication.InvitationWithCode Main Content", desc: "desc", body: "<tr><td>InvitationWithCode content {{InviteCode}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 06, key: "Emails.Authentication.NewUserRegistered.ToUser.MainContent", name: "Email.Authentication.NewUserRegistered.ToUser Main Content", desc: "desc", body: "<tr><td>NewUserRegistered content {{FirstName}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 07, key: "Emails.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser.MainContent", name: "Email.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser Main Content", desc: "desc", body: "<tr><td>NewUserRegistered content {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 08, key: "Emails.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser.MainContent", name: "Email.Authentication.EmailVerificationAndForcedPasswordReset.ToUser Main Content", desc: "desc", body: "<tr><td>EmailVerificationAndForcedPasswordReset content {{ResetToken}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 09, key: "Emails.Authentication.BackOfficeApprovedUserNotification.ToUser.MainContent", name: "Email.Authentication.BackOfficeApprovedUserNotification.ToUser Main Content", desc: "desc", body: "<tr><td>BackOfficeApprovedUserNotification content {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 10, key: "Emails.Authentication.UserPasswordResetTokenRequest.MainContent", name: "Email.Authentication.UserPasswordResetTokenRequest Main Content", desc: "desc", body: "<tr><td>UserPasswordResetTokenRequest content {{ResetToken}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 11, key: "Emails.Authentication.UserForgotUsernameRequest.MainContent", name: "Email.Authentication.UserForgotUsernameRequest Main Content", desc: "desc", body: "<tr><td>UserForgotUsernameRequest {{Username}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 12, key: "Emails.Authentication.NewSellerRegistered.ToSeller.MainContent", name: "Email.Authentication.NewSellerRegistered.ToSeller Main Content", desc: "desc", body: "<tr><td>NewSellerRegistered content {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 13, key: "Emails.Authentication.NewSellerRegistered.ToBackOffice.MainContent", name: "Email.Authentication.NewSellerRegistered.ToBackOffice Main Content", desc: "desc", body: "<tr><td>NewSellerRegistered content {{Token}}</td></tr>").ConfigureAwait(false),
                    // Messaging
                    await CreateADummyEmailTemplateAsync(id: 14, key: "Emails.Messaging.NewWaiting.MainContent", name: "Email.Messaging.NewWaiting Main Content", desc: "desc", body: "<tr><td>New Message is Waiting content {{Subject}} {{Body}}</td></tr>").ConfigureAwait(false),
                    // Products
                    await CreateADummyEmailTemplateAsync(id: 15, key: "Emails.Products.ProductNowInStock.MainContent", name: "Email.Products.ProductNowInStock Main Content", desc: "desc", body: "<tr><td>Product Now In Stock content <a href=\"{{RootUrl}}{{ProductDetailUrlFragment}}/{{ProductSeoUrl}}\">{{ProductName}}</a></td></tr>").ConfigureAwait(false),
                    // Sales
                    await CreateADummyEmailTemplateAsync(id: 16, key: "Emails.SalesOrders.CustomerReceipt.MainContent", name: "Email.SalesOrders.CustomerReceipt Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 17, key: "Emails.SalesOrders.ToBackOffice.MainContent", name: "Email.SalesOrders.ToBackOffice Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 18, key: "Emails.SalesOrders.ToBackOfficeStore.MainContent", name: "Email.SalesOrders.ToBackOfficeStore Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 19, key: "Emails.SalesOrders.Confirmed.MainContent", name: "Email.SalesOrders.Confirmed Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 20, key: "Emails.SalesOrders.Backordered.MainContent", name: "Email.SalesOrders.Backordered Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 21, key: "Emails.SalesOrders.Split.MainContent", name: "Email.SalesOrders.Split Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 22, key: "Emails.SalesOrders.InvoiceCreated.MainContent", name: "Email.SalesOrders.InvoiceCreated Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 23, key: "Emails.SalesOrders.PartialPayment.MainContent", name: "Email.SalesOrders.PartialPayment Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 24, key: "Emails.SalesOrders.FullPayment.MainContent", name: "Email.SalesOrders.FullPayment Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 25, key: "Emails.SalesOrders.Processing.MainContent", name: "Email.SalesOrders.Processing Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 26, key: "Emails.SalesOrders.DropShipped.MainContent", name: "Email.SalesOrders.DropShipped Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 27, key: "Emails.SalesOrders.Shipped.MainContent", name: "Email.SalesOrders.Shipped Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 28, key: "Emails.SalesOrders.ReadyForPickup.MainContent", name: "Email.SalesOrders.ReadyForPickup Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 29, key: "Emails.SalesOrders.Completed.MainContent", name: "Email.SalesOrders.Completed Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                    await CreateADummyEmailTemplateAsync(id: 30, key: "Emails.SalesOrders.Voided.MainContent", name: "Email.SalesOrders.Voided Main Content", desc: "desc", body: "<tr><td>Sales Order Status Change Notification {{Token}}</td></tr>").ConfigureAwait(false),
                };
                await InitializeMockSetEmailTemplatesAsync(mockContext, RawEmailTemplates).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Email Types
            if (DoAll || DoMessaging || DoEmailTypeTable)
            {
                var index = 0;
                RawEmailTypes = new()
                {
                    await CreateADummyEmailTypeAsync(id: ++index, key: "General", name: "General", desc: "desc", sortOrder: 1, displayName: "General").ConfigureAwait(false),
                };
                await InitializeMockSetEmailTypesAsync(mockContext, RawEmailTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Messages
            if (DoAll || DoMessaging || DoMessageTable)
            {
                RawMessages = new()
                {
                    await CreateADummyMessageAsync(id: 1, key: "MESSAGE-1", context: "The context", subject: "Message Subject", body: "Message Body").ConfigureAwait(false),
                };
                await InitializeMockSetMessagesAsync(mockContext, RawMessages).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Message Attachments
            if (DoAll || DoMessaging || DoMessageAttachmentTable)
            {
                RawMessageAttachments = new()
                {
                    await CreateADummyMessageAttachmentAsync(id: 1, key: "MESSAGE-ATTACHMENT-1", name: "some file", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetMessageAttachmentsAsync(mockContext, RawMessageAttachments).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Message Recipients
            if (DoAll || DoMessaging || DoMessageRecipientTable)
            {
                RawMessageRecipients = new()
                {
                    await CreateADummyMessageRecipientAsync(id: 1, key: "MESSAGE-RECIPIENT-1", slaveID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetMessageRecipientsAsync(mockContext, RawMessageRecipients).ConfigureAwait(false);
            }
            #endregion
            #region Apply data and set up IQueryable: Product Notifications
            if (DoAll || DoMessaging || DoProductNotificationTable)
            {
                RawProductNotifications = new()
                {
                    await CreateADummyProductNotificationAsync(id: 1, key: "Notification-1", name: "1152-1", desc: "desc", productID: 1152).ConfigureAwait(false),
                };
                await InitializeMockSetProductNotificationsAsync(mockContext, RawProductNotifications).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
