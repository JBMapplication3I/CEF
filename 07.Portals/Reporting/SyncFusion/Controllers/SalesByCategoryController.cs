// <copyright file="SalesByCategoryController.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the accounts by region controller class</summary>
namespace Clarity.Ecommerce.Reporting.Syncfusion.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SalesByCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
