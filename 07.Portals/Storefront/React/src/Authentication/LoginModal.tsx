import { Modal } from "../_shared/modals/Modal";
import { LoginForm } from "../_shared/forms/LoginForm";

interface ILoginModalProps {
  show: boolean;
  onConfirm?: Function;
  onCancel: Function;
}

export const LoginModal = (props: ILoginModalProps): JSX.Element => {
  const { show, onConfirm, onCancel } = props;

  return (
    <Modal title="Sign In" show={show} onCancel={onCancel} size="md">
      <LoginForm
        onLoginSuccess={onConfirm}
        showCancel={true}
        onCancel={onCancel}
      />
    </Modal>
  );
};
