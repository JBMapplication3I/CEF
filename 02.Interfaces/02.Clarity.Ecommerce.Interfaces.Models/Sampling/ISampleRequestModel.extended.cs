// <copyright file="ISampleRequestModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISampleRequestModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface ISampleRequestModel
    {
        /// <summary>Gets or sets the identifier of the sales group.</summary>
        /// <value>The identifier of the sales group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the sales group key.</summary>
        /// <value>The sales group key.</value>
        string? SalesGroupKey { get; set; }

        /// <summary>Gets or sets the group the sales belongs to.</summary>
        /// <value>The sales group.</value>
        ISalesGroupModel? SalesGroup { get; set; }
    }
}
