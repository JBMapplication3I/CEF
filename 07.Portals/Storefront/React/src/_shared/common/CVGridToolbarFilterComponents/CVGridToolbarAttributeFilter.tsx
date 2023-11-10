import { useState } from "react";
import useCustomCompareEffect from "../../customHooks/useCustomCompareEffect";
import { useTranslation } from "react-i18next";
import { useViewState } from "../../customHooks/useViewState";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import cvApi from "../../../_api/cvApi";
import { faMinus, faPlus } from "@fortawesome/free-solid-svg-icons";
import {
  Col,
  Form,
  Dropdown,
  Card,
  ListGroup,
  Accordion,
  Button
} from "react-bootstrap";
import {
  useProductSearchViewModel,
  compareProductCatalogSearchForm
} from "../../customHooks/catalog/useProductSearchViewModel";
import { ProductSearchViewModel } from "../../../_api/cvApi.shared";
import { IHttpPromiseCallbackArg } from "../../../_api/cvApi.shared";
import { ICVGridToolbarAttributesFilterProps } from "./_CVGridToolbarFilterTypes";

export const CVGridToolbarAttributeFilter = (
  props: ICVGridToolbarAttributesFilterProps
) => {
  const {
    toolbarFilterTitle,
    uniqueFieldID,
    attributesState,
    onRemoveAttributeChange,
    onAddAttributeChange
  } = props;
  const { setRunning, finishRunning, viewState } = useViewState();
  const [productSearchViewModel, setProductSearchViewModel] =
    useProductSearchViewModel();
  const [expandedAttributes, setExpandedAttributes] = useState<string[]>([]);

  const { t } = useTranslation();

  useCustomCompareEffect(
    catalogSearch,
    productSearchViewModel?.Form,
    compareProductCatalogSearchForm
  );

  function catalogSearch(): void {
    setRunning();
    if (!productSearchViewModel?.Form) {
      return;
    }
    // TODO: need a way to cancel this promise if it's running and the productSearchViewModel gets modified again
    cvApi.providers
      .SearchProductCatalogWithProvider(productSearchViewModel.Form)
      .then((res: IHttpPromiseCallbackArg<ProductSearchViewModel>) => {
        setProductSearchViewModel(res.data);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(
          true,
          err.message || "Failed to search product catalog with provider"
        );
      });
  }

  function toggleFilterOnAttribute(attr: string): void {
    setRunning();
    if (attributesState.includes(attr)) {
      const attrIndex = attributesState.indexOf(attr);
      onRemoveAttributeChange(attrIndex);
    }
    if (!attributesState.includes(attr)) {
      onAddAttributeChange(attr);
    }
    finishRunning();
  }

  return (
    <Col key={toolbarFilterTitle}>
      <Form.Label htmlFor={uniqueFieldID}>{toolbarFilterTitle}</Form.Label>
      <Form.Group controlId={uniqueFieldID}>
        <Dropdown style={{ display: "flex", justifyContent: "center" }}>
          <Dropdown.Toggle variant="outline-dark">
            Select an Attribute
          </Dropdown.Toggle>
          <Dropdown.Menu>
            <Card.Body
              className="p-3 bg-light"
              style={{
                maxHeight: "50vh",
                overflowY: "auto",
                minWidth: "5vw"
              }}>
              <ListGroup as="ul" className="collapse-filter list-unstyled mb-0">
                {productSearchViewModel?.Attributes &&
                  productSearchViewModel?.Attributes?.map(
                    (attr: any): JSX.Element => {
                      const { Key, Value } = attr;
                      return (
                        <Accordion.Item
                          key={Key}
                          eventKey="attributes-collapse"
                          style={{ textAlign: "left" }}
                          className="border-0">
                          <Button
                            variant="link"
                            className="bg-light p-0 border-0 d-flex align-items-center justify-content-between w-100"
                            onClick={() => {
                              if (expandedAttributes.includes(Key)) {
                                setExpandedAttributes(
                                  expandedAttributes.filter(
                                    (a: any) => a !== Key
                                  )
                                );
                              } else {
                                setExpandedAttributes([
                                  ...expandedAttributes,
                                  Key
                                ]);
                              }
                            }}>
                            <span>{Key}</span>
                            <FontAwesomeIcon
                              icon={
                                expandedAttributes.includes(Key)
                                  ? faMinus
                                  : faPlus
                              }
                              className="small"
                            />
                          </Button>
                          <ListGroup
                            style={{ alignItems: "flex-start" }}
                            className={`collapse collapse-list list-unstyled ${
                              expandedAttributes.includes(Key) ? "show" : ""
                            }`}>
                            {Value &&
                              Value.map((item: any): JSX.Element => {
                                return (
                                  <Accordion.Item
                                    key={item.Key}
                                    eventKey={item.Key}
                                    className="py-0 border-0 bg-light w-100">
                                    <Form.Check
                                      inline
                                      type="checkbox"
                                      className="mb-0">
                                      <Form.Check.Label className="mb-0">
                                        <Form.Check.Input
                                          type="checkbox"
                                          onClick={(e) =>
                                            toggleFilterOnAttribute(item.Key)
                                          }
                                        />
                                        &nbsp;
                                        <span>{item.Key}</span>
                                      </Form.Check.Label>
                                    </Form.Check>
                                  </Accordion.Item>
                                );
                              })}
                          </ListGroup>
                        </Accordion.Item>
                      );
                    }
                  )}
                <ListGroup.Item className="filter-item mt-3 d-flex justify-content-end bg-light border-0">
                  <Button
                    variant="secondary"
                    size="sm"
                    onClick={() => setExpandedAttributes([])}>
                    {t("ui.storefront.searchCatalog.filters.CollapseAll")}
                  </Button>
                  <Button
                    variant="primary"
                    size="sm"
                    className="ml-1"
                    onClick={(): void => {
                      setExpandedAttributes(
                        productSearchViewModel?.Attributes?.map(
                          (a: any) => a.Key
                        )
                      );
                    }}>
                    {t("ui.storefront.searchCatalog.filters.ExpandAll")}
                  </Button>
                </ListGroup.Item>
              </ListGroup>
            </Card.Body>
          </Dropdown.Menu>
        </Dropdown>
      </Form.Group>
    </Col>
  );
};
