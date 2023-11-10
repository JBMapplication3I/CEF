import { useTranslation } from "react-i18next";
import { ConfirmationModal } from "../../_shared/modals";

interface IRemoveFromCartModalProps {
  item: any;
  show: boolean;
  onConfirm: Function;
  onCancel: Function;
}

const RemoveFromCartModal = (props: IRemoveFromCartModalProps) => {
  const { item, show, onConfirm, onCancel } = props;
  const { t } = useTranslation();

  return (
    <ConfirmationModal
      title={t("ui.storefront.common.RemoveItem")}
      show={show}
      confirmBtnLabel={t("ui.storefront.common.RemoveItem")}
      cancelBtnLabel={t("ui.storefront.cart.continueShopping")}
      onConfirm={onConfirm}
      onCancel={onCancel}
      size="md">
      <p id="lbRFCMContent">
        <span>{t("ui.storefront.cart.AreYouSureYouWantToRemove")}</span>
        &nbsp;
        <b>{item.Name}</b>&nbsp;
        {/* <span>{t("ui.storefront.cart.FromTheCartOfType.Template")}</span> */}
      </p>
      {/* <div>
        <button
          type="button"
          className="btn btn-outline-secondary"
          id="btnRFCMCancel"
          name="btnRFCMCancel"
          title={t("ui.storefront.common.Cancel")}
          aria-label={t("ui.storefront.common.Cancel")}
          onClick={() => "cancel()"}></button>
        <button
          type="button"
          className="btn btn-danger"
          id="btnRFCMOk"
          name="btnRFCMOk"
          title={t("ui.storefront.common.RemoveItem")}
          aria-label={t("ui.storefront.common.RemoveItem")}
          onClick={() => "ok()"}>
          Remove Item
        </button>
      </div> */}
    </ConfirmationModal>
  );
};

export default RemoveFromCartModal;
