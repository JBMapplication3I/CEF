// <copyright file="AuthProviderLogin.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication provider login class</summary>
// ReSharper disable StyleCop.SA1027
namespace Clarity.Ecommerce.MVC.Api.Endpoints
{
	using JetBrains.Annotations;
	using ServiceStack;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	/// <summary>An authentication provider login.</summary>
	[PublicAPI, Route("/auth/identity", Verbs = "POST")]
	public class AuthProviderLogin
	{
		/// <summary>Gets or sets the username.</summary>
		/// <value>The username.</value>
		[Required,
			DefaultValue(null),
			ApiMember(Name = nameof(Username), DataType = "string", ParameterType = "body", IsRequired = true)]
		public string? Username { get; set; }

		/// <summary>Gets or sets the password.</summary>
		/// <value>The password.</value>
		[Required,
			DefaultValue(null),
			ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true)]
		public string? Password { get; set; }

		/// <summary>Gets or sets a value indicating whether the remember me.</summary>
		/// <value>True if remember me, false if not.</value>
		[DefaultValue(true),
			ApiMember(Name = nameof(RememberMe), DataType = "bool", ParameterType = "body", IsRequired = false)]
		public bool RememberMe { get; set; }
	}
}
