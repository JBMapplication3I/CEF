import { faTasks } from "@fortawesome/free-solid-svg-icons";
import { CatalogFilterHeaderButton } from "./CatalogFilterHeaderButton";
import { IBrandsFilterProps } from "./_CatalogFilterComponentsTypes";
import { Button, Col, Card, Accordion, ListGroup } from "react-bootstrap";
export const BrandsFilter = (props: IBrandsFilterProps): JSX.Element => {
  const {
    expandedFilterName,
    setExpandedFilterName,
    productSearchViewModel,
    setProductSearchViewModel,
    brands
  } = props;

  const onClickBrandName = (
    e: React.MouseEvent<HTMLButtonElement>,
    value: string
  ) => {
    e.preventDefault();
    setProductSearchViewModel({
      ...productSearchViewModel,
      Form: { ...productSearchViewModel?.Form, Page: 1, BrandName: value }
    });
  };

  return (
    <Col lg={12} md={12} sm={6}>
      <Card className="applied-filters mb-1">
        <Card.Header className="p-2 bg-light text-body">
          <CatalogFilterHeaderButton
            expandedFilterName={expandedFilterName}
            setExpandedFilterName={setExpandedFilterName}
            title={"Brands"}
            icon={faTasks}
          />
        </Card.Header>
        <Accordion.Collapse
          eventKey="brands-collapse"
          className={expandedFilterName === "Brands" ? "show" : ""}>
          <Card.Body
            className="p-3 bg-light"
            style={{ maxHeight: "50vh", overflowY: "auto" }}>
            <ListGroup as="ul" className="collapse-filter list-unstyled mb-0">
              {brands?.map((brand: [string, number]): JSX.Element => {
                const brandName = brand[0];
                const productAmount = brand[1];
                return (
                  <Accordion.Item
                    className="bg-light p-0 border-0"
                    eventKey="brands-collapse"
                    key={brandName}>
                    <Button
                      variant="link"
                      className="p-0 form-check-label"
                      style={{ textAlign: "left" }}
                      onClick={(e) => onClickBrandName(e, brandName)}>
                      <span className="text">{brandName}</span>
                      <span className="count"> ({productAmount})</span>
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
