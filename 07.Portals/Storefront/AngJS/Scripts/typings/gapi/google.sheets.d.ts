declare namespace google.sheets {
    export interface Object {
        $t: string;
    }
    export interface TypedObject extends Object {
        type: string; // "text"
    }
    //
    export interface Content extends TypedObject {
        // $t: string; // "CVI Marketing - Gameplan" // Inherited
        // type: string; // "text" // Inherited
    }
    export interface Title extends TypedObject {
        // $t: string; // "CVI Marketing - Gameplan" // Inherited
        // type: string; // "text" // Inherited
    }
    export interface Date extends Object {
        // $t: string; // "2017-02-10T02:23:19.562Z" // Inherited
    }
    export interface ID extends Object {
        // $t: string; // "https://spreadsheets.google.com/feeds/spreadsheets/private/full/1BC-rSTUxGR_rYNrBpP0E2w6AWYIkuZqh7CvjHQ31tKM" // Inherited
    }
    export interface Author {
        email: { $t: string }; // "chrisreddick7@gmail.com"
        name: { $t: string }; // "chrisreddick7"
    }
    export interface Category {
        scheme: string; // "http://schemas.google.com/spreadsheets/2006"
        term: string; // "http://schemas.google.com/spreadsheets/2006#spreadsheet"
    }
    export interface Link {
        href: string; // "https://spreadsheets.google.com/feeds/worksheets/1BC-rSTUxGR_rYNrBpP0E2w6AWYIkuZqh7CvjHQ31tKM/private/full"
        rel: string; // "http://schemas.google.com/spreadsheets/2006#worksheetsfeed"
        type: string; // "application/atom+xml"
    }
    export interface Entry {
        author: Author[];
        category: Category[];
        content: Content;
        gs$colCount: Object; // $t: "23"
        gs$rowCount: Object; // $t: "98"
        gsx$_chk2m: Object; // $t: "Activity(ies)"
        gsx$_ciyn3: Object; // $t: "Desired Result(s)"
        gsx$_ckd7g: Object; // $t: "Review Method(s)"
        gsx$_clrrx: Object; // $t: "Status"
        gsx$_cokwr: Object; // $t: "Category"
        gsx$_cpzh4: Object; // $t: "Item"
        gsx$_cre1l: Object; // $t: "Overview"
        gsx$_cyevm: Object; // $t: "Notes"
        gsx$_cztg3: Object; // $t: "Detailed Description"
        id: ID;
        link: Link[];
        title: Title;
        updated: Date;
    }
    export interface NamedEntry {
        entry: Entry;
        name: string;
    }
    //
    export interface GoogleSheetsDocumentDetailsReturnObjectFeed {
        author: Author[];
        category: Category[];
        entry: Entry[];
        id: ID;
        link: Link[];
        openSearch$startIndex: Object; // $t: "1"
        openSearch$totalResults: Object; // $t: "53"
        title: Title;
        updated: Date;
        xmlns: string; // "http://www.w3.org/2005/Atom"
        xmlns$gs: string; // "http://schemas.google.com/spreadsheets/2006"
        xmlns$openSearch: string; // "http://a9.com/-/spec/opensearchrss/1.0/"
    }
    export interface GoogleSheetsDocumentDetailsReturnObject {
        encoding: string; // "UTF-8"
        feed: GoogleSheetsDocumentDetailsReturnObjectFeed;
        version: string; // "1.0"
    }
    export interface GoogleSheetsDocumentSheetDetailsReturnObjectFeed {
        author: Author[];
        category: Category[];
        entry: Entry[];
        id: ID;
        link: Link[];
        openSearch$startIndex: Object; // $t: "1"
        openSearch$totalResults: Object; // $t: "51"
        title: Title;
        updated: Date;
        xmlns: string; // "http://www.w3.org/2005/Atom"
        xmlns$gsx: string; // "http://schemas.google.com/spreadsheets/2006/extended"
        xmlns$openSearch: string; // "http://a9.com/-/spec/opensearchrss/1.0/"
    }
    export interface GoogleSheetsDocumentSheetDetailsReturnObject {
        encoding: string; // "UTF-8"
        feed: GoogleSheetsDocumentSheetDetailsReturnObjectFeed;
        version: string; // "1.0"
    }
}