import React from "react";
import { useForm } from "react-hook-form";
import { SelectFormGroup, TextInputFormGroup } from "./formGroups";
import { useTranslation } from "react-i18next";
import { Form, Row, Col, Button } from "react-bootstrap";
interface INewWalletFormData {
  txtCVV: string;
  txtCardHolderName: string;
  txtCardNumber: string;
  txtReferenceName: string;
  ddlExpirationMonth: string;
  ddlExpirationYear: string;
}

interface INewWalletFormProps {
  onConfirm: (data: INewWalletFormConfirmData) => void;
  onCancel?: Function;
  showCancel?: boolean;
}

export interface INewWalletFormConfirmData {
  CardNickName: string;
  CreditCardNumber: string;
  CreditCardCVV: string;
  CardHolderName: string;
  ExpirationMonth: number;
  ExpirationYear: string;
}

const thisYear = new Date().getFullYear();

const availableExpirationYears: number[] = [];
for (let i = 0; i < 11; i++) {
  availableExpirationYears.push(thisYear + i);
}

export const NewWalletForm = (props: INewWalletFormProps): JSX.Element => {
  const { onConfirm, showCancel, onCancel } = props;

  const { t } = useTranslation();

  const {
    register,
    handleSubmit,
    reset,
    clearErrors,
    formState: { errors }
  } = useForm();

  const handleConfirm = (data: INewWalletFormData): void => {
    const {
      txtCVV,
      txtCardHolderName,
      txtCardNumber,
      txtReferenceName,
      ddlExpirationMonth,
      ddlExpirationYear
    } = data;
    const userWalletData: INewWalletFormConfirmData = {
      CardNickName: txtReferenceName,
      CreditCardNumber: txtCardNumber,
      CreditCardCVV: txtCVV,
      CardHolderName: txtCardHolderName,
      ExpirationMonth: +ddlExpirationMonth,
      ExpirationYear: ddlExpirationYear
    };
    onConfirm(userWalletData);
  };

  const handleCancel = (_e: React.MouseEvent<HTMLButtonElement>) => {
    if (onCancel) {
      onCancel();
    }
    reset();
    clearErrors();
  };

  return (
    <Form
      className="position-relative"
      autoComplete="off"
      onSubmit={handleSubmit(handleConfirm)}>
      <Row>
        <TextInputFormGroup
          register={register}
          errors={errors}
          formIdentifier="txtReferenceName"
          formClasses="col-12"
          labelKey="ui.storefront.common.CardNickname"
          maxLength={128}
        />
        <Col xs={12}>
          <p className="small">
            {t("ui.storefront.common.ECheckAccountNickname.Message")}
          </p>
        </Col>
      </Row>
      <Row>
        <TextInputFormGroup
          register={register}
          errors={errors}
          formClasses="col-12"
          formIdentifier="txtCardHolderName"
          labelKey="ui.storefront.checkout.creditCard.cardholder"
          required={true}
          maxLength={128}
        />
      </Row>
      <Row>
        <TextInputFormGroup
          register={register}
          errors={errors}
          formIdentifier="txtCardNumber"
          formClasses="col-12"
          labelKey="ui.storefront.checkout.views.paymentInformation.cardNumber"
          required={true}
          minLength={15}
          maxLength={16}
          pattern={/[0-9]+/}
          patternMessage="Credit card may only contain numbers"
          placeholderText="1234 5678 9012 3456"
        />
      </Row>
      <Row>
        <SelectFormGroup
          register={register}
          errors={errors}
          formIdentifier="ddlExpirationMonth"
          formClasses="col-12 col-sm-6"
          labelKey="ui.storefront.common.Month"
          includeNull={true}
          nullKey="ui.storefront.common.Month"
          required={true}
          options={[
            { option: "January", value: "01" },
            { option: "February", value: "02" },
            { option: "March", value: "03" },
            { option: "April", value: "04" },
            { option: "May", value: "05" },
            { option: "June", value: "06" },
            { option: "July", value: "07" },
            { option: "August", value: "08" },
            { option: "September", value: "09" },
            { option: "October", value: "10" },
            { option: "November", value: "11" },
            { option: "December", value: "12" }
          ]}
        />
        <SelectFormGroup
          register={register}
          errors={errors}
          formIdentifier="ddlExpirationYear"
          formClasses="col-6 col-sm-3"
          labelKey="ui.storefront.common.Year"
          required={true}
          includeNull={true}
          nullKey="ui.storefront.common.Year"
          options={availableExpirationYears.map((year: number) => ({
            option: year.toString()
          }))}
        />
        <TextInputFormGroup
          register={register}
          errors={errors}
          formIdentifier="txtCVV"
          formClasses="col-6 col-sm-3"
          labelKey="ui.storefront.wallet.walletCard.CVVAVS"
          required={true}
          placeholderText="123"
          pattern={/^\d{3,4}$/}
          patternMessage="CVV must be 3 or 4 numbers"
        />
      </Row>
      <Row className="mt-2">
        <Col xs={12}>
          <div className="d-flex justify-content-end gap-2">
            {showCancel ? (
              <Button variant="secondary" onClick={handleCancel}>
                {t("ui.storefront.common.Cancel")}
              </Button>
            ) : null}
            <Button
              variant="primary"
              type="submit"
              id={`btnAddWallet`}
              name={`btnAddWallet`}>
              {t(
                "ui.storefront.checkout.splitShipping.addressModal.AddAddress"
              )}
            </Button>
          </div>
        </Col>
      </Row>
    </Form>
  );
};
