// <copyright file="CEFActionResponse{TResult}.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF action response class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    /// <summary>A CEF action response.</summary>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    /// <seealso cref="Clarity.Ecommerce.MVC.Api.Models.CEFActionResponse"/>
    /// <seealso cref="CEFActionResponse"/>
    public class CEFActionResponse<TResult> : CEFActionResponse
    {
        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse{TResult}"/> class.</summary>
        public CEFActionResponse()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse{TResult}"/> class.</summary>
        /// <param name="response">The response.</param>
        public CEFActionResponse(CEFActionResponse response)
            : this()
        {
            Messages = response.Messages;
            ActionSucceeded = response.ActionSucceeded;
        }

        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse{TResult}"/> class.</summary>
        /// <param name="response">The response.</param>
        /// <param name="result">  The result.</param>
        public CEFActionResponse(CEFActionResponse response, TResult result)
            : this(response)
        {
            Result = result;
        }

        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse{TResult}"/> class.</summary>
        /// <param name="actionSucceeded">True if the action operation was a success, false if it failed.</param>
        public CEFActionResponse(bool actionSucceeded)
            : base(actionSucceeded)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse{TResult}"/> class.</summary>
        /// <param name="actionSucceeded">True if the action operation was a success, false if it failed.</param>
        /// <param name="messages">       A variable-length parameters list containing messages.</param>
        public CEFActionResponse(bool actionSucceeded, params string[] messages)
            : base(actionSucceeded, messages)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse{TResult}"/> class.</summary>
        /// <param name="result">         The result.</param>
        /// <param name="actionSucceeded">True if the action operation was a success, false if it failed.</param>
        public CEFActionResponse(TResult? result, bool actionSucceeded)
            : base(actionSucceeded)
        {
            Result = result;
        }

        /// <summary>Initializes a new instance of the <see cref="CEFActionResponse{TResult}"/> class.</summary>
        /// <param name="result">         The result.</param>
        /// <param name="actionSucceeded">True if the action operation was a success, false if it failed.</param>
        /// <param name="messages">       A variable-length parameters list containing messages.</param>
        public CEFActionResponse(TResult? result, bool actionSucceeded, params string[] messages)
            : base(actionSucceeded, messages)
        {
            Result = result;
        }

        /// <summary>Gets or sets the result.</summary>
        /// <value>The result.</value>
        public TResult? Result { get; set; }
    }
}
