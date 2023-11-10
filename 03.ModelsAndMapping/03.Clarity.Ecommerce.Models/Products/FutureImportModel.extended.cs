// <copyright file="FutureImportModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the future import model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the future import.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IFutureImportModel"/>
    public partial class FutureImportModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(FileName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The filename including extension. This will be joined to the relative folder path where the uploads would go automatically.")]
        [DefaultValue(null)]
        public string? FileName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RunImportAt), DataType = "DateTime", ParameterType = "body", IsRequired = true,
            Description = "The date and time at which to run the import (must be in the future)")]
        [DefaultValue(null)]
        public DateTime RunImportAt { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Attempts), DataType = "int", ParameterType = "body", IsRequired = false,
            Description = "How many attempts have been made to import this file")]
        [DefaultValue(0)]
        public int Attempts { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasError), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Whether or not the import had any errors when it last ran")]
        [DefaultValue(false)]
        public bool HasError { get; set; }
    }
}
