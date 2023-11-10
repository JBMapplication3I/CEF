// <copyright file="IHttpPromiseCallbackArg.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.MVC.Core
{
    /// <summary>Interface for HTTP promise callback argument.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public interface IHttpPromiseCallbackArg<T> : IHttpPromiseCallback
    {
        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        public T? data { get; set; }
    }

    /// <summary>Interface for HTTP promise callback.</summary>
    public interface IHttpPromiseCallback
    {
        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public int? status { get; set; }

        /// <summary>Gets or sets the status text.</summary>
        /// <value>The status text.</value>
        public string? statusText { get; set; }
    }
}
