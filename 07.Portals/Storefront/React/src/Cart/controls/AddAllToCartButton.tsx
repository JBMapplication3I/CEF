import { useState } from "react";
import { faChevronDown, faQuoteRight } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, ButtonGroup } from "react-bootstrap";
import { setCart, setQuoteCart } from "../../_redux/actions";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { AddAllToCartModal } from "../modals";
import cvApi from "../../_api/cvApi";
import cssClasses from "./AddToCartButton.module.scss";
import scssVariables from "../../_meta/css/exposedToJSVariables.module.scss";
import { useTranslation } from "react-i18next";

interface IAddAllToCartButtonProps {
  classes?: string;
  excludeQuoteCart?: boolean;
  label?: string;
  disabled?: boolean;
  useConfirmModal?: boolean;
  products: Array<any>;
}

export const AddAllToCartButton = (
  props: IAddAllToCartButtonProps
): JSX.Element => {
  const {
    classes,
    excludeQuoteCart,
    label,
    disabled,
    useConfirmModal,
    products
  } = props;

  const [showAddToQuoteCartButton, setShowAddToQuoteCartButton] =
    useState<boolean>(false);
  const [showConfirmationModal, setShowConfirmationModal] =
    useState<boolean>(false);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const onAddCartItems = (TypeName: string, Items: Array<any>): void => {
    if (!products) {
      console.log("Missing or invalid products. Add to cart failed.");
      return;
    }

    setRunning();
    cvApi.shopping
      .AddCartItems({
        Items: products,
        TypeName
      })
      .then((res: any) => {
        if (!res.data.ActionSucceeded) {
          finishRunning(true, res.data);
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

  return (
    <div className="position-relative w-100">
      <AddAllToCartModal
        show={showConfirmationModal}
        onConfirm={() => {}}
        onCancel={() => setShowConfirmationModal(false)}
        count={products ? products.length : 0}
      />
      <ButtonGroup className="w-100">
        <Button
          className={
            classes ??
            "btn-block btn-lg btn-success rounded-start text-white text-nowrap"
          }
          style={{
            borderRight: excludeQuoteCart
              ? "none"
              : `2px solid ${scssVariables.light}`
          }}
          disabled={disabled ?? viewState.running ?? false}
          onClick={() => onAddCartItems("Cart", products)}>
          {label ?? t("ui.storefront.cart.addToCart.plural")}
        </Button>
        {!excludeQuoteCart ? (
          <Button
            className={classes ?? "btn-success text-white rounded-end"}
            style={{ maxWidth: "fit-content" }}
            disabled={disabled ?? viewState.running ?? false}
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
            className="px-2"
            onClick={() => onAddCartItems("Quote Cart", products)}>
            <FontAwesomeIcon icon={faQuoteRight} />
            &nbsp;
            <span>{t("ui.storefront.quotes.cart.add")}</span>
          </Button>
        </div>
      ) : null}
    </div>
  );
};
