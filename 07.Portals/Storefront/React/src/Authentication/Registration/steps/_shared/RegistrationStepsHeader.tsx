import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";
import { faCheckSquare, faSquare } from "@fortawesome/free-regular-svg-icons";

interface IRegistrationStepsHeaderProps {
  valid: boolean;
  titleKey: string;
}

export const RegistrationStepsHeader = (
  props: IRegistrationStepsHeaderProps
) => {
  const { valid, titleKey } = props;
  const { t } = useTranslation();
  return (
    <h5>
      <FontAwesomeIcon
        icon={valid ? faCheckSquare : faSquare}
        className={`text-${valid ? "success" : "primary"}`}
      />
      &nbsp;
      <span className={`text-${valid ? "success" : "primary"}`}>
        <span>{t(titleKey)}</span>
      </span>
    </h5>
  );
};
