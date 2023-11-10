import React, { useState } from "react";
import { faMinus, faPlus, faTasks } from "@fortawesome/free-solid-svg-icons";
import { CatalogFilterHeaderButton } from "./CatalogFilterHeaderButton";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, Col, Card, Accordion, ListGroup } from "react-bootstrap";
import { useHistory, useLocation } from "react-router";
import { IAttributesFilterProps } from "./_CatalogFilterComponentsTypes";
import { useTranslation } from "react-i18next";
import { Dictionary } from "../../../_api/cvApi.shared";

export const AttributesFilter = (
  props: IAttributesFilterProps
): JSX.Element => {
  const {
    expandedFilterName,
    setExpandedFilterName,
    allAttributes,
    productSearchViewModel,
    setProductSearchViewModel
  } = props;

  const [expandedAttributes, setExpandedAttributes] = useState<string[]>([]);
  const { t } = useTranslation();

  const onClickAddAttribute = (
    e: React.MouseEvent<HTMLButtonElement>,
    key: string,
    value: string
  ): void => {
    //`{Color:[Black]}`
    e.preventDefault();
    let attributesAll: Dictionary<Array<string>> = {};
    if (
      productSearchViewModel?.Form?.AttributesAll &&
      productSearchViewModel?.Form?.AttributesAll[key]
    ) {
      attributesAll[key] = [
        ...productSearchViewModel?.Form?.AttributesAll[key]
      ];
      attributesAll[key].push(value);
    } else {
      attributesAll[key] = [value];
    }
    setProductSearchViewModel({
      ...productSearchViewModel,
      Form: {
        ...productSearchViewModel?.Form,
        Page: 1,
        AttributesAll: attributesAll
      }
    });
    // TODO@JDW: Figure out why this doesn't trigger useEffects
    // setProductSearchViewModel(productSearchViewModel => {
    //   if (!productSearchViewModel.Form.AttributesAll) {
    //     productSearchViewModel.Form.AttributesAll = {};
    //   }
    //   if (productSearchViewModel.Form.AttributesAll[key]) {
    //     productSearchViewModel.Form.AttributesAll[key].push(value);
    //   }
    //   productSearchViewModel.Form.AttributesAll[key] = [value];
    //   return productSearchViewModel;
    // });
  };

  return (
    <Col xs={12} sm={6} md={12}>
      <Card className="applied-filters mb-1">
        <Card.Header className="p-2 bg-light text-body">
          <CatalogFilterHeaderButton
            expandedFilterName={expandedFilterName}
            setExpandedFilterName={setExpandedFilterName}
            title={"Attributes"}
            icon={faTasks}
          />
        </Card.Header>
        <Accordion.Collapse
          className={expandedFilterName === "Attributes" ? "show" : ""}
          eventKey="attributes-collapse">
          <Card.Body
            className="p-3 bg-light"
            style={{ maxHeight: "50vh", overflowY: "auto" }}>
            <ListGroup as="ul" className="collapse-filter list-unstyled mb-0">
              {productSearchViewModel?.Attributes &&
                productSearchViewModel?.Attributes?.map(
                  (attr: any): JSX.Element => {
                    const { Key, Value } = attr;
                    return (
                      <Accordion.Item
                        key={Key}
                        eventKey="attributes-collapse"
                        className="border-0">
                        <Button
                          variant="link"
                          className="bg-light p-0 border-0 d-flex align-items-center justify-content-between w-100"
                          onClick={() => {
                            if (expandedAttributes.includes(Key)) {
                              setExpandedAttributes(
                                expandedAttributes.filter((a) => a !== Key)
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
                                  <Button
                                    variant="link"
                                    className="form-check-label py-0 bg-light"
                                    onClick={(e) => {
                                      onClickAddAttribute(e, Key, item.Key);
                                      console.log("Value", Value);
                                    }}>
                                    <span className="text small">
                                      {item.Key}
                                    </span>
                                    <span className="count small">
                                      ({item.Value})
                                    </span>
                                  </Button>
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
                    setExpandedAttributes(allAttributes.map((a: any) => a.Key));
                  }}>
                  {t("ui.storefront.searchCatalog.filters.ExpandAll")}
                </Button>
              </ListGroup.Item>
            </ListGroup>
          </Card.Body>
        </Accordion.Collapse>
      </Card>
    </Col>
  );
};
