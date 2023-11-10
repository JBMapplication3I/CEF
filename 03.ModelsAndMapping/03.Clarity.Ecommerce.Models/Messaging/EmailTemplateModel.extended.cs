// <copyright file="EmailTemplateModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email template model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the email template.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="Interfaces.Models.IEmailTemplateModel"/>
    public partial class EmailTemplateModel
    {
        /// <inheritdoc/>
        public string? Subject { get; set; }

        /// <inheritdoc/>
        public string? Body { get; set; }
    }
}
