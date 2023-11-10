// <copyright file="IHaveAStateBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAStateBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have the state base model.</summary>
    /// <typeparam name="TStateModel">Type of the state model.</typeparam>
    /// <seealso cref="IHaveAStateBaseModel"/>
    public interface IHaveAStateBaseModel<TStateModel> : IHaveAStateBaseModel
        where TStateModel : IStateableBaseModel
    {
        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        TStateModel? State { get; set; }
    }

    /// <summary>Interface for have the state base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveAStateBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the state.</summary>
        /// <value>The identifier of the state.</value>
        int StateID { get; set; }

        /// <summary>Gets or sets the state key.</summary>
        /// <value>The state key.</value>
        string? StateKey { get; set; }

        /// <summary>Gets or sets the name of the state.</summary>
        /// <value>The name of the state.</value>
        string? StateName { get; set; }

        /// <summary>Gets or sets the name of the state display.</summary>
        /// <value>The name of the state display.</value>
        string? StateDisplayName { get; set; }

        /// <summary>Gets or sets the state translation key.</summary>
        /// <value>The state translation key.</value>
        string? StateTranslationKey { get; set; }

        /// <summary>Gets or sets the state sort order.</summary>
        /// <value>The state sort order.</value>
        int? StateSortOrder { get; set; }
    }
}
