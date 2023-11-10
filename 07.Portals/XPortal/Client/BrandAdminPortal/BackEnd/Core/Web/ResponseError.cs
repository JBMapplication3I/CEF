// <copyright file="ResponseError.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    /// <summary>A response error.</summary>
    public class ResponseError
    {
        /// <summary>Gets or sets the response status.</summary>
        /// <value>The response status.</value>
        public ResponseStatus? ResponseStatus { get; set; }
    }
}
