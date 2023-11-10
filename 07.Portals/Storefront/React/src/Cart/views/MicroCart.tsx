import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faQuoteRight, faShoppingCart } from "@fortawesome/free-solid-svg-icons";
import { connect } from "react-redux";
import { currencyFormatter } from "../../_shared/common/Formatters";
import { CEFConfig, IReduxStore } from "../../_redux/_reduxTypes";
import { CartModel } from "../../_api/cvApi._DtoClasses";

interface IMicroCartProps {
  cart?: CartModel; // redux
  quoteCart?: any; // redux
  cefConfig?: CEFConfig; //redux
  type: "Quote" | "Cart";
}

interface IMapStateToMicroCartProps {
  cart: CartModel;
  quoteCart: any;
  cefConfig: CEFConfig;
}

const mapStateToProps = (state: IReduxStore): IMapStateToMicroCartProps => {
  return {
    cart: state.cart,
    quoteCart: state.quoteCart,
    cefConfig: state.cefConfig
  };
};

export const MicroCart = connect(mapStateToProps)((props: IMicroCartProps): JSX.Element => {
  const { cart, quoteCart, type, cefConfig } = props;

  const isQuoteCart = type === "Quote";

  if (
    (cefConfig && cefConfig.featureSet && !cefConfig.featureSet.carts.enabled) ||
    (!isQuoteCart && !cart)
  ) {
    return null;
  }

  if (isQuoteCart) {
    if (cefConfig?.featureSet?.salesQuotes?.enabled) {
      return (
        <a className="cef-micro-cart nav-link text-primary" href="/quote-cart">
          <FontAwesomeIcon icon={faQuoteRight} className="fa-lg" />
          {
            <span>{`(${quoteCart && quoteCart.ItemQuantity ? quoteCart.ItemQuantity : 0}) ${
              quoteCart && quoteCart.Totals
                ? currencyFormatter.format(quoteCart.Totals.Total)
                : currencyFormatter.format(0.0)
            }`}</span>
          }
        </a>
      );
    }
    return null;
  }

  return (
    <a className="cef-micro-cart nav-link text-primary" href="/cart">
      <FontAwesomeIcon icon={faShoppingCart} className="fa-lg" />
      {
        <span>{`(${cart && cart.ItemQuantity ? cart.ItemQuantity : 0}) ${
          cart && cart.Totals
            ? currencyFormatter.format(cart.Totals.Total)
            : currencyFormatter.format(0.0)
        }`}</span>
      }
    </a>
  );
});
