import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useViewState } from "../_shared/customHooks/useViewState";
import cvApi from "../_api/cvApi";
import { AddressModel, StoreModel } from "../_api/cvApi._DtoClasses";
import { StoreLocatorCard } from "./StoreLocatorCard";
import { LoadingWidget } from "../_shared/common/LoadingWidget";
import axios from "axios";
import { connect } from "react-redux";
import { CEFConfig, IReduxStore } from "../_redux/_reduxTypes";
import { StoreLocatorMarker } from "./StoreLocatorMarker";
import GoogleMapReact from "google-map-react";
import { Form, Col, Row, InputGroup } from "react-bootstrap";

export type TCoords = {
  lat: number;
  lng: number;
};

interface IStoreLocatorProps {
  cefConfig: CEFConfig;
}

const mapStateToProps = (state: IReduxStore) => {
  return {
    cefConfig: state.cefConfig
  };
};

export const StoreLocator = connect(mapStateToProps)((props: IStoreLocatorProps) => {
  const { cefConfig } = props;

  const [zipCode, setZipCode] = useState<string>("");
  const [stores, setStores] = useState<Array<StoreModel>>([]);
  const [center, setCenter] = useState<TCoords>({
    lat: 30.3542409,
    lng: -97.7742702
  });
  const [IDOfselectedStore, setIDOfselectedStore] = useState<number>(null);
  const [radius, setRadius] = useState<number>(5);
  const [page, setPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(8);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  async function getAllStores() {
    setRunning();
    try {
      const getStoresResult = (
        await cvApi.stores.GetStores({
          // TODO: GetStores DTO
          // Radius: radius
          // ZipCode: zipCode
        })
      ).data;
      const stores = getStoresResult.Results;
      const storesToAssign = [];
      for (let i = 0; i < stores.length; i++) {
        const store = stores[i];
        const address = store.Contact.Address;
        if (address.Latitude != null && address.Longitude != null) {
          storesToAssign.push(store);
        } else {
          const googleLatLngResult: any = await getLatLngFromAddress(address, cefConfig);
          const latLngResult: TCoords = googleLatLngResult.data.results[0].geometry.location;
          const { lat, lng } = latLngResult;
          store.Contact.Address.Latitude = lat;
          store.Contact.Address.Longitude = lng;
          storesToAssign.push(store);
        }
      }
      if (zipCode.length >= 5) {
        const centerQuery = await axios.get(
          `https://maps.googleapis.com/maps/api/geocode/json?address=${zipCode}&key=${cefConfig.google.maps.apiKey}`
        );
        const center = centerQuery.data.results[0].geometry.location;
        const { lat, lng } = center;
        setCenter({
          lat,
          lng
        });
      }
      setStores(storesToAssign);
      finishRunning();
    } catch (err: any) {
      finishRunning(true, err);
    }
  }

  useEffect(() => {
    setRunning();
    getAllStores();
  }, [cefConfig]);

  const TooltipBody = (props: any) => {
    return (
      <ul>
        <li>
          <input value="Test" readOnly />
        </li>
        <li>Another</li>
        <li>More</li>
      </ul>
    );
  };

  if (viewState.running) {
    return (
      <Row className="mt-4 mb-5">
        <Col xs={12}>
          <div className="d-flex flex-column align-items-center">
            <h3>{t("ui.storefront.common.Loading.Ellipses")}</h3>
            <LoadingWidget />
          </div>
        </Col>
      </Row>
    );
  }

  return (
    <>
      <Row className="mt-4 mb-5">
        <Col xs={12}>
          <div className="d-flex flex-column align-items-center">
            <h3>{t("ui.storefront.location.customerLocationEntry.findStores")}</h3>
            <p>{t("ui.storefront.cart.cartShippingEstimator.enterYourZipOrPostalCode")}</p>
            <Form.Group>
              <InputGroup>
                <input
                  className="p-2 rounded-start border-1"
                  value={zipCode}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => setZipCode(e.target.value)}
                  onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>) => {
                    if (e.key === "Enter") {
                      e.preventDefault();
                      if (zipCode.length >= 5) {
                        getAllStores();
                      }
                    }
                  }}
                  placeholder={t(
                    "ui.storefront.cart.cartShippingEstimator.enterYourZipOrPostalCode"
                  )}
                />
                <select
                  className="input-group-append rounded-end"
                  onChange={(e) => setRadius(+e.target.value)}
                  value={radius}>
                  {[5, 10, 15].map((n) => {
                    return (
                      <option key={n.toString()} value={n}>
                        {`${n} mi`}
                      </option>
                    );
                  })}
                </select>
              </InputGroup>
            </Form.Group>
          </div>
        </Col>
      </Row>
      {stores.length ? (
        <Row className="mt-4 mb-5" style={{ height: "70vh" }}>
          <Col xs={6} style={{ maxHeight: "100%", overflowY: "auto" }}>
            {stores.map((store) => {
              return (
                <StoreLocatorCard
                  key={`storeCard_${store.ID}`}
                  store={store}
                  onStoreClicked={(id: number) => {
                    setIDOfselectedStore(id);
                    setCenter({
                      lat: store.Contact.Address.Latitude,
                      lng: store.Contact.Address.Longitude
                    });
                  }}
                  selected={IDOfselectedStore === store.ID}
                />
              );
            })}
          </Col>
          <Col xs={6}>
            <GoogleMapReact
              bootstrapURLKeys={{
                key: cefConfig.google.maps.apiKey
              }}
              key={`${center.lat}${center.lng}`}
              defaultCenter={center}
              defaultZoom={10}>
              {stores.map((store) => {
                return (
                  <StoreLocatorMarker
                    key={store.ID}
                    lat={store.Contact.Address.Latitude}
                    lng={store.Contact.Address.Longitude}
                    storeID={store.ID}
                    onMarkerClicked={(id: number) => setIDOfselectedStore(id)}
                    clearSelectedStore={() => setIDOfselectedStore(null)}
                    selected={IDOfselectedStore === store.ID}
                    useTooltip={true}
                    TooltipComponent={TooltipBody}
                  />
                );
              })}
            </GoogleMapReact>
          </Col>
        </Row>
      ) : (
        <h3 className="text-center mb-5">No stores found</h3>
      )}
    </>
  );
});

function getLatLngFromAddress(address: AddressModel, cefConfig: CEFConfig) {
  if (!address || !cefConfig) {
    return new Promise((resolve, reject) => {
      reject("Address or CefConfig unavailable; cannot fetch address");
    });
  }
  const { Street1, City, Region } = address;
  const addressQueryString = `${Street1}%20${City}%20${Region}`;
  return axios.get(
    `https://maps.googleapis.com/maps/api/geocode/json?address=${addressQueryString}&key=${cefConfig.google.maps.apiKey}`
  );
}
