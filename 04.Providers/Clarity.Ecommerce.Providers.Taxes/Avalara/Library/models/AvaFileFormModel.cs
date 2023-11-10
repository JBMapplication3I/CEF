﻿/*
 * AvaTax API Client Library
 *
 * (c) 2004-2019 Avalara, Inc.
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * @author Genevieve Conty
 * @author Greg Hester
 */

namespace Avalara.AvaTax.RestClient
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents information about a tax form known to Avalara
    /// </summary>
    public class AvaFileFormModel
    {
        /// <summary>
        /// Unique Id of the form
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// Name of the file being returned
        /// </summary>
        public string? returnName { get; set; }

        /// <summary>
        /// Name of the submitted form
        /// </summary>
        public string? formName { get; set; }

        /// <summary>
        /// A description of the submitted form
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// The date this form starts to take effect
        /// </summary>
        public DateTime? effDate { get; set; }

        /// <summary>
        /// The date the form finishes to take effect
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// State/Province/Region where the form is submitted for
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// The country this form is submitted for
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// The type of the form being submitted
        /// </summary>
        public FormTypeId? formTypeId { get; set; }

        /// <summary>
        /// The type of Filing option
        /// </summary>
        public FilingOptionTypeId? filingOptionTypeId { get; set; }

        /// <summary>
        /// The type of the due date
        /// </summary>
        public DueDateTypeId? dueDateTypeId { get; set; }

        /// <summary>
        /// Due date
        /// </summary>
        public int? dueDay { get; set; }

        /// <summary>
        /// The type of E-file due date.
        /// </summary>
        public DueDateTypeId? efileDueDateTypeId { get; set; }

        /// <summary>
        /// The date by when the E-filing should be submitted
        /// </summary>
        public int? efileDueDay { get; set; }

        /// <summary>
        /// The time of day by when the E-filing should be submitted
        /// </summary>
        public DateTime? efileDueTime { get; set; }

        /// <summary>
        /// Whether the customer has discount
        /// </summary>
        public bool? hasVendorDiscount { get; set; }

        /// <summary>
        /// The way system does the rounding
        /// </summary>
        public RoundingTypeId? roundingTypeId { get; set; }

        /// <summary>
        /// The outlet type of the form
        /// </summary>
        public OutletTypeId? outletTypeId { get; set; }

        /// <summary>
        /// Convert this object to a JSON string of itself
        /// </summary>
        /// <returns>A JSON string of this object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
