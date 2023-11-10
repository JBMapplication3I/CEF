import { useEffect, useState } from "react";
import { currencyFormatter } from "../_shared/common/Formatters";
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt } from "@fortawesome/free-regular-svg-icons";
import { AddToCartButton } from "./controls/AddToCartButton";
import { AddToCartQuantitySelector } from "./controls";
import cvApi from "../_api/cvApi";
import {
  Container,
  Row,
  Col,
  Table,
  Button,
  Card,
  InputGroup,
  FormControl
} from "react-bootstrap";
import { connect } from "react-redux";
import { setCart, setQuoteCart } from "../_redux/actions";
import { useViewState } from "../_shared/customHooks/useViewState";
import { LoadingWidget } from "../_shared/common/LoadingWidget";
import { useTranslation } from "react-i18next";
import ImageWithFallback from "../_shared/common/ImageWithFallback";
import { AppliedCartItemDiscountModel, CartModel } from "../_api/cvApi._DtoClasses";
import {
  AppliedDiscountBaseModel,
  SalesItemBaseModel
} from "../_api/cvApi.shared";
import { SuggestProductCatalogWithProviderDto } from "../_api";
import { IReduxStore } from "../_redux/_reduxTypes";

interface ICartProps {
  cart?: CartModel; // redux
  quoteCart?: CartModel; // redux
  setCart?: Function; // redux
  setQuoteCart?: Function; // redux
  cartType: "Quote Cart" | "Cart";
  includeQuickOrder?: boolean;
}

interface IQuickCartItem {
  name: string;
  ID: number;
}

interface IMapStateToCartProps {
  cart: CartModel;
  quoteCart: CartModel;
}

const mapStateToProps = (state: IReduxStore): IMapStateToCartProps => {
  return {
    cart: state.cart,
    quoteCart: state.quoteCart
  };
};

