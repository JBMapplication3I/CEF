/**
 * @file framework/admin/controls/system/importExportMappingDetail.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Import/Export Mapping Detail class
 */
module cef.admin {
    class ImportExportMappingDetailController extends DetailBaseController<api.ImportExportMappingModel> {
        // Forced overrides
        detailName = "Import Export Mapping";
        // Collections
        types: api.TypeModel[];
        stores: api.StoreModel[] = [];
        // UI Data
        forms: {
            "Details": ng.IFormController
        }
        /** JSON schema data string */
        schema: string;
        /** Results from the validation */
        validationResults: string;
        /**
         * When validation fails, assigns this value to set the
         * text area where the problem is
         */
        mappingCaret: number;
        // Required Functions
        loadNewRecord(): ng.IPromise<api.ImportExportMappingModel> {
            this.record = <api.ImportExportMappingModel>{
                // Base Properties
                ID: 0,
                CustomKey: null,
                Active: true,
                CreatedDate: new Date(),
                UpdatedDate: null,
                // HaveJsonAttributesBase Properties
                SerializableAttributes: new api.SerializableAttributesDictionary,
                // NameableBase Properties
                Name: null,
                Description: null,
                // Import Export Mapping Properties
                MappingJson: null,
                MappingJsonHash: null
            };
            return this.$q.resolve(this.record);
        }
        constructorPreAction(): ng.IPromise<void> { this.detailName = "ImportExportMapping"; return this.$q.resolve(); }
        loadRecordCall(id: number): ng.IHttpPromise<api.ImportExportMappingModel> { return this.cvApi.structure.GetImportExportMappingByID(id); }
        createRecordCall(routeParams: api.ImportExportMappingModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.structure.CreateImportExportMapping(routeParams); }
        updateRecordCall(routeParams: api.ImportExportMappingModel): ng.IHttpPromise<api.CEFActionResponseT<number>> { return this.cvApi.structure.UpdateImportExportMapping(routeParams); }
        deactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.DeactivateImportExportMappingByID(id); }
        reactivateRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.ReactivateImportExportMappingByID(id); }
        deleteRecordCall(id: number): ng.IHttpPromise<api.CEFActionResponse> { return this.cvApi.structure.DeleteImportExportMappingByID(id); }
        // Supportive Functions
        validateMappingAgainstSchema() {
            if (!this.schema || !this.record.MappingJson) {
                this.$translate("ui.admin.controls.system.importExportMappingDetail.Errors.MissingSchema")
                    .then(translated => this.validationResults = translated as string);
                return;
            }
            try {
                const env: djsv.Environment = new djv();
                // Use `addSchema` to add json-schema
                env.addSchema("inline-paste", angular.fromJson(this.schema));
                const result = env.validate(
                    "inline-paste#/common",
                    angular.fromJson(this.record.MappingJson));
                if (result) {
                    // There was an issue
                    this.validationResults = result;
                    return;
                }
                this.$translate("ui.admin.controls.system.importExportMappingDetail.ValidatedSuccessfully")
                        .then(translated => this.validationResults = translated as string);
            } catch (e) {
                this.validationResults = (e as Error).message;
                let point = e.message.indexOf("at position ");
                if (point !== -1) {
                    point += "at position ".length;
                    let sub = e.message.substr(point, 10);
                    if (sub.indexOf(" ") !== -1) {
                        sub = sub.substr(0, sub.indexOf(" "));
                    }
                    this.mappingCaret = Number(sub);
                }
            }
        }
        // Constructor
        constructor(
                public readonly $scope: ng.IScope,
                public readonly $translate: ng.translate.ITranslateService,
                public readonly $stateParams: ng.ui.IStateParamsService,
                public readonly $state: ng.ui.IStateService,
                public readonly $window: ng.IWindowService,
                public readonly $q: ng.IQService,
                protected readonly cefConfig: core.CefConfig,
                public readonly cvApi: api.ICEFAPI,
                public readonly cvConfirmModalFactory: modals.IConfirmModalFactory,
                public readonly $filter: ng.IFilterService) {
            super($scope, $q, $filter, $window, $state, $stateParams, $translate, cefConfig, cvApi, cvConfirmModalFactory);
        }
    }

    adminApp.directive("importExportMappingDetail", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/system/importExportMappingDetail.html", "ui"),
        controller: ImportExportMappingDetailController,
        controllerAs: "importExportMappingDetailCtrl"
    }));

    adminApp.directive("cefControlledCaret", (): ng.IDirective => ({
        scope: {
            value: "=ngModel",
            caret: "="
        },
        link(scope: ng.IScope, element: ng.IAugmentedJQuery, attrs) {
            const watcher = (newValue: string | number, oldValue: string | number) => {
                if (!element || !newValue || newValue === oldValue) {
                    return;
                }
                var value = String(scope["value"]);
                var caret = Number(scope["caret"]);
                if (value.length <= (caret + 1)) {
                    return;
                }
                // Cross-browser compatibility (different browsers, different function calls)
                if (element["createTextRange"]) {
                    const range = element["createTextRange"]();
                    range.move("character", caret);
                    range.select();
                    return;
                }
                element.focus();
                if (element["setSelectionRange"]) {
                    element["setSelectionRange"](caret, caret);
                    return;
                }
            };
            scope.$watch("value", watcher);
            scope.$watch("caret", watcher);
        }
    }));
}
