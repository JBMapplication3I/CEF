// <copyright file="IImportDocumentExcel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImportDocumentExcel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for import document excel.</summary>
    public interface IImportDocumentExcel : IImportDocumentBase
    {
        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        IEnumerator<string[]> GetEnumerator();
    }
}
