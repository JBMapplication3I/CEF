// <copyright file="IRow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Importer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Interface for row.</summary>
    public interface IRow
    {
        /// <summary>Gets the entry by column.</summary>
        /// <value>The entry by column.</value>
        Dictionary<uint, Cell> EntryByColumn { get; }

        /// <summary>Gets the entry by column uof m.</summary>
        /// <value>The entry by column uof m.</value>
        Dictionary<uint, Cell> EntryByColumnUofM { get; }

        /// <summary>Gets the sheet.</summary>
        /// <value>The sheet.</value>
        IImporterProviderBase Sheet { get; }

        /// <summary>Entry by column name.</summary>
        /// <param name="fileName">  Filename of the file.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>A Cell.</returns>
        Task<Cell?> EntryByColumnNameAsync(string fileName, string columnName);

        /// <summary>Entry by mapping field.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="field">   The field.</param>
        /// <returns>A Cell.</returns>
        Task<Cell?> EntryByMappingFieldAsync(string fileName, Enums.ProductImportFieldEnum field);

        /// <summary>Enumerates entries by mapping field in this collection.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="field">   The field.</param>
        /// <returns>An enumerator that allows foreach to be used to process entries by mapping field in this collection.</returns>
        Task<IEnumerable<Cell?>> EntriesByMappingFieldAsync(string fileName, Enums.ProductImportFieldEnum field);
    }
}
