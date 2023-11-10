import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";
import { faCheckSquare, faSquare } from "@fortawesome/free-regular-svg-icons";
import { Button } from "react-bootstrap";

interface ICheckoutStepsHeaderProps {
  valid: boolean;
  titleKey: string;
  index: number;
  allowEdit?: boolean;
  onEditClicked?: () => void;
}

export const CheckoutStepsHeader = (props: ICheckoutStepsHeaderProps) => {
  const { valid, titleKey, index, allowEdit, onEditClicked } = props;
  const { t } = useTranslation();
  return (
    <div className="d-flex justify-content-between align-items-end">
      <h5>
        <FontAwesomeIcon
          icon={valid ? faCheckSquare : faSquare}
          className={`text-${valid ? "success" : "primary"}`}
        />
        &nbsp;
        <span className={`text-${valid ? "success" : "primary"}`}>
          <span>
            Step {index}. {t(titleKey)}
          </span>
        </span>
      </h5>
      {allowEdit && (
        <Button variant="outline-secondary" onClick={onEditClicked}>
          {t("ui.storefront.common.Edit")}
        </Button>
      )}
    </div>
  );
};
