module cef.admin.controls.inventory {
    class ExcelImporterController extends core.TemplatedControllerBase {
        // Properies
        importLog: api.ImportResponse;
        sheetName: string;
        // Functions
        // NOTE: This must remain an arrow function to work with Kendo
        uploadSheet = (e) => {
            e.data = { EntityFileType: "ImportProduct" };
        }
        // NOTE: This must remain an arrow function to work with Kendo
        uploadedSheet = (e) => {
            this.sheetName = e.response.UploadFiles[0].FileName.split("\\").pop();;
        }
        importSheet() {
            this.$translate("ui.admin.sales.widgets.salesQuoteImporterExcel.ImportConfirm.Message.Template", { name: this.sheetName })
                .then(translated => this.cvConfirmModalFactory(translated as string).then(ok => {
                    if (!ok) { return; }
                    this.setRunning();
                    this.cvApi.products.ImportProductsFromExcel({ FileName: this.sheetName })
                        .then(r => {
                            this.importLog = r.data;
                            this.finishRunning(
                                this.importLog.ErrorMessages && this.importLog.ErrorMessages.length > 0,
                                null,
                                this.importLog.ErrorMessages);
                        }, result => this.finishRunning(true, result))
                        .catch(reason => this.finishRunning(true, reason));
                }));
        }
        importSEOSheet() {
            this.$translate("ui.admin.sales.widgets.salesQuoteImporterExcel.ImportConfirm.Message.Template", { name: this.sheetName })
                .then(translated => this.cvConfirmModalFactory(translated as string).then(ok => {
                    if (!ok) { return; }
                    this.setRunning();
                    // Your new endpoint here
                    this.cvApi.products.ImportProductKeywordsFromExcel({ FileName: this.sheetName })
                        .then(r => {
                            this.importLog = r.data;
                            this.finishRunning(
                                this.importLog.ErrorMessages && this.importLog.ErrorMessages.length > 0,
                                null,
                                this.importLog.ErrorMessages);
                        }, result => this.finishRunning(true, result))
                        .catch(reason => this.finishRunning(true, reason));
                }));
        }
        // Constructor
        constructor(
                private readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvApi: api.ICEFAPI,
                private readonly cvConfirmModalFactory: admin.modals.IConfirmModalFactory) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefExcelImporter", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: true,
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/excelImporter.html", "ui"),
        controller: ExcelImporterController,
        controllerAs: "excelCtrl"
    }));
}
