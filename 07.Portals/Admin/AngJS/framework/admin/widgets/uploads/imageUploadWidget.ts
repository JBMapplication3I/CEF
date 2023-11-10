/**
 * @file framework/admin/widgets/uploads/imageUploadWidget.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Image upload widget class
 */
module cef.admin.widgets.uploads {
    /** This is a generic directive that facilitates uploading IMAGE files. */
    class UploadImageController extends core.TemplatedControllerBase {
        // Properties
        uploadType: string; // Bound by Scope
        text: string; // Bound by Scope
        allowMultiple: boolean; // Bound by Scope
        typeKey: string; // Bound by Scope
        uploadCallback: (...params) => void | any; // Bound by Scope
        master: { Images: api.IImageBaseModel[]; }; // Bound by Scope
        isPrimary: boolean; // Bound by Scope
        // Functions
        createFileModel(name: string, fileName: string): api.ProductImageModel {
            return <api.ProductImageModel>{
                Active: true,
                CreatedDate: new Date(),
                IsPrimary: this.isPrimary || false,
                Name: name,
                TypeID: 0,
                TypeKey: this.typeKey,
                ThumbnailIsStoredInDB: false,
                OriginalIsStoredInDB: false,
                OriginalFileName: fileName
            };
        }
        // NOTE: This must remain an arrow function to maintain 'this' properly in the Kendo scope
        uploadedFile = (e: kendo.ui.UploadSuccessEvent): void => {
            const files = e.files;
            files.forEach(file => {
                const model = <api.IImageBaseModel>{
                    ID: 0,
                    Active: true,
                    CreatedDate: new Date(),
                    IsPrimary: this.isPrimary,
                    Name: file.name,
                    TypeID: 0,
                    TypeKey: this.typeKey,
                    ThumbnailIsStoredInDB: false,
                    OriginalIsStoredInDB: false,
                    OriginalFileName: file.name
                };
                if (!this.allowMultiple
                    && _.some(this.master.Images, { TypeKey: this.typeKey, Active: true })) {
                    this.master.Images.forEach(img => {
                        if (img.TypeKey === this.typeKey) {
                            img.Active = false;
                        }
                    });
                }
                if (this.isPrimary) {
                    this.master.Images.forEach(img => {
                        if (img.IsPrimary) {
                            img.IsPrimary = false;
                        }
                    });
                }
                if (!this.master.Images) { this.master.Images = []; }
                if (!this.master.Images.length) {
                    model.IsPrimary = true;
                }
                this.master.Images.push(model);
            });
            if (angular.isFunction(this.uploadCallback)) {
                this.uploadCallback(this.master.Images);
            }
            this.finishRunning();
            this.$scope.$apply();
        };
        // NOTE: This must remain an arrow function to maintain 'this' properly in the Kendo scope
        uploadFile = (e: kendo.ui.UploadUploadEvent): void => {
            if (!e.files) {
                e.preventDefault();
                return;
            }
            if (_.some(e.files, x => /\s/gi.test(x.name))) {
                alert("ERROR! Image files cannot have any spaces in the name. Please correct the file names and retry.");
                e.preventDefault();
                return;
            }
            e.data = { EntityFileType: this.uploadType };
        };

        constructor(
                private readonly $scope: ng.IScope,
                readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            if (!this.text) {
                $translate("ui.admin.widgets.imageUploadWidget.DefaultMessage")
                    .then(translated => this.text = translated);
            }
            if (!this.allowMultiple) { this.allowMultiple = false; }
            if (!this.typeKey) { this.typeKey = "General"; }
            if (!this.isPrimary) { this.isPrimary = false; }
        }
    }

    adminApp.directive("cefUploadImageWidget", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            uploadType: "@",
            master: "=",
            uploadCallback: "=?",
            text: "=?",
            allowMultiple: "=?",
            typeKey: "@?",
            isPrimary: "=?"
        },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/uploads/imageUploadWidget.html", "ui"),
        controller: UploadImageController,
        controllerAs: "cefUploadImageWidgetCtrl",
        bindToController: true
    }));
}
