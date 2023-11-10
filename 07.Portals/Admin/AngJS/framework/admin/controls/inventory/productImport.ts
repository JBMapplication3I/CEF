module cef.admin.controls.inventory {
    export interface IImporter {
        name: string;
    }

    class InventoryProductImportController extends core.TemplatedControllerBase {
        // Properties
        importers: IImporter[];
        importer: IImporter;
        // Functions
        // <None>
        // Events
        onImporterChange(): void {
            /*
            if (this.importer && this.importer.name === "Google Sheets") {
                this.googleLogin.checkAuth();
            }
            */
        }
        // Constructor
        constructor(
                protected readonly cefConfig: core.CefConfig/*,
                private readonly googleLogin: GoogleLoginService*/) {
            super(cefConfig);
            this.importers = [
                // NOTE: These should not be translated
                { name: "Excel" },
                // { name: "Google Sheets" }
            ];
            this.importer = this.importers[0];
        }
    }

    adminApp.directive("productImportGoogle", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        templateUrl: $filter("corsLink")("/framework/admin/controls/inventory/productImport.html", "ui"),
        controller: InventoryProductImportController,
        controllerAs: "inventoryProductImportCtrl"
    }));
}
