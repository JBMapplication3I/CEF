// <copyright file="DiscountSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount search model interface</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    public partial class DiscountSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Code { get; set; }
    }
}
