// <copyright file="HomeController.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the home controller class</summary>
namespace Clarity.Ecommerce.UI.MVCHost.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>A controller for handling homes.</summary>
    /// <seealso cref="Controller"/>
    public class HomeController : Controller
    {
        /// <summary>Gets the index.</summary>
        /// <returns>An IActionResult.</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
