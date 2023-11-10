import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { Modal } from "../../_shared/modals/Modal";
import { INewAddressFormCallbackData, NewAddressForm } from "../../_shared/forms/NewAddressForm";
import { ContactModel } from "../../_api/cvApi._DtoClasses";
import { Row } from "react-bootstrap";
interface INewAddressModalProps {
  type?: "billing" | "shipping";
  contact?: ContactModel;
  show: boolean;
  onConfirm: (newAddressFormData: INewAddressFormCallbackData) => any;
  onCancel: Function;
  parentRunning?: boolean;
}

export const NewAddressModal = (props: INewAddressModalProps) => {
  const { type, contact, show, onConfirm, onCancel, parentRunning } = props;

  return (
    <Modal
      title={`Add a New${type ? " " + type : ""} Address`}
      show={show}
      onCancel={onCancel}>
      {parentRunning ? (
        <LoadingWidget />
      ) : (
        <div className="modal-body form-vertical">
          <Row>
            <NewAddressForm
              contact={contact}
              onConfirm={onConfirm}
              type={type}
              showCancel={true}
              onCancel={onCancel}
            />
          </Row>
        </div>
      )}
    </Modal>
  );
};
