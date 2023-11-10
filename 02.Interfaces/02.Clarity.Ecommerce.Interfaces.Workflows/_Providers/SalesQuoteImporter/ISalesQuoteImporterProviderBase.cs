// <copyright file="ISalesQuoteImporterProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesQuoteImporterProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Importer
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for sales quote importer provider base.</summary>
    public interface ISalesQuoteImporterProviderBase : IProviderBase
    {
        /// <summary>Import file as sales quote.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="mappingID">         Identifier for the mapping.</param>
        /// <param name="mappingKey">        The mapping key.</param>
        /// <param name="mappingName">       Name of the mapping.</param>
        /// <returns>A <see cref="CEFActionResponse{ISalesQuoteModel}"/>.</returns>
        Task<CEFActionResponse<ISalesQuoteModel>> ImportFileAsSalesQuoteAsync(
            string? contextProfileName,
            string fileName,
            int? mappingID = null,
            string? mappingKey = null,
            string? mappingName = null);

        /// <summary>Export sales quote as file.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="id">                The identifier.</param>
        /// <param name="format">            Describes the format to use.</param>
        /// <param name="mappingID">         Identifier for the mapping.</param>
        /// <param name="mappingKey">        The mapping key.</param>
        /// <param name="mappingName">       Name of the mapping.</param>
        /// <returns>A <see cref="CEFActionResponse{TResult}"/>.</returns>
        Task<CEFActionResponse<object>> ExportSalesQuoteAsFileAsync(
            string? contextProfileName,
            int id,
            string format,
            int? mappingID = null,
            string? mappingKey = null,
            string? mappingName = null);
    }
}
