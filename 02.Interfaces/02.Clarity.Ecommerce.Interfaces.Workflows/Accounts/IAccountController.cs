// <copyright file="IAccountController.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountController interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for account controller.</summary>
    public interface IAccountController
    {
        /// <summary>Gets the account.</summary>
        /// <param name="id">          The identifier.</param>
        /// <param name="currentModel">The current model.</param>
        /// <returns>An IAccountModel.</returns>
        Task<IAccountModel> GetAsync(string id, IAccountModel? currentModel = null);
    }
}
