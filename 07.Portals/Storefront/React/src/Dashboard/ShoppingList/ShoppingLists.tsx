import { Fragment, useEffect, useState } from "react";
import { faClipboardList } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Column } from "@material-table/core";
import cvApi from "../../_api/cvApi";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { Link } from "react-router-dom";
import { Table } from "../../_shared/common/Table";
import { CreateShoppingListModal } from "./CreateShoppingListModal";
import { useTranslation } from "react-i18next";
import { Row, Col, Button } from "react-bootstrap";
export const ShoppingLists = (): JSX.Element => {
  const [showCreateListModal, setShowCreateListModal] =
    useState<boolean>(false);
  const [shoppingLists, setShoppingLists] = useState<Array<any>>([]);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    getShoppingLists();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  function getShoppingLists(): void {
    setRunning();
    cvApi.shopping
      .GetCurrentUserCartTypes({ IncludeNotCreated: false })
      .then((res) => {
        setShoppingLists(res.data.Results);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "Failed to get current user cart types"
        );
      });
  }

  function ViewDetailsButton(rowData: any): JSX.Element {
    return (
      // TODO: don't use string literals for path names, this needs to be in cvServiceStrings or something
      <Link
        to={`/dashboard/shopping-lists/Detail/${rowData.Name}`}
        className="btn btn-outline-primary btn-sm"
        type="button">
        {t("ui.storefront.common.View")}
      </Link>
    );
  }

  const columns: Array<Column<any>> = [
    {
      title: "List Name",
      field: "Name",
      cellStyle: {
        width: "50%"
      }
    },
    {
      title: "List Date",
      field: "CreatedDate",
      cellStyle: {
        width: "50%"
      }
    },
    {
      render: ViewDetailsButton
    }
  ];

  return (
    <Fragment>
      <CreateShoppingListModal
        show={showCreateListModal}
        onCancel={() => setShowCreateListModal(false)}
        onConfirm={() => {
          getShoppingLists();
          setShowCreateListModal(false);
        }}
      />
      <Row>
        <Col xs={12}>
          <h1>
            <FontAwesomeIcon icon={faClipboardList} className="me-2" />
            <span>Shopping List</span>
          </h1>
        </Col>
        <Col xs={12}>
          <Row>
            <Col
              xs={12}
              className="d-flex justify-content-end align-items-center">
              <Button
                variant="secondary"
                className="mb-3"
                id="btnShoppingListCreate"
                name="btnShoppingListCreate"
                title="Create a New List"
                aria-label="Create a New List"
                onClick={() => setShowCreateListModal(true)}>
                {t(
                  "ui.storefront.userDashboard2.userDashboard.controls.createANewList"
                )}
              </Button>
            </Col>
            <Col xs={12}>
              {shoppingLists.length ? (
                <Table data={shoppingLists} columns={columns} />
              ) : (
                <span className="h4" id="ShoppingListNoItemsText">
                  {t(
                    "ui.storefront.userDashboard2.controls.shoppingListDetail.NoItemsHaveBeenAddedToThisShoppingList"
                  )}
                </span>
              )}
            </Col>
          </Row>
        </Col>
      </Row>
    </Fragment>
  );
};
