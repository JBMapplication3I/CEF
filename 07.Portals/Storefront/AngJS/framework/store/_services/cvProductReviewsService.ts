module cef.store.services {
    export interface IProductReviewsService {
        getCached(id: number): api.ProductReviewInformationModel;
        get(id: number): ng.IPromise<api.ProductReviewInformationModel>;
        factoryAssign(product: api.ProductModel): api.ProductModel;
    }

    export class ProductReviewsService implements IProductReviewsService {
        // Properties
        private summaries: { [id: number]: api.ProductReviewInformationModel } = { };
        private promises: { [id: number]: ng.IPromise<api.ProductReviewInformationModel> } = { };
        // Functions
        private genBlankRatingsObj(): api.CalculatedRatings {
            return {
                value: null,
                count: null,
                reviews: null,
                loading: true,
            };
        }
        getCached(id: number): api.ProductReviewInformationModel {
            if (!this.summaries[id] && !this.promises[id]) {
                this.promises[id] = this.get(id).finally(() => { delete this.promises[id]; });
            }
            return this.summaries[id];
        }
        get(id: number): ng.IPromise<api.ProductReviewInformationModel> {
            if (!id) {
                return this.$q.reject();
            }
            if (this.summaries[id]) {
                return this.$q.resolve(this.summaries[id]);
            }
            if (this.promises[id]) {
                return this.promises[id];
            }
            return this.$q((resolve, reject) => {
                this.cvApi.products.GetProductReview(id).then(r => {
                    if (!r || !r.data) {
                        reject("No data returned");
                        return;
                    }
                    this.summaries[id] = r.data;
                    resolve(this.summaries[id]);
                }).catch(reject);
            });
        }
        factoryAssign(product: api.ProductModel): api.ProductModel {
            if (angular.isFunction(product.readRatings)) {
                // Already processed
                return product;
            }
            product["$_rawRatings"] = this.genBlankRatingsObj();
            product.readRatings = () => product["$_rawRatings"];
            // TODO: MemCache the results by product ID so we can avoid repeat calls for same product
            this.cvApi.products.GetProductReview(product.ID).then(r => {
                if (!r?.data) {
                    console.error(r && r.data);
                    return;
                }
                const ratings = product["$_rawRatings"] as api.CalculatedRatings;
                // Assign updated values
                ratings.value = r.data.Value;
                ratings.count = r.data.Count;
                ratings.reviews = r.data.Reviews;
                // Finish
                ratings.loading = false;
                product["$_rawRatings"] = ratings;
            })
            return product;
        }
        // Constructor
        constructor(
            private readonly $q: ng.IQService,
            private readonly cefConfig: core.CefConfig,
            private readonly cvApi: api.ICEFAPI) {
        }
    }
}
