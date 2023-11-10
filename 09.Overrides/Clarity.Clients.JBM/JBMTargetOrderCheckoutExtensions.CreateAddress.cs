// <copyright file="JBMTargetOrderCheckoutExtensions.CreateAddress.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout extensions class</summary>
namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Mapper;
    using Clients.JBM;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using Hangfire.Storage.Monitoring;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Authenticators;
    using ServiceStack;
    using Utilities;

    public partial class JBMTargetOrderCheckoutProvider : TargetOrderCheckoutProvider
    {
        private async Task<ISalesGroupModel?> VerifyShippingExistsInFusionOrCreate(ISalesGroupModel salesGroup, int currentUserID, string? contextProfileName)
        {
            int count = 0;
            foreach (var sub in salesGroup.SubSalesOrders!)
            {
                if (!long.TryParse(sub.ShippingContact!.CustomKey!, out _))
                {
                    try
                    {
                        using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                        var billingContact = await context.Contacts
                            .AsNoTracking()
                            .FilterByID(salesGroup.BillingContactID)
                            .Select(x => x.JsonAttributes)
                            .SingleOrDefaultAsync()
                            .ConfigureAwait(false);
                        string? billToSiteUseId = string.Empty;
                        if (billingContact.DeserializeAttributesDictionary().TryGetValue("billToSiteUseId", out var siteUse)
                            && Contract.CheckValidKey(siteUse?.Value))
                        {
                            billToSiteUseId = siteUse!.Value;
                        }
                        else
                        {
                            await Logger.LogErrorAsync(
                                "Failed to retrieve billToSiteUseId",
                                $"Failed to retrieve billToSiteUseId for SalesGroup {salesGroup.ID}.",
                                contextProfileName);
                            return null;
                        }
                        string? partySiteId = null;
                        string? partySiteNumber = null;
                        try
                        {
                            (partySiteId, partySiteNumber) = await BuildFusionAddressAndSendAsync(sub, currentUserID, contextProfileName);
                        }
                        catch (Exception e)
                        {
                            await Logger.LogErrorAsync(
                                name: "Failed to push address to Fusion.",
                                message: $"Failed to push address for contact ID:{sub.ShippingContactID}",
                                ex: e,
                                contextProfileName);
                        }
                        var updatedSubOrder = await UpdateSubWithShipToDataFromFusion(
                                subOrder: sub,
                                billToSiteUseId: billToSiteUseId!,
                                partySiteId: partySiteId,
                                partySiteNumber: partySiteNumber,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        if (Contract.CheckNotNull(updatedSubOrder))
                        {
                            salesGroup.SubSalesOrders[count] = updatedSubOrder!;
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    count++;
                }
            }
            return salesGroup;
        }

        private async Task<ISalesOrderModel?> UpdateSubWithShipToDataFromFusion(
            ISalesOrderModel subOrder,
            string billToSiteUseId,
            string? partySiteId,
            string? partySiteNumber,
            string? contextProfileName)
        {
            if (Contract.CheckAnyInvalidKey(partySiteId, partySiteNumber))
            {
                await Logger.LogErrorAsync(
                    "Failed to receive partySiteId or partySiteNumber from Fusion",
                    $"Failed to retrieve billToSiteUseId for SalesGroup {subOrder.SalesGroupAsSubID}.",
                    contextProfileName);
                throw new ArgumentNullException("No party site id or party site number was provided.");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var contact = await context.Contacts.FilterByID(subOrder.ShippingContactID).SingleOrDefaultAsync().ConfigureAwait(false);
            contact.CustomKey = partySiteId;
            var serAttrObject = contact.SerializableAttributes;
            serAttrObject.TryAdd("addressId", new SerializableAttributeObject
            {
                Key = "addressId",
                Value = partySiteId!,
            });
            serAttrObject.TryAdd("billToSiteUseId", new SerializableAttributeObject
            {
                Key = "billToSiteUseId",
                Value = billToSiteUseId,
            });
            contact.TypeID = 1;
            contact.JsonAttributes = serAttrObject.SerializeAttributesDictionary();
            contact.Address!.CustomKey = partySiteNumber;
            context.Contacts.Add(contact);
            context.Entry(contact).State = EntityState.Modified;
            try
            {
                await context.SaveUnitOfWorkAsync();
            }
            catch (Exception e)
            {
                await Logger.LogErrorAsync(
                    name: "Failed to save contact to the database.",
                    message: string.Empty,
                    ex: e,
                    contextProfileName);
            }
            // Ensure account contact exists for any new contact created in checkout.
            var existingAccountContactID = await context.AccountContacts
                .Where(x => x.SlaveID == subOrder.ShippingContactID)
                .Select(y => y.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(existingAccountContactID))
            {
                var newAccountContact = new AccountContact
                {
                    CustomKey = $"{subOrder.AccountID}|{partySiteId}",
                    MasterID = subOrder.AccountID!.Value,
                    SlaveID = subOrder.ShippingContactID!.Value,
                    Active = true,
                    CreatedDate = DateExtensions.GenDateTime,
                    IsBilling = false,
                    IsPrimary = false,
                    TransmittedToERP = true,
                    JsonAttributes = "{}",
                };
                context.AccountContacts.Add(newAccountContact);
                try
                {
                    await context.SaveUnitOfWorkAsync();
                }
                catch (Exception e)
                {
                    await Logger.LogErrorAsync(
                        name: "Failed to save contact/account contact to the database.",
                        message: string.Empty,
                        ex: e,
                        contextProfileName);
                }
            }
            subOrder.ShippingContact!.CustomKey = contact.CustomKey;
            return subOrder;
        }

        private async Task<(string? partySiteId, string? partySiteNumber)> BuildFusionAddressAndSendAsync(
            ISalesOrderModel subOrder,
            int currentUserID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            long? locationId = await MapToCreateLocationAndPushToFusion(subOrder.ShippingContact).ConfigureAwait(false);
            (string? partySiteId, string? partySiteNumber) = await MapToMergeOrganizationAndPushToFusion(
                    (long)locationId,
                    subOrder.AccountKey!,
                    subOrder.ShippingContact!.Address!.Company)
                .ConfigureAwait(false);
            var rawAccountAttributes = await context.Accounts
                .AsNoTracking()
                .FilterByID(subOrder.AccountID)
                .Select(x => x.JsonAttributes)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var customerAccountId = string.Empty;
            if (rawAccountAttributes.DeserializeAttributesDictionary().TryGetValue("customerAccountId", out var custAcctId)
                && Contract.CheckValidKey(custAcctId?.Value))
            {
                customerAccountId = custAcctId!.Value;
            }
            else
            {
                await Logger.LogErrorAsync(
                    name: "Could not get the customer account id for the current account.",
                    message: $"Could not get the customer account id for account ID: {subOrder.AccountID}",
                    forceEmail: false,
                    ex: null,
                    data: rawAccountAttributes ?? string.Empty,
                    contextProfileName);
                throw new ArgumentNullException("Could not get the customer account id for the current account.");
            }
            var names = (await context.Users.FilterByID(currentUserID).Select(x => new { x.Contact!.FirstName, x.Contact.LastName, }).ToListAsync().ConfigureAwait(false)).FirstOrDefault();
            await MapToMergeCustomerAccountAndPushToFusion(
                    subOrder.AccountKey!,
                    names?.FirstName ?? subOrder.ShippingContact.FirstName!,
                    names?.LastName ?? subOrder.ShippingContact.LastName!,
                    customerAccountId,
                    subOrder.ShippingContact.SerializableAttributes.TryGetValue("contactNumber", out var contact)
                        ? contact!.Value
                        : null,
                    partySiteId!)
                .ConfigureAwait(false);
            return (partySiteId, partySiteNumber);
        }

        private async Task<string?> MapToMergeCustomerAccountAndPushToFusion(
            string accountKey,
            string firstName,
            string lastName,
            string? customerAccountId,
            string? contactNumber,
            string partySiteId)
        {
            if (Contract.CheckAnyInvalidKey(customerAccountId))
            {
                throw new ArgumentNullException("No CustomerAccountId was found.");
            }
            var mergeCustNamespaces = new XmlSerializerNamespaces();
            mergeCustNamespaces.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            mergeCustNamespaces.Add("typ", "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/");
            mergeCustNamespaces.Add("par", "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/");
            mergeCustNamespaces.Add("sour", "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/sourceSystemRef/");
            mergeCustNamespaces.Add("cus", "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/");
            mergeCustNamespaces.Add("cus1", "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountContactRole/");
            mergeCustNamespaces.Add("cus2", "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountContact/");
            mergeCustNamespaces.Add("cus3", "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountRel/");
            mergeCustNamespaces.Add("cus4", "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountSiteUse/");
            mergeCustNamespaces.Add("cus5", "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountSite/");
            mergeCustNamespaces.Add("cus6", "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccount/");
            var doc = await PushToFusion<MergeCustomerAccountEnvelope>(
                    MapToMergeCustomerAccountEnvelope(
                        accountKey: accountKey,
                        firstName: firstName,
                        lastName: lastName,
                        customerAccountId: customerAccountId!,
                        contactNumber: contactNumber,
                        partySiteId: partySiteId),
                    $"{JBMConfig.JBMFusionBaseURL}{MergeCustomerAccountEnvelope.URI}",
                    mergeCustNamespaces)
                .ConfigureAwait(false);
            return doc!.Descendants().Where(x => x.Name.LocalName == "CustomerAccountSiteId").Select(y => y.Value).FirstOrDefault();
        }

        private MergeCustomerAccountEnvelope MapToMergeCustomerAccountEnvelope(
            string accountKey,
            string firstName,
            string lastName,
            string customerAccountId,
            string? contactNumber,
            string partySiteId)
        {
            return new MergeCustomerAccountEnvelope
            {
                Body = new MergeCustomerAccountBody
                {
                    MergeCustomerAccount = new MergeCustomerAccountMergeCustomerAccount
                    {
                        CustomerAccount = new MergeCustomerAccountCustomerAccount
                        {
                            PartyId = long.Parse(accountKey),
                            CustomerAccountId = long.Parse(customerAccountId),
                            CustomerAccountSite = new MergeCustomerAccountCustomerAccountSite
                            {
                                PartySiteId = long.Parse(partySiteId),
                                CreatedByModule = "HZ_WS",
                                SetId = JBMConfig.SetId,
                                CustAcctSiteInformation = new CustAcctSiteInformation
                                {
                                    contactNumber = contactNumber,
                                    contactFn = firstName,
                                    contactLn = lastName,
                                },
                                CustomerAccountSiteUse = new CustomerAccountSiteUse
                                {
                                    SiteUseCode = "SHIP_TO",
                                    CreatedByModule = "HZ_WS",
                                },
                            },
                        },
                    },
                },
            };
        }

        private async Task<(string? partySiteId, string? partySiteNumber)> MapToMergeOrganizationAndPushToFusion(long locationId, string accountKey, string? company)
        {
            var mergeOrgNamespaces = new XmlSerializerNamespaces();
            mergeOrgNamespaces.Add("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            mergeOrgNamespaces.Add("ns1", "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/");
            mergeOrgNamespaces.Add("ns2", "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/");
            mergeOrgNamespaces.Add("ns3", "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/");
            var doc = await PushToFusion<MergeOrganizationEnvelope>(
                    MapToMergeOrganizationEnvelope(
                        locationId,
                        accountKey,
                        company),
                    $"{JBMConfig.JBMFusionBaseURL}{MergeOrganizationEnvelope.URI}",
                    mergeOrgNamespaces)
                .ConfigureAwait(false);
            return (doc!.Descendants().Where(x => x.Name.LocalName == "PartySiteId").Select(y => y.Value).FirstOrDefault(), doc!.Descendants().Where(x => x.Name.LocalName == "PartySiteNumber").Select(y => y.Value).FirstOrDefault());
        }

        private MergeOrganizationEnvelope MapToMergeOrganizationEnvelope(long locationId, string accountKey, string? company)
        {
            return new MergeOrganizationEnvelope
            {
                Body = new MergeOrganizationBody
                {
                    MergeOrganization = new MergeOrganization
                    {
                        OrganizationParty = new OrganizationParty
                        {
                            PartyId = long.Parse(accountKey),
                            PartySite = new PartySite
                            {
                                CreatedByModule = "HZ_WS",
                                LocationId = locationId,
                                PartySiteName = company,
                                PartySiteUse = new PartySiteUse
                                {
                                    CreatedByModule = "HZ_WS",
                                    SiteUseType = "SHIP_TO",
                                },
                            },
                        },
                    },
                },
            };
        }

        private async Task<long?> MapToCreateLocationAndPushToFusion(IContactModel? shippingContact)
        {
            var doc = await PushToFusion<CreateLocationEnvelope>(
                    MapToCreateLocationEnvelope(shippingContact),
                    $"{JBMConfig.JBMFusionBaseURL}{CreateLocationEnvelope.URI}")
                .ConfigureAwait(false);
            return long.Parse(doc!.Descendants().Where(x => x.Name.LocalName == "LocationId").Select(y => y.Value).FirstOrDefault());
        }

        private CreateLocationEnvelope MapToCreateLocationEnvelope(IContactModel? shippingContact)
        {
            return new CreateLocationEnvelope
            {
                Header = null,
                Body = new CreateLocationBody
                {
                    CreateLocation = new CreateLocationCreateLocation
                    {
                        Location = new CreateLocationLocation
                        {
                            Address1 = shippingContact!.Address!.Street1,
                            Address2 = shippingContact.Address.Street2,
                            City = shippingContact.Address.City,
                            Country = shippingContact.Address.Country!.Code == "USA"
                                ? "US"
                                : shippingContact.Address.Country.Code,
                            PostalCode = shippingContact.Address.PostalCode,
                            State = shippingContact.Address.RegionCode,
                        },
                    },
                },
            };
        }

        private async Task<XDocument?> PushToFusion<T>(T requestModel, string uriString, XmlSerializerNamespaces? namespaces = null)
            where T : class
        {
            var ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using var writer = new Utf8StringWriter();
            if (Contract.CheckNotNull(namespaces) && namespaces!.Count > 0)
            {
                ser.Serialize(writer, requestModel, namespaces);
            }
            else
            {
                ser.Serialize(writer, requestModel);
            }
            var envelope = writer.ToString();
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Post, uriString);
            request.Headers.Add(
                "Authorization",
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{JBMConfig.JBMFusionUsername}:{JBMConfig.JBMFusionPassword}"))}");
            envelope = envelope.Replace("_x003A_", ":");
            using var content = new StringContent(envelope, Encoding.UTF8, "text/xml");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return XDocument.Load(await response.Content.ReadAsStreamAsync().ConfigureAwait(false));
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        // Use UTF8 encoding but write no BOM to the wire
        public override Encoding Encoding
        {
            get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
        }
    }
}
