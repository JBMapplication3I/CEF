import { useState } from "react";
import { AccountContactModel } from "../../_api/cvApi._DtoClasses";
import { AddressBlock } from "./AddressBlock";
import { NewAddressModal } from "./NewAddressModal";
import { INewAddressFormCallbackData } from "../../_shared/forms/NewAddressForm";
import { Form, InputGroup, Button } from "react-bootstrap";
interface IContactProps {
  accountContacts: Array<AccountContactModel>;
  onChange: (contact: AccountContactModel) => any;
  title?: string;
  classes?: string;
  hideAddressBlock?: boolean;
  allowAdd?: boolean;
  onAddressAdded?: (newAddressData: INewAddressFormCallbackData) => any;
  initialContact?: AccountContactModel;
}

export const Contact = (props: IContactProps): JSX.Element => {
  const {
    accountContacts,
    onChange,
    title,
    hideAddressBlock,
    allowAdd,
    onAddressAdded,
    initialContact,
    classes
  } = props;

  const [selectedContact, setSelectedContact] = useState<AccountContactModel>(
    initialContact ?? null
  );
  const [showNewAddressModal, setShowNewAddressModal] = useState(false);

  return (
    <div className={classes ?? "wrap mb-2"}>
      <strong className="d-block">{title ?? ""}</strong>
      <Form.Select
        id="headquarters"
        aria-label="headquarters"
        className={!hideAddressBlock && selectedContact && selectedContact.Slave ? "mb-2" : null}
        value={
          showNewAddressModal ? "Add a new Address" : selectedContact ? selectedContact.ID : ""
        }
        required
        onChange={(e) => {
          if (e.target.value === "Add a new Address") {
            setSelectedContact(null);
            setShowNewAddressModal(true);
            return;
          }
          const currentContactVal = accountContacts.find(
            (c: { ID: number }) => c.ID.toString() === e.target.value
          );
          if (currentContactVal) {
            onChange(currentContactVal);
          }
          setSelectedContact(currentContactVal);
        }}>
        <option disabled={!!selectedContact} className={`${selectedContact ? "disabled" : ""}`}>
          Please select an address
        </option>
        {accountContacts &&
          accountContacts.map((accountContact) => {
            const { ID, CustomKey, Slave, SlaveKey } = accountContact;
            return (
              <option key={ID} value={ID}>
                {Slave.Address.CustomKey ?? Slave.CustomKey ?? SlaveKey ?? CustomKey}
              </option>
            );
          })}
        {allowAdd ? <option value="Add a new Address">Add a new Address</option> : null}
      </Form.Select>
      <InputGroup.Text className="p-0 bg-white border-0">
        <Button
          variant="secondary"
          className="rounded-right"
          onClick={() => setShowNewAddressModal(true)}>
          Add
        </Button>
      </InputGroup.Text>
      {!hideAddressBlock && selectedContact && selectedContact.Slave && (
        <AddressBlock
          address={{
            Name: selectedContact.Slave.CustomKey,
            ...selectedContact.Slave,
            ...selectedContact.Slave.Address
          }}
        />
      )}
      <NewAddressModal
        show={showNewAddressModal}
        onCancel={() => {
          setShowNewAddressModal(false);
        }}
        onConfirm={(newAddressData: INewAddressFormCallbackData) => {
          if (onAddressAdded) {
            onAddressAdded(newAddressData);
          }
          setShowNewAddressModal(false);
        }}
      />
    </div>
  );
};
