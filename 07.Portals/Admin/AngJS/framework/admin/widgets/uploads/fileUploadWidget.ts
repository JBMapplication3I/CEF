/**
 * @file framework/admin/widgets/uploads/fileUploadWidget.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc File upload widget class
 */
module cef.admin.widgets.uploads {
    /** This is a generic directive that facilitates uploading files. */
    class UploadFileController extends core.TemplatedControllerBase {
        // Bound Scope Properties
        uploadType: string;
        text: string;
        allowMultiple: boolean;
        typeKey: string;
        uploadCallback: (...params) => void | any;
        master: api.HaveStoredFilesBaseModel<api.AmAStoredFileRelationshipTableModel>;
        form: ng.IFormController;
        // Functions
        createFileModel(name: string, fileName: string): api.AmAStoredFileRelationshipTableModel {
            return <api.AmAStoredFileRelationshipTableModel>{
                Active: true,
                CreatedDate: new Date(),
                Name: name,
                FileAccessTypeID: 0,
                MasterID: 0,
                SlaveID: 0,
                Slave: <api.StoredFileModel>{
                    Active: true,
                    CreatedDate: new Date(),
                    Name: name,
                    FileName: fileName,
                    IsStoredInDB: false
                }
            };
        }
        // Events
        // NOTE: This must remain an arrow function to maintain 'this' properly in the Kendo scope
        uploadedFile = (e: kendo.ui.UploadSuccessEvent): void => {
            if (!Array.isArray(this.master.StoredFiles)) { this.master.StoredFiles = []; }
            let added = false;
            if (e.response) {
                var r = e.response as api.IUploadResponse;
                if (r.UploadFiles && r.UploadFiles.length) {
                    r.UploadFiles.forEach(uFile => {
                        const name = uFile.FileName.substring(uFile.FileName.lastIndexOf('\\') + 1);
                        this.master.StoredFiles.push(this.createFileModel(name, name));
                        added = true;
                    });
                }
            }
            if (!added) {
                e.files.forEach(file => {
                    const model = <api.AmAStoredFileRelationshipTableModel>{
                        Active: true,
                        CreatedDate: new Date(),
                        Name: file.name,
                        FileAccessTypeID: 0,
                        MasterID: 0,
                        SlaveID: 0,
                        Slave: <api.StoredFileModel>{
                            Active: true,
                            CreatedDate: new Date(),
                            Name: file.name,
                            FileName: file.name,
                            IsStoredInDB: false
                        }
                    };
                    this.master.StoredFiles.push(model);
                });
            }
            if (angular.isFunction(this.uploadCallback)) {
                this.uploadCallback(this.master.StoredFiles);
            }
            if (this.form) {
                this.form.$setDirty();
            }
            this.$scope.$apply();
        };
        // NOTE: This must remain an arrow function to maintain 'this' properly in the Kendo scope
        uploadFile = (e: kendo.ui.UploadUploadEvent): void => {
            e.data = { EntityFileType: this.uploadType };
        };
        // Constructor
        constructor(
                private readonly $scope: ng.IScope,
                protected readonly cefConfig: core.CefConfig,
                readonly $translate: ng.translate.ITranslateService) {
            super(cefConfig);
            if (!this.text) {
                $translate("ui.admin.widgets.imageUploadWidget.DefaultMessage")
                    .then(translated => this.text = translated);
            }
            if (!this.allowMultiple) { this.allowMultiple = false; }
            if (!this.typeKey) { this.typeKey = "General"; }
        }
    }

    adminApp.directive("cefUploadFileWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            uploadType: "@",
            master: "=",
            uploadCallback: "=?",
            text: "=?",
            allowMultiple: "=?",
            form: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/uploads/fileUploadWidget.html", "ui"),
        controller: UploadFileController,
        controllerAs: "cefUploadFileWidgetCtrl",
        bindToController: true
    }));
}
