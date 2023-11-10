module cef.store.product.controls.cards {
    export class ProductCardThumbnailWidgetController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        productFromScope?: api.ProductModel | api.CategoryModel;
        productId: number;
        productImage: string;
        width: number;
        height: number;
        overrideImageRoot: string;
        withMargin: boolean = true;
        isCategory: boolean;
        noLink: boolean;
        // Properties
        get product(): api.ProductModel | api.CategoryModel {
            return this.isCategory
                ?   _.isObject(this.productFromScope) ? this.productFromScope
                    : this.cvCategoryService.getCached({ id: this.productId })
                :   _.isObject(this.productFromScope) ? this.productFromScope
                    : this.cvProductService.getCached({ id: this.productId });
        }
        get isReady(): boolean {
            return Boolean(this.product);
        }
        index: number;
        ready = false;
        imageName: string;
        // Functions
        protected get imageResizerQueryString(): string {
            return `?maxwidth=${this.width || 250}&maxheight=${this.height || 250}&mode=pad&scale=both`;
        }
        protected get imageResizerBody(): any {
            return {
                maxwidth: this.width || 250,
                maxheight: this.height || 250,
                mode: "pad",
                scale: "both"
            };
        }
        get src(): string {
            if (!this.imageName && this.product) {
                if (!this.product.PrimaryImageFileName) {
                    if (this.productImage) {
                        // Read the value below
                    } else {
                        // The product has no image
                        return this.onerrorsrc;
                    }
                } else {
                    this.imageName = this.product.PrimaryImageFileName;
                }
            }
            if (!this.imageName && this.productImage) {
                this.imageName = this.productImage; // Was provided directly
            }
            if (this.imageName) {
                return this.makeLink(this.imageName);
            }
            if (!this.product && !this.productId) {
                // Nothing to go off of
                return this.onerrorsrc;
            }
            if (this.viewState.running || !this.productId) {
                // We're either waiting for the scope to load or the service to come back
                return this.onerrorsrc;
            }
            this.setRunning();
            const lookup = { id: this.productId };
            if (this.isCategory) {
                this.cvCategoryService.get(lookup).then(r => {
                    if (!r) {
                        r = this.cvCategoryService.getCached(lookup);
                    }
                    this.imageName = r.PrimaryImageFileName;
                    this.finishRunning();
                });
            } else {
                this.cvProductService.get(lookup).then(r => {
                    if (!r) {
                        r = this.cvProductService.getCached(lookup);
                    }
                    this.imageName = r.PrimaryImageFileName;
                    this.finishRunning();
                });
            }
            return this.onerrorsrc;
        }
        get onerrorsrc(): string {
            return this.$filter("corsLink")(`/Content/placeholder.jpg${this.imageResizerQueryString}`, "ui");
        }
        protected makeLink(fileName: string): string {
            let folder = (this.isCategory ? "categories" : "products");
            return (this.overrideImageRoot
                ? this.$filter("corsLink")(`${this.overrideImageRoot + fileName + this.imageResizerQueryString}`, "site")
                : this.$filter("corsImageLink")(fileName, folder, this.imageResizerBody));
        }
        // Constructor
        constructor(
                protected readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvProductService: services.IProductService,
                protected readonly cvCategoryService: services.ICategoryService) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefProductCardThumbnailWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            productFromScope: "=?",
            productId: "=?",
            productImage: "=?",
            width: "=",
            height: "=",
            overrideImageRoot: "@?",
            withMargin: "=?",
            isCategory: "=?",
            noLink: "=?",
            index: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/cards/productCardThumbnailWidget.html", "ui"),
        controller: ProductCardThumbnailWidgetController,
        controllerAs: "pctwCtrl",
        bindToController: true
    }));
}
