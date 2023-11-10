module cef.store.core.translations {
    export interface IPart {
        name: string;
        isActive: boolean;
        priority: number;
        tables: { [lang: string]: ng.translate.ITranslationTable };
        langPromises: { [lang: string]: ng.IPromise<ng.translate.ITranslationTable> };
        urlTemplate: string | ng.translate.IUrlTemplateFunc;
        /**
         * @name parseUrl
         * @method
         * @desc Returns a parsed url template string and replaces given target lang and part name it.
         * @param {string|IUrlTemplateFunc} urlTemplate - Either a string containing an url pattern (with
         * '{part}' and '{lang}') or a function(part, lang) returning a string.
         * @param {string} targetLang - Language key for language to be used.
         * @return {string} Parsed url template string
         */
        parseUrl(
            urlTemplate: string | ng.translate.IUrlTemplateFunc,
            targetLang: string
            ): string;
        getTable(
            lang: string,
            keyStartsWith: string,
            $q: ng.IQService,
            $filter: ng.IFilterService,
            $http: ng.IHttpService,
            $window: ng.IWindowService,
            urlTemplate: string | ng.translate.IUrlTemplateFunc,
            errorHandler: (name: string, lang: string, errorRespone: any) => ng.IPromise<any>
            ): ng.IPromise<ng.translate.ITranslationTable>;
    }
}
