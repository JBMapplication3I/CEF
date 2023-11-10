import { useEffect, useState } from "react";
import axios from "../../axios";
import { useForm, useWatch } from "react-hook-form";
import cvApi from "../../_api/cvApi";
import {
  PasswordInputFormGroup,
  TextInputFormGroup
} from "../../_shared/forms/formGroups";
import { useTranslation } from "react-i18next";
import { UserModel } from "../../_api/cvApi._DtoClasses";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { useViewState } from "../../_shared/customHooks/useViewState";
import { Alert, Col, Row, Button, Form } from "react-bootstrap";
import { faCheckCircle } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface IMyProfileDataEntry {
  txtProfileDisplayName: string;
  txtProfileEmail1: string;
  txtProfileFax1: string;
  txtProfileFirstName: string;
  txtProfileLastName: string;
  txtProfilePasswordCurrent: string;
  txtProfilePasswordNew: string;
  txtProfilePhone1: string;
}

export const MyProfile = () => {
  const [userName, setUserName] = useState<string>("");
  const [showSavedMessage, setShowSavedMessage] = useState<boolean>(false);
  const [currentUser, setCurrentUser] = useState<UserModel>(null);

  const { setRunning, finishRunning, viewState } = useViewState();

  const {
    register,
    handleSubmit,
    setValue,
    control,
    formState: { errors }
  } = useForm({
    criteriaMode: "all"
  });
  const { t } = useTranslation();

  const formFieldsWatch = useWatch({
    control
  });

  useEffect(() => {
    if (showSavedMessage) {
      setShowSavedMessage(false);
    }
  }, [formFieldsWatch]);

  useEffect(() => {
    getCurrentUserProfileInformation();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  function getCurrentUserProfileInformation() {
    setRunning();
    cvApi.contacts
      .GetCurrentUser()
      .then((result) => {
        if (!result || !result.data) {
          finishRunning(true, "Could not find current user");
          return;
        }
        setCurrentUser(result.data);
        const { DisplayName, UserName } = result.data;
        const { FirstName, LastName, FullName, Email1, Phone1, Fax1 } =
          result.data.Contact;
        if (FirstName && LastName) {
          setValue("txtProfileFirstName", FirstName);
          setValue("txtProfileLastName", LastName);
        } else {
          const nameSegments = FullName.split(" ");
          setValue("txtProfileFirstName", nameSegments[0]);
          setValue("txtProfileLastName", nameSegments[nameSegments.length - 1]);
        }
        setValue("txtProfileEmail1", Email1);
        setValue("txtProfilePhone1", Phone1);
        setValue("txtProfileFax1", Fax1);
        setValue("txtProfileDisplayName", DisplayName);
        setUserName(UserName);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  function handleSaveButtonClicked(data: IMyProfileDataEntry): void {
    setRunning();
    const {
      txtProfileDisplayName,
      txtProfileEmail1,
      txtProfileFax1,
      txtProfileFirstName,
      txtProfileLastName,
      txtProfilePasswordCurrent,
      txtProfilePasswordNew,
      txtProfilePhone1
    } = data;
    cvApi.authentication
      .ValidatePassword({
        UserName: userName,
        Password: txtProfilePasswordCurrent
      })
      .then((result) => {
        if (!result || !result.data) {
          alert("Save unsuccessful");
          return new Promise((resolve, reject) => {
            reject();
          });
        }
        const passwordIsValid = result.data.ActionSucceeded;
        if (!passwordIsValid) {
          alert("Password invalid");
          return new Promise((resolve, reject) => {
            reject();
          });
        }
        if (txtProfilePasswordNew.length) {
          return cvApi.authentication.ChangePassword({
            UserName: userName,
            Password: txtProfilePasswordCurrent,
            NewPassword: txtProfilePasswordNew
          });
        } else {
          return Promise.resolve(true);
        }
      })
      .then((_res) => {
        if (currentUser) {
          return axios.put("/Contacts/CurrentUser/Update", {
            ...currentUser,
            displayName: txtProfileDisplayName,
            Contact: {
              ...currentUser.Contact,
              FirstName: txtProfileFirstName,
              LastName: txtProfileLastName,
              Email1: txtProfileEmail1,
              Fax1: txtProfileFax1,
              Phone1: txtProfilePhone1
            }
          });
        } else {
          return new Promise((resolve) => resolve(true));
        }
      })
      .then(() => {
        setShowSavedMessage(true);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  }

  return (
    <Form as={Row} className="position-relative">
      {viewState.running ? <LoadingWidget overlay={true} /> : null}
      <Col xs="12">
        <h1>{t("ui.storefront.userDashboard2.userDashboard.MyProfile")}</h1>
      </Col>
      <Col xs="6">
        <Row>
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfileFirstName"
            formClasses="col-6"
            labelKey="ui.storefront.common.FirstName"
            required={true}
            maxLength={128}
            placeholderKey="ui.storefront.common.FirstName"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfileLastName"
            formClasses="col-6"
            labelKey="ui.storefront.common.LastName"
            required={true}
            maxLength={128}
            placeholderKey="ui.storefront.common.LastName"
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfileDisplayName"
            formClasses="col-12"
            labelKey="ui.storefront.common.DisplayName"
            maxLength={128}
            placeholderKey="ui.storefront.common.DisplayName"
          />
        </Row>
      </Col>
      <Col xs="6">
        <Row>
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfilePhone1"
            formClasses="col-6"
            labelKey="ui.storefront.common.Phone"
            isPhone={true}
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfileFax1"
            formClasses="col-6"
            labelKey="ui.storefront.common.Fax"
            isFax={true}
          />
          <TextInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfileEmail1"
            formClasses="col-12"
            labelKey="ui.storefront.common.Email"
            isEmail={true}
          />
        </Row>
      </Col>
      <Col xs="12">
        <hr className="w-100 mt-0 mb-3" />
      </Col>
      <Col md="6">
        <Row>
          <PasswordInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfilePasswordCurrent"
            formClasses="col-12"
            labelKey="ui.storefront.userDashboard2.controls.userProfile.CurrentPasswordRequiredForProfileChanges"
            required={true}
            tooltipText="You are required to enter your current password in order to update your profile"
            placeholderKey="ui.storefront.userDashboard2.controls.userProfile.EnterYourCurrentPassword"
          />
          <PasswordInputFormGroup
            register={register}
            errors={errors}
            formIdentifier="txtProfilePasswordNew"
            formClasses="col-12"
            labelKey="ui.storefront.userDashboard2.controls.userProfile.OptionallySetANewPassword"
            tooltipText="You may update your current password while saving profile changes"
            placeholderKey="ui.storefront.userDashboard2.controls.userProfile.EnterYourNewPassword"
            minLength={7}
            required={false}
          />
        </Row>
      </Col>
      <Col xs="12">
        <Row className="mt-3">
          <Col xs="auto">
            <Button
              variant="primary"
              className="btn-lg mb-3"
              type="submit"
              onClick={handleSubmit(handleSaveButtonClicked)}
              id="btnSaveUserProfile"
              name="btnSaveUserProfile"
              title="Save">
              {t("ui.storefront.common.Save")}
            </Button>
          </Col>
          <Col>
            {showSavedMessage && (
              <Alert variant="success" className="mb-0">
                <FontAwesomeIcon icon={faCheckCircle} />{" "}
                {t("ui.storefront.common.Success.Exclamation")}{" "}
                {t("ui.storefront.menu.miniMenu.Profile")}{" "}
                {t("ui.storefront.product.reviews.reviewEntry.hasBeenSaved")}
              </Alert>
            )}
          </Col>
        </Row>
      </Col>
    </Form>
  );
};
