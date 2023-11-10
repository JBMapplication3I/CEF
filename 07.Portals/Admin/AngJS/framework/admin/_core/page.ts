module cef.admin.core {
    export interface IPageScope extends ng.IScope {
        caption: string;
        headicon: string;
        additionalCss: string;
    }

    export interface IPageController extends ng.IControllerService {
        $scope: IPageScope
    }

    export const cvPageDirectiveFn = ($compile: ng.ICompileService, $translate: ng.translate.ITranslateService): ng.IDirective => ({
        restrict: "EA",
        scope: false,
        transclude: true, // Required
        // Note: This is a control-specific tempalte that must remain inline
        template: `<div class="full-height" ng-transclude></div>`,
        controller: function ($scope: ng.IScope) {
            this.$scope = $scope;
        },
        controllerAs: "pageCtrl",
        link: (scope: IPageScope, __, attributes) => {
            const load = () => {
                if (attributes["caption"]) {
                    if (attributes["caption"].indexOf("{{") !== -1) {
                        scope.caption = $compile(attributes["caption"])(scope) as any;
                    } else {
                        scope.caption = attributes["caption"];
                    }
                }
                scope.headicon = attributes["headIcon"] || attributes["head-icon"];
            };
            if ($translate.isReady()) {
                load();
                return;
            }
            $translate.onReady().then(() => load());
        }
    });
}
