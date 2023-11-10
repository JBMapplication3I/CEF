module cef.admin.core {
    export const cefCompilerFn = ($compile: ng.ICompileService, $parse: ng.IParseService): ng.IDirective => ({
        restrict: "EA",
        link: (scope: any, element, attr: any) => {
            scope.$watch(attr.content,
                () => {
                    element.html($parse(attr.content)(scope));
                    $compile(element.contents() as any)(scope);
                },
                true);
        }
    });
}