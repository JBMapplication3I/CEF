import { useTranslation } from "react-i18next";
import { Button } from "react-bootstrap";
interface IViewDetailsButtonProps {
  seoUrl: string;
}

export const ViewDetailsButton = (
  props: IViewDetailsButtonProps
): JSX.Element => {
  const { seoUrl } = props;
  const { t } = useTranslation();
  return (
    <Button
      as="a"
      variant="primary"
      href={`/product?seoUrl=${seoUrl}`}
      className="text-center">
      {t("ui.storefront.common.ViewDetails")}
    </Button>
  );
};
