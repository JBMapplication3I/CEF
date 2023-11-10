// <copyright file="HangfireSchema.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire schema class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for hangfire schema.</summary>
    /// <seealso cref="IAmExcludedFromT4Generation"/>
    public interface IHangfireSchema : IAmExcludedFromT4Generation
    {
        int Version { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "Schema")]
    public class HangfireSchema : IHangfireSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Version { get; set; }
    }
}
