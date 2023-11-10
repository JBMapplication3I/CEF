module cef.admin {
    class AssociatorWidget extends core.TemplatedControllerBase {
        // Bound Scope Properties
        titleKey: string;
        selectKey: string;
        addedKey: string;
        addedSubKey: string;
        name: string;
        master: api.BaseModel;
        apiCall: () => (routeParams?: api.BaseSearchModel) => ng.IHttpPromise<api.PagedResultsBase<api.BaseModel>>;
        collection: string;
        defaults: object;
        // Properties
        paging: core.ServerSidePaging<api.BaseModel, api.PagedResultsBase<api.BaseModel>>;
        addedQuickFilter: string;
        // Functions
        load(): ng.IPromise<void> {
            this.paging = new core.ServerSidePaging<api.BaseModel, api.PagedResultsBase<api.BaseModel>>(
                this.$rootScope,
                this.$scope,
                this.$filter,
                this.$q,
                this.cvServiceStrings,
                this.apiCall(),
                8,
                this.name);
            return this.$q.resolve();
        }
        add(id: number): void {
            if (!id) { return; }
            // Ensure the data is loaded
            const model = _.find(this.paging.dataUnpaged, x => x.ID === id);
            if (!model) { return; }
            if (!this.master[this.collection]) {
                this.master[this.collection] = [];
            }
            // Ensure it's not already in the collection
            if (_.find(this.master[this.collection], x => x["SlaveID"] === model.ID)) {
                return;
            }
            // Add it
            const toAdd = {
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                SerializableAttributes: { },
                // IAmARelationshipTable Properties
                MasterID: this.master.ID,
                SlaveID: model.ID,
                SlaveKey: model.CustomKey,
                SlaveName: model["Name"]
            };
            if (angular.isObject(this.defaults)) {
                angular.merge(toAdd, this.defaults);
            }
            this.master[this.collection].push(toAdd);
            this.forms["associate"].$setDirty();
            this.$rootScope.$broadcast(this.cvServiceStrings.events.associator.added,
                this.master,
                this.collection,
                this.name,
                toAdd);
        }
        remove(index: number): void {
            const removed = this.master[this.collection].splice(index, 1);
            this.forms["associate"].$setDirty();
            this.$rootScope.$broadcast(this.cvServiceStrings.events.associator.removed,
                this.master,
                this.collection,
                this.name,
                removed);
        }
        // Events
        // <None>
        // Constructor
        constructor(
                private readonly $rootScope: ng.IRootScopeService,
                private readonly $scope: ng.IScope,
                private readonly $q: ng.IQService,
                private readonly $filter: ng.IFilterService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvServiceStrings: services.IServiceStrings) {
            super(cefConfig);
            this.load();
        }
    }

    adminApp.directive("cefAssociatorWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            titleKey: "@",
            selectKey: "@",
            addedKey: "@",
            addedSubKey: "@",
            name: "@",
            master: "=",
            apiCall: "&",
            collection: "@",
            defaults: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/associations/associatorWidget.html", "ui"),
        controller: AssociatorWidget,
        controllerAs: "associatorWidgetCtrl",
        bindToController: true
    }));
}
