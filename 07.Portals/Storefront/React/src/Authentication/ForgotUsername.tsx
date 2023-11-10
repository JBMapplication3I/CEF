import { useEffect, useState } from "react";
import { faAt } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useViewState } from "../_shared/customHooks/useViewState";
import { Message } from "../_shared/common/Message";
import { LoadingWidget } from "../_shared/common/LoadingWidget";
import { useTranslation } from "react-i18next";
import cvApi from "../_api/cvApi";
import { Form, Col, Row, InputGroup, ListGroup, Button } from "react-bootstrap";

export const ForgotUsername = (): JSX.Element => {
  const [email, setEmail] = useState<string>("");
  const [wasSent, setWasSent] = useState<boolean>(false);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    setWasSent(false);
  }, [email]);

  function submitRequestForUsername(): void {
    setRunning();
    cvApi.authentication
      .ForgotUsername({ Email: email })
      .then((res) => {
        setWasSent(true);
        // @ts-ignore
        finishRunning(res.data.Messages.length, null, res.data.Messages);
      })
      .catch((err) => {
        finishRunning(true, err);
      });
  }

  return (
    <Form as={Row} className="form-vertical">
      <Col xs={12}>
        <h2 className="text-center ng-scope">
          {t("ui.storefront.user.forgotUsername.RequestYourUsername")}
        </h2>
        <p>
          <span>
            {t(
              "ui.storefront.user.forgotPassword.YourPrivacyIsImportantToUs.Message"
            )}
          </span>
          &nbsp;
          <span>
            {t(
              "ui.storefront.user.forgotPassword.IsAddedToYourEmailWhitelistApprovedSendersAndCheckYourSpamJunkFolders"
            )}
          </span>
        </p>
      </Col>
      <Col xs={12}>
        <Row>
          {viewState.running ? (
            <LoadingWidget />
          ) : (
            <Form.Group as={Col} xs={12} className="has-error">
              <Form.Label htmlFor="txtForgotUsernameEmail">
                <span>{t("ui.storefront.common.Email")}</span>
                &nbsp;
                <span className="text-danger">*</span>
              </Form.Label>
              <InputGroup>
                <InputGroup.Text>
                  <span className="input-group-text h-100">
                    <FontAwesomeIcon icon={faAt} className="fa-fw" />
                  </span>
                </InputGroup.Text>
                <Form.Control
                  type="email"
                  id="txtForgotUsernameEmail"
                  name="txtForgotUsernameEmail"
                  placeholder="john.smith@email.com"
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </InputGroup>
            </Form.Group>
          )}
        </Row>
        <Row className="align-items-center">
          <Col className="text-right">
            <a href="/authentication/registration">
              <span>{t("ui.storefront.common.Register")}</span>
            </a>
          </Col>
          <Col xs="auto">
            <Button
              variant="primary"
              onClick={submitRequestForUsername}
              title={t("ui.storefront.user.forgotUsername.RetrieveUsername")}
              aria-label={t(
                "ui.storefront.user.forgotUsername.RetrieveUsername"
              )}>
              {t("ui.storefront.user.forgotUsername.RetrieveUsername")}
            </Button>
          </Col>
        </Row>
        {wasSent && viewState.hasError ? (
          <Row className="mt-3">
            <Col xs={12}>
              <ListGroup as="ul" className="list-unstyled">
                {Array.isArray(viewState.errorMessages) &&
                viewState.errorMessages.length ? (
                  viewState.errorMessages.map((msg) => {
                    return (
                      <ListGroup.Item key={msg as string}>
                        <Message error={true} message={msg as string} />
                      </ListGroup.Item>
                    );
                  })
                ) : (
                  <ListGroup.Item>
                    <Message
                      error={true}
                      message={viewState.errorMessage as string}
                    />
                  </ListGroup.Item>
                )}
              </ListGroup>
            </Col>
          </Row>
        ) : null}
      </Col>
    </Form>
  );
};
