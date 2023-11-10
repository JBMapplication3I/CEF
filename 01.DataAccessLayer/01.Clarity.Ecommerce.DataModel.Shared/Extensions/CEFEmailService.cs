// <copyright file="CEFEmailService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF email service class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System.Configuration;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    /// <summary>A CEF email service.</summary>
    internal class CEFEmailService : IIdentityMessageService
    {
        /// <summary>Sends an email.</summary>
        /// <param name="message">The message.</param>
        /// <returns>A Task.</returns>
        public virtual async Task SendAsync(IdentityMessage message)
        {
            var text = message.Body;
            var html = message.Body;
            // do whatever you want to the message
            var msg = new MailMessage();
            msg.From ??= new(ConfigurationManager.AppSettings["Clarity.Emails.Defaults.From"]
                ?? "noreply@clarityecommerce.com");
            msg.To.Add(new MailAddress(message.Destination));
            ////msg.Bcc.Add(new MailAddress("brandon.murphy@claritymis.com"));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            using var smtpClient = new SmtpClient();
            await smtpClient.SendMailAsync(msg).ConfigureAwait(false);
        }
    }
}
