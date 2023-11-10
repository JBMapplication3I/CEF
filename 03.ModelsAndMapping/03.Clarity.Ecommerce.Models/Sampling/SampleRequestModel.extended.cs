// <copyright file="SampleRequestModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample request model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    public partial class SampleRequestModel
    {
        /// <inheritdoc/>
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupKey { get; set; }

        /// <inheritdoc cref="ISampleRequestModel.SalesGroup" />
        public SalesGroupModel? SalesGroup { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISampleRequestModel.SalesGroup { get => SalesGroup; set => SalesGroup = (SalesGroupModel?)value; }
    }
}
