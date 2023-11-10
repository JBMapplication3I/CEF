/* eslint-disable react-hooks/exhaustive-deps */
import { Fragment, useEffect, useState } from "react";
import { connect } from "react-redux";
import cvApi from "../../_api/cvApi";
import { ProductModel, UserModel } from "../../_api/cvApi._DtoClasses";
import { CEFConfig } from "../../_redux/_reduxTypes";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { Modal } from "../../_shared/modals/Modal";
import { CreateShoppingListModal } from "./CreateShoppingListModal";
import { useTranslation } from "react-i18next";
import { Form, Button, Alert } from "react-bootstrap";

interface IAddToShoppingListModalProps {
  show: boolean;
  onCancel: Function;
  product: ProductModel;
  productID?: number;
  productName?: string;
  shoppingLists?: any;
  cefConfig?: CEFConfig;
  currentUser?: UserModel;
}

interface IMapStateToAddToShoppingListModalProps {
  cefConfig: CEFConfig;
  staticCarts: any;
}

const mapStateToProps = (state: IMapStateToAddToShoppingListModalProps) => {
  const { staticCarts, cefConfig } = state;
  return {
    shoppingLists: staticCarts.shoppingLists,
    cefConfig: cefConfig
  };
};

export const AddToShoppingListModal = connect(mapStateToProps)(
  (props: IAddToShoppingListModalProps): JSX.Element => {
    const { show, onCancel, product } = props;

    const [shoppingLists, setShoppingLists] = useState<Array<any>>([]);
    const [selectedShoppingListName, setSelectedShoppingListName] =
      useState<string>("Select a Shopping List");
    const [itemIsIncludedInList, setItemIsIncludedInList] = useState<boolean>(false);
    const [showCreateShoppingListModal, setShowCreateShoppingListModal] = useState<boolean>(false);
    const [showAddToShoppingListModal, setShowAddToShoppingListModal] = useState<boolean>(true);
    const [showConfirmMessage, setShowConfirmMessage] = useState<boolean>(false);

    const { t } = useTranslation();
    const { setRunning, finishRunning, viewState } = useViewState();

    useEffect(() => {
      return () => {
        setShoppingLists(null);
      };
    }, []);

    useEffect(() => {
      if (show) {
        getShoppingLists();
        setSelectedShoppingListName("Select a Shopping List");
        setShowConfirmMessage(false);
      }
    }, [show]);

    useEffect(() => {
      if (showAddToShoppingListModal) {
        getShoppingLists();
      }
    }, [showAddToShoppingListModal]);

    function getShoppingLists(): void {
      setRunning();
      cvApi.shopping
        .GetCurrentUserCartTypes({ IncludeNotCreated: false })
        .then((res) => {
          setShoppingLists(res.data.Results);
          finishRunning();
        })
        .catch((err: any) => {
          finishRunning(true, err.message || "Failed to get current user cart types");
        });
    }

    function checkIfItemIsInCart(TypeName: string): void {
      cvApi.shopping
        .GetCurrentStaticCart({ TypeName })
        .then((res: any) => {
          if (res.data && res.data.SalesItems) {
            let cartItems = res.data.SalesItems;
            let included = false;
            for (let i = 0; i < cartItems.length; i++) {
              if (cartItems[i].ProductID === product.ID) {
                included = true;
                break;
              }
            }
            setItemIsIncludedInList(included);
          } else {
            setItemIsIncludedInList(false);
          }
        })
        .catch((err: any) => {
          console.log(err);
        });
    }

    function addItemToShoppingList(
      typeName: string,
      productID: number,
      quantity: number = 1
    ): void {
      cvApi.shopping
        .AddStaticCartItem({
          TypeName: typeName,
          ProductID: productID,
          Quantity: quantity
        })
        .then((res: any) => {
          setShowConfirmMessage(true);
        })
        .catch((err: any) => {
          console.log(err);
        });
    }

    const switchToCreateShoppingListModal = (): void => {
      setShowAddToShoppingListModal(false);
      setShowCreateShoppingListModal(true);
      setShowConfirmMessage(false);
    };

    const switchToAddToShoppingListModal = (): void => {
      setShowAddToShoppingListModal(true);
      setShowCreateShoppingListModal(false);
      setShowConfirmMessage(false);
    };

    return (
      <Fragment>
        <CreateShoppingListModal
          show={showCreateShoppingListModal}
          onCancel={switchToAddToShoppingListModal}
          onConfirm={() => {
            setShowAddToShoppingListModal(false);
            onCancel();
          }}
          cancelBtnLabel="Back"
          displayMessageOnConfirm={true}
        />
        <Modal
          title="Select a Shopping List"
          onCancel={onCancel}
          show={show && showAddToShoppingListModal}
          size="md">
          <Form.Select
            className="mb-3 ng-pristine ng-untouched ng-valid ng-empty"
            id="shoppingListSelect"
            name="shoppingListSelect"
            onChange={(e) => {
              checkIfItemIsInCart(e.target.value);
              setSelectedShoppingListName(e.target.value);
              if (showConfirmMessage) {
                setShowConfirmMessage(false);
              }
            }}
            value={selectedShoppingListName}>
            <option value="Select a Shopping List" disabled={true}>
              {t("ui.storefront.userDashboard2.controls.addToShoppingList.SelectAShoppingList")}
            </option>
            {shoppingLists.map((shoppingList: any) => {
              const { Name, ID } = shoppingList;
              return (
                <option key={ID} value={Name}>
                  {Name}
                </option>
              );
            })}
          </Form.Select>
          {showConfirmMessage ? (
            <Alert variant="success" id="ShoppingListModalNameText">
              {product.Name || product.CustomKey} has been added to{" "}
              <span>{selectedShoppingListName}</span>
              &nbsp;
            </Alert>
          ) : null}
          <div className="d-flex justify-content-end align-items-center gap-1">
            <Button
              variant="secondary"
              className="ng-scope"
              id="closeSelectShoppingListModal"
              onClick={() => onCancel()}>
              {t("ui.storefront.userDashboard2.controls.createShoppingList.BackToExistingLists")}
            </Button>
            <Button
              variant="secondary"
              className="ng-scope"
              id="btnModalCreateShoppingList"
              name="btnModalCreateShoppingList"
              onClick={() => switchToCreateShoppingListModal()}>
              {t("ui.storefront.userDashboard2.controls.addToShoppingList.CreateAndAddAnother")}
            </Button>
            {itemIsIncludedInList ? (
              <Button
                variant="primary"
                className="ng-scope"
                id="addToShoppingListModalBtn"
                disabled={true}>
                {t("ui.storefront.userDashboard2.controls.addToShoppingList.AlreadyInThisList")}
              </Button>
            ) : (
              <Button
                variant="primary"
                className="ng-scope"
                id="addToShoppingListModalBtn"
                onClick={() => {
                  addItemToShoppingList(selectedShoppingListName, product.ID);
                }}>
                {t("ui.storefront.userDashboard2.controls.addToShoppingList.AddToList")}
              </Button>
            )}
          </div>
        </Modal>
      </Fragment>
    );
  }
);
