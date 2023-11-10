import { useEffect, useState } from "react";
import { faStar, faTrashAlt } from "@fortawesome/free-regular-svg-icons";
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
import { Button, Row, Col } from "react-bootstrap";

export const Favorites = () => {
  const [favoritesListSalesItems, setFavoritesListSalesItems] = useState<any>(
    []
  );

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    getFavoritesList();
  }, []);

  function getFavoritesList(): void {
    setRunning("Fetching favoriteslist...");
    cvApi.shopping
      .GetCurrentStaticCart({ TypeName: "Favorites List" })
      .then((res: any) => {
        if (res.data && res.data.SalesItems) {
          setFavoritesListSalesItems(res.data.SalesItems);
          finishRunning();
        } else {
          finishRunning(true, "Favorites list call did not return items");
        }
      });
  }

  function clearFavoritesList(): void {
    setRunning();
    cvApi.shopping
      .ClearCurrentStaticCart({ TypeName: "Favorites List" })
      .then((_res: any) => {
        setFavoritesListSalesItems([]);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to clear favorites list");
      });
  }

  function deleteFavoritesListItem(productId: number): void {
    setRunning();
    cvApi.shopping
      .RemoveStaticCartItemByProductIDAndType({
        TypeName: "Favorites List",
        ProductID: productId
      })
      .then((res: any) => {
        if (res.data.ActionSucceeded) {
          getFavoritesList();
          finishRunning();
        } else {
          finishRunning(true, res.data.Messages[0]);
        }
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "Failed to delete favorites list item"
        );
      });
  }

  function setFavoritesListItemQuantity(
    productId: number,
    quantity: number
  ): void {
    const currentList = [...favoritesListSalesItems];
    for (let i = 0; i < currentList.length; i++) {
      if (currentList[i].ProductID === productId) {
        currentList[i].Quantity = quantity;
        break;
      }
    }
    setFavoritesListSalesItems([...currentList]);
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
            setFavoritesListItemQuantity(salesItem.ProductID, quantity);
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
        id="btnRemoveItem0FromFavoritesList"
        name="btnRemoveItem0FromFavoritesList"
        title="Remove from Favorites List"
        aria-label="Remove from Favorites List"
        onClick={() => {
          deleteFavoritesListItem(salesItem.ProductID);
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
          <FontAwesomeIcon icon={faStar} className="me-2" />
          <span>{t("ui.storefront.common.FavoritesList")}</span>
        </h1>
      </Col>
      <Col xs={12}>
        <Row>
          <Col xs={12} className="d-flex justify-content-end">
            <Button
              variant="secondary"
              className="mb-3"
              id="btnClearFavoritesList"
              name="btnClearFavoritesList"
              title="Clear Favorites List"
              aria-label="Clear Favorites List"
              disabled={!favoritesListSalesItems.length}
              onClick={clearFavoritesList}>
              {t(
                "ui.storefront.userDashboard2.controls.favorites.ClearFavorites"
              )}
            </Button>
          </Col>
          <Col xs={12}>
            {favoritesListSalesItems.length ? (
              <Table data={favoritesListSalesItems} columns={columns} />
            ) : (
              <span className="h4" id="FavoritesListNoItemsText">
                {t(
                  "ui.storefront.userDashboard2.controls.favorites.NoItemsHaveBeenAddedToTheFavorites"
                )}
              </span>
            )}
          </Col>
        </Row>
      </Col>
    </Row>
  );
};
