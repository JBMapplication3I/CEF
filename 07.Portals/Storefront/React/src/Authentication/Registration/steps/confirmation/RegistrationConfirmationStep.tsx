import { useForm } from "react-hook-form";
import { LoadingWidget } from "../../../../_shared/common/LoadingWidget";
import { CheckboxFormGroup } from "../../../../_shared/forms/formGroups";
import { Form, Row, Col, Button } from "react-bootstrap";
interface IRegistrationConfirmationStepProps {
  submitRegistrationForm: Function;
  parentRunning: boolean;
  continueText: string;
}

export const RegistrationConfirmationStep = (
  props: IRegistrationConfirmationStepProps
): JSX.Element => {
  const { submitRegistrationForm, parentRunning, continueText } = props;
  const {
    register,
    handleSubmit,
    formState: { errors }
  } = useForm({});

  const onSubmit = (_data: any) => {
    submitRegistrationForm();
  };

  return (
    <Form
      as={Row}
      className="position-relative"
      onSubmit={handleSubmit(onSubmit)}>
      {parentRunning ? <LoadingWidget overlay={true} /> : null}
      <Col xs={12}>
        <CheckboxFormGroup
          formIdentifier="ckAgreed"
          register={register}
          errors={errors}
          checkboxOptions={[
            {
              labelText: "I agree to the terms of use for this website",
              labelKey:
                "ui.storefront.user.registration.IAgreeToTheTermsOfUseForThisWebsite",
              required: true,
              requiredMessage: "You are required to agree to the terms",
              identifier: "ckAgreedTerms"
            }
          ]}
        />
      </Col>
      <Col xs={12}>
        <Button
          as="input"
          variant="primary"
          type="submit"
          className="my-3"
          title={continueText}
          id="btnSubmit_registrationStepConfirmation"
          name="btnSubmit_registrationStepConfirmation"
        />
      </Col>
    </Form>
  );
};
