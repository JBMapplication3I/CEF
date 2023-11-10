// <copyright file="DoMockingSetupForContextRunnerSystem.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner system class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerSystemAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Import Export Mappings
            if (DoAll || DoSystem || DoImportExportMappingTable)
            {
                var index = 0;
                RawImportExportMappings = new()
                {
                    await CreateADummyImportExportMappingAsync(id: ++index, key: "IMPORT-EXPORT-MAPPING-1", name: "Import Export Mapping 1", desc: "desc", mappingJson: string.Empty, mappingJsonHash: 123456879).ConfigureAwait(false),
                };
                await InitializeMockSetImportExportMappingsAsync(mockContext, RawImportExportMappings).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Notes
            if (DoAll || DoSystem || DoNoteTable)
            {
                var index = 0;
                RawNotes = new()
                {
                    await CreateADummyNoteAsync(id: ++index, key: "NOTE-A", note1: "This is an order note", salesOrderID: 30000).ConfigureAwait(false),
                    await CreateADummyNoteAsync(id: ++index, key: "NOTE-U", note1: "This is a user note").ConfigureAwait(false),
                };
                await InitializeMockSetNotesAsync(mockContext, RawNotes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Note Types
            if (DoAll || DoSystem || DoNoteTypeTable)
            {
                var index = 0;
                RawNoteTypes = new()
                {
                    await CreateADummyNoteTypeAsync(id: ++index, key: "Order Note", name: "Order Note", desc: "desc", displayName: "Order Note", isPublic: true).ConfigureAwait(false),
                    await CreateADummyNoteTypeAsync(id: ++index, key: "Private Note", name: "Private Note", desc: "desc", displayName: "Private Note").ConfigureAwait(false),
                    await CreateADummyNoteTypeAsync(id: ++index, key: "Customer Note", name: "Customer Note", desc: "desc", displayName: "Customer Note", isCustomer: true, isPublic: true).ConfigureAwait(false),
                };
                await InitializeMockSetNoteTypesAsync(mockContext, RawNoteTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Settings
            if (DoAll || DoSystem || DoSettingTable)
            {
                var index = 0;
                RawSettings = new()
                {
                    // Email Defaults
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Defaults.RequiresHttps", typeID: 01, value: "true", settingGroupID: 02).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Defaults.ReturnPath", typeID: 02, value: "/", settingGroupID: 02).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Defaults.From", typeID: 03, value: "dev@claritymis.com", settingGroupID: 02).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Defaults.To", typeID: 04, value: "", settingGroupID: 02).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Defaults.CC", typeID: 05, value: "", settingGroupID: 02).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Defaults.BCC", typeID: 06, value: "", settingGroupID: 02).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Defaults.Token", typeID: 07, value: Guid.Empty.ToString(), settingGroupID: 02).ConfigureAwait(false),
                    // Authentication
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.Invitation.Subject", typeID: 08, value: "Invite to join {{CompanyName}}", settingGroupID: 03).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.InvitationWithCode.Subject", typeID: 09, value: "Invite to join {{CompanyName}}", settingGroupID: 04).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.NewUserRegistered.ToUser.Subject", typeID: 10, value: "Invite to join {{CompanyName}}", settingGroupID: 05).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser.Subject", typeID: 11, value: "Invite to join {{CompanyName}}", settingGroupID: 06).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser.Subject", typeID: 12, value: "Invite to join {{CompanyName}}", settingGroupID: 07).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.BackOfficeApprovedUserNotification.ToUser.Subject", typeID: 13, value: "Invite to join {{CompanyName}}", settingGroupID: 08).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.UserPasswordResetTokenRequest.Subject", typeID: 14, value: "Invite to join {{CompanyName}}", settingGroupID: 09).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.UserForgotUsernameRequest.Subject", typeID: 15, value: "Invite to join {{CompanyName}}", settingGroupID: 10).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.NewSellerRegistered.ToSeller.Subject", typeID: 16, value: "Invite to join {{CompanyName}}", settingGroupID: 11).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Authentication.NewSellerRegistered.ToBackOffice.Subject", typeID: 17, value: "Invite to join {{CompanyName}}", settingGroupID: 12).ConfigureAwait(false),
                    // Messaging
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Messaging.NewWaiting.Subject", typeID: 18, value: "Invite to join {{CompanyName}}", settingGroupID: 13).ConfigureAwait(false),
                    // Products
                    await CreateADummySettingAsync(id: ++index, key: "Emails.Products.ProductNowInStock.Subject", typeID: 19, value: "Invite to join {{CompanyName}}", settingGroupID: 14).ConfigureAwait(false),
                    // Sales
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.CustomerReceipt.Subject", typeID: 20, value: "Invite to join {{CompanyName}}", settingGroupID: 15).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.ToBackOffice.Subject", typeID: 21, value: "Invite to join {{CompanyName}}", settingGroupID: 16).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.ToBackOfficeStore.Subject", typeID: 22, value: "Invite to join {{CompanyName}}", settingGroupID: 17).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.Confirmed.Subject", typeID: 23, value: "Invite to join {{CompanyName}}", settingGroupID: 18).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.Backordered.Subject", typeID: 24, value: "Invite to join {{CompanyName}}", settingGroupID: 19).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.Split.Subject", typeID: 25, value: "Invite to join {{CompanyName}}", settingGroupID: 20).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.InvoiceCreated.Subject", typeID: 26, value: "Invite to join {{CompanyName}}", settingGroupID: 21).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.PartialPayment.Subject", typeID: 27, value: "Invite to join {{CompanyName}}", settingGroupID: 22).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.FullPayment.Subject", typeID: 28, value: "Invite to join {{CompanyName}}", settingGroupID: 23).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.Processing.Subject", typeID: 29, value: "Invite to join {{CompanyName}}", settingGroupID: 24).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.DropShipped.Subject", typeID: 30, value: "Invite to join {{CompanyName}}", settingGroupID: 25).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.Shipped.Subject", typeID: 31, value: "Invite to join {{CompanyName}}", settingGroupID: 26).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.ReadyForPickup.Subject", typeID: 32, value: "Invite to join {{CompanyName}}", settingGroupID: 27).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.Completed.Subject", typeID: 33, value: "Invite to join {{CompanyName}}", settingGroupID: 28).ConfigureAwait(false),
                    await CreateADummySettingAsync(id: ++index, key: "Emails.SalesOrders.Voided.Subject", typeID: 34, value: "Invite to join {{CompanyName}}", settingGroupID: 29).ConfigureAwait(false),
                };
                await InitializeMockSetSettingsAsync(mockContext, RawSettings).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Setting Groups
            if (DoAll || DoSystem || DoSettingGroupTable)
            {
                RawSettingGroups = new()
                {
                    await CreateADummySettingGroupAsync(id: 01, key: "General", name: "General", desc: "desc", displayName: "General").ConfigureAwait(false),
                    // Email Defaults
                    await CreateADummySettingGroupAsync(id: 02, key: "Emails.Defaults", name: "Emails.Defaults", desc: "Email Settings Defaults are propagated to emails when they do not have an overriding setting with a matching key component. Note that all emails must have their own Main Body Content value.", displayName: "Emails Settings Defaults").ConfigureAwait(false),
                    // Authentication
                    await CreateADummySettingGroupAsync(id: 03, key: "Emails.Authentication.Invitation", name: "Emails.Authentication.Invitation", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 04, key: "Emails.Authentication.InvitationWithCode", name: "Emails.Authentication.InvitationWithCode", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 05, key: "Emails.Authentication.NewUserRegistered.ToUser", name: "Emails.Authentication.NewUserRegistered.ToUser", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 06, key: "Emails.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser", name: "Emails.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 07, key: "Emails.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser", name: "Emails.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 08, key: "Emails.Authentication.BackOfficeApprovedUserNotification.ToUser", name: "Emails.Authentication.BackOfficeApprovedUserNotification.ToUser", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 09, key: "Emails.Authentication.UserPasswordResetTokenRequest", name: "Emails.Authentication.UserPasswordResetTokenRequest", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 10, key: "Emails.Authentication.UserForgotUsernameRequest", name: "Emails.Authentication.UserForgotUsernameRequest", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 11, key: "Emails.Authentication.NewSellerRegistered.ToSeller", name: "Emails.Authentication.NewSellerRegistered.ToSeller", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 12, key: "Emails.Authentication.NewSellerRegistered.ToBackOffice", name: "Emails.Authentication.NewSellerRegistered.ToBackOffice", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    // Messaging
                    await CreateADummySettingGroupAsync(id: 13, key: "Emails.Messaging.NewWaiting", name: "Emails.Messaging.NewWaiting", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    // Products
                    await CreateADummySettingGroupAsync(id: 14, key: "Emails.Products.ProductNowInStock", name: "Emails.Products.ProductNowInStock", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    // Sales
                    await CreateADummySettingGroupAsync(id: 15, key: "Emails.SalesOrders.CustomerReceipt", name: "Emails.SalesOrders.CustomerReceipt", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 16, key: "Emails.SalesOrders.ToBackOffice", name: "Emails.SalesOrders.ToBackOffice", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 17, key: "Emails.SalesOrders.ToBackOfficeStore", name: "Emails.SalesOrders.ToBackOfficeStore", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 18, key: "Emails.SalesOrders.Confirmed", name: "Emails.SalesOrders.Confirmed", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 19, key: "Emails.SalesOrders.Backordered", name: "Emails.SalesOrders.Backordered", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 20, key: "Emails.SalesOrders.Split", name: "Emails.SalesOrders.Split", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 21, key: "Emails.SalesOrders.InvoiceCreated", name: "Emails.SalesOrders.InvoiceCreated", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 22, key: "Emails.SalesOrders.PartialPayment", name: "Emails.SalesOrders.PartialPayment", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 23, key: "Emails.SalesOrders.FullPayment", name: "Emails.SalesOrders.FullPayment", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 24, key: "Emails.SalesOrders.Processing", name: "Emails.SalesOrders.Processing", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 25, key: "Emails.SalesOrders.DropShipped", name: "Emails.SalesOrders.DropShipped", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 26, key: "Emails.SalesOrders.Shipped", name: "Emails.SalesOrders.Shipped", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 27, key: "Emails.SalesOrders.ReadyForPickup", name: "Emails.SalesOrders.ReadyForPickup", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 28, key: "Emails.SalesOrders.Completed", name: "Emails.SalesOrders.Completed", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                    await CreateADummySettingGroupAsync(id: 29, key: "Emails.SalesOrders.Voided", name: "Emails.SalesOrders.Voided", desc: "desc", displayName: "Invitation to Join Site Notice to User (Invite Token from Settings)").ConfigureAwait(false),
                };
                await InitializeMockSetSettingGroupsAsync(mockContext, RawSettingGroups).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Setting Types
            if (DoAll || DoSystem || DoSettingTypeTable)
            {
                RawSettingTypes = new()
                {
                    // Email Defaults
                    await CreateADummySettingTypeAsync(id: 01, key: "Emails.Defaults.RequiresHttps", name: "Emails.Defaults.RequiresHttps", desc: "desc", displayName: "Email Requires HTTPS on return links").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 02, key: "Emails.Defaults.ReturnPath", name: "Emails.Defaults.ReturnPath", desc: "desc", displayName: "Email primary link Return Path").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 03, key: "Emails.Defaults.From", name: "Emails.Defaults.From", desc: "desc", displayName: "Email 'from' address(es)").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 04, key: "Emails.Defaults.To", name: "Emails.Defaults.To", desc: "desc", displayName: "Email 'to' address(es)").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 05, key: "Emails.Defaults.CC", name: "Emails.Defaults.CC", desc: "desc", displayName: "Email 'cc' address(es)").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 06, key: "Emails.Defaults.BCC", name: "Emails.Defaults.BCC", desc: "desc", displayName: "Email 'bcc' address(es)").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 07, key: "Emails.Defaults.Token", name: "Emails.Defaults.Token", desc: "desc", displayName: "Email Invitation Token").ConfigureAwait(false),
                    // Authentication
                    await CreateADummySettingTypeAsync(id: 08, key: "Emails.Authentication.Invitation.Subject", name: "Emails.Authentication.Invitation.Subject", desc: "desc", displayName: "Email.Authentication.Invitation Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 09, key: "Emails.Authentication.InvitationWithCode.Subject", name: "Emails.Authentication.InvitationWithCode.Subject", desc: "desc", displayName: "Email.Authentication.InvitationWithCode Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 10, key: "Emails.Authentication.NewUserRegistered.ToUser.Subject", name: "Emails.Authentication.NewUserRegistered.ToUser.Subject", desc: "desc", displayName: "Email.Authentication.NewUserRegistered.ToUser Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 11, key: "Emails.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser.Subject", name: "Emails.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser.Subject", desc: "desc", displayName: "Email.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 12, key: "Emails.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser.Subject", name: "Emails.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser.Subject", desc: "desc", displayName: "Email.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 13, key: "Emails.Authentication.BackOfficeApprovedUserNotification.ToUser.Subject", name: "Emails.Authentication.BackOfficeApprovedUserNotification.ToUser.Subject", desc: "desc", displayName: "Email.Authentication.BackOfficeApprovedUserNotification.ToUser Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 14, key: "Emails.Authentication.UserPasswordResetTokenRequest.ToUser.Subject", name: "Emails.Authentication.UserPasswordResetTokenRequest.Subject", desc: "desc", displayName: "Email.Authentication.UserPasswordResetTokenRequest Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 15, key: "Emails.Authentication.UserForgotUsernameRequest.Subject", name: "Emails.Authentication.UserForgotUsernameRequest.Subject", desc: "desc", displayName: "Email.Authentication.Invitation UserForgotUsernameRequest line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 16, key: "Emails.Authentication.NewSellerRegistered.ToSeller.Subject", name: "Emails.Authentication.NewSellerRegistered.ToSeller.Subject", desc: "desc", displayName: "Email.Authentication.NewSellerRegistered.ToSeller Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 17, key: "Emails.Authentication.NewSellerRegistered.ToBackOffice.Subject", name: "Emails.Authentication.NewSellerRegistered.ToBackOffice.Subject", desc: "desc", displayName: "Email.Authentication.NewSellerRegistered.ToBackOffice Subject line").ConfigureAwait(false),
                    // Messaging
                    await CreateADummySettingTypeAsync(id: 18, key: "Emails.Messaging.NewWaiting.Subject", name: "Emails.Messaging.NewWaiting.Subject", desc: "desc", displayName: "Email.Messaging.NewWaiting Subject line").ConfigureAwait(false),
                    // Products
                    await CreateADummySettingTypeAsync(id: 19, key: "Emails.Products.ProductNowInStock.Subject", name: "Emails.Products.ProductNowInStock.Subject", desc: "desc", displayName: "Email.Products.ProductNowInStock Subject line").ConfigureAwait(false),
                    // Sales
                    await CreateADummySettingTypeAsync(id: 20, key: "Emails.SalesOrders.CustomerReceipt.Subject", name: "Emails.SalesOrders.CustomerReceipt.Subject", desc: "desc", displayName: "Email.SalesOrders.CustomerReceipt Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 21, key: "Emails.SalesOrders.ToBackOffice.Subject", name: "Emails.SalesOrders.ToBackOffice.Subject", desc: "desc", displayName: "Email.SalesOrders.ToBackOffice Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 22, key: "Emails.SalesOrders.ToBackOfficeStore.Subject", name: "Emails.SalesOrders.ToBackOfficeStore.Subject", desc: "desc", displayName: "Email.SalesOrders.ToBackOfficeStore Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 23, key: "Emails.SalesOrders.Confirmed.Subject", name: "Emails.SalesOrders.Confirmed.Subject", desc: "desc", displayName: "Email.SalesOrders.Confirmed Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 24, key: "Emails.SalesOrders.Backordered.Subject", name: "Emails.SalesOrders.Backordered.Subject", desc: "desc", displayName: "Email.SalesOrders.Backordered Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 25, key: "Emails.SalesOrders.Split.Subject", name: "Emails.SalesOrders.Split.Subject", desc: "desc", displayName: "Email.SalesOrders.Split Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 26, key: "Emails.SalesOrders.InvoiceCreated.Subject", name: "Emails.SalesOrders.InvoiceCreated.Subject", desc: "desc", displayName: "Email.SalesOrders.InvoiceCreated Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 27, key: "Emails.SalesOrders.PartialPayment.Subject", name: "Emails.SalesOrders.PartialPayment.Subject", desc: "desc", displayName: "Email.SalesOrders.PartialPayment Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 28, key: "Emails.SalesOrders.FullPayment.Subject", name: "Emails.SalesOrders.FullPayment.Subject", desc: "desc", displayName: "Email.SalesOrders.FullPayment Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 29, key: "Emails.SalesOrders.Processing.Subject", name: "Emails.SalesOrders.Processing.Subject", desc: "desc", displayName: "Email.SalesOrders.Processing Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 30, key: "Emails.SalesOrders.DropShipped.Subject", name: "Emails.SalesOrders.DropShipped.Subject", desc: "desc", displayName: "Email.SalesOrders.DropShipped Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 31, key: "Emails.SalesOrders.Shipped.Subject", name: "Emails.SalesOrders.Shipped.Subject", desc: "desc", displayName: "Email.SalesOrders.Shipped Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 32, key: "Emails.SalesOrders.ReadyForPickup.Subject", name: "Emails.SalesOrders.ReadyForPickup.Subject", desc: "desc", displayName: "Email.SalesOrders.ReadyForPickup Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 33, key: "Emails.SalesOrders.Completed.Subject", name: "Emails.SalesOrders.Completed.Subject", desc: "desc", displayName: "Email.SalesOrders.Completed Subject line").ConfigureAwait(false),
                    await CreateADummySettingTypeAsync(id: 34, key: "Emails.SalesOrders.Voided.Subject", name: "Emails.SalesOrders.Voided.Subject", desc: "desc", displayName: "Email.SalesOrders.Voided Subject line").ConfigureAwait(false),
                };
                await InitializeMockSetSettingTypesAsync(mockContext, RawSettingTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Record Versions
            if (DoAll || DoSystem || DoRecordVersionTable)
            {
                RawRecordVersions = new()
                {
                    await CreateADummyRecordVersionAsync(id: 01, key: "Product-V1", name: "Product V1", recordID: 1152, serializedRecord: "{ Name: \"Product Name\" }").ConfigureAwait(false),
                };
                await InitializeMockSetRecordVersionsAsync(mockContext, RawRecordVersions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Record Version Types
            if (DoAll || DoSystem || DoRecordVersionTypeTable)
            {
                RawRecordVersionTypes = new()
                {
                    await CreateADummyRecordVersionTypeAsync(id: 01, key: "Product", name: "Product", displayName: "Product").ConfigureAwait(false),
                };
                await InitializeMockSetRecordVersionTypesAsync(mockContext, RawRecordVersionTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Event Logs
            if (DoAll || DoSystem || DoEventLogTable)
            {
                var index = 0;
                RawEventLogs = new()
                {
                    await CreateADummyEventLogAsync(id: ++index, key: "Test Event Log entry", name: "Test Event Log entry").ConfigureAwait(false),
                };
                await InitializeMockSetEventLogsAsync(mockContext, RawEventLogs).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
