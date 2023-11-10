/**
 * @file framework/store/widgets/forms/textInputFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Text Input Form Group class
 */
module cef.store.widgets.forms {
    class TextInputFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        placeholderKey: string;
        placeholderText: string;
        minLength: number;
        maxLength: number;
        isEmail: boolean;
        isPhone: boolean;
        isFax: boolean;
        isUsername: boolean;
        // Properties
        get inputPrefix(): string { return this.isEmail ? "em" : (this.isPhone || this.isFax) ? "tel" : "txt"; }
        get inputType(): string { return this.isEmail ? "email" : (this.isPhone || this.isFax) ? "tel" : "text"; }
        // Functions
        // <None at this level>
        // Events
        // <None at this level>
        // Constructor
        constructor(
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig) {
            super($translate, cefConfig);
            this.doStartTouched();
        }
    }

    cefApp.directive("textInputFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
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
            startTouched: "=?",
            // Text-Specific Properties
            placeholderKey: "@?",
            placeholderText: "@?",
            minLength: "=?",
            maxLength: "=?",
            isEmail: "=?",
            isPhone: "=?",
            isFax: "=?",
            isUsername: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/store/widgets/forms/textInputFormGroup.html", "ui"),
        controller: TextInputFormGroupController,
        controllerAs: "tIFGCtrl",
        bindToController: true
    }));

    cefApp.directive("ngAutocomplete", () => ({
        restrict: "A",
        // Intentionally No Template
        link: <ng.IDirectivePrePost>{
            pre: (
                scope: ng.IScope,
                instanceElement: ng.IAugmentedJQuery,
                instanceAttributes: ng.IAttributes,
                controller: any,
                transclude: ng.ITranscludeFunction
            ): void => {
                ////if (instanceAttributes.id !== "txtStreet1") { return; }
                const debug = `ngAutocomplete link test pre: ${instanceAttributes.id}`;
                ////console.log(debug);
                if (scope.tIFGCtrl) {
                    ////console.log(`${debug} scope is tIFGCtrl`);
                    const ctrl = scope.tIFGCtrl as TextInputFormGroupController;
                    if (ctrl.isUsername) {
                        ////console.log(`${debug} is username, setting autocomplete to 'username'`);
                        instanceAttributes.autocomplete = "username";
                        instanceElement.attr("autocomplete", instanceAttributes.autocomplete)
                    } else if (ctrl.isEmail) {
                        ////console.log(`${debug} is email, setting autocomplete to 'email'`);
                        instanceAttributes.autocomplete = "email";
                        instanceElement.attr("autocomplete", instanceAttributes.autocomplete)
                    } else if (ctrl.isPhone) {
                        ////console.log(`${debug} is phone, setting autocomplete to 'tel'`);
                        instanceAttributes.autocomplete = "tel";
                        instanceElement.attr("autocomplete", instanceAttributes.autocomplete)
                    } else if (ctrl.isFax) {
                        ////console.log(`${debug} is fax, setting autocomplete to 'fax'`);
                        instanceAttributes.autocomplete = "fax";
                        instanceElement.attr("autocomplete", instanceAttributes.autocomplete)
                    } else if (ctrl.autocomplete) {
                        ////console.log(`${debug} has autocomplete value, setting autocomplete to '${ctrl.autocomplete}'`);
                        instanceAttributes.autocomplete = ctrl.autocomplete;
                        instanceElement.attr("autocomplete", instanceAttributes.autocomplete);
                    } else {
                        ////console.log(`${debug} doesn't have an autocomplete value, removing the attribute`);
                        ////delete instanceAttributes.autocomplete;
                        instanceElement.removeAttr("autocomplete");
                        instanceElement.removeAttr("ngAutocomplete");
                        instanceElement.removeAttr("ng-autocomplete");
                    }
                }
                if (scope.sFGCtrl) {
                    ////console.log(`${debug} scope is sFGCtrl`);
                    const ctrl = scope.sFGCtrl as SelectFormGroupController;
                    if (ctrl.autocomplete) {
                        ////console.log(`${debug} has autocomplete value, setting autocomplete to '${ctrl.autocomplete}'`);
                        instanceAttributes.autocomplete = ctrl.autocomplete;
                        instanceElement.attr("autocomplete", instanceAttributes.autocomplete);
                    } else {
                        ////console.log(`${debug} doesn't have an autocomplete value, removing the attribute`);
                        ////delete instanceAttributes.autocomplete;
                        instanceElement.removeAttr("autocomplete");
                        instanceElement.removeAttr("ngAutocomplete");
                        instanceElement.removeAttr("ng-autocomplete");
                    }
                }
            }
        }
    }));

    cefApp.directive("ngAutofocus", () => ({
        restrict: "A",
        // Intentionally No Template
        link: <ng.IDirectivePrePost>{
            pre: (
                scope: ng.IScope,
                instanceElement: ng.IAugmentedJQuery,
                instanceAttributes: ng.IAttributes,
                controller: any,
                transclude: ng.ITranscludeFunction
            ): void => {
                const debug = `ngAutofocus link test pre: ${instanceAttributes.id}`;
                ////console.log(debug);
                if (scope.tIFGCtrl) {
                    ////console.log(`${debug} scope is tIFGCtrl`);
                    const ctrl = scope.tIFGCtrl as TextInputFormGroupController;
                    if (ctrl.isAutofocus) {
                        ////console.log(`${debug} is autofocus, setting autofocus attribute to 'true'`);
                        ////instanceAttributes.autofocus = "true";
                        instanceElement.attr("autofocus", "true")
                    } else {
                        ////console.log(`${debug} doesn't have an autofocus value, removing the attribute`);
                        ////delete instanceAttributes.autofocus;
                        instanceElement.removeAttr("autofocus");
                        instanceElement.removeAttr("ngAutofocus");
                        instanceElement.removeAttr("ng-autofocus");
                    }
                }
                if (scope.sFGCtrl) {
                    ////console.log(`${debug} scope is sFGCtrl`);
                    const ctrl = scope.sFGCtrl as SelectFormGroupController;
                    if (ctrl.isAutofocus) {
                        ////console.log(`${debug} is autofocus, setting autofocus attribute to 'true'`);
                        ////instanceAttributes.autofocus = "true";
                        instanceElement.attr("autofocus", "true")
                    } else {
                        ////console.log(`${debug} doesn't have an autofocus value, removing the attribute`);
                        ////delete instanceAttributes.autofocus;
                        instanceElement.removeAttr("autofocus");
                        instanceElement.removeAttr("ngAutofocus");
                        instanceElement.removeAttr("ng-autofocus");
                    }
                }
            }
        }
    }));
}
