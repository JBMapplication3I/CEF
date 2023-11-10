// <copyright file="CheckoutPayByPayoneer.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout pay by payoneer class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A checkout pay by payoneer.</summary>
    /// <seealso cref="ICheckoutPayByPayoneer"/>
    public class CheckoutPayByPayoneer : ICheckoutPayByPayoneer
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(PayoneerAccountID), DataType = "long?", ParameterType = "body", IsRequired = false,
             Description = "Payoneer Account ID"), DefaultValue(null)]
        public long? PayoneerAccountID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PayoneerCustomerID), DataType = "long?", ParameterType = "body", IsRequired = false,
             Description = "Payoneer Customer ID"), DefaultValue(null)]
        public long? PayoneerCustomerID { get; set; }
    }
}
