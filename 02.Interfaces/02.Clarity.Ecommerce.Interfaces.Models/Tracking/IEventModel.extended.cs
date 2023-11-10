// <copyright file="IEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEventModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for event model.</summary>
    public partial interface IEventModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the visit.</summary>
        /// <value>The identifier of the visit.</value>
        int? VisitID { get; set; }

        /// <summary>Gets or sets the visit.</summary>
        /// <value>The visit.</value>
        IVisitModel? Visit { get; set; }

        /// <summary>Gets or sets the visit key.</summary>
        /// <value>The visit key.</value>
        string? VisitKey { get; set; }

        /// <summary>Gets or sets the name of the visit.</summary>
        /// <value>The name of the visit.</value>
        string? VisitName { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the page view events.</summary>
        /// <value>The page view events.</value>
        List<IPageViewEventModel>? PageViewEvents { get; set; }
        #endregion
    }
}
