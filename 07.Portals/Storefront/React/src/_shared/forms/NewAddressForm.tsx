import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { faBriefcase, faCity, faRoad } from "@fortawesome/free-solid-svg-icons";
import { SelectFormGroup, TextInputFormGroup } from "./formGroups";
import { useViewState } from "../customHooks/useViewState";
import { useTranslation } from "react-i18next";
import { LoadingWidget } from "../common/LoadingWidget";
import cvApi from "../../_api/cvApi";
import { ContactModel, CountryModel, RegionModel } from "../../_api/cvApi._DtoClasses";
import { Form, Col, Row, Button } from "react-bootstrap";

export interface INewAddressFormData {
  txtKeyRegistrationAddressBookBilling: string;
  txtFirstNameRegistrationAddressBookBilling: string;
  txtLastNameRegistrationAddressBookBilling: string;
  emEmail1RegistrationAddressBookBilling: string;
  txtCompanyRegistrationAddressBookBilling: string;
  ddlCountryIDRegistrationAddressBookBilling: string;
  telPhone1RegistrationAddressBookBilling: string;
  txtStreet1RegistrationAddressBookBilling: string;
  telFax1RegistrationAddressBookBilling: string;
  txtCityRegistrationAddressBookBilling: string;
  ddlRegionIDRegistrationAddressBookBilling: string;
  txtZipRegistrationAddressBookBilling: string;
}

export interface INewAddressFormCallbackData {
  CustomKey: string;
  FirstName: string;
  LastName: string;
  Email1: string;
  Company?: string;
  Country: CountryModel;
  Phone1: string;
  Street1: string;
  Street2?: string;
  Fax1: string;
  City: string;
  Region: RegionModel;
  PostalCode: string;
  IsBilling: boolean;
  CreatedDate: Date;
}

interface INewAddressFormProps {
  type: "billing" | "shipping";
  contact?: ContactModel;
  onConfirm: (newAddressFormData: INewAddressFormCallbackData) => any;
  onCancel?: Function;
  showCancel?: boolean;
}

