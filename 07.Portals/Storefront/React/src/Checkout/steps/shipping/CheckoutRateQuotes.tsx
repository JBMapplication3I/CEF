import { Row, Col, Card } from "react-bootstrap";
import { CartModel, RateQuoteModel, CartTotals } from "../../../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import { currencyFormatter } from "../../../_shared/common/Formatters";
import { ServiceStrings } from "../../../_shared/ServiceStrings";
import { IViewState } from "../../../_shared/customHooks/_customHooksTypes";
import { LoadingWidget } from "../../../_shared/common/LoadingWidget";
import { AddressBlock } from "../../../Dashboard/AddressBook/AddressBlock";
import { PurchaseRateQuotesManagerWidget } from "../../PurchaseRateQuotesManagerWidget";

interface ICheckoutRateQuotesProps {
  targetedCarts: CartModel[];
  shippingRatesEstimatorEnabled?: boolean;
  parentViewState: IViewState;
  setEstimatedShippingCost: Function;
  onRateQuoteSelected: Function;
}

export const CheckoutRateQuotes = (props: ICheckoutRateQuotesProps): JSX.Element => {
  const {
    targetedCarts,
    shippingRatesEstimatorEnabled,
    parentViewState,
    setEstimatedShippingCost,
    onRateQuoteSelected
  } = props;

  const { t } = useTranslation();

  return (
    <>
      {targetedCarts.map((cart) => {
        const { TypeName, NothingToShip, SalesItems } = cart;
        const rateQuoteSelected: boolean = cart?.RateQuotes?.some(
          (quote: RateQuoteModel) => quote.Selected
        );
        return (
          <Col xs={12} key={TypeName}>
            {/* TODO: add other condition to NothingToShip */}
            <Card
              className={`mb-3 ${
                (!parentViewState.hasError && rateQuoteSelected) || NothingToShip ? "is-valid" : ""
              }`}>
              {parentViewState.running && <LoadingWidget overlay={true} padIn={true} />}
              <Card.Header className="ps-3">
                {t(NothingToShip ? "Non-shippable products" : "Product(s) being shipped")}
              </Card.Header>
              <Card.Body className="p-3">
                {SalesItems.sort((itemOne, itemTwo) => {
                  if (itemOne.Name && itemTwo.Name) {
                    return itemOne.Name > itemTwo.Name ? 1 : -1;
                  }
                  return itemOne.ProductName > itemTwo.ProductName ? 1 : -1;
                }).map((sItem, index) => {
                  return (
                    <div className="d-block" key={sItem.ID} id={`ShipGroupProduct_${index}`}>
                      {sItem.Quantity +
                        (sItem.QuantityBackOrdered || 0) +
                        (sItem.QuantityPreSold || 0)}
                      x
                      {sItem.Name
                        ? sItem.Name + " (" + sItem.Sku + ")"
                        : sItem.ProductName + " (" + sItem.ProductKey + ")"}
                    </div>
                  );
                })}
              </Card.Body>
              {!NothingToShip ? (
                <>
                  <Card.Header>
                    {t("ui.storefront.checkout.views.shippingInformation.yourFullAddress")}
                  </Card.Header>
                  {cart.ShippingContact ? (
                    <Card.Body className="p-3 row">
                      <div className="col-6">
                        <AddressBlock
                          address={{
                            ...cart.ShippingContact.Address,
                            Name: cart.ShippingContact.FirstName
                          }}
                          hidePhone1={true}
                          hideFax1={true}
                          hideEmail1={true}
                        />
                      </div>
                      <div className="col-6">
                        <AddressBlock
                          address={{
                            ...cart.ShippingContact.Address,
                            Name: cart.ShippingContact.FirstName,
                            Phone1: cart.ShippingContact.Phone1,
                            Email1: cart.ShippingContact.Email1,
                            Fax1: cart.ShippingContact.Fax1
                          }}
                          hideName={true}
                          hideCompany={true}
                          hideStreet1={true}
                          hideStreet2={true}
                          hideCity={true}
                          hideCountryKey={true}
                          hideRegionName={true}
                          hidePostalCode={true}
                        />
                      </div>
                    </Card.Body>
                  ) : null}
                  <Card.Header>
                    {t("ui.storefront.cart.cartShippingEstimator.yourShippingRateQuotes")}
                  </Card.Header>
                  <Card.Body className="p-0">
                    {cart.ShippingContact && shippingRatesEstimatorEnabled ? (
                      <div className="w-100">
                        <PurchaseRateQuotesManagerWidget
                          quotes={cart.RateQuotes}
                          onRateSelected={(rateQuote) => {
                            setEstimatedShippingCost(rateQuote.Rate);
                            onRateQuoteSelected(cart.TypeName, rateQuote.ID);
                          }}
                        />
                      </div>
                    ) : null}
                  </Card.Body>
                </>
              ) : null}
            </Card>
          </Col>
        );
      })}
    </>
  );
};
