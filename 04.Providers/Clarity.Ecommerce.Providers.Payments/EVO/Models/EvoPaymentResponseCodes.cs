// <copyright file="EvoPaymentResponseCodes.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Evo payment response codes class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System.ComponentModel;

    /// <summary>Values that represent Evo payment response codes.</summary>
    public enum EvoPaymentResponseCodes
    {
        /// <summary>
        /// Success = 200
        /// </summary>
        [Description("Request Successful")]
        Success = 200,

        /// <summary>
        /// BadRequest = 400
        /// </summary>
        [Description("The request is missing required information")]
        BadRequest = 400,

        /// <summary>
        /// Unauthorized = 401
        /// </summary>
        [Description("The authorization header provided by the client (either Device ID / Device Password, or Security Token) is invalid or revoked")]
        Unauthorized = 401,

        /// <summary>
        /// NotFound = 404
        /// </summary>
        [Description("The requested resource could not be located. Check for typos in your URL")]
        NotFound = 404,

        /// <summary>
        /// PreconditionFailed = 412
        /// </summary>
        [Description("Missing fields or mandatory parameters")]
        PreconditionFailed = 412,

        /// <summary>
        /// InternalServerError = 500
        /// </summary>
        [Description("PayFabric server has encountered an error")]
        InternalServerError = 500,
    }
}
