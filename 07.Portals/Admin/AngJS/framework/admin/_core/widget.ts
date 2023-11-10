module cef.admin.core {
    export interface IWidgetScope extends ng.IScope {
        caption: string;
        headicon: string;
        additionalCss: string;
    }

    export interface IWidgetMenuItemScope extends ng.IScope {
        url?: string;
        altUrl?: string;
        click?;
        toggle?: string;
        caption?: string;
        altCaption?: string;
        iconClass?: string;
        altIconClass?: string;
        buttonClass?: string;
        altButtonClass?: string;
        target?: string;
        permission?: string;
    }

    export const cvWidgetDirectiveFn = ($filter: ng.IFilterService, $compile: ng.ICompileService, $translate: ng.translate.ITranslateService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        require: "?^cvPage",
        transclude: true, // Required
        templateUrl: $filter("corsLink")("/framework/admin/_core/widget.html", "ui"),
        controllerAs: "WidgetCtrl",
        controller: () => { },
        link: (scope: IWidgetScope, $element: ng.IAugmentedJQuery, attributes: ng.IAttributes, pageController: IPageController) => {
            const load = () => {
                if (attributes["caption"] != null) {
                    if (attributes["caption"].indexOf("{{") !== -1) {
                        scope.caption = $compile(attributes["caption"])(scope) as any;
                    } else {
                        scope.caption = attributes["caption"];
                    }
                }
                if (attributes["additionalCss"] != null) {
                    scope.additionalCss = attributes["additionalCss"];
                } else if (attributes["additional-css"] != null) {
                    scope.additionalCss = attributes["additional-css"];
                }
                if (attributes["headIcon"] != null) {
                    scope.headicon = attributes["head-icon"];
                } else if (attributes["head-icon"] != null) {
                    scope.headicon = attributes["head-icon"];
                }
                // Transclude in the header and body content
                const ngTransclude = $element.find("#ng-transclude");
                $element.find("#widget-head-transclude").append(ngTransclude.children("widget-head,.widget-head").contents() as any);
                $element.find("#widget-body-transclude").append(ngTransclude.children("widget-body,.widget-body").contents() as any);
                ngTransclude.remove();
                if (pageController != null) {
                    if (angular.isDefined(pageController.$scope.caption) && !angular.isDefined(scope.caption)) {
                        scope.caption = pageController.$scope.caption;
                    }
                    if (angular.isDefined(pageController.$scope.headicon) && !angular.isDefined(scope.headicon)) {
                        scope.headicon = pageController.$scope.headicon;
                    }
                    if (angular.isDefined(pageController.$scope.additionalCss) && !angular.isDefined(scope.additionalCss)) {
                        scope.additionalCss = pageController.$scope.additionalCss;
                    }
                }
            };
            if ($translate.isReady()) {
                load();
                return;
            }
            $translate.onReady().then(() => load());
        }
    });

    /** Adds menu items to the .widget-head .widget-menu of the form: */
    export class WidgetMenuItemController {
        constructor(private readonly cvSecurityService: services.ISecurityService) { }
    }

    /** Adds menu items to the .widget-head .widget-menu of the form: */
    export const cvWidgetMenuItemDirectiveFn = ($compile: ng.ICompileService, $translate: ng.translate.ITranslateService): ng.IDirective => ({
        require: "^cvWidget",
        replace: true,
        restrict: "EA",
        scope: {
            toggle: "@?",
            caption: "@",
            altCaption: "@?",
            url: "@?",
            altUrl: "@?",
            click: "@?",
            iconClass: "@?",
            altIconClass: "@?",
            buttonClass: "@?",
            altButtonClass: "@?",
            target: "@?",
            permission: "@?"
        },
        controller: WidgetMenuItemController,
        controllerAs: "cvWidgetMenuItemCtrl",
        link: (scope: IWidgetMenuItemScope, $element: ng.IAugmentedJQuery, attributes: ng.IAttributes, ctrl: WidgetMenuItemController) => {
            const load = () => {
                if (scope.buttonClass == null) { scope.buttonClass = "btn-primary"; }
                const hasToggle = scope.toggle != null;
                if (attributes["caption"]) {
                    if (attributes["caption"].indexOf("{{") !== -1) {
                        scope.caption = $compile(attributes["caption"])(scope) as any;
                    } else {
                        scope.caption = attributes["caption"];
                    }
                }
                if (attributes["altCaption"]) {
                    if (attributes["altCaption"].indexOf("{{") !== -1) {
                        scope.altCaption = $compile(attributes["altCaption"])(scope) as any;
                    } else {
                        scope.altCaption = attributes["altCaption"];
                    }
                }
                $element.parents(".widget").find(".widget-head").find("#cv-widget-menu").append(
                    $compile($(`<a${scope.target != null ? ` target="${scope.target}"` : ""
                        }${hasToggle && scope.altUrl != null && scope.url != null
                        ? ` ng-href="${scope.toggle} ? ${scope.url} : ${scope.altUrl}"`
                        : scope.url != null ? ` href="${scope.url}"` : ""}${scope.click != null ? ` ng-click="${scope.click}"` : ""
                        } class="btn btn-sm ${hasToggle && scope.altButtonClass != null && scope.buttonClass != null
                        ? `\" ng-class="{'${scope.buttonClass}': ${scope.toggle}, '${scope.altButtonClass}': !${scope.toggle}}"`
                        : scope.buttonClass != null ? scope.buttonClass + "\"" : "\""}${scope.permission != null
                        ? ` ng-if="cvWidgetMenuItemCtrl.cvSecurityService.hasPermission(${scope.permission})"`
                        : ""}>${hasToggle && scope.altIconClass != null && scope.iconClass != null
                        ? `<i class="far fa-fw" ng-class="{'${scope.iconClass}': ${scope.toggle}, '${scope.altIconClass}': !${scope.toggle}}"></i> `
                        : scope.iconClass != null ? `<i class="far fa-fw ${scope.iconClass}"></i> `
                        : "no icon"}<span${hasToggle && scope.altCaption != null && scope.caption != null
                        ? ` ng-bind="${scope.toggle} ? '${scope.caption}' : '${scope.altCaption}'"`
                        : ""}>${hasToggle && scope.altCaption != null && scope.caption != null
                        ? "" : scope.caption != null ? scope.caption : ""}</span></a>`))(scope) as any);
            };
            if ($translate.isReady()) {
                load();
                return;
            }
            $translate.onReady().then(() => load());
        }
    });
}
