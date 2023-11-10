/**
 * @file framework/admin/widgets/forms/passwordInputFormGroup.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Password Input Form Group class
 */
module cef.admin.widgets.forms {
    class PasswordInputFormGroupController extends InputFormGroupControllerBase {
        // Bound Scope Properties
        placeholderKey: string;
        placeholderText: string;
        minLength: number;
        maxLength: number;
        isNew: boolean;
        // Properties
        get inputPrefix(): string { return "pw"; }
        get buttonTitle(): string {
            return 'ui.admin.common.'
                + (this.showPassword ? 'Hide' : 'Show')
                + (this.isNew ? 'New' : 'Current')
                + 'Password';
        }
        protected showPassword: boolean;
        // Functions
        // <None at this level>
        // Events
        extraOnChange($event: ng.IAngularEvent): void {
            if (!this.isNew) {
                // Just check the required part
                this.control.$setValidity("required", this.control.$modelValue);
                if (angular.isFunction(this.whenChanged)) {
                    this.whenChanged({ $event: $event, control: this.control });
                }
                return;
            }
            this.control.$setValidity("password", true);
            // control.$modelValue has the new value, before it's been assigned to this.loginData.Password
            if (!this.control.$modelValue) {
                // All bad because it's empty
                this.control.$setValidity("required",  false);
                this.control.$setValidity("password",  false);
                this.control.$setValidity("pwminlength", false);
                this.control.$setValidity("pwoneupper",  false);
                this.control.$setValidity("pwonelower",  false);
                this.control.$setValidity("pwonenumber", false);
                this.control.$setValidity("pwbyserver",  true);
                return;
            }
            this.control.$setValidity("pwminlength", !(this.control.$modelValue.length < 7));
            this.control.$setValidity("pwoneupper",  /.*[A-Z].*/.test(this.control.$modelValue));
            this.control.$setValidity("pwonelower",  /.*[a-z].*/.test(this.control.$modelValue));
            this.control.$setValidity("pwonenumber", /.*\d.*/.test(this.control.$modelValue));
            if (this.control.$invalid) {
                // Don't bother to ask the server if we already know we're bad
                this.control.$setValidity("pwbyserver", true);
                this.control.$setValidity("password", false);
                if (angular.isFunction(this.whenChanged)) {
                    this.whenChanged({ $event: $event, control: this.control });
                }
                return;
            }
            this.cvAuthenticationService.validatePasswordIsGood({
                Password: this.control.$modelValue
            }).then(r => {
                if (!r || !r.data || !r.data.ActionSucceeded) {
                    this.control.$setValidity("pwbyserver", false);
                    return;
                }
                this.control.$setValidity("pwbyserver", true);
            }).catch(() => this.control.$setValidity("pwbyserver", false))
            .finally(() => {
                this.control.$setValidity("password", this.control.$valid);
                if (angular.isFunction(this.whenChanged)) {
                    this.whenChanged({ $event: $event, control: this.control });
                }
            });
        }
        // Constructor
        constructor(
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig,
                private readonly cvAuthenticationService: services.IAuthenticationService) {
            super($translate, cefConfig);
        }
    }

    adminApp.directive("passwordInputFormGroup", ($filter: ng.IFilterService): ng.IDirective => ({
        restrict: "EA",
        scope: {
            // Base Properties
            formIdentifier: "@key",
            property: "=ngModel",
            autocomplete: "@?autoComp",
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
            isNew: "=?"
        },
        replace: true, // NOTE: This is required for layout purposes
        templateUrl: $filter("corsLink")("/framework/admin/widgets/forms/passwordInputFormGroup.html", "ui"),
        controller: PasswordInputFormGroupController,
        controllerAs: "pwIFGCtrl",
        bindToController: true
    }));
}
