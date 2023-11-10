namespace Clarity.Ecommerce.UI.Testing.Tests
{
    using System;
    using System.Threading;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class Discount : TestsBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SetProperties(testContext);
        }

        [TestMethod]
        public void AddAndRemoveOrderDiscount_AmountValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffOrderDiscountValue]);
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddOrderDiscountToCart(
                code: Ids.TenDollarsOffOrderDiscountCode,
                type: Ids.OrderDiscountType,
                name: Ids.TenDollarsOffOrderDiscountName,
                value: dollarOff.ToString(MoneyFormat) + " Off",
                discountsTotal: (Math.Min(product1Price, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - Math.Min(product1Price, dollarOff)).ToString(MoneyFormat));
            Driver.RemoveOrderDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveOrderDiscount_PercentValueType()
        {
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffOrderDiscountValue]);
            var discountAmount = Math.Round(percOff * product1Price, 2, MidpointRounding.AwayFromZero);
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddOrderDiscountToCart(
                code: Ids.FiftyPercentOffOrderDiscountCode,
                type: Ids.OrderDiscountType,
                name: Ids.FiftyPercentOffOrderDiscountName,
                value: percOff.ToString(PercentFormat) + " Off",
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - discountAmount).ToString(MoneyFormat));
            Driver.RemoveOrderDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveShipmentDiscount_AmountValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            GetToShippingRatesInCheckout();
            // TODO
            Assert.IsTrue(Driver.ClickById(Ids.CartShipmentInfoCarrierRadioButtonId, ShipmentCarrierFedExGroundCustomKey));
            var shippingAmount = decimal.Parse(Driver.GetWebElementById(Ids.TotalsWidgetShippingId).Text.Remove(0, 1));
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffShipmentDiscountValue]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddShipmentDiscountToCart(
                code: Ids.TenDollarsOffShipmentDiscountCode,
                type: Ids.ShipmentDiscountType,
                name: Ids.TenDollarsOffShipmentDiscountName,
                value: dollarOff.ToString(MoneyFormat) + " Off",
                discountsTotal: (Math.Min(shippingAmount, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price + Math.Max(0m, shippingAmount - dollarOff)).ToString(MoneyFormat));
            Driver.RemoveShipmentDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: (product1Price + shippingAmount).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveShipmentDiscount_PercentValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            GetToShippingRatesInCheckout();
            // TODO
            Assert.IsTrue(Driver.ClickById(Ids.CartShipmentInfoCarrierRadioButtonId, ShipmentCarrierFedExGroundCustomKey));
            var shippingAmount = decimal.Parse(Driver.GetWebElementById(Ids.TotalsWidgetShippingId).Text.Remove(0, 1));
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffShipmentDiscountValue]);
            var discountAmount = Math.Round(percOff * shippingAmount, 2, MidpointRounding.AwayFromZero);
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddShipmentDiscountToCart(
                code: Ids.FiftyPercentOffShipmentDiscountCode,
                type: Ids.ShipmentDiscountType,
                name: Ids.FiftyPercentOffShipmentDiscountName,
                value: percOff.ToString(PercentFormat) + " Off",
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price + shippingAmount - discountAmount).ToString(MoneyFormat));
            Driver.RemoveShipmentDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: (product1Price + shippingAmount).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveShipmentFedexGroundDiscount_AmountValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            GetToShippingRatesInCheckout();
            // TODO
            Assert.IsTrue(Driver.ClickById(Ids.CartShipmentInfoCarrierRadioButtonId, ShipmentCarrierFedExGroundCustomKey));
            var shippingAmount = decimal.Parse(Driver.GetWebElementById(Ids.TotalsWidgetShippingId).Text.Remove(0, 1));
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffShipmentFedExGroundDiscountValue]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddShipmentDiscountToCart(
                code: Ids.TenDollarsOffShipmentFedExGroundDiscountCode,
                type: Ids.ShipmentDiscountType,
                name: Ids.TenDollarsOffShipmentFedExGroundDiscountName,
                value: dollarOff.ToString(MoneyFormat) + " Off",
                discountsTotal: (Math.Min(shippingAmount, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price + Math.Max(0m, shippingAmount - dollarOff)).ToString(MoneyFormat));
            Driver.RemoveShipmentDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: (product1Price + shippingAmount).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveShipmentFedexGroundDiscount_PercentValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            GetToShippingRatesInCheckout();
            // TODO
            Assert.IsTrue(Driver.ClickById(Ids.CartShipmentInfoCarrierRadioButtonId, ShipmentCarrierFedExGroundCustomKey));
            var shippingAmount = decimal.Parse(Driver.GetWebElementById(Ids.TotalsWidgetShippingId).Text.Remove(0, 1));
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffShipmentFedExGroundDiscountValue]);
            var discountAmount = Math.Round(percOff * shippingAmount, 2, MidpointRounding.AwayFromZero);
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddShipmentDiscountToCart(
                code: Ids.FiftyPercentOffShipmentFedExGroundDiscountCode,
                type: Ids.ShipmentDiscountType,
                name: Ids.FiftyPercentOffShipmentFedExGroundDiscountName,
                value: percOff.ToString(PercentFormat) + " Off",
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price + shippingAmount - discountAmount).ToString(MoneyFormat));
            Driver.RemoveShipmentDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: (product1Price + shippingAmount).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveShipmentUPS2ndDayAirDiscount_AmountValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            GetToShippingRatesInCheckout();
            // TODO
            Assert.IsTrue(Driver.ClickById(Ids.CartShipmentInfoCarrierRadioButtonId, ShipmentCarrierUpsSecondDayAirCustomKey));
            var shippingAmount = decimal.Parse(Driver.GetWebElementById(Ids.TotalsWidgetShippingId).Text.Remove(0, 1));
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffShipmentUPSSecondDayAirDiscountValue]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddShipmentDiscountToCart(
                code: Ids.TenDollarsOffShipmentUPSSecondDayAirDiscountCode,
                type: Ids.ShipmentDiscountType,
                name: Ids.TenDollarsOffShipmentUPSSecondDayAirDiscountName,
                value: dollarOff.ToString(MoneyFormat) + " Off",
                discountsTotal: (dollarOff * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price + shippingAmount - dollarOff).ToString(MoneyFormat));
            Driver.RemoveShipmentDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: (product1Price + shippingAmount).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveShipmentUPS2ndDayAirDiscount_PercentValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            GetToShippingRatesInCheckout();
            // TODO
            Assert.IsTrue(Driver.ClickById(Ids.CartShipmentInfoCarrierRadioButtonId, ShipmentCarrierUpsSecondDayAirCustomKey));
            var shippingAmount = decimal.Parse(Driver.GetWebElementById(Ids.TotalsWidgetShippingId).Text.Remove(0, 1));
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffShipmentUPSSecondDayAirDiscountValue]);
            var discountAmount = Math.Round(percOff * shippingAmount, 2, MidpointRounding.AwayFromZero);
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddShipmentDiscountToCart(
                code: Ids.FiftyPercentOffShipmentUPSSecondDayAirDiscountCode,
                type: Ids.ShipmentDiscountType,
                name: Ids.FiftyPercentOffShipmentUPSSecondDayAirDiscountName,
                value: percOff.ToString(PercentFormat) + " Off",
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price + shippingAmount - discountAmount).ToString(MoneyFormat));
            Driver.RemoveShipmentDiscountFromCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: (product1Price + shippingAmount).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveProductDiscount_AmountValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffProductDiscountValue]);
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.TenDollarsOffProductDiscountCode,
                name: Ids.TenDollarsOffProductDiscountName,
                value: (product1Price - dollarOff).ToString(MoneyFormat),
                discountsTotal: (Math.Min(product1Price, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - Math.Min(product1Price, dollarOff)).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveProductDiscount_PercentValueType()
        {
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffProductDiscountValue]);
            var discountAmount = Math.Round(percOff * product1Price, 2, MidpointRounding.AwayFromZero);
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.FiftyPercentOffProductDiscountCode,
                name: Ids.FiftyPercentOffProductDiscountName,
                value: percOff.ToString(PercentFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - discountAmount).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveProductTypeDiscount_AmountValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffProductTypeDiscountValue]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.TenDollarsOffProductTypeDiscountCode,
                name: Ids.TenDollarsOffProductTypeDiscountName,
                value: (product1Price - dollarOff).ToString(MoneyFormat),
                discountsTotal: (Math.Min(product1Price, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - Math.Min(product1Price, dollarOff)).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveProductTypeDiscount_PercentValueType()
        {
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffProductTypeDiscountValue]);
            var discountAmount = Math.Round(percOff * product1Price, 2, MidpointRounding.AwayFromZero);
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.FiftyPercentOffProductTypeDiscountCode,
                name: Ids.FiftyPercentOffProductTypeDiscountName,
                value: percOff.ToString(PercentFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - discountAmount).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveProductCategoryDiscount_AmountValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffProductCategoryDiscountValue]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.TenDollarsOffProductCategoryDiscountCode,
                name: Ids.TenDollarsOffProductCategoryDiscountName,
                value: (product1Price - dollarOff).ToString(MoneyFormat),
                discountsTotal: (Math.Min(product1Price, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - Math.Min(product1Price, dollarOff)).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveProductCategoryDiscount_PercentValueType()
        {
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffProductCategoryDiscountValue]);
            var discountAmount = Math.Round(
                percOff * product1Price, 2, MidpointRounding.AwayFromZero);
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.FiftyPercentOffProductCategoryDiscountCode,
                name: Ids.FiftyPercentOffProductCategoryDiscountName,
                value: percOff.ToString(PercentFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - discountAmount).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveBuy2Get1ProductDiscount_AmountValueType()
        {
            const int Quantity = 3;
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl), Quantity);
            var product1Price = decimal.Parse(this[Ids.Product06Price]);
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffBuy2Get1ProductDiscountValue]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.TenDollarsOffBuy2Get1ProductDiscountCode,
                name: Ids.TenDollarsOffBuy2Get1ProductDiscountName,
                value: (product1Price * Quantity - dollarOff).ToString(MoneyFormat),
                discountsTotal: (Math.Min(product1Price, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: (product1Price * Quantity).ToString(MoneyFormat),
                total: (product1Price * Quantity - Math.Min(product1Price, dollarOff)).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: (product1Price * Quantity).ToString(MoneyFormat),
                totalText: (product1Price * Quantity).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveBuy2Get1ProductDiscount_PercentValueType()
        {
            const int Quantity = 3;
            var price = decimal.Parse(this[Ids.Product06Price]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffBuy2Get1ProductDiscountValue]);
            var discountAmount = Math.Round(
                percOff * price, 2, MidpointRounding.AwayFromZero);
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl), Quantity);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.FiftyPercentOffBuy2Get1ProductDiscountCode,
                name: Ids.FiftyPercentOffBuy2Get1ProductDiscountName,
                value: percOff.ToString(PercentFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: (price * Quantity).ToString(MoneyFormat),
                total: (price * Quantity - discountAmount).ToString(MoneyFormat));
            Driver.RemoveDiscountFromCartItem(
                subtotalText: (price * Quantity).ToString(MoneyFormat),
                totalText: (price * Quantity).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveBuy2Get1ProductCategoryDiscount_AmountValueType()
        {
            // Category: Movies
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl));
            Driver.AddCartQuickSearchItem(Ids.Product07CustomKey, Ids.Product07Name);
            Driver.AddCartQuickSearchItem(Ids.Product08CustomKey, Ids.Product08Name);
            // Product7 has lowest price - it should get discount
            var product7Price = decimal.Parse(this[Ids.Product07Price]);
            var priceMath = product7Price + decimal.Parse(this[Ids.Product06Price]) + decimal.Parse(this[Ids.Product08Price]);
            var discountAmount = Math.Min(product7Price, decimal.Parse(this[Ids.TenDollarsOffBuy2Get1ProductCategoryDiscountValue]));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.TenDollarsOffBuy2Get1ProductCategoryDiscountCode,
                name: Ids.TenDollarsOffBuy2Get1ProductCategoryDiscountName,
                value: (product7Price - discountAmount).ToString(MoneyFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: priceMath.ToString(MoneyFormat),
                total: (priceMath - discountAmount).ToString(MoneyFormat),
                productIndex: "1");
            Driver.RemoveDiscountFromCartItem(
                subtotalText: priceMath.ToString(MoneyFormat),
                totalText: priceMath.ToString(MoneyFormat),
                productIndex: "1");
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveBuy2Get1ProductCategoryDiscount_PercentValueType()
        {
            var product7Price = decimal.Parse(this[Ids.Product07Price]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffBuy2Get1ProductCategoryDiscountValue]);
            var discountAmount = Math.Round(percOff * product7Price, 2, MidpointRounding.AwayFromZero);
            // Category: Movies
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl));
            Driver.AddCartQuickSearchItem(Ids.Product07CustomKey, Ids.Product07Name);
            Driver.AddCartQuickSearchItem(Ids.Product08CustomKey, Ids.Product08Name);
            // Product7 has lowest price - it should get discount
            var priceMath = product7Price + decimal.Parse(this[Ids.Product06Price]) + decimal.Parse(this[Ids.Product08Price]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.FiftyPercentOffBuy2Get1ProductCategoryDiscountCode,
                name: Ids.FiftyPercentOffBuy2Get1ProductCategoryDiscountName,
                value: percOff.ToString(PercentFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: priceMath.ToString(MoneyFormat),
                total: (priceMath - discountAmount).ToString(MoneyFormat),
                productIndex: "1");
            Driver.RemoveDiscountFromCartItem(
                subtotalText: priceMath.ToString(MoneyFormat),
                totalText: priceMath.ToString(MoneyFormat),
                productIndex: "1");
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveBuy2Get1ProductTypeDiscount_AmountValueType()
        {
            // Category: Movies
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl));
            Driver.AddCartQuickSearchItem(Ids.Product07CustomKey, Ids.Product07Name);
            Driver.AddCartQuickSearchItem(Ids.Product08CustomKey, Ids.Product08Name);
            // Product7 has lowest price - it should get discount
            var product7Price = decimal.Parse(this[Ids.Product07Price]);
            var priceMath = product7Price + decimal.Parse(this[Ids.Product06Price]) + decimal.Parse(this[Ids.Product08Price]);
            var discountAmount = Math.Min(product7Price, decimal.Parse(this[Ids.TenDollarsOffBuy2Get1ProductTypeDiscountValue]));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.TenDollarsOffBuy2Get1ProductTypeDiscountCode,
                name: Ids.TenDollarsOffBuy2Get1ProductTypeDiscountName,
                value: (product7Price - discountAmount).ToString(MoneyFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: priceMath.ToString(MoneyFormat),
                total: (priceMath - discountAmount).ToString(MoneyFormat),
                productIndex: "1");
            Driver.RemoveDiscountFromCartItem(
                subtotalText: priceMath.ToString(MoneyFormat),
                totalText: priceMath.ToString(MoneyFormat),
                productIndex: "1");
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveBuy2Get1ProductTypeDiscount_PercentValueType()
        {
            var product7Price = decimal.Parse(this[Ids.Product07Price]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffBuy2Get1ProductTypeDiscountValue]);
            var discountAmount = Math.Round(
                percOff * product7Price, 2, MidpointRounding.AwayFromZero);
            // Category: Movies
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product06SeoUrl));
            Driver.AddCartQuickSearchItem(Ids.Product07CustomKey, Ids.Product07Name);
            Driver.AddCartQuickSearchItem(Ids.Product08CustomKey, Ids.Product08Name);
            // Product6 has lowest price - it should get discount
            var priceMath = product7Price + decimal.Parse(this[Ids.Product06Price]) + decimal.Parse(this[Ids.Product08Price]);
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToCart(
                code: Ids.FiftyPercentOffBuy2Get1ProductTypeDiscountCode,
                name: Ids.FiftyPercentOffBuy2Get1ProductTypeDiscountName,
                value: percOff.ToString(PercentFormat),
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: priceMath.ToString(MoneyFormat),
                total: (priceMath - discountAmount).ToString(MoneyFormat),
                productIndex: "1");
            Driver.RemoveDiscountFromCartItem(
                subtotalText: priceMath.ToString(MoneyFormat),
                totalText: priceMath.ToString(MoneyFormat),
                productIndex: "1");
            Driver.Close();
        }

        // Discounts for MiniCart
        [TestMethod]
        public void AddAndRemoveOrderDiscountFromCheckout_PercentValueType()
        {
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffOrderTypeDiscountValue]);
            var discountAmount = Math.Round(
                percOff * product1Price, 2, MidpointRounding.AwayFromZero);
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddOrderDiscountToMiniCart(
                code: Ids.FiftyPercentOffOrderTypeDiscountCode,
                type: Ids.OrderDiscountType,
                name: Ids.FiftyPercentOffOrderTypeDiscountName,
                value: percOff.ToString(PercentFormat) + " Off",
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - discountAmount).ToString(MoneyFormat));
            Driver.RemoveOrderDiscountFromMiniCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveProductDiscountFromCheckout_AmountValueType()
        {
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var dollarOff = decimal.Parse(this[Ids.TenDollarsOffItemProductTypeDiscountValue]);
            var discountValue = Math.Min(product1Price, dollarOff);
            var value = discountValue == product1Price ? "FREE" : (product1Price - discountValue).ToString(MoneyFormat);
            if (int.TryParse(value, out var valueInt) && valueInt < 0)
            {
                Assert.Fail();
            }
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddProductDiscountToMiniCart(
                code: Ids.TenDollarsOffItemProductTypeDiscountCode,
                name: Ids.TenDollarsOffItemProductTypeDiscountName,
                value: value,
                discountsTotal: (Math.Min(product1Price, dollarOff) * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: Math.Max(0, product1Price - dollarOff).ToString(MoneyFormat));
            Driver.RemoveDiscountFromMiniCartItem(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: product1Price.ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void AddAndRemoveShipmentDiscountFromCheckout_PercentValueType()
        {
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            GetToShippingRatesInCheckout();
            Assert.IsTrue(Driver.ClickById(
                Ids.MiniCartShipmentInfoCarrierRadioButtonId, ShipmentCarrierUpsGroundCustomKey));
            Thread.Sleep(250);
            var shippingAmount = decimal.Parse(Driver.GetWebElementById(Ids.CartTotalsWidgetShippingId).Text.Remove(0, 1));
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffShipTypeDiscountValue]);
            var discountAmount = Math.Round(percOff * shippingAmount, 2, MidpointRounding.AwayFromZero);
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            Driver.AddShipmentDiscountToMiniCart(
                code: Ids.FiftyPercentOffShipTypeDiscountCode,
                type: Ids.ShipmentDiscountType,
                name: Ids.FiftyPercentOffShipTypeDiscountName,
                value: percOff.ToString(PercentFormat) + " Off",
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price + shippingAmount - discountAmount).ToString(MoneyFormat));
            Driver.RemoveShipmentDiscountFromMiniCart(
                subtotalText: product1Price.ToString(MoneyFormat),
                totalText: (product1Price + shippingAmount).ToString(MoneyFormat));
            Driver.Close();
        }

        [TestMethod]
        public void ValidateCartTotalOnReloadWithDiscountsFromCheckout_PercentValueType()
        {
            var product1Price = decimal.Parse(this[Ids.Product01SalePrice]);
            var percOff = decimal.Parse(this[Ids.FiftyPercentOffOrderTypeDiscountValue]);
            var discountAmount = Math.Round(percOff * product1Price, 2, MidpointRounding.AwayFromZero);
            Driver.AddProductToCart(GenerateFullProductDetailsPageUrl(Ids.Product01SeoUrl));
            Assert.IsTrue(Driver.GoToPageByUrl(GenerateFullCheckoutPageUrl()));
            Driver.AddOrderDiscountToMiniCart(
                code: Ids.FiftyPercentOffOrderTypeDiscountCode,
                type: Ids.OrderDiscountType,
                name: Ids.FiftyPercentOffOrderTypeDiscountName,
                value: percOff.ToString(PercentFormat) + " Off",
                discountsTotal: (discountAmount * -1m).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                total: (product1Price - discountAmount).ToString(MoneyFormat));
            Driver.ValidateCartTotalsWidget(
                total: (product1Price - discountAmount).ToString(MoneyFormat),
                subtotal: product1Price.ToString(MoneyFormat),
                discount: (discountAmount * -1m).ToString(MoneyFormat));
            Driver.Close();
        }

        private void GetToShippingRatesInCheckout()
        {
            Driver.ClickById(Ids.CheckoutAsGuestButton);
            Driver.AddCheckoutBillingAddress(
                FirstName: "Bob",
                LastName: "Ross",
                Email: "happylittleaccidents@test.com",
                Phone: "555-555-5555",
                Company: Ids.Company,
                Country: Ids.Country,
                Street1: Ids.Street1,
                City: Ids.City,
                Region: Ids.Region,
                ZipCode: Ids.ZipCode,
                Submit: Ids.CheckoutBillingSubmit
            );
            Driver.AddCheckoutShippingAddress(
                FirstName: "Bob",
                LastName: "Ross",
                Email: "happylittleaccidents@test.com",
                Phone: "555-555-5555",
                Company: Ids.Company,
                Country: Ids.Country,
                Street1: Ids.Street1,
                City: Ids.City,
                Region: Ids.Region,
                ZipCode: Ids.ZipCode,
                Submit: Ids.CheckoutReestimateShipping
            );
        }
    }
}
