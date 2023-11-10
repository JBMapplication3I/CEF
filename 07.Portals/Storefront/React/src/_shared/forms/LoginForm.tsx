import React, { useCallback, useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash, faQuestionCircle } from "@fortawesome/free-solid-svg-icons";
import { LoadingWidget } from "../common/LoadingWidget";
import axios from "../../axios";
import { ErrorView } from "../common/ErrorView";
import { connect } from "react-redux";
import { logUserIn } from "../../_redux/actions";
import { useViewState } from "../customHooks/useViewState";
import { useHistory } from "react-router";
import { useTranslation } from "react-i18next";
import { CEFConfig } from "../../_redux/_reduxTypes";
import cvApi from "../../_api/cvApi";
import { MFARequirementsModel, UserModel } from "../../_api/cvApi._DtoClasses";
import { CEFActionResponseT } from "../../_api/cvApi.shared";
import { IReduxStore } from "../../_redux/_reduxTypes";
import { Container, Row, Col, Form, InputGroup, Button, Modal } from "react-bootstrap";

interface ILoginFormProps {
  currentUser?: UserModel; // redux
  showCancel?: boolean;
  onCancel?: Function;
  onLoginSuccess?: Function;
  returnUrl?: string;
  cefConfig?: CEFConfig; // redux
}

interface ILoginData {
  Username?: string;
  Password?: string;
  MFAToken?: string;
}
interface IMapStateToLoginProps {
  currentUser: UserModel;
}

const mapStateToProps = (state: IReduxStore): IMapStateToLoginProps => {
  return {
    currentUser: state.currentUser
  };
};

