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
    /// Represents everything downloaded from resource files
    /// </summary>
    public class ResourceFileDownloadResult
    {
        /// <summary>
        /// True if this download succeeded
        /// </summary>
        public bool? success { get; set; }

        /// <summary>
        /// Bytes of the file
        /// </summary>
        public byte bytes { get; set; }

        /// <summary>
        /// Original filename
        /// </summary>
        public string? filename { get; set; }

        /// <summary>
        /// Mime content type
        /// </summary>
        public string? contentType { get; set; }

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
