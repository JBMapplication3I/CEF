import { Fragment, useState } from "react";
import { faBalanceScale, faTimes } from "@fortawesome/free-solid-svg-icons";
import { CatalogFilterHeaderButton } from "./CatalogFilterHeaderButton";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, Col, Card, Accordion, ListGroup } from "react-bootstrap";
import cvApi from "../../../_api/cvApi";
import { ConfirmationModal } from "../../../_shared/modals/ConfirmationModal";
import { useViewState } from "../../../_shared/customHooks/useViewState";
import { LoadingWidget } from "../../../_shared/common/LoadingWidget";
import { connect } from "react-redux";
import { ICompareFilterProps } from "./_CatalogFilterComponentsTypes";
import { refreshCompareList } from "../../../_redux/actions";
import { useTranslation } from "react-i18next";
import { IReduxStore } from "../../../_redux/_reduxTypes";

interface IMapStateToCompareFilterProps {
  compareCartItems: Array<any>;
}

const mapStateToProps = (state: IReduxStore): IMapStateToCompareFilterProps => {
  return {
    compareCartItems: state.staticCarts.compareList?.SalesItems ?? []
  };
};

export const CompareFilter = connect(mapStateToProps)((props: ICompareFilterProps): JSX.Element => {
  const { expandedFilterName, setExpandedFilterName, compareCartItems, removeItemFromCompareCart } =
    props;

  const [showClearCompareCartModal, setShowClearCompareCartModal] = useState(false);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  function removeAllItemsFromCompareCart(): void {
    setRunning();
    cvApi.shopping
      .ClearCurrentCompareCart()
      .then((res: any) => {
        refreshCompareList();
        setShowClearCompareCartModal(false);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to remove all items from compare cart");
      });
  }

  return (
    <Fragment>
      <ConfirmationModal
        title="Clear Compare Cart"
        show={showClearCompareCartModal}
        onConfirm={removeAllItemsFromCompareCart}
        onCancel={() => setShowClearCompareCartModal(false)}>
        <span className="d-block pb-1">
          {t("ui.storefront.cart.AreYouSureYouWantToClearTheCart")}
        </span>
      </ConfirmationModal>
      <Col xs={12} sm={6} md={12}>
        <Card className="applied-filters mb-1">
          <Card.Header className="p-2 bg-light text-body">
            <CatalogFilterHeaderButton
              expandedFilterName={expandedFilterName}
              setExpandedFilterName={setExpandedFilterName}
              title={"Compare"}
              icon={faBalanceScale}
            />
          </Card.Header>
          <Accordion.Collapse
            eventKey="compare-collapse"
            className={expandedFilterName === "Compare" ? "show" : ""}>
            {viewState.running ? (
              <LoadingWidget />
            ) : (
              <Card.Body
                className="p-3 bg-light"
                style={{ maxHeight: "50vh", overflowY: "auto" }}>
                <ListGroup
                  as="ul"
                  className="collapse-filter list-unstyled mb-0">
                  {compareCartItems && compareCartItems.length
                    ? compareCartItems.map((salesItem: any): JSX.Element => {
                        const { ProductID, ProductName, ProductSeoUrl } = salesItem;
                        return (
                          <Accordion.Item
                            key={ProductID}
                            eventKey="compare-collapse"
                            className="d-flex align-items-start bg-light border-0">
                            <Button
                              variant="light"
                              className=" m-0 p-0 mr-1"
                              onClick={() =>
                                removeItemFromCompareCart(ProductID)
                              }>
                              <FontAwesomeIcon
                                icon={faTimes}
                                className="text-danger small"
                              />
                            </Button>
                            <a
                              href={`/product?seoUrl=${ProductSeoUrl}`}
                              className="product-name">
                              <small>{ProductName}</small>
                            </a>
                          </Accordion.Item>
                        );
                      })
                    : null}
                </ListGroup>
                <ListGroup
                  as="ul"
                  className="collapse-filter list-unstyled mb-0 bg-light">
                  <Accordion.Item
                    eventKey="compare-collapse"
                    className="filter-item mt-3 d-flex justify-content-end border-0 bg-light">
                    <Button
                      variant="primary"
                      size="sm"
                      className="ml-1"
                      onClick={() => setShowClearCompareCartModal(true)}>
                      {t("ui.storefront.common.Clear")}
                    </Button>
                    <a
                      href="/catalog/compare"
                      className="btn btn-secondary btn-sm">
                      {t("ui.storefront.common.Compare")}
                    </a>
                  </Accordion.Item>
                </ListGroup>
              </Card.Body>
            )}
          </Accordion.Collapse>
        </Card>
      </Col>
    </Fragment>
  );
});
