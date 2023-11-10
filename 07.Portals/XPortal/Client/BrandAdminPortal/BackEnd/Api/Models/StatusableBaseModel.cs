// <copyright file="ITypeModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITypeModel interface</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    /// <summary>A data model for the statuseable base.</summary>
    /// <seealso cref="DisplayableBaseModel"/>
    public abstract partial class StatusableBaseModel : DisplayableBaseModel
    {
        /// <summary>Gets or sets the identifier of the status.</summary>
        /// <value>The identifier of the status.</value>
        public int StatusID { get; set; }
    }
}
