/**
 * @file framework/store/bulkUpload/mapping/uploadMapping.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Upload mapping class
 */
module cef.store.widgets.uploadMapping {
    class UploadMappingController extends core.TemplatedControllerBase {
        // Properties
        uploadType: string; // Bound by Scope
        data: any; // Bound by Scope
        viewstate = { processing: false };
        items: any;
        fileList: any;
        master: api.HaveStoredFilesBaseModel<api.AmAStoredFileRelationshipTableModel>; // Bound by Scope
        uploadCallback: (...params) => void | any; // Bound by Scope
        // Functions
        createStoredFileModel(name: string, fileName: string): api.StoredFileModel {
            return <api.StoredFileModel>{
                ID: null,
                Active: true,
                CreatedDate: new Date(),
                Name: name,
                OriginalFileName: fileName,
                IsStoredInDB: false
            };
        }
        createFileModel(name: string, fileName: string): api.AmAStoredFileRelationshipTableModel {
            return <api.AmAStoredFileRelationshipTableModel>{
                Active: true,
                CreatedDate: new Date(),
                Name: name,
                FileAccessTypeID: null,
                MasterID: null,
                SlaveID: null,
                Slave: <api.StoredFileModel>{
                    Active: true,
                    CreatedDate: new Date(),
                    Name: name,
                    FileName: fileName,
                    IsStoredInDB: false
                }
            };
        }
        // NOTE: This must remain an arrow function to maintain 'this' properly in the Kendo scope
        pushFile = (file): void => {
            if (!this.fileList) { this.fileList = []; }
            this.fileList.push(file);
        };
        // NOTE: This must remain an arrow function to maintain 'this' properly in the Kendo scope
        uploadedFile = (e: kendo.ui.UploadSuccessEvent): void => {
            function assignName(array: any) {
                array.forEach((params: any, index: any) => {
                    params.data = {
                        name: params[index],
                        assignment: null
                    }
                });
            }
            if (!this.master || !this.master.StoredFiles) {
                this.master = { StoredFiles: [] }
            };
            let added = false;
            if (e.response) {
                var r = e.response as api.IUploadResponse;
                if (r.UploadFiles && r.UploadFiles.length) {
                    r.UploadFiles.forEach(uFile => {
                        const name = uFile.FileName.substring(uFile.FileName.lastIndexOf('\\') + 1);
                        this.master.StoredFiles.push(this.createFileModel(name, name));
                        this.$rootScope.$broadcast('BulkUploadFileName', name);
                        this.cvApi.shopping.GetFileHeaders({ FileName: name }).then(response => {
                            this.data = {
                                headers: response.data,
                                name: name
                            };
                            assignName([this.data.headers.FileHeaders, this.data.headers.SystemHeaders]);
                        });
                        added = true;
                    });
                    this.viewstate.processing = false;
                }
            }
            if (!added) {
                e.files.forEach(file => {
                    const model = <api.AmAStoredFileRelationshipTableModel>{
                        Active: true,
                        CreatedDate: new Date(),
                        Name: file.name,
                        FileAccessTypeID: null,
                        MasterID: null,
                        SlaveID: null,
                        Slave: <api.StoredFileModel>{
                            Active: true,
                            CreatedDate: new Date(),
                            Name: file.name,
                            FileName: file.name,
                            IsStoredInDB: false
                        }
                    };
                    const fileName = e.response.UploadFiles[0].FileName.split("\\").pop();
                    this.$rootScope.$broadcast('BulkUploadFileName', fileName);
                    this.cvApi.shopping.GetFileHeaders({ FileName: fileName }).then(response => {
                        this.data = {
                            headers: response.data,
                            name: fileName
                        };
                        assignName([this.data.headers.FileHeaders, this.data.headers.SystemHeaders]);
                    });
                    this.master.StoredFiles.push(model);
                    this.viewstate.processing = false;
                });
            }
            if (angular.isFunction(this.uploadCallback)) {
                this.uploadCallback();
            }
            this.$scope.$apply();
        };
        // NOTE: This must remain an arrow function to maintain 'this' properly in the Kendo scope
        uploadFile = (e: kendo.ui.UploadUploadEvent): void => {
            this.viewstate.processing = true;
            e.data = { EntityFileType: this.uploadType };
        };
        // Constructors
        constructor(
                private readonly $scope: ng.IScope,
                private readonly $rootScope: ng.IRootScopeService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI) {
            super(cefConfig);
        }
    }

    cefApp.directive("cefUploadMapping", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        scope: {
            mappingObject: "=",
            uploadType: "@",
            master: "=",
            uploadCallback: "=?",
            text: "=?",
            allowMultiple: "=?",
            data: "="
        },
        templateUrl: $filter("corsLink")("/framework/store/widgets/uploadMapping/uploadMapping.html", "ui"),
        controller: UploadMappingController,
        controllerAs: "cefUploadMappingCtrl",
        bindToController: true
    }));
}
