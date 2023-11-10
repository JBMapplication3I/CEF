import { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { ProductModel } from "../../../_api/cvApi.shared";
import { GeneralAttributeModel } from "../../../_api/cvApi._DtoClasses";
import cvApi from "../../../_api/cvApi";
import { useCefConfig } from "../../../_shared/customHooks/useCefConfig";
import { TabContent } from "../../../_shared/common/TabContent";
import { Tabs } from "../../../_shared/common/Tabs";
import { Row, Col } from "react-bootstrap";
import { ProductDetailsTabsDefaultTabContent } from "./ProductDetailsTabsDefaultTabContent";
import { ReviewsMain } from "../ReviewsMain";
import { ProductDetailsTabAttribute } from "./ProductDetailsTabAttribute";

export interface IFullTabSetTab {
  attributes: GeneralAttributeModel[];
  key: string;
  header: string;
  sort: number;
}

interface IKeysTabbedAndGrouped {
  [key: string]: Array<string>;
}

interface IProductDetailsTabsProps {
  product: ProductModel;
}

export const ProductDetailsTabs = (props: IProductDetailsTabsProps) => {
  const { product } = props;

  const [fullTabSet, setFullTabSet] = useState<Array<IFullTabSetTab>>(null);

  const { t } = useTranslation();
  const cefConfig = useCefConfig();

  useEffect(() => {
    if (!fullTabSet && product && cefConfig) {
      loadAttributes();
    }
  }, [fullTabSet, product, cefConfig]);

  const containsNoResults = (httpResponse: any): boolean => {
    const hasData = httpResponse?.data?.Results?.length;
    return !hasData;
  };

  function loadAttributes(): void {
    cvApi.attributes
      .GetGeneralAttributes({
        Active: true,
        AsListing: true,
        HideFromProductDetailView: false
      })
      .then((res) => {
        if (containsNoResults(res)) {
          console.warn("Failed to fetch general attributes");
          return;
        }
        const generalAttributes: GeneralAttributeModel[] = res.data.Results;
        const keysTabbedAndGrouped = getKeysTabbedAndGrouped(generalAttributes);

        const fullTabSet = Object.entries(keysTabbedAndGrouped).map((keyValArr: any) => {
          const [tabIdentifier, tabAttributesList] = keyValArr;
          const fullTab: any = {
            attributes: generalAttributes.filter((genAttr) =>
              tabAttributesList.includes(genAttr.CustomKey)
            ),
            key: tabIdentifier,
            header: t("ui.storefront.product.detail.productDetails.Specifications")
          };

          if (tabIdentifier === "default-tab") {
            fullTab.header = t("ui.storefront.product.details.MoreInformation");
            fullTab.sort = -999999;
          } else {
            fullTab.sort =
              generalAttributes.find((x) => x.CustomKey === tabIdentifier).SortOrder ?? 0;
          }
          return fullTab;
        });

        if (cefConfig?.featureSet?.reviews?.enabled) {
          fullTabSet.push({
            attributes: [],
            key: "reviews-tab",
            header: t("ui.storefront.product.widgets.relatedProducts.Reviews"),
            sort: 999999
          });
        }

        fullTabSet.sort((fullTab1, fullTab2) => fullTab1.sort - fullTab2.sort);

        setFullTabSet(fullTabSet);
      })
      .catch((err) => {
        console.log(err);
      });
  }

  function getKeysTabbedAndGrouped(generalAttributes: GeneralAttributeModel[] = []) {
    const keysTabbedAndGrouped: IKeysTabbedAndGrouped = {};

    for (const attributeName in product.SerializableAttributes) {
      const attribute = generalAttributes.find((a) => a.CustomKey === attributeName);

      let thisTabKey = "default-tab";
      if (attribute.IsTab) {
        thisTabKey = attributeName;
      }

      if (!keysTabbedAndGrouped[thisTabKey]) {
        keysTabbedAndGrouped[thisTabKey] = [];
      }

      keysTabbedAndGrouped[thisTabKey].push(attributeName);
    }

    return keysTabbedAndGrouped;
  }

  if (!fullTabSet || !fullTabSet.length) {
    return null;
  }

  return (
    <Row>
      <Col xs={12}>
        <Tabs tabContainerClasses="mt-2 mb-4">
          {fullTabSet.map((fullTab) => {
            const { header, key, attributes } = fullTab;
            if (key === "default-tab") {
              return (
                <TabContent label={t("ui.storefront.product.details.MoreInformation")} key={key}>
                  <ProductDetailsTabsDefaultTabContent product={product} fullTabSet={fullTabSet} />
                </TabContent>
              );
            }
            if (key === "reviews-tab") {
              return (
                <TabContent
                  label={t("ui.storefront.product.widgets.relatedProducts.Reviews")}
                  title={t("ui.storefront.product.detail.productDetails.productReviews")}
                  key={key}>
                  <ReviewsMain id={product.ID} />
                </TabContent>
              );
            }
            return (
              <TabContent key={header} label={key}>
                <Row>
                  <Col xs={12}>
                    <h3>{header}</h3>
                    {attributes
                      .sort((attr1, attr2) =>
                        attr1.SortOrder && attr2.SortOrder ? attr1.SortOrder - attr2.SortOrder : 0
                      )
                      .map((attr) => {
                        return (
                          <dl key={attr.CustomKey}>
                            <ProductDetailsTabAttribute product={product} attribute={attr} />
                          </dl>
                        );
                      })}
                  </Col>
                </Row>
              </TabContent>
            );
          })}
        </Tabs>
      </Col>
    </Row>
  );
};
