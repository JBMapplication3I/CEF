// <copyright file="CEFSMSService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cefsms service class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System.Configuration;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;
    using Utilities;

    /// <summary>The CEF SMS service.</summary>
    internal class CEFSMSService : IIdentityMessageService
    {
        /// <summary>(Immutable) The account SID.</summary>
        private static readonly string? AccountSid
            = ConfigurationManager.AppSettings["Clarity.SMS.Twilio.AccountSID"];

        /// <summary>(Immutable) The authentication token.</summary>
        private static readonly string? AuthToken
            = ConfigurationManager.AppSettings["Clarity.SMS.Twilio.AuthToken"];

        /// <summary>(Immutable) from number.</summary>
        private static readonly string? FromNumber
            = ConfigurationManager.AppSettings["Clarity.SMS.Twilio.FromNumber"];

        /// <summary>True if this CEFSMSService has been initialized.</summary>
        private static bool hasBeenInitialized;

        /// <summary>Gets the recent results.</summary>
        /// <value>The recent results.</value>
        internal static RollingList<MessageResource> RecentResults { get; } = new(50);

        /// <summary>Sends an asynchronous.</summary>
        /// <param name="message">The message.</param>
        /// <returns>A Task.</returns>
        public virtual async Task SendAsync(IdentityMessage message)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            if (!hasBeenInitialized)
            {
                if (!Contract.CheckAllValid(AccountSid, AuthToken, FromNumber))
                {
                    return;
                }
                TwilioClient.Init(AccountSid, AuthToken);
                hasBeenInitialized = true;
            }
            var message2 = await MessageResource.CreateAsync(
                    body: message.Body,
                    @from: new(FromNumber),
                    to: new(message.Destination ?? "+15128254669"))
                .ConfigureAwait(false);
            RecentResults.Add(message2);
        }
    }
}
