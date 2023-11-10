// <copyright file="ValidateAddressResult.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the validate address result class</summary>
namespace Clarity.Ecommerce.Providers.AddressValidation.MelissaData.Models
{
    using System.Collections.Generic;

    /// <summary>Result Model from the validate WebRequest.</summary>
    [JetBrains.Annotations.PublicAPI]
    public class ValidateAddressResult
    {
        /// <summary>Gets or sets the Version of the Melissa Data Software.</summary>
        /// <value>The version.</value>
        public string? Version { get; set; }

        /// <summary>Gets or sets the Internal Unique Reference number for the request.</summary>
        /// <value>The transmission reference.</value>
        public string? TransmissionReference { get; set; }

        /// <summary>Gets or sets the Errors from the Request as a whole.</summary>
        /// <value>The transmission results.</value>
        public string? TransmissionResults { get; set; }

        /// <summary>Gets or sets the Total Records in the request.</summary>
        /// <value>The total number of records.</value>
        public string? TotalRecords { get; set; }

        /// <summary>Gets or sets the list of the records coming back from the request.</summary>
        /// <value>The records.</value>
        public List<Record>? Records { get; set; }
    }
}
