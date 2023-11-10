/**
 * @file framework/store/widgets/forms/textareaInputFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Text Input Form Group class
 */
module cef.store.widgets.forms {
    class TextareaFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        placeholderKey: string;
        placeholderText: string;
        minLength: number;
        maxLength: number;
        showCharacterCounter: boolean;
        rows: number;
        // Properties
        get inputPrefix(): string { return "ta"; }
        // Functions
        // <None at this level>
        // Events
        // <None at this level>
        // Constructor
        constructor(
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig) {
            super($translate, cefConfig);
        }
    }

    cefApp.directive("textareaFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
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
            minLength: "=?",
            maxLength: "=?",
            showCharacterCounter: "=?",
            rows: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/textareaFormGroup.html", "ui"),
        controller: TextareaFormGroupController,
        controllerAs: "taFGCtrl",
        bindToController: true
    }));
}
