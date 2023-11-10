// <copyright file="CEFAPI.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF API class</summary>
namespace Clarity.Ecommerce.MVC.Api.Callers
{
	using System.Threading.Tasks;
	using Endpoints;
	using Microsoft.JSInterop;
	using ServiceStack;

	/// <content>A CEF API.</content>
	public partial class CEFAPI
	{
		/// <summary>Provider login.</summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <param name="name">     The name.</param>
		/// <param name="request">  The request.</param>
		/// <returns>A Task{IHttpPromiseCallbackArg{AuthenticateResponse}}.</returns>
		public async Task<Core.IHttpPromiseCallbackArg<AuthenticateResponse>> LoginAsync(
			IJSRuntime jsRuntime,
			string name,
			AuthProviderLogin request)
		{
			var loginResult = await CEFService.Client.PostAsync(
				new Authenticate
				{
					provider = name,
					UserName = request.Username,
					Password = request.Password,
					RememberMe = true,
				})
				.ConfigureAwait(false);
			if (loginResult is not null)
			{
				CEFService.Client.Headers?.Set(
					Service.CEFService.SessionHeaderOptName,
					Service.CEFService.SessionOptPerm);
				CEFService.Client.Headers?.Set(
					Service.CEFService.SessionHeaderName,
					loginResult.SessionId);
				CEFService.Client.Headers?.Set(
					HttpHeaders.XUserAuthId,
					loginResult.UserId);
			}
			await CEFService.WriteCookiesAsync(jsRuntime).ConfigureAwait(false);
			return new Core.HttpPromiseCallbackArg<AuthenticateResponse>(loginResult);
		}

		/// <summary>Logout.</summary>
		/// <param name="jsRuntime">The js runtime.</param>
		/// <returns>A Task{Core.IHttpPromiseCallbackArg{AuthenticateResponse}}.</returns>
		public async Task<Core.IHttpPromiseCallbackArg<AuthenticateResponse>> LogoutAsync(IJSRuntime jsRuntime)
		{
			var logoutResult = new Core.HttpPromiseCallbackArg<AuthenticateResponse>(
				await CEFService.RequestAsync<Authenticate, AuthenticateResponse>(
					new() { provider = "logout" }));
			CEFService.Client.Headers?.Remove(Service.CEFService.SessionHeaderOptName);
			CEFService.Client.Headers?.Remove(Service.CEFService.SessionHeaderName);
			CEFService.Client.Headers?.Remove(HttpHeaders.XUserAuthId);
			await CEFService.WriteCookiesAsync(jsRuntime);
			return logoutResult;
		}

		/// <summary>Gets current master.</summary>
		/// <returns>The current master.</returns>
		/// <remarks>NOTE: This switches automatically based on which portal is calling it.</remarks>
#if BRANDADMIN
		public async Task<Models.BrandModel?> GetCurrentMasterAsync()
		{
			return (await GetCurrentBrandAdministration().ConfigureAwait(false)).data?.Result;
		}
#elif FRANCHISEADMIN
		public async Task<Models.FranchiseModel?> GetCurrentMasterAsync()
		{
			return (await GetCurrentFranchiseAdministration().ConfigureAwait(false)).data?.Result;
		}
#elif MANUFACTURERADMIN
		public async Task<Models.ManufacturerModel?> GetCurrentMasterAsync()
		{
			return (await GetCurrentManufacturerAdministration().ConfigureAwait(false)).data?.Result;
		}
#elif STOREADMIN
		public async Task<Models.StoreModel?> GetCurrentMasterAsync()
		{
			return (await GetCurrentStoreAdministration().ConfigureAwait(false)).data?.Result;
		}
#elif VENDORADMIN
		public async Task<Models.VendorModel?> GetCurrentMasterAsync()
		{
			return (await GetCurrentVendorAdministration().ConfigureAwait(false)).data?.Result;
		}
#endif
	}
}
