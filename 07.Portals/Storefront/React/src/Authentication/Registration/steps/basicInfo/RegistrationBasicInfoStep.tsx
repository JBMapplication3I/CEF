import { useForm, useWatch } from "react-hook-form";
import { useEffect } from "react";
import { CEFConfig } from "../../../../_redux/_reduxTypes";
import {
  TextInputFormGroup,
  PasswordInputFormGroup
} from "../../../../_shared/forms/formGroups";
import { IRegistrationStepBasicInfoData } from "../../_registrationTypes";
import { Form, Row, Col, Button } from "react-bootstrap";
import cvApi from "../../../../_api/cvApi";

interface IRegistrationBasicInfoStepProps {
  cefConfig: CEFConfig;
  onCompleteBasicInfoStep: Function;
  continueText: string;
}

export const RegistrationBasicInfoStep = (
  props: IRegistrationBasicInfoStepProps
): JSX.Element => {
  const { onCompleteBasicInfoStep, continueText, cefConfig } = props;
  const userNameIsEmailEnabled: boolean =
    cefConfig.featureSet.registration.usernameIsEmail;

  const {
    register,
    handleSubmit,
    setError,
    setValue,
    control,
    formState: { errors }
  } = useForm({
    mode: "all",
    criteriaMode: "all",
    reValidateMode: "onBlur"
  });

  const boundUserName = useWatch({
    control,
    name: "emEmailRegistration",
    disabled: !userNameIsEmailEnabled,
    exact: true
  });

  useEffect(() => {
    if (userNameIsEmailEnabled) {
      setValue("emUsername", boundUserName);
    }
  }, [boundUserName]);

  const onSubmit = async (data: IRegistrationStepBasicInfoData) => {
    const r = await cvApi.authentication.ValidateEmailIsUnique({
      Email: data.emEmailRegistration
    });
    if (!r.data.ActionSucceeded) {
      setError("emEmailRegistration", {
        type: "uniqueEmail",
        message: "Account with that email already exists."
      });
      return;
    }
    onCompleteBasicInfoStep(data);
  };

  return (
    <Row as={Form} onSubmit={handleSubmit(onSubmit)} autoComplete="off">
      <Col xs={12}>
        <Row>
          <TextInputFormGroup
            register={register}
            errors={errors}
            formClasses="col-md-6 form-group"
            formIdentifier="firstNameRegistration"
            labelText="First Name"
            labelKey="ui.storefront.common.FirstName"
            placeholderText="First Name"
            placeholderKey="ui.storefront.common.FirstName"
            required={true}
            maxLength={128}
            maxLengthMessage="First name may not exceed 128 characters"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formClasses="col-md-6 form-group"
            formIdentifier="lastNameRegistration"
            labelText="Last Name"
            labelKey="ui.storefront.common.LastName"
            placeholderText="Last Name"
            placeholderKey="ui.storefront.common.LastName"
            required={true}
            maxLength={128}
            maxLengthMessage="Last name may not exceed 128 characters"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            isEmail={true}
            formClasses="col-12 form-group"
            formIdentifier="emEmailRegistration"
            labelText="Email"
            labelKey="ui.storefront.common.Email"
            placeholderText="Email"
            placeholderKey="ui.storefront.common.Email"
            required={true}
            maxLength={128}
            maxLengthMessage="Last name may not exceed 128 characters"
            pattern={
              /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
            }
            patternMessage="Email is not valid/formatted properly"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            isPhone={true}
            formClasses="col-12 form-group"
            formIdentifier="telPhoneRegistration"
            labelText="Phone"
            labelKey="ui.storefront.common.Phone"
            placeholderText="Phone"
            placeholderKey="ui.storefront.common.Phone"
            required={true}
            maxLength={40}
            maxLengthMessage="Phone numbers may not exceed 40 characters"
            minLength={10}
            minLengthMessage="Phone numbers must include country code and area code."
          />
        </Row>
      </Col>
      <Col xs={12}>
        <hr />
        <Row>
          <TextInputFormGroup
            register={register}
            errors={errors}
            isPhone={true}
            formClasses="col-md-6 form-group"
            formIdentifier="emUsername"
            labelText="Username"
            disabled={userNameIsEmailEnabled ? true : false}
            labelKey="ui.storefront.common.Username"
            placeholderText="Username"
            placeholderKey="ui.storefront.common.Username"
            required={true}
            maxLength={100}
            minLength={3}
          />
          <PasswordInputFormGroup
            register={register}
            errors={errors}
            formClasses="col-md-6 form-group"
            formIdentifier="pwPasswordRegistration"
            labelText="Password"
            labelKey="ui.storefront.common.Password"
            placeholderText="••••••"
            required={true}
            minLength={7}
          />
        </Row>
      </Col>
      <Col xs={12}>
        <Button
          as="input"
          variant="primary"
          className="my-3"
          value={continueText}
          type="submit"
        />
      </Col>
    </Row>
  );
};
