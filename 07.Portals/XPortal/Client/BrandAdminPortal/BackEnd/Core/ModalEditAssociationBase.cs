// <copyright file="ModalEditAssociationBase.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the modal edit association base class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    using System;
    using System.Threading.Tasks;
    using Blazorise;
    using Microsoft.AspNetCore.Components;
    using Newtonsoft.Json;

    /// <summary>A modal edit association base.</summary>
    /// <seealso cref="ComponentBase"/>
    public abstract class ModalEditAssociationBase<TAssocModel> : ComponentBase
        where TAssocModel : Api.Models.AmARelationshipTableBaseModel
    {
        /// <summary>Gets a value indicating whether the result was true or false.</summary>
        /// <value>True if result, false if not.</value>
        public bool Result { get; protected set; }

        /// <summary>Gets or sets the association.</summary>
        /// <value>The association.</value>
        protected TAssocModel Association { get; set; } = null!;

        /// <summary>Gets or sets the callback.</summary>
        /// <value>The callback.</value>
        protected Action<bool, TAssocModel>? Callback { get; set; }

        /// <summary>Gets or sets the callback asynchronous.</summary>
        /// <value>The callback asynchronous.</value>
        protected Func<bool, TAssocModel, Task>? CallbackAsync { get; set; }

        /// <summary>Gets or sets the modal reference.</summary>
        /// <value>The modal reference.</value>
        protected Modal? ModalRef { get; set; }

        /// <summary>Shows.</summary>
        /// <param name="association">The association.</param>
        /// <param name="callback">   The callback.</param>
        /// <returns>A Task.</returns>
        public virtual Task ShowAsync(
            TAssocModel association,
            Action<bool, TAssocModel>? callback = null)
        {
            Association = JsonConvert.DeserializeObject<TAssocModel>(JsonConvert.SerializeObject(association))!;
            Callback = callback;
            Result = false;
            return ModalRef!.Show();
        }

        /// <summary>Shows the modal.</summary>
        /// <param name="association">The association.</param>
        /// <param name="callback">   The callback.</param>
        /// <returns>A Task.</returns>
        public virtual Task ShowAsync(
            TAssocModel association,
            Func<bool, TAssocModel, Task>? callback = null)
        {
            Association = JsonConvert.DeserializeObject<TAssocModel>(JsonConvert.SerializeObject(association))!;
            CallbackAsync = callback;
            Result = false;
            return ModalRef!.Show();
        }

        /// <summary>Hides the modal.</summary>
        /// <param name="accept">True to accept.</param>
        /// <returns>A Task.</returns>
        protected async Task HideAsync(bool accept)
        {
            Result = accept;
            await ModalRef!.Hide();
            if (Callback is not null)
            {
                Callback(accept, Association);
            }
            else if (CallbackAsync is not null)
            {
                await CallbackAsync(accept, Association).ConfigureAwait(false);
            }
        }
    }
}
