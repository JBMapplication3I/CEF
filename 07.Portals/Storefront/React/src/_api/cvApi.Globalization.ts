/**
 * @file _api/cvApi.Globalization.ts
 * @author Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
 * @desc Endpoints generated based on C# routes.
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Storefront/React/src/_api/
 */

import axios from "../axios";

import {
	LanguageModel,
	LanguageSearchModel,
	LanguagePagedResults,
} from "./cvApi._DtoClasses";

import {
	IHttpPromise
} from "./cvApi.shared";

/**
 * Use to get a list of languages
 * @see {@link LanguageSearchModel}
 * @public
 */
export interface GetLanguagesDto extends LanguageSearchModel {
}

export class Globalization {
	/**
	 * Use to get a specific language by the custom key
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetLanguageByKey
	 * @path <API Root>/Globalization/Language/Key/{Key*}
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<LanguageModel>}
	 * @public
	 */
	GetLanguageByKey = (key: string): IHttpPromise<LanguageModel> =>
		axios.get(["Globalization", "Language", "Key", encodeURIComponent(key)].join("/"));
	
	/**
	 * Use to get a list of languages
	 * @generatedByCSharpType Clarity.Ecommerce.Service.GetLanguages
	 * @path <API Root>/Globalization/Languages
	 * @verb GET
	 * @priority 1
	 * @returns {ng.IHttpPromise<LanguagePagedResults>}
	 * @public
	 */
	GetLanguages = (routeParams?: GetLanguagesDto): IHttpPromise<LanguagePagedResults> =>
		axios.get(["Globalization", "Languages"].join("/"),
		{
			params: routeParams
		});
}
