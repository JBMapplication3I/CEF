module cef.store.purchasing.steps.splitShipping.controls {
    cefApp.directive("quantityGroup", (): ng.IDirective => ({
        restrict: "A",
        require: "form",
        scope: { quantity: "=quantityGroup" },
        link(scope, el, attrs, ctrl: any) {
            ctrl.quantityGroup = { total: scope.quantity };
        }
    }));
}
