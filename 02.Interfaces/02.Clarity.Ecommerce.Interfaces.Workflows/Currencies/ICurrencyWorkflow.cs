// <copyright file="ICurrencyWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICurrencyWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;

    public partial interface ICurrencyWorkflow
    {
        /// <summary>Converts from one currency to another.</summary>
        /// <param name="keyA">              The key a.</param>
        /// <param name="keyB">              The key b.</param>
        /// <param name="value">             The value.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A decimal.</returns>
        Task<decimal> ConvertAsync(string keyA, string keyB, decimal value, string? contextProfileName);
    }
}
