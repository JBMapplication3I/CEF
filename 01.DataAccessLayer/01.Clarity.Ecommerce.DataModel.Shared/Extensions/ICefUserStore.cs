// <copyright file="ICEFUserStore.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICEFUserStore interface</summary>
namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;
    using Microsoft.AspNet.Identity;

    /// <summary>Interface for CEF user store.</summary>
    /// <seealso cref="IUserStore{User,Int32}"/>
    public interface ICEFUserStore : IUserRoleStore<User, int>
    {
        /// <summary>Gets the context.</summary>
        /// <value>The context.</value>
        IClarityEcommerceEntities Context { get; }
    }
}
