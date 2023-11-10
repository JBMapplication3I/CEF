// <copyright file="IImportDocumentBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImportDocumentBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.IO;

    /// <summary>Interface for import document base.</summary>
    public interface IImportDocumentBase
    {
        /// <summary>Gets or sets source document.</summary>
        /// <value>The source document.</value>
        string SourceDocument { get; set; }

        /// <summary>Gets the extension.</summary>
        /// <value>The extension.</value>
        string Extension { get; }

        /// <summary>Gets the number of rows.</summary>
        /// <value>The number of rows.</value>
        int RowCount { get; }

        /// <summary>Gets the number of columns.</summary>
        /// <value>The number of columns.</value>
        int ColumnCount { get; }

        /// <summary>Gets the grid.</summary>
        /// <value>The grid.</value>
        string[][] Grid { get; }

        /// <summary>Indexer to get items within this collection using array index syntax.</summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>The indexed item.</returns>
        string this[int row, int col] { get; }

        /// <summary>Indexer to get items within this collection using array index syntax.</summary>
        /// <param name="row">The row.</param>
        /// <returns>The indexed item.</returns>
        string[] this[int row] { get; }

        /// <summary>Imports the given file.</summary>
        /// <param name="fileName">Filename of the file.</param>
        void Import(string fileName);

        /// <summary>Imports from the source.</summary>
        /// <param name="source">            Source for the.</param>
        /// <param name="sourceDocumentName">Name of the source document.</param>
        void Import(Stream source, string sourceDocumentName);
    }
}
