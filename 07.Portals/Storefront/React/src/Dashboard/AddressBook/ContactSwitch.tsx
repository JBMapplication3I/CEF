import { useEffect, useState } from "react";
import { Form } from "react-bootstrap";
import { AddressBlock } from "./AddressBlock";
import { AccountContactModel } from "../../_api/cvApi._DtoClasses";
import { INewAddressFormCallbackData } from "../../_shared/forms/NewAddressForm";
import { NewAddressModal } from "./NewAddressModal";
import { Button } from "react-bootstrap";

/* TODO: Restore/allow In Store Pickup and Ship to Store options
  case shipToStoreOption.ID: {
      // TODO: Use the address of the store
      setDestinationContactForTarget(target, shipToStoreOption.ID, cvServiceStrings.attributes.shipToStore);
      break;
  }
  case inStorePickupOption.ID: {
      // TODO: Use the address of the store
      setDestinationContactForTarget(target, inStorePickupOption.ID, cvServiceStrings.attributes.inStorePickup);
      break;
  }
*/

interface IContactSwitchProps {
  accountContacts: Array<AccountContactModel>;
  onChange: (contact: AccountContactModel) => any;
  title?: string;
  classes?: string;
  hideAddressBlock?: boolean;
  allowAdd?: boolean;
  useAddButton?: boolean;
  onAddressAdded?: (newAddressData: INewAddressFormCallbackData) => any;
  initialContact?: AccountContactModel;
  allowInStorePickup?: boolean;
  allowShipToStore?: boolean;
}

export const ContactSwitch = (props: IContactSwitchProps): JSX.Element => {
  const {
    accountContacts,
    onChange,
    title,
    classes,
    hideAddressBlock,
    allowAdd,
    onAddressAdded,
    initialContact,
    useAddButton
  } = props;

  const [selectedContact, setSelectedContact] = useState(initialContact ?? null);
  const [showNewAddressModel, setShowNewAddressModal] = useState(false);

  useEffect(() => {
    if (initialContact) {
      setSelectedContact(initialContact);
    }
  }, [initialContact]);

  return (
    <div className={`wrap mb-2 ${classes ?? ""}`}>
      <strong className="d-block">{title ?? ""}</strong>
      <Form.Select
        id="headquarters"
        aria-label="headquarters"
        className={!hideAddressBlock && selectedContact && selectedContact.Slave ? "mb-2" : ""}
        value={
          showNewAddressModel ? "Add a new Address" : selectedContact ? selectedContact.ID : ""
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
        {allowAdd && !useAddButton ? (
          <option value="Add a new Address">Add a new Address</option>
        ) : null}
      </Form.Select>
      {allowAdd && useAddButton && (
        <div className="input-group-append">
          <Button
            variant="secondary"
            className="rounded-right"
            type="button"
            onClick={() => setShowNewAddressModal(true)}>
            Add
          </Button>
        </div>
      )}
      {!hideAddressBlock && selectedContact && selectedContact.Slave && (
        <AddressBlock
          address={{
            Name: selectedContact.Slave.CustomKey,
            ...selectedContact.Slave,
            ...selectedContact.Slave.Address
          }}
        />
      )}
      {allowAdd ? (
        <NewAddressModal
          show={showNewAddressModel}
          onCancel={() => {
            setShowNewAddressModal(false);
          }}
          onConfirm={(newAddressData: INewAddressFormCallbackData) => {
            onAddressAdded(newAddressData);
            setShowNewAddressModal(false);
          }}
        />
      ) : null}
    </div>
  );
};
