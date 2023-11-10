import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAt, faFax, faMapMarkerAlt, faPhone } from "@fortawesome/free-solid-svg-icons";
import classes from "./AddressBlock.module.scss";
import { Table } from "react-bootstrap";
import { t } from "i18next";
interface IAddressBlockProps {
  address: {
    Company?: string;
    PostalCode?: string;
    CountryKey?: string;
    Name: string;
    FirstName?: string;
    LastName?: string;
    Street1?: string;
    Street2?: string;
    City?: string;
    RegionName?: string;
    Phone1?: string;
    Fax1?: string;
    Email1?: string;
  };
  hideCompany?: boolean;
  hidePostalCode?: boolean;
  hideCountryKey?: boolean;
  hideName?: boolean;
  hideFirstName?: boolean;
  hideLastName?: boolean;
  hideStreet1?: boolean;
  hideStreet2?: boolean;
  hideCity?: boolean;
  hideRegionName?: boolean;
  hidePhone1?: boolean;
  hideFax1?: boolean;
  hideEmail1?: boolean;
}

export const AddressBlock = (props: IAddressBlockProps): JSX.Element => {
  const {
    address,
    hideCompany,
    hidePostalCode,
    hideCountryKey,
    hideName,
    hideFirstName,
    hideLastName,
    hideStreet1,
    hideStreet2,
    hideCity,
    hideRegionName,
    hidePhone1,
    hideFax1,
    hideEmail1
  } = props;

  const {
    Name,
    FirstName,
    LastName,
    Company,
    Street1,
    Street2,
    City,
    RegionName,
    PostalCode,
    CountryKey,
    Phone1,
    Fax1,
    Email1
  } = address;

  if (!Name && !FirstName && !Street1) {
    return <span></span>;
  }

  return (
    <Table borderless size="sm" className={classes.addressBlockTable}>
      <tbody>
        {!hideName && (Name || FirstName) && (
          <tr>
            <td>
              <FontAwesomeIcon icon={faMapMarkerAlt} className="fa-fw fa-lg" />
            </td>
            <td>
              <span>
                {Name ? Name : FirstName && LastName ? FirstName + " " + LastName : FirstName}
              </span>
            </td>
          </tr>
        )}
        {!hideCompany && Company && (
          <tr>
            <td></td>
            <td>{Company}</td>
          </tr>
        )}
        {!hideStreet1 && Street1 && (
          <tr>
            <td></td>
            <td>{Street1}</td>
          </tr>
        )}
        {!hideStreet2 && Street2 && (
          <tr>
            <td></td>
            <td>{Street2}</td>
          </tr>
        )}
        {!hideCity && !hideRegionName && !hidePostalCode && (
          <tr>
            <td></td>
            <td>
              {!hideCity ? City : null}
              {!hideCity && City ? "," : ""} {!hideRegionName ? RegionName : null}{" "}
              {!hidePostalCode ? PostalCode : null}
            </td>
          </tr>
        )}
        {!hideCountryKey && CountryKey && (
          <tr>
            <td></td>
            <td>{CountryKey}</td>
          </tr>
        )}
        {!hidePhone1 && Phone1 && (
          <tr>
            <td>
              <FontAwesomeIcon icon={faPhone} className="fa-fw fa-lg" />
            </td>
            <td>
              <a href={Phone1}>{Phone1}</a>
            </td>
          </tr>
        )}
        {!hideFax1 && Fax1 && (
          <tr>
            <td>
              <FontAwesomeIcon icon={faFax} className="fa-fw fa-lg" />
            </td>
            <td>
              <a href={Fax1}>{Fax1}</a>
            </td>
          </tr>
        )}
        {!hideEmail1 && Email1 && (
          <tr>
            <td>
              <FontAwesomeIcon icon={faAt} className="fa-fw fa-lg" />
            </td>
            <td>
              <a className="contactAddress_email" href={Email1}>
                {Email1}
              </a>
            </td>
          </tr>
        )}
      </tbody>
    </Table>
  );
};
