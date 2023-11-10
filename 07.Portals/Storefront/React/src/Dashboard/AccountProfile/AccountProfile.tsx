import { useEffect, useState } from "react";
import { useForm, useWatch } from "react-hook-form";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import cvApi from "../../_api/cvApi";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { useTranslation } from "react-i18next";
import { TextInputFormGroup } from "../../_shared/forms/formGroups";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { UpdateCurrentAccountDto } from "../../_api/cvApi.Accounts";
import { AccountModel } from "../../_api/cvApi._DtoClasses";
import { Alert, Button, Col, Row, Form } from "react-bootstrap";

interface IAccountProfileEntryData {
  txtAccountName: string;
  taAccountDescription: string;
  txtAccountCustomKey: string;
  txtTaxExemptionNo: string;
  txtTaxEntityUseCode: string;
}

export const AccountProfile = () => {
  const [currentAccount, setCurrentAccount] = useState({} as AccountModel);
  const [showSavedMessage, setShowSavedMessage] = useState(false);

  const {
    register,
    handleSubmit,
    setValue,
    control,
    formState: { errors }
  } = useForm();

  const { t } = useTranslation();

  const formFieldsWatch = useWatch({ control });

  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    if (showSavedMessage) {
      setShowSavedMessage(false);
    }
  }, [formFieldsWatch]);

  useEffect(() => {
    getCurAccount();
  }, []);

  function getCurAccount(): void {
    setRunning();
    cvApi.accounts
      .GetCurrentAccount()
      .then((res: any) => {
        const { Name, CustomKey, Description, TaxExemptionNo, TaxEntityUseCode } = res.data;
        setCurrentAccount(res.data);
        setValue("txtAccountName", Name);
        setValue("taAccountDescription", Description);
        setValue("txtAccountCustomKey", CustomKey);
        setValue("txtTaxExemptionNo", TaxExemptionNo);
        setValue("txtTaxEntityUseCode", TaxEntityUseCode);
        finishRunning(false);
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to get current account");
      });
  }

  function updateCurAccount(data: IAccountProfileEntryData): void {
    const {
      txtAccountName,
      taAccountDescription,
      txtAccountCustomKey,
      txtTaxExemptionNo,
      txtTaxEntityUseCode
    } = data;
    const toSend = {
      ...currentAccount,
      Name: txtAccountName,
      CustomKey: txtAccountCustomKey,
      Description: taAccountDescription,
      TaxExemptionNo: txtTaxExemptionNo,
      TaxEntityUseCode: txtTaxEntityUseCode,
      UpdatedDate: new Date()
    } as UpdateCurrentAccountDto;
    setRunning();
    cvApi.accounts
      .UpdateCurrentAccount(toSend)
      .then((_res: any) => {
        setShowSavedMessage(true);
        finishRunning(false);
        getCurAccount();
      })
      .catch((err: any) => {
        finishRunning(true, err.message || "Failed to update current account");
      });
  }

  return (
    <Form as={Row} className="position-relative" onSubmit={handleSubmit(updateCurAccount)}>
      <Col xs="12">
        <h1>{t("ui.storefront.userDashboard2.userDashboard.AccountProfile")}</h1>
      </Col>
      {viewState.running ? <LoadingWidget overlay={true} /> : null}
      <Col sm="6">
        <Row>
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtAccountName"
            formClasses="col-12 mb-2"
            labelKey="ui.storefront.common.CompanyName"
            required={true}
            requiredMessage={t("ui.storefront.common.validation.ThisFieldIsRequired")}
            maxLength={128}
            maxLengthMessage="Account name may not exceed 128 characters"
            placeholderKey="ui.storefront.userDashboard2.controls.accountProfile.EnterYourCompanyName"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtAccountCustomKey"
            formClasses="col-12 mb-2"
            labelKey="ui.storefront.common.Key"
            labelText="Key"
            required={true}
            disabled={true}
            requiredMessage={t("ui.storefront.common.validation.ThisFieldIsRequired")}
            maxLength={128}
            maxLengthMessage="Account custom key may not exceed 128 characters"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="taAccountDescription"
            formClasses="col-12 mb-2"
            labelKey="ui.storefront.userDashboard2.controls.accountProfile.AccountDescription"
            placeholderKey="ui.storefront.userDashboard2.controls.accountProfile.DescribeYourAccount"
            required={false}
            maxLength={600}
            maxLengthMessage="Company description may not exceed 600 characters"
          />
        </Row>
      </Col>
      <Col sm="6">
        <Row>
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtTaxExemptionNo"
            formClasses="col-12 mb-2"
            maxLength={12}
            maxLengthMessage="Tax Exemption Number may not exceed 12 characters"
            labelKey="ui.storefront.userDashboard2.controls.accountProfile.TaxExemptionNo"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtTaxEntityUseCode"
            formClasses="col-12 mb-2"
            labelKey="ui.storefront.userDashboard2.controls.accountProfile.TaxEntityUseCode"
            maxLength={128}
            maxLengthMessage="Tax Entity Code may not exceed 128 characters"
          />
        </Row>
      </Col>
      <hr />
      <Col xs="12">
        <Row>
          <Col xs="auto">
            <Button variant="primary" type="submit" className="btn-lg">
              {t("ui.storefront.common.Save")}
            </Button>
          </Col>
          {showSavedMessage && (
            <Col>
              <Alert variant="success" className="p-2 ml-3 mb-3">
                <FontAwesomeIcon icon={faCheckCircle} />
                &nbsp;
                <span>{t("ui.storefront.userDashboard2.controls.userProfile.SaveSuccessful")}</span>
              </Alert>
            </Col>
          )}
          {viewState.hasError && (
            <Col>
              <Alert variant="danger" className="p-2 ml-3 mb-3">
                <FontAwesomeIcon icon={faCheckCircle} />
                &nbsp;
                <span>
                  {t("ui.storefront.common.Error")}: {viewState.errorMessage}
                </span>
              </Alert>
            </Col>
          )}
        </Row>
      </Col>
    </Form>
  );
};
