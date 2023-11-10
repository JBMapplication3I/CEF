// <copyright file="IHaveAStatusBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStatusBaseSearchModel interface</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have a status base search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public interface IHaveAStatusBaseSearchModel : IBaseSearchModel
    {
        /// <summary>Gets or sets the identifier of the status.</summary>
        /// <value>The identifier of the status.</value>
        int? StatusID { get; set; }

        /// <summary>Gets or sets the status IDs.</summary>
        /// <value>The status IDs.</value>
        int?[]? StatusIDs { get; set; }

        /// <summary>Gets or sets the identifier of the excluded status.</summary>
        /// <value>The identifier of the excluded status.</value>
        int? ExcludedStatusID { get; set; }

        /// <summary>Gets or sets the excluded status IDs.</summary>
        /// <value>The excluded status IDs.</value>
        int?[]? ExcludedStatusIDs { get; set; }

        /// <summary>Gets or sets the status key.</summary>
        /// <value>The status key.</value>
        string? StatusKey { get; set; }

        /// <summary>Gets or sets the status keys.</summary>
        /// <value>The status keys.</value>
        string?[]? StatusKeys { get; set; }

        /// <summary>Gets or sets the excluded status key.</summary>
        /// <value>The excluded status key.</value>
        string? ExcludedStatusKey { get; set; }

        /// <summary>Gets or sets the excluded status keys.</summary>
        /// <value>The excluded status keys.</value>
        string?[]? ExcludedStatusKeys { get; set; }

        /// <summary>Gets or sets the name of the status.</summary>
        /// <value>The name of the status.</value>
        string? StatusName { get; set; }

        /// <summary>Gets or sets a list of names of the status.</summary>
        /// <value>A list of names of the status.</value>
        string?[]? StatusNames { get; set; }

        /// <summary>Gets or sets the name of the excluded status.</summary>
        /// <value>The name of the excluded status.</value>
        string? ExcludedStatusName { get; set; }

        /// <summary>Gets or sets a list of names of the excluded statuses.</summary>
        /// <value>A list of names of the excluded statuses.</value>
        string?[]? ExcludedStatusNames { get; set; }

        /// <summary>Gets or sets the name of the status display.</summary>
        /// <value>The name of the status display.</value>
        string? StatusDisplayName { get; set; }

        /// <summary>Gets or sets a list of names of the status displays.</summary>
        /// <value>A list of names of the status displays.</value>
        string?[]? StatusDisplayNames { get; set; }

        /// <summary>Gets or sets the name of the excluded status display.</summary>
        /// <value>The name of the excluded status display.</value>
        string? ExcludedStatusDisplayName { get; set; }

        /// <summary>Gets or sets a list of names of the excluded status displays.</summary>
        /// <value>A list of names of the excluded status displays.</value>
        string?[]? ExcludedStatusDisplayNames { get; set; }

        /// <summary>Gets or sets the status translation key.</summary>
        /// <value>The status translation key.</value>
        string? StatusTranslationKey { get; set; }
    }
}
