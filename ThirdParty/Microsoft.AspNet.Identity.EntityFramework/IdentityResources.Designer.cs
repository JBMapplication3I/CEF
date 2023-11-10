// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityResources
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class IdentityResources
    {
        private static ResourceManager resourceMan;

        private static CultureInfo resourceCulture;

        internal IdentityResources()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (IdentityResources.resourceMan == null)
                {
                    IdentityResources.resourceMan = new ResourceManager("Microsoft.AspNet.Identity.EntityFramework.IdentityResources", typeof(IdentityResources).Assembly);
                }
                return IdentityResources.resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => IdentityResources.resourceCulture;
            set => IdentityResources.resourceCulture = value;
        }

        /// <summary>
        ///   Looks up a localized string similar to Database Validation failed..
        /// </summary>
        internal static string DbValidationFailed => IdentityResources.ResourceManager.GetString(nameof(DbValidationFailed), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to Email {0} is already taken..
        /// </summary>
        internal static string DuplicateEmail => IdentityResources.ResourceManager.GetString(nameof(DuplicateEmail), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to User name {0} is already taken..
        /// </summary>
        internal static string DuplicateUserName => IdentityResources.ResourceManager.GetString(nameof(DuplicateUserName), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to Entity Type {0} failed validation..
        /// </summary>
        internal static string EntityFailedValidation => IdentityResources.ResourceManager.GetString(nameof(EntityFailedValidation), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to A user with that external login already exists..
        /// </summary>
        internal static string ExternalLoginExists => IdentityResources.ResourceManager.GetString(nameof(ExternalLoginExists), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to The model backing the 'ApplicationDbContext' context has changed since the database was created. This could have happened because the model used by ASP.NET Identity Framework has changed or the model being used in your application has changed. To resolve this issue, you need to update your database. Consider using Code First Migrations to update the database (http://go.microsoft.com/fwlink/?LinkId=301867).  Before you update your database using Code First Migrations, please disable the schema consistency ch [rest of string was truncated]";.
        /// </summary>
        internal static string IdentityV1SchemaError => IdentityResources.ResourceManager.GetString(nameof(IdentityV1SchemaError), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to Incorrect type, expected type of {0}..
        /// </summary>
        internal static string IncorrectType => IdentityResources.ResourceManager.GetString(nameof(IncorrectType), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to {0} cannot be null or empty..
        /// </summary>
        internal static string PropertyCannotBeEmpty => IdentityResources.ResourceManager.GetString(nameof(PropertyCannotBeEmpty), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to Role {0} already exists..
        /// </summary>
        internal static string RoleAlreadyExists => IdentityResources.ResourceManager.GetString(nameof(RoleAlreadyExists), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to Role is not empty..
        /// </summary>
        internal static string RoleIsNotEmpty => IdentityResources.ResourceManager.GetString(nameof(RoleIsNotEmpty), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to Role {0} does not exist..
        /// </summary>
        internal static string RoleNotFound => IdentityResources.ResourceManager.GetString(nameof(RoleNotFound), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to User already in role..
        /// </summary>
        internal static string UserAlreadyInRole => IdentityResources.ResourceManager.GetString(nameof(UserAlreadyInRole), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to The UserId cannot be found..
        /// </summary>
        internal static string UserIdNotFound => IdentityResources.ResourceManager.GetString(nameof(UserIdNotFound), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to UserLogin already exists for loginProvider: {0} with providerKey: {1}.
        /// </summary>
        internal static string UserLoginAlreadyExists => IdentityResources.ResourceManager.GetString(nameof(UserLoginAlreadyExists), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to User {0} does not exist..
        /// </summary>
        internal static string UserNameNotFound => IdentityResources.ResourceManager.GetString(nameof(UserNameNotFound), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to User is not in role..
        /// </summary>
        internal static string UserNotInRole => IdentityResources.ResourceManager.GetString(nameof(UserNotInRole), IdentityResources.resourceCulture);

        /// <summary>
        ///   Looks up a localized string similar to Value cannot be null or empty..
        /// </summary>
        internal static string ValueCannotBeNullOrEmpty => IdentityResources.ResourceManager.GetString(nameof(ValueCannotBeNullOrEmpty), IdentityResources.resourceCulture);
    }
}
