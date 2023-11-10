import { useEffect, useState } from "react";
import { faBell, faTrashAlt } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Column } from "@material-table/core";
import { Link } from "react-router-dom";
import cvApi from "../../_api/cvApi";
import {
  AddToCartButton,
  AddToCartQuantitySelector
} from "../../Cart/controls";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { Table } from "../../_shared/common/Table";
import { convertSalesItemToBasicProductData } from "../../_shared/common/Formatters";
import { useTranslation } from "react-i18next";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { Row, Col, Button } from "react-bootstrap";
export const InStockAlerts = (): JSX.Element => {
  const [inStockAlertSalesItems, setInStockAlertSalesItems] = useState<
    Array<any>
  >([]);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    getInStockAlerts();
  }, []);

  function getInStockAlerts(): void {
    setRunning("Fetching notify me list...");
    cvApi.shopping
      .GetCurrentStaticCart({ TypeName: "Notify Me When In Stock" })
      .then((res: any) => {
        if (res.data && res.data.SalesItems) {
          setInStockAlertSalesItems(res.data.SalesItems);
          finishRunning();
        } else {
          finishRunning(true, "Notify me list call did not return items");
        }
      });
  }

  function clearInStockAlerts(): void {
    setRunning();
    cvApi.shopping
      .ClearCurrentStaticCart({ TypeName: "Notify Me When In Stock" })
      .then((_res: any) => {
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to clear in stock alerts");
      });
  }

  function deleteInStockAlertItem(productId: number): void {
    setRunning();
    cvApi.shopping
      .RemoveStaticCartItemByProductIDAndType({
        TypeName: "Notify Me When In Stock",
        ProductID: productId
      })
      .then((res: any) => {
        if (res.data.ActionSucceeded) {
          getInStockAlerts();
          finishRunning();
        } else {
          finishRunning(true, res.data.Messages[0]);
        }
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "failed to delete in stock alert item"
        );
      });
  }

  function setInStockAlertItemQuantity(
    productId: number,
    quantity: number
  ): void {
    const currentList = [...inStockAlertSalesItems];
    for (let i = 0; i < currentList.length; i++) {
      if (currentList[i].ProductID === productId) {
        currentList[i].Quantity = quantity;
        break;
      }
    }
    setInStockAlertSalesItems([...currentList]);
  }

  const ProductImage = (salesItem: any): JSX.Element => {
    return (
      <Link to={`/InStockAlertDetail/${salesItem.ProductSeoUrl}`}>
        {/* Not sure if this path is correct */}
        <img
          src={`/images/ecommerce/Product/Images/${salesItem.ProductSeoUrl}.jpg?maxheight=100&maxwidth=100&mode=pad&scale=both`}
          alt={salesItem.ProductName}
        />
      </Link>
    );
  };

  const ProductSeoUrl = (salesItem: any): JSX.Element => (
    <Link to={`/InStockAlertDetail/${salesItem.ProductSeoUrl}`}>
      {salesItem.ProductKey}
    </Link>
  );

  const ProductName = (salesItem: any): JSX.Element => (
    <Link to={`/InStockAlertDetail/${salesItem.ProductSeoUrl}`}>
      {salesItem.ProductName}
    </Link>
  );

  const ProductQuantity = (salesItem: any): JSX.Element => {
    return (
      <div className="d-flex gap-3">
        <AddToCartQuantitySelector
          id={salesItem.ProductID}
          initialValue={salesItem.Quantity}
          onChange={(quantity: number) => {
            setInStockAlertItemQuantity(salesItem.ProductID, quantity);
          }}
        />
        <AddToCartButton
          product={convertSalesItemToBasicProductData(salesItem)}
          quantity={salesItem.Quantity}
          classes="btn btn-dark"
          icon={true}
        />
      </div>
    );
  };

  const DeleteItemButton = (salesItem: any): JSX.Element => {
    return (
      <Button
        variant="link"
        className="text-danger"
        id="btnRemoveItem0FromInStockAlert"
        name="btnRemoveItem0FromInStockAlert"
        title="Remove from In Stock Alerts List"
        aria-label="Remove from In Stock Alerts List"
        onClick={() => {
          deleteInStockAlertItem(salesItem.ProductID);
        }}>
        <FontAwesomeIcon icon={faTrashAlt} className="fa-lg" />
      </Button>
    );
  };

  const columns: Array<Column<any>> = [
    {
      width: "fit-content",
      render: ProductImage
    },
    {
      title: "SKU",
      width: "fit-content",
      field: "ProductKey",
      render: ProductSeoUrl
    },
    {
      title: "Name",
      field: "ProductName",
      render: ProductName,
      cellStyle: {
        width: "100%"
      }
    },
    {
      title: "Price",
      width: "fit-content",
      field: "UnitCorePrice"
    },
    {
      title: "Subtotal",
      width: "fit-content",
      field: "UnitCorePrice"
    },
    {
      title: "Quantity",
      render: ProductQuantity,
      width: "fit-content"
    },
    {
      render: DeleteItemButton
    }
  ];

  return (
    <Row className="position-relative">
      {viewState.running ? <LoadingWidget overlay={true} /> : null}
      <Col xs={12}>
        <h1>
          <FontAwesomeIcon icon={faBell} className="me-2" />
          <span>
            {t("ui.storefront.userDashboard2.userDashboard.InStockAlerts")}
          </span>
        </h1>
      </Col>
      <Col xs={12}>
        <Row>
          <Col xs={12} className="d-flex justify-content-end">
            <Button
              variant="secondary"
              className="mb-3"
              id="btnClearInStockAlert"
              name="btnClearInStockAlert"
              title="Clear Notify Me List"
              aria-label="Clear Notify Me List"
              disabled={!inStockAlertSalesItems.length}
              onClick={clearInStockAlerts}>
              {t(
                "ui.storefront.userDashboard2.controls.notifyMeList.ClearNotifyMeList"
              )}
            </Button>
          </Col>
          <Col xs={12}>
            {inStockAlertSalesItems.length ? (
              <Table data={inStockAlertSalesItems} columns={columns} />
            ) : (
              <span className="h4" id="InStockAlertNoItemsText">
                {t(
                  "ui.storefront.userDashboard2.controls.notifyMeList.NoItemsHaveBeenAddedToTheNotifyMeWhenInStockList"
                )}
              </span>
            )}
          </Col>
        </Row>
      </Col>
    </Row>
  );
};
