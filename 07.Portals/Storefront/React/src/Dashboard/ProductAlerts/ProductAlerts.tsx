import { useEffect, useState } from "react";
import { faHeart, faTrashAlt } from "@fortawesome/free-regular-svg-icons";
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
import { Row, Col, Button } from "react-bootstrap";
export const ProductAlerts = (): JSX.Element => {
  const [productAlertSalesItems, setProductAlertSalesItems] = useState<any>([]);

  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    getProductAlert();
  }, []);

  function getProductAlert(): void {
    setRunning("Fetching product alerts...");
    cvApi.shopping
      .GetCurrentStaticCart({ TypeName: "Product Alert" })
      .then((res) => {
        if (res.data && res.data.SalesItems) {
          setProductAlertSalesItems(res.data.SalesItems);
          finishRunning();
        } else {
          finishRunning(true, "Product Alert call did not return items");
        }
      });
  }

  function clearProductAlert(): void {
    setRunning();
    setProductAlertSalesItems([]);
    cvApi.shopping
      .ClearCurrentStaticCart({ TypeName: "Product Alert" })
      .then(() => {
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "");
      });
  }

  function deleteProductAlertItem(productId: number): void {
    setRunning();
    cvApi.shopping
      .RemoveStaticCartItemByProductIDAndType({
        TypeName: "Product Alert",
        ProductID: productId
      })
      .then((res: any) => {
        if (res.data.ActionSucceeded) {
          getProductAlert();
          finishRunning();
        } else {
          finishRunning(true, res.data.Messages[0]);
        }
      })
      .catch((err: any) => {
        finishRunning(true, err.message);
      });
  }

  function setProductAlertItemQuantity(
    productId: number,
    quantity: number
  ): void {
    const currentList = [...productAlertSalesItems];
    for (let i = 0; i < currentList.length; i++) {
      if (currentList[i].ProductID === productId) {
        currentList[i].Quantity = quantity;
        break;
      }
    }
    setProductAlertSalesItems([...currentList]);
  }

  const ProductImage = (salesItem: any): JSX.Element => {
    return (
      <a href={`/product?seoUrl=${salesItem.ProductSeoUrl}`}>
        {/* Not sure if this path is correct */}
        <img
          src={`/images/ecommerce/Product/Images/${salesItem.ProductSeoUrl}.jpg?maxheight=100&maxwidth=100&mode=pad&scale=both`}
          alt={salesItem.ProductName}
        />
      </a>
    );
  };

  const ProductSeoUrl = (salesItem: any): JSX.Element => (
    <a href={`/product?seoUrl=${salesItem.ProductSeoUrl}`}>
      {salesItem.ProductKey}
    </a>
  );

  const ProductName = (salesItem: any): JSX.Element => (
    <a href={`/product?seoUrl=${salesItem.ProductSeoUrl}`}>
      {salesItem.ProductName}
    </a>
  );

  const ProductQuantity = (salesItem: any): JSX.Element => {
    return (
      <div className="d-flex gap-3">
        <AddToCartQuantitySelector
          id={salesItem.ProductID}
          initialValue={salesItem.Quantity}
          onChange={(quantity: number) => {
            setProductAlertItemQuantity(salesItem.ProductID, quantity);
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
        id="btnRemoveItem0FromProductAlert"
        name="btnRemoveItem0FromProductAlert"
        title="Remove from Product Alert"
        aria-label="Remove from Product Alert"
        onClick={() => {
          deleteProductAlertItem(salesItem.ProductID);
        }}>
        <FontAwesomeIcon icon={faTrashAlt} className="fa-lg" />
      </Button>
    );
  };

  const columns: Array<Column<any>> = [
    {
      render: ProductImage,
      width: "fit-content"
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
    <Row>
      <Col xs={12}>
        <h1>
          <FontAwesomeIcon icon={faHeart} className="me-2" />
          <span>Product Alert</span>
        </h1>
      </Col>
      <Col xs={12}>
        <Row>
          <Col xs={12} className="d-flex justify-content-end">
            <Button
              variant="secondary"
              className="mb-3"
              id="btnClearProductAlert"
              name="btnClearProductAlert"
              title="Clear Product Alert"
              aria-label="Clear Product Alert"
              disabled={!productAlertSalesItems.length}
              onClick={clearProductAlert}>
              Clear Product Alert
            </Button>
          </Col>
          <Col xs={12}>
            {productAlertSalesItems.length ? (
              <Table data={productAlertSalesItems} columns={columns} />
            ) : (
              <span
                className="h4"
                id="ProductAlertNoItemsText"
                data-translate="ui.storefront.userDashboard2.controls.favorites.NoItemsHaveBeenAddedToTheProductAlert">
                No items have been added to the Product Alert.
              </span>
            )}
          </Col>
        </Row>
      </Col>
    </Row>
  );
};
