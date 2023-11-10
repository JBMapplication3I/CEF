import { useTranslation } from "react-i18next";
import { IPCCWLayoutProps } from "./_ProductCardControlsWidgetTypes";
import ImageWithFallback from "../../../_shared/common/ImageWithFallback";
import { ButtonGroup, Button, Card } from "react-bootstrap";

export const PCCWOverlayLayout = (props: IPCCWLayoutProps): JSX.Element => {
  const { product, items } = props;
  const { t } = useTranslation();

  return (
    <Card className="card-img-top bg-white">
      <div className="image-with-control-overlay">
        <div
          className="product-image justify-content-center"
          id={`cartProductImageThumbnail${product.ID}`}>
          <ImageWithFallback
            className="img-fluid d-block"
            width="200"
            height="200"
            src={product.PrimaryImageFileName}
            alt={product.Name}
          />
        </div>
        <div className="text-center image-overlay-controls p-3 bg-transparent w-100">
          <ButtonGroup vertical className="center">
            {items.map((item: any) => {
              const {
                hide,
                idPrefix,
                titleKey,
                title,
                onClick,
                icon,
                isSelected
              } = item;
              if (!hide) {
                return (
                  <Button
                    variant="secondary"
                    className="rounded-pill text-decoration-none d-flex align-items-center mt-1"
                    key={titleKey}
                    id={`${idPrefix}${product.ID}`}
                    title={t(titleKey)}
                    aria-label={t(titleKey)}
                    onClick={onClick}>
                    <div className="d-inline-block px-2">{icon}</div>
                    <div className="flex-grow-1">
                      <span>{t(titleKey)}</span>
                    </div>
                  </Button>
                );
              }
              return <Button className="d-none" key={titleKey}></Button>;
            })}
          </ButtonGroup>
        </div>
      </div>
    </Card>
  );
};
