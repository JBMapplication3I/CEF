// <copyright file="ValidateAddressResult.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the validate address result class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;
    using Utilities;

    /// <summary>Encapsulates the result of a validate address.</summary>
    [PublicAPI]
    public class ValidateAddressResult
    {
        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        public ValidateAddressResultAddress? Address { get; set; }

        /// <summary>Gets or sets the result code.</summary>
        /// <value>The result code.</value>
        public string? ResultCode { get; set; }

        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        public List<ValidateAddressResultMessage> Messages { get; set; } = new();

        /// <summary>Gets a value indicating whether the success.</summary>
        /// <value>True if success, false if not.</value>
        public bool Success => ResultCode == "Success";

        /// <summary>Gets the errors.</summary>
        /// <value>The errors.</value>
        public string Errors
        {
            get
            {
                var sb = new StringBuilder();
                if (Contract.CheckValidKey(ResultCode))
                {
                    sb.AppendLine($"ResultCode: {ResultCode}");
                }
                if (Messages?.Any() != true)
                {
                    return sb.ToString();
                }
                foreach (var message in Messages)
                {
                    sb.AppendLine($"{message.Severity}: {message.Summary}");
                }
                return sb.ToString();
            }
        }
    }
}
