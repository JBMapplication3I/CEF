// <copyright file="ApiResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the API response class</summary>
namespace Clarity.Ecommerce.DNN.Extensions.Services
{
    public class ApiResponse
    {
        /// <summary>Initializes a new instance of the <see cref="ApiResponse"/> class.</summary>
        /// <param name="success">True if the operation was a success, false if it failed.</param>
        /// <param name="message">The message.</param>
        /// <param name="data">   The data.</param>
        public ApiResponse(bool success = true, string message = "", object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        /// <summary>Gets or sets a value indicating whether the success.</summary>
        /// <value>True if success, false if not.</value>
        public bool Success { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>Succeeded Response.</summary>
        /// <param name="message">The message.</param>
        /// <param name="data">   The data.</param>
        /// <returns>An ApiResponse.</returns>
        public static ApiResponse Succeeded(string message, object data = null)
        {
            return new ApiResponse(true, message, data);
        }

        /// <summary>Succeeded the given data Response.</summary>
        /// <param name="data">The data.</param>
        /// <returns>An ApiResponse.</returns>
        public static ApiResponse Succeeded(object data = null)
        {
            return new ApiResponse(true, string.Empty, data);
        }

        /// <summary>Failed Response.</summary>
        /// <param name="message">The message.</param>
        /// <param name="data">   The data.</param>
        /// <returns>An ApiResponse.</returns>
        public static ApiResponse Failed(string message, object data = null)
        {
            return new ApiResponse(false, message, data);
        }

        /// <summary>Failed the given data Response.</summary>
        /// <param name="data">The data.</param>
        /// <returns>An ApiResponse.</returns>
        public static ApiResponse Failed(object data = null)
        {
            return new ApiResponse(false, string.Empty, data);
        }
    }
}
