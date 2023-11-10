// <copyright file="Logger.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the logger class</summary>
// ReSharper disable IntroduceOptionalParameters.Global, MemberCanBePrivate.Global, UnusedMember.Global
#if NET5_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Clarity.Ecommerce.Testing.Net5")]
#endif

namespace Clarity.Ecommerce
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using JSConfigs;
    using Utilities;

    /// <summary>A logger.</summary>
    public class Logger : ILogger
    {
#if WINDOWS
        private const string EventLogName = "Application";
#endif

        private const string EmailTemplate = @"
<table style=""width: 100%;"">
<tbody>
<tr>
<td width=""20%""><b>Application</b></td>
<td width=""80%"">{{Application}}</td>
</tr>
<tr>
<td><b>Node</b></td>
<td>{{Node}}</td>
</tr>
<tr>
<td><b>Site</b></td>
<td>{{Site}}</td>
</tr>
<tr>
<td><b>Timestamp</b></td>
<td>{{Timestamp}}</td>
</tr>
<tr>
<td colspan=""2""><b>Message</b></td>
</tr>
<tr>
<td colspan=""2"">{{Message}}</td>
</tr>
</tbody>
</table>";

        /// <summary>Name of the application.</summary>
        private string applicationName = "Clarity eCommerce Framework";

        /// <summary>true to enable, false to disable the email alerts.</summary>
        private bool emailAlertsOn;

        /// <summary>true to email alerts on error only.</summary>
        private bool emailAlertsOnErrorOnly;

        /// <summary>The email from address.</summary>
        private string? emailFromAddress = string.Empty;

        /// <summary>The email recipient addresses.</summary>
        private string? emailRecipientAddresses = string.Empty;

        /// <summary>The email SMTP host.</summary>
        private string? emailSMTPHost = string.Empty;

        /// <summary>true to email authenticate.</summary>
        private bool emailAuthenticate;

        /// <summary>The email username.</summary>
        private string? emailUsername = string.Empty;

        /// <summary>The email password.</summary>
        private string? emailPassword = string.Empty;

        /// <summary>true to email use SSL or TLS.</summary>
        // ReSharper disable once InconsistentNaming
        private bool emailUseSSLOrTLS;

        /// <summary>The email port.</summary>
        private int emailPort;

        /// <summary>Name of the event log.</summary>
        /// <summary>Values that represent log levels.</summary>
        public enum LogLevels
        {
            /// <summary>An enum constant representing the error option.</summary>
            Error = 10,

            /// <summary>An enum constant representing the warning option.</summary>
            Warning = 20,

            /// <summary>An enum constant representing the information option.</summary>
            Information = 30,

            /// <summary>An enum constant representing the debug option.</summary>
            Debug = 40,
        }

#if NET5_0_OR_GREATER
        // Summary:
        //     Specifies the event type of an event log entry.
        private enum EventLogEntryType
        {
            /// <summary>An error event. This indicates a significant problem the user should know about; usually a
            /// loss of functionality or data.</summary>
            Error = 1,

            /// <summary>
            /// A warning event. This indicates a problem that is not immediately significant, but that may signify
            /// conditions that could cause future problems.
            /// </summary>
            Warning = 2,

            /// <summary>
            /// An information event. This indicates a significant, successful operation.
            /// </summary>
            Information = 4,

            /// <summary>
            /// A success audit event. This indicates a security event that occurs when an audited access attempt is
            /// successful; for example, logging on successfully.
            /// </summary>
            // ReSharper disable once UnusedMember.Local
            SuccessAudit = 8,

            /// <summary>
            /// A failure audit event. This indicates a security event that occurs when an audited access attempt fails;
            /// for example, a failed attempt to open a file.
            /// </summary>
            // ReSharper disable once UnusedMember.Local
            FailureAudit = 16,
        }
#endif

        /// <summary>Sets the extra logger.</summary>
        /// <value>The extra logger.</value>
        // ReSharper disable once StyleCop.SA1623
        internal Action<string?>? ExtraLogger { private get; set; }

#if WINDOWS
        /// <summary>Gets or sets the application log.</summary>
        /// <value>The application log.</value>
        private EventLog AppLog { get; set; }
#endif

        /// <summary>Gets or sets a value indicating whether this Logger has loaded.</summary>
        /// <value>True if this Logger has loaded, false if not.</value>
        private bool HasLoaded { get; set; }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return LogError(name, message, emailAlertsOn, null, null, contextProfileName);
        }

        /// <inheritdoc/>
        public Task<Guid> LogErrorAsync(string name, string? message, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return LogErrorAsync(name, message, emailAlertsOn, null, null, contextProfileName);
        }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, bool forceEmail, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return LogError(name, message, forceEmail, null, null, contextProfileName);
        }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, string data, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return LogError(name, message, emailAlertsOn, null, data, contextProfileName);
        }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, Exception ex, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return LogError(name, message, emailAlertsOn, ex, null, contextProfileName);
        }

        /// <inheritdoc/>
        public Task<Guid> LogErrorAsync(string name, string? message, Exception ex, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return LogErrorAsync(name, message, emailAlertsOn, ex, null, contextProfileName);
        }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, Exception ex, string? data, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return LogError(name, message, emailAlertsOn, ex, data, contextProfileName);
        }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, bool forceEmail, Exception? ex, string? data, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntry(
                name,
                message,
                EventLogEntryType.Error,
                forceEmail,
                contextProfileName,
                ex: ex,
                data: data,
                logLevel: (int)LogLevels.Error);
        }

        /// <inheritdoc/>
        public Task<Guid> LogErrorAsync(string name, string? message, bool forceEmail, Exception? ex, string? data, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntryAsync(
                name,
                message,
                EventLogEntryType.Error,
                forceEmail,
                contextProfileName,
                ex: ex,
                data: data,
                logLevel: (int)LogLevels.Error);
        }

        /// <inheritdoc/>
        public Guid LogInformation(string name, string message, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            var emailAlerts = emailAlertsOn;
            if (emailAlertsOnErrorOnly)
            {
                emailAlerts = false;
            }
            return LogInformation(name, message, emailAlerts, contextProfileName);
        }

        /// <inheritdoc/>
        public Task<Guid> LogInformationAsync(string name, string message, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            var emailAlerts = emailAlertsOn;
            if (emailAlertsOnErrorOnly)
            {
                emailAlerts = false;
            }
            return LogInformationAsync(name, message, emailAlerts, contextProfileName);
        }

        /// <inheritdoc/>
        public Guid LogInformation(string name, string message, bool forceEmail, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntry(
                name,
                message,
                EventLogEntryType.Information,
                forceEmail,
                contextProfileName,
                logLevel: (int)LogLevels.Information);
        }

        /// <inheritdoc/>
        public Task<Guid> LogInformationAsync(string name, string message, bool forceEmail, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntryAsync(
                name,
                message,
                EventLogEntryType.Information,
                forceEmail,
                contextProfileName,
                logLevel: (int)LogLevels.Information);
        }

        /// <inheritdoc/>
        public Guid LogWarning(string name, string message, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            var emailAlerts = emailAlertsOn;
            if (emailAlertsOnErrorOnly)
            {
                emailAlerts = false;
            }
            return LogWarning(name, message, emailAlerts, contextProfileName);
        }

        /// <inheritdoc/>
        public Task<Guid> LogWarningAsync(string name, string message, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            var emailAlerts = emailAlertsOn;
            if (emailAlertsOnErrorOnly)
            {
                emailAlerts = false;
            }
            return LogWarningAsync(name, message, emailAlerts, contextProfileName);
        }

        /// <inheritdoc/>
        public Guid LogWarning(string name, string message, bool forceEmail, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntry(
                name,
                message,
                EventLogEntryType.Warning,
                forceEmail,
                contextProfileName,
                logLevel: (int)LogLevels.Warning);
        }

        /// <inheritdoc/>
        public Task<Guid> LogWarningAsync(string name, string message, bool forceEmail, string? contextProfileName)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntryAsync(
                name: name,
                message: message,
                entryType: EventLogEntryType.Warning,
                forceEmail: forceEmail,
                contextProfileName: contextProfileName,
                logLevel: (int)LogLevels.Warning);
        }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, string data, string? contextProfileName, params object[] args)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntry(
                name,
                string.Format(message, args),
#if WINDOWS
                EventLogEntryType.Error,
#endif
                contextProfileName,
                data: data,
                logLevel: (int)LogLevels.Error);
        }

        /// <inheritdoc/>
        public Guid LogError(string name, string message, Exception ex, string data, string? contextProfileName, params object[] args)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntry(
                name,
                string.Format(message, args),
#if WINDOWS
                EventLogEntryType.Error,
#endif
                contextProfileName,
                ex: ex,
                data: data,
                logLevel: (int)LogLevels.Error);
        }

        /// <inheritdoc/>
        public Guid LogInformation(string name, string message, string? contextProfileName, params object[] args)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntry(
                name,
                string.Format(message, args),
#if WINDOWS
                EventLogEntryType.Information,
#endif
                contextProfileName,
                logLevel: (int)LogLevels.Information);
        }

        /// <inheritdoc/>
        public Guid LogWarning(string name, string message, string? contextProfileName, params object[] args)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntry(
                name,
                string.Format(message, args),
#if WINDOWS
                EventLogEntryType.Warning,
#endif
                contextProfileName,
                logLevel: (int)LogLevels.Warning);
        }

        /// <inheritdoc/>
        public Task<Guid> LogWarningAsync(string name, string message, string? contextProfileName, params object[] args)
        {
            if (!HasLoaded)
            {
                LoadSettings();
            }
            return WriteEntryAsync(
                name: name,
                message: string.Format(message, args),
                entryType: EventLogEntryType.Warning,
                forceEmail: false,
                contextProfileName: contextProfileName,
                logLevel: (int)LogLevels.Warning);
        }

        /// <summary>Writes to database event log.</summary>
        /// <param name="message">           The message.</param>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="dataID">            Identifier for the data.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="logLevel">          The log level.</param>
        /// <returns>A GUID.</returns>
        private static Guid WriteToDbEventLog(
            string message,
            string name,
            string? contextProfileName,
            int? dataID = null,
            int? storeID = null,
            int? logLevel = null)
        {
            var guid = Guid.NewGuid();
            if (contextProfileName != null)
            {
                // Don't act in tests
                return guid;
            }
            try
            {
                using var cnn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["ClarityEcommerceEntities"].ConnectionString);
                cnn.Open();
                var sql = new StringBuilder()
                    .Append("INSERT INTO [")
                    .Append(cnn.Database)
                    .Append("].[System].[SystemLog] ([Active],[CustomKey],[CreatedDate],[Name],[Description],[StoreID],[DataID],[LogLevel]) VALUES (1,'")
                    .Append(guid)
                    .Append("',CURRENT_TIMESTAMP,'")
                    .Append(name?.Replace("'", "''")) // Must escape single quotes
                    .Append("','")
                    .Append(message?.Replace("'", "''")) // Must escape single quotes
                    .Append("',")
                    .Append(storeID?.ToString() ?? "NULL")
                    .Append(',')
                    .Append(dataID?.ToString() ?? "NULL")
                    .Append(',')
                    .Append(logLevel?.ToString() ?? "10")
                    .Append(')');
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                using var command = new SqlCommand(sql.ToString(), cnn);
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(@"There was an error reported by SQL Server");
                Console.WriteLine(ex);
            }
            return guid;
        }

        /// <summary>Writes to database event log.</summary>
        /// <param name="message">           The message.</param>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="dataID">            Identifier for the data.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="logLevel">          The log level.</param>
        /// <returns>A Task{Guid}.</returns>
        private static async Task<Guid> WriteToDbEventLogAsync(
            string? message,
            string name,
            string? contextProfileName,
            int? dataID = null,
            int? storeID = null,
            int? logLevel = null)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var guid = Guid.NewGuid();
            // Must wrap in null check for Tests
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.EventLogs is null)
            {
                return guid;
            }
            var log = RegistryLoaderWrapper.GetInstance<DataModel.EventLog>(contextProfileName);
            log.Active = true;
            log.CreatedDate = DateExtensions.GenDateTime;
            log.CustomKey = $"CEF: {guid}";
            log.Name = name;
            log.Description = message;
            log.DataID = dataID;
            log.StoreID = storeID;
            log.LogLevel = logLevel;
            context.EventLogs!.Add(log);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            return guid;
        }

        /// <summary>Sends an email.</summary>
        /// <param name="recipientAddress">The recipient address.</param>
        /// <param name="message">         The message.</param>
        /// <param name="entryType">       The entry type.</param>
        private void SendEmail(string? recipientAddress, string message, EventLogEntryType entryType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailSMTPHost)
                    || emailAuthenticate
                       && (!emailAuthenticate
                           || string.IsNullOrWhiteSpace(emailUsername)
                           || string.IsNullOrWhiteSpace(emailPassword))
                    || string.IsNullOrWhiteSpace(emailFromAddress)
                    || string.IsNullOrWhiteSpace(emailRecipientAddresses))
                {
                    return;
                }
                var mySmtpClient = new SmtpClient(emailSMTPHost);
                if (emailPort > 0)
                {
                    mySmtpClient.Port = emailPort;
                }
                if (emailAuthenticate)
                {
                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    mySmtpClient.Credentials = new NetworkCredential(emailUsername, emailPassword);
                }
                else
                {
                    mySmtpClient.UseDefaultCredentials = true;
                }
                if (emailUseSSLOrTLS)
                {
                    mySmtpClient.EnableSsl = emailUseSSLOrTLS;
                }
                // add from, to mail addresses
                var from = new MailAddress(emailFromAddress!);
                var finalRecipientAddress = recipientAddress ?? emailRecipientAddresses;
                var to = new MailAddress(finalRecipientAddress!);
                var myMail = new MailMessage(from, to);
                // add ReplyTo
                var replyTo = new MailAddress(emailFromAddress!);
                myMail.ReplyToList.Add(replyTo);
                // set subject and encoding
                var msg = message.Length > 100 ? message[..100] : message;
                msg = msg.Replace("\n", " ").Replace("\r", " ").Replace("  ", " ");
                myMail.Subject = applicationName + " : " + entryType + " " + msg;
                myMail.SubjectEncoding = Encoding.UTF8;
                // set body-message and encoding
                myMail.Body = EmailTemplate
                    .Replace("{{Application}}", applicationName)
                    .Replace("{{Site}}", CEFConfigDictionary.SiteRouteHostUrl)
                    .Replace("{{Node}}", CEFConfigDictionary.SchedulerNodeName)
                    .Replace("{{Timestamp}}", DateExtensions.GenDateTime.ToString("s"))
                    .Replace("{{Message}}", message.Replace("\n", "<br/>"));
                myMail.BodyEncoding = Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;
                // send it
                mySmtpClient.Send(myMail);
            }
            catch (SmtpException)
            {
                // Do Nothing
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>Sends an email.</summary>
        /// <param name="recipientAddress">The recipient address.</param>
        /// <param name="message">         The message.</param>
        /// <param name="entryType">       Type of the entry.</param>
        /// <returns>A Task.</returns>
        private async Task SendEmailAsync(string? recipientAddress, string? message, EventLogEntryType entryType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message)
                    || string.IsNullOrWhiteSpace(emailSMTPHost)
                    || emailAuthenticate
                    && (!emailAuthenticate
                        || string.IsNullOrWhiteSpace(emailUsername)
                        || string.IsNullOrWhiteSpace(emailPassword))
                    || string.IsNullOrWhiteSpace(emailFromAddress)
                    || string.IsNullOrWhiteSpace(emailRecipientAddresses))
                {
                    return;
                }
                var mySmtpClient = new SmtpClient(emailSMTPHost);
                if (emailPort > 0)
                {
                    mySmtpClient.Port = emailPort;
                }
                if (emailAuthenticate)
                {
                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    mySmtpClient.Credentials = new NetworkCredential(emailUsername, emailPassword);
                }
                else
                {
                    mySmtpClient.UseDefaultCredentials = true;
                }
                if (emailUseSSLOrTLS)
                {
                    mySmtpClient.EnableSsl = emailUseSSLOrTLS;
                }
                // add from, to mail addresses
                var from = new MailAddress(emailFromAddress!);
                var finalRecipientAddress = recipientAddress ?? emailRecipientAddresses;
                var to = new MailAddress(finalRecipientAddress!);
                var myMail = new MailMessage(from, to);
                // add ReplyTo
                var replyTo = new MailAddress(emailFromAddress!);
                myMail.ReplyToList.Add(replyTo);
                // set subject and encoding
                var msg = message!.Length > 100 ? message[..100] : message;
                msg = msg.Replace("\n", " ").Replace("\r", " ").Replace("  ", " ");
                myMail.Subject = applicationName + " : " + entryType + " " + msg;
                myMail.SubjectEncoding = Encoding.UTF8;
                // set body-message and encoding
                var body = EmailTemplate.Replace("{{Application}}", applicationName)
                    .Replace("{{Site}}", CEFConfigDictionary.SiteRouteHostUrl)
                    .Replace("{{Node}}", CEFConfigDictionary.SchedulerNodeName)
                    .Replace("{{Timestamp}}", DateExtensions.GenDateTime.ToString("s"))
                    .Replace("{{Message}}", message!.Replace("\n", "<br/>"));
                myMail.Body = body;
                myMail.BodyEncoding = Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;
                // send it
                await mySmtpClient.SendMailAsync(myMail).ConfigureAwait(false);
            }
            catch (SmtpException)
            {
                // Do Nothing
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <summary>Loads the settings.</summary>
        private void LoadSettings()
        {
            if (HasLoaded)
            {
                return;
            }
            CEFConfigDictionary.Load();
            applicationName = CEFConfigDictionary.LoggerApplicationName;
            emailAlertsOn = CEFConfigDictionary.LoggerEmailAlertsEnabled;
            emailAlertsOnErrorOnly = CEFConfigDictionary.LoggerEmailAlertsOnErrorOnly;
            emailFromAddress = CEFConfigDictionary.LoggerEmailFromAddresses;
            emailRecipientAddresses = CEFConfigDictionary.LoggerEmailRecipientAddresses;
            emailSMTPHost = CEFConfigDictionary.LoggerEmailSMTPHost;
            emailPort = CEFConfigDictionary.LoggerEmailSMTPPort;
            emailAuthenticate = CEFConfigDictionary.LoggerEmailAuthenticate;
            emailUsername = CEFConfigDictionary.LoggerEmailUsername;
            emailPassword = CEFConfigDictionary.LoggerEmailPassword;
            emailUseSSLOrTLS = CEFConfigDictionary.LoggerEmailUseSSLOrTLS;
#if WINDOWS
            AppLog = new EventLog(EventLogName) { Source = applicationName };
#endif
            HasLoaded = true;
        }

        /// <summary>Writes an entry.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="data">              The data.</param>
        /// <param name="logLevel">          The log level.</param>
        /// <returns>A GUID.</returns>
        private Guid WriteEntry(
            string name,
            string message,
#if WINDOWS
            EventLogEntryType entryType,
#endif
            string? contextProfileName,
            Exception? ex = null,
            string? data = null,
            int? logLevel = null)
        {
            if (ex != null)
            {
                message = $"{message}\r\nErrors:\r\n {ex.Message}\r\nSource:\r\n{ex.Source}"
                    + $"\r\nInner Exception:\r\n{ex.InnerException?.ToString() ?? "none"}"
                    + $"\r\nStack Trace:\r\n{ex.StackTrace}";
            }
            if (!string.IsNullOrWhiteSpace(data))
            {
                message = $"{message}\r\nData:\r\n{data}";
            }
            try
            {
#if WINDOWS
                AppLog.WriteEntry(message, entryType);
#endif
            }
            catch
            {
                /* Do Nothing */
            }
            var guid = default(Guid);
            try
            {
                guid = WriteToDbEventLog(message, name, contextProfileName, logLevel: logLevel);
            }
            catch
            {
                /* Do Nothing */
            }
            Debug.WriteLine(DateExtensions.GenDateTime.ToString("O") + ": " + message);
            ExtraLogger?.Invoke(message);
            return guid;
        }

        /// <summary>Writes an entry.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="entryType">         The entry type.</param>
        /// <param name="forceEmail">        true to force email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="data">              The data.</param>
        /// <param name="logLevel">          The log level.</param>
        /// <returns>A GUID.</returns>
        private Guid WriteEntry(
            string name,
            string message,
            EventLogEntryType entryType,
            bool forceEmail,
            string? contextProfileName,
            Exception? ex = null,
            string? data = null,
            int? logLevel = null)
        {
            if (ex != null)
            {
                message = $"{message}\r\nErrors:\r\n {ex.Message}\r\nSource:\r\n{ex.Source}"
                    + $"\r\nInner Exception:\r\n{ex.InnerException?.ToString() ?? "none"}"
                    + $"\r\nStack Trace:\r\n{ex.StackTrace}";
            }
            if (!string.IsNullOrWhiteSpace(data))
            {
                message = $"{message}\r\nData:\r\n{data}";
            }
            try
            {
#if WINDOWS
                AppLog.WriteEntry(message, entryType);
#endif
            }
            catch
            {
                /* Do Nothing */
            }
            var guid = default(Guid);
            try
            {
                guid = WriteToDbEventLog(message, name, contextProfileName, logLevel: logLevel);
            }
            catch
            {
                /* Do Nothing */
            }
            Debug.WriteLine($"{DateExtensions.GenDateTime:O}: {message}");
            ExtraLogger?.Invoke(message);
            var sendEmail = emailAlertsOn && forceEmail;
            if (sendEmail && emailAlertsOnErrorOnly && entryType != EventLogEntryType.Error)
            {
                sendEmail = false; // only send emails on errors...
            }
            if (sendEmail)
            {
                SendEmail(message, entryType);
            }
            return guid;
        }

        /// <summary>Writes an entry.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="message">           The message.</param>
        /// <param name="entryType">         Type of the entry.</param>
        /// <param name="forceEmail">        True to force email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ex">                The ex.</param>
        /// <param name="data">              The data.</param>
        /// <param name="logLevel">          The log level.</param>
        /// <returns>A Task{Guid}.</returns>
        private async Task<Guid> WriteEntryAsync(
            string name,
            string? message,
            EventLogEntryType entryType,
            bool forceEmail,
            string? contextProfileName,
            Exception? ex = null,
            string? data = null,
            int? logLevel = null)
        {
            if (ex != null)
            {
                message = $"{message}\r\nErrors:\r\n {ex.Message}\r\nSource:\r\n{ex.Source}"
                    + $"\r\nInner Exception:\r\n{ex.InnerException?.ToString() ?? "none"}"
                    + $"\r\nStack Trace:\r\n{ex.StackTrace}";
            }
            if (!string.IsNullOrWhiteSpace(data))
            {
                message = $"{message}\r\nData:\r\n{data}";
            }
            try
            {
#if WINDOWS
                AppLog.WriteEntry(message, entryType);
#endif
            }
            catch
            {
                // Do Nothing
            }
            var guid = default(Guid);
            try
            {
                guid = await WriteToDbEventLogAsync(message, name, contextProfileName, logLevel: logLevel).ConfigureAwait(false);
            }
            catch
            {
                // Do Nothing
            }
            Debug.WriteLine($"{DateExtensions.GenDateTime:O}: {message}");
            ExtraLogger?.Invoke($"{name}\r\n{message}");
            var sendEmail = emailAlertsOn && forceEmail;
            if (sendEmail && emailAlertsOnErrorOnly && entryType != EventLogEntryType.Error)
            {
                sendEmail = false; // only send emails on errors...
            }
            if (sendEmail)
            {
                await SendEmailAsync(message, entryType).ConfigureAwait(false);
            }
            return guid;
        }

        /// <summary>Sends an email.</summary>
        /// <param name="message">  The message.</param>
        /// <param name="entryType">The entry type.</param>
        private void SendEmail(string message, EventLogEntryType entryType)
        {
            SendEmail(null, message, entryType);
        }

        private Task SendEmailAsync(string? message, EventLogEntryType entryType)
        {
            return SendEmailAsync(null, message, entryType);
        }
    }
}
