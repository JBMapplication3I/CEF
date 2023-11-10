import { useState } from "react";
import { currencyFormatter } from "../_shared/common/Formatters";
import { IPurchaseRateQuotesManagerWidgetProps } from "./_checkoutTypes";
import { DateTime } from "luxon";
import { useTranslation } from "react-i18next";
import { Row, Col, Table, Form, Button } from "react-bootstrap";

type TPurchaseRateQuotesManagerWidgetSortBy = "rate" | "estDel";

export const PurchaseRateQuotesManagerWidget = (props: IPurchaseRateQuotesManagerWidgetProps) => {
  const { quotes, onRateSelected } = props;

  const activeQuote = Array.isArray(quotes) ? quotes.find((q) => q.Active && q.Selected) : null;

  const [sortBy, setSortBy] = useState<TPurchaseRateQuotesManagerWidgetSortBy>("rate");
  const [rateSortAsc, setRateSortAsc] = useState(false);
  const [estDeliverySortAsc, setEstDeliverySortAsc] = useState(false);
  const [selectedShippingRate, setSelectedShippingRate] = useState(null);
  const [showShowMoreButton, setShowShowMoreButton] = useState<boolean>(true);
  const [quantityOfRatesShown, setQuantityOfRatesShown] = useState(
    activeQuote ? quotes.findIndex((q) => q.Active && q.Selected) + 1 : 6
  );

  const { t } = useTranslation();

  const showMore = () => {
    if (quantityOfRatesShown < quotes.length) {
      setQuantityOfRatesShown(quotes.length);
    }
  };

  const showLess = () => {
    setQuantityOfRatesShown(4);
  };

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
      <Col xs={12}>
        <Table size="sm" responsive striped hover className="col-sm-12 m-0">
          <colgroup>
            <col className="w-40" />
            <col className="w-10" />
            <col className="w-25" />
            <col className="w-25" />
          </colgroup>
          <thead className="border-medium border-2 border-left-0 border-right-0">
            <tr className="border-0">
              <th
                className={`border-0 p-2 sortable ${
                  sortBy === "rate" ? (rateSortAsc ? "asc" : "desc") : ""
                }`}
                colSpan={2}
                onClick={handleRateHeadingClicked}
                id="RateText"
                data-translate="Rate">
                Rate
              </th>
              <th id="TargetShipText" className="border-0">
                Target Ship
              </th>
              <th
                className={`border-0 p-2 sortable ${
                  sortBy === "estDel" ? (estDeliverySortAsc ? "asc" : "desc") : ""
                }`}
                onClick={handleEstDelHeadingClicked}
                id="EstDelText"
                data-translate="Est. Del.">
                Est. Del.
              </th>
            </tr>
          </thead>
          <tbody className="border-top-0">
            {Array.isArray(quotes)
              ? quotes
                  .slice(0, quantityOfRatesShown)
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
                  .map((q, index: number): JSX.Element => {
                    return (
                      <tr
                        key={q.ID}
                        className={
                          selectedShippingRate && selectedShippingRate.ID === q.ID
                            ? "table-success"
                            : ""
                        }>
                        <td className="p-2">
                          <Form.Check className="custom-control custom-radio">
                            <Form.Check.Input
                              className="custom-control-input"
                              type="radio"
                              checked={
                                selectedShippingRate ? selectedShippingRate.ID === q.ID : false
                              }
                              onChange={() => {
                                setSelectedShippingRate(q);
                                if (onRateSelected) {
                                  onRateSelected(q);
                                }
                              }}
                            />
                            <Form.Check.Label
                              className="custom-control-label mb-0 pointer"
                              onClick={() => {
                                setSelectedShippingRate(q);
                                if (onRateSelected) {
                                  onRateSelected(q);
                                }
                              }}>
                              {q.ShipCarrierMethodName}
                            </Form.Check.Label>
                          </Form.Check>
                        </td>
                        <td className="text-right p-2">
                          <Form.Label className="custom-control pl-0 mb-0 pointer">
                            {q.Rate > 0 ? (
                              <span>{currencyFormatter.format(q.Rate)}</span>
                            ) : (
                              <span className="text-success">{t("ui.storefront.common.Free")}</span>
                            )}
                          </Form.Label>
                        </td>
                        <td className="p-2">
                          <Form.Label className="custom-control pl-0 mb-0 pointer">
                            {/* @ts-ignore */}
                            {DateTime.fromISO(q.TargetShippingDate).toLocaleString(
                              DateTime.DATE_MED
                            )}
                          </Form.Label>
                        </td>
                        <td className="p-2">
                          <Form.Label className="custom-control pl-0 mb-0 pointer">
                            {/* @ts-ignore */}
                            {DateTime.fromISO(q.TargetShippingDate).toLocaleString(
                              DateTime.DATE_MED
                            )}
                          </Form.Label>
                        </td>
                      </tr>
                    );
                  })
              : null}
            {showShowMoreButton && (
              <tr>
                <td colSpan={4}>
                  <Button
                    variant="secondary"
                    className="w-100"
                    onClick={() => {
                      quantityOfRatesShown >= quotes.length ? showLess() : showMore();
                    }}>
                    {t(
                      `${
                        quantityOfRatesShown >= quotes.length
                          ? "ui.storefront.checkout.rateQuotes.ShowLess"
                          : "ui.storefront.checkout.rateQuotes.ShowMore"
                      }`
                    )}
                  </Button>
                </td>
              </tr>
            )}
          </tbody>
        </Table>
      </Col>
    </Row>
  );
};
