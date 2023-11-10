/**
 * @file framework/store/product/controls/reviews/reviewEntry.ts
 * @author Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
 * @desc Review entry class
 */
module cef.store.product.controls.reviews {
    class ReviewEntryController extends core.TemplatedControllerBase {
        // Properties Bound by Scope
        productId: number;
        hideComment: boolean;
        // Properties
        review: api.ReviewModel = null;
        reviewSuccessMessage: string;
        // Functions
        private loadNewReview(): api.ReviewModel {
            return <api.ReviewModel>{
                Active: true,
                CreatedDate: new Date(),
                //
                Title: null,
                Comment: null,
                Value: 0,
                Approved: false,
                //
                TypeID: 0, // Set by the server
                SubmittedByUserID: null, // Set by the Server
            }
        }
        private saveReview(): void {
            this.cvLoginModalFactory(null, null, false, true).then(modalResult => {
                if (!modalResult || !this.cvAuthenticationService.isAuthenticated()) {
                    return;
                }
                this.setRunning("Saving...");
                this.review.ProductID = this.productId;
                this.cvApi.reviews.CreateReview(this.review).then(() => {
                    this.finishRunning();
                    this.$rootScope.$broadcast(this.cvServiceStrings.events.products.refreshReviews);
                }).catch(result => this.finishRunning(true, result));
            });
        }
        rate(val: string | number): void {
            if (val) {
                this.review.Value = Number(val);
            }
        }
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvAuthenticationService: services.IAuthenticationService,
                private readonly cvLoginModalFactory: user.ILoginModalFactory) {
            super(cefConfig);
            this.review = this.loadNewReview();
            this.reviewSuccessMessage = "ui.storefront.product.reviews.reviewEntry."
                + (this.hideComment ? "successYourRatingHasBeenSaved" : "successYourReviewHasBeenSaved");
        }
    }

    cefApp.directive("cefReviewEntry", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            productId: "=",
            hideComment: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/reviews/reviewEntry.html", "ui"),
        controller: ReviewEntryController,
        controllerAs: "reCtrl",
        bindToController: true
    }));
}
