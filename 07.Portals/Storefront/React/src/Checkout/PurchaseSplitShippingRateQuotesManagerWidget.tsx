import { useState } from "react";
import { currencyFormatter } from "../_shared/common/Formatters";
import { IPurchaseSplitShippingRateQuotesManagerWidgetProps } from "./_checkoutTypes";
import { DateTime } from "luxon";
import { useTranslation } from "react-i18next";
import { CEFConfig } from "../_redux/_reduxTypes";
import { connect } from "react-redux";
import { CartModel, RateQuoteModel } from "../_api/cvApi._DtoClasses";
import { Row, Col, Card, Table } from "react-bootstrap";

type TPurchaseSplitShippingRateQuotesManagerWidgetSortBy = "rate" | "estDel";

interface IMapStateToRateQuotesManagerWidget {
  cefConfig: CEFConfig; // redux
  cart: CartModel; // redux
}

const mapStateToProps = (state: IMapStateToRateQuotesManagerWidget) => {
  return {
    cefConfig: state.cefConfig,
    cart: state.cart
  };
};

export const PurchaseSplitShippingRateQuotesManagerWidget = connect(mapStateToProps)(
  (props: IPurchaseSplitShippingRateQuotesManagerWidgetProps) => {
    const { cefConfig, quotes, onRateSelected } = props;

    const [sortBy, setSortBy] =
      useState<TPurchaseSplitShippingRateQuotesManagerWidgetSortBy>("rate");
    const [rateSortAsc, setRateSortAsc] = useState(false);
    const [estDeliverySortAsc, setEstDeliverySortAsc] = useState(false);
    const [selectedShippingRate, setSelectedShippingRate] = useState(null);

    const { t } = useTranslation();

    const handleRateHeadingClicked = (): void => {
      if (sortBy !== "rate") {
        setSortBy("rate");
        setRateSortAsc(true);
      } else {
        setRateSortAsc(!rateSortAsc);
      }
    };

    const handleEstDelHeadingClicked = (): void => {
      if (sortBy !== "estDel") {
        setSortBy("estDel");
        setEstDeliverySortAsc(true);
      } else {
        setEstDeliverySortAsc(!estDeliverySortAsc);
      }
    };

    return (
      <Row>
        <div id="lbResolvedToTargetsMessage">
          <h4>
            {t(
              cefConfig.featureSet?.shipping?.rates?.estimator?.enabled
                ? "ui.storefront.checkout.splitShipping.EachItemHasBeenResolvedToAShipment.Message"
                : "ui.storefront.checkout.splitShipping.EachItemHasBeenResolvedToAShipmentNoEstimator.Message"
            )}
          </h4>
        </div>
        <Col xs={12}>
          <Card className="mb-3">
            <Card.Header className="pl-3">Product(s) being shipped</Card.Header>
            <Card.Body className="p-3"></Card.Body>
          </Card>

          {selectedShippingRate ? (
            <div className="d-flex flex-column align-items-center text-success">
              <h6>{selectedShippingRate.ShipCarrierMethodName}</h6>
              <h5>{currencyFormatter.format(selectedShippingRate.Rate)}</h5>
            </div>
          ) : (
            <h5 className="text-danger text-center" id="PleaseSelectARateText">
              {t("ui.storefront.cart.cartShippingEstimator.PleaseSelectARate")}
            </h5>
          )}
        </Col>
        <Col xs={12} sm={8}>
          <Table striped hover className="table-condensed col-sm-12">
            <colgroup>
              <col className="w-40" />
              <col className="w-10" />
              <col className="w-25" />
              <col className="w-25" />
            </colgroup>
            <thead>
              <tr>
                <th
                  className={`sortable ${sortBy === "rate" ? (rateSortAsc ? "asc" : "desc") : ""}`}
                  colSpan={2}
                  onClick={handleRateHeadingClicked}
                  id="RateText"
                  data-translate="Rate">
                  Rate
                </th>
                <th id="TargetShipText">Target Ship</th>
                <th
                  className={`sortable ${
                    sortBy === "estDel" ? (estDeliverySortAsc ? "asc" : "desc") : ""
                  }`}
                  onClick={handleEstDelHeadingClicked}
                  id="EstDelText"
                  data-translate="Est. Del.">
                  Est. Del.
                </th>
              </tr>
            </thead>
            <tbody>
              {quotes
                .sort((a, b) => {
                  const aEstDeliveryDate = new Date(a.EstimatedDeliveryDate);
                  const bEstDeliveryDate = new Date(b.EstimatedDeliveryDate);
                  let returnValue;
                  switch (sortBy) {
                    case "rate":
                      returnValue = rateSortAsc ? a.Rate - b.Rate : b.Rate - a.Rate;
                      break;
                    case "estDel":
                      returnValue = estDeliverySortAsc
                        ? aEstDeliveryDate.getTime() - bEstDeliveryDate.getTime()
                        : bEstDeliveryDate.getTime() - aEstDeliveryDate.getTime();
                      break;
                  }
                  return returnValue;
                })
                .map((q: RateQuoteModel, index: number): JSX.Element => {
                  return (
                    <tr key={q.ID}>
                      <td>
                        <div className="custom-control custom-radio">
                          <input
                            className="custom-control-input"
                            checked={
                              selectedShippingRate ? selectedShippingRate.ID === q.ID : false
                            }
                            onChange={() => {
                              /* must be here to avoid error, label handles click because this input is hidden */
                            }}
                            id={`btnSelectShippingRate_${index}`}
                            type="radio"
                          />
                          <label
                            className="custom-control-label mb-0 pointer"
                            onClick={() => {
                              setSelectedShippingRate(q);
                              if (onRateSelected) {
                                onRateSelected(q);
                              }
                            }}
                            htmlFor={`btnSelectShippingRate_${index}`}>
                            {q.ShipCarrierMethodName}
                          </label>
                        </div>
                      </td>
                      <td className="text-right">
                        <label className="custom-control pl-0 mb-0 pointer">
                          {q.Rate > 0 ? (
                            <span>{currencyFormatter.format(q.Rate)}</span>
                          ) : (
                            <span className="text-success">{t("ui.storefront.common.Free")}</span>
                          )}
                        </label>
                      </td>
                      <td>
                        <label className="custom-control pl-0 mb-0 pointer">
                          {q.TargetShippingDate.toLocaleString(DateTime.DATE_MED as string)}
                        </label>
                      </td>
                      <td>
                        <label className="custom-control pl-0 mb-0 pointer">
                          {q.TargetShippingDate.toLocaleString(DateTime.DATE_MED as string)}
                        </label>
                      </td>
                    </tr>
                  );
                })}
            </tbody>
          </Table>
        </Col>
      </Row>
    );
  }
);
