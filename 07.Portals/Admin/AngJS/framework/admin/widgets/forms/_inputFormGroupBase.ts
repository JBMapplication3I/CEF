/**
 * @file framework/admin/widgets/forms/_inputformGroupBase.ts
 * @author Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
 * @desc Input Form Group base class
 */
module cef.admin.widgets.forms {
    export abstract class InputFormGroupControllerBase extends core.TemplatedControllerBase {
        // Bound Scope Properties
        formIdentifier: string;
        property: any;
        autocomplete: string;
        labelKey: string;
        labelText: string;
        tooltipKey: string;
        tooltipText: string;
        forceTooltipWithFocus: boolean;
        leftIcon: string;
        rightIcon: string;
        disabled: boolean;
        required: boolean;
        inputClass: string;
        formClasses: string;
        whenChanged: Function;
        isAutofocus: boolean;
        onBlur: Function;
        onFocus: Function;
        onKeydown: Function;
        onKeypress: Function;
        onKeyup: Function;
        onMousedown: Function;
        onMouseenter: Function;
        onMouseleave: Function;
        onMousemove: Function;
        onMouseover: Function;
        onMouseup: Function;
        options: any = <ng.INgModelOptions>{ // ng-model-options
            updateOn: "default blur",
            debounce: { default: 500, blur: 0 },
            allowInvalid: false
        };
        showValidTooltip: boolean;
        hideInvalidTooltip: boolean;
        failsOn: boolean;
        startTouched: boolean;
        // Properties
        tooltipIsOpen = false;
        abstract get inputPrefix(): string;
        get tooltip() {
            if (!this.tooltipKey && !this.tooltipText) {
                return null;
            }
            if (!this.tooltipKey) {
                return this.tooltipText;
            }
            const translated = this.$translate.instant(this.tooltipKey) as string;
            if (translated === this.tooltipKey) {
                return this.tooltipText;
            }
            return translated;
        }
        get applyIsValidClass(): boolean {
            return (this.formValid && !this.failsOn)
                && (this.required || this.property) // Don't show for an empty input that isn't required
                && (this.property || this.formDirty || this.formTouched || this.formSubmitted);
        }
        get applyIsInvalidClass(): boolean {
            return (this.formInvalid || this.failsOn)
                && (this.property || this.formDirty || this.formTouched || this.formSubmitted);
        }
        get formValid(): boolean {
            return this.control && this.control.$valid;
        }
        get formInvalid(): boolean {
            return this.control && this.control.$invalid;
        }
        get formError(): boolean {
            return this.control && this.control.$error;
        }
        get formSuccess(): boolean {
            return this.control && this.control.$$success;
        }
        get formDirty(): boolean {
            return this.control && this.control.$dirty;
        }
        get formTouched(): boolean {
            return this.control && this.control.$touched;
        }
        get formSubmitted(): boolean {
            return this.form.$$submitted;
        }
        get control(): ng.INgModelController {
            return <ng.INgModelController>this.forms[this.formIdentifier][this.inputName];
        }
        get form(): ng.IFormController {
            return this.forms[this.formIdentifier];
        }
        get inputName(): string {
            return this.inputPrefix + this.formIdentifier;
        }
        // Functions
        doStartTouched(): void {
            if (this.startTouched) {
                if (!this.forms || !this.form) {
                    setTimeout(() => this.doStartTouched(), 10);
                    return;
                }
                this.control.$setTouched();
            }
        }
        // Events
        extraOnChange($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.whenChanged)) {
                this.whenChanged({
                    $event: $event,
                    control: this.control,
                    newValue: this.control["$$rawModelValue"]
                });
            }
        }
        extraOnFocus($event: ng.IAngularEvent): void {
            if (this.forceTooltipWithFocus) {
                this.tooltipIsOpen = true;
            }
            if (angular.isFunction(this.onFocus)) {
                this.onFocus({ $event: $event, control: this.control });
            }
        }
        extraOnBlur($event: ng.IAngularEvent): void {
            if (this.forceTooltipWithFocus) {
                this.tooltipIsOpen = false;
            }
            if (angular.isFunction(this.onBlur)) {
                this.onBlur({ $event: $event, control: this.control });
            }
        }
        extraOnKeydown($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onKeydown)) {
                this.onKeydown({ $event: $event, control: this.control });
            }
        }
        extraOnKeypress($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onKeypress)) {
                this.onKeypress({ $event: $event, control: this.control });
            }
        }
        extraOnKeyup($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onKeyup)) {
                this.onKeyup({ $event: $event, control: this.control });
            }
        }
        extraOnMousedown($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onMousedown)) {
                this.onMousedown({ $event: $event, control: this.control });
            }
        }
        extraOnMouseenter($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onMouseenter)) {
                this.onMouseenter({ $event: $event, control: this.control });
            }
        }
        extraOnMouseleave($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onMouseleave)) {
                this.onMouseleave({ $event: $event, control: this.control });
            }
        }
        extraOnMousemove($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onMousemove)) {
                this.onMousemove({ $event: $event, control: this.control });
            }
        }
        extraOnMouseover($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onMouseover)) {
                this.onMouseover({ $event: $event, control: this.control });
            }
        }
        extraOnMouseup($event: ng.IAngularEvent): void {
            if (angular.isFunction(this.onMouseup)) {
                this.onMouseup({ $event: $event, control: this.control });
            }
        }
        // Constructor
        constructor(
                protected readonly $translate: ng.translate.ITranslateService,
                protected readonly cefConfig: core.CefConfig) {
            super(cefConfig);
        }
    }
}
