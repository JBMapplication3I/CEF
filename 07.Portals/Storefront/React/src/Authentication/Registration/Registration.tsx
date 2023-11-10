import { Fragment, useEffect, useState } from "react";
import axios from "../../axios";
import cvApi from "../../_api/cvApi";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import { logUserIn } from "../../_redux/actions/userActions";
import { RegisterNewUserDto } from "../../_api/cvApi.Authentication";
import { RegistrationStepsHeader } from "./steps/_shared/RegistrationStepsHeader";
import { useTranslation } from "react-i18next";
import { useViewState } from "../../_shared/customHooks/useViewState";
import {
  IRegistrationStep,
  registrationStepFactory
} from "./steps/registrationSteps";
import { ServiceStrings } from "../../_shared/ServiceStrings";
// import { IPurchaseStep, purchaseStepFactory } from "./steps/registrationSteps";
import { RegistrationBasicInfoStep } from "./steps/basicInfo/RegistrationBasicInfoStep";
import { RegistrationAddressBookStep } from "./steps/addressBook/RegistrationAddressBookStep";
import { RegistrationWalletStep } from "./steps/wallet/RegistrationWalletStep";
import { RegistrationCustomStep } from "./steps/custom/RegistrationCustomStep";
import { RegistrationConfirmationStep } from "./steps/confirmation/RegistrationConfirmationStep";
import { RegistrationCompletedStep } from "./steps/completed/RegistrationCompletedStep";
import {
  IMapStateToRegistrationProps,
  IRegistrationProps,
  IRegistrationStepBasicInfoData
} from "./_registrationTypes";
import { INewAddressFormCallbackData } from "../../_shared/forms/NewAddressForm";
import { IReduxStore } from "../../_redux/_reduxTypes";
import { Container, Row, Col } from "react-bootstrap";

const mapStateToProps = (state: IReduxStore): IMapStateToRegistrationProps => {
  return {
    cart: state.cart,
    cefConfig: state.cefConfig,
    currentAccount: state.currentAccount,
    currentAccountAddressBook: state.currentAccountAddressBook,
    currentUser: state.currentUser
  };
};

