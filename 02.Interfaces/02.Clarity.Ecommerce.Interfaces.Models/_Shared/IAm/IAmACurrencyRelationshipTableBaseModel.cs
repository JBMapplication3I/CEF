// <copyright file="IAmACurrencyRelationshipTableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmACurrencyRelationshipTableBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a currency relationship table base model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseModel{ICurrencyModel}"/>
    public interface IAmACurrencyRelationshipTableBaseModel
        : IAmARelationshipTableBaseModel<ICurrencyModel>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int CurrencyID { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        string? CurrencyName { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        ICurrencyModel? Currency { get; set; }
        #endregion
    }
}
