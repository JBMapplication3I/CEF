import { useTranslation } from "react-i18next";
import { IPCCWLayoutProps } from "./_ProductCardControlsWidgetTypes";
import { Button, ButtonGroup } from "react-bootstrap";
export const PCCWHorizontalLayout = (props: IPCCWLayoutProps): JSX.Element => {
  const { product, items } = props;
  const { t } = useTranslation();

  return (
    <ButtonGroup className="justify-content-between w-100">
      {items.map((item: any): JSX.Element => {
        const { hide, hideI, idPrefix, titleKey, onClick, icon } = item;
        if (!hide) {
          return (
            <Button
              variant="link"
              className="wrap text-decoration-none"
              id={`${idPrefix}${product.ID}`}
              key={titleKey}
              title={t(titleKey)}
              aria-label={t(titleKey)}
              onClick={onClick}>
              {icon}
              {!hideI && <br />}
            </Button>
          );
        }
        return <Button className="d-none" key={titleKey}></Button>;
      })}
    </ButtonGroup>
  );
};