export const Registration = connect(mapStateToProps)((props: IRegistrationProps): JSX.Element => {
  const { cefConfig, currentUser } = props;

  const [allSteps, setAllSteps] = useState<IRegistrationStep[]>(null);
  const [step, setStep] = useState(currentUser.ID ? 4 : 1);
  const [basicInfoStepData, setStepBasicInfoStepData] =
    useState<IRegistrationStepBasicInfoData>({
      firstNameRegistration: "",
      lastNameRegistration: "",
      emEmailRegistration: "",
      pwPasswordRegistration: ""
    });
  const [stepAddressBookBillingData, setStepAddressBookBillingData] =
    useState<INewAddressFormCallbackData>(null);
  const [stepAddressBookShippingData, setStepAddressBookShippingData] =
    useState<INewAddressFormCallbackData>(null);
  const [stepWalletData, setStepWalletData] = useState<any>();
  const [stepCustomData, setStepCustomData] = useState<any>();

  const { setRunning, finishRunning, viewState } = useViewState();
  const { t } = useTranslation();

  useEffect(() => {
    if (!currentUser.ID && step !== 1) {
      setStep(1);
    }
  }, [currentUser]);

  const proceed = (): void => {
    setStep((s) => s + 1);
  };

  const onCompleteBasicInfoStep = (
    data: IRegistrationStepBasicInfoData
  ): void => {
    setStepBasicInfoStepData(data);
    proceed();
  };

  const onCompleteAddressBookStep = (
    billingAddress: INewAddressFormCallbackData,
    shippingAddress: INewAddressFormCallbackData
  ): void => {
    setStepAddressBookBillingData(billingAddress);
    setStepAddressBookShippingData(shippingAddress);
    proceed();
  };

  const onCompleteWalletStep = (walletData: any): void => {
    setStepWalletData(walletData);
    proceed();
  };

  const onCompleteCustomStep = (customData: any): void => {
    setStepCustomData(customData);
    proceed();
  };

  const submitRegistrationForm = () => {
    setRunning();
    cvApi.authentication
      .RegisterNewUser({
        AccessFailedCount: 0,
        Active: true,
        ID: null,
        ContactID: null,
        Contact: {
          ID: null,
          Active: true,
          Address: {
            Active: true,
            City: stepAddressBookShippingData.City,
            Company: stepAddressBookShippingData.Company,
            Country: stepAddressBookShippingData.Country,
            CountryCode: stepAddressBookShippingData.Country.Code,
            CountryID: stepAddressBookShippingData.Country.ID,
            CountryKey: stepAddressBookShippingData.Country.CustomKey,
            CountryName: stepAddressBookShippingData.Country.Name,
            CreatedDate: new Date(),
            CustomKey: null,
            ID: null,
            PostalCode: stepAddressBookShippingData.PostalCode,
            Street1: stepAddressBookShippingData.Street1
          },
          AddressID: null,
          AddressKey: stepAddressBookShippingData.CustomKey,
          CreatedDate: new Date(),
          CustomKey: basicInfoStepData.emEmailRegistration,
          Email1: basicInfoStepData.emEmailRegistration,
          FirstName: basicInfoStepData.firstNameRegistration,
          LastName: basicInfoStepData.lastNameRegistration,
          Phone1: stepAddressBookShippingData.Phone1,
          SameAsBilling: true,
          TypeID: 1,
          TypeKey: "General"
        },
        CreatedDate: new Date(),
        CustomKey: basicInfoStepData.emEmailRegistration,
        Email: basicInfoStepData.emEmailRegistration,
        EmailConfirmed: false,
        IsApproved: false,
        IsCatalogSubscriber: false,
        IsDeleted: false,
        IsEmailSubscriber: false,
        IsSMSAllowed: false,
        IsSuperAdmin: false,
        LockoutEnabled: false,
        OverridePassword: basicInfoStepData.pwPasswordRegistration,
        Password: basicInfoStepData.pwPasswordRegistration,
        PhoneNumberConfirmed: false,
        RequirePasswordChangeOnNextLogin: false,
        SerializableAttributes: {},
        Status: null,
        StatusID: null,
        StatusKey: "Registered",
        StatusName: null,
        TwoFactorEnabled: false,
        TypeID: null,
        TypeKey: null,
        TypeName: "Customer",
        UserName: basicInfoStepData.emEmailRegistration,
        InService: null,
        UseAutoPay: null
      } as RegisterNewUserDto)
      .then((res) => {
        return axios.post("/auth/identity", {
          Password: basicInfoStepData.pwPasswordRegistration,
          Username: basicInfoStepData.emEmailRegistration,
          RememberMe: true
        });
      })
      .then((res) => {
        localStorage.setItem("user", JSON.stringify(res.data));
        return cvApi.contacts.GetCurrentUser();
      })
      .then((res) => {
        proceed();
        logUserIn(res.data);
        finishRunning();
      })
      .catch(console.log);
  };

  useEffect(() => {
    if (!cefConfig || allSteps) {
      return;
    }
    const stepsToShow = registrationStepFactory(cefConfig);
    const fullSteps = stepsToShow.map((step, index) => {
      const continueText = t(stepsToShow[index + 1]?.continueTextKey ?? "");
      return {
        ...step,
        continueText
      };
    });
    setAllSteps(fullSteps);
  }, [cefConfig]);

  return (
    <Container>
      <Row>
        <Col xs={12} className="text-center">
          <h1 className="mb-2">
            <span>{t("ui.storefront.user.registration.RegisterWith")}</span>{" "}
            <span>{cefConfig?.companyName}</span>
          </h1>
          <p>
            <span>
              {t(
                "ui.storefront.user.registration.YourPrivacyIsImportantToUsPleaseReadOur"
              )}
            </span>
            &nbsp;
            <Link to="/privacy-statement">
              <span>
                {t("ui.storefront.user.registration.PrivacyStatement")}
              </span>
            </Link>
            &nbsp;
            <span>
              {t(
                "ui.storefront.user.registration.RegardingTheFullTermsOfWhyWeCollectThisDataAndHowYourInformationWillBeUsed"
              )}
            </span>
          </p>
        </Col>
        <Col xs={12}>
          {allSteps &&
            allSteps.map((stepData, index) => {
              const { continueText, titleKey: title } = stepData;
              let component = null;
              switch (stepData.name) {
                case ServiceStrings.registration.steps.basicInfo:
                  component = (
                    <RegistrationBasicInfoStep
                      key={stepData.name}
                      cefConfig={cefConfig}
                      continueText={continueText}
                      onCompleteBasicInfoStep={onCompleteBasicInfoStep}
                    />
                  );
                  break;
                case ServiceStrings.registration.steps.addressBook:
                  component = (
                    <RegistrationAddressBookStep
                      key={stepData.name}
                      continueText={continueText}
                      onCompleteAddressBookStep={onCompleteAddressBookStep}
                    />
                  );
                  break;
                case ServiceStrings.registration.steps.wallet:
                  component = (
                    <RegistrationWalletStep
                      key={stepData.name}
                      onCompleteWalletStep={onCompleteWalletStep}
                      continueText={continueText}
                    />
                  );
                  break;
                case ServiceStrings.registration.steps.custom:
                  component = (
                    <RegistrationCustomStep
                      key={stepData.name}
                      onCompleteCustomStep={onCompleteCustomStep}
                      continueText={continueText}
                    />
                  );
                  break;
                case ServiceStrings.registration.steps.confirmation:
                  component = (
                    <RegistrationConfirmationStep
                      key={stepData.name}
                      continueText={continueText}
                      submitRegistrationForm={submitRegistrationForm}
                      parentRunning={viewState.running}
                    />
                  );
                  break;
                case ServiceStrings.registration.steps.completed:
                  component = (
                    <RegistrationCompletedStep
                      key={stepData.name}
                      emailRegisteredWith={
                        basicInfoStepData ? basicInfoStepData.emEmailRegistration : ""
                      }
                    />
                  );
                  break;
                default:
                  console.log("Failed to find step for registration in cefConfig");
                  break;
              }
              return (
                <Fragment key={title}>
                  <Row>
                    <Col xs={12} sm="auto">
                      <RegistrationStepsHeader
                        valid={step >= index + 2 || !!currentUser.ID}
                        titleKey={title}
                      />
                      <hr />
                    </Col>
                  </Row>
                  {step === index + 1 && component}
                </Fragment>
              );
            })}
        </Col>
      </Row>
    </Container>
  );
});
