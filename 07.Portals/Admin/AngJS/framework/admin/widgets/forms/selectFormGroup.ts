/**
 * @file framework/admin/widgets/forms/selectFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Select Form Group class
 */
module cef.admin.widgets.forms {
    export class SelectFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        includeNull: boolean;
        nullKey: string;
        nullText: string;
        selections: any[];
        selectionsNameKey: string;
        hideOptionKey: boolean;
        optionsAsValue: string;
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

    adminApp.directive("selectFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
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
            selectionsNameKey: "@?",
            optionsAsValue: "@?",
            includeNull: "@?",
            nullKey: "@?",
            nullText: "@?",
            hideOptionKey: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/admin/widgets/forms/selectFormGroup.html", "ui"),
        controller: SelectFormGroupController,
        controllerAs: "sFGCtrl",
        bindToController: true
    }));
}
