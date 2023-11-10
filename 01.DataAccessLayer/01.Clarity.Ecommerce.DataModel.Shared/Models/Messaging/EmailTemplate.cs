// <copyright file="EmailTemplate.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email template class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IEmailTemplate : INameableBase
    {
        #region EmailTemplate Properties
        /// <summary>Gets or sets the subject.</summary>
        /// <value>The subject.</value>
        string? Subject { get; set; }

        /// <summary>Gets or sets the body.</summary>
        /// <value>The body.</value>
        string? Body { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;

    [SqlSchema("Messaging", "EmailTemplate")]
    public class EmailTemplate : NameableBase, IEmailTemplate
    {
        #region EmailTemplate Properties
        /// <inheritdoc/>
        [Required, StringLength(255), DefaultValue(null)]
        public string? Subject { get; set; }

        /// <inheritdoc/>
        [Required, DefaultValue(null), DontMapOutWithListing]
        public string? Body { get; set; }
        #endregion
    }
}
