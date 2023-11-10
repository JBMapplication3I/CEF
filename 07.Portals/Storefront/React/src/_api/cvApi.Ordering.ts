/**
 * @file _api/cvApi.Ordering.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Endpoints generated based on C# routes.
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Storefront/React/src/_api/
 */

import axios from "../axios";

import {
	ImplementsDisplayNameBase,
	ImplementsNameBase,
	SalesOrderSearchModel,
	SalesOrderPagedResults,
	SalesOrderModel,
	SalesOrderEventSearchModel,
	SalesOrderEventPagedResults,
	StateModel,
	StateSearchModel,
	SalesOrderStatePagedResults,
	StatusModel,
	StatusSearchModel,
	SalesOrderStatusPagedResults,
	TypeModel,
	TypeSearchModel,
	SalesOrderTypePagedResults,
	SubscriptionSearchModel,
	SubscriptionModel,
	SubscriptionHistorySearchModel,
	SubscriptionHistoryPagedResults,
	ImplementsIDBase,
} from "./cvApi._DtoClasses";

import {
	CEFActionResponse,
	IHttpPromise
} from "./cvApi.shared";

/**
 * Check if this Display Name exists and return the id if it does (null if it does not)
 * @see {@link ImplementsDisplayNameBase}
 * @public
 */
export interface CheckSalesOrderStateExistsByDisplayNameDto extends ImplementsDisplayNameBase {
}

/**
 * Check if this Name exists and return the id if it does (null if it does not)
 * @see {@link ImplementsNameBase}
 * @public
 */
export interface CheckSalesOrderStateExistsByNameDto extends ImplementsNameBase {
}

/**
 * Check if this Display Name exists and return the id if it does (null if it does not)
 * @see {@link ImplementsDisplayNameBase}
 * @public
 */
export interface CheckSalesOrderStatusExistsByDisplayNameDto extends ImplementsDisplayNameBase {
}

/**
 * Check if this Name exists and return the id if it does (null if it does not)
 * @see {@link ImplementsNameBase}
 * @public
 */
export interface CheckSalesOrderStatusExistsByNameDto extends ImplementsNameBase {
}

/**
 * Check if this Display Name exists and return the id if it does (null if it does not)
 * @see {@link ImplementsDisplayNameBase}
 * @public
 */
export interface CheckSalesOrderTypeExistsByDisplayNameDto extends ImplementsDisplayNameBase {
}

/**
 * Check if this Name exists and return the id if it does (null if it does not)
 * @see {@link ImplementsNameBase}
 * @public
 */
export interface CheckSalesOrderTypeExistsByNameDto extends ImplementsNameBase {
}

/**
 * Use to get history of sales orders for the current account
 * @see {@link SalesOrderSearchModel}
 * @public
 */
export interface GetCurrentAccountSalesOrdersDto extends SalesOrderSearchModel {
}

/**
 * Use to get history of sales order for the current store
 * @see {@link SalesOrderSearchModel}
 * @public
 */
export interface GetCurrentStoreSalesOrdersDto extends SalesOrderSearchModel {
}

/**
 * Use to get history of sales order for the current user
 * @see {@link SalesOrderSearchModel}
 * @public
 */
export interface GetCurrentUserSalesOrdersDto extends SalesOrderSearchModel {
}

/**
 * Use to get a list of sales order events
 * @see {@link SalesOrderEventSearchModel}
 * @public
 */
export interface GetSalesOrderEventsDto extends SalesOrderEventSearchModel {
}

/**
 * Use to get a list of sales order states
 * @see {@link StateSearchModel}
 * @public
 */
export interface GetSalesOrderStatesDto extends StateSearchModel {
}

/**
 * Use to get a list of sales order statuses
 * @see {@link StatusSearchModel}
 * @public
 */
export interface GetSalesOrderStatusesDto extends StatusSearchModel {
}

/**
 * Use to get a list of sales order types
 * @see {@link TypeSearchModel}
 * @public
 */
export interface GetSalesOrderTypesDto extends TypeSearchModel {
}

/**
 * View subscription to update. Get by subscriptionID.
 * @see {@link SubscriptionSearchModel}
 * @public
 */
export interface GetSubscriptionBySalesOrderIDDto extends SubscriptionSearchModel {
}

/**
 * Use to get all on-demand subscriptions.
 * @see {@link SubscriptionHistorySearchModel}
 * @public
 */
export interface GetSubscriptionHistoryBySubscriptionIDDto extends SubscriptionHistorySearchModel {
	SubID: number;
}

/**
 * Send an email with the sales order receipt.
 * @see {@link ImplementsIDBase}
 * @public
 */
export interface SendSalesOrderConfirmationEmailDto extends ImplementsIDBase {
	Email?: string;
}

