import {
  faStar as faStarFilled,
  faStarHalfAlt
} from "@fortawesome/free-solid-svg-icons";
import { faStar } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconProp } from "@fortawesome/fontawesome-svg-core";
import { useTranslation } from "react-i18next";

interface IRatingSummaryProps {
  scale: number;
  value: number; // average
  count: number; // amount of ratings
  hideCount?: boolean;
}

export const RatingSummary = (props: IRatingSummaryProps): JSX.Element => {
  const { scale, value, count, hideCount } = props;
  const { t } = useTranslation();

  const getStarIcon = (rating: number): IconProp => {
    if (rating < value) {
      return faStarFilled;
    }
    const difference = Math.round(10 * (rating - value)) / 10;
    if (difference < 0.3) {
      return faStarFilled;
    }
    if (difference < 0.9) {
      return faStarHalfAlt;
    }
    return faStar;
  };

  return (
    <div className="product-rating">
      {Array(scale)
        .fill(null)
        .map((_n: null, index: number) => {
          return (
            <FontAwesomeIcon
              key={index}
              icon={getStarIcon(index + 1)}
              className="fa-lg"
            />
          );
        })}
      <span className="ml-1">
        {`${Math.round(10 * value) / 10}/${scale}`}
        &nbsp;
        {!hideCount ? (
          <span id="UserRatingsText">
            <span>
              {`(${count} ${t(
                "ui.storefront.product.reviews.ratingSummary.userRating.Plural"
              )})`}
            </span>
          </span>
        ) : null}
      </span>
    </div>
  );
};
