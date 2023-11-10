import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMapMarker } from "@fortawesome/free-solid-svg-icons";
import { StoreLocatorMarkerTip } from "./StoreLocatorMarkerTip";
import { useState } from "react";

interface IStoreLocatorMarkerProps {
  onMarkerClicked: Function;
  storeID: number;
  lat: number;
  lng: number;
  selected: boolean;
  useTooltip?: boolean;
  TooltipComponent?: any;
  clearSelectedStore: Function;
}

export const StoreLocatorMarker = (props: IStoreLocatorMarkerProps) => {
  const {
    onMarkerClicked,
    storeID,
    selected,
    useTooltip,
    TooltipComponent,
    clearSelectedStore
  } = props;

  const [showTooltip, setShowTooltip] = useState<boolean>(false);

  return (
    <div className="position-relative">
      {(useTooltip && showTooltip) || selected ? (
        <StoreLocatorMarkerTip
          onXButtonClicked={() => {
            setShowTooltip(false);
            clearSelectedStore();
          }}>
          <TooltipComponent />
        </StoreLocatorMarkerTip>
      ) : null}
      <FontAwesomeIcon
        icon={faMapMarker}
        className={`text-danger pointer fa-${
          selected || showTooltip ? "3" : "2"
        }x`}
        style={{ transform: "translate(-50%, -100%)" }}
        onClick={() => onMarkerClicked(storeID)}
        onMouseEnter={() => setShowTooltip(true)}
        onMouseLeave={() => setShowTooltip(false)}
      />
    </div>
  );
};
