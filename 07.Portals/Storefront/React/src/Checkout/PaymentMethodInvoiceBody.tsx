import { useForm } from "react-hook-form";
import { IPaymentMethodInvoiceBodyProps, IInvoiceFormData } from "./_checkoutTypes";
import { TextInputFormGroup } from "../_shared/forms/formGroups";
import { FileUploadWidget } from "../_shared/common/FileUploadWidget";
import { useTranslation } from "react-i18next";
import { Form, Row, Col, Button } from "react-bootstrap";

const thisYear = new Date().getFullYear();

const availableExpirationYears: number[] = [];
for (let i = 0; i < 11; i++) {
  availableExpirationYears.push(thisYear + i);
}

export const PaymentMethodInvoiceBody = (props: IPaymentMethodInvoiceBodyProps): JSX.Element => {
  const { onSubmit, continueText, submitDisabled } = props;

  const { t } = useTranslation();

  const {
    register,
    handleSubmit,
    formState: { errors }
  } = useForm({
    reValidateMode: "onBlur"
  });

  const onSubmitInvoiceForm = (data: IInvoiceFormData): void => {
    const { txtPONumber } = data;
    onSubmit({
      CustomerPurchaseOrderNumber: txtPONumber
    });
  };

  return (
    <Row as={Form} className="mb-3" onSubmit={handleSubmit(onSubmitInvoiceForm)}>
      <Col xs={12}>
        <Row className="mb-3">
          <Col xs={12}>
            <TextInputFormGroup
              formIdentifier="txtPONumber"
              register={register}
              errors={errors}
              labelText="PO Number"
              labelKey="ui.storefront.checkout.views.paymentInformation.poNumber"
              required={true}
              requiredMessage="Purchase Order Number is required"
              placeholderText="Enter your purchase order or reference number"
              placeholderKey="ui.storefront.checkout.views.paymentInformation.enterYourPurchaseOrderOrReferenceNumber"
            />
            <FileUploadWidget />
          </Col>
        </Row>
        <Row>
          <Col xs={12}>
            <Button
              variant="primary"
              className={`my-3`}
              disabled={submitDisabled}
              id="btnSubmit_purchaseStepPayment"
              title="Confirm Order and Purchase"
              type="submit">
              {continueText ??
                t("ui.storefront.checkout.views.paymentInformation.confirmOrderAndPurchase")}
            </Button>
          </Col>
        </Row>
      </Col>
    </Row>
  );
};
