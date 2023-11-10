import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash } from "@fortawesome/free-regular-svg-icons";
import { faAt } from "@fortawesome/free-solid-svg-icons";
import { useForm } from "react-hook-form";
import { useViewState } from "../_shared/customHooks/useViewState";
import { useLocation, useHistory } from "react-router-dom";
import { Message } from "../_shared/common/Message";
import { useTranslation } from "react-i18next";
import cvApi from "../_api/cvApi";
import { Form, Col, Row, InputGroup, ListGroup, Button } from "react-bootstrap";
interface IPasswordResetData {
  emForcedResetEmail: string;
  pwForcedResetPassword: string;
  pwForcedResetNewPassword: string;
}

export const ForcedPasswordReset = () => {
  const [showCurrentPassword, setShowCurrentPassword] =
    useState<boolean>(false);
  const [showNewPassword, setShowNewPassword] = useState<boolean>(false);
  const [wasSent, setWasSent] = useState<boolean>(false);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const history = useHistory();
  const location = useLocation();

  const params = new URLSearchParams(location.search);
  const returnUrl = params.get("returnUrl");

  const {
    register,
    handleSubmit,
    formState: { errors }
  } = useForm({
    criteriaMode: "all"
  });

  const toggleShowCurrentPassword = () => {
    setShowCurrentPassword(!showCurrentPassword);
  };

  const toggleShowNewPassword = () => {
    setShowNewPassword(!showNewPassword);
  };

  const onSubmit = (data: IPasswordResetData) => {
    setRunning();
    cvApi.authentication
      .ForcedPasswordReset({
        Email: data.emForcedResetEmail,
        OldPassword: data.pwForcedResetPassword,
        NewPassword: data.pwForcedResetNewPassword
      })
      .then((res) => {
        // @ts-ignore
        finishRunning(res.data.Messages.length, null, res.data.Messages);
        setWasSent(true);
        // @ts-ignore
        if (!res.data.Message.length) {
          history.push(returnUrl ?? "/");
        }
      })
      .catch((err) => {
        console.log(err);
        finishRunning(true, err);
      });
  };

  return (
    <Form as={Row} className="form-vertical" onSubmit={handleSubmit(onSubmit)}>
      <Col xs={12}>
        <h2 className="text-center">
          {t(
            "ui.storefront.user.forcedPasswordReset.YouAreRequiredToChangeYourPassword"
          )}
        </h2>
      </Col>
      <Form.Group as={Col} xs={12}>
        <Form.Label htmlFor="emForcedResetEmail">
          <span>{t("ui.storefront.common.Email")}</span>
          <span className="text-danger">&nbsp;*</span>
        </Form.Label>
        <InputGroup>
          <InputGroup.Text>
            <FontAwesomeIcon icon={faAt} className="fa-fw" />
          </InputGroup.Text>
          <Form.Control
            className="rounded-right"
            {...register("emForcedResetEmail", {
              required: {
                value: true,
                message: t(
                  "ui.storefront.common.validation.ThisFieldIsRequired"
                )
              },
              pattern: {
                value:
                  // eslint-disable-next-line no-useless-escape
                  /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i,
                message: "Email is not valid/formatted properly"
              }
            })}
            id="emForcedResetEmail"
            placeholder="Email"
            type="email"
          />
          <InputGroup.Text className="w-100 pt-1 pl-1">
            {errors.emForcedResetEmail ? (
              <span className="text-danger">
                {errors.emForcedResetEmail.message}
              </span>
            ) : null}
          </InputGroup.Text>
        </InputGroup>
      </Form.Group>
      <Form.Group as={Col} xs={12}>
        <Form.Label htmlFor="pwForcedResetPassword">
          <span>
            {t("ui.storefront.user.forcedPasswordReset.CurrentPassword")}
          </span>
          <span className="text-danger">&nbsp;*</span>
        </Form.Label>
        <InputGroup>
          <Form.Control
            className="rounded-right"
            {...register("pwForcedResetPassword", {
              required: {
                value: true,
                message: t(
                  "ui.storefront.common.validation.ThisFieldIsRequired"
                )
              }
            })}
            type={showCurrentPassword ? "text" : "password"}
            placeholder="Password"
            placeholder-key="ui.storefront.userDashboard2.controls.userProfile.EnterYourCurrentPassword"
            id="pwForcedResetPassword"
          />
          <InputGroup.Text>
            <Button
              variant="outline-secondary"
              className="rounded-right"
              onClick={toggleShowCurrentPassword}>
              <FontAwesomeIcon
                icon={showCurrentPassword ? faEyeSlash : faEye}
              />
            </Button>
          </InputGroup.Text>
          <div className="w-100">
            {errors.pwForcedResetPassword && (
              <ListGroup as="ul" className="list-unstyled">
                {errors.pwForcedResetPassword &&
                  Object.values<string>(errors.pwForcedResetPassword.types).map(
                    (msg): JSX.Element => {
                      return (
                        <ListGroup.Item className="text-danger" key={msg}>
                          {msg}
                        </ListGroup.Item>
                      );
                    }
                  )}
              </ListGroup>
            )}
          </div>
        </InputGroup>
      </Form.Group>
      <Form.Group as={Col} xs={12}>
        <Form.Label
          htmlFor="pwForcedResetNewPassword"
          className="control-label">
          <span>
            {t("ui.storefront.user.forgotPasswordReturn.NewPassword")}
          </span>
          <span className="text-danger">&nbsp;*</span>
        </Form.Label>
        <InputGroup>
          <Form.Control
            className="rounded-right"
            {...register("pwForcedResetNewPassword", {
              required: {
                value: true,
                message: t(
                  "ui.storefront.common.validation.ThisFieldIsRequired"
                )
              },
              minLength: {
                value: 7,
                message: t(
                  "ui.storefront.user.registration.PasswordRequirements.Message"
                )
              },
              validate: {
                // hasNumber: value => value.match(/\d+/g),
                hasLowerCase: (value) => {
                  if (!value.match(/[a-z]/g)) {
                    return t(
                      "ui.storefront.user.registration.PasswordRequirements.Message"
                    ) as string;
                  } else {
                    return true;
                  }
                },
                hasUpperCase: (value) => {
                  if (!value.match(/[A-Z]/g)) {
                    return t(
                      "ui.storefront.user.registration.PasswordRequirements.Message"
                    ) as string;
                  } else {
                    return true;
                  }
                },
                hasNumber: (value) => {
                  if (!value.match(/\d+/g)) {
                    return t(
                      "ui.storefront.user.registration.PasswordRequirements.Message"
                    ) as string;
                  } else {
                    return true;
                  }
                }
              }
            })}
            type={showNewPassword ? "text" : "password"}
            placeholder="Password"
            placeholder-key="ui.storefront.userDashboard2.controls.userProfile.EnterYourCurrentPassword"
            id="pwForcedResetNewPassword"
          />
          <InputGroup.Text>
            <Button
              variant="outline-secondary"
              className="rounded-right"
              onClick={toggleShowNewPassword}>
              <FontAwesomeIcon icon={showNewPassword ? faEyeSlash : faEye} />
            </Button>
          </InputGroup.Text>
          <div className="w-100">
            {errors.pwForcedResetNewPassword ? (
              <ListGroup as="ul" className="list-unstyled">
                {Object.values<string>(
                  errors.pwForcedResetNewPassword.types
                ).map((msg): JSX.Element => {
                  return (
                    <ListGroup.Item className="text-danger" key={msg}>
                      {msg}
                    </ListGroup.Item>
                  );
                })}
              </ListGroup>
            ) : null}
          </div>
        </InputGroup>
      </Form.Group>
      <Col xs={12}>
        <Button
          as="input"
          variant="primary"
          size="lg"
          type="submit"
          className="btn-block"
          value={
            t("ui.storefront.user.forcedPasswordReset.ChangePassword") as string
          }
        />
      </Col>
      <Col xs={12}>
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
