/**
 * @file framework/store/_api/cvApi.Inventory.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc The Angular Service module written in TypeScript
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Storefront/AngJS/framework/store/_api/
 */
module cef.store.api {

	/**
	 * Get InventoryLocationRegions by InventoryLocation ID
	 * @see {@link InventoryLocationRegionSearchModel}
	 * @public
	 */
	export interface GetInventoryLocationRegionsByInventoryLocationIDDto extends InventoryLocationRegionSearchModel {
	}
	/**
	 * Use to get a list of inventory locations
	 * @see {@link InventoryLocationSearchModel}
	 * @public
	 */
	export interface GetInventoryLocationsDto extends InventoryLocationSearchModel {
	}
	/**
	 * Use to get a list of inventory location sections
	 * @see {@link InventoryLocationSectionSearchModel}
	 * @public
	 */
	export interface GetInventoryLocationSectionsDto extends InventoryLocationSectionSearchModel {
	}

	export class Inventory extends ServiceStackRoute {
		/**
		 * Get InventoryLocationRegions by InventoryLocation ID
		 * @param {@link cef.store.api.GetInventoryLocationRegionsByInventoryLocationIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetInventoryLocationRegionsByInventoryLocationID
		 * @path <API Root>/Inventory/InventoryLocation/Regions/ByInventoryLocationID
		 * @verb POST
		 * @returns {ng.IHttpPromise<InventoryLocationRegionPagedResults>}
		 * @public
		 */
		GetInventoryLocationRegionsByInventoryLocationID = (routeParams?: GetInventoryLocationRegionsByInventoryLocationIDDto) => this.$http<InventoryLocationRegionPagedResults>({
			url: [this.rootUrl, "Inventory", "InventoryLocation", "Regions", "ByInventoryLocationID"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to get a list of inventory locations
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetInventoryLocations
		 * @path <API Root>/Inventory/InventoryLocations
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<InventoryLocationPagedResults>}
		 * @public
		 */
		GetInventoryLocations = (routeParams?: GetInventoryLocationsDto) => this.$http<InventoryLocationPagedResults>({
			url: [this.rootUrl, "Inventory", "InventoryLocations"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of inventory location sections
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetInventoryLocationSections
		 * @path <API Root>/Inventory/InventoryLocationSections
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<InventoryLocationSectionPagedResults>}
		 * @public
		 */
		GetInventoryLocationSections = (routeParams?: GetInventoryLocationSectionsDto) => this.$http<InventoryLocationSectionPagedResults>({
			url: [this.rootUrl, "Inventory", "InventoryLocationSections"].join("/"),
			method: "GET",
			params: routeParams
		});

	}
}
