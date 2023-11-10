// <copyright file="InputFormGroupControllerBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the input form group controller base class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Components.Web;

    /// <summary>An input form group controller base.</summary>
    /// <typeparam name="TValue">Type of the value.</typeparam>
    /// <seealso cref="TemplatedControllerBase"/>
    public abstract class InputFormGroupControllerBase<TValue> : TemplatedControllerBase
    {
        /// <summary>True to form invalid.</summary>
        // ReSharper disable once InconsistentNaming
        protected bool formInvalid;

        /// <summary>Identifier for the field.</summary>
        // ReSharper disable once InconsistentNaming
        protected FieldIdentifier fieldIdentifier;

        /// <summary>Gets the field CSS classes.</summary>
        /// <value>The field CSS classes.</value>
        protected virtual string? FieldCssClasses => CascadedEditContext?.FieldCssClass(fieldIdentifier);

        #region Two-Way Bindings
#pragma warning disable 8618
        /// <summary>Gets or sets a context for the cascaded edit.</summary>
        /// <value>The cascaded edit context.</value>
        [CascadingParameter]
        public EditContext CascadedEditContext { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        [Parameter/*, EditorRequired NOTE: Causes RZ2012 when using @bind-Value */]
        public TValue Value { get; set; }

        /// <summary>Gets or sets the value changed.</summary>
        /// <value>The value changed.</value>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>Gets or sets the value expression.</summary>
        /// <value>The value expression.</value>
        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }
#pragma warning restore 8618
        #endregion

        #region One-way Bindings
        /// <summary>Gets or sets the identifier of the form.</summary>
        /// <value>The identifier of the form.</value>
        [Parameter]
        public string? FormIdentifier { get; set; }

        /// <summary>Gets or sets the autocomplete.</summary>
        /// <value>The autocomplete.</value>
        [Parameter]
        public string? Autocomplete { get; set; }

        /// <summary>Gets or sets the label text.</summary>
        /// <value>The label text.</value>
        [Parameter]
        public string? LabelText { get; set; }

#if TRANSLATIONS
        /// <summary>Gets or sets the label key.</summary>
        /// <value>The label key.</value>
        [Parameter]
        public string? LabelKey { get; set; }
#endif

        /// <summary>Gets or sets the tooltip text.</summary>
        /// <value>The tooltip text.</value>
        [Parameter]
        public string? TooltipText { get; set; }

        /// <summary>Gets or sets the left icon.</summary>
        /// <value>The left icon.</value>
        [Parameter]
        public string? LeftIcon { get; set; }

        /// <summary>Gets or sets the right icon.</summary>
        /// <value>The right icon.</value>
        [Parameter]
        public string? RightIcon { get; set; }

        /// <summary>Gets or sets the input classes.</summary>
        /// <value>The input classes.</value>
        [Parameter]
        public string? InputClasses { get; set; }

        /// <summary>Gets or sets the form classes.</summary>
        /// <value>The form classes.</value>
        [Parameter]
        public string? FormClasses { get; set; }

        /// <summary>Gets or sets a value indicating whether this InputFormGroupControllerBase{TValue} is disabled.</summary>
        /// <value>True if disabled, false if not.</value>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>Gets or sets a value indicating whether the required.</summary>
        /// <value>True if required, false if not.</value>
        [Parameter]
        public bool Required { get; set; }

        /// <summary>Gets or sets a value indicating whether the value should be read-only.</summary>
        /// <value>True if read-only, false if not.</value>
        [Parameter]
        public bool ReadOnly { get; set; }

        /// <summary>Gets or sets a value indicating whether the valid tooltip is shown.</summary>
        /// <value>True if show valid tooltip, false if not.</value>
        [Parameter]
        public bool ShowValidTooltip { get; set; }

        /// <summary>Gets or sets a value indicating whether the invalid tooltip is hidden.</summary>
        /// <value>True if hide invalid tooltip, false if not.</value>
        [Parameter]
        public bool HideInvalidTooltip { get; set; }

        /// <summary>Gets or sets a value indicating whether the autofocus.</summary>
        /// <value>True if autofocus, false if not.</value>
        [Parameter]
        public bool Autofocus { get; set; }

        /// <summary>Gets or sets a value indicating whether the tooltip with focus should be forced.</summary>
        /// <value>True if force tooltip with focus, false if not.</value>
        [Parameter]
        public bool ForceTooltipWithFocus { get; set; }

        /// <summary>Gets or sets a value indicating whether the cant be invalid.</summary>
        /// <value>True if cant be invalid, false if not.</value>
        [Parameter]
        public bool CantBeInvalid { get; set; }

        /// <summary>Gets or sets a value indicating whether the start touched.</summary>
        /// <value>True if start touched, false if not.</value>
        [Parameter]
        public bool StartTouched { get; set; }

        /// <summary>Gets or sets a value indicating whether the no label.</summary>
        /// <value>True if no label, false if not.</value>
        [Parameter]
        public bool NoLabel { get; set; }

        /// <summary>Gets a value indicating whether this InputFormGroupControllerBase{TValue} has value.</summary>
        /// <value>True if this InputFormGroupControllerBase{TValue} has value, false if not.</value>
        protected virtual bool HasValue => true;

        /// <summary>Gets or sets the on focus out.</summary>
        /// <value>The on focus out.</value>
        [Parameter]
        public EventCallback<FocusEventArgs>? OnFocusOut { get; set; }

        /// <summary>Gets or sets the on focus in.</summary>
        /// <value>The on focus in.</value>
        [Parameter]
        public EventCallback<FocusEventArgs>? OnFocusIn { get; set; }

        /// <summary>Gets or sets the on key down.</summary>
        /// <value>The on key down.</value>
        [Parameter]
        public EventCallback<KeyboardEventArgs>? OnKeyDown { get; set; }

        /// <summary>Gets or sets the on key press.</summary>
        /// <value>The on key press.</value>
        [Parameter]
        public EventCallback<KeyboardEventArgs>? OnKeyPress { get; set; }

        /// <summary>Gets or sets the on key up.</summary>
        /// <value>The on key up.</value>
        [Parameter]
        public EventCallback<KeyboardEventArgs>? OnKeyUp { get; set; }

        /// <summary>Gets or sets the on mouse down.</summary>
        /// <value>The on mouse down.</value>
        [Parameter]
        public EventCallback<MouseEventArgs>? OnMouseDown { get; set; }

        /// <summary>Gets or sets the on mouse up.</summary>
        /// <value>The on mouse up.</value>
        [Parameter]
        public EventCallback<MouseEventArgs>? OnMouseUp { get; set; }

        /// <summary>Gets or sets the on mouse in.</summary>
        /// <value>The on mouse in.</value>
        [Parameter]
        public EventCallback<MouseEventArgs>? OnMouseIn { get; set; }

        /// <summary>Gets or sets the on mouse out.</summary>
        /// <value>The on mouse out.</value>
        [Parameter]
        public EventCallback<MouseEventArgs>? OnMouseOut { get; set; }

        /// <summary>Gets or sets the on mouse over.</summary>
        /// <value>The on mouse over.</value>
        [Parameter]
        public EventCallback<MouseEventArgs>? OnMouseOver { get; set; }

        /// <summary>Gets or sets the on mouse move.</summary>
        /// <value>The on mouse move.</value>
        [Parameter]
        public EventCallback<MouseEventArgs>? OnMouseMove { get; set; }
        #endregion

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            DebugBeginMethod();
            await base.OnInitializedAsync().ConfigureAwait(false);
            if (HasValue)
            {
                fieldIdentifier = FieldIdentifier.Create(ValueExpression);
            }
            await SetRunningAsync().ConfigureAwait(false);
            await FinishRunningAsync().ConfigureAwait(false);
            ViewState.loading = false;
            DebugEndMethod();
        }

        #region Properties
        /// <summary>The tooltip is open.</summary>
        protected bool TooltipIsOpen;

        /// <summary>Gets the input prefix.</summary>
        /// <value>The input prefix.</value>
        protected abstract string InputPrefix { get; }

        /// <summary>Gets the tooltip.</summary>
        /// <value>The tooltip.</value>
        protected virtual string? Tooltip
        {
            get
            {
                return TooltipText ?? LabelText;
                // TODO: Translatable Keys
                // if (Contract.CheckAllInvalidKeys(TooltipKey, TooltipText))
                // {
                //     return null;
                // }
                // if (!Contract.CheckValidKey(TooltipKey))
                // {
                //     return TooltipText;
                // }
                // var translated = translateService.translate(TooltipKey);
                // if (translated == TooltipKey)
                // {
                //     return TooltipText;
                // }
                // return translated;
            }
        }

        /// <summary>Gets a value indicating whether the form valid.</summary>
        /// <value>True if form valid, false if not.</value>
        protected virtual bool? FormValid => CascadedEditContext?.Validate();

        /// <summary>Gets a value indicating whether the form invalid.</summary>
        /// <value>True if form invalid, false if not.</value>
        protected virtual bool? FormInvalid => CascadedEditContext?.Validate();

        /// <summary>Gets the form error.</summary>
        /// <value>The form error.</value>
        protected virtual IEnumerable<string>? FormError
            => CascadedEditContext?.GetValidationMessages();

        /// <summary>Gets a value indicating whether the form dirty.</summary>
        /// <value>True if form dirty, false if not.</value>
        protected virtual bool? FormDirty => CascadedEditContext?.IsModified();

        /// <summary>Gets the name of the input.</summary>
        /// <value>The name of the input.</value>
        protected virtual string InputName => InputPrefix + FormIdentifier;
        #endregion

        #region Events
        /// <summary>Extra on focus in.</summary>
        /// <param name="event">Focus event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnFocusIn(FocusEventArgs @event)
        {
            if (ForceTooltipWithFocus)
            {
                TooltipIsOpen = true;
            }
            return OnFocusIn?.InvokeAsync(@event) ?? Task.CompletedTask;
        }

        /// <summary>Extra on focus out.</summary>
        /// <param name="event">Focus event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnFocusOut(FocusEventArgs @event)
        {
            if (ForceTooltipWithFocus)
            {
                TooltipIsOpen = false;
            }
            return OnFocusOut?.InvokeAsync(@event) ?? Task.CompletedTask;
        }

        /// <summary>Extra on change.</summary>
        /// <param name="e">Change event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnChange(ChangeEventArgs e)
            => ValueChanged.InvokeAsync((TValue?)e.Value);

        /// <summary>Extra on key down.</summary>
        /// <param name="event">Keyboard event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnKeyDown(KeyboardEventArgs @event)
            => OnKeyDown?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on key press.</summary>
        /// <param name="event">Keyboard event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnKeyPress(KeyboardEventArgs @event)
            => OnKeyPress?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on key up.</summary>
        /// <param name="event">Keyboard event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnKeyUp(KeyboardEventArgs @event)
            => OnKeyUp?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on mouse in.</summary>
        /// <param name="event">Mouse event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnMouseIn(MouseEventArgs @event)
            => OnMouseIn?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on mouse out.</summary>
        /// <param name="event">Mouse event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnMouseOut(MouseEventArgs @event)
            => OnMouseOut?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on mouse move.</summary>
        /// <param name="event">Mouse event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnMouseMove(MouseEventArgs @event)
            => OnMouseMove?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on mouse over.</summary>
        /// <param name="event">Mouse event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnMouseOver(MouseEventArgs @event)
            => OnMouseOver?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on mouse down.</summary>
        /// <param name="event">Mouse event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnMouseDown(MouseEventArgs @event)
            => OnMouseDown?.InvokeAsync(@event) ?? Task.CompletedTask;

        /// <summary>Extra on mouse up.</summary>
        /// <param name="event">Mouse event information.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ExtraOnMouseUp(MouseEventArgs @event)
            => OnMouseUp?.InvokeAsync(@event) ?? Task.CompletedTask;
        #endregion
    }
}
