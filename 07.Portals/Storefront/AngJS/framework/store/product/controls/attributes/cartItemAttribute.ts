module cef.store.product.controls.attributes {
    cefApp.directive("cefCartItemAttribute", (): ng.IDirective => ({
        restrict: "A",
        require: "ngModel",
        link: ($scope, el, attrs, ctrl) => {
            function formatCartItemAttribute(val: any) {
                return { Name: attrs["cefCartItemAttribute"], Value: val };
            }
            if (attrs["cefCartItemAttribute"]) {
                (ctrl as any).$parsers.push(formatCartItemAttribute);
            }
        }
    }));
}
