import React, { useState } from "react";
import {
  faChevronLeft,
  faChevronRight
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, ListGroup } from "react-bootstrap";
interface ICarouselProps {
  children: JSX.Element[] | React.ReactNode[];
  showPagination?: boolean;
  hideArrows?: boolean;
}

export const Carousel = (props: ICarouselProps): JSX.Element => {
  const { children, showPagination, hideArrows } = props;

  const [setToShow, setSetToShow] = useState<number>(0);

  const childrenWithActive: Array<JSX.Element> = children.map(
    (child: any, index: number) => {
      return React.cloneElement(child, {
        active: index === setToShow,
        key: index
      });
    }
  );

  return (
    <div className="carousel col-12" data-bs-ride="carousel">
      <div className="carousel-inner px-5">{childrenWithActive}</div>
      {hideArrows ? null : (
        <Button
          className="carousel-control-prev"
          onClick={() => {
            if (setToShow === 0) {
              setSetToShow(children.length - 1);
            } else {
              setSetToShow(setToShow - 1);
            }
          }}>
          <FontAwesomeIcon
            icon={faChevronLeft}
            className="fa-2x text-dark carousel-control-prev-icon"
          />
          <span className="sr-only">Previous</span>
        </Button>
      )}
      {hideArrows ? null : (
        <Button
          className="carousel-control-next"
          onClick={() => {
            if (setToShow < children.length - 1) {
              setSetToShow(setToShow + 1);
            } else if (setToShow === children.length - 1) {
              setSetToShow(0);
            }
          }}>
          <FontAwesomeIcon
            icon={faChevronRight}
            className="fa-2x text-dark carousel-control-next-icon"
          />
          <span className="sr-only">Next</span>
        </Button>
      )}
      {showPagination ? (
        <div className="pagination-slide">
          <ListGroup as="ul">
            {children.map((_child: React.ReactNode, index: number) => (
              <ListGroup.Item as="li" className="mt-4" key={index}>
                <Button
                  onClick={() => setSetToShow(index)}
                  className={`p-1 page-link border-0 ${
                    index === setToShow ? "active" : ""
                  }`}>
                  <span className="sr-only">{index}</span>
                </Button>
              </ListGroup.Item>
            ))}
          </ListGroup>
        </div>
      ) : null}
    </div>
  );
};
