// <copyright file="InterpaymentsSurchargeProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Interpayments surcharge provider class.</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Surcharges.InterPayments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.Providers.Surcharges;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>Calculate surcharges via
    /// <a href="https://api.interpayments.com/assets/transactionFeeApi.html">InterPayments</a>.</summary>
    /// <seealso cref="SurchargeProviderBase"/>
    internal class InterpaymentsSurchargeProvider : SurchargeProviderBase
    {
        /// <inheritdoc />
        public override bool HasValidConfiguration => InterPaymentsSurchargeProviderConfig.IsValid(false);

        /// <inheritdoc />
        public override async Task<(SurchargeDescriptor descriptor, decimal amount)> CalculateSurchargeAsync(SurchargeDescriptor descriptor, string? contextProfileName)
        {
            if (descriptor.TotalAmount is not { } totalAmount)
            {
                throw new ArgumentException("Total amount was not provided when calculating surcharge.");
            }
            Contract.Requires<ArgumentException>(totalAmount > 0, $"Total amount ({descriptor.TotalAmount}) may not be <= 0.");
            _ = Contract.RequiresNotNull(descriptor.BillingContact, "Missing BillingContact when calculating surcharge.");
            _ = Contract.RequiresValidKey(descriptor.BillingContact!.Address?.PostalCode, "Missing address and/or postal code for address. Unable to calculate surcharge.");
            _ = Contract.RequiresValidKey(descriptor.BIN);
            descriptor = await ResolveKeysOrThrowAsync(descriptor, contextProfileName).ConfigureAwait(false);
            var cacheClient = Contract.RequiresNotNull(await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false));
            // TTL on the cache entry will always be <365 days
            if (await cacheClient.GetAsync<CachedSurcharge>(SurchargeCacheKeyFor(descriptor)).ConfigureAwait(false) is { } cachedSurcharge
                && cachedSurcharge.BIN == descriptor.BIN
                && cachedSurcharge.CachedAt.DayOfYear == DateTime.UtcNow.DayOfYear
                && cachedSurcharge.TotalAmount == descriptor.TotalAmount
                && cachedSurcharge.PostalCode == descriptor.BillingContact!.Address!.PostalCode
                && cachedSurcharge.CountryCode == descriptor.BillingContact.Address.CountryCode
                && cachedSurcharge.ApplicableInvoiceIDs.SetEquals(descriptor.ApplicableInvoices!.Select(i => i.ID)))
            {
                return (descriptor, cachedSurcharge.Surcharge);
            }
            using var client = NewClient();
            var reqBody = JsonConvert.SerializeObject(
                new GetSurchargeRequest
                {
                    BIN = descriptor.BIN!,
                    Amount = totalAmount,
                    PostalCode = descriptor.BillingContact!.Address!.PostalCode!,
                    Processor = null,
                    MerchantTransactionID = descriptor.Key!,
                },
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, });
            var res = await client.PostAsync(
                    client.BaseAddress + "/ch",
                    new StringContent(reqBody, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
            GetSurchargeRequest.Response? resDTO = null;
            try
            {
                var resStr = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                res.EnsureSuccessStatusCode();
                resDTO = JsonConvert.DeserializeObject<GetSurchargeRequest.Response>(
                    await res.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("Calculation failed.", ex.Message + "\n" + ex.StackTrace, contextProfileName);
            }
            // Per InterPayments docs, surcharging should not be a critical path. Errors should be considered a $0 surcharge.
            resDTO ??= new GetSurchargeRequest.Response
            {
                SalesTransactionID = "ERR_NO_RESPONSE",
                Status = "didn't get a response",
                TransactionFee = 0,
            };
            if (string.IsNullOrWhiteSpace(resDTO.SalesTransactionID))
            {
                // No way to mark it complete, so we can't surcharge.
                resDTO.SalesTransactionID ??= "ERR_NO_STXID";
                resDTO.TransactionFee = 0;
                resDTO.Status = "didn't get a sTxId";
            }
            await cacheClient.AddAsync(KeyCacheKeyFor(descriptor), resDTO.SalesTransactionID).ConfigureAwait(false);
            await cacheClient.AddAsync(
                    SurchargeCacheKeyFor(descriptor),
                    new CachedSurcharge
                    {
                        BIN = descriptor.BIN!,
                        TotalAmount = descriptor.TotalAmount!.Value,
                        PostalCode = descriptor.BillingContact.Address.PostalCode!,
                        CountryCode = descriptor.BillingContact.Address.CountryCode!,
                        ApplicableInvoiceIDs = descriptor.ApplicableInvoices!.Select(i => i.ID).ToHashSet(),
                        CachedAt = DateTime.UtcNow,
                        Key = descriptor.Key!,
                        Surcharge = resDTO.TransactionFee,
                    },
                    timeToLive: TimeSpan.FromHours(5))
                .ConfigureAwait(false);
            await Logger.LogInformationAsync(
                    "Calculated Surcharge",
                    $"Surcharge for {descriptor.Key}/{descriptor.ProviderKey} is {resDTO.TransactionFee}.",
                    contextProfileName)
                .ConfigureAwait(false);
            return (descriptor, resDTO.TransactionFee);
        }

        /// <inheritdoc />
        public override async Task<SurchargeDescriptor> CancelAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName)
        {
            descriptor = await ResolveKeysOrThrowAsync(descriptor, contextProfileName).ConfigureAwait(false);
            if (descriptor.ProviderKey is null)
            {
                return descriptor; // Nothing to cancel, no real error to consider.
            }
            using var client = NewClient();
            var res = await client.PostAsync(
                    client.BaseAddress + "/ch/cancel",
                    new StringContent(
                        JsonConvert.SerializeObject(
                            new CancelSurchargeRequest { SalesTransactionID = descriptor.ProviderKey, },
                            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, }),
                        Encoding.UTF8,
                        "application/json"))
                .ConfigureAwait(false);
            return descriptor;
        }

        /// <inheritdoc />
        public override async Task<SurchargeDescriptor> MarkCompleteAsync(
            SurchargeDescriptor descriptor,
            bool mayThrow,
            string? contextProfileName)
        {
            descriptor = await ResolveKeysOrThrowAsync(descriptor, contextProfileName);
            if (descriptor.ProviderKey is null)
            {
                await Logger.LogErrorAsync(
                        "Missing Provider Key",
                        $"Did not receive a ProviderKey and it was not resolvable from the given details. Cannot possibly complete the sale. Key:`{descriptor.Key}`.",
                        contextProfileName)
                    .ConfigureAwait(false);
                return descriptor;
            }
            // Per InterPayments docs, surcharging should not be a critical path. I manually set the key to ERR_*
            // if I was unable to get a surcharge, so we can skip those ones here.
            try
            {
                if (!descriptor.ProviderKey.StartsWith("ERR_"))
                {
                    using var client = NewClient();
                    var res = await client.PostAsync(
                            client.BaseAddress + "/ch/sale",
                            new StringContent(
                                JsonConvert.SerializeObject(
                                    new CompleteSurchargeRequest { SalesTransactionID = descriptor.ProviderKey, },
                                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, }),
                                Encoding.UTF8,
                                "application/json"))
                        .ConfigureAwait(false);
                    res.EnsureSuccessStatusCode();
                    await Logger.LogInformationAsync(
                            "Complete Surcharge",
                            $"Surcharge for {descriptor.Key}/{descriptor.ProviderKey} is marked completed.",
                            contextProfileName)
                        .ConfigureAwait(false);
                }
            }
            catch when (mayThrow)
            {
                throw;
            }
            catch (Exception ex) when (descriptor.ApplicableInvoices?.Any() == true)
            {
                // If we've surcharged the customer, we need to make sure we mark it as complete in InterPayments.
                // If something went wrong, we'll enqueue it in Hangfire to make sure it gets done.
                await Logger.LogErrorAsync(
                        "Failed to mark surcharge complete. Scheduling in background.",
                        descriptor.ProviderKey + ": " + ex.Message + "\n" + ex.StackTrace,
                        contextProfileName)
                    .ConfigureAwait(false);
                var key = $"MissingCompletion:{descriptor.ProviderKey}";
                var value = JsonConvert.SerializeObject(
                    descriptor,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects, });
                foreach (var inv in descriptor.ApplicableInvoices)
                {
                    // Since it's conceivable that a single invoice could have multiple retrying completions, I use a
                    // unique key each time
                    inv.SerializableAttributes[key] = new()
                    {
                        Key = key,
                        Value = value,
                    };
                    await Workflows.SalesInvoices.UpdateAsync(inv, contextProfileName).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                // Just nothing we can do, as we have nothing to attach to.
                await Logger.LogErrorAsync(
                        "Failed to mark surcharge complete. UNABLE TO SCHEDULE.",
                        descriptor.ProviderKey + ": " + ex.Message + "\n" + ex.StackTrace,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            return descriptor;
        }

        /// <inheritdoc />
        public override async Task<SurchargeDescriptor> TryResolveProviderKeyAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName)
        {
            if (descriptor.ProviderKey is not null)
            {
                return descriptor;
            }
            var cache = Contract.RequiresNotNull(await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false));
            if (await cache.GetAsync<string>(KeyCacheKeyFor(descriptor)).ConfigureAwait(false) is { } providerKey)
            {
                descriptor.ProviderKey = providerKey;
            }
            return descriptor;
        }

        /// <summary>Get an HTTP client with appropriate BaseURL and auth token.</summary>
        /// <returns>A HttpClient.</returns>
        private static HttpClient NewClient() => new()
        {
            BaseAddress = new Uri(InterPaymentsSurchargeProviderConfig.BaseURL),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", InterPaymentsSurchargeProviderConfig.APIKey),
            },
        };

        /// <summary>Generate a Redis cache key for a descriptor. Requires the Key to be set.</summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>A string.</returns>
        private string KeyCacheKeyFor(SurchargeDescriptor descriptor) => $"InterPaymentsProvider:{descriptor.Key}:ProviderKey";

        /// <summary>Generate a Redis cache key for a descriptor to store a <see cref="CachedSurcharge"/>.</summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>A string.</returns>
        private string SurchargeCacheKeyFor(SurchargeDescriptor descriptor) => $"InterPaymentsProvider:{descriptor.Key}:CachedSurcharge";

