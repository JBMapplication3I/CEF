import { Row, Col } from "react-bootstrap";
import { CartModel, RateQuoteModel, CartTotals } from "../../../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import { currencyFormatter } from "../../../_shared/common/Formatters";
import { ServiceStrings } from "../../../_shared/ServiceStrings";

interface ISplitShippingTotalsProps {
  targetedCarts: CartModel[];
  masterCart: CartModel;
}

export const SplitShippingTotals = (props: ISplitShippingTotalsProps): JSX.Element => {
  const { targetedCarts, masterCart } = props;

  const { t } = useTranslation();

  function totalTargetedCartsShippingRaw(): number {
    if (!targetedCarts || !targetedCarts.length) {
      return -1;
    }
    let valid = true;
    let total = 0;
    targetedCarts.forEach((x) => {
      if (x.NothingToShip) {
        return true;
      } // Skip
      if (
        !x.RateQuotes ||
        !x.RateQuotes.length ||
        !x.RateQuotes.some((y: RateQuoteModel) => y.Active && y.Selected)
      ) {
        valid = false;
        return false;
      }
      const selectedRateQuote = x.RateQuotes.find((y: RateQuoteModel) => y.Active && y.Selected);
      total += selectedRateQuote.Rate;
      return true;
    });
    return valid ? total : -1;
  }

  function grandTotal(type: string = ServiceStrings.carts.types.cart): string {
    const shipping = totalTargetedCartsShippingRaw();
    if (shipping < 0) {
      return t("ui.storefront.cartService.targetCarts.InvalidValueSelectRateQuote.Message");
    }
    const totals: CartTotals = masterCart.Totals;
    if (!totals) {
      return currencyFormatter.format(0);
    }
    const newTotal =
      (totals.SubTotal || 0) +
      (shipping || 0) +
      (totals.Handling || 0) +
      (totals.Fees || 0) +
      (totals.Tax || 0) +
      Math.abs(totals.Discounts || 0) * -1;
    return currencyFormatter.format(newTotal);
  }

  return (
    <Row>
      <Col xs={12}>
        <p className="text-end fw-bold big">
          <span id="TotalShippingText">
            {t("ui.storefront.checkout.splitShipping.TotalShipping.Colon")}
          </span>
          &nbsp;
          <span id="TotalShippingAmountText">
            {currencyFormatter.format(
              targetedCarts.reduce((accu, curCart) => {
                if (curCart.NothingToShip) {
                  return accu;
                }
                if (!Array.isArray(curCart.RateQuotes) || !curCart.RateQuotes.length) {
                  console.log("Cart missing rate quotes, not included in total shipping");
                  return accu;
                }
                const selectedRateQuote = curCart.RateQuotes?.find(
                  (rq: RateQuoteModel) => rq.Active && rq.Selected
                );
                if (!selectedRateQuote) {
                  console.log("Cart missing selected rate quotes, not included in total shipping");
                  return accu;
                }
                return accu + selectedRateQuote.Rate;
              }, 0)
            )}
          </span>
          <br />
          <span id="TotalTaxesText">
            {t("ui.storefront.checkout.splitShipping.TotalTaxes.Colon")}
          </span>
          &nbsp;
          <span id="TotalTaxesAmountText">
            {currencyFormatter.format(masterCart.Totals ? masterCart.Totals.Tax : 0)}
          </span>
          <br />
          <span id="NewGrandTotalText">
            {t("ui.storefront.checkout.splitShipping.NewGrandTotal.Colon")}
          </span>
          &nbsp;<span id="NewGrandTotalAmountText">{grandTotal()}</span>
        </p>
      </Col>
    </Row>
  );
};
