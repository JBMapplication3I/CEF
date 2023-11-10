import {
  GeneralAttributeModel,
  ProductModel
} from "../../../_api/cvApi._DtoClasses";
import { InterweaveWrapper } from "../../../_shared/InterweaveWrapper";

interface IProductDetailsTabAttributeProps {
  product: ProductModel;
  attribute: GeneralAttributeModel;
}

export const ProductDetailsTabAttribute = (
  props: IProductDetailsTabAttributeProps
): JSX.Element => {
  const { product, attribute } = props;

  return (
    <div id={`${attribute.CustomKey}Text`}>
      <dt>{attribute.DisplayName || attribute.Name || attribute.CustomKey}</dt>
      <dd>
        <span>
          <InterweaveWrapper
            content={product.SerializableAttributes[attribute.CustomKey].Value}
            allowAttributes={true}
            allowElements={true}
          />
        </span>{" "}
        {!attribute.IsMarkup && (
          <span>
            {product.SerializableAttributes[attribute.CustomKey].UofM}
          </span>
        )}
      </dd>
    </div>
  );
};
