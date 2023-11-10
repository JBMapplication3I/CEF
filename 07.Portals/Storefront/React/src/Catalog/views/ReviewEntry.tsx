import { faExclamationTriangle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Fragment, useState } from "react";
import { useForm } from "react-hook-form";
import { connect } from "react-redux";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { RatingEntry } from "./RatingEntry";
import { LoginModal } from "../../Authentication/LoginModal";
import { useTranslation } from "react-i18next";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import cvApi from "../../_api/cvApi";
import { CreateReviewDto } from "../../_api";
import { IReduxStore } from "../../_redux/_reduxTypes";
import { UserModel } from "../../_api/cvApi._DtoClasses";
import { Alert, Button, Form, Card, InputGroup } from "react-bootstrap";

interface IReviewEntryProps {
  id: number;
  currentUser?: UserModel; //redux
}

interface IMyProfileDataEntry {
  txtProductReviewTitle?: string;
  txtProductReviewComment?: string;
}

interface IMapStateToCefReviewEntryProps {
  currentUser: UserModel;
}

const mapStateToProps = (state: IReduxStore): IMapStateToCefReviewEntryProps => {
  return {
    currentUser: state.currentUser
  };
};

export const ReviewEntry = connect(mapStateToProps)((props: IReviewEntryProps): JSX.Element => {
  const { id, currentUser } = props;

  const [submitted, setSubmitted] = useState<boolean>(false);
  const [ratingAmount, setRatingAmount] = useState<number>(0);
  const [showLoginModal, setShowLoginModal] = useState<boolean>(false);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const {
    register,
    handleSubmit,
    formState: { errors }
  } = useForm();

  const saveReview = (data: IMyProfileDataEntry) => {
    const { txtProductReviewTitle, txtProductReviewComment } = data;
    if (!ratingAmount) {
      return;
    }
    setRunning();
    cvApi.reviews
      .CreateReview({
        Active: true,
        Approved: false,
        Comment: txtProductReviewComment,
        Title: txtProductReviewTitle,
        CreatedDate: new Date(),
        ProductID: id,
        SubmittedByUserID: null,
        TypeID: 0,
        Value: ratingAmount
      } as CreateReviewDto)
      .then((_res: any) => {
        setSubmitted(true);
        finishRunning();
      })
      .catch((err: any) => {
        console.log(err);
        finishRunning(true, err);
      });
  };

  return (
    <Form
      className={`d-flex flex-column position-relative card ${
        submitted ? "border-success" : ""
      }`}
      onSubmit={handleSubmit(saveReview)}>
      {viewState.running ? <LoadingWidget overlay={true} /> : null}
      <Card.Header className="w-100">
        {t("ui.storefront.product.reviews.productReview.writeAReview")}
      </Card.Header>
      {currentUser.Contact ? (
        <Card.Body className="w-100">
          <Form.Group>
            <Form.Label id="WriteAReviewRatingText">
              {t("ui.storefront.product.reviews.reviewEntry.Rating")}
            </Form.Label>
            <InputGroup>
              <RatingEntry
                scale={5}
                ratingAmount={ratingAmount}
                setRatingAmount={setRatingAmount}
              />
            </InputGroup>
          </Form.Group>
          <Form.Group>
            <Form.Label
              htmlFor="txtProductReviewTitle"
              id="WriteAReviewTitleText">
              {t("ui.storefront.common.Title")}
            </Form.Label>
            <InputGroup>
              <Form.Control
                className="form-control"
                {...register("txtProductReviewTitle", {
                  required: {
                    value: true,
                    message: "This field is required"
                  }
                })}
                disabled={submitted}
                id="txtProductReviewTitle"
                name="txtProductReviewTitle"
                type="text"
              />
              {errors.txtProductReviewTitle ? (
                <Alert variant="danger">
                  <FontAwesomeIcon
                    icon={faExclamationTriangle}
                    className="fa-fw"
                  />
                  <span className="sr-only">
                    {t("ui.storefront.common.Error")}
                  </span>
                  &nbsp;
                  <span>{errors.txtProductReviewComment.message}</span>
                </Alert>
              ) : null}
            </InputGroup>
          </Form.Group>
          <Form.Group>
            <Form.Label htmlFor="txtProductReviewComment">
              {t("ui.storefront.product.reviews.reviewEntry.Comment")}
            </Form.Label>
            <InputGroup>
              <Form.Control
                as="textarea"
                className="mb-2"
                id="txtProductReviewComment"
                disabled={submitted}
                {...register("txtProductReviewComment", {
                  required: {
                    value: true,
                    message: "This field is required"
                  }
                })}
              />
              {errors.txtProductReviewComment ? (
                <Alert variant="danger">
                  <FontAwesomeIcon
                    icon={faExclamationTriangle}
                    className="fa-fw"
                  />
                  <span className="sr-only">
                    {t("ui.storefront.common.Error")}
                  </span>
                  &nbsp;
                  <span>{errors.txtProductReviewComment.message}</span>
                </Alert>
              ) : null}
            </InputGroup>
          </Form.Group>
          <div className="d-flex justify-content-end">
            <Button
              variant="primary"
              type="submit"
              title="Save"
              id="btnSaveReview"
              name="btnSaveReview"
              disabled={submitted}>
              {t("ui.storefront.common.Save")}
            </Button>
          </div>
        </Card.Body>
      ) : (
        <Fragment>
          <LoginModal
            show={showLoginModal}
            onCancel={() => setShowLoginModal(false)}
            onConfirm={() => setShowLoginModal(false)}
          />
          <Button
            variant="primary"
            size="lg"
            title="Sign In to Review"
            onClick={() => setShowLoginModal(true)}>
            {t("ui.storefront.product.reviews.reviewEntry.signInToReview")}
          </Button>
        </Fragment>
      )}
      {submitted ? (
        <Card.Footer className="border-success bg-success text-white">
          <p>
            {t(
              "ui.storefront.product.reviews.reviewEntry.successYourReviewHasBeenSaved"
            )}
          </p>
          <p className="mb-0">
            <b>{t("ui.storefront.common.Note")}</b>
            :&nbsp;
            <span id="ReviewAdminNote">
              {t(
                "ui.storefront.product.reviews.reviewEntry.theWebsiteAdministratorRequiresThatReviewsAreApprovedBefore"
              )}
            </span>
          </p>
        </Card.Footer>
      ) : null}
    </Form>
  );
});
