// <copyright file="CurrencyService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using ServiceStack;

    [PublicAPI,
        Route("/Currencies/Convert", "GET",
            Summary = "Convert a decimal value from one currency to another")]
    public partial class ConvertCurrencyValueAtoB : IReturn<decimal>
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        [ApiMember(Name = nameof(Value), DataType = "decimal", ParameterType = "query", IsRequired = true,
            Description = "The decimal value to convert")]
        public decimal Value { get; set; }

        /// <summary>Gets or sets the key a.</summary>
        /// <value>The key a.</value>
        [ApiMember(Name = nameof(KeyA), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The key of the starting currency (convert from)")]
        public string KeyA { get; set; } = null!;

        /// <summary>Gets or sets the key b.</summary>
        /// <value>The key b.</value>
        [ApiMember(Name = nameof(KeyB), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The key of the currency to convert to")]
        public string KeyB { get; set; } = null!;
    }

    public partial class CurrencyService
    {
        public async Task<object?> Get(ConvertCurrencyValueAtoB request)
        {
            return await Workflows.Currencies.ConvertAsync(request.KeyA, request.KeyB, request.Value, null).ConfigureAwait(false);
        }
    }
}
