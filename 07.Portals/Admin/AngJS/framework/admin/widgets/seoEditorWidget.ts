module cef.admin.api {
    export interface IHaveSeoBaseModel {
        /** SEO Keywords to use in the Meta tags of the page for this object */
        SeoKeywords?: string;
        /** SEO URL to use to link to the page for this object */
        SeoUrl?: string;
        /** SEO Description to use in the Meta tags of the page for this object */
        SeoDescription?: string;
        /** SEO General Meta Data to use in the Meta tags of the page for this object */
        SeoMetaData?: string;
        /** SEO Page Title to use in the Meta tags of the page for this object */
        SeoPageTitle?: string;
    }
}

module cef.admin {
    class SeoEditorController extends core.TemplatedControllerBase {
        // Properties
        master: api.IHaveSeoBaseModel; // Bound by Scope
        seoUrlRegex = /^[A-Za-z0-9-]+$/;
        // Functions
        // <None>
        // Constructors
        constructor(protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }

    adminApp.directive("cefSeoEditor", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: { master: "=" },
        templateUrl: $filter("corsLink")("/framework/admin/widgets/seoEditorWidget.html", "ui"),
        controller: SeoEditorController,
        controllerAs: "seoEditorCtrl",
        bindToController: true
    }));
}
