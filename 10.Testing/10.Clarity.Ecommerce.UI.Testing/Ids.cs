namespace Clarity.Ecommerce.UI.Testing
{
    public enum Ids
    {
        #region Base URL
        [ThrowWithoutValue]
        BaseUrl,
        #endregion
        #region Relative URLs
        [DefaultValue("/Cart")]
        CartPageRelativeUrl,
        [DefaultValue("/Checkout")]
        CheckoutPageRelativeUrl,
        [DefaultValue("/Catalog#!/c/p/Results/Format/grid/Page/1/Size/9/Sort/Relevance?category={0}%7C{1}")]
        CategoryPageRelativeUrlFormat,
        [DefaultValue("/Product/{0}")]
        ProductPageRelativeUrlFormat,
        [DefaultValue("/Authentication/Registration#!?returnUrl=%5BReturnUrl%5D")]
        RegistrationPageRelativeUrl,
        #endregion
        #region Credentials
        [ThrowWithoutValue]
        Email,
        [ThrowWithoutValue]
        UserName,
        [ThrowWithoutValue]
        Password,
        [ThrowWithoutValue]
        DisplayName,
        #endregion
        #region Address
        [DefaultValue("6805 N Capital of Texas Hwy")]
        Street1,
        [DefaultValue("Suite 312")]
        Street2,
        [DefaultValue("Austin")]
        City,
        [DefaultValue("United States of America")]
        Country,
        [DefaultValue("Texas")]
        Region,
        [DefaultValue("78731")]
        ZipCode,
        [DefaultValue("Clarity Ventures, Inc.")]
        Company,
        #endregion
        #region Categories Data
        [DefaultValue("16")]
        Category1Index,
        [DefaultValue("CAT-1")]
        Category1CustomKey,
        [DefaultValue("Movies")]
        Category1Name,
        [DefaultValue("7")]
        Category2Index,
        [DefaultValue("CAT-2")]
        Category2CustomKey,
        [DefaultValue("Books")]
        Category2Name,
        [DefaultValue("13")]
        Category3Index,
        [DefaultValue("CAT-3")]
        Category3CustomKey,
        [DefaultValue("Toys")]
        Category3Name,
        #endregion
        #region Products Data
        [DefaultValue("Acer® - Aspire 17.3\" Gaming Laptop")]
        Product01Name,
        [DefaultValue("432957")]
        Product01CustomKey,
        [DefaultValue("Acer-Aspire-Laptop-V3-772G-9850-16GB-1TB")]
        Product01SeoUrl,
        [DefaultValue("1299.99")]
        Product01BasePrice,
        [DefaultValue("917.99")]
        Product01SalePrice,

        [DefaultValue("Adobe® Acrobat XI Standard")]
        Product02Name,
        [DefaultValue("ADIDCS6AC")]
        Product02CustomKey,
        [DefaultValue("adobe-CS6-acrobat-XI-professional")]
        Product02SeoUrl,
        [DefaultValue("279.00")]
        Product02Price,

        [DefaultValue("Allen-Edmonds® Leather Shoe Care Kit")]
        Product06Name,
        [DefaultValue("GR35466")]
        Product06CustomKey,
        [DefaultValue("allen-edmonds-leather-shoe-care-kit")]
        Product06SeoUrl,
        [DefaultValue("94.95")]
        Product06Price,
        [DefaultValue("kit")]
        Product06UofM,
        [DefaultValue("Make sure your shoes never lose their shine with this Allen-Edmonds™ shoe care kit. With a couple of buffs here and some elbow grease there, you'll have your favorite pair of shoes back to their original luster in no time!...")]
        Product06QuickDescription,

        [DefaultValue("Woodlore® Men's Shoe Trees")]
        Product07Name,
        [DefaultValue("DSW3456976")]
        Product07CustomKey,
        [DefaultValue("woodlore-mens-cedar-shoe-tree-set")]
        Product07SeoUrl,
        [DefaultValue("24.99")]
        Product07Price,

        [DefaultValue("Apple iPhone X 256GB")]
        Product08Name,
        [DefaultValue("6009686")]
        Product08CustomKey,
        [DefaultValue("apple-iphone-x-256gb-space-grey")]
        Product08SeoUrl,
        [DefaultValue("1099.99")]
        Product08Price,

        [DefaultValue("Fury")]
        Product09Name,
        [DefaultValue("PROD-3")]
        Product09CustomKey,
        [DefaultValue("Fury")]
        Product09SeoUrl,
        [DefaultValue("12.99")]
        Product09Price,

        [DefaultValue("BIC® Mechanical Pencils, 0.7 mm, Black Barrel, Pack Of 12")]
        Product14Name,
        [DefaultValue("811943")]
        Product14CustomKey,
        [DefaultValue("BIC-mechanical-pencils-pack-12")]
        Product14SeoUrl,
        [DefaultValue("4.99m")]
        Product14Price,

        [DefaultValue("Energizer® Lithium Advanced AA Batteries, Pack Of 8")]
        Product15Name,
        [DefaultValue("308450")]
        Product15CustomKey,
        [DefaultValue("Energizer-Max-Alkaline-AA-Batteries-8-pack")]
        Product15SeoUrl,
        [DefaultValue("5.999")]
        Product15Price,
        #endregion
        #region Discounts Data
        [DefaultValue("Order Discount")]
        OrderDiscountType,

        // $10 off Order Discount
        [DefaultValue("10order")]
        TenDollarsOffOrderDiscountCode,
        [DefaultValue("10 dollar off order")]
        TenDollarsOffOrderDiscountName,
        [DefaultValue("10")]
        TenDollarsOffOrderDiscountValue,

        // 50% off Order Discount
        [DefaultValue("50PO")]
        FiftyPercentOffOrderDiscountCode,
        [DefaultValue("50 percent off")]
        FiftyPercentOffOrderDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffOrderDiscountValue,

        [DefaultValue("Shipping Discount")]
        ShipmentDiscountType,

        // $10 off Shipment Discount
        [DefaultValue("10shipment")]
        TenDollarsOffShipmentDiscountCode,
        [DefaultValue("Shipping $10 Off")]
        TenDollarsOffShipmentDiscountName,
        [DefaultValue("10")]
        TenDollarsOffShipmentDiscountValue,

        // 50% off Shipment Discount
        [DefaultValue("50POSHIP")]
        FiftyPercentOffShipmentDiscountCode,
        [DefaultValue("Shipping 50% Off")]
        FiftyPercentOffShipmentDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffShipmentDiscountValue,

        // $10 off ShipmentFedExGround Discount
        [DefaultValue("10shipFedExGround")]
        TenDollarsOffShipmentFedExGroundDiscountCode,
        [DefaultValue("Shipping $10 Off (FedEx Ground)")]
        TenDollarsOffShipmentFedExGroundDiscountName,
        [DefaultValue("10")]
        TenDollarsOffShipmentFedExGroundDiscountValue,

        // 50% off ShipmentFedExGround Discount
        [DefaultValue("50POshipFedExGround")]
        FiftyPercentOffShipmentFedExGroundDiscountCode,
        [DefaultValue("Shipping 50% Off (FedEx Ground)")]
        FiftyPercentOffShipmentFedExGroundDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffShipmentFedExGroundDiscountValue,

        // $10 off ShipmentUPS2ndDayAir Discount
        [DefaultValue("10shipUPS2ndDayAir")]
        TenDollarsOffShipmentUPSSecondDayAirDiscountCode,
        [DefaultValue("Shipping $10 Off (UPS 2nd Day Air)")]
        TenDollarsOffShipmentUPSSecondDayAirDiscountName,
        [DefaultValue("10")]
        TenDollarsOffShipmentUPSSecondDayAirDiscountValue,

        // 50% off ShipmentUPS2ndDayAir Discount
        [DefaultValue("50POUPS2ndDayAir")]
        FiftyPercentOffShipmentUPSSecondDayAirDiscountCode,
        [DefaultValue("Shipping 50% Off (UPS 2nd Day Air)")]
        FiftyPercentOffShipmentUPSSecondDayAirDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffShipmentUPSSecondDayAirDiscountValue,

        // $10 off Product Discount
        [DefaultValue("10product")]
        TenDollarsOffProductDiscountCode,
        [DefaultValue("Product $10 Off")]
        TenDollarsOffProductDiscountName,
        [DefaultValue("10")]
        TenDollarsOffProductDiscountValue,

        // 50% off Product Discount
        [DefaultValue("50POPRODUCT")]
        FiftyPercentOffProductDiscountCode,
        [DefaultValue("Product 50% Off")]
        FiftyPercentOffProductDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffProductDiscountValue,

        // $10 off ProductType Discount
        [DefaultValue("10productType")]
        TenDollarsOffProductTypeDiscountCode,
        [DefaultValue("ProductType $10 Off")]
        TenDollarsOffProductTypeDiscountName,
        [DefaultValue("10")]
        TenDollarsOffProductTypeDiscountValue,

        // 50% off ProductType Discount
        [DefaultValue("50POPRODUCTTYPE")]
        FiftyPercentOffProductTypeDiscountCode,
        [DefaultValue("ProductType 50% Off")]
        FiftyPercentOffProductTypeDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffProductTypeDiscountValue,

        // $10 off ProductCategory Discount
        [DefaultValue("10productCategory")]
        TenDollarsOffProductCategoryDiscountCode,
        [DefaultValue("ProductCategory $10 Off")]
        TenDollarsOffProductCategoryDiscountName,
        [DefaultValue("10")]
        TenDollarsOffProductCategoryDiscountValue,

        // 50% off ProductCategory Discount
        [DefaultValue("50POPRODUCTCATEGORY")]
        FiftyPercentOffProductCategoryDiscountCode,
        [DefaultValue("ProductCategory 50% Off")]
        FiftyPercentOffProductCategoryDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffProductCategoryDiscountValue,

        // Buy2Get1 $10 off Product Discount
        [DefaultValue("Buy2Get1-$10-Product")]
        TenDollarsOffBuy2Get1ProductDiscountCode,
        [DefaultValue("Buy2Get1 $10 Off Product")]
        TenDollarsOffBuy2Get1ProductDiscountName,
        [DefaultValue("10")]
        TenDollarsOffBuy2Get1ProductDiscountValue,

        // Buy2Get1 50% off Product Discount
        [DefaultValue("Buy2Get1-50%-Product")]
        FiftyPercentOffBuy2Get1ProductDiscountCode,
        [DefaultValue("Buy2Get1 50% Off Product")]
        FiftyPercentOffBuy2Get1ProductDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffBuy2Get1ProductDiscountValue,

        // Buy2Get1 $10 off ProductCategory Discount
        [DefaultValue("Buy2Get1-$10-ProdCat")]
        TenDollarsOffBuy2Get1ProductCategoryDiscountCode,
        [DefaultValue("Buy2Get1 $10 Off ProductCategory")]
        TenDollarsOffBuy2Get1ProductCategoryDiscountName,
        [DefaultValue("10")]
        TenDollarsOffBuy2Get1ProductCategoryDiscountValue,

        // Buy2Get1 50% off ProductCategory Discount
        [DefaultValue("Buy2Get1-50%-ProdCat")]
        FiftyPercentOffBuy2Get1ProductCategoryDiscountCode,
        [DefaultValue("Buy2Get1 50% Off ProductCategory")]
        FiftyPercentOffBuy2Get1ProductCategoryDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffBuy2Get1ProductCategoryDiscountValue,

        // Buy2Get1 $10 off ProductType Discount
        [DefaultValue("Buy2Get1-$10-ProdTyp")]
        TenDollarsOffBuy2Get1ProductTypeDiscountCode,
        [DefaultValue("Buy2Get1 $10 Off ProductType")]
        TenDollarsOffBuy2Get1ProductTypeDiscountName,
        [DefaultValue("10")]
        TenDollarsOffBuy2Get1ProductTypeDiscountValue,

        // Buy2Get1 50% off ProductType Discount
        [DefaultValue("Buy2Get1-50%-ProdTyp")]
        FiftyPercentOffBuy2Get1ProductTypeDiscountCode,
        [DefaultValue("Buy2Get1 50% Off ProductType")]
        FiftyPercentOffBuy2Get1ProductTypeDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffBuy2Get1ProductTypeDiscountValue,

        // 50% off Order Discount
        [DefaultValue("50PO")]
        FiftyPercentOffOrderTypeDiscountCode,
        [DefaultValue("50 percent off")]
        FiftyPercentOffOrderTypeDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffOrderTypeDiscountValue,

        // $10 off Product Discount
        [DefaultValue("10product")]
        TenDollarsOffItemProductTypeDiscountCode,
        [DefaultValue("Product $10 Off")]
        TenDollarsOffItemProductTypeDiscountName,
        [DefaultValue("10")]
        TenDollarsOffItemProductTypeDiscountValue,

        // 10% off Shipping Discount
        [DefaultValue("50POSHIP")]
        FiftyPercentOffShipTypeDiscountCode,
        [DefaultValue("Shipping 50% Off")]
        FiftyPercentOffShipTypeDiscountName,
        [DefaultValue("0.5")]
        FiftyPercentOffShipTypeDiscountValue,
        #endregion
        #region Categories Menu
        [DefaultValue("categoriesMenuLinkAcross")]
        CategoriesMenuLinkAcrossId,
        [DefaultValue("categoriesMenuLinkDown")]
        CategoriesMenuLinkDownId,
        [DefaultValue("categoriesMenuLinkMega")]
        CategoriesMenuLinkMegaId,
        [DefaultValue("menuItem_{0}")]
        CategoriesMenuCategoryMegaLinkId,
        [DefaultValue("categoriesMenuCategorySeeAllLinkFor{0}.item")]
        CategoriesMenuCategoryNoChildrenSeeAllItemLinkId,
        [DefaultValue("categoriesMenuCategorySeeAllLinkFor{0}.{1}.header")]
        CategoriesMenuCategoryWithChildrenSeeAllHeaderLinkId,
        [DefaultValue("categoriesMenuCategorySeeAllLinkFor{0}.{1}.item")]
        CategoriesMenuCategoryWithChildrenSeeAllItemLinkId,
        [DefaultValue("categoriesMenuCategorySeeAllLinkFor{0}.{1}.{2}.item")]
        CategoriesMenuCategoryWithSubChildrenSeeAllItemLinkId,
        #endregion
        #region Product Catalog Page
        [DefaultValue("productCatalogDetailsLinkFor{0}")]
        ProductCatalogDetailsLinkForId,
        #endregion
        #region Product Details Page
        [DefaultValue("productName")]
        ProductDetailsProductName,
        [DefaultValue("productCustomKey")]
        ProductDetailsProductCustomKey,
        [DefaultValue("productUofM")]
        ProductDetailsProductUofM,
        [DefaultValue("productPrice")]
        ProductDetailsProductPrice,
        [DefaultValue("productShortDescription")]
        ProductDetailsProductQuickDescription,
        [DefaultValue("productDetailsAddToCartButton")]
        ProductDetailsAddToCartButtonId,
        [DefaultValue("btnIncreaseProductQuantity")]
        ProductDetailsIncreaseProductQuantity,
        [DefaultValue("btnDecreaseProductQuantity")]
        ProductDetailsDecreaseProductQuantity,
        [DefaultValue("productDetailAddToCartModalCartTotal")]
        ProductDetailsAddToCartModalCartTotalId,
        [DefaultValue("productDetailsViewCartHref")]
        ProductDetailsViewCartHrefButtonId,
        [DefaultValue("productDetailsViewCartClose")]
        ProductDetailsViewCartCloseButtonId,
        [DefaultValue("btnProductDetailsAddToWishList")]
        ProductDetailsAddToWishListButtonId,
        [DefaultValue("btnProductDetailsRemoveFromWishList")]
        ProductDetailsRemoveFromWishListButtonId,
        [DefaultValue("productQtyInput")]
        ProductDetailsQuantityInputId,
        [DefaultValue("tabMoreInformation")]
        ProductDetailsTabMoreInformation,
        [DefaultValue("tabReviews")]
        ProductDetailsTabReviews,
        [DefaultValue("productDetailsRatingStar{0}")]
        ProductDetailsRatingStars,
        [DefaultValue("txtTitle")]
        ProductDetailsRatingTitle,
        [DefaultValue("txtaComment")]
        ProductDetailsRatingComment,
        [DefaultValue("btnSaveReview")]
        ProductDetailsSubmitRatingButton,
        [DefaultValue("reviewSuccess")]
        ProductDetailsReviewSuccessString,
        [DefaultValue("addToShoppingListBtn")]
        ProductDetailsAddToShoppingListBtn,
        [DefaultValue("addToShoppingListModalBtn")]
        ProductDetailsAddToShoppingListModalBtn,
        [DefaultValue("shoppingListSelect")]
        ProductDetailsShoppingListSelect,
        [DefaultValue("txtName")]
        ProductDetailsShoppingListCreate,
        [DefaultValue("wishListCreateBtn")]
        ProductDetailsWishListCreateBtn,
        [DefaultValue("txtName")]
        ProductDetailsWishListCreateTxt,
        [DefaultValue("submitCreateListBtn")]
        ProductDetailsSubmitCreateListBtn,
        [DefaultValue("closeShoppingListModal")]
        ProductDetailsCloseShoppingListModal,
        [DefaultValue("productDetailsStock")]
        ProductDetailsStock,
        [DefaultValue("cartProductImageThumbnail{0}")]
        ProductDetailsCartProductImageThumbnailId,
        [DefaultValue("cartProductName{0}")]
        ProductDetailsCartProductNameId,
        [DefaultValue("st-1")]
        ProductDetailsShareThisOne,
        [DefaultValue("closeSelectShoppingListModal")]
        ProductDetailsCloseSelectShoppingListModal,
        [DefaultValue("shoppingListDetailsBtn{0}")]
        ProductDetailsShoppingListDetailsBtn,
        [DefaultValue("product{0}Name")]
        ProductDetailsShoppingListProductName,
        [DefaultValue("relatedProductsScrollLeft")]
        ProductDetailsRelatedProductsScrollLeft,
        [DefaultValue("relatedProductsScrollRight")]
        ProductDetailsRelatedProductsScrollRight,
        #endregion
        #region Cart Page
        [DefaultValue("No Items have been added to the Cart.")]
        EmptyCartText,
        [DefaultValue("btnIncreaseItemQuantity{0}")]
        CartIncreaseProductQuantityButtonId,
        [DefaultValue("btnReduceItemQuantity{0}")]
        CartDecreaseProductQuantityButtonId,
        [DefaultValue("btnRemoveItem{0}FromCart")]
        CartRemoveProductButtonId,
        [DefaultValue("removeItemFromCartModal{0}")]
        CartRemoveProductModalId,
        [DefaultValue("cartRemoveProductModalCancelButton{0}")]
        CartRemoveProductModalCancelButtonId,
        [DefaultValue("cartRemoveProductModalConfirmationButton{0}")]
        CartRemoveProductModalConfirmationButtonId,
        [DefaultValue("cartEmptyLabel")]
        CartEmptyLabelId,
        [DefaultValue("txtQuickAddSearch")]
        CartQuickOrderSearchInputId,
        [DefaultValue("btnIncreaseItemQuantity")]
        CartIncreaseQuickSearchItemQuantityButtonId,
        [DefaultValue("btnReduceItemQuantity")]
        CartDecreaseQuickSearchItemQuantityButtonId,
        [DefaultValue("nudItemQuantity")]
        CartQuickSearchItemQuantityId,
        [DefaultValue("btnAddQuickOrderItemToCart")]
        CartQuickSearchAddItemId,
        [DefaultValue("btnProceedToCheckout")]
        CartProceedToCheckoutButtonId,
        [DefaultValue("txtCheckoutUserName")]
        CartCheckoutMethodLoginUsernameId,
        [DefaultValue("pwCheckoutPassword")]
        CartCheckoutMethodLoginPasswordId,
        [DefaultValue("checkoutLoginButton")]
        CartCheckoutMethodLoginButtonId,
        [DefaultValue("checkoutLoginErrorMessage")]
        CartCheckoutMethodLoginErrorMessageId,
        #endregion
        #region Wish List Buttons
        [DefaultValue("btnRemoveItem{0}FromWishList")]
        WishListRemoveProductButtonId,
        [DefaultValue("removeItemFromWishListModal{0}")]
        WishListRemoveProductModalId,
        #endregion
        #region Favorites List Buttons
        [DefaultValue("btnRemoveItem{0}FromFavoritesList")]
        FavoritesListRemoveProductButtonId,
        [DefaultValue("removeItemFromFavoritesListModal{0}")]
        FavoritesListRemoveProductModalId,
        #endregion
        #region Notify Me List Buttons
        [DefaultValue("btnRemoveItem{0}FromNotifyMeList")]
        NotifyMeListRemoveProductButtonId,
        [DefaultValue("removeItemFromNotifyMeListModal{0}")]
        NotifyMeListRemoveProductModalId,
        #endregion
        #region Shopping List Buttons
        [DefaultValue("btnRemoveItem{0}FromShoppingList")]
        ShoppingListRemoveProductButtonId,
        [DefaultValue("removeItemFromShoppingListModal{0}")]
        ShoppingListRemoveProductModalId,
        #endregion
        #region Sales Quote Buttons
        [DefaultValue("btnRemoveItem{0}FromSalesQuote")]
        SalesQuoteRemoveProductButtonId,
        [DefaultValue("removeItemFromSalesQuoteModal{0}")]
        SalesQuoteRemoveProductModalId,
        #endregion
        #region Sales Lead BUttons
        [DefaultValue("btnRemoveItem{0}FromSalesLead")]
        SalesLeadRemoveProductButtonId,
        [DefaultValue("removeItemFromSalesLeadModal{0}")]
        SalesLeadRemoveProductModalId,
        #endregion
        #region Cart Products
        [DefaultValue("cartProductImageThumbnail{0}")]
        CartProductImageThumbnailId,
        [DefaultValue("cartProductSku{0}")]
        CartProductSkuId,
        [DefaultValue("cartProductName{0}")]
        CartProductNameId,
        [DefaultValue("cartProductPrice{0}")]
        CartProductPriceId,
        [DefaultValue("nudItemQuantity{0}")]
        CartProductQuantityId,
        [DefaultValue("cartProductSubtotal{0}")]
        CartProductSubtotalId,
        #endregion
        #region Cart Shipment Info
        [DefaultValue("ddlCountryID")]
        CartShipmentInfoCountryDdlId,
        [DefaultValue("ddlRegionID")]
        CartShipmentInfoRegionDdlId,
        [DefaultValue("txtPostalCode")]
        CartShipmentInfoZipCodeInputId,
        [DefaultValue("cartGetShippingRatesButton")]
        CartShipmentInfoGetRatesButtonId,
        [DefaultValue("rd_{0}")]
        CartShipmentInfoCarrierRadioButtonId,
        #endregion
        #region Cart Order Discounts
        [DefaultValue("orderDiscountType{0}")]
        CartOrderDiscountTypeId,
        [DefaultValue("orderDiscountName{0}")]
        CartOrderDiscountNameId,
        [DefaultValue("orderDiscountValue{0}")]
        CartOrderDiscountValueId,
        [DefaultValue("orderDiscountTotal{0}")]
        CartOrderDiscountTotalId,
        [DefaultValue("btnRemoveOrderDiscount{0}")]
        CartOrderDiscountRemoveButtonId,
        #endregion
        #region Cart Shipment Discounts
        [DefaultValue("shipDiscountType{0}")]
        CartShipmentDiscountTypeId,
        [DefaultValue("shipDiscountName{0}")]
        CartShipmentDiscountNameId,
        [DefaultValue("shipDiscountValue{0}")]
        CartShipmentDiscountValueId,
        [DefaultValue("shipDiscountTotal{0}")]
        CartShipmentDiscountTotalId,
        [DefaultValue("btnRemoveShipDiscount{0}")]
        CartShipmentDiscountRemoveButtonId,
        #endregion
        #region Cart Item Discounts
        [DefaultValue("miniCartProductDiscountDescription{0}.{1}")]
        CartProductDiscountDescriptionId,
        [DefaultValue("cartProductDiscountValue{0}.{1}")]
        CartProductDiscountValueId,
        [DefaultValue("btnRemoveDiscountFromItem{0}.{1}")]
        CartProductDiscountRemoveButtonId,
        #endregion
        #region Cart Discount
        [DefaultValue("txtCoupon")]
        CartDiscountInputId,
        [DefaultValue("btnApplyDiscountToCart")]
        CartDiscountApplyButtonId,
        #endregion
        #region Cart Totals
        [DefaultValue("cartTotalsSubtotal")]
        CartTotalsSubtotalId,
        [DefaultValue("cartTotalsShipping")]
        CartTotalsShippingId,
        [DefaultValue("cartTotalsHandling")]
        CartTotalsHandlingId,
        [DefaultValue("cartTotalsFees")]
        CartTotalsFeesId,
        [DefaultValue("cartTotalsDiscounts")]
        CartTotalsDiscountsId,
        [DefaultValue("cartTotalsTaxes")]
        CartTotalsTaxesId,
        [DefaultValue("cartTotalsTotal")]
        CartTotalsTotalId,
        #endregion
        #region Totals Widget
        [DefaultValue("cartTotalsWidgetSubtotal")]
        TotalsWidgetSubtotalId,
        [DefaultValue("cartTotalsWidgetShipping")]
        TotalsWidgetShippingId,
        [DefaultValue("cartTotalsWidgetHandling")]
        TotalsWidgetHandlingId,
        [DefaultValue("cartTotalsWidgetFees")]
        TotalsWidgetFeesId,
        [DefaultValue("cartTotalsWidgetDiscounts")]
        TotalsWidgetDiscountsId,
        [DefaultValue("cartTotalsWidgetTaxes")]
        TotalsWidgetTaxesId,
        [DefaultValue("cartTotalsWidgetTotal")]
        TotalsWidgetTotalId,
        #endregion
        #region Micro Cart
        [DefaultValue("microCartButton")]
        MicroCartButtonId,
        #endregion
        #region Mini Cart
        [DefaultValue("miniCartProductImageThumbnail{0}")]
        MiniCartProductImageThumbnailId,
        [DefaultValue("miniCartProductName{0}")]
        MiniCartProductNameId,
        [DefaultValue("miniCartProductPrice{0}")]
        MiniCartProductPriceId,
        [DefaultValue("miniCartProductQuantity{0}")]
        MiniCartProductQuantityId,
        [DefaultValue("miniCartProductDiscountDescription{0}.{1}")]
        MiniCartDiscountDescriptionId,
        [DefaultValue("rd_{0}")]
        MiniCartShipmentInfoCarrierRadioButtonId,
        [DefaultValue("shipDiscountName{0}")]
        MiniCartShipmentDiscountNameId,
        [DefaultValue("shipDiscountValue{0}")]
        MiniCartShipmentDiscountValueId,
        [DefaultValue("shipDiscountTotal{0}")]
        MiniCartShipmentDiscountTotalId,
        [DefaultValue("btnRemoveShipDiscount{0}")]
        MiniCartShipmentDiscountRemoveButtonId,
        #endregion
        #region Mini Menu
        [DefaultValue("miniMenuLoginButton")]
        MiniMenuLoginButtonId,
        [DefaultValue("miniMenuUserNameButton")]
        MiniMenuUserNameButtonId,
        [DefaultValue("txtUserName")]
        MiniMenuLoginUsername,
        [DefaultValue("pwPassword")]
        MiniMenuLoginPassword,
        [DefaultValue("loginButton")]
        MiniMenuSubmitLoginCredentials,
        [DefaultValue("miniMenuMyDashboardBtn")]
        MiniMenuMyDashboardBtn,
        [DefaultValue("miniMenuMyProfileBtn")]
        MiniMenuMyProfileBtn,
        [DefaultValue("miniMenuAccountProfileBtn")]
        MiniMenuAccountProfileBtn,
        [DefaultValue("miniMenuAddressBookBtn")]
        MiniMenuAddressBookBtn,
        [DefaultValue("miniMenuWalletBtn")]
        MiniMenuWalletBtn,
        [DefaultValue("miniMenuGroupsBtn")]
        MiniMenuGroupsBtn,
        [DefaultValue("miniMenuOrdersBtn")]
        MiniMenuOrdersBtn,
        [DefaultValue("miniMenuReturnsBtn")]
        MiniMenuReturnsBtn,
        [DefaultValue("miniMenuInvoicesBtn")]
        MiniMenuInvoicesBtn,
        [DefaultValue("miniMenuQuotesBtn")]
        MiniMenuQuotesBtn,
        [DefaultValue("miniMenuWishListBtn")]
        MiniMenuWishListBtn,
        [DefaultValue("miniMenuFavoritesListBtn")]
        MiniMenuFavoritesListBtn,
        [DefaultValue("miniMenuInStockAlertsBtn")]
        MiniMenuInStockAlertsBtn,
        [DefaultValue("miniMenuShoppingListsBtn")]
        MiniMenuShoppingListsBtn,
        [DefaultValue("miniMenuLogoutBtn")]
        MiniMenuLogoutBtn,
        #endregion
        #region Checkout
        [DefaultValue("checkoutMiniCart")]
        CheckoutMiniCartId,
        [DefaultValue("txtCheckoutUserName")]
        CheckoutUserName,
        [DefaultValue("pwCheckoutPassword")]
        CheckoutPassword,
        [DefaultValue("checkoutLoginButton")]
        CheckoutLoginButton,
        [DefaultValue("checkoutAsNewUser")]
        CheckoutAsGuestButton,
        [DefaultValue("txtShippingFirstName")]
        CheckoutShippingFirstName,
        [DefaultValue("txtShippingLastName")]
        CheckoutShippingLastName,
        [DefaultValue("txtShippingEmail")]
        CheckoutShippingEmail,
        [DefaultValue("txtShippingPhone")]
        CheckoutShippingPhone,
        [DefaultValue("txtShippingCompany")]
        CheckoutShippingCompany,
        [DefaultValue("ddlShippingCountry")]
        CheckoutShippingCountry,
        [DefaultValue("txtShippingStreet1")]
        CheckoutShippingStreet1,
        [DefaultValue("txtShippingCity")]
        CheckoutShippingCity,
        [DefaultValue("ddlShippingState")]
        CheckoutShippingRegion,
        [DefaultValue("txtShippingZip")]
        CheckoutShippingZipCode,
        [DefaultValue("btnReestimateShipping")]
        CheckoutReestimateShipping,
        [DefaultValue("checkoutshippingsubmit")]
        CheckoutShippingSubmit,
        [DefaultValue("txtBillingFirstName")]
        CheckoutBillingFirstName,
        [DefaultValue("txtBillingLastName")]
        CheckoutBillingLastName,
        [DefaultValue("txtBillingEmail")]
        CheckoutBillingEmail,
        [DefaultValue("txtBillingPhone")]
        CheckoutBillingPhone,
        [DefaultValue("txtBillingCompany")]
        CheckoutBillingCompany,
        [DefaultValue("ddlBillingCountry")]
        CheckoutBillingCountry,
        [DefaultValue("txtBillingStreet1")]
        CheckoutBillingStreet1,
        [DefaultValue("txtBillingCity")]
        CheckoutBillingCity,
        [DefaultValue("ddlBillingState")]
        CheckoutBillingRegion,
        [DefaultValue("txtBillingZip")]
        CheckoutBillingZipCode,
        [DefaultValue("checkoutbillingsubmit")]
        CheckoutBillingSubmit,
        #endregion
        #region Totals Widget
        [DefaultValue("cartTotalsWidgetSubtotal")]
        CartTotalsWidgetSubtotalId,
        [DefaultValue("cartTotalsWidgetShipping")]
        CartTotalsWidgetShippingId,
        [DefaultValue("cartTotalsWidgetHandling")]
        CartTotalsWidgetHandlingId,
        [DefaultValue("cartTotalsWidgetFees")]
        CartTotalsWidgetFeesId,
        [DefaultValue("cartTotalsWidgetDiscounts")]
        CartTotalsWidgetDiscountsId,
        [DefaultValue("cartTotalsWidgetTaxes")]
        CartTotalsWidgetTaxesId,
        [DefaultValue("cartTotalsWidgetTotal")]
        CartTotalsWidgetTotalId,
        #endregion
        #region Login Modal
        [DefaultValue("loginButton")]
        LoginButtonId,
        [DefaultValue("registerLink")]
        RegisterLinkId,
        #endregion
        #region Registration
        [DefaultValue("txtUserName")]
        RegistrationUsernameId,
        [DefaultValue("txtPassword")]
        RegistrationPasswordId,
        [DefaultValue("emptyUsernameIcon")]
        RegistrationEmptyUsernameIconId,
        [DefaultValue("invalidUsernameIcon")]
        RegistrationInvalidUsernameIconId,
        [DefaultValue("validUsernameIcon")]
        RegistrationValidUsernameIconId,
        [DefaultValue("emptyPasswordIcon")]
        RegistrationEmptyPasswordIconId,
        [DefaultValue("invalidPasswordIcon")]
        RegistrationInvalidPasswordIconId,
        [DefaultValue("validPasswordIcon")]
        RegistrationValidPasswordIconId,
        [DefaultValue("ckAgreed")]
        RegistrationTermsAgreedCheckboxId,
        [DefaultValue("registerButton")]
        RegistrationRegisterButtonId,
        [DefaultValue("registrationConfirmation")]
        RegistrationConfirmationId,
        #endregion
        #region Contact Widget
        [DefaultValue("customKey{0}")]
        ContactWidgetCustomKeyId,
        [DefaultValue("firstName{0}")]
        ContactWidgetFirstNameId,
        [DefaultValue("lastName{0}")]
        ContactWidgetLastNameId,
        [DefaultValue("email{0}")]
        ContactWidgetEmailId,
        [DefaultValue("lastName{0}")]
        ContactWidgetLastNameKeyId,
        [DefaultValue("emptyEmailIcon{0}")]
        ContactWidgetEmptyEmailIconId,
        [DefaultValue("invalidEmailIcon{0}")]
        ContactWidgetInvalidEmailIconId,
        [DefaultValue("validEmailIcon{0}")]
        ContactWidgetValidEmailIconId,
        [DefaultValue("phone{0}")]
        ContactWidgetPhoneId,
        [DefaultValue("phone2{0}")]
        ContactWidgetPhone2Id,
        [DefaultValue("phone3{0}")]
        ContactWidgetPhone3Id,
        [DefaultValue("company{0}")]
        ContactWidgetCompanyId,
        [DefaultValue("fax{0}")]
        ContactWidgetFaxId,
        [DefaultValue("country{0}")]
        ContactWidgetCountryId,
        [DefaultValue("street1{0}")]
        ContactWidgetStreet1Id,
        [DefaultValue("street2{0}")]
        ContactWidgetStreet2Id,
        [DefaultValue("street3{0}")]
        ContactWidgetStreet3Id,
        [DefaultValue("city{0}")]
        ContactWidgetCityId,
        [DefaultValue("state{0}")]
        ContactWidgetStateId,
        [DefaultValue("zip{0}")]
        ContactWidgetZipCodeId,
        [DefaultValue("addMoreShippingButton{0}")]
        ContactWidgetAddMoreShippingButtonId,
        #endregion
        #region User Dashboard
        [DefaultValue("userDashboardSideMenuMyDashboardBtn")]
        UserDashboardSideMenuMyDashboardBtn,
        [DefaultValue("userDashboardSideMenuAccountSettingsBtn")]
        UserDashboardSideMenuAccountSettingsBtn,
        [DefaultValue("userDashboardSideMenuMyProfileBtn")]
        UserDashboardSideMenuMyProfileBtn,
        [DefaultValue("userDashboardSideMenuAccountProfileBtn")]
        UserDashboardSideMenuAccountProfileBtn,
        [DefaultValue("userDashboardSideMenuAddressBookBtn")]
        UserDashboardSideMenuAddressBookBtn,
        [DefaultValue("userDashboardSideMenuWalletBtn")]
        UserDashboardSideMenuWalletBtn,
        [DefaultValue("userDashboardSideMenuGroupsBtn")]
        UserDashboardSideMenuGroupsBtn,
        [DefaultValue("userDashboardSideMenuOrdersBtn")]
        UserDashboardSideMenuOrdersBtn,
        [DefaultValue("userDashboardSideMenuReturnsBtn")]
        UserDashboardSideMenuReturnsBtn,
        [DefaultValue("userDashboardSideMenuInvoicesBtn")]
        UserDashboardSideMenuInvoicesBtn,
        [DefaultValue("userDashboardSideMenuQuotesBtn")]
        UserDashboardSideMenuQuotesBtn,
        [DefaultValue("userDashboardSideMenuDownloadsBtn")]
        UserDashboardSideMenuDownloadsBtn,
        [DefaultValue("userDashboardSideMenuWishListBtn")]
        UserDashboardSideMenuWishListBtn,
        [DefaultValue("userDashboardSideMenuFavoritesBtn")]
        UserDashboardSideMenuFavoritesBtn,
        [DefaultValue("userDashboardSideMenuInStockAlertsBtn")]
        UserDashboardSideMenuInStockAlertsBtn,
        [DefaultValue("userDashboardSideMenuShoppingListsBtn")]
        UserDashboardSideMenuShoppingListsBtn,
        [DefaultValue("WishListItem{0}")]
        UserDashboardWishListItem,
        #endregion
        [DefaultValue("Invalid")]
        Invalid,
    }
}
