// <copyright file="ImplementsEmailBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements email base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements email.</summary>
    public abstract class ImplementsEmailBase
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "path", IsRequired = true,
            Description = "The Email of the record to call")]
        public string? Email { get; set; }

        /// <summary>Gets or sets the no cache.</summary>
        /// <value>The no cache.</value>
        [ApiMember(Name = nameof(noCache), DataType = "bool", ParameterType = "long", IsRequired = false,
            Description = "Specifying a value will reduce or prevent chance of getting cached data.")]
        // ReSharper disable once InconsistentNaming, StyleCop.SA1300
#pragma warning disable SA1300, IDE1006
        public long? noCache { get; set; }
#pragma warning restore SA1300, IDE1006
    }
}
