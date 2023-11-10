import React, { useEffect, useState, useRef } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMinus, faPlus } from "@fortawesome/free-solid-svg-icons";
import { InputGroup, Button, Form } from "react-bootstrap";
import classes from "./AddToCartQuantitySelector.module.scss";

interface IAddToCartQuantitySelectorProps {
  onChange: Function;
  initialValue?: number;
  id?: number;
  increaseDisabled?: boolean;
  decreaseDisabled?: boolean;
  useInput?: boolean;
}

export const AddToCartQuantitySelector = (props: IAddToCartQuantitySelectorProps): JSX.Element => {
  const { onChange, initialValue, id, increaseDisabled, decreaseDisabled, useInput } = props;
  const [quantity, setQuantity] = useState(initialValue ?? 1);

  const firstRender = useRef(true);

  useEffect(() => {
    if (firstRender.current) {
      firstRender.current = false;
      return;
    }
    onChange(quantity);
  }, [quantity]);

  const onChangeQuantity = (e: React.ChangeEvent<HTMLInputElement>) => {
    e.preventDefault();
    setQuantity(+e.target.value);
  };

  const incrementQuantity = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    setQuantity(quantity + 1);
  };

  const decrementQuantity = (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault();
    if (quantity <= 1) {
      return;
    }
    setQuantity(quantity - 1);
  };

  if (!id) {
    console.log("No id was provided to the AddToCartQuantitySelector.");
  }

  return (
    <InputGroup className="flex-nowrap w-100">
      <InputGroup.Text className="p-0">
        <Button
          variant="outline-secondary"
          disabled={decreaseDisabled}
          id={`btnReduceItemQuantity${props.id ?? ""}`}
          onClick={decrementQuantity}>
          <FontAwesomeIcon icon={faMinus} />
        </Button>
      </InputGroup.Text>
      {useInput ? (
        <Form.Control
          className={`text-center qty ${classes.addToCartQuantityInput}`}
          style={{
            minWidth: `calc(${quantity.toString().length}ch + 40px)`
          }}
          aria-label="num"
          type="number"
          placeholder="1"
          value={quantity}
          onChange={onChangeQuantity}
        />
      ) : (
        <span
          className={`form-control text-center qty ${classes.addToCartQuantityInput}`}
          style={{
            minWidth: `calc(${quantity.toString().length}ch + 40px)`
          }}>
          {quantity}
        </span>
      )}
      <InputGroup.Text className="p-0">
        <Button
          variant="outline-secondary"
          disabled={increaseDisabled}
          id={`btnIncreaseItemQuantity${props.id ?? ""}`}
          onClick={incrementQuantity}>
          <FontAwesomeIcon icon={faPlus} />
        </Button>
      </InputGroup.Text>
    </InputGroup>
  );
};

AddToCartQuantitySelector.defaultProps = {
  useInput: true
};
