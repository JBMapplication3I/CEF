// <copyright file="BaseValidatingContext`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base validating context` 1 class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using Owin;
    using Provider;

    /// <summary>Base class used for certain event contexts.</summary>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseContext{TOptions}"/>
    public abstract class BaseValidatingContext<TOptions> : BaseContext<TOptions>
    {
        /// <summary>The error argument provided when SetError was called on this context. This is eventually returned to
        /// the client app as the OAuth "error" parameter.</summary>
        /// <value>The error.</value>
        public string Error
        {
            get;
            private set;
        }

        /// <summary>The optional errorDescription argument provided when SetError was called on this context. This is
        /// eventually returned to the client app as the OAuth "error_description" parameter.</summary>
        /// <value>Information describing the error.</value>
        public string ErrorDescription
        {
            get;
            private set;
        }

        /// <summary>The optional errorUri argument provided when SetError was called on this context. This is eventually
        /// returned to the client app as the OAuth "error_uri" parameter.</summary>
        /// <value>The error URI.</value>
        public string ErrorUri
        {
            get;
            private set;
        }

        /// <summary>True if application code has called any of the SetError methods on this context.</summary>
        /// <value>True if this BaseValidatingContext{TOptions} has error, false if not.</value>
        public bool HasError
        {
            get;
            private set;
        }

        /// <summary>True if application code has called any of the Validate methods on this context.</summary>
        /// <value>True if this BaseValidatingContext{TOptions} is validated, false if not.</value>
        public bool IsValidated
        {
            get;
            private set;
        }

        /// <summary>Initializes base class used for certain event contexts.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        protected BaseValidatingContext(IOwinContext context, TOptions options) : base(context, options)
        {
        }

        /// <summary>Marks this context as not validated by the application. IsValidated and HasError become false as a
        /// result of calling.</summary>
        public virtual void Rejected()
        {
            IsValidated = false;
            HasError = false;
        }

        /// <summary>Marks this context as not validated by the application and assigns various error information
        /// properties. HasError becomes true and IsValidated becomes false as a result of calling.</summary>
        /// <param name="error">Assigned to the Error property.</param>
        public void SetError(string error)
        {
            SetError(error, null);
        }

        /// <summary>Marks this context as not validated by the application and assigns various error information
        /// properties. HasError becomes true and IsValidated becomes false as a result of calling.</summary>
        /// <param name="error">           Assigned to the Error property.</param>
        /// <param name="errorDescription">Assigned to the ErrorDescription property.</param>
        public void SetError(string error, string errorDescription)
        {
            SetError(error, errorDescription, null);
        }

        /// <summary>Marks this context as not validated by the application and assigns various error information
        /// properties. HasError becomes true and IsValidated becomes false as a result of calling.</summary>
        /// <param name="error">           Assigned to the Error property.</param>
        /// <param name="errorDescription">Assigned to the ErrorDescription property.</param>
        /// <param name="errorUri">        Assigned to the ErrorUri property.</param>
        public void SetError(string error, string errorDescription, string errorUri)
        {
            Error = error;
            ErrorDescription = errorDescription;
            ErrorUri = errorUri;
            Rejected();
            HasError = true;
        }

        /// <summary>Marks this context as validated by the application. IsValidated becomes true and HasError becomes
        /// false as a result of calling.</summary>
        /// <returns>True if the validation has taken effect.</returns>
        public virtual bool Validated()
        {
            IsValidated = true;
            HasError = false;
            return true;
        }
    }
}