import { useEffect, useState } from "react";
import Card from "react-bootstrap/Card";
import { useTranslation } from "react-i18next";
import { AddressBlock } from "../Dashboard/AddressBook/AddressBlock";
import { StoreModel } from "../_api/cvApi._DtoClasses";
import classes from "./StoreLocatorCard.module.scss";
import axios from "axios";
import { useCefConfig } from "../_shared/customHooks/useCefConfig";
import { CEFConfig } from "../_redux/_reduxTypes";
import { Button, Col, Row, Table } from "react-bootstrap";

interface IStoreCardProps {
  store: StoreModel;
  onStoreClicked: Function;
  selected: boolean;
  useSmallText?: boolean;
}

interface IStoreHoursItem {
  day: string;
  open: number;
  close: number;
}

const weekDays = [
  "Monday",
  "Tuesday",
  "Wednesday",
  "Thursday",
  "Friday",
  "Saturday",
  "Sunday"
];

export const StoreLocatorCard = (props: IStoreCardProps) => {
  const { store, onStoreClicked, selected, useSmallText } = props;
  const { Name, ContactFirstName, ContactLastName, Contact } = store;
  const { Address } = Contact;

  const [storeHours, setStoreHours] = useState<any>([]);
  const [currentUserCoords, setCurrentUserCoords] = useState(null);
  const [currentUserFormattedAddress, setCurrentUserFormattedAddress] =
    useState<string>(null);
  const cefConfig = useCefConfig();
  const { t } = useTranslation();

  useEffect(() => {
    if (store) {
      setStoreHours(buildStoreHoursArray(store));
    }
  }, [store]);

  useEffect(() => {
    if (navigator && navigator.permissions) {
      navigator.permissions.query({ name: "geolocation" }).then((status) => {
        if (status.state !== "denied") {
          navigator.geolocation.getCurrentPosition(
            (locationData) => {
              setCurrentUserCoords({
                lat: locationData.coords.latitude,
                lng: locationData.coords.longitude
              });
              if (currentUserFormattedAddress != null) {
                return;
              }
              getAddressFromLatLng(
                {
                  lat: locationData.coords.latitude,
                  lng: locationData.coords.longitude
                },
                cefConfig
              )
                .then((res: any) => {
                  setCurrentUserFormattedAddress(
                    res.data.results[0].formatted_address
                  );
                })
                .catch((err) => {
                  console.log(err);
                  console.log("Unabled to get user's address with coordinates");
                });
            },
            (_err) => {
              console.log(_err);
            }
          );
        }
      });
    }
  }, [cefConfig]);

  const getGetDirectionsLink = (): string => {
    const root = "http://maps.google.com/maps";
    if (currentUserFormattedAddress !== null) {
      return `${root}/dir/${encodeURI(
        `${Address.Street1}, ${Address.City}, ${Address.RegionCode} ${Address.PostalCode}`
      )} /${encodeURI(currentUserFormattedAddress)}`;
    }
    return `${root}/place/${encodeURI(
      `${Address.Street1}, ${Address.City}, ${Address.RegionCode} ${Address.PostalCode}`
    )}`;
  };

  return (
    <Card
      className={`shadow mb-4 pointer ${
        selected ? "border-dark border-1" : ""
      }`}
      style={{ width: "100%" }}
      onClick={() => onStoreClicked(store.ID)}>
      <div className="d-flex">
        <Card.Body className={classes.storeCard}>
          <Card.Title className="mb-4">{Name}</Card.Title>
          <Card.Subtitle style={{ fontWeight: 400 }}>
            {ContactFirstName} {ContactLastName}
          </Card.Subtitle>
          <Row>
            <Col xs={12} sm={5}>
              <AddressBlock
                address={{
                  ...Contact,
                  ...Contact.Address,
                  Name: Contact.FirstName + Contact.LastName
                }}
              />
            </Col>
            <Col xs={12} sm={7}>
              <Table
                size="sm"
                borderless
                className={`store-hours ${useSmallText ? "small" : ""}`}>
                <tbody>
                  {storeHours.length
                    ? getCollapsedDaysWithHours(storeHours).map(
                        (dayObj: IStoreHoursItem) => {
                          const { day, open, close } = dayObj;
                          return (
                            <tr key={dayObj.day}>
                              <td>
                                <b>{day}&nbsp;&nbsp;&nbsp;&nbsp;</b>
                              </td>
                              {!open ? (
                                <td>
                                  {t(
                                    "ui.storefront.location.shipTo.shipToDetails.Closed"
                                  )}
                                </td>
                              ) : null}
                              {open !== null ? (
                                <td className="text-right">
                                  {militaryToTwelveHour(open)}
                                </td>
                              ) : null}
                              {close !== null ? <td>&nbsp;-&nbsp;</td> : null}
                              {close !== null ? (
                                <td>{militaryToTwelveHour(close)}</td>
                              ) : null}
                            </tr>
                          );
                        }
                      )
                    : null}
                </tbody>
              </Table>
            </Col>
          </Row>
          <Row>
            <Col xs={12} sm={6}></Col>
            <Col xs={12} sm={6}>
              <Button
                as="a"
                variant="primary"
                className="w-100"
                href={getGetDirectionsLink()}
                target="_blank"
                rel="noopener noreferrer">
                {t("ui.storefront.common.GetDirection.Plural")}
              </Button>
            </Col>
          </Row>
        </Card.Body>
      </div>
    </Card>
  );
};

