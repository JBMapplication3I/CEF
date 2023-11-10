// <copyright file="Bootstrap4FieldClassProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bootstrap 4 field class provider class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    using System.Linq;
    using Microsoft.AspNetCore.Components.Forms;

    /// <summary>A bootstrap 4 field class provider.</summary>
    /// <seealso cref="FieldCssClassProvider"/>
    public class Bootstrap4FieldClassProvider : FieldCssClassProvider
    {
        /// <inheritdoc/>
        public override string GetFieldCssClass(
            EditContext editContext,
            in FieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
            var isModified = editContext.IsModified(fieldIdentifier);
            return isValid
                ? isModified ? "modified is-valid" : "is-valid"
                : isModified ? "modified is-invalid" : "is-invalid";
        }
    }
}
