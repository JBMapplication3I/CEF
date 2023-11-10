module cef.store.product.controls.reviews {
    class RatingSummaryController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        value: number;
        count: number;
        scale: number;
        hideCount: boolean;
        // Functions
        scaleArray(): number[] {
            return Array(this.scale).join().split(",").map((_, idx) => idx + 1);
        }
    }

    cefApp.directive("cefRatingSummary", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            value: "=",
            count: "=",
            scale: "=",
            hideCount: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/store/product/controls/reviews/ratingSummary.html", "ui"),
        controller: RatingSummaryController,
        controllerAs: "rsCtrl",
        bindToController: true
    }));
}
