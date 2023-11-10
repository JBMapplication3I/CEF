// <copyright file="_AuthenticationEmailsService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication emails service class</summary>
// ReSharper disable StyleCop.SA1402
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>A send invitation.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInAdmin,
     Route("/Providers/Emails/Authentication/SendInvitation", "POST",
         Summary = "Membership Registration Process step 1: Send Invitation (User gets an email with an invite token)")]
    public class SendInvitation : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email to send the invitation to")]
        public string Email { get; set; } = null!;
    }

    /// <summary>A send receipt notification with email.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin,
     Route("/Providers/Emails/ReceiptNotificationToEmailAddress", "POST", Summary = "Use to update a specific sales order item")]
    public class SendReceiptNotificationWithEmail : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        [ApiMember(Name = nameof(OrderID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Order ID")]
        public int OrderID { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email address to send the notification to")]
        public string Email { get; set; } = null!;
    }

    /// <summary>A send return shipped notification.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Route("/Providers/Emails/ReturnShippedNotification/{ID}", "POST", Summary = "")]
    public class SendReturnShippedNotification : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A queue generic email.</summary>
    /// <seealso cref="EmailQueueModel"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Route("/Providers/Emails/QueueGenericEmail", "POST", Summary = "Use to create a generic email")]
    public class QueueGenericEmail : EmailQueueModel, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the replacements.</summary>
        /// <value>The replacements.</value>
        [ApiMember(Name = nameof(Replacements), DataType = "Dictionary<string, string>", ParameterType = "body", IsRequired = true,
            Description = "Replacements dictionary for email template")]
        public Dictionary<string, object?>? Replacements { get; set; }
    }

    /// <summary>A send calendar events assistance request.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, Route("/Providers/Emails/CalendarEvents/RequestAssistance", "POST", Summary = "Use to send email to events coordinator requesting assistance", Priority = 1)]
    public class SendCalendarEventsAssistanceRequest : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "Body", IsRequired = true)]
        public string Email { get; set; } = null!;

        /// <summary>Gets or sets from date.</summary>
        /// <value>from date.</value>
        [ApiMember(Name = nameof(FromDate), DataType = "Date", ParameterType = "Body", IsRequired = false)]
        public DateTime? FromDate { get; set; }

        /// <summary>Gets or sets to date.</summary>
        /// <value>to date.</value>
        [ApiMember(Name = nameof(ToDate), DataType = "Date", ParameterType = "Body", IsRequired = false)]
        public DateTime? ToDate { get; set; }

        /// <summary>Gets or sets the request values.</summary>
        /// <value>The request values.</value>
        [ApiMember(Name = nameof(RequestValues), DataType = "Dictionary<string, object>", ParameterType = "Body", IsRequired = false)]
        public Dictionary<string, object?>? RequestValues { get; set; }
    }

    /// <summary>An invite user with code.</summary>
    /// <seealso cref="IReturn{Int32}"/>
    [Authenticate,
     Route("/Providers/Emails/Contacts/User/InviteWithCode", "POST", Priority = 1,
         Summary = "Use to get a list of users")]
    public class InviteUserWithCode : IReturn<int>
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Email")]
        public string Email { get; set; } = null!;
    }

    /// <summary>An authentication e mails service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class AuthenticationEmailsService : ClarityEcommerceServiceBase
    {
        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
#pragma warning disable CA1822 // Mark members as static
        public async Task<object> Post(SendInvitation request)
#pragma warning restore CA1822 // Mark members as static
        {
            var result = await new AuthenticationInvitationWithStaticTokenToCustomerEmail().QueueAsync(
                    contextProfileName: null,
                    to: request.Email)
                .ConfigureAwait(false);
            return result.ActionSucceeded ? CEFAR.PassingCEFAR() : result;
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(SendReceiptNotificationWithEmail request)
        {
            var result = await new SalesOrdersForwardReceiptToCustomerEmail().QueueAsync(
                    to: request.Email,
                    contextProfileName: null,
                    parameters: new()
                    {
                        ["salesOrder"] = (await Workflows.SalesOrders.GetAsync(request.OrderID, contextProfileName: null).ConfigureAwait(false))!,
                    })
                .ConfigureAwait(false);
            return result.ActionSucceeded ? CEFAR.PassingCEFAR() : result;
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(SendReturnShippedNotification request)
        {
            var result = await new SalesReturnsShippedNotificationToBackOfficeEmail().QueueAsync(
                    contextProfileName: null,
                    to: null,
                    parameters: new()
                    {
                        ["salesReturn"] = (await Workflows.SalesReturns.GetAsync(request.ID, contextProfileName: null).ConfigureAwait(false))!,
                    })
                .ConfigureAwait(false);
            return result.ActionSucceeded ? CEFAR.PassingCEFAR() : result;
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}</returns>
#pragma warning disable CA1822 // Mark members as static
        public async Task<object> Post(QueueGenericEmail request)
#pragma warning restore CA1822 // Mark members as static
        {
            var result = await new GenericEmail().QueueAsync(
                    contextProfileName: null,
                    to: null,
                    parameters: new() { ["queueModel"] = request, },
                    customReplacements: request.Replacements)
                .ConfigureAwait(false);
            return result.ActionSucceeded ? CEFAR.PassingCEFAR() : result;
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Post(SendCalendarEventsAssistanceRequest request)
        {
            try
            {
                var result = await new CalendarEventsAssistanceRequestNotificationToBackOfficeEmail().QueueAsync(
                        contextProfileName: null,
                        to: request.Email,
                        parameters: new() { ["fromDate"] = request.FromDate, ["toDate"] = request.ToDate, }!,
                        customReplacements: request.RequestValues)
                    .ConfigureAwait(false);
                return result.ActionSucceeded ? CEFAR.PassingCEFAR() : result;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("SendCalendarEventsAssistanceRequestException", ex.Message, ex, null).ConfigureAwait(false);
                return CEFAR.FailingCEFAR("There was an error sending the back-office Calendar Event Assistance Request.");
            }
        }

        /// <summary>Post this message.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}</returns>
        public async Task<object> Post(InviteUserWithCode request)
        {
            Contract.RequiresValidKey(request.Email, "Email is required");
            var user = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var (newUserID, inviteCode) = await Workflows.Users.InviteWithCodeAsync(request.Email, user, null).ConfigureAwait(false);
            try
            {
                await new AuthenticationInvitationWithGeneratedTokenToCustomerEmail().QueueAsync(
                        contextProfileName: null,
                        to: request.Email,
                        parameters: new()
                        {
                            ["fromUserEmail"] = user.Email!,
                            ["token"] = inviteCode,
                        })
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Crash silently
                await Logger.LogErrorAsync("InviteUserWithCode", ex.Message, ex, null).ConfigureAwait(false);
            }
            return newUserID;
        }
    }

    /// <summary>An emails feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class EmailsFeature : IPlugin
    {
        /// <summary>Registers this EmailsFeature.</summary>
        /// <param name="appHost">The application host.</param>
        [PublicAPI]
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
