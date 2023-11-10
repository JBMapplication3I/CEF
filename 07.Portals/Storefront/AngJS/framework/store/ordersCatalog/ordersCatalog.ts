// <copyright file="ordersCatalog.ts" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>orders catalog class</summary>
module cef.store.ordersCatalog {
    export class OrdersCatalogController extends core.TemplatedControllerBase {
        // Properties
        category: api.CategoryModel[] = [];
        productList: api.ProductModel[] = [];
        showPanel: boolean;
        // Functions
        load() {
            // First load all of the products that were previously ordered
            this.cvApi.products.GetProductsByPreviouslyOrdered().then(response => {
                this.productList = response.data.Results;
                // We need to set up the categories too:
                const categories2: { [id: string] : api.CategoryModel; } = {};
                for (let productListIndex = 0; productListIndex < this.productList.length; productListIndex++) {
                    if (!this.productList[productListIndex].ProductCategories) {
                        continue;
                    }
                    for (let productCategoriesIndex = 0;
                         productCategoriesIndex < this.productList[productListIndex].ProductCategories.length;
                         productCategoriesIndex++) {
                        categories2[this.productList[productListIndex].ProductCategories[productCategoriesIndex].SlaveName]
                            = this.productList[productListIndex].ProductCategories[productCategoriesIndex].Slave;
                    }
                }
                this.category = _.values(categories2);
                // We'll sort the categories to make it easier for user
                this.category.sort((a, b) => {
                    const nameA = a.Name.toUpperCase(); // ignore upper and lowercase
                    const nameB = b.Name.toUpperCase();
                    if (nameA < nameB) {
                        return -1;
                    }
                    if (nameA > nameB) {
                        return 1;
                    }
                    return 0;
                });
            });
         }
        grabProducts(catID: number): void {
            this.cvApi.products.GetProductsByPreviouslyOrdered({
                Active: true,
                AsListing: true,
                CategoryID: catID,
                // CategoryName: "Previous Orders"
            }).then(r => this.productList = r.data.Results);
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.showPanel = true;
            this.load();
        }
    }

    cefApp.directive("cefOrdersCatalog", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/store/ordersCatalog/ordersCatalog.html", "ui"),
        controller: OrdersCatalogController,
        controllerAs: "ordersCatalogCtrl",
    }));
}
