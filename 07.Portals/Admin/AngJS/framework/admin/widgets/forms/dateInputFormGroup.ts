/**
 * @file framework/admin/widgets/forms/dateInputFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Date Input Form Group class
 */
module cef.admin.widgets.forms {
    class DateInputFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        placeholderKey: string;
        placeholderText: string;
        min: Date;
        max: Date;
        includeTime: boolean;
        // Properties
        get inputPrefix(): string { return "date"; }
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

    adminApp.directive("dateInputFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
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
            // Date-Specific Properties
            placeholderKey: "@?",
            placeholderText: "@?",
            min: "=?",
            max: "=?",
            includeTime: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/admin/widgets/forms/dateInputFormGroup.html", "ui"),
        controller: DateInputFormGroupController,
        controllerAs: "difgCtrl",
        bindToController: true
    }));
}
