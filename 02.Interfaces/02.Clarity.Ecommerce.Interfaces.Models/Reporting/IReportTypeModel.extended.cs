// <copyright file="IReportTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IReportTypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for report type model.</summary>
    /// <seealso cref="ITypableBaseModel"/>
    public partial interface IReportTypeModel
    {
        /// <summary>Gets or sets the template.</summary>
        /// <value>The template.</value>
        byte[]? Template { get; set; }
    }
}
