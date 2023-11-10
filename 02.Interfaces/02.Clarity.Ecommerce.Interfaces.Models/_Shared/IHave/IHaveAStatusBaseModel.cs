// <copyright file="IHaveAStatusBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStatusBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have the status base model.</summary>
    /// <typeparam name="TStatusModel">Type of the status model.</typeparam>
    /// <seealso cref="IHaveAStatusBaseModel"/>
    public interface IHaveAStatusBaseModel<TStatusModel> : IHaveAStatusBaseModel
        where TStatusModel : IStatusableBaseModel
    {
        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        TStatusModel? Status { get; set; }
    }

    /// <summary>Interface for have the status base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveAStatusBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the status.</summary>
        /// <value>The identifier of the status.</value>
        int StatusID { get; set; }

        /// <summary>Gets or sets the status key.</summary>
        /// <value>The status key.</value>
        string? StatusKey { get; set; }

        /// <summary>Gets or sets the name of the status.</summary>
        /// <value>The name of the status.</value>
        string? StatusName { get; set; }

        /// <summary>Gets or sets the name of the status display.</summary>
        /// <value>The name of the status display.</value>
        string? StatusDisplayName { get; set; }

        /// <summary>Gets or sets the status translation key.</summary>
        /// <value>The status translation key.</value>
        string? StatusTranslationKey { get; set; }

        /// <summary>Gets or sets the status sort order.</summary>
        /// <value>The status sort order.</value>
        int? StatusSortOrder { get; set; }
    }
}
