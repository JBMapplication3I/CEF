/**
 * @file framework/store/widgets/forms/toggleFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Toggle Form Group class
 */
module cef.store.widgets.forms {
    class ToggleFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        togOnKey: string;
        togOnText: string;
        togOnStyle: string;
        togOffKey: string;
        togOffText: string;
        togOffStyle: string;
        // Properties
        get inputPrefix(): string { return "tog"; }
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

    cefApp.directive("toggleFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            // Base Properties
            formIdentifier: "@key",
            property: "=ngModel",
            disabled: "=?ngDisabled",
            required: "=?ngRequired",
            whenChanged: "&?whenChanged",
            labelKey: "@?",
            labelText: "@?",
            tooltipKey: "@?",
            tooltipText: "@?",
            forceTooltipWithFocus: "=?",
            inputClass: "@?",
            formClasses: "=?ngFormClass",
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
            // Toggle-Specific Properties
            togOnKey: "@?",
            togOnText: "@?",
            togOnStyle: "@?",
            togOffKey: "@?",
            togOffText: "@?",
            togOffStyle: "@?",
            value: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/toggleFormGroup.html", "ui"),
        controller: ToggleFormGroupController,
        controllerAs: "tgfgCtrl",
        bindToController: true
    }));
}
