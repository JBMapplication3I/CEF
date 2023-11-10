// <copyright file="IObjectNoteBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IObjectNoteBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for object note base.</summary>
    /// <typeparam name="TMaster">Type of the master entity.</typeparam>
    /// <seealso cref="IBase"/>
    public interface IObjectNoteBase<TMaster> : IBase
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the master.</summary>
        /// <value>The master.</value>
        TMaster? Master { get; set; }
    }
}
