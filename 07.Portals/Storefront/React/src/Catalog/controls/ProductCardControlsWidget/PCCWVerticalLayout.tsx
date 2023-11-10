import { IPCCWLayoutProps } from "./_ProductCardControlsWidgetTypes";
import { useTranslation } from "react-i18next";
import { Button, ListGroup } from "react-bootstrap";
export const PCCWVerticalLayout = (props: IPCCWLayoutProps): JSX.Element => {
  const { product, items } = props;
  const { t } = useTranslation();

  return (
    <ListGroup as="ul" className="list-unstyled">
      {items.map((item: any) => {
        const { hide, idPrefix, titleKey, onClick, icon } = item;
        if (!hide) {
          return (
            <ListGroup.Item key={titleKey} className="p-0">
              <Button
                variant="outline-dark"
                className="border-0 w-100 d-flex align-items-center"
                id={`${idPrefix}${product.ID}`}
                title={t(titleKey)}
                aria-label={t(titleKey)}
                onClick={onClick}>
                <div className="d-inline-block px-2">{icon}</div>
                <div>
                  <span>{t(titleKey)}</span>
                </div>
              </Button>
            </ListGroup.Item>
          );
        }
        return <ListGroup.Item className="d-none" key={titleKey} />;
      })}
    </ListGroup>
  );
};
