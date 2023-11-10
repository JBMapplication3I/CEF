// <copyright file="ReportTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the report type model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the report type.</summary>
    /// <seealso cref="TypableBaseModel"/>
    /// <seealso cref="Interfaces.Models.IReportTypeModel"/>
    public partial class ReportTypeModel
    {
        /// <inheritdoc/>
        public byte[]? Template { get; set; }
    }
}
