module cef.admin.widgets.loading {
    class LoadingWidgetController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        overlay: boolean;
        padIn: boolean;
        size: number;
        message: string;
        messageKey: string;
        // Functions
        classesObj(): any {
            const obj = {
                "overlay position-absolute align-items-center o-70 ay-0": this.overlay,
                "ax-0": this.overlay && !this.padIn,
                "ax-3": this.overlay &&  this.padIn
            };
            if (this.classes) {
                obj[this.classes] = true;
            } else {
                obj["bg-inverse"] = this.overlay;
            }
            return obj;
        }
        // Constructor
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefLoadingWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            overlay: "=?",
            padIn: "=?",
            size: "=?",
            message: "=?",
            messageKey: "@?",
            classes: "@?"
        },
        replace: true, // Required for overlay mode
        templateUrl: $filter("corsLink")("/framework/admin/widgets/loading/loadingWidget.html", "ui"),
        controller: LoadingWidgetController,
        controllerAs: "loadingWidgetCtrl",
        bindToController: true
    }));
}
