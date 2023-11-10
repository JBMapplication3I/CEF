// <copyright file="Index.cshtml.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the index.cshtml class</summary>
namespace Clarity.Ecommerce.SignalRCore.Pages
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

#pragma warning disable SA1649 // File name should match first type name
    public class IndexModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
#pragma warning disable IDE0052
        // ReSharper disable once NotAccessedField.Local
        private readonly ILogger<IndexModel> logger;
#pragma warning restore IDE0052

        private readonly IService service;

        public IndexModel(ILogger<IndexModel> logger, IService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [BindProperty]
        public StandardBid StandardBid { get; set; } = null!;

        public async Task OnGetAsync()
        {
            var hubs = new Hubs(service);
            var highestCurrentBid = await hubs.GetHighestCurrentBidAsync(lotID: 7).ConfigureAwait(false);
            ViewData["Message"] = $"Current Bid: ${highestCurrentBid ?? 0.00m}";
        }

        public async Task OnPostMaxBidAsync()
        {
            var hubs = new Hubs(service);
            await hubs.ProcessMaxAutoBidAsync(StandardBid).ConfigureAwait(false);
            ViewData["Message"] = $"{hubs.CurrentBid}";
        }

        public async Task OnPostQuickBidAsync()
        {
            var hubs = new Hubs(service);
            await hubs.ProcessQuickBidAsync(StandardBid).ConfigureAwait(false);
            ViewData["Message"] = $"{hubs.CurrentBid}";
        }
    }
}
