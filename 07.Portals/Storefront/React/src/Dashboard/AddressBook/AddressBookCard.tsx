import { AddressBlock } from "./AddressBlock";
import { Col, Card, Button } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrashAlt } from "@fortawesome/free-regular-svg-icons";
import { faPencilAlt } from "@fortawesome/free-solid-svg-icons";
import { ConfirmationModal } from "../../_shared/modals/ConfirmationModal";
import { AccountContactModel } from "../../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import { SelectFormGroup } from "../../_shared/forms/formGroups";
import { useEffect, useState } from "react";

interface IAddressBookCardProps {
  accountContactID?: number;
  accountContacts: Array<AccountContactModel>;
  type: "Shipping" | "Billing" | "Other";
  showDropdown?: boolean;
  showDeleteAddressConfirmationModalID: number;
  setShowDeleteAddressConfirmationModalID: Function;
  handleDefaultAddressChanged?: Function;
  handleEditAddressClicked: Function;
  deleteAddress: Function;
  markAccountContact?: Function;
}

export const AddressBookCard = (props: IAddressBookCardProps): JSX.Element => {
  const [selectedAccountContact, setSelectedAccountContact] =
    useState<AccountContactModel>(null);
  const {
    accountContactID,
    accountContacts,
    type,
    showDropdown,
    handleEditAddressClicked,
    showDeleteAddressConfirmationModalID,
    handleDefaultAddressChanged,
    setShowDeleteAddressConfirmationModalID,
    deleteAddress
  } = props;
  const { ID, Slave } = selectedAccountContact ?? {};
  const { t } = useTranslation();

  useEffect(() => {
    if (!accountContacts) {
      return;
    }
    let selectedContact;
    if (accountContactID) {
      selectedContact = accountContacts.find(
        (accountContact: AccountContactModel) =>
          accountContact.ID === accountContactID
      );
      setSelectedAccountContact(selectedContact);
      return;
    }
    selectedContact = accountContacts.find((contact: AccountContactModel) =>
      type === "Billing" ? contact.IsBilling : contact.IsPrimary
    );
    setSelectedAccountContact(selectedContact);
  }, [accountContacts]);

  const getAccountContact = (id: number) => {
    return accountContacts.find(
      (accountContact: AccountContactModel) => accountContact.ID === id
    );
  };

  return (
    <Col xl="4" className="mb-3">
      <Card className="h-100">
        <Card.Header className="text-uppercase">
          {type === "Billing"
            ? "Default Billing"
            : type === "Shipping"
            ? "Default Shipping"
            : Slave?.AddressKey}
        </Card.Header>
        {showDropdown ? (
          <Card.Body className="p-2">
            <select
              className="form-control d-inline-block"
              id="sort"
              aria-label="sort"
              value={selectedAccountContact?.ID ?? -1}
              onChange={(e) => {
                let newAccountContactID = parseInt(e.target.value);
                handleDefaultAddressChanged(
                  newAccountContactID,
                  type === "Billing" ? true : false
                );
                setSelectedAccountContact(
                  getAccountContact(newAccountContactID)
                );
              }}>
              <option value={-1} disabled={true}>
                Please select a {type} address
              </option>
              {accountContacts &&
                accountContacts.map((ac: AccountContactModel): JSX.Element => {
                  return <option key={ac.ID} label={ac.Name} value={ac.ID}></option>;
                })}
            </select>
          </Card.Body>
        ) : null}
        {selectedAccountContact ? (
          <>
            <Card.Body className="p-2">
              <AddressBlock
                address={{
                  Name: Slave.FullName,
                  ...Slave,
                  ...Slave.Address
                }}
              />
            </Card.Body>
            <Card.Footer className="btn-group p-0">
              <Button
                variant="link"
                className="text-decoration-none"
                onClick={() =>
                  handleEditAddressClicked(selectedAccountContact)
                }>
                <FontAwesomeIcon icon={faPencilAlt} className="fa-lg" />
                <span>{t("ui.storefront.common.Edit")}</span>
              </Button>
              <Button
                variant="link"
                className="text-danger text-decoration-none"
                onClick={() => setShowDeleteAddressConfirmationModalID(ID)}>
                <FontAwesomeIcon icon={faTrashAlt} className="fa-lg" />
                <span>{t("ui.storefront.common.Delete")}</span>
              </Button>
              <ConfirmationModal
                title="Do you really want to remove this address?"
                confirmBtnLabel="Delete Address"
                show={showDeleteAddressConfirmationModalID === ID}
                onConfirm={() => deleteAddress(ID)}
                onCancel={() => setShowDeleteAddressConfirmationModalID(null)}>
                <AddressBlock
                  address={{
                    Name: Slave.FullName,
                    ...Slave,
                    ...Slave.Address
                  }}
                />
              </ConfirmationModal>
            </Card.Footer>
          </>
        ) : null}
      </Card>
    </Col>
  );
};
