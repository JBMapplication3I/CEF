// <copyright file="IHaveContactsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveContactsBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have contacts base.</summary>
    /// <typeparam name="TMaster"> Type of the master entity.</typeparam>
    /// <typeparam name="TContact">Type of the contact relationship entity (which has a Contact on each).</typeparam>
    public interface IHaveContactsBase<TMaster, TContact>
        : IBase
        where TMaster : IHaveContactsBase<TMaster, TContact>
        where TContact : IAmAContactRelationshipTable<TMaster, TContact>
    {
        /// <summary>Gets or sets the contacts.</summary>
        /// <value>The contacts.</value>
        ICollection<TContact>? Contacts { get; set; }
    }
}
