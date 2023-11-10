import React from "react";
import { faDollarSign } from "@fortawesome/free-solid-svg-icons";
import { Button, Col, Card, Accordion, ListGroup } from "react-bootstrap";
import { CatalogFilterHeaderButton } from "./CatalogFilterHeaderButton";
import { IPriceRangesFilterProps } from "./_CatalogFilterComponentsTypes";

export const PriceRangesFilter = (
  props: IPriceRangesFilterProps
): JSX.Element => {
  const {
    expandedFilterName,
    setExpandedFilterName,
    pricingRanges,
    productSearchViewModel,
    setProductSearchViewModel
  } = props;

  const onClickAddPriceRange = (
    e: React.MouseEvent<HTMLButtonElement>,
    value: string
  ) => {
    e.preventDefault();
    setProductSearchViewModel({
      ...productSearchViewModel,
      Form: { ...productSearchViewModel?.Form, Page: 1, PricingRanges: [value] }
    });
    // TODO@JDW: Figure out why this doesn't trigger useEffects
    // setProductSearchViewModel(productSearchViewModel => {
    //   productSearchViewModel.Form.PriceRanges = [value];
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
            title={"Price Range"}
            icon={faDollarSign}
          />
        </Card.Header>
        <Accordion.Collapse
          eventKey="price-range-collapse"
          className={expandedFilterName === "Price Range" ? "show" : ""}>
          <Card.Body
            className="p-3 bg-light"
            style={{ maxHeight: "50vh", overflowY: "auto" }}>
            <ListGroup as="ul" className="collapse-filter list-unstyled mb-0">
              {pricingRanges &&
                pricingRanges.length &&
                pricingRanges.map((range): JSX.Element => {
                  const { Label, DocCount } = range;
                  return (
                    <Accordion.Item
                      key={Label}
                      eventKey="pricing-range-collapse"
                      className="bg-light p-0 border-0">
                      <Button
                        variant="link"
                        className="p-0 form-check-label"
                        onClick={(e) => onClickAddPriceRange(e, Label)}
                        type="button">
                        <span className="text">{Label}</span>
                        <span className="count">({DocCount})</span>
                      </Button>
                    </Accordion.Item>
                  );
                })}
            </ListGroup>
          </Card.Body>
        </Accordion.Collapse>
      </Card>
    </Col>
  );
};
