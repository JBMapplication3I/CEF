// <copyright file="EmailQueueCRUDWorkflow.extended.SplitTemplateQueuableEmails.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue workflow class</summary>
// ReSharper disable BadExpressionBracesLineBreaks, BadIndent, MissingLinebreak
// ReSharper disable StyleCop.SA1201, StyleCop.SA1202, StyleCop.SA1204
namespace Clarity.Ecommerce.Workflow
{
    public partial class EmailQueueWorkflow
    {
#if SPLIT_TEMPLATES
        #region Authentication
        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailInvitationWithStaticTokenAsync(
            string email,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.Invitation", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{Token}}", emailSettings.Token },
                { "{{Email}}", email },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(email, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailInviteCodeAsync(
            string email,
            IUserModel fromUser,
            string inviteCode,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.InvitationWithCode", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{SiteName}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{Email}}", email },
                { "{{InviteUser}}", fromUser.Email },
                { "{{InviteCode}}", inviteCode },
            };
            return await FormatAndQueueSplitTemplateEmailAsync(email, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewUserRegistrationToUserAsync(
            IUserModel user,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.NewUserRegistered.ToUser", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{FirstName}}", user.Contact?.FirstName },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(user.Email ?? user.Contact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewUserRegistrationEmailValidationToUserAsync(
            IUserModel userModel,
            string validationToken,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.NewUserRegistered.EmailVerificationNoReset.ToUser", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{FirstName}}", userModel.Contact?.FirstName },
                { "{{Email}}", userModel.Email },
                { "{{Token}}", validationToken },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(userModel.Email?, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewUserRegistrationEmailValidationAndForcedPasswordResetToUserAsync(
            ICreateLiteAccountAndSendValidationEmail model,
            string passwordResetToken,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.NewUserRegistered.EmailVerificationAndForcedPasswordReset.ToUser", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var rootURL = emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl;
            rootURL = !string.IsNullOrEmpty(model.RootURL) && !string.Equals(model.RootURL, rootURL, StringComparison.OrdinalIgnoreCase) ? model.RootURL : rootURL;
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", rootURL },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{Email}}", model.Email },
                { "{{Token}}", emailSettings.Token }, // Static Invite token
                { "{{ValidationToken}}", model.Token }, // Identity generated Validation token
                { "{{ResetToken}}", passwordResetToken }, // Identity generated password reset token
                { "{{SellerType}}", model.SellerType },
                { "{{MembershipLevel}}", model.Membership },
                { "{{MembershipType}}", model.MembershipType },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(model.Email, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailBackOfficeApprovedUserNotificationToUserAsync(
            IUserModel userModel,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.BackOfficeApprovedUserNotification.ToUser", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{FirstName}}", userModel.Contact.FirstName },
                { "{{Email}}", userModel.Email },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(userModel.Email, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailUserPasswordResetTokenRequestAsync(
            string email,
            string username,
            string resetToken,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.UserPasswordResetTokenRequest", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{Email}}", email },
                { "{{Username}}", username },
                { "{{ResetToken}}", HttpUtility.UrlEncode(resetToken) },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(email, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailUserForgotUsernameRequestAsync(
            string email,
            string username,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.UserForgotUsernameRequest", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{Email}}", email },
                { "{{Username}}", username },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(email, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSellerRegistrationToSellerAsync(
            IUserModel user,
            IStoreModel store,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.NewSellerRegistered.ToSeller", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{StoreName}}", store?.Name },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(user.Email ?? user.Contact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSellerRegistrationToBackOfficeAsync(
            IUserModel user,
            IStoreModel store,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Authentication.NewSellerRegistered.ToBackOffice", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{StoreName}}", store?.Name },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(user.Email ?? user.Contact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Messaging
        /// <inheritdoc/>
        public async Task<List<CEFActionResponse<int>>> QueueSplitTemplateEmailNewMessageWaitingNotificationToRecipientsAsync(
            IMessageModel message,
            string? contextProfileName)
        {
            Contract.RequiresValidID(message.ID);
            var emailSettings = new SplitTemplateEmailSettings("Emails.Messaging.NewWaiting", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{MessageID}}", message.ID.ToString() },
                { "{{SenderUsername}}", message.SentByUserUserName },
                { "{{Subject}}", message.Subject },
                { "{{Body}}", message.Body },
            };
            var results = new List<CEFActionResponse<int>>();
            foreach (var recipient in message.MessageRecipients)
            {
                var result = await FormatAndQueueSplitTemplateEmailAsync(recipient.Slave.Email ?? recipient.Slave.Contact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
                if (!result.ActionSucceeded)
                {
                    results.Add(CEFAR.FailingCEFAR<int>("Unable to queue email"));
                    continue;
                }
                results.Add(result);
            }
            return results;
        }
        #endregion

        #region Products
        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailProductNowInStockNotificationAsync(
            string email,
            IProductModel product,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.Products.ProductNowInStock", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{Email}}", email },
                { "{{ProductDetailUrlFragment}}", SplitTemplateEmailSettings.ProductDetailUrlFragment },
                { "{{ProductSeoUrl}}", product.SeoUrl },
                { "{{ProductName}}", product.Name },
            };
            var result = await FormatAndQueueSplitTemplateEmailAsync(email, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region Sales
        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderCustomerReceiptNotificationAsync(
            ISalesOrderModel order,
            string cc,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.CustomerReceipt", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSalesOrderNotificationToBackOfficeAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.ToBackOffice", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailNewSalesOrderNotificationToBackOfficeStoreAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            if (!order.StoreID.HasValue)
            {
                // Do nothing
                return CEFAR.FailingCEFAR<int>("Unable to send email: No Store assigned to the order");
            }
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var receiverEmail = context.Stores.FilterByID(order.StoreID).Single().Contact.Email1;
                if (string.IsNullOrWhiteSpace(receiverEmail))
                {
                    // Don't know where to send it
                    return CEFAR.FailingCEFAR<int>("Unable to send email: No primary contact email for the store");
                }
            }
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.ToBackOfficeStore", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderConfirmedNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.Confirmed", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderBackOrderNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.Backordered", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderSplitNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.Split", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderInvoiceNotificationAsync(
            ISalesOrderModel order,
            ISalesInvoiceModel invoice,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.InvoiceCreated", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var secondDictionary = await StandardInvoiceReplacementsAsync(emailSettings, invoice).ConfigureAwait(false);
            secondDictionary.ToList().ForEach(x => replacementDictionary[x.Key] = x.Value);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderPartialPaymentNotificationAsync(
            ISalesOrderModel order,
            IPaymentModel payment,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.PartialPayment", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var secondDictionary = await StandardPaymentReplacementsAsync(emailSettings, payment).ConfigureAwait(false);
            secondDictionary.ToList().ForEach(x => replacementDictionary[x.Key] = x.Value);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderFullPaymentNotificationAsync(
            ISalesOrderModel order,
            IPaymentModel payment,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.FullPayment", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var secondDictionary = await StandardPaymentReplacementsAsync(emailSettings, payment).ConfigureAwait(false);
            secondDictionary.ToList().ForEach(x => replacementDictionary[x.Key] = x.Value);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderProcessingNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.Processing", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderDropShipNotificationAsync(
            ISalesOrderModel order,
            IPurchaseOrderModel purchaseOrder,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.DropShipped", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderShippedNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.Shipped", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderReadyForPickupNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.ReadyForPickup", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderCompletedNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.Completed", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> QueueSplitTemplateEmailSalesOrderVoidedNotificationAsync(
            ISalesOrderModel order,
            string? contextProfileName)
        {
            var emailSettings = new SplitTemplateEmailSettings("Emails.SalesOrders.Voided", Workflows.Settings, Workflows.EmailTemplates, contextProfileName);
            var replacementDictionary = await StandardOrderReplacementsAsync(emailSettings, order).ConfigureAwait(false);
            var result = await FormatAndQueueSplitTemplateEmailAsync(order.BillingContact.Email1, replacementDictionary, emailSettings, contextProfileName).ConfigureAwait(false);
            return result;
        }
        #endregion

        /// <summary>Standard order replacements.</summary>
        /// <param name="emailSettings">The email settings.</param>
        /// <param name="order">        The order.</param>
        /// <returns>A Dictionary{string,string}.</returns>
        // ReSharper disable once CyclomaticComplexity
        private static Task<Dictionary<string, string>> StandardOrderReplacementsAsync(
            SplitTemplateEmailSettings emailSettings,
            ISalesOrderModel order)
        {
            var lineItemsHTML = "<table style='width:100%;'><thead><tr><th>Name</th><td>Quantity</th><th>Price</th><th>Download(s)</th></tr></thead><tbody>";
            foreach (var item in order.SalesItems)
            {
                var downloads = string.Empty;
                if (item.ProductDownloads?.Count > 0)
                {
                    foreach (var download in item.ProductDownloads)
                    {
                        downloads += $"<a href=\"{{RootUrl}}/Images/Product/Document{download}\">Click to Download</a><br/>";
                    }
                }
                lineItemsHTML += "<tr>"
                                  + $"<td><a href=\"{{RootUrl}}/{{ProductDetailUrlFragment}}/{item.ProductSeoUrl}\">{item.ProductName}</a></td>"
                                  + $"<td style='text-align: right;'>{item.TotalQuantity:N4}</td>"
                                  + $"<td style='text-align: right;'>{item.ExtendedPrice:C2}</td>"
                                  + $"<td>{downloads}</td>"
                               + "</tr>";
            }
            lineItemsHTML += "</tbody></table>";
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath ?? string.Empty },
                { "{{CompanyName}}", emailSettings.CompanyName ?? string.Empty },
                { "{{Email}}", order.BillingContact?.Email1 ?? string.Empty },
                { "{{ID}}", order.ID.ToString() },
                { "{{ProductDetailUrlFragment}}", SplitTemplateEmailSettings.ProductDetailUrlFragment ?? string.Empty },
                { "{{PurchaseDetails}}", lineItemsHTML },
                { "{{FirstName}}", order.BillingContact?.FirstName ?? string.Empty },
                { "{{LastName}}", order.BillingContact?.LastName ?? string.Empty },
                { "{{Name}}", order.BillingContact?.FullName ?? string.Empty },
                // { "{{ShippingMethod}}", shipping },
                { "{{PurchaseOrderID}}", order.PurchaseOrderNumber ?? string.Empty },
                { "{{TransactionID}}", order.PaymentTransactionID ?? string.Empty },
                { "{{PaymentTransactionID}}", order.PaymentTransactionID ?? string.Empty },
                { "{{TaxTransactionID}}", order.TaxTransactionID ?? string.Empty },
                { "{{OrderTotal}}", order.Totals?.Total.ToString("C2") ?? string.Empty },
                { "{{Date}}", (order.OriginalDate ?? DateExtensions.GenDateTime).ToString("f") },
                { "{{BillingName}}", order.BillingContact?.FullName ?? string.Empty },
                { "{{BillingFirstName}}", order.BillingContact?.FirstName ?? string.Empty },
                { "{{BillingLastName}}", order.BillingContact?.LastName ?? string.Empty },
                { "{{BillingEmail}}", order.BillingContact?.Email1 ?? string.Empty },
                { "{{BillingPhone}}", order.BillingContact?.Phone1 ?? string.Empty },
                {
                    "{{BillingStreetAddress}}",
                    (order.BillingContact?.Address?.Street1 ?? string.Empty)
                        + " " + (order.BillingContact?.Address?.Street2 ?? string.Empty)
                },
                { "{{BillingCity}}", order.BillingContact?.Address?.City ?? string.Empty },
                { "{{BillingState}}", order.BillingContact?.Address?.RegionCode ?? string.Empty },
                { "{{BillingZipCode}}", order.BillingContact?.Address?.PostalCode ?? string.Empty },
                {
                    "{{BillingAddress}}",
                    (order.BillingContact?.Address?.Street1 ?? string.Empty)
                        + " " + (order.BillingContact?.Address?.Street2 ?? string.Empty)
                        + " " + (order.BillingContact?.Address?.City ?? string.Empty)
                        + ", " + (order.BillingContact?.Address?.RegionName ?? string.Empty)
                        + " " + (order.BillingContact?.Address?.PostalCode ?? string.Empty)
                },
            };
            if (order.ShippingSameAsBilling ?? order.ShippingContact?.Address == null)
            {
                replacementDictionary["{{ShippingFirstName}}"] = order.BillingContact?.FirstName ?? string.Empty;
                replacementDictionary["{{ShippingLastName}}"] = order.BillingContact?.LastName ?? string.Empty;
                replacementDictionary["{{ShippingCity}}"] = order.BillingContact?.Address?.City ?? string.Empty;
                replacementDictionary["{{ShippingState}}"] = order.BillingContact?.Address?.RegionCode ?? string.Empty;
                replacementDictionary["{{ShippingZipCode}}"] = order.BillingContact?.Address?.PostalCode ?? string.Empty;
                replacementDictionary["{{ShippingStreetAddress}}"] = (order.BillingContact?.Address?.Street1 ?? string.Empty)
                                                             + " " + (order.BillingContact?.Address?.Street2 ?? string.Empty);
                replacementDictionary["{{ShippingAddress}}"] = (order.BillingContact?.Address?.Street1 ?? string.Empty)
                                      + " " + (order.BillingContact?.Address?.Street2 ?? string.Empty)
                                      + " " + (order.BillingContact?.Address?.City ?? string.Empty)
                                      + ", " + (order.BillingContact?.Address?.RegionName ?? string.Empty)
                                      + " " + (order.BillingContact?.Address?.PostalCode ?? string.Empty);
            }
            else
            {
                replacementDictionary["{{ShippingFirstName}}"] = order.ShippingContact.FirstName ?? string.Empty;
                replacementDictionary["{{ShippingLastName}}"] = order.ShippingContact.LastName ?? string.Empty;
                replacementDictionary["{{ShippingCity}}"] = order.ShippingContact.Address.City ?? string.Empty;
                replacementDictionary["{{ShippingState}}"] = order.ShippingContact.Address.RegionCode ?? string.Empty;
                replacementDictionary["{{ShippingZipCode}}"] = order.ShippingContact.Address.PostalCode ?? string.Empty;
                replacementDictionary["{{ShippingStreetAddress}}"] = (order.ShippingContact.Address.Street1 ?? string.Empty)
                                                                 + " " + (order.ShippingContact.Address.Street2 ?? string.Empty);
                replacementDictionary["{{ShippingAddress}}"] = (order.ShippingContact.Address.Street1 ?? string.Empty)
                                      + " " + (order.ShippingContact.Address.Street2 ?? string.Empty)
                                      + " " + (order.ShippingContact.Address.City ?? string.Empty)
                                      + ", " + (order.ShippingContact.Address.RegionName ?? string.Empty)
                                      + " " + (order.ShippingContact.Address.PostalCode ?? string.Empty);
            }
            if (order.SalesOrderPayments?.Count > 0)
            {
                replacementDictionary.Add("{{CardType}}", order.SalesOrderPayments[0].Slave.PaymentMethodName);
                replacementDictionary.Add("{{CardLastFour}}", order.SalesOrderPayments[0].Slave.Last4CardDigits);
            }
            else if (order.SalesOrderPayments?.Count > 0)
            {
                replacementDictionary.Add("{{CardLastFour}}", order.SalesOrderPayments[0].Slave.Last4CardDigits);
            }
            else
            {
                replacementDictionary.Add("{{CardLastFour}}", " ? ");
            }
            return Task.FromResult(replacementDictionary);
        }

        /// <summary>Standard invoice replacements.</summary>
        /// <param name="emailSettings">The email settings.</param>
        /// <param name="invoice">      The invoice.</param>
        /// <returns>A Dictionary{string,string}.</returns>
        private static Task<Dictionary<string, string>> StandardInvoiceReplacementsAsync(
            SplitTemplateEmailSettings emailSettings,
            ISalesInvoiceModel invoice)
        {
            var lineItemsHTML = "<table style='width:100%;'><thead><tr><th>Name</th><th>Quantity</td><th>Price</th><th>Download(s)</th></tr></thead><tbody>";
            foreach (var item in invoice.SalesItems)
            {
                var downloads = string.Empty;
                if (item.ProductDownloads?.Count > 0)
                {
                    downloads = item.ProductDownloads.Aggregate(
                        downloads,
                        (c, d) => c + $"<a href=\"{{RootUrl}}/Images/Product/Document{d}\">Click to Download</a><br/>");
                }
                lineItemsHTML += "<tr>"
                                  + $"<td><a href=\"{{RootUrl}}/{{ProductDetailUrlFragment}}/{item.ProductSeoUrl}\">{item.ProductName}</a></td>"
                                  + $"<td style='text-align: right;'>{item.TotalQuantity:N4}</td>"
                                  + $"<td style='text-align: right;'>{item.ExtendedPrice:C2}</td>"
                                  + $"<td>{downloads}</td>"
                               + "</tr>";
            }
            lineItemsHTML += "</tbody></table>";
            var replacementDictionary = new Dictionary<string, string?>
            {
                { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                { "{{ReturnPath}}", emailSettings.ReturnPath },
                { "{{CompanyName}}", emailSettings.CompanyName },
                { "{{Email}}", invoice.BillingContact.Email1 },
                { "{{ProductDetailUrlFragment}}", SplitTemplateEmailSettings.ProductDetailUrlFragment },
                { "{{InvoiceDetails}}", lineItemsHTML },
                { "{{FirstName}}", invoice.BillingContact.FirstName },
                { "{{LastName}}", invoice.BillingContact.LastName },
                { "{{Name}}", invoice.BillingContact.FullName },
                // { "{{ShippingMethod}}", shipping },
                { "{{OrderTotal}}", invoice.Totals.Total.ToString("C2") },
                { "{{Date}}", (invoice.OriginalDate ?? DateExtensions.GenDateTime).ToString("f") },
                { "{{BillingName}}", invoice.BillingContact.FullName },
                { "{{BillingEmail}}", invoice.BillingContact.Email1 },
                { "{{BillingPhone}}", invoice.BillingContact.Phone1 },
                {
                    "{{BillingAddress}}",
                    $"{invoice.BillingContact.Address.Street1}"
                        + $" {invoice.BillingContact.Address.Street2}"
                        + $" {invoice.BillingContact.Address.City}, {invoice.BillingContact.Address.RegionName} {invoice.BillingContact.Address.PostalCode}"
                },
            };
            if (invoice.ShippingSameAsBilling ?? invoice.ShippingContact == null)
            {
                replacementDictionary["{{ShippingAddress}}"] = $"{invoice.BillingContact.Address.Street1}"
                    + $" {invoice.BillingContact.Address.Street2}"
                    + $" {invoice.BillingContact.Address.City}, {invoice.BillingContact.Address.RegionName} {invoice.BillingContact.Address.PostalCode}";
            }
            else
            {
                replacementDictionary["{{ShippingAddress}}"] = $"{invoice.ShippingContact.Address.Street1}"
                    + $" {invoice.ShippingContact.Address.Street2}"
                    + $" {invoice.ShippingContact.Address.City}, {invoice.ShippingContact.Address.RegionName} {invoice.ShippingContact.Address.PostalCode}";
            }
            return Task.FromResult(replacementDictionary);
        }

        /// <summary>Standard payment replacements.</summary>
        /// <param name="emailSettings">The email settings.</param>
        /// <param name="payment">      The payment.</param>
        /// <returns>A Dictionary{string,string}.</returns>
        private static Task<Dictionary<string, string>> StandardPaymentReplacementsAsync(
            SplitTemplateEmailSettings emailSettings,
            IPaymentModel payment)
        {
            return Task.FromResult(
                new Dictionary<string, string?>
                {
                    { "{{RootUrl}}", emailSettings.RequiresHttps ? SplitTemplateEmailSettings.RootUrlSSL : SplitTemplateEmailSettings.RootUrl },
                    { "{{CardType}}", payment.PaymentMethodName },
                    { "{{CardLastFour}}", payment.Last4CardDigits },
                    // ReSharper disable once PossibleInvalidOperationException
                    { "{{Amount}}", payment.Amount.Value.ToString("C") },
                });
        }

        /// <summary>Format and queue split template email.</summary>
        /// <param name="email">                The email.</param>
        /// <param name="replacementDictionary">Dictionary of replacements.</param>
        /// <param name="emailSettings">        The email settings.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The formatted and queue split template email.</returns>
        private async Task<CEFActionResponse<int>> FormatAndQueueSplitTemplateEmailAsync(
            string email,
            Dictionary<string, string?> replacementDictionary,
            SplitTemplateEmailSettings emailSettings,
            string? contextProfileName)
        {
            var subjectWithReplacements = DoReplacements(replacementDictionary, emailSettings.Subject);
            var body = emailSettings.IncludesContentTemplate + emailSettings.HeaderContentTemplate + emailSettings.MainContentTemplate + emailSettings.FooterContentTemplate;
            var bodyWithReplacements = DoReplacements(replacementDictionary, body);
            var toQueue = RegistryLoaderWrapper.GetInstance<IEmailQueueModel>(contextProfileName);
            toQueue.Active = true;
            toQueue.AddressFrom = emailSettings.From;
            toQueue.Subject = subjectWithReplacements;
            toQueue.Body = bodyWithReplacements;
            toQueue.IsHtml = true;
            toQueue.AddressesTo = email;
            toQueue.AddressesCc = string.Empty;
            toQueue.AddressesBcc = string.Empty;
            if (!string.IsNullOrWhiteSpace(emailSettings.To))
            {
                toQueue.AddressesTo += emailSettings.To;
            }
            if (!string.IsNullOrWhiteSpace(emailSettings.CC))
            {
                toQueue.AddressesCc += emailSettings.CC;
            }
            if (!string.IsNullOrWhiteSpace(emailSettings.BCC))
            {
                toQueue.AddressesBcc += emailSettings.BCC;
            }
            toQueue.Attempts = 0;
            toQueue.HasError = false;
            toQueue.StatusKey = "Pending";
            toQueue.StatusName = "Pending";
            toQueue.TypeKey = "General";
            toQueue.TypeName = "General";
            return await AddEmailToQueueAsync(toQueue, contextProfileName).ConfigureAwait(false);
        }
#endif
    }
}
