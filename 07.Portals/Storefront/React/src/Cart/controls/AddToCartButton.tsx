import { useState } from "react";
import {
  faCartPlus,
  faChevronDown,
  faQuoteRight
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, ButtonGroup } from "react-bootstrap";
import cvApi from "../../_api/cvApi";
import { setCart, setQuoteCart } from "../../_redux/actions";
import { useViewState } from "../../_shared/customHooks/useViewState";
import cssClasses from "./AddToCartButton.module.scss";
import scssVariables from "../../_meta/css/exposedToJSVariables.module.scss";
import { AddToCartModal } from "../modals";
import { useTranslation } from "react-i18next";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";

interface IAddToCartButtonProps {
  classes?: string;
  icon?: boolean;
  excludeQuoteCart?: boolean;
  label?: string;
  disabled?: boolean;
  useConfirmModal?: boolean;
  hideOutOfStock?: boolean;
  product: any;
  quantity: number;
}

export const AddToCartButton = (props: IAddToCartButtonProps): JSX.Element => {
  const {
    product,
    quantity,
    classes,
    icon,
    excludeQuoteCart,
    label,
    disabled,
    useConfirmModal,
    hideOutOfStock
  } = props;
  const productID = product.ProductID || product.ID;

  const [showAddToQuoteCartButton, setShowAddToQuoteCartButton] =
    useState<boolean>(false);
  const [showConfirmationModal, setShowConfirmationModal] =
    useState<boolean>(false);
  const { setRunning, finishRunning, viewState } = useViewState();
  const { t } = useTranslation();

  const onAddCartItem = (TypeName: string): void => {
    if (!productID) {
      console.log("Missing or invalid id. Add to cart failed.");
      return;
    }

    setRunning();
    cvApi.shopping
      .AddCartItem({
        ProductID: productID,
        Quantity: quantity,
        TypeName
      })
      .then((res: any) => {
        if (!res.data.ActionSucceeded) {
          return Promise.reject(res.data);
        }
        return cvApi.shopping.GetCurrentCart({ TypeName, Validate: false });
      })
      .then((res: any) => {
        if (TypeName === "Cart") {
          setCart!(res.data.Result);
        } else {
          setQuoteCart!(res.data.Result);
        }
        setShowAddToQuoteCartButton(false);
        if (useConfirmModal) {
          setShowConfirmationModal(true);
        }
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to add cart item");
      });
  };

  const showOutOfStockButton = (): boolean => {
    if (hideOutOfStock) {
      return false;
    }
    return (
      product.readInventory &&
      product.readInventory().IsOutOfStock &&
      !product.readInventory().AllowBackOrder
    );
  };

  return (
    <div className="position-relative">
      <AddToCartModal
        show={showConfirmationModal}
        onCancel={() => setShowConfirmationModal(false)}
        product={product}
      />
      {viewState.running ? <LoadingWidget overlay={true} size="2x" /> : null}
      <ButtonGroup className="w-100">
        {showOutOfStockButton() && (
          <Button
            variant="primary"
            size="lg"
            className="rounded w-100"
            id={`btnDetailsAddToCartProductOutOfStock${productID}`}
            name={`btnDetailsAddToCartProductOutOfStock${productID}`}
            disabled={true}>
            {t("ui.storefront.common.OutOfStock")}
          </Button>
        )}
        {!showOutOfStockButton() && (
          <Button
            variant="primary"
            size={label ? null : "lg"}
            className={
              classes ?? "btn-block rounded-start text-white text-nowrap"
            }
            style={{
              borderRight: excludeQuoteCart
                ? "none"
                : `2px solid ${scssVariables.light}`
            }}
            disabled={Boolean(disabled)}
            onClick={() => onAddCartItem("Cart")}>
            {icon ? (
              <FontAwesomeIcon icon={faCartPlus} />
            ) : label ? (
              label
            ) : (
              t("ui.storefront.cart.addToCart")
            )}
          </Button>
        )}
        {!excludeQuoteCart ? (
          <Button
            variant="primary"
            className={classes ?? "rounded-end"}
            style={{ maxWidth: "fit-content" }}
            disabled={Boolean(disabled)}
            onClick={() =>
              setShowAddToQuoteCartButton(!showAddToQuoteCartButton)
            }>
            <span className="sr-only"></span>
            <FontAwesomeIcon icon={faChevronDown} className="fa-sm" />
          </Button>
        ) : null}
      </ButtonGroup>
      {!excludeQuoteCart ? (
        <div
          className={`${showAddToQuoteCartButton ? "d-flex" : "d-none"} ${
            cssClasses.addToQuoteCartContainer
          }`}>
          <Button
            variant="outline-light"
            className="px-2 text-dark"
            onClick={() => onAddCartItem("Quote Cart")}>
            <FontAwesomeIcon icon={faQuoteRight} />
            &nbsp;
            <span>{t("ui.storefront.quotes.cart.add")}</span>
          </Button>
        </div>
      ) : null}
    </div>
  );
};
