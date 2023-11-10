module cef.store.product.controls.reviews {
    class ReviewsMainController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        hideComment: boolean;
        private _productId: number;
        get productId(): number {
            return this._productId;
        }
        set productId(newValue: number) {
            if (!newValue || this._productId === newValue) {
                return;
            }
            this._productId = newValue;
            if (this._lastRunBad) {
                setTimeout(() => this.getReviews(), 100);
                return;
            }
            this.getReviews();
        }
        // Properties
        private _lastRunBad = false;
        get summary(): api.ProductReviewInformationModel {
            return this
                && this.productId
                && this.cvProductReviewsService
                && this.cvProductReviewsService.getCached(this.productId);
        }
        // Functions
        private getReviews(): void {
            if (!this.productId || !this.cvProductReviewsService) {
                this._lastRunBad = true;
                return;
            }
            this._lastRunBad = false;
            this.setRunning();
            this.cvProductReviewsService.get(this.productId)
                .then(() => this.finishRunning())
                .catch(reason => this.finishRunning(true, reason));
        }
        // Constructor
        constructor(
                readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvProductReviewsService: services.IProductReviewsService) {
            super(cefConfig);
            const unbind1 = $scope.$on(cvServiceStrings.events.products.refreshReviews,
                () => this.getReviews());
            $scope.$on(cvServiceStrings.events.$scope.$destroy, () => {
                if (angular.isFunction(unbind1)) { unbind1(); }
            })
        }
    }

    cefApp.directive("cefReviewsMain", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            productId: "=",
            hideComment: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/reviews/reviewsMain.html", "ui"),
        controller: ReviewsMainController,
        controllerAs: "rmCtrl",
        bindToController: true
    }));
}
