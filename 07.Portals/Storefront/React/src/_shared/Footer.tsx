import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faMapMarker,
  faPhoneSquare,
  faAt
} from "@fortawesome/free-solid-svg-icons";
import {
  faFacebook,
  faTwitter,
  faLinkedin,
  faCcVisa,
  faCcPaypal,
  faCcMastercard,
  faCcAmex,
  faCcDiscover
} from "@fortawesome/free-brands-svg-icons";
import ImageWithFallback from "../_shared/common/ImageWithFallback";
import { useTranslation } from "react-i18next";

export const Footer = (): JSX.Element => {
  const { t } = useTranslation();

  return (
    <footer
      id="footer"
      className="container-fluid bg-dark-blue text-light py-4 d-print-none text-white-50">
      {/* <div id="footerTop2" className="row">
        <div className="col-12">
          <div cef-brand-formatting-footer></div>
        </div>
      </div> */}
      <div id="footerTop" className="row"></div>
      <div id="footerMid" className="row">
        <div className="order-1 col-12 order-sm-1 col-sm-6 order-md-1 col-md-4 order-lg-1 col-lg-3 lg-ml-auto order-xl-1 col-xl-2 xl-ml-auto">
          <a
            id="btnFooterLogo"
            className="mb-1"
            title="Logo"
            href="https://www.clarity-ventures.com/"
            rel="noopener noreferrer"
            target="_blank">
            <ImageWithFallback
              className="img-fluid lazyload"
              alt="Clarity Ventures Website"
              width="259"
              height="74"
              src={"clarity-logo-white-on-black.png"}
              kind="brands"
            />
          </a>
          <p className="text-center small mt-2">
            The Clarity eCommerce platform takes you where the other stores
            can&apos;t go. It allows full customization of integrations,
            reporting, styling, tracking, inventory... the list goes on.
          </p>
        </div>
        <div className="order-2 col-7 order-sm-2 col-sm-6 order-md-5 col-md-4 order-lg-5 col-lg-3 lg-mr-auto order-xl-5 col-xl-auto xl-mr-auto mb-3">
          <h4 className="text-uppercase small text-light">
            {t("ui.storefront.common.ContactUs")}
          </h4>
          <div className="row no-gutters align-items-center mb-3">
            <div className="col-auto">
              <div className="border border-light rounded-circle p-2">
                <FontAwesomeIcon icon={faMapMarker} className="fa-fw fa-lg" />
              </div>
            </div>
            <div className="col pl-3" id="companyAddress">
              <address className="mb-0 small">
                <span className="address">
                  6805 Capital of Texas Hwy
                  <br />
                  Suite 312
                  <br />
                  Austin, TX 78731
                </span>
              </address>
            </div>
          </div>
          <div className="row no-gutters align-items-center mb-3">
            <div className="col-auto">
              <div className="border border-light rounded-circle p-2">
                <FontAwesomeIcon icon={faPhoneSquare} className="fa-fw fa-lg" />
              </div>
            </div>
            <div className="col pl-3" id="companyPhone">
              <a className="text-white-50 small" href="tel:800-928-8160">
                800-928-8160
              </a>
            </div>
          </div>
          <div className="row no-gutters align-items-center mb-3">
            <div className="col-auto">
              <div className="border border-light rounded-circle p-2">
                <FontAwesomeIcon icon={faAt} className="fa-fw fa-lg" />
              </div>
            </div>
            <div className="col pl-3" id="companyEmail">
              <a
                className="text-white-50 small"
                href="mailto:rfq@clarity-ventures.com">
                rfq@clarity-ventures.com
              </a>
            </div>
          </div>
        </div>
        <div className="order-3 col-5 xs-text-right order-sm-6 col-sm-6 order-md-6 col-md-auto order-lg-6 col-lg-auto lg-mr-auto order-xl-6 col-xl-6 xl-text-right col-tk-auto">
          <a href="https://www.facebook.com/ClarityTeam" title="Facebook">
            <FontAwesomeIcon
              icon={faFacebook}
              className="fa-3x text-white-50"
            />
          </a>
          <a href="https://twitter.com/ClarityTeam" title="Twitter">
            <FontAwesomeIcon icon={faTwitter} className="fa-3x text-white-50" />
          </a>
          <a href="https://www.linkedin.com/company/1155055" title="LinkedIn">
            <FontAwesomeIcon
              icon={faLinkedin}
              className="fa-3x text-white-50"
            />
          </a>
        </div>
        <div className="order-4 col-6 order-sm-3 col-sm-6 order-md-2 col-md-auto order-lg-2 col-lg-auto order-xl-2 col-xl-auto">
          <h4 className="text-uppercase small text-light">Shopping Guide</h4>
          <ul className="links pl-3 small">
            <li>
              <a className="text-white-50" title="FAQs">
                {t("ui.storefront.storeDashboard.storeMenu.SupportAndFAQs")}
              </a>
            </li>
            <li>
              <a className="text-white-50" title="Payment">
                {t("ui.storefront.checkout.checkoutPanels.Payment")}
              </a>
            </li>
            <li>
              <a className="text-white-50" title="Shipment">
                Shipment
              </a>
            </li>
            <li>
              <a className="text-white-50" title="Where is my order?">
                Where is my order?
              </a>
            </li>
            <li className="last">
              <a className="text-white-50" title="Return policy">
                Return policy
              </a>
            </li>
          </ul>
        </div>
        <div className="order-5 col-6 order-sm-4 col-sm-6 order-md-3 col-md-auto order-lg-3 col-lg-auto order-xl-3 col-xl-auto">
          <h4 className="text-uppercase small text-light">Features</h4>
          <ul className="links pl-3 small">
            <li className="first Profile">
              <a className="text-white-50">
                <span>
                  {t("ui.storefront.userDashboard2.controls.YourAccount")}
                </span>
              </a>
            </li>
            <li>
              <Link to="/dashboard/wishList" className="text-white-50">
                <span>{t("ui.storefront.menu.miniMenu.wishList")}</span>
              </Link>
            </li>
            <li>
              <Link to="/dashboard/favoritesList" className="text-white-50">
                <span>{t("ui.storefront.menu.miniMenu.FavoritesList")}</span>
              </Link>
            </li>
            <li>
              <Link to="/dashboard/addressBook" className="text-white-50">
                <span>{t("ui.storefront.menu.miniMenu.addressBook")}</span>
              </Link>
            </li>
            <li>
              <Link to="/dashboard/inStockAlerts" className="text-white-50">
                <span>{t("ui.storefront.menu.miniMenu.inStockAlerts")}</span>
              </Link>
            </li>
            <li className="last Orders">
              <Link to="/dashboard/orderHistory" className="text-white-50">
                <span>
                  {t(
                    "ui.storefront.storeDashboard.storeOrderHistory.OrderHistory"
                  )}
                </span>
              </Link>
            </li>
          </ul>
        </div>
        <div className="order-6 col-6 order-sm-5 col-sm-6 order-md-4 col-md-auto order-lg-4 col-lg-auto order-xl-4 col-xl-auto">
          <h4 className="text-uppercase small text-light">
            {t("ui.storefront.storeDashboard.storeProfileEditor.Information")}
          </h4>
          <ul className="links pl-3 small">
            <li>
              <Link
                to="/contact-us"
                className="text-white-50"
                title={t("ui.storefront.common.ContactUs") as string}>
                {t("ui.storefront.common.ContactUs")}
              </Link>
            </li>
            <li>
              <Link
                to="/terms-and-conditions"
                className="text-white-50"
                title={t("ui.storefront.common.TermsAndConditions")}>
                {t("ui.storefront.common.TermsAndConditions")}
              </Link>
            </li>
            <li>
              <Link to="/privacy" className="text-white-50" title="Privacy">
                Privacy
              </Link>
            </li>
            {/* <li><a className="text-white-50" ui-sref-plus uisrp-root="/Stores" title="Our stores" className="link-rss">Our Stores</a></li> */}
          </ul>
        </div>
        <div className="order-7 col-12 xs-text-center order-sm-7 col-sm-12 sm-text-center order-md-7 col-md-auto order-lg-7 col-lg-auto lg-ml-auto order-xl-7 col-xl-6 col-tk-auto mr-tk-auto">
          <FontAwesomeIcon icon={faCcPaypal} className="fa-3x mr-1" />
          <FontAwesomeIcon icon={faCcVisa} className="fa-3x mr-1" />
          <FontAwesomeIcon icon={faCcMastercard} className="fa-3x mr-1" />
          <FontAwesomeIcon icon={faCcAmex} className="fa-3x mr-1" />
          <FontAwesomeIcon icon={faCcDiscover} className="fa-3x mr-1" />
        </div>
      </div>
      <div id="footerBot" className="row mt-3">
        <div className="col-12 text-center small" id="copyright-entry">
          Copyright 2021 by Clarity Ventures |&nbsp;
          <a
            className="text-white-50"
            href="https://www.clarity-ventures.com/"
            target="_blank"
            rel="noopener noreferrer">
            Enterprise eCommerce By Clarity
          </a>
        </div>
      </div>
    </footer>
  );
};
