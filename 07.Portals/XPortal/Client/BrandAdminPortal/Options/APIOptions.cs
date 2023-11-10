// <copyright file="APIOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the API Options class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    /// <summary>An API options.</summary>
    public class APIOptions
    {
        /// <summary>Gets or sets the kind.</summary>
        /// <value>The kind.</value>
        public string? Kind { get; set; }

        /// <summary>Gets or sets the base address.</summary>
        /// <value>The base address.</value>
        public string? BaseAddress { get; set; }

        /// <summary>Base address for previewing images in the product editor.</summary>
        /// <value>The base image address.</value>
        public string? BaseImageAddress { get; set; }
    }
}
