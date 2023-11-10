namespace ClarityTC.pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    internal class ClarityProductDetailsPage : Pages
    {
        //CART PARTS
        private const string AddACartButtonXpath = "//*[contains(text(),'Add to Cart')]";
        private const string AddToCartWindowXpath = "//div[@class='modal-content']";
        private const string ContinuesShoppingButtonXpath = "//button/child::span[contains(@data-translate,'continueShopping')]/..";
        //PRODUCT INFO
        private const string BookTitleXpath = "//h1[@class='cef-product-title mt ng-binding']";
        private const string ProductBasePriceXpath = "//span[contains(@ng-bind,'pdc.product.PriceBase') and @itemprop = 'price']";
        private const string ProductSalePriceXpath = "//span[contains(@ng-if,'pdc.product.PriceSale')]";
        private const string ProductStockInfoXpath = "//div[@class='cef-in-out-stock']/span[contains(@ng-if,'sumInventoryQuantities')]";
        private const string ProductSkuInfoXpath = "//p[@class='cef-sku-num']";
        private const string ProductUofMInfoXpath = "//p[@class='cef-uofm']";
        private const string ProductQuickDescriptionInfoXpath = "//div[@class='cef-quick-description']";
        //OTHER PARTS
        private const string CatalogLinkXpath = "//a[@ng-href='/Catalog']";
        private const string IncreaseAmountButtonXpath = "//button[contains(@ng-click,'pdc.quantity = pdc.quantity + 1')]";
        private const string DecreaseAmountButtonXpath = "//button[contains(@ng-click,'pdc.quantity = pdc.quantity - 1')]";
        //My ACCOUNT PARTS
        private const string MyAccountButtonXpath = "//button[contains(@class,'myAccount')]";
        private const string MyDashboardXpath = "//a[contains(@ng-click,'DashboardState:userDashboard.dashboard')]";
        //ADD TO LIST
        private const string SelectAShoppingListXpath = "//select[contains(@ng-model,'cefShoppingListsCtrl.listItem')]";
        private const string ShoppingListCreateListButtonXpath = "//button[contains(@data-translate, 'CreateAndAddAnother')]";
        private const string ShoppingListSaveListButtonXpath = "//button[contains(@ng-click,'cefShoppingListsCtrl.createShoppingList')]";
        private const string AddToShoppingListButtonXpath = "//button[contains(@ng-click,'cefShoppingListsCtrl.addToList')]";
        private const string AddToShoppingListXpath = "//div[@type='addToShoppingList']/descendant::button[@ng-show='isAuthed()']";
        private const string AddToShoppingListModalXpath = "//div[@class='modal-content']";
        //SOCIAL LINKS
        private const string ClarityFacebookButtonXpath = "//a/i[contains(@class,'fa-facebook-square')]/..";
        private const string ClarityTwitterButtonXpath = "//a/i[contains(@class,'fa-twitter-square')]/..";
        //FIND BY ID
        private const string ProductQtyInputID = "productQtyInput";
        private const string ShoppingListCreateNewInputID = "txtName";

        private const string OtherProductsImageXpath = "//a[@class='product-image']";
        private const string OtherProductsTitleXpath = "//a[contains(@class,'product-title')]";
        private const string OtherProductTitleTextXpath = "//a[contains(@class,'product-title')]/p";

        private IWebElement? clarityTwitterButton;
        private IWebElement? clarityFacebookButton;
        private readonly IWebElement addACartButton;
        private IWebElement? bookTitle;
        private readonly IWebElement catalogLink;
        private IWebElement? continuesShoppingButton;
        private IWebElement? increaseAmountButton;
        private IWebElement? decreaseAmountButton;
        private IWebElement? addToCartWindow;
        private IWebElement? productBaseSalePrice;
        private IWebElement? productQtyInput;
        private IWebElement? addToShoppingList;
        private IWebElement? selectAShoppingList;
        private IWebElement? addToShoppingListButton;
        private IWebElement? addToShoppingListModal;
        private IWebElement? myAccountButton;
        private IWebElement? myDashboard;
        private IWebElement? shoppingListCreateListButton;
        private IWebElement? shoppingListCreateNewInput;
        private IWebElement? shoppingListSaveListButton;
        private List<IWebElement>? otherProductsImage;
        private List<IWebElement>? otherProductsTitle;
        private List<IWebElement>? otherProductTitleText;

        private Actions? actions;

        public ClarityProductDetailsPage(IWebDriver driver) : base(driver)
        {
            addACartButton = WaitToFindElementByXpath(driver, AddACartButtonXpath, 100);
            bookTitle = WaitToFindElementByXpath(driver, BookTitleXpath, 100);
            catalogLink = WaitToBeClickEnableByXpath(driver, CatalogLinkXpath, 100);
        }

        public string OtherProductTitleText_get(int product)
        {
            otherProductTitleText = Driver.FindElements(By.XPath(OtherProductTitleTextXpath)).ToList();
            return otherProductTitleText[product - 1].GetAttribute("innerHTML");
        }

        public void OtherProductsImage_click(int product)
        {
            otherProductsImage = Driver.FindElements(By.XPath(OtherProductsImageXpath)).ToList();
            otherProductsImage[product - 1].Click();
        }

        public void OtherProductsTitle_click(int product)
        {
            otherProductsTitle = Driver.FindElements(By.XPath(OtherProductsTitleXpath)).ToList();
            otherProductsTitle[product - 1].Click();
        }
        public void clarityTwitterButton_click()
        {
            clarityTwitterButton = Driver.FindElement(By.XPath(ClarityTwitterButtonXpath));
            clarityTwitterButton.Click();
        }

        public void clarityFacebookButton_click()
        {
            clarityFacebookButton = Driver.FindElement(By.XPath(ClarityFacebookButtonXpath));
            clarityFacebookButton.Click();
        }

        public void addACartButton_click()
        {
            addACartButton.Click();
        }

        public bool addACartButton_checkEnable()
        {
            return Driver.FindElement(By.XPath(AddACartButtonXpath)).Enabled;
        }
        public string ReturnBookTitle()
        {
            bookTitle = WaitToFindElementByXpath(Driver, BookTitleXpath, 100);
            var bookTitleArray = bookTitle.GetAttribute("innerHTML").Split('<');
            var title = bookTitleArray[0];
            return title.Trim();
        }

        public void ContinuesShoppingButton_click()
        {
            continuesShoppingButton = Driver.FindElement(By.XPath(ContinuesShoppingButtonXpath));
            continuesShoppingButton.Click();
            System.Threading.Thread.Sleep(2000);
        }

        public void increaseAmountButton_click()
        {
            increaseAmountButton = Driver.FindElement(By.XPath(IncreaseAmountButtonXpath));
            increaseAmountButton.Click();
        }

        public void decreaseAmountButton_click()
        {
            decreaseAmountButton = Driver.FindElement(By.XPath(DecreaseAmountButtonXpath));
            decreaseAmountButton.Click();
        }

        public void AddToCartWindow_clickOutside()
        {
            addToCartWindow = Driver.FindElement(By.XPath(AddToCartWindowXpath));
            actions = new Actions(Driver);
            actions.MoveToElement(addToCartWindow, 300, 300).Click().Build().Perform();
            System.Threading.Thread.Sleep(2000);
        }

        public double ProductSalePrice_getValue()
        {
            try
            {
                productBaseSalePrice = Driver.FindElement(By.XPath(ProductSalePriceXpath));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                try
                {
                    productBaseSalePrice = Driver.FindElement(By.XPath(ProductBasePriceXpath));
                }
                catch (Exception o)
                {
                    Console.WriteLine(o);
                    return 0;
                }
            }
            var productPrice = productBaseSalePrice.GetAttribute("innerHTML").Split('$');
            return Convert.ToDouble(productPrice[1]);
        }

        public bool ProductInfoDetails_checkVisibility()
        {
            return Driver.FindElement(By.XPath(BookTitleXpath)).Displayed
                && Driver.FindElement(By.XPath(ProductSkuInfoXpath)).Displayed
                && Driver.FindElement(By.XPath(ProductUofMInfoXpath)).Displayed
                && Driver.FindElement(By.XPath(ProductQuickDescriptionInfoXpath)).Displayed
                && (Driver.FindElement(By.XPath(ProductBasePriceXpath)).Displayed
                    || Driver.FindElement(By.XPath(ProductSalePriceXpath)).Displayed);
        }

        public double productQtyInput_getValue()
        {
            productQtyInput = Driver.FindElement(By.Id(ProductQtyInputID));
            return Convert.ToDouble(productQtyInput.GetAttribute("value"));
        }

        public double ProductTotalAmountAddedToCart()
        {
            var productQuantityInput = productQtyInput_getValue();
            var productSalePrice = ProductSalePrice_getValue();
            return productSalePrice * productQuantityInput;
        }

        public bool AddToCartWindow_CheckIfDisplayed()
        {
            try
            {
                addToCartWindow = Driver.FindElement(By.XPath(AddToCartWindowXpath));
                return addToCartWindow.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddToShoppingList_click()
        {
            addToShoppingList = Driver.FindElement(By.XPath(AddToShoppingListXpath));
            addToShoppingList.Click();
        }

        public void SelectAShoppingList_selectOption(int selectIndex)
        {
            selectAShoppingList = Driver.FindElement(By.XPath(SelectAShoppingListXpath));
            var selectAShoppingListSelectElement = new SelectElement(selectAShoppingList);
            selectAShoppingListSelectElement.SelectByIndex(selectIndex);
        }

        public string SelectAShoppingList_getText()
        {
            selectAShoppingList = Driver.FindElement(By.XPath(SelectAShoppingListXpath));
            var selectAShoppingListSelectElement = new SelectElement(selectAShoppingList);
            return selectAShoppingListSelectElement.SelectedOption.Text;
        }

        public void AddToShoppingListButton_click()
        {
            addToShoppingListButton = Driver.FindElement(By.XPath(AddToShoppingListButtonXpath));
            addToShoppingListButton.Click();
        }

        public void AddToShoppingListModal_clickOutside()
        {
            addToShoppingListModal = Driver.FindElement(By.XPath(AddToShoppingListModalXpath));
            actions = new Actions(Driver);
            actions.MoveToElement(addToShoppingListModal, 300, 300).Click().Build().Perform();
            System.Threading.Thread.Sleep(1500);
        }

        public void myAccountButton_click()
        {
            myAccountButton = Driver.FindElement(By.XPath(MyAccountButtonXpath));
            myAccountButton.Click();
            System.Threading.Thread.Sleep(2000);
        }

        public void myDashboard_click()
        {
            myDashboard = Driver.FindElement(By.XPath(MyDashboardXpath));
            myDashboard.Click();
        }

        public void ShoppingListCreateListButton_click()
        {
            shoppingListCreateListButton = Driver.FindElement(By.XPath(ShoppingListCreateListButtonXpath));
            shoppingListCreateListButton.Click();
        }

        public void ShoppingListCreateNewInput_sendText(string textToSend)
        {
            shoppingListCreateNewInput = Driver.FindElement(By.Id(ShoppingListCreateNewInputID));
            shoppingListCreateNewInput.SendKeys(textToSend);
        }

        public void ShoppingListSaveListButton_click()
        {
            shoppingListSaveListButton = Driver.FindElement(By.XPath(ShoppingListSaveListButtonXpath));
            shoppingListSaveListButton.Click();
        }

        public bool ProductStockInfo_CheckDisplay()
        {
            return Driver.FindElement(By.XPath(ProductStockInfoXpath)).Displayed;
        }

        public string AddProductToShoppingList(int shoppingListToUse)
        {
            AddToShoppingList_click();
            WaitUntilJsReady();
            SelectAShoppingList_selectOption(shoppingListToUse);
            WaitUntilJsReady();
            var selectedShoppingList = SelectAShoppingList_getText();
            WaitUntilJsReady();
            AddToShoppingListButton_click();
            WaitUntilJsReady();
            AddToShoppingListModal_clickOutside();
            WaitUntilJsReady();
            return selectedShoppingList;
        }
    }
}
