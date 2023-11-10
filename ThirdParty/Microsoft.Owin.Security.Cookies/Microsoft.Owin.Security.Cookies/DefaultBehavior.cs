// <copyright file="DefaultBehavior.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the default behavior class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>A default behavior.</summary>
    internal static class DefaultBehavior
    {
        /// <summary>The apply redirect.</summary>
        internal static readonly Action<CookieApplyRedirectContext> ApplyRedirect;

        /// <summary>Initializes static members of the Microsoft.Owin.Security.Cookies.DefaultBehavior class.</summary>
        static DefaultBehavior()
        {
            ApplyRedirect = context =>
            {
                if (!IsAjaxRequest(context.Request))
                {
                    context.Response.Redirect(context.RedirectUri);
                    return;
                }
                var respondedJson = new RespondedJson
                {
                    Status = context.Response.StatusCode,
                    Headers = new RespondedJson.RespondedJsonHeaders { Location = context.RedirectUri },
                };
                context.Response.StatusCode = 200;
                context.Response.Headers.Append("X-Responded-JSON", respondedJson.ToString());
            };
        }

        /// <summary>Query if 'request' is ajax request.</summary>
        /// <param name="request">The request.</param>
        /// <returns>True if ajax request, false if not.</returns>
        private static bool IsAjaxRequest(IOwinRequest request)
        {
            var query = request.Query;
            if (query != null && query["X-Requested-With"] == "XMLHttpRequest")
            {
                return true;
            }
            var headers = request.Headers;
            return headers != null && headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>A responded json.</summary>
        [DataContract]
        private class RespondedJson
        {
            /// <summary>The serializer.</summary>
            public static readonly DataContractJsonSerializer Serializer;

            /// <summary>Initializes static members of the Microsoft.Owin.Security.Cookies.DefaultBehavior.RespondedJson
            /// class.</summary>
            static RespondedJson()
            {
                Serializer = new DataContractJsonSerializer(typeof(RespondedJson));
            }

            /// <summary>Gets or sets the headers.</summary>
            /// <value>The headers.</value>
            [DataMember(Name = "headers", Order = 2)]
            public RespondedJsonHeaders Headers
            {
                get;
                set;
            }

            /// <summary>Gets or sets the status.</summary>
            /// <value>The status.</value>
            [DataMember(Name = "status", Order = 1)]
            public int Status
            {
                get;
                set;
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                string str;
                using (var memoryStream = new MemoryStream())
                {
                    Serializer.WriteObject(memoryStream, this);
                    str = Encoding.ASCII.GetString(memoryStream.ToArray());
                }
                return str;
            }

            /// <summary>A responded JSON headers.</summary>
            [DataContract]
            public class RespondedJsonHeaders
            {
                /// <summary>Gets or sets the location.</summary>
                /// <value>The location.</value>
                [DataMember(Name = "location", Order = 1)]
                public string Location
                {
                    get;
                    set;
                }
            }
        }
    }
}
