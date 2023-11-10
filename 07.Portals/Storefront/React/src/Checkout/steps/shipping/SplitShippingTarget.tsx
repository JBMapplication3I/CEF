import { Row, Col, Button } from "react-bootstrap";
import {
  AccountContactModel,
  SalesItemTargetBaseModel
} from "../../../_api/cvApi._DtoClasses";
import { AddToCartQuantitySelector } from "../../../Cart/controls";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faTrash } from "@fortawesome/free-solid-svg-icons";
import { ContactSwitch } from "../../../Dashboard/AddressBook/ContactSwitch";
import { INewAddressFormCallbackData } from "../../../_shared/forms/NewAddressForm";
import { useTranslation } from "react-i18next";

interface ISplitShippingTargetProps {
  target: SalesItemTargetBaseModel;
  accountContacts: AccountContactModel[];
  onContactChanged: (contact: AccountContactModel) => void;
  onAddressAdded: (newAddressData: INewAddressFormCallbackData) => void;
  decreaseDisabled: boolean;
  increaseDisabled: boolean;
  onQuantityChanged: (val: number) => void;
  removeDisabled: boolean;
  addDisabled: boolean;
  onRemoveClicked: () => void;
  onAddClicked: () => void;
  showAdd: boolean;
}

export const SplitShippingTarget = (props: ISplitShippingTargetProps): JSX.Element => {
  const {
    target,
    accountContacts,
    onContactChanged,
    onAddressAdded,
    decreaseDisabled,
    increaseDisabled,
    onQuantityChanged,
    removeDisabled,
    addDisabled,
    onRemoveClicked,
    onAddClicked,
    showAdd
  } = props;

  const { t } = useTranslation();

  return (
    <Row>
      <Col xs={12} md>
        <div className="form-group mb-0">
          <ContactSwitch
            hideAddressBlock={true}
            allowAdd={true}
            initialContact={
              target.DestinationContact
                ? accountContacts.find((ac) => ac.Slave.ID === target.DestinationContact.ID)
                : null
            }
            accountContacts={accountContacts}
            onChange={onContactChanged}
            onAddressAdded={onAddressAdded}
          />
        </div>
      </Col>
      <Col md={3}>
        <AddToCartQuantitySelector
          key={target.Quantity.toString()} // Required, maybe could replace with useEffect in AddToCartQuantitySelector
          initialValue={target.Quantity}
          decreaseDisabled={decreaseDisabled}
          increaseDisabled={increaseDisabled}
          useInput={false}
          onChange={onQuantityChanged}
        />
      </Col>
      <Col xs="auto">
        <Button
          type="button"
          variant="outline-danger"
          disabled={removeDisabled}
          onClick={onRemoveClicked}>
          <FontAwesomeIcon icon={faTrash} />
          <span className="sr-only">{t("ui.storefront.checkout.splitShipping.RemoveTarget")}</span>
        </Button>
        {showAdd ? (
          <Button
            type="button"
            variant="outline-success"
            disabled={addDisabled}
            onClick={onAddClicked}>
            <FontAwesomeIcon icon={faPlus} />
            <span className="sr-only">{t("ui.storefront.checkout.splitShipping.AddTarget")}</span>
          </Button>
        ) : null}
      </Col>
    </Row>
  );
};
