// <copyright file="ReportType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the report type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IReportType : ITypableBase
    {
        /// <summary>Gets or sets the template.</summary>
        /// <value>The template.</value>
        byte[]? Template { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using Interfaces.DataModel;

    [SqlSchema("Reporting", "ReportTypes")]
    public class ReportType : TypableBase, IReportType
    {
        /// <inheritdoc/>
        [/*Column(TypeName = "varbinary(max)"),*/ DontMapOutWithLite, DontMapOutWithListing, DefaultValue(null)]
        public byte[]? Template { get; set; }
    }
}
