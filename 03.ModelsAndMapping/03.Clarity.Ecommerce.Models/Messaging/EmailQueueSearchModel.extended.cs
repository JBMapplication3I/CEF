// <copyright file="EmailQueueSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue search model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the email queue search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IEmailQueueSearchModel"/>
    public partial class EmailQueueSearchModel
    {
        /// <inheritdoc/>
        public int? TemplateID { get; set; }
    }
}