export class Ordering {
	/**
	 * Check if this Display Name exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderStateExistsByDisplayName
	 * @path <API Root>/Ordering/SalesOrderState/Exists/DisplayName
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderStateExistsByDisplayName = (routeParams?: CheckSalesOrderStateExistsByDisplayNameDto): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderState", "Exists", "DisplayName"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Check if this key exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderStateExistsByKey
	 * @path <API Root>/Ordering/SalesOrderState/Exists/Key/{Key*}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderStateExistsByKey = (key: string): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderState", "Exists", "Key", encodeURIComponent(key)].join("/"));

	/**
	 * Check if this Name exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderStateExistsByName
	 * @path <API Root>/Ordering/SalesOrderState/Exists/Name
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderStateExistsByName = (routeParams?: CheckSalesOrderStateExistsByNameDto): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderState", "Exists", "Name"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Check if this Display Name exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderStatusExistsByDisplayName
	 * @path <API Root>/Ordering/SalesOrderStatus/Exists/DisplayName
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderStatusExistsByDisplayName = (routeParams?: CheckSalesOrderStatusExistsByDisplayNameDto): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderStatus", "Exists", "DisplayName"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Check if this key exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderStatusExistsByKey
	 * @path <API Root>/Ordering/SalesOrderStatus/Exists/Key/{Key*}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderStatusExistsByKey = (key: string): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderStatus", "Exists", "Key", encodeURIComponent(key)].join("/"));

	/**
	 * Check if this Name exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderStatusExistsByName
	 * @path <API Root>/Ordering/SalesOrderStatus/Exists/Name
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderStatusExistsByName = (routeParams?: CheckSalesOrderStatusExistsByNameDto): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderStatus", "Exists", "Name"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Check if this Display Name exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderTypeExistsByDisplayName
	 * @path <API Root>/Ordering/SalesOrderType/Exists/DisplayName
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderTypeExistsByDisplayName = (routeParams?: CheckSalesOrderTypeExistsByDisplayNameDto): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderType", "Exists", "DisplayName"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Check if this key exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderTypeExistsByKey
	 * @path <API Root>/Ordering/SalesOrderType/Exists/Key/{Key*}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderTypeExistsByKey = (key: string): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderType", "Exists", "Key", encodeURIComponent(key)].join("/"));

	/**
	 * Check if this Name exists and return the id if it does (null if it does not)
	 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckSalesOrderTypeExistsByName
	 * @path <API Root>/Ordering/SalesOrderType/Exists/Name
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<number>}
	 * @public
	 */
	CheckSalesOrderTypeExistsByName = (routeParams?: CheckSalesOrderTypeExistsByNameDto): IHttpPromise<number> =>
		axios.get(["Ordering", "SalesOrderType", "Exists", "Name"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get history of sales orders for the current account
	 * @param {@link cef.store.api.GetCurrentAccountSalesOrdersDto} routeParams - The route parameters as a Body Object
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrentAccountSalesOrders
	 * @path <API Root>/Ordering/CurrentAccount/SalesOrders
	 * @verb POST
	 * @returns {ng.IHttpPromise<SalesOrderPagedResults>}
	 * @public
	 */
	GetCurrentAccountSalesOrders = (routeParams?: GetCurrentAccountSalesOrdersDto): IHttpPromise<SalesOrderPagedResults> =>
		axios.post(["Ordering", "CurrentAccount", "SalesOrders"].join("/"), routeParams);

	/**
	 * Use to get history of sales order for the current store
	 * @param {@link cef.store.api.GetCurrentStoreSalesOrdersDto} routeParams - The route parameters as a Body Object
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrentStoreSalesOrders
	 * @path <API Root>/Ordering/CurrentStore/SalesOrders
	 * @verb POST
	 * @returns {ng.IHttpPromise<SalesOrderPagedResults>}
	 * @public
	 */
	GetCurrentStoreSalesOrders = (routeParams?: GetCurrentStoreSalesOrdersDto): IHttpPromise<SalesOrderPagedResults> =>
		axios.post(["Ordering", "CurrentStore", "SalesOrders"].join("/"), routeParams);

	/**
	 * Use to get history of sales order for the current user
	 * @param {@link cef.store.api.GetCurrentUserSalesOrdersDto} routeParams - The route parameters as a Body Object
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrentUserSalesOrders
	 * @path <API Root>/Ordering/CurrentUser/SalesOrders
	 * @verb POST
	 * @returns {ng.IHttpPromise<SalesOrderPagedResults>}
	 * @public
	 */
	GetCurrentUserSalesOrders = (routeParams?: GetCurrentUserSalesOrdersDto): IHttpPromise<SalesOrderPagedResults> =>
		axios.post(["Ordering", "CurrentUser", "SalesOrders"].join("/"), routeParams);

	/**
	 * Use to get a specific sales order
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderByID
	 * @path <API Root>/Ordering/SalesOrder/ID/{ID}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SalesOrderModel>}
	 * @public
	 */
	GetSalesOrderByID = (id: number): IHttpPromise<SalesOrderModel> =>
		axios.get(["Ordering", "SalesOrder", "ID", id].join("/"));

	/**
	 * Use to get a list of sales order events
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderEvents
	 * @path <API Root>/Ordering/SalesOrderEvents
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SalesOrderEventPagedResults>}
	 * @public
	 */
	GetSalesOrderEvents = (routeParams?: GetSalesOrderEventsDto): IHttpPromise<SalesOrderEventPagedResults> =>
		axios.get(["Ordering", "SalesOrderEvents"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get a specific sales order state
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderStateByID
	 * @path <API Root>/Ordering/SalesOrderState/ID/{ID}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<StateModel>}
	 * @public
	 */
	GetSalesOrderStateByID = (id: number): IHttpPromise<StateModel> =>
		axios.get(["Ordering", "SalesOrderState", "ID", id].join("/"));

	/**
	 * Use to get a list of sales order states
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderStates
	 * @path <API Root>/Ordering/SalesOrderStates
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SalesOrderStatePagedResults>}
	 * @public
	 */
	GetSalesOrderStates = (routeParams?: GetSalesOrderStatesDto): IHttpPromise<SalesOrderStatePagedResults> =>
		axios.get(["Ordering", "SalesOrderStates"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get a specific sales order status
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderStatusByID
	 * @path <API Root>/Ordering/SalesOrderStatus/ID/{ID}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<StatusModel>}
	 * @public
	 */
	GetSalesOrderStatusByID = (id: number): IHttpPromise<StatusModel> =>
		axios.get(["Ordering", "SalesOrderStatus", "ID", id].join("/"));

	/**
	 * Use to get a list of sales order statuses
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderStatuses
	 * @path <API Root>/Ordering/SalesOrderStatuses
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SalesOrderStatusPagedResults>}
	 * @public
	 */
	GetSalesOrderStatuses = (routeParams?: GetSalesOrderStatusesDto): IHttpPromise<SalesOrderStatusPagedResults> =>
		axios.get(["Ordering", "SalesOrderStatuses"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get a specific sales order type
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderTypeByID
	 * @path <API Root>/Ordering/SalesOrderType/ID/{ID}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<TypeModel>}
	 * @public
	 */
	GetSalesOrderTypeByID = (id: number): IHttpPromise<TypeModel> =>
		axios.get(["Ordering", "SalesOrderType", "ID", id].join("/"));

	/**
	 * Use to get a list of sales order types
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSalesOrderTypes
	 * @path <API Root>/Ordering/SalesOrderTypes
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SalesOrderTypePagedResults>}
	 * @public
	 */
	GetSalesOrderTypes = (routeParams?: GetSalesOrderTypesDto): IHttpPromise<SalesOrderTypePagedResults> =>
		axios.get(["Ordering", "SalesOrderTypes"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get a specific sales order and check for ownership by the current Account.
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSecureSalesOrder
	 * @path <API Root>/Ordering/SecureSalesOrder/{ID}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SalesOrderModel>}
	 * @public
	 */
	GetSecureSalesOrder = (id: number): IHttpPromise<SalesOrderModel> =>
		axios.get(["Ordering", "SecureSalesOrder", id].join("/"));

	/**
	 * View subscription to update. Get by subscriptionID.
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSubscriptionBySalesOrderID
	 * @path <API Root>/Ordering/SalesOrder/Detail/Subscription
	 * @verb GET
	 * @returns {ng.IHttpPromise<SubscriptionModel>}
	 * @public
	 */
	GetSubscriptionBySalesOrderID = (routeParams?: GetSubscriptionBySalesOrderIDDto): IHttpPromise<SubscriptionModel> =>
		axios.get(["Ordering", "SalesOrder", "Detail", "Subscription"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get all on-demand subscriptions.
	 * @param {@link cef.store.api.GetSubscriptionHistoryBySubscriptionIDDto} routeParams - The route parameters as a Body Object
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSubscriptionHistoryBySubscriptionID
	 * @path <API Root>/Ordering/SalesOrder/Detail/Subscription/History
	 * @verb POST
	 * @returns {ng.IHttpPromise<SubscriptionHistoryPagedResults>}
	 * @public
	 */
	GetSubscriptionHistoryBySubscriptionID = (routeParams: GetSubscriptionHistoryBySubscriptionIDDto): IHttpPromise<SubscriptionHistoryPagedResults> =>
		axios.post(["Ordering", "SalesOrder", "Detail", "Subscription", "History"].join("/"), routeParams);

	/**
	 * Send an email with the sales order receipt.
	 * @param {@link cef.store.api.SendSalesOrderConfirmationEmailDto} routeParams - The route parameters as a Body Object
	 * @generatedByCSharpType Clarity.Ecommerce.Service.SendSalesOrderConfirmationEmail
	 * @path <API Root>/Ordering/SalesOrder/SendReceiptEmail
	 * @verb POST
	 * @returns {ng.IHttpPromise<CEFActionResponse>}
	 * @public
	 */
	SendSalesOrderConfirmationEmail = (routeParams?: SendSalesOrderConfirmationEmailDto): IHttpPromise<CEFActionResponse> =>
		axios.post(["Ordering", "SalesOrder", "SendReceiptEmail"].join("/"), routeParams);
}
