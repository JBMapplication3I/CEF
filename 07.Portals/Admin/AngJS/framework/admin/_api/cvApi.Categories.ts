/**
 * @file framework/admin/_api/cvApi.Categories.ts
 * @author Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
 * @desc The Angular Service module written in TypeScript
 * @remarks This file was auto-generated by cvApi.tt in 07.Portals/Admin/AngJS/framework/admin/_api/
 */
module cef.admin.api {

	/**
	 * Use to create a new category.
	 * @see {@link CategoryModel}
	 * @public
	 */
	export interface CreateCategoryDto extends CategoryModel {
	}
	/**
	 * Use to create a new category file.
	 * @see {@link CategoryFileModel}
	 * @public
	 */
	export interface CreateCategoryFileDto extends CategoryFileModel {
	}
	/**
	 * Use to create a new category image.
	 * @see {@link CategoryImageModel}
	 * @public
	 */
	export interface CreateCategoryImageDto extends CategoryImageModel {
	}
	/**
	 * Use to create a new category image type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface CreateCategoryImageTypeDto extends TypeModel {
	}
	/**
	 * Use to create a new category type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface CreateCategoryTypeDto extends TypeModel {
	}
	/**
	 * Use to get a list of categories
	 * @see {@link CategorySearchModel}
	 * @public
	 */
	export interface GetCategoriesDto extends CategorySearchModel {
	}
	/**
	 * Use to get a specific category
	 * @public
	 */
	export interface GetCategoryByIDDto {
		/** When true, don't map out the Product Categories (use when this would result in a lot of unnecessary data) */
		ExcludeProductCategories?: boolean;
	}
	/**
	 * Use to get a list of category files
	 * @see {@link CategoryFileSearchModel}
	 * @public
	 */
	export interface GetCategoryFilesDto extends CategoryFileSearchModel {
	}
	/**
	 * Use to get a list of category images
	 * @see {@link CategoryImageSearchModel}
	 * @public
	 */
	export interface GetCategoryImagesDto extends CategoryImageSearchModel {
	}
	/**
	 * Use to get a list of category image types
	 * @see {@link TypeSearchModel}
	 * @public
	 */
	export interface GetCategoryImageTypesDto extends TypeSearchModel {
	}
	/**
	 * Use to get a tree of categories. Include the ParentID and set IncludeChildrenInResults to false and this can function as lazy loading for Kendo Trees.
	 * @see {@link CategorySearchModel}
	 * @public
	 */
	export interface GetCategoryTreeDto extends CategorySearchModel {
	}
	/**
	 * Use to get a list of category types
	 * @see {@link TypeSearchModel}
	 * @public
	 */
	export interface GetCategoryTypesDto extends TypeSearchModel {
	}
	/**
	 * Use to update an existing category.
	 * @see {@link CategoryModel}
	 * @public
	 */
	export interface UpdateCategoryDto extends CategoryModel {
	}
	/**
	 * Use to update an existing category file.
	 * @see {@link CategoryFileModel}
	 * @public
	 */
	export interface UpdateCategoryFileDto extends CategoryFileModel {
	}
	/**
	 * Use to update an existing category image.
	 * @see {@link CategoryImageModel}
	 * @public
	 */
	export interface UpdateCategoryImageDto extends CategoryImageModel {
	}
	/**
	 * Use to update an existing category image type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface UpdateCategoryImageTypeDto extends TypeModel {
	}
	/**
	 * Use to update an existing category type.
	 * @see {@link TypeModel}
	 * @public
	 */
	export interface UpdateCategoryTypeDto extends TypeModel {
	}

