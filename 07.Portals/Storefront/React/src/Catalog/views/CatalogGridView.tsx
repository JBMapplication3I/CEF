import React, { UIEvent, useState } from "react";
import { ProductModel } from "../../_api/cvApi._DtoClasses";
import { LoadingWidget } from "../../_shared/common/LoadingWidget";
import { ProductCard } from "./ProductCard";
import { Row } from "react-bootstrap";
let timeoutHolder: any;

interface ICatalogGridViewProps {
  products: Array<ProductModel>;
  onScrollToBottom?: Function;
  useInfinite?: boolean;
  parentRunning?: boolean;
}

export const CatalogGridView = ({
  products,
  onScrollToBottom,
  useInfinite,
  parentRunning
}: ICatalogGridViewProps) => {
  const [canCallScrollFn, setCanCallScrollFn] = useState(true);
  let style: React.CSSProperties = {};
  if (useInfinite) {
    style.maxHeight = "70vh";
    style.overflowY = "scroll";
    style.overflowX = "hidden";
  }
  return (
    <div
      className="grid-body"
      style={style}
      onScroll={(event: UIEvent<HTMLDivElement>) => {
        const { currentTarget } = event;
        const { scrollHeight, scrollTop, clientHeight } = currentTarget;
        const bottom = scrollHeight - Math.ceil(scrollTop) - 50 < clientHeight;
        if (useInfinite && bottom && onScrollToBottom && canCallScrollFn) {
          onScrollToBottom();
          setCanCallScrollFn(false);
          timeoutHolder = setTimeout(() => {
            setCanCallScrollFn(true);
            clearTimeout(timeoutHolder);
          }, 2000);
        }
      }}>
      <Row
        xs={1}
        sm={2}
        md={2}
        lg={3}
        xl={3}
        className="row-cols-fk-6 row-cols-tk-5">
        {products
          ? products.map((product) => {
              const { ID } = product;
              return <ProductCard product={product} key={ID} />;
            })
          : null}
      </Row>
      {parentRunning ? <LoadingWidget /> : null}
    </div>
  );
};
