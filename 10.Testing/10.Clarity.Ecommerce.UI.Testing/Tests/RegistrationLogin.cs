namespace Clarity.Ecommerce.UI.Testing.Tests
{
    using System;
    using System.Threading;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("UI Tests")]
    public class RegistrationLogin : TestsBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SetProperties(testContext);
        }

        [TestMethod]
        public void Tc36306_VerifyRegistrationProcess()
        {
            // Verify Registration page takes the required fields
            // https://clarity-ventures.visualstudio.com/CEF-Product/_workitems/edit/36306
            // WARNING: RUNNING THIS TEST REQUIRES CHANGING THE EMAILS ON THE DEFAULT SEED USERS FROM:
            // development@claritymis.com TO THE NEW FORMAT: {username.ToLower()}@claritydemos.com

            Assert.IsTrue(Driver.GoToPageByUrl(Ids.BaseUrl));

            // Step 1
            // Navigate to the top right and click the login button
            Assert.IsTrue(Driver.ClickById(Ids.MiniMenuLoginButtonId));
            Thread.Sleep(500); // Wait for the Modal to open

            // Step 2
            // Once the modal opens, click on "register for an account"
            Assert.IsTrue(Driver.ClickById(Ids.RegisterLinkId));
            Assert.AreEqual(GenerateFullRegistrationPageUrl(), Driver.Url);

            // Step 3
            // Enter "clarity" as the username
            Driver.GetWebElementById(Ids.RegistrationEmptyUsernameIconId);
            Assert.IsTrue(Driver.InputTextById(Ids.RegistrationUsernameId, Ids.UserName));
            Driver.WaitUntilJsReady();
            // See if this is visible
            Driver.GetWebElementById(Ids.RegistrationInvalidUsernameIconId);

            // Step 4
            // Delete and enter a username that doesn't exist
            var userName = Guid.NewGuid().ToString().Replace("-", string.Empty);
            Assert.IsTrue(Driver.InputTextById(Ids.RegistrationUsernameId, userName));
            Driver.WaitUntilJsReady();
            Driver.GetWebElementById(Ids.RegistrationValidUsernameIconId);

            // Step 5
            // Enter a 4 digit password
            Driver.GetWebElementById(Ids.RegistrationEmptyPasswordIconId);
            Assert.IsTrue(Driver.InputTextById(Ids.RegistrationPasswordId, "1234"));
            Driver.WaitUntilJsReady();
            Driver.GetWebElementById(Ids.RegistrationInvalidPasswordIconId);

            // Step 6
            // Delete it and enter "T3sting!" as password
#if NET5_0_OR_GREATER
            var password = Guid.NewGuid().ToString().Substring(0, 24);
#else
            var password = System.Web.Security.Membership.GeneratePassword(24, 1);
#endif
            Assert.IsTrue(Driver.InputTextById(Ids.RegistrationPasswordId, password));
            Driver.WaitUntilJsReady();
            Driver.GetWebElementById(Ids.RegistrationValidPasswordIconId);

            // Step 7
            // Enter a first name
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetFirstNameId, userName.Substring(0, userName.Length / 2), string.Empty));

            // Step 8
            // Enter a last name
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetLastNameId, userName.Substring(userName.Length / 2), string.Empty));

            // Step 9
            // Enter a valid email
            Driver.GetWebElementById(Ids.ContactWidgetEmptyEmailIconId, string.Empty);
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetEmailId, Ids.Email, string.Empty));
            Driver.WaitUntilJsReady();
            Driver.GetWebElementById(Ids.ContactWidgetInvalidEmailIconId, string.Empty);
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetEmailId, $"{userName}@email.com", string.Empty));
            Driver.WaitUntilJsReady();
            Driver.GetWebElementById(Ids.ContactWidgetValidEmailIconId, string.Empty);

            // Step 10
            // Enter a 10 digit phone number
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetPhoneId, "012-345-6789", string.Empty));
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetCompanyId, Ids.Company, string.Empty));

            // Step 11
            // Enter a street address
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetStreet1Id, Ids.Street1, string.Empty));
            Assert.IsTrue(Driver.ClickById(Ids.ContactWidgetAddMoreShippingButtonId, string.Empty));
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetStreet2Id, Ids.Street2, string.Empty));

            // Step 12
            // Select the country
            Driver.SelectCountryAndRegion(
                Ids.Country, Ids.ContactWidgetCountryId, Ids.Region, Ids.ContactWidgetStateId, string.Empty);

            // Step 13
            // Enter the city
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetCityId, Ids.City, string.Empty));

            // Step 14
            // Enter a zip code for the previously entered city
            Assert.IsTrue(Driver.InputTextById(Ids.ContactWidgetZipCodeId, Ids.ZipCode, string.Empty));

            // Step 15
            // Click the "I agree to terms of use" checkbox
            Assert.IsTrue(Driver.ClickById(Ids.RegistrationTermsAgreedCheckboxId));

            // Step 16
            // Click on register.
            Assert.IsTrue(Driver.ClickById(Ids.RegistrationRegisterButtonId));

            Driver.GetWebElementById(Ids.RegistrationConfirmationId);
            Driver.Close();
        }
    }
}
