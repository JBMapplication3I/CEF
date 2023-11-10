// <copyright file="ImplementsDisplayNameBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the implements display name base class</summary>
namespace Clarity.Ecommerce.Service
{
    using ServiceStack;

    /// <summary>The implements display name.</summary>
    public abstract class ImplementsDisplayNameBase
    {
        /// <summary>Gets or sets the display name.</summary>
        /// <value>The display name.</value>
        [ApiMember(Name = nameof(DisplayName), DataType = "string", ParameterType = "path", IsRequired = true,
            Description = "The Display Name of the record to call.")]
        public string DisplayName { get; set; } = null!;

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
