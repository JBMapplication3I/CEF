import { useForm } from "react-hook-form";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUndo } from "@fortawesome/free-solid-svg-icons";
import { faSave } from "@fortawesome/free-regular-svg-icons";
import { Modal } from "../../_shared/modals";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { TextInputFormGroup } from "../../_shared/forms/formGroups";
import { useTranslation } from "react-i18next";
import { Form, Card, Col, Button, ButtonGroup } from "react-bootstrap";

interface IWalletEntryData {
  txtWalletCardNumber: string;
  txtWalletCardNickname: string;
  txtWalletCardHolderName: string;
  ddlWalletExpirationMonth: string;
  ddlWalletExpirationYear: string;
}

const monthsAbbreviated = [
  "Jan",
  "Feb",
  "Mar",
  "Apr",
  "May",
  "Jun",
  "Jul",
  "Aug",
  "Sep",
  "Oct",
  "Nov",
  "Dec"
];

const thisYear = new Date().getFullYear();

const availableExpirationYears: number[] = [];
for (let i = 0; i < 11; i++) {
  availableExpirationYears.push(thisYear + i);
}

interface INewWalletModalProps {
  onConfirm: Function;
  onCancel: Function;
  show: boolean;
  loading?: boolean;
}

export const NewWalletModal = (props: INewWalletModalProps): JSX.Element => {
  const { t } = useTranslation();
  const { onConfirm, onCancel, show, loading } = props;
  const {
    register,
    handleSubmit,
    setValue,
    clearErrors,
    reset,
    formState: { errors }
  } = useForm({
    mode: "all",
    reValidateMode: "onBlur"
  });

  function handleConfirmButtonClicked(data: IWalletEntryData) {
    const formatted = {
      CreditCardNumber: data.txtWalletCardNumber,
      CardNickName: data.txtWalletCardNickname,
      CardHolderName: data.txtWalletCardHolderName,
      ExpirationMonth: monthsAbbreviated.indexOf(data.ddlWalletExpirationMonth),
      ExpirationYear: data.ddlWalletExpirationYear
    };
    if (onConfirm) {
      onConfirm(formatted);
      for (const key in data) {
        setValue(key, "");
      }
    }
  }

  function handleCancel() {
    onCancel();
    reset();
    clearErrors();
  }

  return (
    <Modal
      title="Add new Wallet Item"
      show={show}
      loading={loading}
      onCancel={handleCancel}>
      <Col className="mb-3">
        {/* TODO: replace this form with NewWalletForm */}
        <Card
          as={Form}
          className="form-vertical wallet-card-panel h-100"
          onSubmit={handleSubmit(handleConfirmButtonClicked)}>
          {loading ? (
            <LoadingWidget />
          ) : (
            <Card.Body className="p-3">
              <TextInputFormGroup
                register={register}
                formIdentifier="txtWalletCardNickname"
                errors={errors}
                formClasses="mb-2"
                required={true}
                labelKey="ui.storefront.wallet.walletCard.CardNickname"
                placeholderKey="ui.storefront.wallet.walletCard.CardNickname"
                maxLength={128}
              />
              <TextInputFormGroup
                register={register}
                formIdentifier="txtWalletCardHolderName"
                errors={errors}
                formClasses="mb-2"
                required={true}
                labelKey="ui.storefront.wallet.walletCard.CardHolderName"
                placeholderKey="ui.storefront.wallet.walletCard.CardHolderName"
                maxLength={128}
              />
              <TextInputFormGroup
                register={register}
                formIdentifier="txtWalletCardNumber"
                errors={errors}
                formClasses="mb-2"
                required={true}
                labelKey="ui.storefront.wallet.walletCard.CardNumber"
                maxLength={19}
                minLength={19}
                pattern={/^[\d+\s]*$/}
                patternMessage="Card number may only contain digits and spaces"
                placeholderText="1234 5678 9012 3456"
                onKeyDown={(e) => {
                  const backspacePressed = e.key !== "Backspace";
                  const currentValue = e.currentTarget.value;
                  if (backspacePressed) {
                    const lengthNoSpaces = currentValue.replace(
                      / /g,
                      ""
                    ).length;
                    if (
                      lengthNoSpaces !== 0 &&
                      lengthNoSpaces % 4 === 0 &&
                      currentValue.length < 17
                    ) {
                      setValue("txtWalletCardNumber", currentValue + " ");
                    }
                  } else {
                    if (currentValue[currentValue.length - 2] === " ") {
                      setValue(
                        "txtWalletCardNumber",
                        currentValue.substr(0, currentValue.length - 1)
                      );
                    }
                  }
                }}
              />
              <div className="mb-2">
                <Form.Label htmlFor="txtWalletCardHolderName">
                  <span>{t("ui.storefront.wallet.walletCard.Expiration")}</span>
                  <span className="text-danger">&nbsp;*</span>
                </Form.Label>
                <div className="d-flex align-items-center">
                  <Form.Select
                    className="mr-1 custom-select"
                    id="ddlWalletExpirationMonth"
                    {...register("ddlWalletExpirationMonth")}>
                    {monthsAbbreviated.map((month) => {
                      return <option key={month}>{month}</option>;
                    })}
                  </Form.Select>
                  <Form.Select
                    className="ml-1 custom-select"
                    id="ddlWalletExpirationYear"
                    {...register("ddlWalletExpirationYear")}>
                    {availableExpirationYears.map((year) => {
                      return <option key={year.toString()}>{year}</option>;
                    })}
                  </Form.Select>
                </div>
              </div>
            </Card.Body>
          )}
          <ButtonGroup as={Card.Footer} className="p-0">
            <Button
              variant="link"
              size="lg"
              id="btnWalletSave"
              type="submit"
              disabled={loading}
              title="Save"
              name="btnWalletSave">
              <FontAwesomeIcon icon={faSave} />
              <span className="ml-1">{t("ui.storefront.common.Save")}</span>
            </Button>
            <Button
              variant="link"
              size="lg"
              id="btnWalletCancel"
              name="btnWalletCancel"
              title="Cancel"
              aria-label="Cancel"
              onClick={() => handleCancel()}>
              <FontAwesomeIcon icon={faUndo} aria-hidden="true" />
              <span className="ml-1">{t("ui.storefront.common.Cancel")}</span>
            </Button>
          </ButtonGroup>
        </Card>
      </Col>
    </Modal>
  );
};
