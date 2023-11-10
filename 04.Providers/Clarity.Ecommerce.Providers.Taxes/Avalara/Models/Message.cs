// <copyright file="Message.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    using System;

    /// <summary>(Serializable)a message.</summary>
    [Serializable]
    public class Message // Result object for Common Response Format
    {
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
        public SeverityLevel Severity { get; set; }

        /// <summary>Gets or sets the source for the.</summary>
        /// <value>The source.</value>
        public string? Source { get; set; }
    }
}
