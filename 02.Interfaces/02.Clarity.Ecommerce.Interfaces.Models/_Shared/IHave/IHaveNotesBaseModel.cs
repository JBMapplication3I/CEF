// <copyright file="IHaveNotesBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveNotesBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have notes base model.</summary>
    public interface IHaveNotesBaseModel : IBaseModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        List<INoteModel>? Notes { get; set; }
        #endregion
    }
}
