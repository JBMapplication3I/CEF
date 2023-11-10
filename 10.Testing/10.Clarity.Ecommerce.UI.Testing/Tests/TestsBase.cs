namespace Clarity.Ecommerce.UI.Testing.Tests
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public abstract class TestsBase
    {
        public const string MoneyFormat = "$#,##0.00";
        public const string PercentFormat = "#,##0%";

        public static Dictionary<Ids, string> Parameters { get; } = new Dictionary<Ids, string>();

        ////public string this[string index]
        ////{
        ////    get
        ////    {
        ////        return Parameters[(Ids)Enum.Parse(typeof(Ids), index, true)];
        ////    }
        ////}

        public string this[Ids index]
        {
            get
            {
                return Parameters[index];
            }
        }

        #region Ship Carrier Methods (These will never change)
        protected const string ShipmentCarrierFedExExpressSaverCustomKey = "FEDEX_EXPRESS_SAVER";
        protected const string ShipmentCarrierFedExFirstClassOvernightCustomKey = "FIRST_OVERNIGHT";
        protected const string ShipmentCarrierFedExGroundCustomKey = "FEDEX_GROUND";
        protected const string ShipmentCarrierFedExPriorityOvernightCustomKey = "PRIORITY_OVERNIGHT";
        protected const string ShipmentCarrierFedExSecondDayAmCustomKey = "FEDEX_2_DAY_AM";
        protected const string ShipmentCarrierFedExSecondDayCustomKey = "FEDEX_2_DAY";
        protected const string ShipmentCarrierFedExStandardOvernightCustomKey = "STANDARD_OVERNIGHT";
        protected const string ShipmentCarrierUps3DaySelectCustomKey = "12";
        protected const string ShipmentCarrierUpsGroundCustomKey = "03";
        protected const string ShipmentCarrierUpsNextDayAirAmCustomKey = "14";
        protected const string ShipmentCarrierUpsNextDayAirCustomKey = "01";
        protected const string ShipmentCarrierUpsNextDayAirSaverCustomKey = "13";
        protected const string ShipmentCarrierUpsSecondDayAirAmCustomKey = "59";
        protected const string ShipmentCarrierUpsSecondDayAirCustomKey = "02";
        #endregion

        protected IWebDriver Driver = null!;

        [TestInitialize]
        public void Setup()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Window.Position = new Point(0, 0);
            Driver.Manage().Window.Size = new Size(1920, 1080);
        }

        [TestCleanup]
        public void TearDown()
        {
            Driver?.Quit();
        }

        protected string GenerateFullProductDetailsPageUrl(Ids productSeoUrl)
        {
            return this[Ids.BaseUrl]
                + string.Format(this[Ids.ProductPageRelativeUrlFormat], this[productSeoUrl]);
        }

        protected string GenerateFullCartPageUrl()
        {
            return this[Ids.BaseUrl] + this[Ids.CartPageRelativeUrl];
        }

        protected string GenerateFullCheckoutPageUrl()
        {
            return this[Ids.BaseUrl] + this[Ids.CheckoutPageRelativeUrl];
        }

        protected string GenerateFullRegistrationPageUrl()
        {
            return this[Ids.BaseUrl] + this[Ids.RegistrationPageRelativeUrl];
        }

        protected static void SetProperties(TestContext testContext)
        {
            foreach (var e in EnumExtensions.AsValues<Ids>())
            {
                var value = $"{testContext.Properties[e.ToString()]}";
                if (string.IsNullOrWhiteSpace($"{testContext.Properties[e.ToString()]}"))
                {
                    if (HaveThrowWithoutValueAttribute(e))
                    {
                        throw new ConfigurationErrorsException(
                            $"Missing '{e}' which is a required value for your run settings file");
                    }
                    var @default = TryGetDefaultValue(e);
                    if (string.IsNullOrWhiteSpace(@default))
                    {
                        Trace.WriteLine($"WARNING: Setting '{e}' is missing in your run settings file, please check "
                            + "the configs repo for the latest version of the file");
                        continue;
                    }
                    // We have a default value we can use
                    value = @default;
                }
                Parameters[e] = value;
            }
            // Cleanup Actions for specific values
            while (Parameters[Ids.BaseUrl].EndsWith("/"))
            {
                Parameters[Ids.BaseUrl] = Parameters[Ids.BaseUrl]
                    .Substring(0, Parameters[Ids.BaseUrl].Length - 1);
            }
            EnsurePrefixOnRelativeUrl(Ids.CartPageRelativeUrl);
            EnsurePrefixOnRelativeUrl(Ids.CheckoutPageRelativeUrl);
            EnsurePrefixOnRelativeUrl(Ids.CategoryPageRelativeUrlFormat);
            EnsurePrefixOnRelativeUrl(Ids.ProductPageRelativeUrlFormat);
        }

        private static bool HaveThrowWithoutValueAttribute(System.Enum value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            // ReSharper disable once InvertIf
            if (memberInfo.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute<ThrowWithoutValueAttribute>(false);
                if (attribute != null)
                {
                    return true;
                }
            }
            return false;
        }

        private static string? TryGetDefaultValue(System.Enum value)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            // ReSharper disable once InvertIf
            if (memberInfo.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute<DefaultValueAttribute>(false);
                if (attribute != null)
                {
                    return attribute.Value;
                }
            }
            return null;
        }

        private static void EnsurePrefixOnRelativeUrl(Ids parameter)
        {
            if (!Parameters[parameter].StartsWith("/"))
            {
                Parameters[parameter] = "/" + Parameters[parameter];
            }
        }
    }
}