function getAddressFromLatLng(
  coords: { lat: number; lng: number },
  cefConfig: CEFConfig
) {
  if (!cefConfig) {
    return new Promise((_resolve, reject) => {
      reject("CefConfig unavailable");
    });
  }
  return axios.get(
    `https://maps.googleapis.com/maps/api/geocode/json?&latlng=${coords.lat},${coords.lng}&key=${cefConfig.google.maps.apiKey}`
  );
}

function decimalToMilitary(n: number): number {
  return Math.round(n * 100);
}

function militaryToTwelveHour(n: number): string {
  const suffix = n >= 1200 ? "pm" : "am";
  let hr = n === 1200 ? n : n % 1200;
  const hoursAsString = hr.toString().padStart(4, "0");
  const hours = hoursAsString.substr(0, 2).replace(/0/g, "");
  const minutes = hoursAsString.substr(hoursAsString.length - 2);
  const time = hours + ":" + minutes + " " + suffix;
  return time;
}

function buildStoreHoursArray(store: StoreModel): Array<IStoreHoursItem> {
  const storeHours = weekDays.reduce((acc: any, day) => {
    // @ts-ignore
    const open = store[`OperatingHours${day}Start`];
    // @ts-ignore
    const close = store[`OperatingHours${day}End`];

    const openInMilitary = open != null ? decimalToMilitary(open) : null;
    const closeInMilitary = close != null ? decimalToMilitary(close) : null;
    acc[day] = {
      open: openInMilitary,
      close: closeInMilitary
    };
    return acc;
  }, {});

  let arr: Array<IStoreHoursItem> = [];
  for (const day in storeHours) {
    arr.push({
      day: day,
      ...storeHours[day]
    });
  }
  return arr;
}

function getCollapsedDaysWithHours(
  storeHours: IStoreHoursItem[]
): IStoreHoursItem[] {
  return storeHours.reduce(
    (existingArr: IStoreHoursItem[], currentValue: IStoreHoursItem) => {
      if (!existingArr.length) {
        return [currentValue];
      }
      const prevStoreHoursItem = existingArr[existingArr.length - 1];
      const previousDay = prevStoreHoursItem.day;
      const previousOpen = prevStoreHoursItem.open;
      const previousClose = prevStoreHoursItem.close;
      const currentDay = currentValue.day;
      const currentOpen = currentValue.open;
      const currentClose = currentValue.close;
      if (previousOpen === currentOpen && previousClose === currentClose) {
        // collapse
        return [...existingArr, currentValue];
      } else {
        return [...existingArr, currentValue];
      }
    },
    []
  );
}
