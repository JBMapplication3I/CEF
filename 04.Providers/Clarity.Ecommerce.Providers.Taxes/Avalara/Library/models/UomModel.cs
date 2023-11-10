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
    /// The "Unit of Measurement" model captures information about a type of measurement. Types of measurement refer to
    /// different scales for the same dimension. For example, measurements of type "Distance" may include units of measurement
    /// such as meters, feet, inches, and miles.
    /// </summary>
    public class UomModel
    {
        /// <summary>
        /// The unique ID number of this unit of measurement.
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The code that refers to this unit of measurement.
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// A short description of this unit of measurement.
        /// </summary>
        public string? shortDesc { get; set; }

        /// <summary>
        /// A longer description of this unit of measurement.
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// The ID number of the measurement type, such as "Distance" or "Mass".
        /// </summary>
        public int? measurementTypeId { get; set; }

        /// <summary>
        /// The code describing the measurement type.
        /// </summary>
        public string? measurementTypeCode { get; set; }

        /// <summary>
        /// For a particular measurement type, this is the ID number of the unit of measurement object corresponding to the
        /// International System of Units (abbreviated SI) unit of measurement standard. This pointer allows you to select
        /// the SI unit of measurement for a particular measurement type.
        /// </summary>
        public string? siUOM { get; set; }

        /// <summary>
        /// A description of the measurement type system.
        /// </summary>
        public string? measurementTypeDescription { get; set; }

        /// <summary>
        /// True if this measurement is an International System of Units (abbreviated SI) defined standard.
        /// </summary>
        public bool? isSiUom { get; set; }

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
