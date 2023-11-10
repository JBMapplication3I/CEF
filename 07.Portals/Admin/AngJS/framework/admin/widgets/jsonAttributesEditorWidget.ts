module cef.admin.widgets {
    export interface IAttributeChangedEventArg {
        key: string;
        property: string;
        newValue: string;
        oldValue?: string;
    }

    class JsonAttributesEditorWidgetController extends core.TemplatedControllerBase {
        // Bound by Scope Properties
        master: api.HaveJsonAttributesBaseModel;
        type: string;
        // Properties
        paging: core.Paging<api.GeneralAttributeModel> = null;
        get typeAsID(): string {
            return this.type && this.type.replace(/\s/g, "");
        }
        // Functions
        loadCollections(): void {
            this.cvApi.attributes.GetGeneralAttributes({ Active: true, AsListing: true }).then(r => {
                const lowerType = this.type.toLowerCase();
                this.paging.data = _.filter(r.data.Results, value => {
                    const lowerValueTypeKey = (value.TypeKey || "").toLowerCase();
                    const lowerValueTypeName = (value.TypeName || "").toLowerCase();
                    return (lowerValueTypeKey === "general"
                            || lowerValueTypeKey === lowerType
                            || lowerValueTypeName === "general"
                            || lowerValueTypeName === lowerType)
                        && (!value.CustomKey || value.CustomKey.indexOf("Payoneer") === -1);
                });
                this.$timeout(this.injectAttributesToMasterDict, 250);
            });
        }
        // Note: This must be an arrow function
        injectAttributesToMasterDict = (): void => {
            if (!this.master) {
                this.$timeout(this.injectAttributesToMasterDict, 250);
                return;
            }
            if (!this.master.SerializableAttributes) {
                this.master.SerializableAttributes = new api.SerializableAttributesDictionary();
            }
            const changedList: IAttributeChangedEventArg[] = [];
            this.paging.data.forEach(x => {
                if (x.IsPredefined) {
                    this.cvApi.attributes.GetGeneralAttributePredefinedOptions({
                        Active: true,
                        AsListing: true,
                        AttributeID: x.ID
                    }).then(r => x["PredefinedOptions"] = r.data.Results);
                }
                if (!this.master.SerializableAttributes[x.CustomKey]) {
                    this.master.SerializableAttributes[x.CustomKey] = <api.SerializableAttributeObject>{
                        ID: x.ID,
                        Key: x.CustomKey,
                        Value: null,
                        UofM: null,
                        SortOrder: null
                    };
                    changedList.push(<IAttributeChangedEventArg>{
                        key: x.CustomKey,
                        property: "Value",
                        newValue: null,
                        oldValue: undefined
                    });
                }
            });
            if (!changedList.length) {
                return;
            }
            this.$rootScope.$broadcast(
                this.cvServiceStrings.events.attributes.changed,
                changedList);
        }
        // Note: This must be an arrow function
        sanitizeMarkup = (e): void => { e.html = e.html.replace(/style="([^"]*)"/g, ""); }
        // Note: This must be an arrow function
        attributeIsMarkup = (key: string): boolean => {
            const attribute = _.find(this.paging.data, value => value.CustomKey === key);
            if (!attribute) { return false; }
            return attribute.IsMarkup;
        }
        // Note: This must be an arrow function
        attributeIsPredefined = (key: string): boolean => {
            const attribute = _.find(this.paging.data, value => value.CustomKey === key);
            if (!attribute) { return false; }
            return attribute.IsPredefined;
        }
        // Note: This must be an arrow function
        masterHasThisAttribute = (key: string): boolean => {
            if (!this.master) { return false; }
            if (!this.master.SerializableAttributes) { return false; }
            if (!this.master.SerializableAttributes[key]) { return false; }
            if (!this.master.SerializableAttributes[key].ID) { return false; }
            return true;
        }
        // Note: This must be an arrow function
        attributeChanged = (
                $event: ng.IAngularEvent,
                key: string,
                property: string,
                newValue: string,
                oldValue: string)
                : void => {
            this.$rootScope.$broadcast(
                this.cvServiceStrings.events.attributes.changed,
                [<IAttributeChangedEventArg>{
                    key: key,
                    property: property,
                    newValue: newValue,
                    oldValue: oldValue
                }]);
        }
        // Note: This must be an arrow function
        removeAttribute = (key: string): void => {
            const oldValue = this.master.SerializableAttributes[key].Value;
            this.master.SerializableAttributes[key] = null;
            this.$rootScope.$broadcast(
                this.cvServiceStrings.events.attributes.changed,
                [<IAttributeChangedEventArg>{
                    key: key,
                    property: "Value",
                    newValue: undefined,
                    oldValue: oldValue
                }]);
        }
        // Constructor
        constructor(
                readonly $filter: ng.IFilterService,
                private readonly $timeout: ng.ITimeoutService,
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                protected readonly cvServiceStrings: services.IServiceStrings,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
            this.paging = new core.Paging<api.GeneralAttributeModel>($filter);
            this.paging.pageSize = 8;
            this.paging.pageSetSize = 3;
            this.loadCollections();
        }
    }

    adminApp.directive("cefJsonAttributesEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { master: "=", type: "@" },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/jsonAttributesEditorWidget.html", "ui"),
        controller: JsonAttributesEditorWidgetController,
        controllerAs: "jaewCtrl",
        bindToController: true
    }));
}
