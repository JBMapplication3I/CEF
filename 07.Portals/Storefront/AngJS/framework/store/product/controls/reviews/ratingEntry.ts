module cef.store.product.controls.reviews {
    class RatingEntryController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        review: api.ReviewModel;
        scale: number;
        // Properties
        currentHoveredRating = 0;
        get value() { return this.review.Value }
        set value(newValue: number) { this.review.Value = newValue; }
        // Functions
        updateHoveredRating(newvalue: number): void {
            this.currentHoveredRating = newvalue;
        }
        resetHoveredRating(): void {
            this.currentHoveredRating = 0;
        }
        scaleArray(): number[] {
            return Array(this.scale || 5).join().split(",").map((_, idx) => idx + 1);
        }
        rate(score: number): void {
            if (!score) { return; }
            this.value = score;
        }
    }

    cefApp.directive("cefRatingEntry", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { review: "=", scale: "=" },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/reviews/ratingEntry.html", "ui"),
        controller: RatingEntryController,
        controllerAs: "reCtrl",
        bindToController: true
    }));
}
