/**
 * @file framework/store/widgets/forms/numberInputFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Number Input Form Group class
 */
module cef.store.widgets.forms {
    class NumberInputFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        placeholderKey: string;
        placeholderText: string;
        min: number;
        max: number;
        step: number;
        precision: number;
        isCurrency: boolean;
        isPercent: boolean;
        // Properties
        get inputPrefix(): string { return "nud"; }
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

    cefApp.directive("numberInputFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
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
            // Number-Specific Properties
            placeholderKey: "@?",
            placeholderText: "@?",
            min: "=?",
            max: "=?",
            step: "=?",
            precision: "=?",
            isCurrency: "=?",
            isPercent: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/numberInputFormGroup.html", "ui"),
        controller: NumberInputFormGroupController,
        controllerAs: "nifgCtrl",
        bindToController: true
    }));
}
