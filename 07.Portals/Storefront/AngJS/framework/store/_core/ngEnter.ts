module cef.store.core {
    export const ngEnterDirectiveFn = (): ng.IDirective => ({
        link(scope, element, attrs) {
            element.bind("keydown keypress", event => {
                if (event.which !== 13) { return; }
                scope.$apply(() => scope.$eval(attrs["ngEnter"]));
                event.preventDefault();
            });
        }
    });
}
