import React, { useState } from "react";
import { faFolder, faMinus, faPlus } from "@fortawesome/free-solid-svg-icons";
import { CatalogFilterHeaderButton } from "./CatalogFilterHeaderButton";
import { LoadingWidget } from "../../../_shared/common/LoadingWidget";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, Col, Card, Accordion, ListGroup } from "react-bootstrap";
import { ICategoriesFilterProps } from "./_CatalogFilterComponentsTypes";
import { useTranslation } from "react-i18next";

export const CategoriesFilter = (props: ICategoriesFilterProps): JSX.Element => {
  const {
    expandedFilterName,
    setExpandedFilterName,
    categoriesTree,
    allCategories,
    productSearchViewModel,
    setProductSearchViewModel
  } = props;

  const [expandedCategories, setExpandedCategories] = useState<string[]>([]);

  const { t } = useTranslation();

  const onClickAddCategory = (e: React.MouseEvent<HTMLButtonElement>, Key: string): void => {
    setProductSearchViewModel({
      ...productSearchViewModel,
      Form: {
        ...productSearchViewModel?.Form,
        Page: 1,
        Category: Key,
        Query: undefined
      }
    });

    e.preventDefault();
  };

  return (
    <Col xs={12} sm={6} md={12}>
      <Card className="applied-filters mb-1">
        <Card.Header className="p-2 bg-light text-body">
          <CatalogFilterHeaderButton
            expandedFilterName={expandedFilterName}
            setExpandedFilterName={setExpandedFilterName}
            title={"Categories"}
            icon={faFolder}
          />
        </Card.Header>
        <Accordion.Collapse
          eventKey="Categories"
          className={expandedFilterName === "Categories" ? "show" : ""}>
          <Card.Body className="p-3 bg-light" style={{ maxHeight: "50vh", overflowY: "auto" }}>
            {!categoriesTree ? (
              <LoadingWidget />
            ) : (
              <Accordion.Item
                as="ul"
                eventKey="Categories"
                className="collapse-filter list-unstyled mb-0 p-0 border-0">
                {/* Level 1 */}
                {categoriesTree?.Children?.map((category): React.ReactNode => {
                  const { Children, Key } = category;
                  if (!Key) {
                    return null;
                  }
                  const [Name1, CustomKey1] = Key?.split("|") || "|";
                  return (
                    <ListGroup.Item
                      key={CustomKey1}
                      className="p-0 collapse-filter-item border-0 bg-light">
                      <Button
                        variant="link"
                        className="p-0 d-flex align-items-center justify-content-between w-100"
                        onClick={(e) => {
                          if (Children?.length) {
                            if (expandedCategories.includes(Name1)) {
                              setExpandedCategories(expandedCategories.filter((c) => c !== Name1));
                            } else {
                              setExpandedCategories([...expandedCategories, Name1]);
                            }
                          } else {
                            onClickAddCategory(e, Key);
                          }
                        }}>
                        <span>{Name1}</span>
                        {Children?.length ? (
                          <FontAwesomeIcon
                            icon={expandedCategories.includes(Name1) ? faMinus : faPlus}
                            className="small"
                          />
                        ) : null}
                      </Button>
                      {expandedCategories.includes(Name1) && Children && Children?.length ? (
                        <ListGroup as="ul" className="collapse-list list-unstyled pl-1">
                          {Children.map((child): JSX.Element => {
                            const { Key } = child;
                            const [Name, CustomKey] = Key.split("|");
                            return (
                              <ListGroup.Item
                                className="p-1 pl-3 collapse-item bg-light border-0"
                                key={Name}>
                                <Button
                                  variant="link"
                                  className="p-0 bg-light"
                                  onClick={(e) => {
                                    onClickAddCategory(e, Key);
                                  }}>
                                  <span className="text">{Name}</span>
                                </Button>
                              </ListGroup.Item>
                            );
                          })}
                        </ListGroup>
                      ) : null}
                    </ListGroup.Item>
                  );
                })}
                <ListGroup.Item className="filter-item pt-3 d-flex justify-content-end bg-light border-0">
                  <Button variant="secondary" size="sm" onClick={() => setExpandedCategories([])}>
                    {t("ui.storefront.searchCatalog.filters.CollapseAll")}
                  </Button>
                  <Button
                    variant="primary"
                    size="sm"
                    className="ms-1"
                    onClick={() =>
                      setExpandedCategories(categoriesTree?.Children?.map((c: any) => c.Name))
                    }>
                    {t("ui.storefront.searchCatalog.filters.ExpandAll")}
                  </Button>
                </ListGroup.Item>
              </Accordion.Item>
            )}
          </Card.Body>
        </Accordion.Collapse>
      </Card>
    </Col>
  );
};
