// <copyright file="IHaveATypeBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveATypeBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have a type base model.</summary>
    /// <typeparam name="TTypeModel">Type of the type model.</typeparam>
    /// <seealso cref="IHaveATypeBaseModel"/>
    public interface IHaveATypeBaseModel<TTypeModel> : IHaveATypeBaseModel
        where TTypeModel : ITypableBaseModel
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        TTypeModel? Type { get; set; }
    }

    /// <summary>Interface for have a type base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveATypeBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the type.</summary>
        /// <value>The identifier of the type.</value>
        int TypeID { get; set; }

        /// <summary>Gets or sets the type key.</summary>
        /// <value>The type key.</value>
        string? TypeKey { get; set; }

        /// <summary>Gets or sets the name of the type.</summary>
        /// <value>The name of the type.</value>
        string? TypeName { get; set; }

        /// <summary>Gets or sets the name of the type display.</summary>
        /// <value>The name of the type display.</value>
        string? TypeDisplayName { get; set; }

        /// <summary>Gets or sets the type translation key.</summary>
        /// <value>The type translation key.</value>
        string? TypeTranslationKey { get; set; }

        /// <summary>Gets or sets the type sort order.</summary>
        /// <value>The type sort order.</value>
        int? TypeSortOrder { get; set; }
    }
}
