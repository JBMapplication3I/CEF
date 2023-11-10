/**
 * @file framework/admin/_api/cvApi.Badges.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc The Angular Service module written in TypeScript
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Admin/AngJS/framework/admin/_api/
 */
module cef.admin.api {

	/**
	 * Use to create a new badge.
	 * @see {@link BadgeModel}
	 * @public
	 */
	export interface CreateBadgeDto extends BadgeModel {
	}
	/**
	 * Use to create a new badge image.
	 * @see {@link BadgeImageModel}
	 * @public
	 */
	export interface CreateBadgeImageDto extends BadgeImageModel {
	}
	/**
	 * Use to create a new badge image type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface CreateBadgeImageTypeDto extends TypeModel {
	}
	/**
	 * Use to create a new badge type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface CreateBadgeTypeDto extends TypeModel {
	}
	/**
	 * Use to get a list of badge images
	 * @see {@link BadgeImageSearchModel}
	 * @public
	 */
	export interface GetBadgeImagesDto extends BadgeImageSearchModel {
	}
	/**
	 * Use to get a list of badge image types
	 * @see {@link TypeSearchModel}
	 * @public
	 */
	export interface GetBadgeImageTypesDto extends TypeSearchModel {
	}
	/**
	 * Use to get a list of badges
	 * @see {@link BadgeSearchModel}
	 * @public
	 */
	export interface GetBadgesDto extends BadgeSearchModel {
	}
	/**
	 * Use to get a list of badge types
	 * @see {@link TypeSearchModel}
	 * @public
	 */
	export interface GetBadgeTypesDto extends TypeSearchModel {
	}
	/**
	 * Use to update an existing badge.
	 * @see {@link BadgeModel}
	 * @public
	 */
	export interface UpdateBadgeDto extends BadgeModel {
	}
	/**
	 * Use to update an existing badge image.
	 * @see {@link BadgeImageModel}
	 * @public
	 */
	export interface UpdateBadgeImageDto extends BadgeImageModel {
	}
	/**
	 * Use to update an existing badge image type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface UpdateBadgeImageTypeDto extends TypeModel {
	}
	/**
	 * Use to update an existing badge type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface UpdateBadgeTypeDto extends TypeModel {
	}

	export class Badges extends ServiceStackRoute {
		/**
		 * Empties the server-side data cache for all badge calls.
		 * @param {@link cef.admin.api.ClearBadgeCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearBadgeCache
		 * @path <API Root>/Badges/Badge/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearBadgeCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Badges", "Badge", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Empties the server-side data cache for all badge image calls.
		 * @param {@link cef.admin.api.ClearBadgeImageCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearBadgeImageCache
		 * @path <API Root>/Badges/BadgeImage/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearBadgeImageCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Badges", "BadgeImage", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Empties the server-side data cache for all badge image type calls.
		 * @param {@link cef.admin.api.ClearBadgeImageTypeCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearBadgeImageTypeCache
		 * @path <API Root>/Badges/BadgeImageType/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearBadgeImageTypeCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Badges", "BadgeImageType", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Empties the server-side data cache for all badge type calls.
		 * @param {@link cef.admin.api.ClearBadgeTypeCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearBadgeTypeCache
		 * @path <API Root>/Badges/BadgeType/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearBadgeTypeCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Badges", "BadgeType", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Use to create a new badge.
		 * @param {@link cef.admin.api.CreateBadgeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateBadge
		 * @path <API Root>/Badges/Badge/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateBadge = (routeParams?: CreateBadgeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "Badge", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to create a new badge image.
		 * @param {@link cef.admin.api.CreateBadgeImageDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateBadgeImage
		 * @path <API Root>/Badges/BadgeImage/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateBadgeImage = (routeParams?: CreateBadgeImageDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "BadgeImage", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to create a new badge image type.
		 * @param {@link cef.admin.api.CreateBadgeImageTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateBadgeImageType
		 * @path <API Root>/Badges/BadgeImageType/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateBadgeImageType = (routeParams?: CreateBadgeImageTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "BadgeImageType", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to create a new badge type.
		 * @param {@link cef.admin.api.CreateBadgeTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateBadgeType
		 * @path <API Root>/Badges/BadgeType/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateBadgeType = (routeParams?: CreateBadgeTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "BadgeType", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Deactivate a specific badge from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateBadgeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateBadgeByID
		 * @path <API Root>/Badges/Badge/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateBadgeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "Badge", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Deactivate a specific badge image from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateBadgeImageByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateBadgeImageByID
		 * @path <API Root>/Badges/BadgeImage/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateBadgeImageByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeImage", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Deactivate a specific badge image type from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateBadgeImageTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateBadgeImageTypeByID
		 * @path <API Root>/Badges/BadgeImageType/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateBadgeImageTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeImageType", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Deactivate a specific badge type from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateBadgeTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateBadgeTypeByID
		 * @path <API Root>/Badges/BadgeType/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateBadgeTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeType", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Removes a specific badge from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteBadgeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteBadgeByID
		 * @path <API Root>/Badges/Badge/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteBadgeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "Badge", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Removes a specific badge image from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteBadgeImageByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteBadgeImageByID
		 * @path <API Root>/Badges/BadgeImage/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteBadgeImageByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeImage", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Removes a specific badge image type from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteBadgeImageTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteBadgeImageTypeByID
		 * @path <API Root>/Badges/BadgeImageType/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteBadgeImageTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeImageType", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Removes a specific badge type from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteBadgeTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteBadgeTypeByID
		 * @path <API Root>/Badges/BadgeType/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteBadgeTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeType", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Use to get a specific badge
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadgeByID
		 * @path <API Root>/Badges/Badge/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<BadgeModel>}
		 * @public
		 */
		GetBadgeByID = (id: number) => this.$http<BadgeModel>({
			url: [this.rootUrl, "Badges", "Badge", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a specific badge image
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadgeImageByID
		 * @path <API Root>/Badges/BadgeImage/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<BadgeImageModel>}
		 * @public
		 */
		GetBadgeImageByID = (id: number) => this.$http<BadgeImageModel>({
			url: [this.rootUrl, "Badges", "BadgeImage", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of badge images
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadgeImages
		 * @path <API Root>/Badges/BadgeImages
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<BadgeImagePagedResults>}
		 * @public
		 */
		GetBadgeImages = (routeParams?: GetBadgeImagesDto) => this.$http<BadgeImagePagedResults>({
			url: [this.rootUrl, "Badges", "BadgeImages"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific badge image type
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadgeImageTypeByID
		 * @path <API Root>/Badges/BadgeImageType/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<TypeModel>}
		 * @public
		 */
		GetBadgeImageTypeByID = (id: number) => this.$http<TypeModel>({
			url: [this.rootUrl, "Badges", "BadgeImageType", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of badge image types
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadgeImageTypes
		 * @path <API Root>/Badges/BadgeImageTypes
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<BadgeImageTypePagedResults>}
		 * @public
		 */
		GetBadgeImageTypes = (routeParams?: GetBadgeImageTypesDto) => this.$http<BadgeImageTypePagedResults>({
			url: [this.rootUrl, "Badges", "BadgeImageTypes"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a list of badges
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadges
		 * @path <API Root>/Badges/Badges
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<BadgePagedResults>}
		 * @public
		 */
		GetBadges = (routeParams?: GetBadgesDto) => this.$http<BadgePagedResults>({
			url: [this.rootUrl, "Badges", "Badges"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific badge type
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadgeTypeByID
		 * @path <API Root>/Badges/BadgeType/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<TypeModel>}
		 * @public
		 */
		GetBadgeTypeByID = (id: number) => this.$http<TypeModel>({
			url: [this.rootUrl, "Badges", "BadgeType", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of badge types
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetBadgeTypes
		 * @path <API Root>/Badges/BadgeTypes
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<BadgeTypePagedResults>}
		 * @public
		 */
		GetBadgeTypes = (routeParams?: GetBadgeTypesDto) => this.$http<BadgeTypePagedResults>({
			url: [this.rootUrl, "Badges", "BadgeTypes"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Reactivate a specific badge from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateBadgeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateBadgeByID
		 * @path <API Root>/Badges/Badge/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateBadgeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "Badge", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Reactivate a specific badge image from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateBadgeImageByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateBadgeImageByID
		 * @path <API Root>/Badges/BadgeImage/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateBadgeImageByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeImage", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Reactivate a specific badge image type from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateBadgeImageTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateBadgeImageTypeByID
		 * @path <API Root>/Badges/BadgeImageType/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateBadgeImageTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeImageType", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Reactivate a specific badge type from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateBadgeTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateBadgeTypeByID
		 * @path <API Root>/Badges/BadgeType/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateBadgeTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Badges", "BadgeType", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Use to update an existing badge.
		 * @param {@link cef.admin.api.UpdateBadgeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateBadge
		 * @path <API Root>/Badges/Badge/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateBadge = (routeParams?: UpdateBadgeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "Badge", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to update an existing badge image.
		 * @param {@link cef.admin.api.UpdateBadgeImageDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateBadgeImage
		 * @path <API Root>/Badges/BadgeImage/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateBadgeImage = (routeParams?: UpdateBadgeImageDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "BadgeImage", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to update an existing badge image type.
		 * @param {@link cef.admin.api.UpdateBadgeImageTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateBadgeImageType
		 * @path <API Root>/Badges/BadgeImageType/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateBadgeImageType = (routeParams?: UpdateBadgeImageTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "BadgeImageType", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to update an existing badge type.
		 * @param {@link cef.admin.api.UpdateBadgeTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateBadgeType
		 * @path <API Root>/Badges/BadgeType/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateBadgeType = (routeParams?: UpdateBadgeTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Badges", "BadgeType", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

	}
}
