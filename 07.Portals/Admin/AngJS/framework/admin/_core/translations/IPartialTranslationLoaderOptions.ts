module cef.admin.core.translations {
    export interface IPartialTranslationLoaderOptions {
        key: string;
        urlTemplate: string | ng.translate.IUrlTemplateFunc;
        loadFailureHandler: any;
        __retries: number;
        $http: ng.IRequestConfig
    }
}
