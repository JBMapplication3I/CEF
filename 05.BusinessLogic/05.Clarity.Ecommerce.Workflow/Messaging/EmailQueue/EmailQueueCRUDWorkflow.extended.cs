// <copyright file="EmailQueueCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue workflow class</summary>
#nullable enable
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Utilities;

    public partial class EmailQueueWorkflow
    {
        /// <summary>Gets or sets the web client factory.</summary>
        /// <value>The web client factory.</value>
        public IWebClientFactory WebClientFactory { protected get; set; } = new SystemWebClientFactory();

        /// <summary>Gets or sets the email queue status identifier of processed.</summary>
        /// <value>The email queue status identifier of processed.</value>
        private static int EmailQueueStatusIDOfProcessed { get; set; }

        /// <inheritdoc/>
        public Task<CEFActionResponse<int>> AddEmailToQueueAsync(IEmailQueueModel model, string? contextProfileName)
        {
            return CreateAsync(model, contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<bool> DequeueEmailAsync(int id, string? contextProfileName)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0 && id != int.MaxValue);
            return await DequeueEmailAsync(
                    await GetAsync(id, contextProfileName).ConfigureAwait(false),
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<bool> DequeueEmailAsync(string key, string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(key));
            return await DequeueEmailAsync(
                    await GetAsync(key, contextProfileName).ConfigureAwait(false),
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<EmailQueue>> FilterQueryByModelCustomAsync(
            IQueryable<EmailQueue> query,
            IEmailQueueSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterEmailQueuesByAddressesToAndCCAndBCC(search.AddressesTo);
        }

        /// <summary>Executes the replacements operation.</summary>
        /// <remarks>This does a multi-pass replacement of the dictionary keys with their values in the
        /// source string.</remarks>
        /// <param name="replacements">The replacements.</param>
        /// <param name="source">      Source for the.</param>
        /// <returns>A string.</returns>
        private static string DoReplacements(Dictionary<string, string?> replacements, string? source)
        {
            var result = source ?? string.Empty;
            var foundThings = true;
            while (foundThings)
            {
                foundThings = false;
                foreach (var kvp in replacements)
                {
                    while (result.Contains(kvp.Key))
                    {
                        result = result.Replace(kvp.Key, kvp.Value ?? string.Empty);
                        foundThings = true;
                    }
                }
            }
            return result;
        }

        /// <summary>Dequeue email.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        private async Task<bool> DequeueEmailAsync(IEmailQueueModel? model, string? contextProfileName)
        {
            if (model == null)
            {
                return true;
            }
            if (model.StatusName == "Sent")
            {
                return true;
            }
            if (Contract.CheckInvalidID(EmailQueueStatusIDOfProcessed))
            {
                EmailQueueStatusIDOfProcessed = await Workflows.EmailStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Sent",
                        byName: "Sent",
                        byDisplayName: "Sent",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            model.StatusID = EmailQueueStatusIDOfProcessed;
            await UpdateAsync(model, contextProfileName).ConfigureAwait(false);
            return true;
        }
    }
}
