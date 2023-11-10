import { useState } from "react";
import { Fragment } from "react";
import {
  AddToCartButton,
  AddToCartQuantitySelector
} from "../../Cart/controls";

interface IProductCardActionButtonWidgetProps {
  product: any;
  quantity?: number;
  quantityAvailable?: number;
}

export const ProductCardActionButtonWidget = (
  props: IProductCardActionButtonWidgetProps
): JSX.Element => {
  const { product, quantity, quantityAvailable } = props;
  const [localQuantity, setLocalQuantity] = useState(
    quantityAvailable ? 1 : quantity ? 1 : null
  );

  return (
    <Fragment>
      <div className="d-block mb-1">
        <AddToCartQuantitySelector
          id={product.ID}
          initialValue={quantity ?? 1}
          onChange={(val: number) => setLocalQuantity(val)}
        />
      </div>
      <AddToCartButton
        classes="btn btn-block btn-lg btn-primary w-100 text-white"
        quantity={localQuantity}
        product={product}
        useConfirmModal={true}
      />
    </Fragment>
  );
};
