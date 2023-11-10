// <copyright file="IAmAContactRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAContactRelationshipTable interface</summary>
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for am a contact relationship table base.</summary>
    /// <typeparam name="TMaster"> Type of the master entity.</typeparam>
    /// <typeparam name="TContact">Type of the contact relationship table entity (Contact is the slave of that relationship).</typeparam>
    public interface IAmAContactRelationshipTable<out TMaster, TContact>
        : IAmARelationshipTable<TMaster, Contact>,
            IHaveAContactBase
        where TMaster : IBase // , IHaveContactsBase<TMaster, TContact>
        where TContact : IAmAContactRelationshipTable<TMaster, TContact>
    {
    }
}
