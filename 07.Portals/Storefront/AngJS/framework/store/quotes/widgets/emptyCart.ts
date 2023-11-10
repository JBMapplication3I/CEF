module cef.store.quotes.widgets {
    cefApp.directive("cefSubmitQuoteCartEmpty", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/store/quotes/widgets/emptyCart.html", "ui"),
        require: "^cefSubmitQuote"
    }));
}
