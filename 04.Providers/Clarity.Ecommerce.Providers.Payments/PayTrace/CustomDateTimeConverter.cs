// <copyright file="CustomDateTimeConverter.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements JSON Converter class for converting DateTime</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using Newtonsoft.Json.Converters;

    /// <summary>A JSON datetime converter.</summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDateTimeConverter"/> class.
        /// </summary>
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "MM/dd/yyyy";
        }
    }
}
