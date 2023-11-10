import { Fragment, MouseEventHandler, useEffect, useState } from "react";
import { faTrashAlt } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Column } from "@material-table/core";
import cvApi from "../../_api/cvApi";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { useHistory, useParams } from "react-router";
import { Link } from "react-router-dom";
import { Table } from "../../_shared/common/Table";
import { convertSalesItemToBasicProductData } from "../../_shared/common/Formatters";
import { useTranslation } from "react-i18next";
import { Row, Col, Button } from "react-bootstrap";
import { auto } from "@popperjs/core";
import {
  AddToCartButton,
  AddToCartQuantitySelector
} from "../../Cart/controls";
import ImageWithFallback from "../../_shared/common/ImageWithFallback";
import {
  CEFActionResponse,
  IHttpPromiseCallbackArg
} from "../../_api/cvApi.shared";

export const ShoppingListsDetail = (): JSX.Element => {
  const [cartName, setCartName] = useState<string>("");
  const [shoppingList, setShoppingListItems] = useState<any[]>([]);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  // @ts-ignore
  const { typeName } = useParams();

  useEffect(() => {
    getShoppingList();
    setCartName(typeName);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cartName]);
  const history = useHistory();

  function getShoppingList(): void {
    setRunning("Fetching shopping list...");
    cvApi.shopping.GetCurrentStaticCart({ TypeName: typeName }).then((r) => {
      if (r && r.data.SalesItems) {
        setShoppingListItems(r.data.SalesItems);
        finishRunning();
      } else {
        finishRunning(true, "Shopping list call did not return items");
      }
    });
  }

  function clearShoppingList(): MouseEventHandler<HTMLButtonElement> {
    setRunning();
    setShoppingListItems([]);
    cvApi.shopping
      .ClearCurrentStaticCart({ TypeName: typeName })
      .then(() => finishRunning())
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || `Failed to clear the shopping list, ${typeName}`
        );
      });
    return;
  }

  function deleteShoppingList(): MouseEventHandler<HTMLButtonElement> {
    setRunning();
    setShoppingListItems([]);
    cvApi.shopping
      .DeleteCartTypeForCurrentUser({ TypeName: typeName })
      .then(() => {
        finishRunning();
        history.push("/dashboard/shopping-lists");
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || `Failed to delete the shopping list, ${typeName}`
        );
      });
    return;
  }

  function DeleteShoppingListButton(rowData: any): JSX.Element {
    function removeShoppingListItem(productID: number): void {
      setRunning();
      cvApi.shopping
        .RemoveStaticCartItemByProductIDAndType({
          TypeName: typeName,
          ProductID: productID
        })
        .then((res: IHttpPromiseCallbackArg<CEFActionResponse>) => {
          if (res?.data?.ActionSucceeded) {
            getShoppingList();
            finishRunning();
          } else {
            finishRunning(true, res?.data?.Messages[0]);
          }
        })
        .catch((err: any) => {
          finishRunning(true, err.message);
        });
    }
    return (
      <Button
        variant="link"
        className="text-danger"
        id="btnRemoveItem0FromShoppingList"
        name="btnRemoveItem0FromShoppingList"
        title="Remove from Shopping List"
        aria-label="Remove from Shopping List"
        onClick={() => {
          removeShoppingListItem(rowData.ProductID);
        }}>
        <FontAwesomeIcon icon={faTrashAlt} className="fa-lg" />
      </Button>
    );
  }

  function setShoppingListItemQuantity(
    productId: number,
    quantity: number
  ): void {
    const currentList = [...shoppingList];
    for (let i = 0; i < currentList.length; i++) {
      if (currentList[i].ProductID === productId) {
        currentList[i].Quantity = quantity;
        break;
      }
    }
    setShoppingListItems([...currentList]);
  }

  function QuantitySelection(rowData: any): JSX.Element {
    return (
      <div className="d-flex gap-3 my-auto">
        <AddToCartQuantitySelector
          id={rowData.ProductID}
          initialValue={rowData.Quantity}
          onChange={(quantity: number) => {
            setShoppingListItemQuantity(rowData.ProductID, quantity);
          }}
        />
        <AddToCartButton
          product={convertSalesItemToBasicProductData(rowData)}
          quantity={rowData.Quantity}
          classes="btn btn-sm btn-success w-100"
          icon={true}
        />
      </div>
    );
  }

  function ProductImage(rowData: any): JSX.Element {
    return (
      <div className="image-preview text-center">
        <ImageWithFallback
          className="img-fluid"
          src={rowData.ProductPrimaryImage}
          alt={rowData.Name}
        />
      </div>
    );
  }
  function ProductSku(rowData: any): JSX.Element {
    return (
      <a href={`/product?seoUrl=${rowData.ProductSeoUrl}`}>
        {rowData.ProductKey}
      </a>
    );
  }

  function ProductName(rowData: any): JSX.Element {
    return (
      <a href={`/product?seoUrl=${rowData.ProductSeoUrl}`}>
        {rowData.ProductName}
      </a>
    );
  }

  function SubTotalPrice(rowData: any): JSX.Element {
    let subTotal = rowData.UnitCorePrice * rowData.Quantity;
    return <span>{subTotal.toFixed(2)}</span>;
  }

  const columns: Array<Column<any>> = [
    {
      render: ProductImage,
      cellStyle: {
        width: "10%"
      }
    },
    {
      title: "Sku",
      render: ProductSku,
      cellStyle: {
        width: "10%"
      }
    },
    {
      title: "Name",
      render: ProductName,
      cellStyle: {
        width: "40%"
      }
    },
    {
      title: "Price",
      field: "UnitCorePrice",
      cellStyle: {
        width: "5%"
      }
    },
    {
      title: "SubTotal",
      render: SubTotalPrice,
      cellStyle: {
        width: "5%"
      }
    },
    {
      title: "Quantity",
      render: QuantitySelection,
      cellStyle: {
        width: "30%"
      }
    },
    {
      render: DeleteShoppingListButton
    }
  ];

  return (
    <Fragment>
      <Row>
        <Col className="form-inline">
          <h1>{typeName}</h1>
        </Col>
        <Col xs={auto}>
          <Button
            variant="secondary"
            className="mb-3 mx-1"
            onClick={() => clearShoppingList()}>
            {t("ui.storefront.common.Clear")}
          </Button>
          <Button
            variant="secondary"
            className="mb-3 mx-1"
            onClick={() => deleteShoppingList()}>
            {t("ui.storefront.common.Delete")}
          </Button>
        </Col>
      </Row>
      <Col xs={12}>
        {shoppingList.length ? (
          <Table data={shoppingList} columns={columns} />
        ) : (
          <span className="h4" id="ShoppingListDetailNoItemsText">
            {t(
              "ui.storefront.userDashboard2.controls.shoppingListDetail.NoItemsHaveBeenAddedToThisShoppingList"
            )}
          </span>
        )}
      </Col>
      <Link
        to={`/dashboard/shopping-lists`}
        className="btn btn-secondary my-3"
        type="button">
        {t("ui.storefront.userDashboard.shoppingLists.BackToShoppingLists")}
      </Link>
    </Fragment>
  );
};
