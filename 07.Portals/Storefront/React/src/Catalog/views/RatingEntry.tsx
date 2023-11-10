import { faStar as faStarFilled } from "@fortawesome/free-solid-svg-icons";
import { faStar } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useState } from "react";
import { useTranslation } from "react-i18next";

interface IRatingEntryProps {
  review?: any;
  scale: number;
  ratingAmount: number;
  setRatingAmount: Function;
}

export const RatingEntry = (props: IRatingEntryProps): JSX.Element => {
  const { scale, ratingAmount, setRatingAmount } = props;

  const [ratingHovered, setRatingHovered] = useState<number>(0);

  const { t } = useTranslation();

  const getStarIcon = (rating: number) => {
    return ratingHovered > 0
      ? rating <= ratingHovered
        ? faStarFilled
        : faStar
      : rating <= ratingAmount
      ? faStarFilled
      : faStar;
  };

  return (
    <div className="product-rating mb-0">
      {Array(scale)
        .fill(null)
        .map((_n: null, index: number) => {
          return (
            <FontAwesomeIcon
              className="fa-lg"
              key={index}
              icon={getStarIcon(index + 1)}
              onMouseEnter={() => setRatingHovered(index + 1)}
              onMouseLeave={() => setRatingHovered(0)}
              onClick={() => setRatingAmount(index + 1)}
            />
          );
        })}
      <span className="ml-2" id="SelectYourRatingText">
        {t("ui.storefront.product.reviews.ratingEntry.selectYourRating")}
      </span>
    </div>
  );
};
