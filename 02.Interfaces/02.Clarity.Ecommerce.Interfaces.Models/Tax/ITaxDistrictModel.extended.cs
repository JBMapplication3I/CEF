// <copyright file="ITaxDistrictModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITaxDistrictModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for tax district model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface ITaxDistrictModel
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the district.</summary>
        /// <value>The identifier of the district.</value>
        int DistrictID { get; set; }

        /// <summary>Gets or sets the district key.</summary>
        /// <value>The district key.</value>
        string? DistrictKey { get; set; }

        /// <summary>Gets or sets the name of the district.</summary>
        /// <value>The name of the district.</value>
        string? DistrictName { get; set; }

        /// <summary>Gets or sets the district.</summary>
        /// <value>The district.</value>
        IDistrictModel? District { get; set; }
        #endregion
    }
}
