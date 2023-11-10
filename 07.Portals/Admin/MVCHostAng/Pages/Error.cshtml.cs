// <copyright file="Error.cshtml.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the error.cshtml class</summary>
#pragma warning disable SA1649 // File name should match first type name
namespace Clarity.Ecommerce.UI.MVCHost.Pages
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>A data Model for the error.</summary>
    /// <seealso cref="PageModel"/>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        /// <summary>Gets or sets the identifier of the request.</summary>
        /// <value>The identifier of the request.</value>
        public string? RequestId { get; set; }

        /// <summary>Gets a value indicating whether the request identifier is shown.</summary>
        /// <value>True if show request identifier, false if not.</value>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>Executes the get action.</summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
