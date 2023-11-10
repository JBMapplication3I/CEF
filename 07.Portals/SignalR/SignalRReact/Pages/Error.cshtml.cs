// <copyright file="Error.cshtml.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the error.cshtml class</summary>
namespace Clarity.Ecommerce.SignalRReact.Pages
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    /// <summary>A data Model for the error.</summary>
    /// <seealso cref="PageModel"/>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
#pragma warning disable SA1649 // File name should match first type name
    public class ErrorModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        /// <summary>(Immutable) The logger.</summary>
#pragma warning disable IDE0052
        // ReSharper disable once NotAccessedField.Local
        private readonly ILogger<ErrorModel> logger;
#pragma warning restore IDE0052

        /// <summary>Initializes a new instance of the <see cref="ErrorModel"/>
        /// class.</summary>
        /// <param name="logger">The logger.</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            this.logger = logger;
        }

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
