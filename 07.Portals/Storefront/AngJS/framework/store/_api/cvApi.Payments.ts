/**
 * @file framework/store/_api/cvApi.Payments.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc The Angular Service module written in TypeScript
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Storefront/AngJS/framework/store/_api/
 */
module cef.store.api {

	/**
	 * Use to add an entry to the current user's wallet
	 * @see {@link WalletModel}
	 * @public
	 */
	export interface CreateCurrentUserWalletEntryDto extends WalletModel {
	}
	/**
	 * Use to get subscriptions by Current User as a dropdown-appropriate listing.
	 * @see {@link SubscriptionSearchModel}
	 * @public
	 */
	export interface GetCurrentUserSubscriptionsDto extends SubscriptionSearchModel {
	}
	/**
	 * Use to get a list of membership levels
	 * @see {@link MembershipLevelSearchModel}
	 * @public
	 */
	export interface GetMembershipLevelsDto extends MembershipLevelSearchModel {
	}
	/**
	 * Use to get a list of memberships
	 * @see {@link MembershipSearchModel}
	 * @public
	 */
	export interface GetMembershipsDto extends MembershipSearchModel {
	}
	/**
	 * Use to get a list of payment methods
	 * @see {@link PaymentMethodSearchModel}
	 * @public
	 */
	export interface GetPaymentMethodsDto extends PaymentMethodSearchModel {
	}
	/**
	 * Retrieve a payments provider authentication token.
	 * @public
	 */
	export interface GetPaymentsProviderAuthenticationTokenDto {
		PaymentsProviderName: string;
	}
	/**
	 * Use to get a list of payment statuses
	 * @see {@link StatusSearchModel}
	 * @public
	 */
	export interface GetPaymentStatusesDto extends StatusSearchModel {
	}
	/**
	 * Retrieve a payments provider transaction report by id or date range.
	 * @public
	 */
	export interface GetPaymentTransactionReportDto {
		StartDate?: Date;
		EndDate?: Date;
		TransactionID?: string;
	}
	/**
	 * Use to get a list of payment types
	 * @see {@link TypeSearchModel}
	 * @public
	 */
	export interface GetPaymentTypesDto extends TypeSearchModel {
	}
	/**
	 * Use to get a list of repeat types
	 * @see {@link RepeatTypeSearchModel}
	 * @public
	 */
	export interface GetRepeatTypesDto extends RepeatTypeSearchModel {
	}
	/**
	 * Use to get a list of subscription statuses
	 * @see {@link StatusSearchModel}
	 * @public
	 */
	export interface GetSubscriptionStatusesDto extends StatusSearchModel {
	}
	/**
	 * Use to get a list of subscription types
	 * @see {@link SubscriptionTypeSearchModel}
	 * @public
	 */
	export interface GetSubscriptionTypesDto extends SubscriptionTypeSearchModel {
	}
	/**
	 * WARNING! There is no Summary value on this endpoint, please ask a Developer to add one
	 * @public
	 */
	export interface GetSurchargeForInvoicePaymentDto {
		BIN?: string;
		BillingContactID: number;
		InvoiceIDs?: Set<number>;
		TotalAmount: number;
	}
	/**
	 * WARNING! There is no Summary value on this endpoint, please ask a Developer to add one
	 * @public
	 */
	export interface PayoneerOrderEventWebhookReturnDto {
		api_key?: ApiKeyWebhookReturn; // Name format overridden
		event?: OrderEventWebhookReturn; // Name format overridden
		order?: OrderWebhookReturn; // Name format overridden
		account?: AccountWebhookReturn; // Name format overridden
	}
	/**
	 * Use to update an entry in the current user's wallet
	 * @see {@link WalletModel}
	 * @public
	 */
	export interface UpdateCurrentUserWalletEntryDto extends WalletModel {
	}
	/**
	 * Use to get all on-demand subscriptions.
	 * @see {@link SubscriptionSearchModel}
	 * @public
	 */
	export interface ViewOnDemandSubscriptionsDto extends SubscriptionSearchModel {
		UID: number;
	}

