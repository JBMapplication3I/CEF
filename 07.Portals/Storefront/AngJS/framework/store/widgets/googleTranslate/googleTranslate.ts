module cef.store.widgets.googleTranslate {
    cefApp.directive("cefGoogleTranslate", ($window: ng.IWindowService, $timeout: ng.ITimeoutService): ng.IDirective => ({
        restrict: "EA",
        scope: { pageLanguage: "@", loadScript: "=?" },
        // NOTE: This is a control-specific template which must remain inline
        template: `<div id="google_translate_element"></div>`,
        link: function ($scope: ng.IScope, el) {
            function googleTranslateElementInit() {
                if ($window.google && $window.google.translate) {
                    const instance = new $window.google.translate.TranslateElement({
                        pageLanguage: $scope.pageLanguage || "en",
                        layout: $window.google.translate.TranslateElement.InlineLayout.SIMPLE
                    }, "google_translate_element");
                } else {
                    console.warn("Google Translate Directive: Initialization of the Google Translate script failed. You may need to add the script tag manually. See source code for details.");
                }
            }
            /**
             * NOTES ON SCRIPT LOADING
             * With multiple scripts loading on a page in unpredictible order, attaching the
             * translate script tag to the DOM from within the directive often fails.
             * If loading fails you will need to add the script tag before the directive.
             * Right above it tends to work fine.
             * <script src="//translate.google.com/translate_a/element.js"></script>
             * Then set load-script="false" as a directive attribute.
             */ 
            if (!!$scope.loadScript) {
                el.prepend(`<script src="//translate.google.com/translate_a/element.js"></script>`);
            }
            $timeout(googleTranslateElementInit, 1500);
        }
    }));
}
