// <copyright file="AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication user account completed registration to back office email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Enums;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;
    using Utilities;
    using IContainer = QuestPDF.Infrastructure.IContainer;

    /// <summary>An authentication user account completed registration to back office email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail : EmailSettingsBase
    {
        private string? registrationType;
        private string? brandName;

        /// <inheritdoc/>
        [AppSettingsKey(".Enabled"),
         DefaultValue(true)]
        public override bool Enabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, GetType()) || asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("AccountCreationBackOffice.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Account Registration with {{CompanyName}}")]
        public override string? Subject
        {
            get
            {
                var value = CEFConfigDictionary.TryGet(out string asValue, GetType())
                    ? asValue
                    : "Account Registration with {{CompanyName}}";
                if (!string.IsNullOrEmpty(registrationType))
                {
                    value = $"({registrationType}) {value}";
                }
                if (!string.IsNullOrEmpty(brandName))
                {
                    value = $"[{brandName}] {value}";
                }
                return value;
            }
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".From"),
         DefaultValue("noreply@claritymis.com")]
        public override string? From
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".To"),
         DefaultValue("clarity-local@claritymis.com")]
        public override string? To
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        protected override string SettingsRoot => "Clarity.Authentication.NewUserRegistered.BackOffice";

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("username") == true && parameters["username"] != null,
                "This email requires a parameter in the dictionary of { [\"username\"] = <string> }");
            var username = parameters!["username"] as string;
            // Generate
            if (parameters.ContainsKey("brandName"))
            {
                brandName = (string?)parameters["brandName"];
            }
            if (parameters.ContainsKey("registrationType"))
            {
                registrationType = (string?)parameters["registrationType"];
            }
            string? registrationPdfPath = null;
            FileEntityType? registrationPdfFileType = null;
            if (parameters.ContainsKey("account") && parameters["account"] is not null)
            {
                var account = (IAccountModel)parameters["account"]!;
                List<AccountContactModel> addressBook = new();
                if (parameters.ContainsKey("addressBook"))
                {
                    addressBook = (List<AccountContactModel>)parameters["addressBook"]!;
                }
                ContactModel? primaryContact = null;
                if (parameters.ContainsKey("primaryContact"))
                {
                    primaryContact = (ContactModel)parameters["primaryContact"]!;
                }
                var generatePdfCefar =
                    await GenerateAndSaveRegistrationInfoPdfForEmail(
                            account,
                            primaryContact,
                            addressBook,
                            contextProfileName)
                        .ConfigureAwait(false);
                if (generatePdfCefar.ActionSucceeded)
                {
                    registrationPdfPath = generatePdfCefar.Result.path;
                    registrationPdfFileType = generatePdfCefar.Result.fileType;
                }
            }
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{Username}}"] = username;
            replacementDictionary["{{RootUrl}}"] = CEFConfigDictionary.EmailTemplateRouteHostUrl;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            var emailWorkflow = RegistryLoaderWrapper.GetInstance<IEmailQueueWorkflow>(contextProfileName);
            return await emailWorkflow.FormatAndQueueEmailAsync(
                    email: CEFConfigDictionary.EmailDefaultsBackOfficeEmailAddress,
                    replacementDictionary: replacementDictionary,
                    emailSettings: this,
                    attachmentPath: new ReadOnlyCollection<string?>(new List<string?>() { registrationPdfPath }),
                    attachmentType: registrationPdfFileType,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        private async Task<CEFActionResponse<(string path, FileEntityType fileType)>> GenerateAndSaveRegistrationInfoPdfForEmail(
            IAccountModel accountModel,
            ContactModel? primaryContact,
            List<AccountContactModel> addressBook,
            string? contextProfileName)
        {
            // Empty fail to ret if any actions do not succeed
            var fail = CEFAR.FailingCEFAR<(string path, FileEntityType fileType)>();
            // Load HTML Template from Emails Dir
            var buildPdfCEFAR = await BuildPdf(accountModel, primaryContact, addressBook, contextProfileName).ConfigureAwait(false);
            if (!buildPdfCEFAR.ActionSucceeded)
            {
                fail.Messages.AddRange(buildPdfCEFAR.Messages);
                return fail;
            }
            var pdfBytes = buildPdfCEFAR.Result!;
            // Write PDF to disk
            var writePdfToDiskCEFAR = await WritePdfToDiskAndGetPath(pdfBytes, accountModel.ID, contextProfileName).ConfigureAwait(false);
            if (!writePdfToDiskCEFAR.ActionSucceeded)
            {
                fail.Messages.AddRange(writePdfToDiskCEFAR.Messages);
                return fail;
            }
            var filePath = writePdfToDiskCEFAR.Result!;
            return (filePath, FileEntityType.StoredFileAccount).WrapInPassingCEFAR();
        }

        private async Task<CEFActionResponse<byte[]>> BuildPdf(
            IAccountModel account,
            ContactModel? primaryContact,
            List<AccountContactModel> addressBook,
            string? contextProfileName)
        {
            try
            {
                // Build content dictionary
                var sectionHeaderVal = "SECTION_HEADER";
                var contentDictCEFAR = await BuildTableDictionary(
                        account,
                        primaryContact,
                        addressBook,
                        sectionHeaderVal,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (!contentDictCEFAR.ActionSucceeded || contentDictCEFAR.Result is null)
                {
                    var fail = CEFAR.FailingCEFAR<byte[]>();
                    fail.Messages.AddRange(contentDictCEFAR.Messages);
                    return fail;
                }
                var contentDict = contentDictCEFAR.Result;
                // Build PDF
                var pdf = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        // Margin
                        page.Margin(50);
                        // Page Header
                        page.Header()
                            .Text("Account Registration Information")
                            .Bold()
                            .FontSize(14);
                        // Page Content
                        page.Content().Column(col =>
                        {
                            col.Item().Element(con =>
                            {
                                con.Table(table =>
                                {
                                    table.ColumnsDefinition(cols =>
                                    {
                                        cols.RelativeColumn();
                                        cols.RelativeColumn();
                                    });
                                    var headerTextStyle = TextStyle.Default.Black();
                                    var labelTextStyle = TextStyle.Default.SemiBold();
                                    table.Header(head =>
                                    {
                                        head.Cell().Text(string.Empty);
                                        head.Cell().Text(string.Empty);
                                    });
                                    foreach (var kvp in contentDict)
                                    {
                                        static IContainer RegularCellStyle(IContainer con) =>
                                            con.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                                        static IContainer HeaderCellStyle(IContainer con) =>
                                            con.BorderBottom(1).BorderColor(Colors.Black).PaddingBottom(10).PaddingTop(20);
                                        if (kvp.Value == sectionHeaderVal)
                                        {
                                            table.Cell().Element(HeaderCellStyle).Text(kvp.Key).Style(headerTextStyle);
                                            table.Cell().Element(HeaderCellStyle).Text(string.Empty);
                                        }
                                        else
                                        {
                                            table.Cell().Element(RegularCellStyle).Text(kvp.Key).Style(labelTextStyle);
                                            table.Cell().Element(RegularCellStyle).Text(kvp.Value);
                                        }
                                    }
                                });
                            });
                        });
                        // Page Footer
                        page.Footer().AlignCenter().Text(t =>
                        {
                            t.CurrentPageNumber();
                            t.Span(" / ");
                            t.TotalPages();
                        });
                    });
                })
                .GeneratePdf();
                return pdf.WrapInPassingCEFAR()!;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail)}.{nameof(BuildPdf)}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<byte[]>(ex.Message);
            }
        }

        private async Task<CEFActionResponse<Dictionary<string, string>>> BuildTableDictionary(
            IAccountModel account,
            ContactModel? primaryContact,
            List<AccountContactModel> addressBook,
            string sectionHeaderValue,
            string? contextProfileName)
        {
            try
            {
                // Default value for values that are null or empty
                var nullStr = "N/A";
                // Get serialized attributes dictionary from account model
                var attribs = account.SerializableAttributes;
                // Initialize dictionary with values that do not require any database queries
                var contentDict = new Dictionary<string, string>()
                {
                    // Account Registration Information
                    ["Account/User Identifiers"] = sectionHeaderValue,
                    ["Account ID"] = account.ID.ToString(),
                    ["User ID"] = account.Users!.First()!.ID.ToString(),
                    // Site and Registration Type
                    ["Site and Registration Type"] = sectionHeaderValue,
                    ["Site"] = brandName ?? nullStr,
                    ["Registration Type"] = registrationType ?? nullStr,
                    // Basic Info
                    ["Basic Information"] = sectionHeaderValue,
                    ["Company"] = attribs.TryGetValue("basicInfoCompany", out var bicObj) ? bicObj.Value : nullStr,
                    ["First Name"] = primaryContact?.FirstName ?? nullStr,
                    ["Last Name"] = primaryContact?.LastName ?? nullStr,
                    ["Email"] = primaryContact?.Email1 ?? nullStr,
                    ["Phone"] = primaryContact?.Phone1 ?? nullStr,
                    ["Business Type"] = account.BusinessType ?? nullStr,
                    ["D&B Number"] = account.DunsNumber ?? nullStr,
                    ["Sales Tax Exempt (Y/N)"] = account.IsTaxable ? "N" : "Y",
                    ["Tax Exempt No."] = account.TaxExemptionNo ?? nullStr,
                    ["EIN No."] = account.EIN ?? nullStr,
                    ["DEA No."] = account.DEANumber ?? nullStr,
                    ["Medical License No."] = account.MedicalLicenseNumber ?? nullStr,
                    /* [should] be set after initialization */
                    ["Medical License State"] = nullStr,
                    ["Medical License Holder"] = account.MedicalLicenseHolderName ?? nullStr,
                    ["[Medical] Business Type"] = attribs.TryGetValue("businessMedicalType", out var bmtObj) ? bmtObj.Value : nullStr,
                    // Accounts Payable Info
                    ["Accounts Payable Contact Info"] = sectionHeaderValue,
                    ["A.P. Contact Name"] = attribs.TryGetValue("accountPayableContactName", out var apcnObj) ? apcnObj.Value : nullStr,
                    ["A.P. Contact Phone"] = attribs.TryGetValue("accountPayableContactPhone", out var apcpObj) ? apcpObj.Value : nullStr,
                    ["A.P. Contact Email"] = attribs.TryGetValue("accountPayableContactEmail", out var apceObj) ? apceObj.Value : nullStr,
                    ["Preferred Invoice Delivery Method"] = attribs.ContainsKey("accountPayableContactEmailForInvoices") ? "Email" : "Mail",
                    ["A.P. Contact Email (for Invoices)"] = attribs.TryGetValue("accountPayableContactEmailForInvoices", out var apcefiObj) ? apcefiObj.Value : nullStr,
                    // Payment and Banking Information
                    ["Payment and Banking Info"] = sectionHeaderValue,
                    ["Officer / Partner / Owner Name"] = attribs.TryGetValue("paymentBankingName", out var pbnObj) ? pbnObj.Value : nullStr,
                    ["Officer / Partner / Owner Title"] = attribs.TryGetValue("paymentBankingTitle", out var pbtObj) ? pbtObj.Value : nullStr,
                    ["Additional Officer / Partner / Owner Name"] = attribs.TryGetValue("paymentBankingOptionalName", out var pbonObj) ? pbonObj.Value : nullStr,
                    ["Additional Officer / Partner / Owner Title"] = attribs.TryGetValue("paymentBankingOptionalTitle", out var pbotObj) ? pbotObj.Value : nullStr,
                    ["Person Responsible for Payment"] = attribs.TryGetValue("paymentBankingResponsiblePerson", out var pbrpObj) ? pbrpObj.Value : nullStr,
                    ["Bank Name"] = attribs.TryGetValue("paymentBankingBankName", out var pbbnObj) ? pbbnObj.Value : nullStr,
                    ["Type of [Bank] Account"] = attribs.TryGetValue("paymentBankingAccountType", out var pbatObj) ? pbatObj.Value : nullStr,
                    ["Bank Contact First Name"] = attribs.TryGetValue("paymentBankingBankContactFirstName", out var pbcfnObj) ? pbcfnObj.Value : nullStr,
                    ["Bank Contact Last Name"] = attribs.TryGetValue("paymentBankingBankContactLastName", out var pbclnObj) ? pbclnObj.Value : nullStr,
                    ["Bank Contact Phone"] = attribs.TryGetValue("paymentBankingPhone", out var pbcpObj) ? pbcpObj.Value : nullStr,
                    ["Bank Contact Fax"] = attribs.TryGetValue("paymentBankingFax", out var pbcfObj) ? pbcfObj.Value : nullStr,
                    // Business References
                    ["Business References"] = sectionHeaderValue,
                    /* BRC 1 */
                    ["Business Reference Name 1"] = attribs.TryGetValue("businessReferencesName1", out var brn1Obj) ? brn1Obj.Value : nullStr,
                    ["Business Reference Contact 1"] = attribs.TryGetValue("businessReferencesContact1", out var brc1Obj) ? brc1Obj.Value : nullStr,
                    ["Business Reference Email 1"] = attribs.TryGetValue("businessReferencesEmail1", out var bre1Obj) ? bre1Obj.Value : nullStr,
                    ["Business Reference Phone 1"] = attribs.TryGetValue("businessReferencesPhone1", out var brp1Obj) ? brp1Obj.Value : nullStr,
                    ["Business Reference Fax 1"] = attribs.TryGetValue("businessReferencesFax1", out var brf1Obj) ? brf1Obj.Value : nullStr,
                    /* BRC 2 */
                    ["Business Reference Name 2"] = attribs.TryGetValue("businessReferencesName2", out var brn2Obj) ? brn2Obj.Value : nullStr,
                    ["Business Reference Contact 2"] = attribs.TryGetValue("businessReferencesContact2", out var brc2Obj) ? brc2Obj.Value : nullStr,
                    ["Business Reference Email 2"] = attribs.TryGetValue("businessReferencesEmail2", out var bre2Obj) ? bre2Obj.Value : nullStr,
                    ["Business Reference Phone 2"] = attribs.TryGetValue("businessReferencesPhone2", out var brp2Obj) ? brp2Obj.Value : nullStr,
                    ["Business Reference Fax 2"] = attribs.TryGetValue("businessReferencesFax2", out var brf2Obj) ? brf2Obj.Value : nullStr,
                    /* BRC 3 */
                    ["Business Reference Name 3"] = attribs.TryGetValue("businessReferencesName3", out var brn3Obj) ? brn3Obj.Value : nullStr,
                    ["Business Reference Contact 3"] = attribs.TryGetValue("businessReferencesContact3", out var brc3Obj) ? brc3Obj.Value : nullStr,
                    ["Business Reference Email 3"] = attribs.TryGetValue("businessReferencesEmail3", out var bre3Obj) ? bre3Obj.Value : nullStr,
                    ["Business Reference Phone 3"] = attribs.TryGetValue("businessReferencesPhone3", out var brp3Obj) ? brp3Obj.Value : nullStr,
                    ["Business Reference Fax 3"] = attribs.TryGetValue("businessReferencesFax3", out var brf3Obj) ? brf3Obj.Value : nullStr,
                    // Agreements
                    ["Agreements"] = sectionHeaderValue,
                    ["Signer confirms they have read the Terms and Conditions"] = attribs.TryGetValue("termsCheck", out var taccObj) ? taccObj.Value.ToUpper() : nullStr,
                    ["eSignature"] = attribs.TryGetValue("eSignature", out var essObj) ? essObj.Value : nullStr,
                    ["Signed Date"] = attribs.TryGetValue("signedDate", out var essdObj) ? essdObj.Value : nullStr,
                    ["Signer understands this is a legal signature"] = attribs.TryGetValue("signatureCheck", out var escObj) ? escObj.Value.ToUpper() : nullStr,
                };
                // Get any values that may require database queries
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                // Get medical license state name from region identifier
                if (int.TryParse(account.MedicalLicenseState, out var medLicenseRegionID))
                {
                    var regionCEFAR = await Workflows.Regions.ResolveWithAutoGenerateOptionalAsync(
                            byID: medLicenseRegionID,
                            byKey: null,
                            model: null,
                            byName: null,
                            context: context)
                        .ConfigureAwait(false);
                    if (regionCEFAR.ActionSucceeded && !string.IsNullOrWhiteSpace(regionCEFAR.Result?.Name))
                    {
                        contentDict["Medical License State"] = regionCEFAR.Result!.Name ?? nullStr;
                    }
                }
                // Contacts
                if (addressBook.Any() != true)
                {
                    return contentDict.WrapInPassingCEFAR()!;
                }
                var billingAccountContact = addressBook.FirstOrDefault(x => x.IsBilling);
                var billing = billingAccountContact?.Slave;
                if (billing is not null)
                {
                    contentDict["Billing Contact"] = sectionHeaderValue;
                    contentDict["Billing First Name"] = billing.FirstName ?? nullStr;
                    contentDict["Billing Last Name"] = billing.LastName ?? nullStr;
                    contentDict["Billing Email 1"] = billing.Email1 ?? nullStr;
                    contentDict["Billing Phone"] = billing.Phone1 ?? nullStr;
                    contentDict["Billing Fax"] = billing.Fax1 ?? nullStr;
                    var address = billing.Address;
                    if (address is not null)
                    {
                        contentDict["Billing Company"] = address.Company ?? nullStr;
                        contentDict["Billing Address Street 1"] = address.Street1 ?? nullStr;
                        contentDict["Billing Address Street 2"] = address.Street2 ?? nullStr;
                        contentDict["Billing Address Street 3"] = address.Street3 ?? nullStr;
                        contentDict["Billing Address City"] = address.City ?? nullStr;
                        contentDict["Billing Address State"] = address.RegionName ?? nullStr;
                        contentDict["Billing Address Postal"] = address.PostalCode ?? nullStr;
                        contentDict["Billing Address Country"] = address.CountryName ?? nullStr;
                    }
                }
                var shippingAccountContact = addressBook.FirstOrDefault(x => x.IsPrimary);
                var shipping = shippingAccountContact?.Slave;
                // ReSharper disable once InvertIf
                if (shipping is not null)
                {
                    contentDict["Shipping Contact"] = sectionHeaderValue;
                    contentDict["Shipping First Name"] = shipping.FirstName ?? nullStr;
                    contentDict["Shipping Last Name"] = shipping.LastName ?? nullStr;
                    contentDict["Shipping Email 1"] = shipping.Email1 ?? nullStr;
                    contentDict["Shipping Phone"] = shipping.Phone1 ?? nullStr;
                    contentDict["Shipping Fax"] = shipping.Fax1 ?? nullStr;
                    var address = shipping.Address;
                    // ReSharper disable once InvertIf
                    if (address is not null)
                    {
                        contentDict["Shipping Company"] = address.Company ?? nullStr;
                        contentDict["Shipping Address Street 1"] = address.Street1 ?? nullStr;
                        contentDict["Shipping Address Street 2"] = address.Street2 ?? nullStr;
                        contentDict["Shipping Address Street 3"] = address.Street3 ?? nullStr;
                        contentDict["Shipping Address City"] = address.City ?? nullStr;
                        contentDict["Shipping Address State"] = address.RegionName ?? nullStr;
                        contentDict["Shipping Address Postal"] = address.PostalCode ?? nullStr;
                        contentDict["Shipping Address Country"] = address.CountryName ?? nullStr;
                    }
                }
                // Return result
                return contentDict.WrapInPassingCEFAR()!;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail)}.{nameof(BuildTableDictionary)}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<Dictionary<string, string>>(ex.Message);
            }
        }

        private async Task<CEFActionResponse<string>> WritePdfToDiskAndGetPath(
            byte[] bytes,
            int accountID,
            string? contextProfileName)
        {
            try
            {
                var fileProvider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
                if (fileProvider is null)
                {
                    return CEFAR.FailingCEFAR<string>($"{nameof(WritePdfToDiskAndGetPath)}: file provider was not found");
                }
                var path = await fileProvider.GetFileSaveRootPathFromFileEntityTypeAsync(
                        FileEntityType.StoredFileAccount)
                    .ConfigureAwait(false);
                var pathDirectories = path.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                var pathBuilder = path.StartsWith("\\\\") ? "\\\\" : string.Empty;
                foreach (var directory in pathDirectories)
                {
                    if (directory.Contains(":"))
                    {
                        pathBuilder = directory;
                    }
                    else
                    {
                        if (pathBuilder != string.Empty && pathBuilder != "\\\\")
                        {
                            pathBuilder += "\\";
                        }
                        pathBuilder += directory;
                    }
                    if (Directory.Exists(pathBuilder))
                    {
                        continue;
                    }
                    Directory.CreateDirectory(pathBuilder);
                }
                var brandStr = brandName?.Replace(" ", "_") ?? "N/A";
                var regTypeStr = registrationType?.Replace(" ", "_") ?? "N/A";
                var fileName = $"Reg-{brandStr}-{regTypeStr}-ACCT-{accountID}.pdf";
                var filePath = Path.Combine(path, fileName);
                var fileExt = Path.GetExtension(fileName);
                if (File.Exists(filePath))
                {
                    var fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
                    for (var i = 2; File.Exists(filePath); i++)
                    {
                        fileName = $"{fileNameNoExt}-{i}{fileExt}";
                        filePath = Path.Combine(path, fileName);
                    }
                }
                using var stream = new FileStream(filePath, FileMode.CreateNew);
                await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                return filePath.WrapInPassingCEFAR()!;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail)}.{nameof(WritePdfToDiskAndGetPath)}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<string>(ex.Message);
            }
        }
    }
}