	export class Payments extends ServiceStackRoute {
		/**
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.AssignPayoneerAccountUserForCurrentStore
		 * @path <API Root>/Payments/CurrentStore/AssignPayoneerAccountUser
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		AssignPayoneerAccountUserForCurrentStore = () => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Payments", "CurrentStore", "AssignPayoneerAccountUser"].join("/"),
			method: "GET",
		});

		/**
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.AssignPayoneerAccountUserForCurrentUser
		 * @path <API Root>/Payments/CurrentUser/AssignPayoneerAccountUser
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		AssignPayoneerAccountUserForCurrentUser = () => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Payments", "CurrentUser", "AssignPayoneerAccountUser"].join("/"),
			method: "GET",
		});

		/**
		 * @param {@link cef.store.api.CreateAPayoneerAccountForCurrentStoreDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.CreateAPayoneerAccountForCurrentStore
		 * @path <API Root>/Payments/CurrentStore/CreateAPayoneerAccount
		 * @verb POST
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		CreateAPayoneerAccountForCurrentStore = () => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "CurrentStore", "CreateAPayoneerAccount"].join("/"),
			method: "POST",
		});

		/**
		 * @param {@link cef.store.api.CreateAPayoneerAccountForCurrentUserDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.CreateAPayoneerAccountForCurrentUser
		 * @path <API Root>/Payments/CurrentUser/CreateAPayoneerAccount
		 * @verb POST
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		CreateAPayoneerAccountForCurrentUser = () => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "CurrentUser", "CreateAPayoneerAccount"].join("/"),
			method: "POST",
		});

		/**
		 * Use to add an entry to the current user's wallet
		 * @param {@link cef.store.api.CreateCurrentUserWalletEntryDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateCurrentUserWalletEntry
		 * @path <API Root>/Payments/Wallet/CurrentUser/Entry/Create
		 * @verb POST
		 * @returns {ng.IHttpPromise<CEFActionResponseT<WalletModel>>}
		 * @public
		 */
		CreateCurrentUserWalletEntry = (routeParams?: CreateCurrentUserWalletEntryDto) => this.$http<CEFActionResponseT<WalletModel>>({
			url: [this.rootUrl, "Payments", "Wallet", "CurrentUser", "Entry", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to deactivate a specific entry in the current user's wallet
		 * @param {@link cef.store.api.CurrentUserMarkWalletEntryAsDefaultDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CurrentUserMarkWalletEntryAsDefault
		 * @path <API Root>/Payments/Wallet/CurrentUser/Entry/SetAsDefault/ID/{ID}
		 * @verb PATCH
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		CurrentUserMarkWalletEntryAsDefault = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Payments", "Wallet", "CurrentUser", "Entry", "SetAsDefault", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Use to deactivate a specific entry in the current user's wallet
		 * @param {@link cef.store.api.DeactivateCurrentUserWalletEntryDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateCurrentUserWalletEntry
		 * @path <API Root>/Payments/Wallet/CurrentUser/Entry/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateCurrentUserWalletEntry = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Payments", "Wallet", "CurrentUser", "Entry", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.GetAuthedURLForBuyerToSetUpPaymentAccounts
		 * @path <API Root>/Payments/CurrentStore/GetPayoneerAuthedURLForBuyerToSetUpPaymentAccounts
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		GetAuthedURLForBuyerToSetUpPaymentAccounts = () => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "CurrentStore", "GetPayoneerAuthedURLForBuyerToSetUpPaymentAccounts"].join("/"),
			method: "GET",
		});

		/**
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.GetAuthedURLForStoreOwnerToAddATrackingNumberToTheEscrowOrder
		 * @path <API Root>/Payments/GetPayoneerAuthedURLForStoreOwnerToAddATrackingNumberToTheEscrowOrder/OrderID/{OrderID}
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		GetAuthedURLForStoreOwnerToAddATrackingNumberToTheEscrowOrder = (orderID: number) => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "GetPayoneerAuthedURLForStoreOwnerToAddATrackingNumberToTheEscrowOrder", "OrderID", orderID].join("/"),
			method: "GET",
		});

		/**
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.GetAuthedURLForStoreOwnerToSetUpPayoutAccountsForCurrentStore
		 * @path <API Root>/Payments/CurrentStore/GetPayoneerAuthedURLForStoreOwnerToSetUpPayoutAccounts
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		GetAuthedURLForStoreOwnerToSetUpPayoutAccountsForCurrentStore = () => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "CurrentStore", "GetPayoneerAuthedURLForStoreOwnerToSetUpPayoutAccounts"].join("/"),
			method: "GET",
		});

		/**
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.GetAuthedURLToReleaseFundsForEscrowOrder
		 * @path <API Root>/Payments/GetPayoneerAuthedURLToReleaseFundsForEscrowOrder/OrderID/{OrderID}
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		GetAuthedURLToReleaseFundsForEscrowOrder = (orderID: number) => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "GetPayoneerAuthedURLToReleaseFundsForEscrowOrder", "OrderID", orderID].join("/"),
			method: "GET",
		});

		/**
		 * Use to get subscriptions by Current User as a dropdown-appropriate listing.
		 * @param {@link cef.store.api.GetCurrentUserSubscriptionsDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrentUserSubscriptions
		 * @path <API Root>/Payments/CurrentUser/Subscriptions
		 * @verb POST
		 * @returns {ng.IHttpPromise<SubscriptionPagedResults>}
		 * @public
		 */
		GetCurrentUserSubscriptions = (routeParams?: GetCurrentUserSubscriptionsDto) => this.$http<SubscriptionPagedResults>({
			url: [this.rootUrl, "Payments", "CurrentUser", "Subscriptions"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to get the wallet for the current user
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrentUserWallet
		 * @path <API Root>/Payments/Wallet/CurrentUser/List
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<Array<WalletModel>>>}
		 * @public
		 */
		GetCurrentUserWallet = () => this.$http<CEFActionResponseT<Array<WalletModel>>>({
			url: [this.rootUrl, "Payments", "Wallet", "CurrentUser", "List"].join("/"),
			method: "GET",
		});

		/**
		 * Use to get an entry from the current user's wallet
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrentUserWalletEntryByID
		 * @path <API Root>/Payments/Wallet/CurrentUser/Entry/ByID/{ID}
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<WalletModel>>}
		 * @public
		 */
		GetCurrentUserWalletEntryByID = (id: number) => this.$http<CEFActionResponseT<WalletModel>>({
			url: [this.rootUrl, "Payments", "Wallet", "CurrentUser", "Entry", "ByID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of membership levels
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetMembershipLevels
		 * @path <API Root>/Payments/MembershipLevels
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<MembershipLevelPagedResults>}
		 * @public
		 */
		GetMembershipLevels = (routeParams?: GetMembershipLevelsDto) => this.$http<MembershipLevelPagedResults>({
			url: [this.rootUrl, "Payments", "MembershipLevels"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of memberships
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetMemberships
		 * @path <API Root>/Payments/Memberships
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<MembershipPagedResults>}
		 * @public
		 */
		GetMemberships = (routeParams?: GetMembershipsDto) => this.$http<MembershipPagedResults>({
			url: [this.rootUrl, "Payments", "Memberships"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * @param {@link cef.store.api.GetPaymentInstructionsUrlForEscrowOrderDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.GetPaymentInstructionsUrlForEscrowOrder
		 * @path <API Root>/Payments/GetPaymentInstructionsUrlForEscrowOrder/{OrderID}/{PayoneerAccountID}/{PayoneerCustomerID}
		 * @verb POST
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		GetPaymentInstructionsUrlForEscrowOrder = (orderID: number, payoneerAccountID: number, payoneerCustomerID: number) => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "GetPaymentInstructionsUrlForEscrowOrder", orderID, payoneerAccountID, payoneerCustomerID].join("/"),
			method: "POST",
		});

		/**
		 * Use to get a list of payment methods
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetPaymentMethods
		 * @path <API Root>/Payments/PaymentMethods
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<PaymentMethodPagedResults>}
		 * @public
		 */
		GetPaymentMethods = (routeParams?: GetPaymentMethodsDto) => this.$http<PaymentMethodPagedResults>({
			url: [this.rootUrl, "Payments", "PaymentMethods"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Retrieve a payments provider authentication token.
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetPaymentsProviderAuthenticationToken
		 * @path <API Root>/Payments/GetPaymentsProviderAuthenticationToken
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<string>>}
		 * @public
		 */
		GetPaymentsProviderAuthenticationToken = (routeParams: GetPaymentsProviderAuthenticationTokenDto) => this.$http<CEFActionResponseT<string>>({
			url: [this.rootUrl, "Payments", "GetPaymentsProviderAuthenticationToken"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of payment statuses
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetPaymentStatuses
		 * @path <API Root>/Payments/PaymentStatuses
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<PaymentStatusPagedResults>}
		 * @public
		 */
		GetPaymentStatuses = (routeParams?: GetPaymentStatusesDto) => this.$http<PaymentStatusPagedResults>({
			url: [this.rootUrl, "Payments", "PaymentStatuses"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Retrieve a payments provider transaction report by id or date range.
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetPaymentTransactionReport
		 * @path <API Root>/Payments/GetPaymentTransactionsReport
		 * @verb GET
		 * @returns {ng.IHttpPromise<CEFActionResponseT<TransactionResponse>>}
		 * @public
		 */
		GetPaymentTransactionReport = (routeParams?: GetPaymentTransactionReportDto) => this.$http<CEFActionResponseT<TransactionResponse>>({
			url: [this.rootUrl, "Payments", "GetPaymentTransactionsReport"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of payment types
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetPaymentTypes
		 * @path <API Root>/Payments/PaymentTypes
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<PaymentTypePagedResults>}
		 * @public
		 */
		GetPaymentTypes = (routeParams?: GetPaymentTypesDto) => this.$http<PaymentTypePagedResults>({
			url: [this.rootUrl, "Payments", "PaymentTypes"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of repeat types
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetRepeatTypes
		 * @path <API Root>/Payments/RepeatTypes
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<RepeatTypePagedResults>}
		 * @public
		 */
		GetRepeatTypes = (routeParams?: GetRepeatTypesDto) => this.$http<RepeatTypePagedResults>({
			url: [this.rootUrl, "Payments", "RepeatTypes"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific subscription
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSubscriptionByID
		 * @path <API Root>/Payments/Subscription/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<SubscriptionModel>}
		 * @public
		 */
		GetSubscriptionByID = (id: number) => this.$http<SubscriptionModel>({
			url: [this.rootUrl, "Payments", "Subscription", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of subscription statuses
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSubscriptionStatuses
		 * @path <API Root>/Payments/SubscriptionStatuses
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<SubscriptionStatusPagedResults>}
		 * @public
		 */
		GetSubscriptionStatuses = (routeParams?: GetSubscriptionStatusesDto) => this.$http<SubscriptionStatusPagedResults>({
			url: [this.rootUrl, "Payments", "SubscriptionStatuses"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of subscription types
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSubscriptionTypes
		 * @path <API Root>/Payments/SubscriptionTypes
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<SubscriptionTypePagedResults>}
		 * @public
		 */
		GetSubscriptionTypes = (routeParams?: GetSubscriptionTypesDto) => this.$http<SubscriptionTypePagedResults>({
			url: [this.rootUrl, "Payments", "SubscriptionTypes"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * @param {@link cef.store.api.GetSurchargeForInvoicePaymentDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Surcharges.GetSurchargeForInvoicePayment
		 * @path <API Root>/Payments/SurchargeForInvoices
		 * @verb POST
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		GetSurchargeForInvoicePayment = (routeParams: GetSurchargeForInvoicePaymentDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Payments", "SurchargeForInvoices"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * @param {@link cef.store.api.PayoneerOrderEventWebhookReturnDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Providers.Payments.Payoneer.PayoneerOrderEventWebhookReturn
		 * @path <API Root>/Payments/Payoneer/OrderEventWebhooks
		 * @verb POST
		 * @returns {ng.IHttpPromise<void>}
		 * @public
		 */
		PayoneerOrderEventWebhookReturn = (routeParams?: PayoneerOrderEventWebhookReturnDto) => this.$http<void>({
			url: [this.rootUrl, "Payments", "Payoneer", "OrderEventWebhooks"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to get all on-demand subscriptions.
		 * @param {@link cef.store.api.RefillOnDemandSubscriptionDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.RefillOnDemandSubscription
		 * @path <API Root>/Payments/CurrentUser/Subscriptions/OnDemand/{ID}
		 * @verb PATCH
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		RefillOnDemandSubscription = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Payments", "CurrentUser", "Subscriptions", "OnDemand", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Use to update an entry in the current user's wallet
		 * @param {@link cef.store.api.UpdateCurrentUserWalletEntryDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateCurrentUserWalletEntry
		 * @path <API Root>/Payments/Wallet/CurrentUser/Entry/Update
		 * @verb PUT
		 * @returns {ng.IHttpPromise<CEFActionResponseT<WalletModel>>}
		 * @public
		 */
		UpdateCurrentUserWalletEntry = (routeParams?: UpdateCurrentUserWalletEntryDto) => this.$http<CEFActionResponseT<WalletModel>>({
			url: [this.rootUrl, "Payments", "Wallet", "CurrentUser", "Entry", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to get all on-demand subscriptions.
		 * @param {@link cef.store.api.ViewOnDemandSubscriptionsDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ViewOnDemandSubscriptions
		 * @path <API Root>/Payments/CurrentUser/Subscriptions/OnDemand
		 * @verb POST
		 * @returns {ng.IHttpPromise<SubscriptionPagedResults>}
		 * @public
		 */
		ViewOnDemandSubscriptions = (routeParams: ViewOnDemandSubscriptionsDto) => this.$http<SubscriptionPagedResults>({
			url: [this.rootUrl, "Payments", "CurrentUser", "Subscriptions", "OnDemand"].join("/"),
			method: "POST",
			data: routeParams
		});

	}
}
