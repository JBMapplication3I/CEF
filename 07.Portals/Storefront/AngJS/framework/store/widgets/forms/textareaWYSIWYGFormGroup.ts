/**
 * @file framework/store/widgets/forms/textInputFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Kendo WYSIWYG Input Form Group class
 */
module cef.store.widgets.forms {
    class TextareaWYSIWYGFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        placeholderKey: string;
        placeholderText: string;
        maxLength: number;
        showCharacterCounter: boolean;
        // Properties
        get inputPrefix(): string { return "taw"; }
        // Functions
        sanitizeMarkup(e: any): void { e.html = e.html.replace(/style="([^"]*)"/g, ""); }
        // Events
        // <None at this level>
        // Constructor
        constructor(
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig) {
            super($translate, cefConfig);
        }
    }

    cefApp.directive("textareaWysiwygFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            // Base Properties
            formIdentifier: "@key",
            property: "=ngModel",
            options: "=?ngModelOptions",
            disabled: "=?ngDisabled",
            required: "=?ngRequired",
            labelKey: "@?",
            labelText: "@?",
            tooltipKey: "@?",
            tooltipText: "@?",
            forceTooltipWithFocus: "=?",
            inputClass: "@?",
            formClasses: "=?ngFormClass",
            whenChanged: "&?whenChanged",
            onBlur: "&?ngBlur",
            onFocus: "&?ngFocus",
            onKeydown: "&?ngKeydown",
            onKeypress: "&?ngKeypress",
            onKeyup: "&?ngKeyup",
            onMousedown: "&?ngMousedown",
            onMouseenter: "&?ngMouseenter",
            onMouseleave: "&?ngMouseleave",
            onMousemove: "&?ngMousemove",
            onMouseover: "&?ngMouseover",
            onMouseup: "&?ngMouseup",
            showValidTooltip: "=?",
            hideInvalidTooltip: "=?",
            failsOn: "=?",
            // Text-Specific Properties
            placeholderKey: "@?",
            placeholderText: "@?",
            maxLength: "@?",
            showCharacterCounter: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/textareaWYSIWYGFormGroup.html", "ui"),
        controller: TextareaWYSIWYGFormGroupController,
        controllerAs: "tawfgCtrl",
        bindToController: true
    }));
}
