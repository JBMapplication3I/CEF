// <copyright file="ExcelSalesQuoteImporterService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel sales quote importer service class</summary>
// ReSharper disable UnusedAutoPropertyAccessor.Global, UnusedMember.Global
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>Imports the specified file as a Sales Quote entity (and related/associated entities based on the mapping
    /// file).</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
     Authenticate,
     UsedInAdmin,
     Route("/Providers/SalesQuoteImporters/Excel/ImportFileAsSalesQuote", "POST",
        Summary = "Imports the specified file as a Sales Quote entity (and related/associated entities based on the mapping file).")]
    public class ExcelImportFileAsSalesQuote : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        /// <remarks>This file should already be uploaded via the standard media uploader endpoint.</remarks>
        [ApiMember(Name = nameof(FileName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The name of the file to process. This file should already be uploaded via the standard media uploader endpoint.")]
        public string FileName { get; set; }

        /// <summary>Gets or sets the identifier of the mapping.</summary>
        /// <value>The identifier of the mapping.</value>
        [ApiMember(Name = nameof(MappingID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The name of the Import/Export Mapping to use for processing. Either the Mapping's Name, Key or ID must be supplied")]
        public int? MappingID { get; set; }

        /// <summary>Gets or sets the mapping key.</summary>
        /// <value>The mapping key.</value>
        [ApiMember(Name = nameof(MappingKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The name of the Import/Export Mapping to use for processing. Either the Mapping's Name, Key or ID must be supplied")]
        public string MappingKey { get; set; }

        /// <summary>Gets or sets the name of the mapping.</summary>
        /// <value>The name of the mapping.</value>
        [ApiMember(Name = nameof(MappingName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The name of the Import/Export Mapping to use for processing. Either the Mapping's Name, Key or ID must be supplied")]
        public string MappingName { get; set; }
    }

    /// <summary>Exports the specified quote as an Excel 2007+ format (.xlsx) file and returns the download url as a
    /// string.</summary>
    /// <seealso cref="ImplementsID"/>
    [PublicAPI]
    [Authenticate]
    [Route("/Providers/SalesQuoteImporters/Excel/ExportSalesQuoteAsFile/{ID}", "POST",
        Summary = "Exports the specified quote as an Excel 2007+ format (.xlsx) file and returns the output file as a download attachment.")]
    public class ExcelExportSalesQuoteAsFile : ImplementsID, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the mapping.</summary>
        /// <value>The identifier of the mapping.</value>
        [ApiMember(Name = nameof(MappingID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The name of the Import/Export Mapping to use for processing. Either the Mapping's Name, Key or ID must be supplied")]
        public int? MappingID { get; set; }

        /// <summary>Gets or sets the mapping key.</summary>
        /// <value>The mapping key.</value>
        [ApiMember(Name = nameof(MappingKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The name of the Import/Export Mapping to use for processing. Either the Mapping's Name, Key or ID must be supplied")]
        public string MappingKey { get; set; }

        /// <summary>Gets or sets the name of the mapping.</summary>
        /// <value>The name of the mapping.</value>
        [ApiMember(Name = nameof(MappingName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The name of the Import/Export Mapping to use for processing. Either the Mapping's Name, Key or ID must be supplied")]
        public string MappingName { get; set; }
    }

    /// <summary>An excel import file as sales quote service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class ExcelImportFileAsSalesQuoteService : ClarityEcommerceServiceBase
    {
        /// <summary>React to this endpoint when accessed with a POST request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        public async Task<object> Post(ExcelImportFileAsSalesQuote request)
        {
            return await new ExcelSalesQuoteImporterProvider().ImportFileAsSalesQuoteAsync(
                    null,
                    request.FileName,
                    request.MappingID,
                    request.MappingKey,
                    request.MappingName)
                .ConfigureAwait(false);
        }

        /// <summary>React to this endpoint when accessed with a POST request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        public async Task<object> Post(ExcelExportSalesQuoteAsFile request)
        {
            var provider = new ExcelSalesQuoteImporterProvider();
            // This will attach the output as a file stream to the response
            var result = await provider.ExportSalesQuoteAsFileAsync(
                    null,
                    request.ID,
                    "xlsx",
                    request.MappingID,
                    request.MappingKey,
                    request.MappingName)
                .ConfigureAwait(false);
            if (!result.ActionSucceeded)
            {
                throw new ArgumentException(result.Messages.Aggregate((c, n) => c + "\r\n" + n));
            }
            return result.Result;
        }
    }

    /// <summary>An excel sales quote importer feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class ExcelSalesQuoteImporterFeature : IPlugin
    {
        /// <summary>Registers this ExcelSalesQuoteImporterFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
