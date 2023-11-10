// <copyright file="SurveyMonkeyProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the survey monkey provider class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using LinqKit;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>A survey monkey provider.</summary>
    /// <seealso cref="SurveyProviderBase"/>
    public class SurveyMonkeyProvider : SurveyProviderBase
    {
        private const string URLCreateSurvey = "https://api.surveymonkey.net/v3/surveys";
        private const string URLGetSurveyPages = "https://api.surveymonkey.net/v3/surveys/{surveyID}/pages";
        private const string URLUpdateSurveyPage = "https://api.surveymonkey.net/v3/surveys/{surveyID}/pages/{pageID}";
        private const string URLCreateSurveyCollector = "https://api.surveymonkey.net/v3/surveys/{surveyID}/collectors";
        private const string URLCreateCollectorMsg = "https://api.surveymonkey.net/v3/collectors/{collectorID}/messages";
        private const string URLAddRecipientsToMsg = "https://api.surveymonkey.net/v3/collectors/{collectorID}/messages/{messageID}/recipients/bulk";
        private const string URLSendMsg = "https://api.surveymonkey.net/v3/collectors/{collectorID}/messages/{messageID}/send";

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => SurveyMonkeyProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool HasDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public override async Task CreateEventSurveyAsync(ICalendarEventModel evt, string contextProfileName)
        {
            // Create survey
            var surveyID = await SendCreateSurveyQueryAsync(evt, contextProfileName).ConfigureAwait(false);
            // Get survey Pages
            var ids = await GetSurveyPagesAsync(surveyID, contextProfileName).ConfigureAwait(false);
            var page1 = ids[1];
            // Update page 1 with new description
            var predicate = PredicateBuilder.New<IUserEventAttendanceModel>();
            predicate = predicate.Or(f => f.TypeKey == "ORGANIZER");
            predicate = predicate.Or(f => f.TypeKey == "SPIRITUAL");
            var organizers = evt.UserEventAttendances
                .Where(predicate)
                .Select(ue => $"{ue.Slave.Contact?.MiddleName}  {ue.Slave.ContactFirstName} {ue.Slave.ContactLastName}")
                .Aggregate((current, next) => $"{current}, {next}");
            var descriptionTemplatePath = SurveyMonkeyProviderConfig.TemplateRoot
                + SurveyMonkeyProviderConfig.DescriptionTemplatePath;
            string description;
            using (var webClient = new WebClient())
            {
                description = await webClient.DownloadStringTaskAsync(
                        SurveyMonkeyProviderConfig.SiteRootUrl + descriptionTemplatePath)
                    .ConfigureAwait(false);
                description = description.Replace("{Organizers}", organizers);
            }
            await UpdatePageAsync(surveyID, page1, description, contextProfileName).ConfigureAwait(false);
            // Create survey collector
            var collectorID = await CreateSurveyCollectorAsync(surveyID, contextProfileName).ConfigureAwait(false);
            // Create collector message
            var collectorMessageID = await CreateCollectorMessageAsync(evt, collectorID, contextProfileName).ConfigureAwait(false);
            // Add recipients
            await AddRecipientsToMessageAsync(evt, collectorID, collectorMessageID, contextProfileName).ConfigureAwait(false);
            // send message
            await SendMessageAsync(collectorMessageID, collectorID, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Sends a web request.</summary>
        /// <param name="webRequest">        The web request.</param>
        /// <param name="json">              The JSON.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{string}.</returns>
        private async Task<string> SendWebRequestAsync(WebRequest webRequest, string json, string contextProfileName)
        {
            if (!string.IsNullOrEmpty(json))
            {
                using var streamWriter = new StreamWriter(await webRequest.GetRequestStreamAsync().ConfigureAwait(false));
                await streamWriter.WriteAsync(json).ConfigureAwait(false);
            }
            // get response and read into string
            try
            {
                using var webResponse = (HttpWebResponse)await webRequest.GetResponseAsync().ConfigureAwait(false);
                using var responseStream = new StreamReader(webResponse.GetResponseStream() ?? throw new InvalidOperationException());
                return await responseStream.ReadToEndAsync().ConfigureAwait(false);
            }
            catch (WebException ex)
            {
                using (var stream = new StreamReader(ex.Response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    var err = await stream.ReadToEndAsync().ConfigureAwait(false);
                    await Logger.LogErrorAsync("Survey Monkey", err, contextProfileName).ConfigureAwait(false);
                }
                throw;
            }
        }

        private Task SendMessageAsync(string collectorMessageID, string collectorID, string contextProfileName)
        {
            var url = URLSendMsg.Replace("{messageID}", collectorMessageID);
            url = url.Replace("{collectorID}", collectorID);
            var request = CreateWebRequest("{}", url);
            return SendWebRequestAsync(request, "{}", contextProfileName);
        }

        private Task AddRecipientsToMessageAsync(
            ICalendarEventModel evt, string collectorID, string collectorMessageID, string contextProfileName)
        {
            var body = new AddRecipientsBulkBody();
            var userList = evt.UserEventAttendances.Select(ev => ev.Slave).ToArray();
            body.Contacts = new MonkeyContact[userList.Length];
            for (var x = 0; x != userList.Length; ++x)
            {
                body.Contacts[x] = new MonkeyContact
                {
                    Email = userList[x].ContactEmail,
                    FirstName = userList[x].ContactFirstName,
                    LastName = userList[x].ContactLastName,
                };
            }
            var json = JsonConvert.SerializeObject(body);
            return SendWebRequestAsync(
                CreateWebRequest(
                    json,
                    URLAddRecipientsToMsg.Replace("{collectorID}", collectorID).Replace("{messageID}", collectorMessageID)),
                json,
                contextProfileName);
        }

        private async Task<string> CreateCollectorMessageAsync(INameableBaseModel evt, string collectorID, string contextProfileName)
        {
            var templatePath = SurveyMonkeyProviderConfig.TemplateRoot
                + SurveyMonkeyProviderConfig.BodyTemplatePath;
            string emailBody;
            using (var webClient = new WebClient())
            {
                emailBody = await webClient.DownloadStringTaskAsync(
                        SurveyMonkeyProviderConfig.SiteRootUrl + templatePath)
                    .ConfigureAwait(false);
            }
            emailBody = emailBody.Replace("{Title}", evt.Name);
            var json = JsonConvert.SerializeObject(new CreateCollectorMessageBody
            {
                Type = "invite",
                Subject = $"Survey re: {evt.Name}",
                BodyHTML = emailBody,
                IsBrandingEnabled = true,
            });
            return JObject.Parse(
                await SendWebRequestAsync(
                    CreateWebRequest(json, URLCreateCollectorMsg.Replace("{collectorID}", collectorID)),
                    json,
                    contextProfileName)
                    .ConfigureAwait(false))
                .GetValue("id")
                ?.ToString();
        }

        private async Task<string> CreateSurveyCollectorAsync(string surveyID, string contextProfileName)
        {
            const string Json = "{\"type\":\"email\"}";
            return JObject.Parse(
                await SendWebRequestAsync(
                        CreateWebRequest(
                            Json,
                            URLCreateSurveyCollector.Replace("{surveyID}", surveyID)),
                        Json,
                        contextProfileName)
                    .ConfigureAwait(false))
                .GetValue("id")
                ?.ToString();
        }

        private Task UpdatePageAsync(string surveyID, string pageID, string newDescription, string contextProfileName)
        {
            var json = $"{{\"description\": \"{newDescription} \"}}";
            return SendWebRequestAsync(
                CreateWebRequest(
                    json,
                    URLUpdateSurveyPage.Replace("{surveyID}", surveyID).Replace("{pageID}", pageID),
                    "PATCH"),
                json,
                contextProfileName);
        }

        private async Task<Dictionary<int, string>> GetSurveyPagesAsync(string surveyID, string contextProfileName)
        {
            var url = URLGetSurveyPages.Replace("{surveyID}", surveyID);
            var request = CreateGetWebRequest(url);
            var ret = await SendWebRequestAsync(request, string.Empty, contextProfileName).ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<GetSurveyPagesResponse>(ret);
            return response?.Data?.ToDictionary(d => d.Position, d => d.ID);
        }

        private async Task<string> SendCreateSurveyQueryAsync(INameableBaseModel evt, string contextProfileName)
        {
            var json = $"{{\"title\": \"{{Title}}\",\"from_survey_id\":\"{SurveyMonkeyProviderConfig.FromSurveyID}\"}}";
            json = json.Replace("{Title}", evt.Name);
            var request = CreateWebRequest(json, URLCreateSurvey);
            var result = await SendWebRequestAsync(request, json, contextProfileName).ConfigureAwait(false);
            return result == null ? null : JObject.Parse(result).GetValue("id")?.ToString();
        }

        private HttpWebRequest CreateGetWebRequest(string url)
        {
            const string Type = "application/json"; // REST XML
            // begin HttpWebRequest
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.ContentType = Type;
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Authorization", $"bearer {SurveyMonkeyProviderConfig.SurveyToken}");
            return webRequest;
        }

        private HttpWebRequest CreateWebRequest(string json, string url, string method = "POST")
        {
            const string Type = "application/json"; // REST XML
            // begin HttpWebRequest
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = method;
            webRequest.ContentType = Type;
            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Authorization", $"bearer {SurveyMonkeyProviderConfig.SurveyToken}");
            webRequest.ContentLength = json.Length;
            return webRequest;
        }
    }
}
