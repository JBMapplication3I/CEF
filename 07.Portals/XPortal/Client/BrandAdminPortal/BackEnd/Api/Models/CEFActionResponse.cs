// <copyright file="CEFActionResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef action response class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>A CEF action response.</summary>
    public class CEFActionResponse
    {
        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse"/> class.</summary>
        public CEFActionResponse()
        {
            Messages = new();
        }

        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse"/> class.</summary>
        /// <param name="actionSucceeded">True if the action operation was a success, false if it failed.</param>
        /// <param name="messages">       A variable-length parameters list containing messages.</param>
        public CEFActionResponse(bool actionSucceeded, params string[]? messages)
        {
            ActionSucceeded = actionSucceeded;
            Messages = messages?.ToList() ?? new List<string>();
        }

        /// <summary>Gets or sets a value indicating whether the action succeeded.</summary>
        /// <value>True if action succeeded, false if not.</value>
        public bool ActionSucceeded { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        public List<string> Messages { get; set; }
    }
}