export const Cart = connect(mapStateToProps)((props: ICartProps) => {
  const { cart, quoteCart, cartType } = props;

  const [suggestions, setSuggestions] = useState<Array<any>>([]);
  const [quickCartItem, setQuickCartItem] = useState<IQuickCartItem>({
    name: "",
    ID: -1
  });
  const [queuedIDs, setQueuedIDs] = useState([]);

  const { setRunning, finishRunning, viewState } = useViewState();
  const { t } = useTranslation();

  useEffect(() => {
    getCart();
  }, []);

  const onQuickCartSearchInputKeyPress = (
    e: React.KeyboardEvent<HTMLInputElement>
  ): void => {
    const value = (e.target as HTMLInputElement).value;
    if (!value || !value.length) {
      setSuggestions([]);
      return;
    }

    cvApi.providers
      .SuggestProductCatalogWithProvider({
        Query: value,
        Page: 1,
        PageSetSize: 8,
        PageSize: 8,
        Sort: 0
      } as SuggestProductCatalogWithProviderDto)
      .then((result) => {
        setSuggestions(result.data);
      })
      .catch((err: any) => {
        console.log(err);
      });
  };

  function onChange(e: any) {
    setQuickCartItem({
      name: e,
      ID: -1
    });
  }

  function getCart(): void {
    setRunning();
    cvApi.shopping
      .GetCurrentCart({
        TypeName: cartType,
        Validate: true
      })
      .then((res: any) => {
        const newCart = res?.data?.Result ?? [];
        if (cartType === "Cart") {
          setCart(newCart);
        }
        if (cartType === "Quote Cart") {
          setQuoteCart(newCart);
        }
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function getDiscountTotalPerItem(
    item: SalesItemBaseModel<AppliedCartItemDiscountModel>
  ): number {
    if (!item || !item.Discounts || !item.Discounts.length) {
      return;
    }
    let total = 0;
    item.Discounts.forEach(
      (discount: AppliedCartItemDiscountModel) =>
        (total += discount.DiscountTotal)
    );
    return total;
  }

  function quickCartHandler(quickCartItemName: any, quickCartItemId: number) {
    setQuickCartItem({
      name: quickCartItemName,
      ID: quickCartItemId
    });
    setSuggestions([]);
  }

  function cartHasItems() {
    const currentCart = getCurrentCart();
    return !!(currentCart && currentCart.SalesItems && currentCart.SalesItems.length > 0);
  }

  function getCurrentCart(): CartModel {
    if (cartType === "Cart") {
      return cart;
    }
    if (cartType === "Quote Cart") {
      return quoteCart;
    }
    // Cart is empty
    return null;
  }

  const removeCartItemByID = (
    e: React.MouseEvent<HTMLButtonElement>,
    ID: number
  ): void => {
    setRunning();
    cvApi.shopping
      .RemoveCartItemByID(ID)
      .then((res) => {
        if (res) {
          getCart();
        } else {
          finishRunning(true, "Failed to remove cart item");
        }
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
    e.preventDefault();
  };

  const onChangeSalesItemQuantity = (
    CartItemID: number,
    Quantity: number
  ): void => {
    setRunning();
    setQueuedIDs((queuedIDs) => [...queuedIDs, CartItemID]);
    cvApi.shopping
      .UpdateCartItemQuantity({
        CartItemID,
        Quantity,
        QuantityBackOrdered: 0,
        QuantityPreSold: 0
      })
      .then((res) => {
        if (res.data.ActionSucceeded) {
          getCart();
        } else {
          finishRunning(true, null, res.data.Messages);
        }
        setQueuedIDs((queuedIDs) =>
          queuedIDs.filter((ID) => ID !== CartItemID)
        );
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  };

  if (viewState.running && !getCurrentCart()) {
    return <LoadingWidget />;
  }

  if (
    (cartType === "Cart" && !cart) ||
    (cartType === "Quote Cart" && !quoteCart)
  ) {
    return (
      <section className="section-cart">
        {t("ui.storefront.checkout.cartEmpty")}
      </section>
    );
  }

  const cartToUse = cartType === "Cart" ? cart : quoteCart;

  return (
    <section className="section-cart">
      <Container className="my-3">
        <Row>
          <Col xs="12">
            <h1 className="mt-3" id="CartPageTitle">
              {cart && cart.TypeName === "Cart"
                ? t("ui.storefront.cart.microCart.MyCart")
                : cart && cart.TypeName === "Quote Cart"
                ? t("ui.storefront.cart.microCart.MyCart")
                : ""}
            </h1>
          </Col>
        </Row>
        <Row>
          <div className="cart-table">
            <Table bordered hover>
              <thead>
                <tr>
                  <th className="d-none w-auto d-lg-table-cell"></th>
                  <th className="sm-w-22 md-w-20 lg-w-16 xl-w-16 tk-w-20 fk-w-16">
                    {t("ui.storefront.cart.SKU")}
                  </th>
                  <th className="w-38 sm-w-38 md-w-26 lg-w-32 xl-w-40 tk-w-40 fk-w-48">
                    {t("ui.storefront.common.Name")}
                  </th>
                  <th className="d-none d-md-table-cell md-w-13 lg-w-10 xl-w-8 tk-w-8 fk-w-8 text-center">
                    {t("ui.storefront.common.Price")}
                  </th>
                  <th className="w-30 sm-w-30 md-w-30 lg-w-20 xl-w-16 tk-w-12 fk-w-8 text-center">
                    {t("ui.storefront.common.Quantity")}
                  </th>
                  <th className="sm-w-10 md-w-12 lg-w-10 xl-w-8 tk-w-8 fk-w-8 text-center">
                    {t("ui.storefront.common.Subtotal")}
                  </th>
                  <th className="w-1"></th>
                </tr>
              </thead>
              <tbody>
                {cartHasItems() ? (
                  cartToUse.SalesItems.map(
                    (item: SalesItemBaseModel<AppliedCartItemDiscountModel>, index: number) => {
                      const {
                        ProductName,
                        ProductKey,
                        ProductSeoUrl,
                        Discounts,
                        UnitCorePrice,
                        UnitSoldPrice,
                        TotalQuantity,
                        ExtendedPrice,
                        ID
                      } = item;
                      const DiscountTotal = getDiscountTotalPerItem(item);
                      return (
                        <tr
                          key={index}
                          className="border-top border-bottom border-medium">
                          <td
                            className="p-0 d-none d-lg-table-cell align-middle"
                            rowSpan={1}>
                            <div>
                              <span>
                                <a
                                  href={`/product?seoUrl=${ProductSeoUrl}`}
                                  id={`cardProductImageThumbnail${index}`}
                                  className="product-image d-flex justify-content-center">
                                  <ImageWithFallback
                                    className="img-fluid d-block"
                                    src={item.ProductPrimaryImage}
                                    alt={ProductName}
                                    width="100"
                                    height="100"
                                  />
                                </a>
                              </span>
                            </div>
                          </td>
                          <td className="align-middle">
                            <a
                              href={`/product?seoUrl=${ProductSeoUrl}`}
                              id={`cardProductSku${index}`}>
                              <label className="form-control-static font-weight-normal pointer">
                                {ProductKey}
                              </label>
                            </a>
                          </td>
                          <td className="align-middle">
                            <a
                              href={`/product?seoUrl=${ProductSeoUrl}`}
                              id={`cartProductName${index}`}>
                              <label className="form-control-static font-weight-normal pointer">
                                {ProductName}
                              </label>
                            </a>{" "}
                            {Discounts && Discounts.length && (
                              <Table
                                borderless
                                className="table-sm w-auto mb-0 bg-light rounded">
                                <tbody>
                                  {Discounts
                                    ? Discounts.map(
                                        (discount, discountIndex) => {
                                          return (
                                            <tr key={discount.ID}>
                                              <td className="text-right w-auto">
                                                <span className="small">
                                                  {discount.SlaveName}
                                                </span>
                                              </td>
                                              <td className="w-auto">
                                                <span className="text-success small">
                                                  {discount.DiscountValueType ===
                                                  1
                                                    ? currencyFormatter.format(
                                                        discount.DiscountValue
                                                      )
                                                    : discount.DiscountValue +
                                                      "%"}
                                                </span>
                                                &nbsp;
                                                <span className="text-success small">
                                                  {t(
                                                    "ui.storefront.common.Off"
                                                  )}
                                                </span>
                                              </td>
                                            </tr>
                                          );
                                        }
                                      )
                                    : null}
                                </tbody>
                              </Table>
                            )}
                          </td>
                          <td className="align-middle d-md-table-cell d-none text-right">
                            <div
                              className={`d-block form-control-static cart-price price text-right text-muted py-0 ${
                                (UnitSoldPrice &&
                                  UnitCorePrice !== UnitSoldPrice &&
                                  UnitSoldPrice > 0) ||
                                (DiscountTotal && DiscountTotal < 0)
                                  ? "strike-through small text-disabled"
                                  : ""
                              }`}
                              id={`cartProductPrice${index}`}>
                              {currencyFormatter.format(UnitCorePrice)}
                            </div>
                            {UnitSoldPrice &&
                            UnitCorePrice !== UnitSoldPrice &&
                            UnitSoldPrice > 0 ? (
                              <div
                                className={`d-block form-control-static cart-price price text-right text-danger py-0 ${
                                  DiscountTotal && DiscountTotal < 0
                                    ? "strike-through small text-disabled"
                                    : ""
                                }`}
                                id={`cartProductPrice${index}`}>
                                {currencyFormatter.format(UnitSoldPrice)}
                              </div>
                            ) : null}
                            {DiscountTotal && DiscountTotal < 0 ? (
                              <div
                                className="d-block form-control-static cart-price price text-right text-success py-0"
                                id={`cartProductPrice${index}`}>
                                {currencyFormatter.format(
                                  (UnitSoldPrice || UnitCorePrice || 0) +
                                    DiscountTotal / TotalQuantity
                                )}
                              </div>
                            ) : null}
                          </td>
                          <td className="align-middle">
                            <div>
                              <AddToCartQuantitySelector
                                id={ID}
                                initialValue={TotalQuantity}
                                onChange={(val: number) =>
                                  onChangeSalesItemQuantity(ID, val)
                                }
                              />
                            </div>
                          </td>
                          <td className="align-middle text-right">
                            <div className="form-control-static font-weight-normal text-right text-muted">
                              {viewState.running &&
                              queuedIDs.indexOf(ID) > -1 ? (
                                <LoadingWidget size="lg" innerClasses="p-0" />
                              ) : (
                                <>
                                  <div
                                    className={`d-block form-control-static cart-price price text-right text-muted py-0 ${
                                      (UnitSoldPrice &&
                                        UnitCorePrice !== UnitSoldPrice &&
                                        UnitSoldPrice > 0) ||
                                      (DiscountTotal && DiscountTotal < 0)
                                        ? "strike-through small text-disabled"
                                        : ""
                                    }`}
                                    id={`cartProductPrice${index}`}>
                                    {currencyFormatter.format(
                                      UnitCorePrice * TotalQuantity
                                    )}
                                  </div>
                                  {UnitSoldPrice &&
                                  UnitCorePrice !== UnitSoldPrice &&
                                  UnitSoldPrice > 0 ? (
                                    <div
                                      className={`d-block form-control-static cart-price price text-right text-danger py-0 ${
                                        DiscountTotal && DiscountTotal < 0
                                          ? "strike-through small text-disabled"
                                          : ""
                                      }`}
                                      id={`cartProductPrice${index}`}>
                                      {currencyFormatter.format(ExtendedPrice)}
                                    </div>
                                  ) : null}
                                  {DiscountTotal && DiscountTotal < 0 ? (
                                    <div
                                      className="d-block form-control-static cart-price price text-right text-success py-0"
                                      id={`cartProductPrice${index}`}>
                                      {currencyFormatter.format(
                                        ExtendedPrice + DiscountTotal
                                      )}
                                    </div>
                                  ) : null}
                                </>
                              )}
                            </div>
                          </td>
                          <td className="align-middle w-1">
                            <Button
                              className="btn-link text-danger"
                              variant=""
                              id={`btnRemoveItem${index}FromCart`}
                              name={`btnRemoveItem${index}FromCart`}
                              type="button"
                              title={t("ui.storefront.cart.removeItemFromCart")}
                              aria-label={t(
                                "ui.storefront.cart.removeItemFromCart"
                              )}
                              onClick={(e) => removeCartItemByID(e, ID)}>
                              <FontAwesomeIcon
                                icon={faTrashAlt}
                                className="fa-lg"
                              />
                            </Button>
                          </td>
                        </tr>
                      );
                    }
                  )
                ) : (
                  <tr className="border-top border-bottom border-medium">
                    <td colSpan={7}>
                      <label className="form-control-static font-weight-normal">
                        {t("ui.storefront.checkout.cartEmpty")}
                      </label>
                    </td>
                  </tr>
                )}
              </tbody>
            </Table>
          </div>
        </Row>
        <Row className="cart-collaterals">
          <Col xs="12" sm="auto">
            <div className="mb-3">
              <Card bg="light">
                <Card.Body>
                  <label className="control-label" htmlFor="txtQuickAddSearch">
                    {t("ui.storefront.cart.QuicklyAddItemsToYourShoppingCart")}
                  </label>
                  <InputGroup className="mb-2">
                    <FormControl
                      className="border-right-0 autocomplete"
                      type="text"
                      aria-label="autocomplete"
                      placeholder={t("ui.storefront.cart.searchProducts")}
                      onChange={(e) => onChange(e.target.value)}
                      onKeyDown={onQuickCartSearchInputKeyPress}
                      value={quickCartItem.name}
                      data-autocomplete='{"request": "inc/autocomplete-source.json"}'
                    />
                    {suggestions.length ? (
                      <div
                        className="position-absolute border bg-white"
                        style={{ top: "105%", zIndex: 1000 }}>
                        <ul className="list-unstyled mb-0">
                          {suggestions.map((suggestion) => (
                            <li key={suggestion.Name} className="p-2">
                              <button
                                type="button"
                                className="btn btn-link"
                                onClick={() =>
                                  quickCartHandler(
                                    suggestion.Name,
                                    suggestion.ID
                                  )
                                }>
                                {suggestion.Name}
                              </button>
                            </li>
                          ))}
                        </ul>
                      </div>
                    ) : null}
                    <InputGroup.Text className="bg-white border-left-0">
                      <FontAwesomeIcon
                        icon={faSearch}
                        className="fa-search"
                        aria-label="search"
                      />
                    </InputGroup.Text>
                  </InputGroup>
                  <div className="mb-3 d-flex justify-content-end">
                    <AddToCartButton
                      product={quickCartItem}
                      quantity={1}
                      disabled={quickCartItem.ID == -1 ? true : false}
                      excludeQuoteCart={true}
                      label={"Add"}
                    />
                  </div>
                </Card.Body>
              </Card>
            </div>
          </Col>
          <Col />
          <Col xs="12" sm="auto" className="totals">
            <h3>{t("ui.storefront.cart.shoppingCartTotal")}</h3>
            <ul className="list-unstyled pl-0 mb-0">
              <li className="p-2 border-top border-medium d-flex justify-content-between bg-light">
                <span className="dt">{t("ui.storefront.common.Subtotal")}</span>
                <span className="dd">
                  {isNaN(getCurrentCart()?.Totals?.SubTotal)
                    ? currencyFormatter.format(0.0)
                    : currencyFormatter.format(
                        getCurrentCart()?.Totals?.SubTotal
                      )}
                </span>
              </li>
              <li className="p-2 border-top border-medium total d-flex justify-content-between bg-light">
                <span className="dt">
                  {t("ui.storefront.common.Discount.Plural")}
                </span>
                <span className="dd">
                  {isNaN(getCurrentCart()?.Totals?.Discounts)
                    ? currencyFormatter.format(0.0)
                    : currencyFormatter.format(
                        getCurrentCart()?.Totals?.Discounts
                      )}
                </span>
              </li>
              <li className="p-2 border-top border-medium total d-flex justify-content-between">
                <span className="dt">{t("ui.storefront.cart.grandTotal")}</span>
                <span className="dd">
                  {viewState.running ? (
                    <LoadingWidget size="lg" innerClasses="p-0" />
                  ) : isNaN(getCurrentCart()?.Totals?.Total) ? (
                    currencyFormatter.format(0.0)
                  ) : (
                    currencyFormatter.format(getCurrentCart()?.Totals?.Total)
                  )}
                </span>
              </li>
              <li className="py-2 border-top border-medium">
                <a
                  href={!cartHasItems() ? "#" : cartType === "Cart" ? "/Checkout" : "/Submit-Quote"}
                  className={`btn btn-success w-100 text-white${
                    !cartHasItems() ? " disabled" : ""
                  }`}>
                  {t(
                    cartType === "Cart"
                      ? "ui.storefront.cart.proceedToCheckout"
                      : "ui.storefront.common.SubmitQuote"
                  )}
                </a>
              </li>
            </ul>
          </Col>
        </Row>
      </Container>
    </section>
  );
});
