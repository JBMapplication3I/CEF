/**
 * @file _api/cvApi.Sampling.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Endpoints generated based on C# routes.
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Storefront/React/src/_api/
 */

import axios from "../axios";

import {
	SampleRequestModel,
	StateSearchModel,
	SampleRequestStatePagedResults,
	StatusSearchModel,
	SampleRequestStatusPagedResults,
	TypeSearchModel,
	SampleRequestTypePagedResults,
} from "./cvApi._DtoClasses";

import {
	IHttpPromise
} from "./cvApi.shared";

/**
 * Use to get a list of sample request states
 * @see {@link StateSearchModel}
 * @public
 */
export interface GetSampleRequestStatesDto extends StateSearchModel {
}

/**
 * Use to get a list of sample request statuses
 * @see {@link StatusSearchModel}
 * @public
 */
export interface GetSampleRequestStatusesDto extends StatusSearchModel {
}

/**
 * Use to get a list of sample request types
 * @see {@link TypeSearchModel}
 * @public
 */
export interface GetSampleRequestTypesDto extends TypeSearchModel {
}

export class Sampling {
	/**
	 * Use to get a specific sample request
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSampleRequestByID
	 * @path <API Root>/Sampling/SampleRequest/ID/{ID}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SampleRequestModel>}
	 * @public
	 */
	GetSampleRequestByID = (id: number): IHttpPromise<SampleRequestModel> =>
		axios.get(["Sampling", "SampleRequest", "ID", id].join("/"));

	/**
	 * Use to get a list of sample request states
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSampleRequestStates
	 * @path <API Root>/Sampling/SampleRequestStates
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SampleRequestStatePagedResults>}
	 * @public
	 */
	GetSampleRequestStates = (routeParams?: GetSampleRequestStatesDto): IHttpPromise<SampleRequestStatePagedResults> =>
		axios.get(["Sampling", "SampleRequestStates"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get a list of sample request statuses
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSampleRequestStatuses
	 * @path <API Root>/Sampling/SampleRequestStatuses
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SampleRequestStatusPagedResults>}
	 * @public
	 */
	GetSampleRequestStatuses = (routeParams?: GetSampleRequestStatusesDto): IHttpPromise<SampleRequestStatusPagedResults> =>
		axios.get(["Sampling", "SampleRequestStatuses"].join("/"),
		{
			params: routeParams
		});

	/**
	 * Use to get a list of sample request types
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetSampleRequestTypes
	 * @path <API Root>/Sampling/SampleRequestTypes
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<SampleRequestTypePagedResults>}
	 * @public
	 */
	GetSampleRequestTypes = (routeParams?: GetSampleRequestTypesDto): IHttpPromise<SampleRequestTypePagedResults> =>
		axios.get(["Sampling", "SampleRequestTypes"].join("/"),
		{
			params: routeParams
		});
}
