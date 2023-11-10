module cef.admin.widgets {
    class HomePanelWidgetController extends core.TemplatedControllerBase {
        // Scope Properties
        headerKey: string;
        listState: string;
        listKey: string;
        newState: string;
        newKey: string;
        footerKey: string;
        keywords: string[];
        // Properties
        get allowList(): boolean {
            return this.cvSecurityService.hasPermission(
                this.$state.get(this.listState).requiresPermission + "");
        }
        get allowNew(): boolean {
            return angular.isDefined(this.newState) && this.newState !== "" && this.newState !== null;
        }
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

    adminApp.directive("cefHomePanelWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            headerKey: "@",
            listState: "@",
            listKey: "@",
            newState: "@?",
            newKey: "@?",
            footerKey: "@",
            keywords: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/homePanelWidget.html", "ui"),
        controller: HomePanelWidgetController,
        controllerAs: "homePanelWidgetCtrl",
        bindToController: true
    }));
}
