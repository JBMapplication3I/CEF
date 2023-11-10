// <copyright file="IVisitorModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IVisitorModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IVisitorModel
    {
        #region Related Objects
        /// <summary>Gets or sets the visits.</summary>
        /// <value>The visits.</value>
        List<IVisitModel>? Visits { get; set; }
        #endregion
    }
}
