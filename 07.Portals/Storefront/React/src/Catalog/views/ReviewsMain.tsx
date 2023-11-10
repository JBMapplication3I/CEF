import { useEffect, useState } from "react";
import { ReviewEntry } from "./ReviewEntry";
import { RatingSummary } from "./RatingSummary";
import { ProductReviewInformationModel } from "../../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import cvApi from "../../_api/cvApi";
import { Row, Col, Card } from "react-bootstrap";

interface IReviewsMainProps {
  id: number;
}

export const ReviewsMain = (props: IReviewsMainProps): JSX.Element => {
  const { id } = props;

  const [productReviewData, setProductReviewData] =
    useState<ProductReviewInformationModel | null>(null);

  const { t } = useTranslation();

  useEffect(() => {
    cvApi.products
      .GetProductReview(id)
      .then((res) => {
        setProductReviewData(res.data);
      })
      .catch((err: any) => {
        console.log(err);
      });
  }, []);

  const approvedReviews = productReviewData?.Reviews?.filter((r) => r.Approved);
  approvedReviews && approvedReviews.sort((a, b) => a.SortOrder - b.SortOrder);
  const reviewsForAverage = approvedReviews?.filter((ar: any) => ar.Value > 0);
  const average =
    reviewsForAverage
      ?.map((ar: any): number => ar.Value)
      .reduce((a: number, b: number): number => {
        return a + b;
      }, 0) / reviewsForAverage?.length;

  return (
    <Row className="border border-light mb-2 pb-2">
      <Col xs={12} sm={6} md={8}>
        {approvedReviews && approvedReviews.length ? (
          <Row>
            <Col sm={12} className="cef-ratings mb-3">
              <RatingSummary
                scale={5}
                count={approvedReviews.length}
                value={average}
              />
            </Col>
            {approvedReviews.map((review: any) => {
              const { ID, Title, Value, Comment } = review;
              return (
                <Col sm={12} key={ID}>
                  <Card>
                    <Card.Header>
                      <div className="d-flex justify-content-between">
                        <span>{Title}</span>
                        {Value > 0 ? (
                          <RatingSummary
                            scale={5}
                            value={Value}
                            count={0}
                            hideCount={true}
                          />
                        ) : null}
                      </div>
                    </Card.Header>
                    <Card.Body>
                      <Card.Text>{Comment}</Card.Text>
                    </Card.Body>
                  </Card>
                </Col>
              );
            })}
          </Row>
        ) : (
          <p>
            {t(
              "ui.storefront.product.reviews.reviewList.thereAreNoReviewsForThisProduct"
            )}
          </p>
        )}
      </Col>
      <Col xs={12} sm={6} md={4}>
        <ReviewEntry id={id} />
      </Col>
    </Row>
  );
};
