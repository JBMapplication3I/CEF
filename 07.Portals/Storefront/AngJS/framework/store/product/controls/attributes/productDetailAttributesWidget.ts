module cef.store.product.controls.attributes {
    class ProductDetailAttributesWidgetController extends core.TemplatedControllerBase {
        // Properties
        product: api.ProductModel; // Bound by Scope
        showTabs: boolean; // Bound by Scope
        detailsAttrList: api.GeneralAttributeModel[];
        // Functions
        productAttr(key: string): api.SerializableAttributeObject {
            return this.product
                && this.product.SerializableAttributes
                && this.product.SerializableAttributes[key];
        }
        resolveAttributes(): ng.IPromise<void> {
            return this.$q && this.$q((resolve, reject) => {
                this.cvApi.attributes.GetGeneralAttributes({
                    Active: true,
                    AsListing: true,
                    HideFromProductDetailView: false
                }).then(r => {
                    if (!r || !r.data || !r.data.Results) {
                        reject();
                        return;
                    }
                    this.detailsAttrList = r.data.Results
                        .filter(x => !x.HideFromProductDetailView)
                        .filter(x => this.showTabs || !x.IsTab)
                        .filter(x => Object.keys(this.product.SerializableAttributes || {})
                                        .some(y => y === x.CustomKey));
                    resolve();
                }).catch(reject);
            });
        }
        // Constructor
        constructor(
                private readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.resolveAttributes();
        }
    }

    cefApp.directive("cefProductDetailAttributesWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            product: "=",
            showTabs: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/attributes/productDetailAttributesWidget.html", "ui"),
        controller: ProductDetailAttributesWidgetController,
        controllerAs: "productDetailAttributesWidgetCtrl",
        bindToController: true
    }));
}
