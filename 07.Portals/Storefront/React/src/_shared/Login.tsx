import React, { useCallback, useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash, faQuestionCircle } from "@fortawesome/free-solid-svg-icons";
import { LoadingWidget } from "./common/LoadingWidget";
import { ErrorView } from "./common/ErrorView";
import { connect } from "react-redux";
import { logUserIn } from "../_redux/actions";
import { useViewState } from "./customHooks/useViewState";
import { useHistory } from "react-router";
import { useTranslation } from "react-i18next";
import axios from "../axios";
import cvApi from "../_api/cvApi";
import { IReduxStore } from "../_redux/_reduxTypes";
import { UserModel } from "../_api/cvApi._DtoClasses";
import {
  Form,
  InputGroup,
  Col,
  Row,
  Container,
  Button,
  Modal
} from "react-bootstrap";

interface ILoginProps {
  currentUser?: UserModel; // redux
  showCancel?: boolean;
  onCancel?: Function;
  onLoginSuccess?: Function;
  returnUrl?: string;
  hideButtonsInFooter?: boolean;
  customColumnSizes?: any;
}

interface IMapStateToLoginProps {
  currentUser: UserModel;
}

const mapStateToProps = (state: IReduxStore): IMapStateToLoginProps => {
  return {
    currentUser: state.currentUser
  };
};

export const Login = connect(mapStateToProps)((props: ILoginProps): JSX.Element => {
  const {
    currentUser,
    showCancel,
    onCancel,
    onLoginSuccess,
    returnUrl,
    customColumnSizes,
    hideButtonsInFooter
  } = props;

  const [userName, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [showPassword, setShowPassword] = useState<boolean>(false);
  // const [rememberMe, setRememberMe] = useState<boolean>(false);
  const [error, setError] = useState<any>(null);

  const history = useHistory();

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const handleSubmit = (
    _e: React.KeyboardEvent<HTMLInputElement> | React.MouseEvent<HTMLButtonElement>
  ): void => {
    setRunning();
    axios
      .post("/auth/identity", {
        Username: userName,
        Password: password
        //, RememberMe: rememberMe
      })
      .then((res: any) => {
        localStorage.setItem("user", JSON.stringify(res.data));
        return cvApi.contacts.GetCurrentUser();
      })
      .then((res) => {
        logUserIn(res.data); // useEffect will call onLoginSuccess()
        finishRunning();
        if (returnUrl) {
          history.push(returnUrl);
        }
      })
      .catch((err: any) => {
        setError(err);
        finishRunning(true, err.message ?? "Failed to handle submit in Login");
      });
  };

  const handleCancel = (): void => {
    setUsername("");
    setPassword("");
    if (onCancel) {
      onCancel();
    }
  };

  const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    setUsername(e.target.value);
    if (error) {
      setError(null);
    }
  };
  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    setPassword(e.target.value);
    if (error) {
      setError(null);
    }
  };
  const toggleShowPassword = (_e: React.MouseEvent<HTMLButtonElement>): void =>
    setShowPassword(!showPassword);

  useEffect((): void => {
    if (currentUser && currentUser.Contact && onLoginSuccess) {
      onLoginSuccess();
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [currentUser]);

  if (currentUser.Contact) {
    return <p>User is logged in</p>;
  }

  return viewState.running ? (
    <LoadingWidget />
  ) : (
    <Container fluid>
      <Row className="form-vertical">
        <Col
          xs={customColumnSizes["xs"] ?? 12}
          md={customColumnSizes["md"] ?? 12}
          xl={customColumnSizes["xl"] ?? 12}
          className="p-3 rounded mx-auto">
          <Row>
            <Form.Group as={Col} xs={12} className="has-error mb-2">
              <Form.Label className="d-block text-left" htmlFor="username">
                <span>{t("ui.storefront.common.Username")}</span>
                <span className="text-danger"> *</span>
                <span className="text-info ml-1">
                  <FontAwesomeIcon icon={faQuestionCircle} />
                </span>
              </Form.Label>
              <InputGroup>
                <input
                  className="form-control"
                  value={userName}
                  type="text"
                  id="username"
                  required
                  data-interactive="true"
                  placeholder="Enter your username"
                  onChange={handleUsernameChange}
                />
              </InputGroup>
            </Form.Group>
            <Form.Group as={Col} xs={12} className="has-error mb-2">
              <Form.Label className="d-block text-left" htmlFor="password">
                <span>{t("ui.storefront.common.Password")}</span>
                <span className="text-danger"> *</span>
                <span className="text-info ml-1">
                  <FontAwesomeIcon icon={faQuestionCircle} />
                </span>
              </Form.Label>
              <InputGroup>
                <input
                  className="form-control"
                  type={showPassword ? "text" : "password"}
                  id="password"
                  required
                  data-interactive="true"
                  placeholder="Enter your password"
                  value={password}
                  onChange={handlePasswordChange}
                  onKeyDown={(e) => {
                    if (e.key === "Enter") {
                      handleSubmit(e);
                    }
                  }}
                />
                <InputGroup.Text className="p-0">
                  <Button
                    variant="outline-secondary"
                    className="rounded-right"
                    onClick={toggleShowPassword}>
                    <FontAwesomeIcon icon={showPassword ? faEyeSlash : faEye} />
                  </Button>
                </InputGroup.Text>
              </InputGroup>
            </Form.Group>
          </Row>
          <ErrorView error={error && error.message} />
          <Row className="align-items-center">
            {hideButtonsInFooter && (
              <Col xs="auto">
                <Button variant="primary" onClick={handleSubmit}>
                  {t("ui.storefront.user.login.SignIn")}
                </Button>
              </Col>
            )}
            <Col className="text-right">
              <small>
                <a
                  href="/authentication/forgot-password"
                  id="btnLoginForgotPassword"
                  onClick={handleCancel}>
                  {t("ui.storefront.user.login.ForgotYourPassword")}
                </a>
                <br />
                <span>Don&apos;t have an account?&nbsp;</span>&nbsp;
                <a href="/Authentication/Registration" onClick={handleCancel}>
                  {t("ui.storefront.common.RegisterForOne")}
                </a>
              </small>
            </Col>
          </Row>
        </Col>
        {!hideButtonsInFooter && (
          <Modal.Footer>
            {showCancel && (
              <Button variant="outline-secondary" onClick={handleCancel}>
                {t("ui.storefront.common.Close")}
              </Button>
            )}
            <Button variant="primary" onClick={handleSubmit}>
              {t("ui.storefront.user.login.SignIn")}
            </Button>
          </Modal.Footer>
        )}
      </Row>
    </Container>
  );
});