export const NewAddressForm = (props: INewAddressFormProps) => {
  const { contact, onConfirm, type, showCancel, onCancel } = props;

  const [countries, setCountries] = useState<CountryModel[]>([]);
  const [regions, setRegions] = useState<RegionModel[]>([]);
  // const [phoneNumber2, setPhoneNumber2] = useState("");
  // const [street2, setStreet2] = useState("");
  // const [faxNumber2, setFaxNumber2] = useState("");
  const { setRunning, finishRunning, viewState } = useViewState();
  const { t } = useTranslation();

  useEffect(() => {
    getGlobalizationData();
    return () => {
      setCountries([]);
      setRegions([]);
    };
  }, []);

  async function getGlobalizationData() {
    setRunning();
    try {
      const countryData = await cvApi.geography.GetCountries();
      const regionData = await cvApi.geography.GetRegions();
      setCountries(countryData.data.Results);
      setRegions(regionData.data.Results);
      finishRunning();
    } catch (err) {
      finishRunning(true, err);
    }
  }

  let defaultValues = {
    txtKeyRegistrationAddressBookBilling: contact ? contact.CustomKey : "",
    txtFirstNameRegistrationAddressBookBilling: contact ? contact.FirstName : "",
    txtLastNameRegistrationAddressBookBilling: contact ? contact.LastName : "",
    emEmail1RegistrationAddressBookBilling: contact ? contact.Email1 : "",
    txtCompanyRegistrationAddressBookBilling: contact ? contact.Address.Company : "",
    ddlCountryIDRegistrationAddressBookBilling: contact
      ? contact.Address.Country.Name
      : "Select a Country",
    telPhone1RegistrationAddressBookBilling: contact ? contact.Phone1 : "",
    txtStreet1RegistrationAddressBookBilling: contact ? contact.Address.Street1 : "",
    telFax1RegistrationAddressBookBilling: contact ? contact.Fax1 : "",
    txtCityRegistrationAddressBookBilling: contact ? contact.Address.City : "",
    ddlRegionIDRegistrationAddressBookBilling: contact
      ? contact.Address.Region.CustomKey
      : "Select a State",
    txtZipRegistrationAddressBookBilling: contact ? contact.Address.PostalCode : ""
  };

  const {
    register,
    handleSubmit,
    watch,
    getValues,
    formState: { errors },
    trigger,
    reset
  } = useForm({
    mode: "all",
    reValidateMode: "onBlur",
    defaultValues: { ...defaultValues }
  });

  const countryValue = watch("ddlCountryIDRegistrationAddressBookBilling");
  const countryValues = getValues("ddlRegionIDRegistrationAddressBookBilling");
  const regionValues = getValues("ddlRegionIDRegistrationAddressBookBilling");

  useEffect(() => {
    // Address is null in first render
    reset(defaultValues);
  }, [contact]);

  useEffect(() => {
    // Reset validation if the country or region change
    trigger([
      "ddlCountryIDRegistrationAddressBookBilling",
      "ddlRegionIDRegistrationAddressBookBilling"
    ]);
  }, [trigger, countryValues, regionValues]);

  const handleConfirm = (data: INewAddressFormData): void => {
    const {
      txtKeyRegistrationAddressBookBilling,
      txtFirstNameRegistrationAddressBookBilling,
      txtLastNameRegistrationAddressBookBilling,
      emEmail1RegistrationAddressBookBilling,
      txtCompanyRegistrationAddressBookBilling,
      ddlCountryIDRegistrationAddressBookBilling,
      telPhone1RegistrationAddressBookBilling,
      txtStreet1RegistrationAddressBookBilling,
      telFax1RegistrationAddressBookBilling,
      txtCityRegistrationAddressBookBilling,
      ddlRegionIDRegistrationAddressBookBilling,
      txtZipRegistrationAddressBookBilling
    } = data;
    const CreatedDate = new Date();
    const userAddressData: INewAddressFormCallbackData = {
      CustomKey: txtKeyRegistrationAddressBookBilling,
      FirstName: txtFirstNameRegistrationAddressBookBilling,
      LastName: txtLastNameRegistrationAddressBookBilling,
      Email1: emEmail1RegistrationAddressBookBilling,
      Country: countries.find(
        (r) =>
          r.CustomKey === ddlCountryIDRegistrationAddressBookBilling ||
          r.Name === ddlCountryIDRegistrationAddressBookBilling
      ),
      Phone1: telPhone1RegistrationAddressBookBilling,
      Street1: txtStreet1RegistrationAddressBookBilling,
      // Street2: street2,
      Fax1: telFax1RegistrationAddressBookBilling,
      City: txtCityRegistrationAddressBookBilling,
      Region: regions.find(
        (r) =>
          r.CustomKey === ddlRegionIDRegistrationAddressBookBilling ||
          r.Name === ddlRegionIDRegistrationAddressBookBilling
      ),
      PostalCode: txtZipRegistrationAddressBookBilling,
      IsBilling: type === "billing",
      CreatedDate: contact && contact.CreatedDate ? contact.CreatedDate : CreatedDate
    };
    if (txtCompanyRegistrationAddressBookBilling) {
      userAddressData.Company = txtCompanyRegistrationAddressBookBilling;
    }
    onConfirm(userAddressData);
  };

  const handleCancel = (_e: React.MouseEvent<HTMLButtonElement>) => {
    if (onCancel) {
      onCancel();
    }
  };

  const getCountryOptions = () => {
    return countries.map((c) => ({ ...c, option: c.Name }));
  };

  return (
    <Col
      as={Form}
      className="position-relative"
      autoComplete="off"
      onSubmit={handleSubmit(handleConfirm)}>
      {viewState.running ? <LoadingWidget overlay={true} padIn={true} /> : null}
      <Row>
        <TextInputFormGroup
          formIdentifier="txtKeyRegistrationAddressBookBilling"
          formClasses="col-6"
          errors={errors}
          register={register}
          labelKey="ui.storefront.checkout.views.accountInformation.addressKey"
          labelText="Address Key"
          required={true}
          requiredMessage="Address Key is required"
          maxLength={15}
          maxLengthMessage="Address key may not exceed 15 characters"
          placeholderKey="ui.storefront.checkout.views.accountInformation.addressKey"
          inputClass="text-capitalize"
        />
      </Row>
      <Row>
        <TextInputFormGroup
          formIdentifier="txtFirstNameRegistrationAddressBookBilling"
          formClasses="col-3 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.FirstName"
          labelText="First Name"
          required={true}
          requiredMessage="First name is required"
          maxLength={128}
          maxLengthMessage="First name may not exceed 128 characters"
          placeholderKey="ui.storefront.common.FirstName"
          inputClass="text-capitalize"
        />
        <TextInputFormGroup
          formIdentifier="txtLastNameRegistrationAddressBookBilling"
          formClasses="col-3 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.LastName"
          labelText="Last Name"
          required={true}
          requiredMessage="Last name is required"
          maxLength={128}
          maxLengthMessage="Last name may not exceed 128 characters"
          placeholderKey="ui.storefront.common.LastName"
          inputClass="text-capitalize"
        />
        <TextInputFormGroup
          formIdentifier="txtCompanyRegistrationAddressBookBilling"
          formClasses="col-6 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.Company"
          labelText="Company"
          maxLength={128}
          maxLengthMessage="Company may not exceed 128 characters"
          placeholderKey="ui.storefront.common.Company"
          inputClass="text-capitalize"
          leftIcon={faBriefcase}
        />
      </Row>
      <Row>
        <TextInputFormGroup
          formIdentifier="emEmail1RegistrationAddressBookBilling"
          formClasses="col-6 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.Email"
          labelText="Email"
          required={true}
          requiredMessage="Email is required"
          pattern={
            /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
          }
          patternMessage="Email is not valid/formatted properly"
          placeholderKey="ui.storefront.common.Email"
          isEmail={true}
        />
        <SelectFormGroup
          formIdentifier="ddlCountryIDRegistrationAddressBookBilling"
          errors={errors}
          register={register}
          labelText="Country"
          labelKey="ui.storefront.common.Country"
          formClasses="col-md-6 form-group"
          required={true}
          hideOptionKey={true}
          includeNull={true}
          nullText="Select a Country"
          nullKey="ui.storefront.common.geography.Country.SelectA"
          options={getCountryOptions()}
        />
      </Row>
      <Row>
        <TextInputFormGroup
          formIdentifier="telPhone1RegistrationAddressBookBilling"
          formClasses="col-6 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.Phone"
          labelText="Phone"
          required={true}
          requiredMessage="Phone number is required"
          maxLength={40}
          maxLengthMessage="Phone numbers may not exceed 40 characters"
          minLength={10}
          minLengthMessage="Phone numbers must include country code and area code."
          placeholderText="1-555-555-5555"
          inputClass="text-capitalize"
          isPhone={true}
        />
        <TextInputFormGroup
          formIdentifier="txtStreet1RegistrationAddressBookBilling"
          formClasses="col-6 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.Street"
          labelText="Street"
          required={true}
          requiredMessage="Street is required"
          maxLength={35}
          maxLengthMessage="Street may not exceed 35 characters"
          minLength={10}
          minLengthMessage="Street address must be a valid address."
          placeholderKey="ui.storefront.common.Street"
          inputClass="text-capitalize"
          leftIcon={faRoad}
        />
      </Row>
      <Row>
        <TextInputFormGroup
          formIdentifier="telFax1RegistrationAddressBookBilling"
          formClasses="col-6 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.Fax"
          labelText="Fax"
          required={false}
          requiredMessage="Fax is required"
          maxLength={40}
          maxLengthMessage="Fax may not exceed 40 characters"
          minLength={10}
          minLengthMessage="Fax numbers must include country code and area code."
          placeholderText="+1(555)555-5555"
          inputClass="text-capitalize"
          isFax={true}
        />
        <TextInputFormGroup
          formIdentifier="txtCityRegistrationAddressBookBilling"
          formClasses="col-6 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.City"
          labelText="City"
          required={true}
          requiredMessage="City is required"
          maxLength={90}
          maxLengthMessage="City max not exceed 90 characters"
          placeholderText="City"
          inputClass="text-capitalize"
          leftIcon={faCity}
        />
        <Col xs={6}>
          <Form.Label>
            <span>&nbsp;</span>
          </Form.Label>
        </Col>
        <SelectFormGroup
          formIdentifier="ddlRegionIDRegistrationAddressBookBilling"
          formClasses="col-3"
          errors={errors}
          register={register}
          options={
            regions && regions.length && countryValue
              ? [
                  ...regions
                    .filter((reg) => reg.CountryName === countryValue)
                    .map((region) => ({
                      option: region.Name,
                      value: region.Name
                    }))
                ]
              : []
          }
          labelKey="ui.storefront.common.geography.State"
          labelText="State"
          includeNull={true}
          required={true}
          nullText="Select a State"
          nullKey="ui.storefront.common.geography.State.SelectA"
        />
        <TextInputFormGroup
          formIdentifier="txtZipRegistrationAddressBookBilling"
          formClasses="col-3 form-group"
          errors={errors}
          register={register}
          labelKey="ui.storefront.common.Zip"
          labelText="Zip"
          required={true}
          requiredMessage="Zip code is required"
          maxLength={128}
          maxLengthMessage="Zip code may not exceed 128 characters"
          minLength={5}
          minLengthMessage="Zip code must be a least 5 characters"
          patternMessage="Zip code is invalid"
          placeholderKey="ui.storefront.common.Zip"
          leftIcon={faCity}
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
              id={`btnAdd${type === "billing" ? "Billing" : "Shipping"}`}
              name={`btnAdd${type === "billing" ? "Billing" : "Shipping"}`}>
              {t("ui.storefront.checkout.splitShipping.addressModal.AddAddress")}
            </Button>
          </div>
        </Col>
      </Row>
    </Col>
  );
};
