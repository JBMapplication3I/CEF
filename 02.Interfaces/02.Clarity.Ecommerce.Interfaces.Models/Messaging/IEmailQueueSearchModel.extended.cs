// <copyright file="IEmailQueueSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEmailQueueSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for email queue search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public partial interface IEmailQueueSearchModel
    {
        /// <summary>Gets or sets the identifier of the template.</summary>
        /// <value>The identifier of the template.</value>
        int? TemplateID { get; set; }
    }
}
