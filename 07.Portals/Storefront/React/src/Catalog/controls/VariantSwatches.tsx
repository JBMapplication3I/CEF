import { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";

import { ProductAssociationModel } from "../../_api/cvApi._DtoClasses";

interface IVariantSwatchesProps {
  listOfVariants: ProductAssociationModel[], 
  onUpdateProductVariant: (seoUrl: string) => void
}

export const VariantSwatches = (props: IVariantSwatchesProps): JSX.Element => {
  const {
    listOfVariants,
    onUpdateProductVariant
  } = props;

  const [orderMode, setOrderMode] = useState<"order-one" | "order-multiple">("order-one");
  const [selectedColor, setSelectedColor] = useState<string>(listOfVariants[0].SlaveSerializableAttributes.Color.Value)
  const [selectedSize, setSelectedSize] = useState<string>(listOfVariants[0].SlaveSerializableAttributes.Size.Value)
  const [listOfColors, setListOfColors] = useState<any>(null);
  const [listOfSizes, setListOfSizes] = useState<any>(null);

  useEffect(() => {
    for (const variant of listOfVariants) {
      if (variant.SlaveSerializableAttributes.Color.Value === selectedColor && 
            variant.SlaveSerializableAttributes.Size.Value === selectedSize) {
        onUpdateProductVariant(variant.SlaveSeoUrl);
        return;
      }
    }
  }, [selectedColor, selectedSize]);

  useEffect(() => {
    const currListOfColors = listOfVariants.reduce((accum, variant) => {
      const currVariantColor = variant.SlaveSerializableAttributes?.Color?.Value;
      if (currVariantColor && !accum.includes(currVariantColor)) {
        accum.push(currVariantColor);
      }
      return accum;
    }, []);

    setListOfColors(currListOfColors.map((color) => {
      return {
        hexVal: color,
        text: ''
      };
    }));
  }, [listOfVariants]);

  useEffect(() => {
    const currListOfSizes = listOfVariants.reduce((accum, variant) => {
      const currVariantSize = variant.SlaveSerializableAttributes?.Size?.Value;
      if (currVariantSize && !accum.includes(currVariantSize)) {
        accum.push(currVariantSize);
      }
      return accum;
    }, []);

    setListOfSizes(currListOfSizes.map((size) => {
      return {
        value: size, 
        isAvailable: true
      };
    }));
  }, [listOfVariants]);

  const { t } = useTranslation();
  
  /* TODO: this function needs to tell the product details page(?) whether to order one type/variant
     of product at a time or multiple different types of products at a time */
  const changeMode = (mode: "order-one" | "order-multiple"): void => {
    console.log("Current order mode:", mode);
  }

  // TODO: will need to get the correct class names in here; different from the angular ones
  return (
    <div className={"product-info"}>
      <div className="form-wrap d-print-none ml-0">
        {/* <!-- start tabs --> */}
        <ul className="nav nav-tabs nav-order">
          <li className="nav-item">
            <a className="nav-link nav-smaller-text active text-uppercase text-danger"
              data-toggle="tab" href="#order-one" onClick={() => changeMode('order-one')}>
              {t(
                "ui.storefront.product.orderOne"
              )}</a>
          </li>
          <li className="nav-item">
            <a className="nav-link nav-smaller-text text-uppercase text-danger" data-toggle="tab"
              href="#order-multiple" onClick={() => changeMode('order-multiple')}>
              {t(
                "ui.storefront.product.orderMultiple"
              )}</a>
          </li>
        </ul>
        {/* <!-- variant options window --> */}
        <div className="tab-content">
          <div className="tab-pane fade show active pt-20 px-3">
            <div>
              <span className="d-block">
              {t(
                "ui.storefront.product.chooseColor"
              )}</span>
              {/* <!-- color options --> */}
              <ul className="list-unstyled mb-20 d-flex choose-color-list">
                {Array.isArray(listOfColors) &&
                  listOfColors.map((color): JSX.Element => {
                  return (
                    <li
                      key={color.hexVal}>
                      <label>
                        <input 
                          type="radio" 
                          name= "color-one-order"
                          checked={selectedColor === color.hexVal}
                          value={selectedColor}
                          onChange={() => {setSelectedColor(color.hexVal);}}
                          />
                        <span  
                          className="fake-input"
                          style={{backgroundColor:  color.hexVal }}></span>
                        <span className="fake-label text-uppercase w-100 text-center">
                          { color.text }<em className="sr-only">size { color.text } one order</em>
                        </span>
                      </label>
                    </li>);
                })}
              </ul>
              {listOfSizes?.length > 0 && selectedColor != null && <span className="d-block">
                {t(
                  "ui.storefront.product.chooseSize"
                )}
              </span>}
              {/* <!-- size options --> */}
              {selectedColor != null && 
                <div className="slick-slider size-slider mb-20 d-flex flex-wrap">
                  {listOfSizes != null &&
                    Array.isArray(listOfSizes) &&
                    listOfSizes.map((size): JSX.Element => {
                      return (
                      <div
                        key={size.value}>
                        <label>
                          {orderMode === 'order-multiple' && <input type="radio" name="size-one-order"
                            disabled={!size.isAvailable} checked={false} />}
                          {orderMode !== 'order-multiple' && 
                            <input 
                              type="radio" 
                              name="size-one-order"
                              checked={selectedSize === size.value} disabled={!size.isAvailable}
                              value={selectedSize}
                              onChange={() => {setSelectedSize(size.value)}} />}
                          <span className="fake-input"></span>
                          <span className="fake-label text-uppercase w-100 text-center">
                            { size.value }<em className="sr-only">size { size.value } one order</em>
                          </span>
                        </label>
                        {/* {orderMode === 'order-multiple' && 
                          <input id="amount-01" className="form-control" type="number"
                            placeholder="0" tabIndex={0}
                            disabled={!size.isAvailable} min="0"
                            value={selectedSize}
                            onChange={(e) => {
                              // console.log(e.target.value);
                              changeSelectedSize(size);
                            }}
                        />} */}
                        {/* <!-- ng-model="pvwCtrl.masterController.productVariantSelections[size.value].quantity" --> */}
                      </div>) 
                    }
                  )}
                </div>}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};