import React, { useEffect, useState } from "react";
import { Form, InputGroup } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import cvApi from "../../_api/cvApi";
import { IHttpPromiseCallbackArg } from "../../_api/cvApi.shared";
import { CartTypeModel } from "../../_api/cvApi._DtoClasses";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { ConfirmationModal } from "../../_shared/modals";

interface ICreateShoppingListModalProps {
  show: boolean;
  onCancel: Function;
  onConfirm?: Function;
  cancelBtnLabel?: string;
  displayMessageOnConfirm?: boolean;
}

export const CreateShoppingListModal = (props: ICreateShoppingListModalProps): JSX.Element => {
  const [listName, setListName] = useState<string>("");
  const [listNameTouched, setListNameTouched] = useState<boolean>(false);
  const [showConfirmMessage, setShowConfirmMessage] = useState<boolean>(false);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const { show, onCancel, onConfirm, cancelBtnLabel, displayMessageOnConfirm } = props;

  useEffect(() => {
    if (show) {
      setShowConfirmMessage(false);
      setListName("");
      setListNameTouched(false);
    }
  }, [show]);

  function createNewShoppingList(): void {
    setRunning();
    cvApi.shopping
      .CreateCartTypeForCurrentUser({
        Active: true,
        CreatedDate: new Date(),
        ID: 0,
        Name: listName
      })
      .then((res: IHttpPromiseCallbackArg<CartTypeModel>) => {
        if (displayMessageOnConfirm) {
          setShowConfirmMessage(true);
        } else if (onConfirm) {
          onConfirm();
        }
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  const onListNameChanged = (val: string): void => {
    if (!listNameTouched) {
      setListNameTouched(true);
    }
    setListName(val);
  };

  return (
    <ConfirmationModal
      show={show}
      title="Create List"
      confirmBtnLabel="Create List"
      cancelBtnLabel={cancelBtnLabel ?? "Cancel"}
      onConfirm={createNewShoppingList}
      confirmDisabled={!listName.length}
      onCancel={onCancel}
      size="md">
      <Form.Group className="has-error mb-2">
        <Form.Label htmlFor="txtModalShoppingListCreateName">
          <span>
            {t("ui.storefront.userDashboard2.controls.createShoppingList.NewShoppingListName")}
          </span>
          <span className="text-danger">&nbsp;*</span>
        </Form.Label>
        <InputGroup>
          <Form.Control
            type="text"
            className="rounded-right"
            id="txtModalShoppingListCreateName"
            maxLength={128}
            onChange={(e) => onListNameChanged(e.target.value)}
            value={listName}
          />
          {!listName.length && listNameTouched ? (
            <InputGroup.Text className="w-100 pt-1 pl-1">
              <span className="text-danger">
                {t("ui.storefront.common.validation.ThisFieldIsRequired")}
              </span>
            </InputGroup.Text>
          ) : null}
        </InputGroup>
      </Form.Group>
      {showConfirmMessage ? (
        <div className="alert alert-success" id="ShoppingListModalNameText">
          <span>{listName}</span>
          &nbsp;
          <span>
            {t("ui.storefront.userDashboard2.controls.createShoppingList.HasBeenCreated")}
          </span>
        </div>
      ) : null}
    </ConfirmationModal>
  );
};
