import { currencyFormatter } from "../../_shared/common/Formatters";
import { ProductCardControlsWidget } from "../controls/ProductCardControlsWidget/ProductCardControlsWidget";
import { ProductCardActionButtonWidget } from "../controls/ProductCardActionButtonWidget";
import { ProductModel } from "../../_api/cvApi.shared";
import { Card } from "react-bootstrap";

interface IProductCardProps {
  product: ProductModel;
  customClasses?: any;
}

export const ProductCard = (props: IProductCardProps) => {
  const { product, customClasses } = props;

  const { CustomKey, ID, Name, readInventory, readPrices, SeoUrl, ShortDescription } = product;

  let salePrice: number;
  let basePrice: number;

  let quantityAvailable: number;

  if (readPrices) {
    const { base, sale } = readPrices();
    basePrice = base;
    salePrice = sale;
  }

  if (readInventory) {
    const { QuantityOnHand } = readInventory();
    quantityAvailable = QuantityOnHand;
  }

  return (
    <div key={ID} className={`catalog-result-card ${customClasses ?? "col mb-3"}`}>
      <Card>
        <ProductCardControlsWidget product={product} render="overlay" hideShoppingList={true} />
        <Card.Body className="p-3">
          <div>
            &nbsp;
            {/* cef-product-card-Name-widget */}
            <div>
              <Card.Title>
                <a href={`/product?seoUrl=${SeoUrl}`}>
                  <span className="rows-2">{Name}</span>
                </a>
              </Card.Title>
            </div>
          </div>
          <div className="product-sku rows-1">
            <Card.Text className="my-1">
              <span className="code">SKU: {CustomKey}</span>
            </Card.Text>
          </div>
          <div className="card-text">
            <Card.Text className="my-1">
              <span className="rows-2 small" id="cardProductShortDesc_">
                {ShortDescription}
              </span>
            </Card.Text>
          </div>
          <div className="my-1 text-center">
            {salePrice !== basePrice ? (
              <span>
                <del className="text-danger small">
                  {basePrice != null ? currencyFormatter.format(basePrice) : "Price not available"}
                </del>
                &nbsp;
                <span className="text-success very-big">
                  {salePrice != null ? currencyFormatter.format(salePrice) : "Price not available"}
                </span>
              </span>
            ) : (
              <span className="text-body very-big">
                {salePrice != null ? currencyFormatter.format(salePrice) : "Price not available"}
              </span>
            )}
          </div>
        </Card.Body>
        <Card.Footer className="card-footer p-1">
          <ProductCardActionButtonWidget product={product} quantityAvailable={quantityAvailable} />
        </Card.Footer>
      </Card>
    </div>
  );
};
