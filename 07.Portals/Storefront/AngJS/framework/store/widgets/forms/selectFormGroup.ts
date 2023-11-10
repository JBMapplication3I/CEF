/**
 * @file framework/store/widgets/forms/selectFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Select Form Group class
 */
module cef.store.widgets.forms {
    export class SelectFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        includeNull: boolean;
        nullKey: string;
        nullText: string;
        selections: any[];
        hideOptionKey: boolean;
        optionsAsValue: string;
        displayAttribute: string;
        hideValidation: boolean;
        // Properties
        get inputPrefix(): string { return "ddl"; }
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

    cefApp.directive("selectFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            // Base Properties
            formIdentifier: "@key",
            property: "=ngModel",
            options: "=?ngModelOptions",
            disabled: "=?ngDisabled",
            required: "=?ngRequired",
            autocomplete: "@?autoComp",
            labelKey: "@?",
            labelText: "@?",
            tooltipKey: "@?",
            tooltipText: "@?",
            forceTooltipWithFocus: "=?",
            leftIcon: "@?",
            rightIcon: "@?",
            inputClass: "@?",
            formClasses: "=?ngFormClass",
            whenChanged: "&?whenChanged",
            isAutofocus: "=?",
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
            // Select-Specific Properties
            selections: "=",
            optionsAsValue: "@?",
            displayAttribute: "@?",
            includeNull: "@?",
            nullKey: "@?",
            nullText: "@?",
            hideOptionKey: "=?",
            hideValidation: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/selectFormGroup.html", "ui"),
        controller: SelectFormGroupController,
        controllerAs: "sFGCtrl",
        bindToController: true
    }));
}
