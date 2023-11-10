// <copyright file="IFutureImportModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFutureImportModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    public partial interface IFutureImportModel
    {
        /// <summary>Gets or sets the filename of the file.</summary>
        /// <value>The name of the file.</value>
        string? FileName { get; set; }

        /// <summary>Gets or sets the Date/Time to run import at.</summary>
        /// <value>The run import at Date/Time value.</value>
        DateTime RunImportAt { get; set; }

        /// <summary>Gets or sets the attempts.</summary>
        /// <value>The attempts.</value>
        int Attempts { get; set; }

        /// <summary>Gets or sets a value indicating whether this Future Import has errors when it tries to run.</summary>
        /// <value>True if this IFutureImport has error, false if not.</value>
        bool HasError { get; set; }
    }
}
