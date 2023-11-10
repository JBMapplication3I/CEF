/**
 * @file admin/controls/sales/salesQuoteImport.ts
 * @since 2018-02-27
 * @version 4.7
 * @author Clarity Ventures, Inc. Copyright 2018. All rights reserved.
 * @desc The page control (container) for the Sales Quote Importer in CEF Admin
 */
module cef.admin.controls.sales {
    class SalesQuoteImportController extends core.TemplatedControllerBase {
        // Properties
        importers: inventory.IImporter[];
        selected: inventory.IImporter;
        // Functions
        // <None>
        // Events
        onImporterChange() {
            /*
            if (this.selected && this.selected.name === "Google Sheets") {
                this.googleLogin.checkAuth();
            }
            */
        }
        // Constructors
        constructor(
                ////private readonly googleLogin: GoogleLoginService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
            this.importers = [
                { name: "Excel" }/*,
                { name: "Google Sheets" } */
            ];
            this.selected = this.importers[0];
        }
    }

    adminApp.directive("cefQuoteImportExcel", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "A",
        templateUrl: $filter("corsLink")("/framework/admin/controls/sales/salesQuoteImport.html", "ui"),
        controller: SalesQuoteImportController,
        controllerAs: "salesQuoteImportCtrl"
    }));
}
