import { useState } from "react";
import { NewAddressModal } from "../../../../Dashboard/AddressBook/NewAddressModal";
import { AddressBlock } from "../../../../Dashboard/AddressBook/AddressBlock";
import { INewAddressFormCallbackData } from "../../../../_shared/forms/NewAddressForm";
import { Row, Col, Button } from "react-bootstrap";
interface IRegistrationAddressBookStepProps {
  onCompleteAddressBookStep: (
    billingAddress: INewAddressFormCallbackData,
    shippingAddress: INewAddressFormCallbackData
  ) => any;
  continueText: string;
}

export const RegistrationAddressBookStep = (
  props: IRegistrationAddressBookStepProps
): JSX.Element => {
  const { onCompleteAddressBookStep, continueText } = props;

  const [userBillingAddress, setUserBillingAddress] =
    useState<INewAddressFormCallbackData>({
      FirstName: "",
      LastName: "",
      Company: "",
      Street1: "",
      Street2: "",
      City: "",
      Region: null,
      PostalCode: "",
      Country: null,
      Phone1: "",
      Fax1: "",
      Email1: "",
      CustomKey: "",
      IsBilling: true,
      CreatedDate: null
    });
  const [userShippingAddress, setUserShippingAddress] =
    useState<INewAddressFormCallbackData>({
      FirstName: "",
      LastName: "",
      Company: "",
      Street1: "",
      Street2: "",
      City: "",
      Region: null,
      PostalCode: "",
      Country: null,
      Phone1: "",
      Fax1: "",
      Email1: "",
      CustomKey: "",
      IsBilling: false,
      CreatedDate: null
    });
  const [showAddBillingAddressModal, setShowAddBillingAddressModal] =
    useState(false);
  const [showAddShippingAddressModal, setShowAddShippingAddressModal] =
    useState(false);
  const [userBillingAddressComplete, setUserBillingAddressComplete] =
    useState<boolean>(false);
  const [userShippingAddressComplete, setUserShippingAddressComplete] =
    useState<boolean>(false);

  return (
    <Row>
      <Col xs={12}>
        <p>
          Your account will need both a billing and a shipping address assigned
          for payments and shipments.
        </p>
      </Col>
      <Col xs={6}>
        <Button
          variant="primary"
          className="btn-block mb-3 w-100"
          onClick={() => setShowAddBillingAddressModal(true)}>
          {userBillingAddressComplete
            ? "Edit Billing Address"
            : "Add a Billing Address"}
        </Button>
        <NewAddressModal
          type="billing"
          show={showAddBillingAddressModal}
          onCancel={() => setShowAddBillingAddressModal(false)}
          onConfirm={(data) => {
            setUserBillingAddress(data);
            setUserBillingAddressComplete(true);
            setShowAddBillingAddressModal(false);
          }}
        />
        <div className="mb-2">
          {userBillingAddressComplete ? (
            <AddressBlock
              address={{
                Name: userBillingAddress.FirstName + " " + userBillingAddress.LastName,
                ...userBillingAddress
              }}
            />
          ) : null}
        </div>
      </Col>
      <Col xs={6}>
        <Button
          variant="secondary"
          className="btn-block mb-3 w-100"
          onClick={() => setShowAddShippingAddressModal(true)}>
          {userShippingAddressComplete
            ? "Edit Shipping Address"
            : "Add a Shipping Address"}
        </Button>
        <NewAddressModal
          show={showAddShippingAddressModal}
          type="shipping"
          onCancel={() => setShowAddShippingAddressModal(false)}
          onConfirm={(data: INewAddressFormCallbackData) => {
            setUserShippingAddress(data);
            setUserShippingAddressComplete(true);
            setShowAddShippingAddressModal(false);
          }}
        />
        {userShippingAddressComplete ? null : (
          <Button
            variant="secondary"
            disabled={userBillingAddressComplete ? false : true}
            className="btn-block mb-3"
            id="btnRegAddShippingByCopyBilling"
            name="btnRegAddShippingByCopyBilling"
            onClick={() => {
              if (userBillingAddressComplete) {
                setUserShippingAddress(userBillingAddress);
                setUserShippingAddressComplete(true);
              }
            }}>
            Copy the billing address for shipping
          </Button>
        )}
        <div className="mb-2">
          {userShippingAddressComplete ? (
            <AddressBlock
              address={{
                Name: userShippingAddress.FirstName + " " + userShippingAddress.LastName,
                ...userShippingAddress
              }}
            />
          ) : null}
        </div>
      </Col>
      <Col xs={12}>
        <Button
          variant="primary"
          className="btn-block mb-3"
          id="btnSubmit_registrationStepAddressBook"
          name="btnSubmit_registrationStepAddressBook"
          disabled={
            !userShippingAddressComplete || !userBillingAddressComplete
              ? true
              : false
          }
          onClick={() => {
            if (userShippingAddressComplete && userBillingAddressComplete) {
              onCompleteAddressBookStep(
                { ...userBillingAddress },
                { ...userShippingAddress }
              );
            }
          }}>
          {continueText}
        </Button>
      </Col>
    </Row>
  );
};