#pragma warning disable CS8618
        /// <summary>Request DTO for calculating the surcharge. Sending the same mTxId will always result in the same
        /// sTxId.</summary>
        internal class GetSurchargeRequest
        {
            /// <summary>Bank identification number.</summary>
            /// <value>The bin.</value>
            [JsonProperty("nicn")]
            internal string BIN { get; set; }

            /// <summary>Technically either postal code or state. We'll always use postal code.</summary>
            /// <value>The postal code.</value>
            [JsonProperty("region")]
            internal string PostalCode { get; set; }

            /// <summary>Amount being paid on the whole transaction.</summary>
            /// <value>The amount.</value>
            [JsonProperty("amount")]
            internal decimal Amount { get; set; }

            /// <summary>Processor, for specifying if we don't have proper data or handling situations where we may need to
            /// use a different payment processor.</summary>
            /// <value>The processor.</value>
            [JsonProperty("processor")]
            internal string? Processor { get; set; }

            /// <summary>A unique ID generated by us.</summary>
            /// <value>The identifier of the merchant transaction.</value>
            [JsonProperty("mTxId")]
            internal string MerchantTransactionID { get; set; }

            /// <summary>Response to the above.</summary>
            internal class Response
            {
                /// <summary>The fee we should charge the customer. If any error occurs, we should consider this to be $0.</summary>
                /// <value>The transaction fee.</value>
                [JsonProperty("transactionFee")]
                internal decimal TransactionFee { get; set; }

                /// <summary>A human-readable status string.</summary>
                /// <value>The status.</value>
                [JsonProperty("response")]
                internal string Status { get; set; }

                /// <summary>The InterPayments-generated ID, to use later to mark it as complete.</summary>
                /// <value>The identifier of the sales transaction.</value>
                [JsonProperty("sTxId")]
                internal string SalesTransactionID { get; set; }
            }
        }

        /// <summary>Cancel an existing surcharge. Doesn't always need to be called - Interpayments will auto-cancel non-
        /// completed surcharges after 90 days.</summary>
        internal class CancelSurchargeRequest
        {
            /// <summary>The <see cref="GetSurchargeRequest.Response.SalesTransactionID"/>.</summary>
            /// <value>The identifier of the sales transaction.</value>
            [JsonProperty("sTxId")]
            internal string SalesTransactionID { get; set; }
        }

        /// <summary>Mark that a surcharge's payment has been completed.</summary>
        internal class CompleteSurchargeRequest
        {
            /// <summary>The <see cref="GetSurchargeRequest.Response.SalesTransactionID"/>.</summary>
            /// <value>The identifier of the sales transaction.</value>
            [JsonProperty("sTxId")]
            internal string SalesTransactionID { get; set; }
        }

        /// <summary>A redis-stored cached version of a surcharge. Only safe to use the same day it was made.</summary>
        internal class CachedSurcharge
        {
            /// <summary>The BIN that was used to calculate this surcharge.</summary>
            /// <value>The bin.</value>
            public string BIN { get; set; }

            /// <summary>The mTxId of this surcharge.</summary>
            /// <value>The key.</value>
            public string Key { get; set; }

            /// <summary>The total amount that was sent to InterPayments.</summary>
            /// <value>The total number of amount.</value>
            public decimal TotalAmount { get; set; }

            /// <summary>The postal code that was sent to InterPayments.</summary>
            /// <value>The postal code.</value>
            public string PostalCode { get; set; }

            /// <summary>The country code that was sent to InterPayments.</summary>
            /// <value>The country code.</value>
            public string CountryCode { get; set; }

            /// <summary>All Invoice IDs that were used to generate this invoice. Shouldn't need to be checked, but is
            /// checked for sanity.</summary>
            /// <value>The applicable invoice i ds.</value>
            public HashSet<int> ApplicableInvoiceIDs { get; set; }

            /// <summary>The surcharge Interpayments gave us.</summary>
            /// <value>The surcharge.</value>
            public decimal Surcharge { get; set; }

            /// <summary>When we got the response from InterPayments. Never re-use one that's more than a day (going by the
            /// clock/12AM) old.</summary>
            /// <value>The cached at.</value>
            public DateTime CachedAt { get; set; }
        }
#pragma warning restore CS8618
    }
}
