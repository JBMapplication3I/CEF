// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IdentityResult
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Collections.Generic;

    /// <summary>Represents the result of an identity operation.</summary>
    public class IdentityResult
    {

        /// <summary>Failure constructor that takes error messages.</summary>
        /// <param name="errors">.</param>
        public IdentityResult(params string[] errors) : this((IEnumerable<string>)errors) { }

        /// <summary>Failure constructor that takes error messages.</summary>
        /// <param name="errors">.</param>
        public IdentityResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new[] { Resources.DefaultError };
            }
            Succeeded = false;
            Errors = errors;
        }

        /// <summary>Constructor that takes whether the result is successful.</summary>
        /// <param name="success">.</param>
        protected IdentityResult(bool success)
        {
            Succeeded = success;
            Errors = Array.Empty<string>();
        }

        /// <summary>Static success result.</summary>
        /// <value>The success.</value>
        public static IdentityResult Success { get; } = new(true);

        /// <summary>List of errors.</summary>
        /// <value>The errors.</value>
        public IEnumerable<string> Errors { get; }

        /// <summary>True if the operation was successful.</summary>
        /// <value>True if succeeded, false if not.</value>
        public bool Succeeded { get; }

        /// <summary>Failed helper method.</summary>
        /// <param name="errors">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Failed(params string[] errors)
        {
            return new(errors);
        }
    }
}
