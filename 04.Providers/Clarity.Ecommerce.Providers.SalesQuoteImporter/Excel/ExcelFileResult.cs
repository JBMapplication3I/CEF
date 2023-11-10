// <copyright file="ExcelFileResult.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel file result class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using ServiceStack;
    using ServiceStack.Web;

    /// <summary>Encapsulates the result of an excel file.</summary>
    /// <seealso cref="IHasOptions"/>
    /// <seealso cref="IStreamWriterAsync"/>
    public class ExcelFileResult : IHasOptions, IStreamWriterAsync
    {
        private readonly Stream responseStream;

        /// <summary>Initializes a new instance of the
        /// <see cref="ExcelFileResult"/> class.</summary>
        /// <param name="stream">  The response stream.</param>
        /// <param name="fileName">Filename of the file.</param>
        public ExcelFileResult(Stream stream, string fileName)
        {
            responseStream = stream;
            long length = -1;
            try
            {
                length = responseStream.Length;
            }
            catch (NotSupportedException)
            {
                // Do Nothing
            }
            Options = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ["Content-Disposition"] = $"attachment; filename=\"{fileName}\";",
                ["X-Filename"] = fileName,
                ["X-Api-Length"] = length.ToString(),
            };
        }

        /// <summary>Gets options for controlling the operation.</summary>
        /// <value>The options.</value>
        public IDictionary<string, string> Options { get; }

        /// <summary>Writes to the response stream.</summary>
        /// <param name="stream">The response stream.</param>
        /// <param name="token"> The token.</param>
        /// <returns>A Task.</returns>
        public async Task WriteToAsync(Stream stream, CancellationToken token)
        {
            if (responseStream == null)
            {
                return;
            }
            using (responseStream)
            {
                responseStream.WriteTo(stream);
                await responseStream.FlushAsync(token).ConfigureAwait(false);
            }
        }
    }
}
