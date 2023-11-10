// <copyright file="WalletService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the wallet service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A get user wallet.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{List{WalletModel}}}"/>
    [PublicAPI,
        Authenticate, RequiredRole("CEF Global Administrator"),
        Route("/Payments/Wallet/User/ID/{ID}", "GET",
            Summary = "Use to get the wallet entry store for a specific user")]
    public partial class GetUserWallet : ImplementsIDBase, IReturn<CEFActionResponse<List<WalletModel>>>
    {
    }

    /// <summary>A create wallet entry.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse{WalletModel}}"/>
    [PublicAPI,
        Authenticate, RequiredRole("CEF Global Administrator"),
        Route("/Payments/Wallet/User/Entry/Create", "POST",
            Summary = "Use to add an entry to the specified user's wallet")]
    public class CreateWalletEntry : WalletModel, IReturn<CEFActionResponse<WalletModel>>
    {
    }

    /// <summary>An update wallet entry.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse{WalletModel}}"/>
    [PublicAPI,
        Authenticate, RequiredRole("CEF Global Administrator"),
        Route("/Payments/Wallet/User/Entry/Update", "PUT",
            Summary = "Use to update an entry in the specified user's wallet")]
    public class UpdateWalletEntry : WalletModel, IReturn<CEFActionResponse<WalletModel>>
    {
    }

    /// <summary>A deactivate wallet entry.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate, RequiredRole("CEF Global Administrator"),
        Route("/Payments/Wallet/User/Entry/Deactivate/ID/{ID}", "PATCH",
            Summary = "Use to deactivate a specific wallet entry")]
    public class DeactivateWalletEntry : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    public partial class WalletService
    {
        /// <summary>GET handler for the <see cref="GetUserWallet"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Get(GetUserWallet request)
        {
            return await GetWallet(request.ID).ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateWalletEntry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Post(CreateWalletEntry request)
        {
            return await CreateWalletEntry(request, null).ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateWalletEntry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Put(UpdateWalletEntry request)
        {
            return await UpdateWalletEntry(request).ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateWalletEntry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Patch(DeactivateWalletEntry request)
        {
            return await DeactivateWalletEntry(Contract.RequiresValidID(request.ID)).ConfigureAwait(false);
        }
    }

    /// <summary>A get current user wallet.</summary>
    /// <seealso cref="IReturn{CEFActionResponse{List{WalletModel}}}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Storefront.UserDashboard.Wallet.View"),
        Route("/Payments/Wallet/CurrentUser/List", "GET",
            Summary = "Use to get the wallet for the current user")]
    public partial class GetCurrentUserWallet : IReturn<CEFActionResponse<List<WalletModel>>>
    {
    }

    /// <summary>A get current user wallet entry by identifier.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{WalletModel}}"/>
    [PublicAPI,
        Authenticate, RequiresAnyPermission("Storefront.UserDashboard.Wallet.View", "Payments.Wallet.View"),
        Route("/Payments/Wallet/CurrentUser/Entry/ByID/{ID}", "GET",
            Summary = "Use to get an entry from the current user's wallet")]
    public partial class GetCurrentUserWalletEntryByID : ImplementsIDBase, IReturn<CEFActionResponse<WalletModel>>
    {
    }

    /// <summary>A create current user wallet entry.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse{WalletModel}}"/>
    [PublicAPI,
        Authenticate, RequiresAnyPermission("Storefront.UserDashboard.Wallet.AddTo", "Payments.Wallet.Create"),
        Route("/Payments/Wallet/CurrentUser/Entry/Create", "POST",
            Summary = "Use to add an entry to the current user's wallet")]
    public partial class CreateCurrentUserWalletEntry : WalletModel, IReturn<CEFActionResponse<WalletModel>>
    {
    }

    /// <summary>An update current user wallet entry.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse{WalletModel}}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Storefront.UserDashboard.Wallet.Update"),
        Route("/Payments/Wallet/CurrentUser/Entry/Update", "PUT",
            Summary = "Use to update an entry in the current user's wallet")]
    public partial class UpdateCurrentUserWalletEntry : WalletModel, IReturn<CEFActionResponse<WalletModel>>
    {
    }

    /// <summary>A deactivate current user wallet entry.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Storefront.UserDashboard.Wallet.RemoveFrom"),
        Route("/Payments/Wallet/CurrentUser/Entry/Deactivate/ID/{ID}", "PATCH",
            Summary = "Use to deactivate a specific entry in the current user's wallet")]
    public partial class DeactivateCurrentUserWalletEntry : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A current user mark wallet entry as default.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Storefront.UserDashboard.Wallet.Update"),
        Route("/Payments/Wallet/CurrentUser/Entry/SetAsDefault/ID/{ID}", "PATCH",
            Summary = "Use to deactivate a specific entry in the current user's wallet")]
    public partial class CurrentUserMarkWalletEntryAsDefault : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    public partial class WalletService
    {
        /// <summary>GET handler for the <see cref="GetCurrentUserWallet"/> endpoint.</summary>
        /// <param name="_">The request body DTO. (The body in this case is empty.)</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Get(GetCurrentUserWallet _)
        {
            var userID = await SelectedUserOrCurrentUserOrThrow401Async(CurrentUserIDOrThrow401).ConfigureAwait(false);
            return await GetWallet(userID).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCurrentUserWalletEntryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Get(GetCurrentUserWalletEntryByID request)
        {
            return await GetWalletEntry(CurrentUserIDOrThrow401, request.ID).ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateCurrentUserWalletEntry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Post(CreateCurrentUserWalletEntry request)
        {
            var userID = await SelectedUserOrCurrentUserOrThrow401Async(CurrentUserIDOrThrow401).ConfigureAwait(false);
            return await CreateWalletEntry(request, userID).ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateCurrentUserWalletEntry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Put(UpdateCurrentUserWalletEntry request)
        {
            request.UserID = CurrentUserIDOrThrow401;
            return await UpdateWalletEntry(request).ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="CurrentUserMarkWalletEntryAsDefault"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Patch(CurrentUserMarkWalletEntryAsDefault request)
        {
            return await SetWalletEntryAsDefault(request.ID).ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateCurrentUserWalletEntry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Patch(DeactivateCurrentUserWalletEntry request)
        {
            await CurrentUserOrThrow401Async().ConfigureAwait(false);
            return await DeactivateWalletEntry(Contract.RequiresValidID(request.ID)).ConfigureAwait(false);
        }
    }

    /// <summary>A get user wallet as cursor.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_List_WalletModel}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Payments.Wallet.View"),
        Route("/Payments/Wallet/User/{UserID}/List", "GET",
            Summary = "Use to get the wallet for the current user")]
    public partial class GetUserWalletAsCSR : IReturn<CEFActionResponse<List<WalletModel>>>
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int UserID { get; set; }
    }

    /// <summary>A get user wallet entry by identifier as cursor.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse_WalletModel}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Payments.Wallet.View"),
        Route("/Payments/Wallet/User/{UserID}/Entry/ByID/{ID}", "GET",
            Summary = "Use to get an entry from the current user's wallet")]
    public partial class GetUserWalletEntryByIDAsCSR : ImplementsIDBase, IReturn<CEFActionResponse<WalletModel>>
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int UserID { get; set; }
    }

    /// <summary>A create user wallet entry as cursor.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse{WalletModel}}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Payments.Wallet.Create"),
        Route("/Payments/Wallet/User/{UserID}/Entry/Create", "POST",
            Summary = "Use to add an entry to the current user's wallet")]
    public partial class CreateUserWalletEntryAsCSR : WalletModel, IReturn<CEFActionResponse<WalletModel>>
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public new int UserID { get; set; }
    }

    /// <summary>An update user wallet entry as cursor.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse{WalletModel}}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Payments.Wallet.Update"),
        Route("/Payments/Wallet/User/{UserID}/Entry/Update", "PUT",
            Summary = "Use to update an entry in the current user's wallet")]
    public partial class UpdateUserWalletEntryAsCSR : WalletModel, IReturn<CEFActionResponse<WalletModel>>
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public new int UserID { get; set; }
    }

    /// <summary>A deactivate user wallet entry as cursor.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate, RequiredPermission("Payments.Wallet.Deactivate"),
        Route("/Payments/Wallet/User/{UserID}/Entry/Deactivate/ID/{ID}", "PATCH",
            Summary = "Use to deactivate a specific entry in the current user's wallet")]
    public partial class DeactivateUserWalletEntryAsCSR : ImplementsIDBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int UserID { get; set; }
    }

    public partial class WalletService
    {
        /// <summary>GET handler for the <see cref="GetUserWalletAsCSR"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Get(GetUserWalletAsCSR request)
        {
            return await GetWallet(Contract.RequiresValidID(request.UserID)).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetUserWalletEntryByIDAsCSR"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Get(GetUserWalletEntryByIDAsCSR request)
        {
            return await GetWalletEntry(Contract.RequiresValidID(request.UserID), request.ID).ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateUserWalletEntryAsCSR"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Post(CreateUserWalletEntryAsCSR request)
        {
            Contract.RequiresValidID(request.UserID);
            return await CreateWalletEntry(request, request.UserID).ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateUserWalletEntryAsCSR"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Put(UpdateUserWalletEntryAsCSR request)
        {
            Contract.RequiresValidID(request.UserID);
            return await UpdateWalletEntry(request).ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateUserWalletEntryAsCSR"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public async Task<object?> Patch(DeactivateUserWalletEntryAsCSR request)
        {
            Contract.RequiresValidID(request.UserID);
            return await DeactivateWalletEntry(Contract.RequiresValidID(request.ID)).ConfigureAwait(false);
        }
    }

    public partial class WalletService
    {
        /// <summary>Blank failed results.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result.</param>
        private static void BlankFailedResults<T>(CEFActionResponse<T> result)
            where T : class
        {
            if (result.ActionSucceeded)
            {
                return;
            }
            result.Result = null; // Ensure if something failed we send no values over the wire
        }

        /// <summary>Blank tokens.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result.</param>
        private static void BlankTokens<T>(CEFActionResponse<List<T>> result)
            where T : class, IWalletModel
        {
            if (Contract.CheckEmpty(result.Result))
            {
                return;
            }
            foreach (var entry in result.Result!)
            {
                entry.Token = null;
            }
        }

        /// <summary>Blank tokens.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result.</param>
        private static void BlankTokens<T>(CEFActionResponse<T> result)
            where T : class, IWalletModel
        {
            if (result.Result == null)
            {
                return;
            }
            result.Result.Token = null;
        }

        /// <summary>Verify payment provider supports wallet.</summary>
        /// <returns>A Task{IWalletProviderBase}.</returns>
        private static async Task<IWalletProviderBase> VerifyPaymentProviderSupportsWallet()
        {
            var provider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName: null);
            if (provider is not IWalletProviderBase wallet)
            {
                throw new ArgumentException("Current payment provider doesn't implement wallet functionalities");
            }
            await provider.InitConfigurationAsync(contextProfileName: null).ConfigureAwait(false);
            return wallet;
        }

        /// <summary>Gets a wallet.</summary>
        /// <param name="userID">Identifier for the user.</param>
        /// <returns>The wallet.</returns>
        private async Task<CEFActionResponse<List<WalletModel>>> GetWallet(int userID)
        {
            var result = await Workflows.Wallets.GetWalletForUserAsync(
                    userID: userID,
                    provider: await VerifyPaymentProviderSupportsWallet().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            BlankFailedResults(result);
            BlankTokens(result);
            return result.ChangeCEFARListType<IWalletModel, WalletModel>();
        }

        /// <summary>Gets wallet entry.</summary>
        /// <param name="userID"> Identifier for the user.</param>
        /// <param name="entryID">Identifier for the entry.</param>
        /// <returns>The wallet entry.</returns>
        private async Task<CEFActionResponse<WalletModel>> GetWalletEntry(int userID, int entryID)
        {
            var result = await Workflows.Wallets.GetWalletEntryForUserAsync(
                    userID: userID,
                    entryID: entryID,
                    provider: await VerifyPaymentProviderSupportsWallet().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            BlankFailedResults(result);
            BlankTokens(result);
            return result.ChangeCEFARType<IWalletModel, WalletModel>();
        }

        /// <summary>Creates wallet entry.</summary>
        /// <param name="request">   The request.</param>
        /// <param name="overrideID">Identifier for the override.</param>
        /// <returns>The new wallet entry.</returns>
        private async Task<CEFActionResponse<WalletModel>> CreateWalletEntry(IWalletModel request, int? overrideID)
        {
            request.UserID = overrideID ?? CurrentUserIDOrThrow401;
            var result = await Workflows.Wallets.CreateWalletEntryAsync(
                    request,
                    await VerifyPaymentProviderSupportsWallet().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
            BlankFailedResults(result);
            BlankTokens(result);
            return result.ChangeCEFARType<IWalletModel, WalletModel>();
        }

        /// <summary>Updates the wallet entry described by request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{CEFActionResponse{WalletModel}}.</returns>
        private async Task<CEFActionResponse<WalletModel>> UpdateWalletEntry(IWalletModel request)
        {
            request.UserID = CurrentUserIDOrThrow401;
            var result = await Workflows.Wallets.UpdateWalletEntryAsync(
                    request,
                    await VerifyPaymentProviderSupportsWallet().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
            BlankFailedResults(result);
            BlankTokens(result);
            return result.ChangeCEFARType<IWalletModel, WalletModel>();
        }

        /// <summary>Sets wallet entry as default.</summary>
        /// <param name="entryID">Identifier for the entry.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private async Task<CEFActionResponse> SetWalletEntryAsDefault(int entryID)
        {
            var result = await Workflows.Wallets.SetWalletEntryAsDefaultAsync(
                    userID: CurrentUserIDOrThrow401,
                    entryID: entryID,
                    provider: await VerifyPaymentProviderSupportsWallet().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            return result;
        }

        /// <summary>Deactivate wallet entry.</summary>
        /// <param name="entryID">Identifier for the entry.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private async Task<CEFActionResponse> DeactivateWalletEntry(int entryID)
        {
            await CurrentUserOrThrow401Async().ConfigureAwait(false);
            return await Workflows.Wallets.DeactivateWalletEntryAsync(
                    entryID,
                    await VerifyPaymentProviderSupportsWallet().ConfigureAwait(false),
                    null)
                .ConfigureAwait(false);
        }
    }
}
