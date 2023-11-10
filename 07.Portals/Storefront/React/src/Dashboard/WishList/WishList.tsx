import { useEffect, useState } from "react";
import { faHeart, faTrashAlt } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Column } from "@material-table/core";
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
import { Col, Row, Button } from "react-bootstrap";
export const WishList = (): JSX.Element => {
  const [wishListSalesItems, setWishListSalesItems] = useState<any>([]);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    getWishList();
  }, []);

  function getWishList(): void {
    setRunning("Fetching wishlist...");
    cvApi.shopping
      .GetCurrentStaticCart({ TypeName: "Wish List" })
      .then((res) => {
        if (res && res.data.SalesItems) {
          setWishListSalesItems(res.data.SalesItems);
          finishRunning();
        } else {
          finishRunning(true, "Wish list call did not return items");
        }
      });
  }

  function clearWishList(): void {
    setRunning();
    setWishListSalesItems([]);
    cvApi.shopping
      .ClearCurrentStaticCart({ TypeName: "Wish List" })
      .then(() => {
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "");
      });
  }

  function deleteWishListItem(productId: number): void {
    setRunning();
    cvApi.shopping
      .RemoveStaticCartItemByProductIDAndType({
        TypeName: "Wish List",
        ProductID: productId
      })
      .then((res: any) => {
        if (res.data.ActionSucceeded) {
          getWishList();
          finishRunning();
        } else {
          finishRunning(true, res.data.Messages[0]);
        }
      })
      .catch((err: any) => {
        finishRunning(true, err.message);
      });
  }

  function setWishListItemQuantity(productId: number, quantity: number): void {
    const currentList = [...wishListSalesItems];
    for (let i = 0; i < currentList.length; i++) {
      if (currentList[i].ProductID === productId) {
        currentList[i].Quantity = quantity;
        break;
      }
    }
    setWishListSalesItems([...currentList]);
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
            setWishListItemQuantity(salesItem.ProductID, quantity);
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
        id="btnRemoveItem0FromWishList"
        name="btnRemoveItem0FromWishList"
        title="Remove from Wish List"
        aria-label="Remove from Wish List"
        onClick={() => {
          deleteWishListItem(salesItem.ProductID);
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
      title: t("ui.storefront.cart.SKU"),
      width: "fit-content",
      field: "ProductKey",
      render: ProductSeoUrl
    },
    {
      title: t("ui.storefront.common.Name"),
      field: "ProductName",
      render: ProductName,
      cellStyle: {
        width: "100%"
      }
    },
    {
      title: t("ui.storefront.common.Price"),
      width: "fit-content",
      field: "UnitCorePrice"
    },
    {
      title: t("ui.storefront.common.Subtotal"),
      width: "fit-content",
      field: "UnitCorePrice"
    },
    {
      title: t("ui.storefront.common.Quantity"),
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
          <FontAwesomeIcon icon={faHeart} className="me-2" />
          <span>{t("ui.storefront.common.WishList")}</span>
        </h1>
      </Col>
      <Col xs={12}>
        <Row>
          <Col xs={12} className="d-flex justify-content-end">
            <Button
              variant="secondary"
              className="mb-3"
              id="btnClearWishList"
              name="btnClearWishList"
              title="Clear Wish List"
              aria-label="Clear Wish List"
              disabled={!wishListSalesItems.length}
              onClick={clearWishList}>
              {t(
                "ui.storefront.userDashboard2.controls.wishList.ClearWishList"
              )}
            </Button>
          </Col>
          <Col xs={12}>
            {wishListSalesItems.length ? (
              <Table data={wishListSalesItems} columns={columns} />
            ) : (
              <span className="h4" id="WishListNoItemsText">
                {t(
                  "ui.storefront.userDashboard2.controls.wishList.NoItemsHaveBeenAddedToTheWishList"
                )}
              </span>
            )}
          </Col>
        </Row>
      </Col>
    </Row>
  );
};
