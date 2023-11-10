// <copyright file="FranchiseAccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise account model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    public partial class FranchiseAccountModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasAccessToFranchise), DataType = "bool", ParameterType = "body", IsRequired = false,
            Description = "A value indicating whether this account has access to the franchise")]
        public bool HasAccessToFranchise { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The price point for this account when purchasing under this franchise.")]
        public int? PricePointID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The price point for this account when purchasing under this franchise.")]
        public string? PricePointKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The price point for this account when purchasing under this franchise.")]
        public string? PricePointName { get; set; }

        /// <inheritdoc cref="IFranchiseAccountModel.PricePoint" />
        [ApiMember(Name = nameof(PricePoint), DataType = "PricePointModel", ParameterType = "body", IsRequired = false,
            Description = "The price point for this account when purchasing under this franchise.")]
        public PricePointModel? PricePoint { get; set; }

        /// <inheritdoc/>
        IPricePointModel? IFranchiseAccountModel.PricePoint { get => PricePoint; set => PricePoint = (PricePointModel?)value; }
    }
}