	export class Categories extends ServiceStackRoute {
		/**
		 * Empties the server-side data cache for all category calls.
		 * @param {@link cef.admin.api.ClearCategoryCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearCategoryCache
		 * @path <API Root>/Categories/Category/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearCategoryCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Categories", "Category", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Empties the server-side data cache for all category file calls.
		 * @param {@link cef.admin.api.ClearCategoryFileCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearCategoryFileCache
		 * @path <API Root>/Categories/CategoryFile/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearCategoryFileCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Categories", "CategoryFile", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Empties the server-side data cache for all category image calls.
		 * @param {@link cef.admin.api.ClearCategoryImageCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearCategoryImageCache
		 * @path <API Root>/Categories/CategoryImage/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearCategoryImageCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Categories", "CategoryImage", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Empties the server-side data cache for all category image type calls.
		 * @param {@link cef.admin.api.ClearCategoryImageTypeCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearCategoryImageTypeCache
		 * @path <API Root>/Categories/CategoryImageType/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearCategoryImageTypeCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Categories", "CategoryImageType", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Empties the server-side data cache for all category type calls.
		 * @param {@link cef.admin.api.ClearCategoryTypeCacheDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ClearCategoryTypeCache
		 * @path <API Root>/Categories/CategoryType/ClearCache
		 * @verb DELETE
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		ClearCategoryTypeCache = () => this.$http<boolean>({
			url: [this.rootUrl, "Categories", "CategoryType", "ClearCache"].join("/"),
			method: "DELETE",
		});

		/**
		 * Use to create a new category.
		 * @param {@link cef.admin.api.CreateCategoryDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateCategory
		 * @path <API Root>/Categories/Category/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateCategory = (routeParams?: CreateCategoryDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "Category", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to create a new category file.
		 * @param {@link cef.admin.api.CreateCategoryFileDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateCategoryFile
		 * @path <API Root>/Categories/CategoryFile/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateCategoryFile = (routeParams?: CreateCategoryFileDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryFile", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to create a new category image.
		 * @param {@link cef.admin.api.CreateCategoryImageDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateCategoryImage
		 * @path <API Root>/Categories/CategoryImage/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateCategoryImage = (routeParams?: CreateCategoryImageDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryImage", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to create a new category image type.
		 * @param {@link cef.admin.api.CreateCategoryImageTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateCategoryImageType
		 * @path <API Root>/Categories/CategoryImageType/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateCategoryImageType = (routeParams?: CreateCategoryImageTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryImageType", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Use to create a new category type.
		 * @param {@link cef.admin.api.CreateCategoryTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.CreateCategoryType
		 * @path <API Root>/Categories/CategoryType/Create
		 * @verb POST
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		CreateCategoryType = (routeParams?: CreateCategoryTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryType", "Create"].join("/"),
			method: "POST",
			data: routeParams
		});

		/**
		 * Deactivate a specific category from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateCategoryByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateCategoryByID
		 * @path <API Root>/Categories/Category/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateCategoryByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "Category", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Deactivate a specific category file from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateCategoryFileByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateCategoryFileByID
		 * @path <API Root>/Categories/CategoryFile/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateCategoryFileByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryFile", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Deactivate a specific category image from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateCategoryImageByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateCategoryImageByID
		 * @path <API Root>/Categories/CategoryImage/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateCategoryImageByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryImage", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Deactivate a specific category image type from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateCategoryImageTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateCategoryImageTypeByID
		 * @path <API Root>/Categories/CategoryImageType/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateCategoryImageTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryImageType", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Deactivate a specific category type from the system [Soft-Delete]
		 * @param {@link cef.admin.api.DeactivateCategoryTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeactivateCategoryTypeByID
		 * @path <API Root>/Categories/CategoryType/Deactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeactivateCategoryTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryType", "Deactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Removes a specific category from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteCategoryByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteCategoryByID
		 * @path <API Root>/Categories/Category/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteCategoryByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "Category", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Removes a specific category file from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteCategoryFileByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteCategoryFileByID
		 * @path <API Root>/Categories/CategoryFile/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteCategoryFileByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryFile", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Removes a specific category image from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteCategoryImageByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteCategoryImageByID
		 * @path <API Root>/Categories/CategoryImage/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteCategoryImageByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryImage", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Removes a specific category image type from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteCategoryImageTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteCategoryImageTypeByID
		 * @path <API Root>/Categories/CategoryImageType/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteCategoryImageTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryImageType", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Removes a specific category type from the system [Hard-Delete]
		 * @param {@link cef.admin.api.DeleteCategoryTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.DeleteCategoryTypeByID
		 * @path <API Root>/Categories/CategoryType/Delete/ID/{ID}
		 * @verb DELETE
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		DeleteCategoryTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryType", "Delete", "ID", id].join("/"),
			method: "DELETE",
		});

		/**
		 * Use to get a list of categories
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategories
		 * @path <API Root>/Categories/Categories
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryPagedResults>}
		 * @public
		 */
		GetCategories = (routeParams?: GetCategoriesDto) => this.$http<CategoryPagedResults>({
			url: [this.rootUrl, "Categories", "Categories"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific category
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryByID
		 * @path <API Root>/Categories/Category/ID
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryModel>}
		 * @public
		 */
		GetCategoryByID = (routeParams?: GetCategoryByIDDto) => this.$http<CategoryModel>({
			url: [this.rootUrl, "Categories", "Category", "ID"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific category file
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryFileByID
		 * @path <API Root>/Categories/CategoryFile/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryFileModel>}
		 * @public
		 */
		GetCategoryFileByID = (id: number) => this.$http<CategoryFileModel>({
			url: [this.rootUrl, "Categories", "CategoryFile", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of category files
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryFiles
		 * @path <API Root>/Categories/CategoryFiles
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryFilePagedResults>}
		 * @public
		 */
		GetCategoryFiles = (routeParams?: GetCategoryFilesDto) => this.$http<CategoryFilePagedResults>({
			url: [this.rootUrl, "Categories", "CategoryFiles"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific category image
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryImageByID
		 * @path <API Root>/Categories/CategoryImage/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryImageModel>}
		 * @public
		 */
		GetCategoryImageByID = (id: number) => this.$http<CategoryImageModel>({
			url: [this.rootUrl, "Categories", "CategoryImage", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of category images
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryImages
		 * @path <API Root>/Categories/CategoryImages
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryImagePagedResults>}
		 * @public
		 */
		GetCategoryImages = (routeParams?: GetCategoryImagesDto) => this.$http<CategoryImagePagedResults>({
			url: [this.rootUrl, "Categories", "CategoryImages"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific category image type
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryImageTypeByID
		 * @path <API Root>/Categories/CategoryImageType/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<TypeModel>}
		 * @public
		 */
		GetCategoryImageTypeByID = (id: number) => this.$http<TypeModel>({
			url: [this.rootUrl, "Categories", "CategoryImageType", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of category image types
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryImageTypes
		 * @path <API Root>/Categories/CategoryImageTypes
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryImageTypePagedResults>}
		 * @public
		 */
		GetCategoryImageTypes = (routeParams?: GetCategoryImageTypesDto) => this.$http<CategoryImageTypePagedResults>({
			url: [this.rootUrl, "Categories", "CategoryImageTypes"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Get the Category site map without replacing it
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategorySiteMapContent
		 * @path <API Root>/Categories/SiteMap
		 * @verb GET
		 * @returns {ng.IHttpPromise<DownloadFileResult>}
		 * @public
		 */
		GetCategorySiteMapContent = () => this.$http<DownloadFileResult>({
			url: [this.rootUrl, "Categories", "SiteMap"].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a tree of categories. Include the ParentID and set IncludeChildrenInResults to false and this can function as lazy loading for Kendo Trees.
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryTree
		 * @path <API Root>/Categories/Tree
		 * @verb GET
		 * @returns {ng.IHttpPromise<Array<ProductCategorySelectorModel>>}
		 * @public
		 */
		GetCategoryTree = (routeParams?: GetCategoryTreeDto) => this.$http<Array<ProductCategorySelectorModel>>({
			url: [this.rootUrl, "Categories", "Tree"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Use to get a specific category type
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryTypeByID
		 * @path <API Root>/Categories/CategoryType/ID/{ID}
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<TypeModel>}
		 * @public
		 */
		GetCategoryTypeByID = (id: number) => this.$http<TypeModel>({
			url: [this.rootUrl, "Categories", "CategoryType", "ID", id].join("/"),
			method: "GET",
		});

		/**
		 * Use to get a list of category types
		 * @generatedByCSharpType Clarity.Ecommerce.Service.GetCategoryTypes
		 * @path <API Root>/Categories/CategoryTypes
		 * @verb GET
		 * @priority 1
		 * @returns {ng.IHttpPromise<CategoryTypePagedResults>}
		 * @public
		 */
		GetCategoryTypes = (routeParams?: GetCategoryTypesDto) => this.$http<CategoryTypePagedResults>({
			url: [this.rootUrl, "Categories", "CategoryTypes"].join("/"),
			method: "GET",
			params: routeParams
		});

		/**
		 * Reactivate a specific category from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateCategoryByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateCategoryByID
		 * @path <API Root>/Categories/Category/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateCategoryByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "Category", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Reactivate a specific category file from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateCategoryFileByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateCategoryFileByID
		 * @path <API Root>/Categories/CategoryFile/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateCategoryFileByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryFile", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Reactivate a specific category image from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateCategoryImageByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateCategoryImageByID
		 * @path <API Root>/Categories/CategoryImage/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateCategoryImageByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryImage", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Reactivate a specific category image type from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateCategoryImageTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateCategoryImageTypeByID
		 * @path <API Root>/Categories/CategoryImageType/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateCategoryImageTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryImageType", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Reactivate a specific category type from the system [Restore from Soft-Delete]
		 * @param {@link cef.admin.api.ReactivateCategoryTypeByIDDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.ReactivateCategoryTypeByID
		 * @path <API Root>/Categories/CategoryType/Reactivate/ID/{ID}
		 * @verb PATCH
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponse>}
		 * @public
		 */
		ReactivateCategoryTypeByID = (id: number) => this.$http<CEFActionResponse>({
			url: [this.rootUrl, "Categories", "CategoryType", "Reactivate", "ID", id].join("/"),
			method: "PATCH",
		});

		/**
		 * Generates a new Category site map and then replaces the existing one per the DropPath value from the web config
		 * @param {@link cef.admin.api.RegenerateCategorySiteMapDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.RegenerateCategorySiteMap
		 * @path <API Root>/Categories/SiteMap/Regenerate
		 * @verb POST
		 * @returns {ng.IHttpPromise<boolean>}
		 * @public
		 */
		RegenerateCategorySiteMap = () => this.$http<boolean>({
			url: [this.rootUrl, "Categories", "SiteMap", "Regenerate"].join("/"),
			method: "POST",
		});

		/**
		 * Use to update an existing category.
		 * @param {@link cef.admin.api.UpdateCategoryDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateCategory
		 * @path <API Root>/Categories/Category/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateCategory = (routeParams?: UpdateCategoryDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "Category", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to update an existing category file.
		 * @param {@link cef.admin.api.UpdateCategoryFileDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateCategoryFile
		 * @path <API Root>/Categories/CategoryFile/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateCategoryFile = (routeParams?: UpdateCategoryFileDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryFile", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to update an existing category image.
		 * @param {@link cef.admin.api.UpdateCategoryImageDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateCategoryImage
		 * @path <API Root>/Categories/CategoryImage/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateCategoryImage = (routeParams?: UpdateCategoryImageDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryImage", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to update an existing category image type.
		 * @param {@link cef.admin.api.UpdateCategoryImageTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateCategoryImageType
		 * @path <API Root>/Categories/CategoryImageType/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateCategoryImageType = (routeParams?: UpdateCategoryImageTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryImageType", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

		/**
		 * Use to update an existing category type.
		 * @param {@link cef.admin.api.UpdateCategoryTypeDto} routeParams - The route parameters as a Body Object
		 * @generatedByCSharpType Clarity.Ecommerce.Service.UpdateCategoryType
		 * @path <API Root>/Categories/CategoryType/Update
		 * @verb PUT
		 * @priority 1
		 * @returns {ng.IHttpPromise<CEFActionResponseT<number>>}
		 * @public
		 */
		UpdateCategoryType = (routeParams?: UpdateCategoryTypeDto) => this.$http<CEFActionResponseT<number>>({
			url: [this.rootUrl, "Categories", "CategoryType", "Update"].join("/"),
			method: "PUT",
			data: routeParams
		});

	}
}
