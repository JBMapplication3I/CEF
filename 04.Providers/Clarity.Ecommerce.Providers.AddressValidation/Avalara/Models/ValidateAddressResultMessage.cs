// <copyright file="ValidateAddressResultMessage.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the validate address result message class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.Avalara.Models
{
    using JetBrains.Annotations;

    /// <summary>A validate address result message.</summary>
    [PublicAPI]
    public class ValidateAddressResultMessage
    {
        /// <summary>Initializes a new instance of the <see cref="ValidateAddressResultMessage"/> class.</summary>
        public ValidateAddressResultMessage()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ValidateAddressResultMessage"/> class.</summary>
        /// <param name="severity">The severity.</param>
        /// <param name="summary"> The summary.</param>
        public ValidateAddressResultMessage(string severity, string summary)
        {
            Severity = severity;
            Summary = summary;
        }

        /// <summary>Gets or sets the summary.</summary>
        /// <value>The summary.</value>
        public string? Summary { get; set; }

        /// <summary>Gets or sets the details.</summary>
        /// <value>The details.</value>
        public string? Details { get; set; }

        /// <summary>Gets or sets the refers to.</summary>
        /// <value>The refers to.</value>
        public string? RefersTo { get; set; }

        /// <summary>Gets or sets the severity.</summary>
        /// <value>The severity.</value>
        public string? Severity { get; set; }

        /// <summary>Gets or sets the source for the.</summary>
        /// <value>The source.</value>
        public string? Source { get; set; }
    }
}
