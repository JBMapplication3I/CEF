import { useEffect, useRef, useState } from "react";
import { CatalogFilterHeaderButton } from "./CatalogFilterHeaderButton";
import { Accordion, Col, Card, Form } from "react-bootstrap";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { ProductSearchViewModel } from "../../../_api/cvApi.shared";
import { IRatingRangesFilterProps } from "./_CatalogFilterComponentsTypes";

const defaultSellerRatings = {
  any: false,
  "1": false,
  "2": false,
  "3": false,
  "4": false,
  "5": false
};

export const SellerRatingsFilter = (props: IRatingRangesFilterProps) => {
  const {
    expandedFilterName,
    setExpandedFilterName,
    productSearchViewModel,
    setProductSearchViewModel,
    ratingRanges
  } = props;
  const [checkedRatings, setCheckedRatings] = useState<{
    [key: string]: boolean;
  }>(defaultSellerRatings);

  useEffect(() => {
    if (!ratingRanges) {
      setCheckedRatings(defaultSellerRatings);
    }
  }, [productSearchViewModel]);

  useEffect(() => {
    // Rating ranges: 0-1, 2-3, 3-4, 4-5, and any === []
    let ranges: Array<string> = [];
    if (!checkedRatings["any"]) {
      Object.keys(checkedRatings).forEach((key: string) => {
        if (checkedRatings[key]) {
          ranges.push(`${parseInt(key) - 1} - ${parseInt(key)}`);
        }
      });
    }
    setProductSearchViewModel({
      ...productSearchViewModel,
      Form: {
        ...productSearchViewModel?.Form,
        Page: 1,
        RatingRanges: ranges,
        Query: undefined
      }
    });
  }, [checkedRatings]);

  const onClickSellerRating = (ratingKey: number | "any"): void => {
    if (typeof ratingKey === "number") {
      setCheckedRatings((prevRatings) => {
        return {
          ...prevRatings,
          [ratingKey.toString()]: !checkedRatings[ratingKey.toString()],
          any: false
        };
      });
      return;
    }
    setCheckedRatings((prevRatings) => {
      return {
        ...defaultSellerRatings,
        any: !prevRatings["any"]
      };
    });
  };

  return (
    <Col xs={12} sm={6} md={12}>
      <Card className="applied-filters mb-1">
        <Accordion flush>
          <Card.Header className="p-2 bg-light text-body">
            <CatalogFilterHeaderButton
              expandedFilterName={expandedFilterName}
              setExpandedFilterName={setExpandedFilterName}
              title={"Seller Ratings"}
              icon={faStar}
            />
          </Card.Header>
          <Accordion.Collapse role="null" eventKey="Seller Ratings">
            <Card.Body>
              <ul className="check-list list-unstyled mb-0">
                <li>
                  <Form.Check type="checkbox" className="big-mode">
                    <Form.Check.Input
                      type="checkbox"
                      checked={checkedRatings["any"]}
                      onChange={(e) => onClickSellerRating("any")}
                    />
                    <Form.Check.Label>Any</Form.Check.Label>
                  </Form.Check>
                </li>
                {Array(5)
                  .fill(null)
                  .map((_, index) => {
                    return (
                      <li key={index}>
                        <Form.Check type="checkbox" className="big-mode">
                          <Form.Check.Input
                            id={`sellerRating${index}`}
                            type="checkbox"
                            checked={checkedRatings[(index + 1).toString()]}
                            onChange={(e) => {
                              onClickSellerRating(index + 1);
                            }}
                          />
                          <Form.Check.Label>
                            <span className="stars">
                              {Array(5)
                                .fill(null)
                                .map((_, index2) => {
                                  return (
                                    <svg
                                      key={index2}
                                      width="20"
                                      height="19"
                                      xmlns="http://www.w3.org/2000/svg"
                                      viewBox="0 0 20 19">
                                      <path
                                        d="M10 14.332 4.122 18.09 5.88 11.34.49 6.909l6.964-.414L10 0l2.546 6.495 6.965.415-5.39 4.429 1.757 6.751z"
                                        fill={
                                          index2 <= index
                                            ? "#f8d200"
                                            : "#D9D9D9"
                                        }></path>
                                    </svg>
                                  );
                                })}
                            </span>
                          </Form.Check.Label>
                        </Form.Check>
                      </li>
                    );
                  })}
              </ul>
            </Card.Body>
          </Accordion.Collapse>
        </Accordion>
      </Card>
    </Col>
  );
};
