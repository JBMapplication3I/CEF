module cef.admin.widgets {
    class HomeButtonPairWidgetController extends core.TemplatedControllerBase {
        // Scope Properties
        listState: string;
        listKey: string;
        newState: string;
        newKey: string;
        // Properties
        get allowList(): boolean {
            return this.cvSecurityService.hasPermission(
                this.$state.get(this.listState).requiresPermission + '');
        }
        get allowNew(): boolean {
            return angular.isDefined(this.newState) && this.newState != "" && this.newState != null;
        }
        get listStateAsID(): string { return this.listState && this.listState.replace(/\./g, ""); }
        get newStateAsID(): string { return this.newState && this.newState.replace(/\./g, ""); }
        // Functions
        // <None>
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $state: ng.ui.IStateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvSecurityService: services.ISecurityService) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefHomeButtonPairWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            listState: "@",
            listKey: "@",
            newState: "@?",
            newKey: "@?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/homeButtonPairWidget.html", "ui"),
        controller: HomeButtonPairWidgetController,
        controllerAs: "homeButtonPairWidgetCtrl",
        bindToController: true
    }));
}
