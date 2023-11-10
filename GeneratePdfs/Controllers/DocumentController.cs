namespace GeneratePdfs.Controllers
{
    using CefEntities;
    using IronPdf;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbContextOptions<ClarityEcommerceEntities> _options;

        public DocumentController(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, DbContextOptions<ClarityEcommerceEntities> options)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _options = options;
        }

        // POST: api/Document
        [HttpPost]
        public async Task<CEFActionResponse<PdfFileResult>> Post(List<int> salesOrderIds)
        {
            using var context = new ClarityEcommerceEntities(_options);
            var salesOrders = await (from so in context.SalesOrders
                join rq in context.RateQuotes on so.Id equals rq.SalesOrderId
                join sg in context.SalesGroups on so.SalesGroupAsSubId equals sg.Id
                join bc in context.Contacts on sg.BillingContactId equals bc.Id
                join bca in context.Addresses on bc.AddressId equals bca.Id
                join bcar in context.Regions on bca.RegionId equals bcar.Id
                join sc in context.Contacts on so.ShippingContactId equals sc.Id
                join sca in context.Addresses on sc.AddressId equals sca.Id
                join scar in context.Regions on sca.RegionId equals scar.Id
                where so.Active && rq.Active && rq.Selected && sg.Active && bc.Active && bca.Active && sc.Active && sca.Active
                && salesOrderIds.Contains(so.Id)
                select new
                {
                    SOID = so.Id,
                    SOCreatedDate = so.CreatedDate,
                    SOSubtotal = so.SubtotalItems,
                    SOShipping = so.SubtotalShipping,
                    SOTaxes = so.SubtotalTaxes,
                    SOTotal = so.Total,
                    SOBalanceDue = so.BalanceDue,
                    RQName = rq.Name,
                    BCFirstName = bc.FirstName,
                    BCLastName = bc.LastName,
                    BCEmail = bc.Email1,
                    BCPhone = bc.Phone1,
                    BCACompany = bca.Name,
                    BCAStreet1 = bca.Street1,
                    BCAStreet2 = bca.Street2,
                    BCAStreet3 = bca.Street3,
                    BCACity = bca.City,
                    BCARRegionName = bcar.Name,
                    BCAPostalCode = bca.PostalCode,
                    SCFirstName = sc.FirstName,
                    SCLastName = sc.LastName,
                    SCEmail = sc.Email1,
                    SCPhone = sc.Phone1,
                    SCACompany = sca.Name,
                    SCAStreet1 = sca.Street1,
                    SCAStreet2 = sca.Street2,
                    SCAStreet3 = sca.Street3,
                    SCACity = sca.City,
                    SCARRegionName = scar.Name,
                    SCAPostalCode = sca.PostalCode,
                })
            .AsNoTracking()
            .ToListAsync()
            .ConfigureAwait(false);

            var salesItems = (await (from soi in context.SalesOrderItems
                join p in context.Products on soi.ProductId equals p.Id
                where soi.Active && p.Active
                && salesOrderIds.Contains(soi.MasterId)
                select new
                {
                    soi.MasterId,
                    soi.Id,
                    soi.UnitSoldPrice,
                    soi.Quantity,
                    ExtendedPrice = soi.UnitSoldPrice * soi.Quantity,
                    ProductKey = p.CustomKey,
                    ProductName = p.Name,
                })
            .AsNoTracking()
            .ToListAsync()
            .ConfigureAwait(false))
            .GroupBy(x => x.MasterId)
            .ToDictionary(grp => grp.Key, grp => grp.ToList());

            var salesItemNotes = (await (from soi in context.SalesOrderItems
                join n in context.Notes on soi.Id equals n.SalesOrderItemId
                where soi.Active && n.Active
                && salesOrderIds.Contains(soi.MasterId)
                select new
                {
                    soi.Id,
                    n.Note1,
                })
            .AsNoTracking()
            .ToListAsync()
            .ConfigureAwait(false))
            .GroupBy(x => x.Id)
            .ToDictionary(grp => grp.Key, grp => grp.ToList());

            var result = new CEFActionResponse<PdfFileResult> { ActionSucceeded = false };
            string originalHtml;
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                originalHtml = await System.IO.File.ReadAllTextAsync(Path.Combine(currentDirectory, "invoice.html")).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result.Messages = new List<string> { ex.Message };
                return result;
            }

            PdfDocument? document = null;
            foreach (var model in salesOrders)
            {
                var html = PerformReplacements(originalHtml, model, ((IEnumerable<dynamic>)salesItems[model.SOID]).ToList(), salesItemNotes);
                var pdfPrintOptions = new ChromePdfRenderOptions()
                {
                    PaperSize = IronPdf.Rendering.PdfPaperSize.Letter,
                    TextFooter = new SimpleHeaderFooter()
                    {
                        RightText = "Page {page} of {total-pages}",
                        DrawDividerLine = false,
                        FontSize = 10,
                    },
                };
                PdfDocument? htmlToPdf;
                try
                {
                    htmlToPdf = await HtmlToPdf.StaticRenderHtmlAsPdfAsync(html, null, pdfPrintOptions).ConfigureAwait(false);
                    document = document == null ? htmlToPdf : document.AppendPdf(htmlToPdf);
                }
                catch (Exception ex)
                {
                    result.Messages = new List<string> { ex.Message };
                    return result;
                }
            }

            // var path = "C:\\Data\\Projects\\ATH\\CEF\\GeneratePdfs\\test_invoice.pdf";
            // System.IO.File.WriteAllBytes(path, document.BinaryData);
            return new CEFActionResponse<PdfFileResult>
            {
                ActionSucceeded = true,
                Result = new PdfFileResult
                {
                    FileName = "invoices.pdf",
                    BinaryData = document?.BinaryData
                }
            };
        }

        private string PerformReplacements(string html, dynamic model, List<dynamic> salesItems, dynamic salesItemNotes)
        {
            var tempHtml = html;
            tempHtml = tempHtml.Replace("{{OrderNumber}}", model.SOID.ToString());
            tempHtml = tempHtml.Replace("{{OrderDate}}", model.SOCreatedDate?.ToString("D"));
            tempHtml = tempHtml.Replace("{{ShippingMethod}}", model.RQName);

            tempHtml = tempHtml.Replace("{{BillingFullName}}", model.BCFirstName + " " + model.BCLastName);
            tempHtml = tempHtml.Replace("{{BillingCompany}}", model.BCACompany);
            tempHtml = tempHtml.Replace("{{BillingAddress}}", model.BCAStreet1 + " " + model.BCAStreet2 + " " + model.BCAStreet3);
            tempHtml = tempHtml.Replace("{{BillingCityStateZip}}", model.BCACity + ", " + model.BCARRegionName + " " + model.BCAPostalCode);
            tempHtml = tempHtml.Replace("{{BillingCountry}}", "United States");
            tempHtml = tempHtml.Replace("{{BillingPhone}}", model.BCPhone);
            tempHtml = tempHtml.Replace("{{BillingEmail}}", model.BCEmail);

            tempHtml = tempHtml.Replace("{{ShippingFullName}}", model.SCFirstName + " " + model.SCLastName);
            tempHtml = tempHtml.Replace("{{ShippingCompany}}", model.SCACompany);
            tempHtml = tempHtml.Replace("{{ShippingAddress}}", model.SCAStreet1 + " " + model.SCAStreet2 + " " + model.SCAStreet3);
            tempHtml = tempHtml.Replace("{{ShippingCityStateZip}}", model.SCACity + ", " + model.SCARRegionName + " " + model.SCAPostalCode);
            tempHtml = tempHtml.Replace("{{ShippingCountry}}", "United States");
            tempHtml = tempHtml.Replace("{{ShippingPhone}}", model.SCPhone);
            tempHtml = tempHtml.Replace("{{ShippingEmail}}", model.SCEmail);
            tempHtml = tempHtml.Replace("{{Subtotal}}", model.SOSubtotal?.ToString("C"));
            tempHtml = tempHtml.Replace("{{Shipping}}", model.SOShipping?.ToString("C"));
            tempHtml = tempHtml.Replace("{{TotalDue}}", (model.SOBalanceDue ?? model.SOTotal)?.ToString("C"));

            var itemDetails = string.Empty;
            for (var i = 0; i < salesItems.Count; i++)
            {
                itemDetails +=
                    @"<div class=""row cell-bottom " + (i % 2 == 1 ? string.Empty : "bgc-default-l4") + @""">
                        <div class=""col-5 small"" style=""left:5px;"">" + salesItems[i].ProductName + "<br />" + salesItems[i].ProductKey + @"{{ProductOptions}}</div>
                        <div class=""col-1 text-center small"">" + salesItems[i].Quantity + @"</div>
						<div class=""col-3 text-center small"">{{Notes}}</div>
                        <div class=""col-1 text-right small"">" + salesItems[i].UnitSoldPrice?.ToString("C") + @"</div>
                        <div class=""col-1 text-right small"">" + salesItems[i].ExtendedPrice?.ToString("C") + @"</div>
                    </div>";
                var notes = string.Empty;
                if (salesItemNotes.ContainsKey(salesItems[i].Id))
                {
                    foreach (var note in salesItemNotes[salesItems[i].Id])
                    {
                        notes += note.Note1 + "<br />";
                    }
                }
                itemDetails = itemDetails.Replace("{{Notes}}", notes);

                // Don't currently know where this is stored
                itemDetails = itemDetails.Replace("{{ProductOptions}}", string.Empty);
            }
            tempHtml = tempHtml.Replace("{{ItemDetails}}", itemDetails);
            
            return tempHtml;
        }
    }
}
