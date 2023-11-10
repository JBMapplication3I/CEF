import React from "react";

interface ICarouselItemProps {
  active?: boolean;
  children?: React.ReactNode;
}

export const CarouselItem = (props: ICarouselItemProps): JSX.Element => {
  const { active, children } = props;
  return (
    <div className={`carousel-item ${active ? "active" : ""}`}>{children}</div>
  );
};
