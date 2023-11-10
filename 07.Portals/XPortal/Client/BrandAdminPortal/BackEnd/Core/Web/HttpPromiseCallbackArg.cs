// <copyright file="HttpPromiseCallbackArg.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    /// <summary>A HTTP promise callback argument.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <seealso cref="HttpPromiseCallback"/>
    /// <seealso cref="IHttpPromiseCallbackArg{T}"/>
    public class HttpPromiseCallbackArg<T> : HttpPromiseCallback, IHttpPromiseCallbackArg<T>
    {
        /// <summary>Initializes a new instance of the <see cref="HttpPromiseCallbackArg{T}"/> class.</summary>
        public HttpPromiseCallbackArg()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HttpPromiseCallbackArg{T}"/> class.</summary>
        /// <param name="data">      The data.</param>
        /// <param name="status">    The status.</param>
        /// <param name="statusText">The status text.</param>
        public HttpPromiseCallbackArg(T? data, int status = 200, string? statusText = "OK")
            : base(status, statusText)
        {
            this.data = data;
            this.status = status;
            this.statusText = statusText;
        }

        /// <inheritdoc/>
        public T? data { get; set; }
    }

    /// <summary>A HTTP promise callback.</summary>
    /// <seealso cref="IHttpPromiseCallback"/>
    public class HttpPromiseCallback : IHttpPromiseCallback
    {
        /// <summary>Initializes a new instance of the <see cref="HttpPromiseCallback"/> class.</summary>
        /// <param name="status">    The status.</param>
        /// <param name="statusText">The status text.</param>
        public HttpPromiseCallback(int status = 200, string? statusText = "OK")
        {
            this.status = status;
            this.statusText = statusText;
        }

        /// <inheritdoc/>
        public int? status { get; set; }

        /// <inheritdoc/>
        public string? statusText { get; set; }
    }
}
