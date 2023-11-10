/**
 * @file framework/store/_api/cvApi.Manufacturers.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc The Angular Service module written in TypeScript
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Storefront/AngJS/framework/store/_api/
 */
module cef.store.api {

	/**
	 * Check if this Name exists and return the id if it does (null if it does not)
	 * @see {@link ImplementsNameBase}
	 * @public
	 */
	export interface CheckManufacturerExistsByNameDto extends ImplementsNameBase {
	}
	/**
	 * Use to get a list of manufacturers
	 * @see {@link ManufacturerSearchModel}
	 * @public
	 */
	export interface GetManufacturersDto extends ManufacturerSearchModel {
	}

	export class Manufacturers extends ServiceStackRoute {
		/**
		 * Check if this ID exists and return the id if it does (null if it does not)
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckManufacturerExistsByID
		 * @path <API Root>/Manufacturers/Manufacturer/Exists/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<number>}
		 * @public
		 */
		CheckManufacturerExistsByID = (id: number) => this.$http<number>({
			url: [this.rootUrl, "Manufacturers", "Manufacturer", "Exists", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Check if this key exists and return the id if it does (null if it does not)
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckManufacturerExistsByKey
		 * @path <API Root>/Manufacturers/Manufacturer/Exists/Key/{Key*}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<number>}
		 * @public
		 */
		CheckManufacturerExistsByKey = (key: string) => this.$http<number>({
			url: [this.rootUrl, "Manufacturers", "Manufacturer", "Exists", "Key", encodeURIComponent(key)].join("/"),
			method: "GET",
		});

		/**
		 * Check if this Name exists and return the id if it does (null if it does not)
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CheckManufacturerExistsByName
		 * @path <API Root>/Manufacturers/Manufacturer/Exists/Name
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<number>}
		 * @public
		 */
		CheckManufacturerExistsByName = (routeParams?: CheckManufacturerExistsByNameDto) => this.$http<number>({
			url: [this.rootUrl, "Manufacturers", "Manufacturer", "Exists", "Name"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific manufacturer
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetManufacturerByID
		 * @path <API Root>/Manufacturers/Manufacturer/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<ManufacturerModel>}
		 * @public
		 */
		GetManufacturerByID = (id: number) => this.$http<ManufacturerModel>({
			url: [this.rootUrl, "Manufacturers", "Manufacturer", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of manufacturers
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetManufacturers
		 * @path <API Root>/Manufacturers/Manufacturers
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<ManufacturerPagedResults>}
		 * @public
		 */
		GetManufacturers = (routeParams?: GetManufacturersDto) => this.$http<ManufacturerPagedResults>({
			url: [this.rootUrl, "Manufacturers", "Manufacturers"].join("/"),
			method: "GET",
			params: routeParams
		});

	}
}