export const LoginForm = connect(mapStateToProps)((props: ILoginFormProps): JSX.Element => {
  const { currentUser, showCancel, onCancel, onLoginSuccess, returnUrl, cefConfig } = props;
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const [showMFA, setShowMFA] = useState<boolean>(false);
  const [hideLogin, setHideLogin] = useState<boolean>(false);
  const [emailFirstAndLastFour, setEmailFirstAndLastFour] = useState<string>("");
  const [loginData, setLoginData] = useState<ILoginData>({
    Username: "",
    Password: "",
    MFAToken: ""
  });
  // const [rememberMe, setRememberMe] = useState<boolean>(false);
  const [error, setError] = useState<any>(null);
  const history = useHistory();
  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  const submit = (): void => {
    setRunning();
    axios
      .post("/auth/identity", {
        Username: loginData.Username,
        Password: loginData.Password + (loginData.MFAToken ?? "")
        //, RememberMe: rememberMe
      })
      .then((res: any) => {
        localStorage.setItem("user", JSON.stringify(res.data));
        return cvApi.contacts.GetCurrentUser();
      })
      .then((res: any) => {
        logUserIn(res.data);
        finishRunning();
        if (returnUrl) {
          history.push(returnUrl);
        } else if (onLoginSuccess) {
          onLoginSuccess();
        }
      })
      .catch((reason: any) => {
        let message = reason?.data?.ResponseStatus?.Message ?? reason?.data?.Message ?? reason;
        if (typeof message === "string" && message.indexOf("Log ID:") !== -1) {
          // Strip the log guid
          message = ("" + message).replace(/Log ID: (CEF: )?.{8}-.{4}-.{4}-.{4}-.{12} \| /g, "");
        }
        setError(message);
        finishRunning(true, message ?? "Failed to handle submit in Login");
      });
  };

  const handleCancel = (): void => {
    setLoginData({
      ...loginData,
      Password: ""
    });
    if (onCancel) {
      onCancel();
    }
  };

  const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    setLoginData({
      ...loginData,
      Username: e.target.value
    });
    if (error) {
      setError(null);
    }
  };

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    setLoginData({
      ...loginData,
      Password: e.target.value
    });
    if (error) {
      setError(null);
    }
  };

  const toggleShowPassword = (_e: React.MouseEvent<HTMLButtonElement>): void =>
    setShowPassword(!showPassword);

  const handleLoginSuccess = useCallback((): void => {
    if (onLoginSuccess) {
      onLoginSuccess();
    }
  }, [onLoginSuccess]);

  function handleSubmit(usePhone?: boolean, state?: string): void {
    setRunning(); // Finish Running will be called for either an error state or when the page reloads, etc.
    // consoleDebug("login.started");
    if (cefConfig?.authProviderMFAEnabled) {
      loginRequiresMFA(loginData).then((requires: CEFActionResponseT<MFARequirementsModel>) => {
        let mfaResult = requires.Result;
        if (mfaResult.Email && mfaResult.EmailFirstAndLastFour) {
          setEmailFirstAndLastFour(mfaResult.EmailFirstAndLastFour.replace(/\*/gi, "•"));
        }
        if (requires && (mfaResult.Phone || mfaResult.Email) && !showMFA) {
          setHideLogin(true);
          setShowMFA(true);
          finishRunning();
          return;
        }
        finishRunning();
        submit();
      });
    } else {
      finishRunning();
      submit();
    }
  }

  function requestMFA(loginData: ILoginData, usePhone: boolean): Promise<boolean> {
    if (!cefConfig.authProviderMFAEnabled) {
      return new Promise((resolve, reject) => resolve(false));
    }
    return new Promise((resolve, reject) => {
      cvApi.authentication
        .RequestMFAForUsername(loginData.Username, { UsePhone: usePhone })
        .then((r) => {
          if (!r || !r.data || !r.data.ActionSucceeded) {
            resolve(false);
            return;
          }
          resolve(true);
        });
    });
  }

  function loginRequiresMFA(
    loginData: ILoginData
  ): Promise<CEFActionResponseT<MFARequirementsModel>> {
    return new Promise((resolve, reject) => {
      cvApi.authentication.CheckForMFAForUsername(loginData.Username).then((r) => {
        if (!r || !r.data || !r.data.ActionSucceeded) {
          reject("ERROR! Something with the login process went wrong");
          return;
        }
        let mfaResult = r.data;
        resolve(mfaResult);
      });
    });
  }

  function requestMFATokenViaEmail(): void {
    setRunning();
    requestMFA(loginData, false).then(() => {
      setHideLogin(false);
      finishRunning();
    });
  }
  function requestMFATokenViaSMS(): void {
    setRunning();
    requestMFA(loginData, true).then(() => {
      setHideLogin(false);
      finishRunning();
    });
  }

  function keySubmit(e: React.KeyboardEvent<HTMLInputElement>): void {
    if (!e || !e.key || e.key !== "Enter") {
      return;
    }
    if (!loginData) {
      return;
    }
    handleSubmit(null, "home");
  }

  useEffect(() => {
    if (currentUser && currentUser.Contact && handleLoginSuccess) {
      handleLoginSuccess();
    }
    return () => {
      setLoginData({
        Username: "",
        Password: "",
        MFAToken: ""
      });
      setError(null);
    };
  }, [currentUser, handleLoginSuccess]);

  if (currentUser.Contact) {
    return (
      <p>
        {t("ui.storefront.user.login.YouAreCurrentlyLoggedInAs") + currentUser.Contact.FirstName}
      </p>
    );
  }

  return viewState.running ? (
    <LoadingWidget />
  ) : (
    <Container fluid>
      <Row className="form-vertical">
        <Col xs={12} className=" p-3 rounded">
          <Row>
            <Form.Group as={Col} xs={12} className="has-error">
              <Form.Label className="d-block text-left" htmlFor="username">
                <span>{t("ui.storefront.common.Username")}</span>
                <span className="text-danger"> *</span>
                <span className="text-info">
                  <FontAwesomeIcon icon={faQuestionCircle} />
                </span>
              </Form.Label>
              <InputGroup>
                <input
                  className="form-control"
                  value={loginData.Username}
                  type="text"
                  id="username"
                  required
                  data-interactive="true"
                  placeholder="Enter your username"
                  onChange={handleUsernameChange}
                />
              </InputGroup>
            </Form.Group>
            <Form.Group as={Col} xs={12} className="has-error">
              <Form.Label className="d-block text-left" htmlFor="password">
                <span>{t("ui.storefront.common.Password")}</span>
                <span className="text-danger"> *</span>
                <span className="text-info">
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
                  placeholder={t("ui.storefront.user.login.EnterYourPassword")}
                  value={loginData.Password}
                  onChange={handlePasswordChange}
                  onKeyDown={(e) => {
                    if (e.key === "Enter") {
                      handleSubmit();
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
              {showMFA ? (
                <>
                  <div className={`form-group col-12 my-2 ${error ? "has-error" : ""}`}>
                    <label className="control-label" htmlFor="txtMFAPrompt">
                      {t("ui.storefront.controls.login.MFARequired")}
                    </label>
                    <span className="d-block">{t("ui.storefront.controls.login.ReceiveCode")}</span>
                    <div className="button-group">
                      <button
                        type="button"
                        className="btn btn-primary"
                        id="mfaEmail"
                        name="mfaEmail"
                        onClick={() => requestMFATokenViaEmail()}>
                        Email:&nbsp;
                        <span>{emailFirstAndLastFour}</span>
                      </button>
                      {/* <button
                        type="button"
                        className="btn btn-primary"
                        id="mfaSMS"
                        name="mfaSMS"
                        ng-click="requestMFATokenViaSMS()">
                        SMS:&nbsp;&bull;&bull;&bull;-&bull;&bull;&bull;-
                        <span ng-bind="mfaResult.PhoneLastFour"></span>
                      </button> */}
                    </div>
                  </div>
                  <div className={`form-group col-12 my-2 ${error ? "has-error" : ""}`}>
                    <label
                      className="control-label"
                      htmlFor="txtMFAPrompt"
                      uib-tooltip="{{'ui.storefront.controls.login.MFAAuthToken.Tooltip'}}">
                      {t("ui.storefront.controls.login.AuthToken")}
                    </label>
                    <div className="input-group">
                      <input
                        type="text"
                        className={`form-control ${viewState.running ? "disabled" : ""}`}
                        id="txtMFAPrompt"
                        name="txtMFAPrompt"
                        autoComplete="off"
                        placeholder={t("ui.storefront.controls.login.EnterYourAuthToken")}
                        onChange={(e) => (loginData["MFAToken"] = e.target.value)}
                        onKeyDown={(e) => keySubmit(e)}
                      />
                      {/* <button
                        type="button"
                        className="btn btn-primary"
                        id="resend"
                        name="resend"
                        ng-click="submit('resend')">
                        Resend
                      </button> */}
                    </div>
                  </div>
                </>
              ) : null}
            </Form.Group>
          </Row>
          <ErrorView error={error && error.message} />
          <Row className="align-items-center">
            <Col className="text-right">
              <small>
                <a
                  href="/authentication/forgot-password"
                  id="btnLoginForgotPassword"
                  onClick={handleCancel}>
                  {t("ui.storefront.user.login.ForgotYourPassword")}
                </a>
                <br />
                <span>{t("ui.storefront.user.login.DontHaveAnAccount")}</span>
                &nbsp;
                <a href="/Authentication/Registration" onClick={handleCancel}>
                  {t("ui.storefront.common.RegisterForOne")}
                </a>
              </small>
            </Col>
          </Row>
        </Col>
        <Modal.Footer>
          {showCancel && (
            <Button variant="outline-secondary" onClick={handleCancel}>
              {t("ui.storefront.common.Close")}
            </Button>
          )}
          <Button variant="primary" onClick={() => handleSubmit()}>
            {t("ui.storefront.user.login.SignIn")}
          </Button>
        </Modal.Footer>
      </Row>
    </Container>
  );
});
