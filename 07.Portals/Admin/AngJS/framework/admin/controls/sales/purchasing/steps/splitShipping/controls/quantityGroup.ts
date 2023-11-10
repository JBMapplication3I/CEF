module cef.admin.purchasing.steps.splitShipping.controls {
    adminApp.directive("quantityGroup", (): ng.IDirective => ({
        restrict: "A",
        require: "form",
        scope: { quantity: "=quantityGroup" },
        link(scope, el, attrs, ctrl: any) {
            ctrl.quantityGroup = { total: scope.quantity };
        }
    }));
}
