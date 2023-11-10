interface JQuery {
    ///https://github.com/dreamerslab/jquery.actual
    actual(selector: string): number;
    modal(selector: string): void;
}

interface JQueryStatic {
    gritter: JQueryGritterStatic;

    ///https://github.com/dreamerslab/jquery.actual
    actual(selector: string): number;
    modal(selector: string): void;
}

interface JQueryGritterStatic {
    (): JQuery;
    options: any;
    add(extend): any;
}

interface String {
    startsWith(prefix: string): boolean;
    endsWith(suffix: string): boolean;
}
