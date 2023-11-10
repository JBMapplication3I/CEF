// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.Resources
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Identity
{
	/// <summary>
	///   A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		internal Resources()
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
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.AspNet.Identity.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		/// <summary>
		///   Overrides the current thread's CurrentUICulture property for all
		///   resource lookups using this strongly typed resource class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
            get => Resources.resourceCulture;
            set => Resources.resourceCulture = value;
		}

		/// <summary>
		///   Looks up a localized string similar to An unknown failure has occured..
		/// </summary>
    internal static string DefaultError => Resources.ResourceManager.GetString(nameof (DefaultError), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Email '{0}' is already taken..
		/// </summary>
    internal static string DuplicateEmail => Resources.ResourceManager.GetString(nameof (DuplicateEmail), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Name {0} is already taken..
		/// </summary>
    internal static string DuplicateName => Resources.ResourceManager.GetString(nameof (DuplicateName), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to A user with that external login already exists..
		/// </summary>
    internal static string ExternalLoginExists => Resources.ResourceManager.GetString(nameof (ExternalLoginExists), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Email '{0}' is invalid..
		/// </summary>
    internal static string InvalidEmail => Resources.ResourceManager.GetString(nameof (InvalidEmail), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Invalid token..
		/// </summary>
    internal static string InvalidToken => Resources.ResourceManager.GetString(nameof (InvalidToken), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to User name {0} is invalid, can only contain letters or digits..
		/// </summary>
    internal static string InvalidUserName => Resources.ResourceManager.GetString(nameof (InvalidUserName), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Lockout is not enabled for this user..
		/// </summary>
    internal static string LockoutNotEnabled => Resources.ResourceManager.GetString(nameof (LockoutNotEnabled), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to No IUserTokenProvider is registered..
		/// </summary>
    internal static string NoTokenProvider => Resources.ResourceManager.GetString(nameof (NoTokenProvider), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to No IUserTwoFactorProvider for '{0}' is registered..
		/// </summary>
    internal static string NoTwoFactorProvider => Resources.ResourceManager.GetString(nameof (NoTwoFactorProvider), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Incorrect password..
		/// </summary>
    internal static string PasswordMismatch => Resources.ResourceManager.GetString(nameof (PasswordMismatch), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Passwords must have at least one digit ('0'-'9')..
		/// </summary>
    internal static string PasswordRequireDigit => Resources.ResourceManager.GetString(nameof (PasswordRequireDigit), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Passwords must have at least one lowercase ('a'-'z')..
		/// </summary>
    internal static string PasswordRequireLower => Resources.ResourceManager.GetString(nameof (PasswordRequireLower), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Passwords must have at least one non letter or digit character..
		/// </summary>
    internal static string PasswordRequireNonLetterOrDigit => Resources.ResourceManager.GetString(nameof (PasswordRequireNonLetterOrDigit), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Passwords must have at least one uppercase ('A'-'Z')..
		/// </summary>
    internal static string PasswordRequireUpper => Resources.ResourceManager.GetString(nameof (PasswordRequireUpper), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Passwords must be at least {0} characters..
		/// </summary>
    internal static string PasswordTooShort => Resources.ResourceManager.GetString(nameof (PasswordTooShort), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to {0} cannot be null or empty..
		/// </summary>
    internal static string PropertyTooShort => Resources.ResourceManager.GetString(nameof (PropertyTooShort), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Role {0} does not exist..
		/// </summary>
    internal static string RoleNotFound => Resources.ResourceManager.GetString(nameof (RoleNotFound), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IQueryableRoleStore&lt;TRole&gt;..
		/// </summary>
    internal static string StoreNotIQueryableRoleStore => Resources.ResourceManager.GetString(nameof (StoreNotIQueryableRoleStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IQueryableUserStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIQueryableUserStore => Resources.ResourceManager.GetString(nameof (StoreNotIQueryableUserStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserClaimStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserClaimStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserClaimStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserConfirmationStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserConfirmationStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserConfirmationStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserEmailStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserEmailStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserEmailStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserLockoutStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserLockoutStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserLockoutStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserLoginStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserLoginStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserLoginStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserPasswordStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserPasswordStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserPasswordStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserPhoneNumberStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserPhoneNumberStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserPhoneNumberStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserRoleStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserRoleStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserRoleStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserSecurityStampStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserSecurityStampStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserSecurityStampStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to Store does not implement IUserTwoFactorStore&lt;TUser&gt;..
		/// </summary>
    internal static string StoreNotIUserTwoFactorStore => Resources.ResourceManager.GetString(nameof (StoreNotIUserTwoFactorStore), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to User already has a password set..
		/// </summary>
    internal static string UserAlreadyHasPassword => Resources.ResourceManager.GetString(nameof (UserAlreadyHasPassword), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to User already in role..
		/// </summary>
    internal static string UserAlreadyInRole => Resources.ResourceManager.GetString(nameof (UserAlreadyInRole), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to UserId not found..
		/// </summary>
    internal static string UserIdNotFound => Resources.ResourceManager.GetString(nameof (UserIdNotFound), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to User {0} does not exist..
		/// </summary>
    internal static string UserNameNotFound => Resources.ResourceManager.GetString(nameof (UserNameNotFound), Resources.resourceCulture);

		/// <summary>
		///   Looks up a localized string similar to User is not in role..
		/// </summary>
    internal static string UserNotInRole => Resources.ResourceManager.GetString(nameof (UserNotInRole), Resources.resourceCulture);
	}
}
