// <copyright file="IHaveATypeBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveATypeBaseSearchModel interface</summary>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have a type base search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public interface IHaveATypeBaseSearchModel : IBaseSearchModel
    {
        /// <summary>Gets or sets the identifier of the type.</summary>
        /// <value>The identifier of the type.</value>
        int? TypeID { get; set; }

        /// <summary>Gets or sets the type IDs.</summary>
        /// <value>The type IDs.</value>
        int?[]? TypeIDs { get; set; }

        /// <summary>Gets or sets the identifier of the excluded type.</summary>
        /// <value>The identifier of the excluded type.</value>
        int? ExcludedTypeID { get; set; }

        /// <summary>Gets or sets the excluded type IDs.</summary>
        /// <value>The excluded type IDs.</value>
        int?[]? ExcludedTypeIDs { get; set; }

        /// <summary>Gets or sets the type key.</summary>
        /// <value>The type key.</value>
        string? TypeKey { get; set; }

        /// <summary>Gets or sets the type keys.</summary>
        /// <value>The type keys.</value>
        string?[]? TypeKeys { get; set; }

        /// <summary>Gets or sets the excluded type key.</summary>
        /// <value>The excluded type key.</value>
        string? ExcludedTypeKey { get; set; }

        /// <summary>Gets or sets the excluded type keys.</summary>
        /// <value>The excluded type keys.</value>
        string?[]? ExcludedTypeKeys { get; set; }

        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        string? TypeName { get; set; }

        /// <summary>Gets or sets a list of names of the types.</summary>
        /// <value>A list of names of the types.</value>
        string?[]? TypeNames { get; set; }

        /// <summary>Gets or sets the name of the excluded type.</summary>
        /// <value>The name of the excluded type.</value>
        string? ExcludedTypeName { get; set; }

        /// <summary>Gets or sets a list of names of the excluded types.</summary>
        /// <value>A list of names of the excluded types.</value>
        string?[]? ExcludedTypeNames { get; set; }

        /// <summary>Gets or sets the name of the type display.</summary>
        /// <value>The name of the type display.</value>
        string? TypeDisplayName { get; set; }

        /// <summary>Gets or sets a list of names of the type displays.</summary>
        /// <value>A list of names of the type displays.</value>
        string?[]? TypeDisplayNames { get; set; }

        /// <summary>Gets or sets the name of the excluded type display.</summary>
        /// <value>The name of the excluded type display.</value>
        string? ExcludedTypeDisplayName { get; set; }

        /// <summary>Gets or sets a list of names of the excluded type displays.</summary>
        /// <value>A list of names of the excluded type displays.</value>
        string?[]? ExcludedTypeDisplayNames { get; set; }

        /// <summary>Gets or sets the type translation key.</summary>
        /// <value>The type translation key.</value>
        string? TypeTranslationKey { get; set; }
    }
}
