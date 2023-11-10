module cef.admin.core {
    export type ProductImportFieldMapping = { [field: string]: number };

    export interface ProductImportField {
        name: string; value: any; searchValue: string;
    }

    export interface StoreSheetsImporterCookie {
        selectedSheetId?: string;
        selectedDocumentId?: string;
        productImportMappings: {
            [sheetId: string]: {
                mappings: { [field: string]: number };
            }
        }
    }
}
