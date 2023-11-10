import Interweave from "interweave";
import { useTranslation } from "react-i18next";
import { ProductModel } from "../../../_api/cvApi.shared";
import { GeneralAttributeModel } from "../../../_api/cvApi._DtoClasses";
import { Row, Col } from "react-bootstrap";
import { ProductDetailsTabAttribute } from "./ProductDetailsTabAttribute";
import { IFullTabSetTab } from "./ProductDetailsTabs";

interface IProductDetailsTabsDefaultTabContentProps {
  product: ProductModel;
  fullTabSet: IFullTabSetTab[];
}

export const ProductDetailsTabsDefaultTabContent = (
  props: IProductDetailsTabsDefaultTabContentProps
): JSX.Element => {
  const { product, fullTabSet } = props;

  const { t } = useTranslation();

  return (
    <Row className="description" id="tabMoreInformation">
      {product && product.Description ? (
        <Col xs={12} className="mb-3">
          <h3 id="ProductOverviewText">
            {t("ui.storefront.product.detail.productDetails.productOverview")}
          </h3>
          <div id="ProductOverviewDescriptionText">
            <Interweave
              content={product.Description}
              allowAttributes={true}
              allowElements={true}
            />
          </div>
        </Col>
      ) : null}
      {fullTabSet
        .filter((ft) => ft.key === "default-tab")
        .map((fullTab) => {
          const { header, attributes } = fullTab;
          return (
            <Col xs={12} key={header}>
              <h3 id="SpecificationsText">
                {t(
                  "ui.storefront.product.detail.productDetails.Specifications"
                )}
              </h3>
              {attributes
                .sort((attr1, attr2) =>
                  attr1.SortOrder && attr2.SortOrder
                    ? attr1.SortOrder - attr2.SortOrder
                    : 0
                )
                .map((attr: GeneralAttributeModel) => {
                  return (
                    <dl key={attr.CustomKey}>
                      <ProductDetailsTabAttribute
                        product={product}
                        attribute={attr}
                      />
                    </dl>
                  );
                })}
            </Col>
          );
        })}
    </Row>
  );
};
