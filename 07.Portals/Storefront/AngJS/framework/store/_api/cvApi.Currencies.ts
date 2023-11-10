/**
 * @file framework/store/_api/cvApi.Currencies.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc The Angular Service module written in TypeScript
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Storefront/AngJS/framework/store/_api/
 */
module cef.store.api {

	/**
	 * Convert a decimal value from one currency to another
	 * @public
	 */
	export interface ConvertCurrencyValueAtoBDto {
		/** The decimal value to convert */
		Value: number;
		/** The key of the starting currency (convert from) */
		KeyA: string;
		/** The key of the currency to convert to */
		KeyB: string;
	}
	/**
	 * Use to get a list of currencies
	 * @see {@link CurrencySearchModel}
	 * @public
	 */
	export interface GetCurrenciesDto extends CurrencySearchModel {
	}

	export class Currencies extends ServiceStackRoute {
		/**
		 * Convert a decimal value from one currency to another
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ConvertCurrencyValueAtoB
		 * @path <API Root>/Currencies/Convert
		 * @verb GET
		 * @returns {ng.IHttpPromise<number>}
		 * @public
		 */
		ConvertCurrencyValueAtoB = (routeParams: ConvertCurrencyValueAtoBDto) => this.$http<number>({
			url: [this.rootUrl, "Currencies", "Convert"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of currencies
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrencies
		 * @path <API Root>/Currencies/Currencies
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CurrencyPagedResults>}
		 * @public
		 */
		GetCurrencies = (routeParams?: GetCurrenciesDto) => this.$http<CurrencyPagedResults>({
			url: [this.rootUrl, "Currencies", "Currencies"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific currency by the custom key
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCurrencyByKey
		 * @path <API Root>/Currencies/Currency/Key/{Key*}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CurrencyModel>}
		 * @public
		 */
		GetCurrencyByKey = (key: string) => this.$http<CurrencyModel>({
			url: [this.rootUrl, "Currencies", "Currency", "Key", encodeURIComponent(key)].join("/"),
			method: "GET",
		});

	}
}
