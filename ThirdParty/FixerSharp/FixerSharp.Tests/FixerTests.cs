namespace FixerSharp.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FixerTests
    {
        [TestInitialize]
        public void TestInitialize() => Fixer.SetApiKey("API_KEY_HERE");

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Invalid_From_Symbol_Throws_Exception()
        {
            Fixer.Convert("ABC", "USD", 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Invalid_To_Symbol_ThrowsException()
        {
            Fixer.Convert("GBP", "XYZ", 100);
        }

        [TestMethod]
        public void Conversion_With_Symbol_Returns_Value()
        {
            var converted = Fixer.Convert(Symbols.GBP, Symbols.USD, 100);
            Assert.AreNotEqual(0d, converted);
        }

        [TestMethod]
        public void Conversion_With_String_Returns_Value()
        {
            var converted = Fixer.Convert("USD", "EUR", 100);
            Assert.AreNotEqual(0d, converted);
        }

        [TestMethod]
        public void Rate_With_Symbol_Returns_Value()
        {
            var rate = Fixer.Rate(Symbols.GBP, Symbols.DKK);
            Assert.IsNotNull(rate);
        }

        [TestMethod]
        public void Rate_With_Strings_Returns_Value()
        {
            var rate = Fixer.Rate("USD", "GBP");
            Assert.IsNotNull(rate);
        }

        [TestMethod]
        public void Rate_Properties_Contain_Data()
        {
            var rate = Fixer.Rate(Symbols.GBP, Symbols.EUR);
            Assert.AreNotEqual(string.Empty, rate.From);
            Assert.AreNotEqual(string.Empty, rate.To);
            Assert.AreNotEqual(0, rate.Rate);
            Assert.AreNotEqual(default, rate.Date);
        }

        [TestMethod]
        public void Rates_For_Previous_Dates_Contain_Different_Data()
        {
            var rate1 = Fixer.Rate(Symbols.GBP, Symbols.EUR, new DateTime(2016, 10, 19));
            var rate2 = Fixer.Rate(Symbols.GBP, Symbols.EUR, new DateTime(2016, 10, 18));
            Assert.AreNotEqual(rate1.Rate, rate2.Rate);
            Assert.AreNotEqual(rate1.Date, rate2.Date);
        }

        [TestMethod]
        public void Conversion_From_Rate()
        {
            var rate = Fixer.Rate(Symbols.EUR, Symbols.GBP);
            Assert.AreNotEqual(0, rate.Convert(1));
            Assert.AreNotEqual(0, rate.Convert(100));
            Assert.AreNotEqual(0, rate.Convert(1_000));
            Assert.AreNotEqual(0, rate.Convert(10_000));
            Assert.AreNotEqual(0, rate.Convert(100_000));
            Assert.AreNotEqual(0, rate.Convert(1_000_000));
            Assert.AreNotEqual(0, rate.Convert(10_000_000));
            Assert.AreNotEqual(0, rate.Convert(100_000_000));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Invalid_Api_Key_ThrowsException()
        {
            Fixer.SetApiKey(null!);
            Fixer.Convert(Symbols.USD, Symbols.EUR, 100);
        }
    }
}
