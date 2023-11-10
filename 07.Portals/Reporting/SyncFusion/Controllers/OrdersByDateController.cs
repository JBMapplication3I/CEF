// <copyright file="OrdersByDateController.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the orders by date controller class</summary>
namespace Clarity.Ecommerce.Reporting.Syncfusion.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class OrdersByDateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
