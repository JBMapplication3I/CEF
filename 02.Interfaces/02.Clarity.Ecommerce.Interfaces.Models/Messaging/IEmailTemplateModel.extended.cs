// <copyright file="IEmailTemplateModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEmailTemplateModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for email template model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface IEmailTemplateModel
    {
        /// <summary>Gets or sets the subject.</summary>
        /// <value>The subject.</value>
        string? Subject { get; set; }

        /// <summary>Gets or sets the body.</summary>
        /// <value>The body.</value>
        string? Body { get; set; }
    }
}
