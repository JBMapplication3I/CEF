import { Link } from "react-router-dom";
import { MicroCart } from "../Cart/views";
import { Categories } from "../_shared/Categories";
import { MiniMenu } from "../_shared/MiniMenu";
import { ExternalSearchBox } from "../Catalog/controls";
import ImageWithFallback from "../_shared/common/ImageWithFallback";
import { LanguageSelectorButton } from "../_shared/LanguageSelectorButton";
import { useTranslation } from "react-i18next";
import { Container, Row, Col } from "react-bootstrap";
export const NavMenu = (): JSX.Element => {
  const { t } = useTranslation();

  return (
    <Container as="header" fluid className="d-print-none" id="header">
      <div
        id="headerMid"
        className="bg-light row align-items-center"
        style={{ minHeight: "70px" }}>
        <Col xs={12} md="auto" className="xs-text-center sm-text-center">
          <Link
            to="/"
            className="navbar-brand"
            title="Clarity eCommerce Development Website">
            &nbsp;
            <ImageWithFallback
              className="img-fluid lazyloaded"
              src={"clarity-ecommerce-logo.png"}
              kind={"brands"}
              alt="Clarity Ventures Inc"
            />
          </Link>
        </Col>
        <Col className="form-inline">
          <ExternalSearchBox />
        </Col>
        <Col xs={12} xl="auto">
          <Row className="align-items-center h-100">
            <Col xs="auto" className="nav-item">
              <MicroCart type="Quote" />
            </Col>
            <Col xs="auto" className="nav-item">
              <MicroCart type="Cart" />
            </Col>
            <Col xs="auto" className="nav-item ml-auto dropdown">
              <MiniMenu />
            </Col>
          </Row>
          <Row className="align-items-center mb-2">
            <Col xs="auto" className="nav-item col-auto">
              <LanguageSelectorButton />
            </Col>
          </Row>
        </Col>
      </div>
      <Row id="headerBot">
        <Col xs={12} className="bg-dark-blue">
          <Container className="px-2">
            <div className="navbar navbar-expand-md navbar-dark bg-dark-blue row py-md-0">
              <button
                className="navbar-toggler"
                type="button"
                data-toggle="collapse"
                data-target="#headerBotContent"
                aria-controls="headerBotContent"
                aria-expanded="false"
                aria-label="Toggle Navigation">
                {t("ui.storefront.userDashboard.ToggleNavigation")}
                <div className="navbar-toggler-icon"></div>
              </button>
              <div className="collapse navbar-collapse" id="headerBotContent">
                <div className="navbar-nav">
                  <ul className="navbar-nav nav nav-pills">
                    <li className="nav-item dropdown mega-dropdown position-static">
                      <Link
                        className="nav-link text-white dropdown-toggle"
                        to="/catalog"
                        data-bs-toggle="dropdown"
                        aria-expanded="false">
                        {t("ui.storefront.common.Product.Plural")}
                      </Link>
                      <div className="dropdown-menu mega-menu w-100 top-auto">
                        <Categories />
                      </div>
                    </li>
                    <li className="nav-item dropdown">
                      <Link
                        className="nav-link text-white dropdown-toggle"
                        id="dropdownMenu"
                        to="/about"
                        data-toggle="dropdown"
                        role="button"
                        aria-haspopup="true"
                        aria-expanded="false">
                        {t("ui.storefront.common.AboutUs")}
                      </Link>
                      <ul
                        className="dropdown-menu"
                        aria-labelledby="dropdownMenu">
                        <li>
                          <Link className="dropdown-item" to="/">
                            Link 1
                          </Link>
                        </li>
                        <li>
                          <Link className="dropdown-item" to="/">
                            Link 2
                          </Link>
                        </li>
                        <li>
                          <Link className="dropdown-item" to="/">
                            Link 3
                          </Link>
                        </li>
                      </ul>
                    </li>
                    <li className="nav-item">
                      <Link className="nav-link text-white" to="/news">
                        News
                      </Link>
                    </li>
                    <li className="nav-item">
                      <Link className="nav-link text-white" to="/industry">
                        Industry
                      </Link>
                    </li>
                    <li className="nav-item">
                      <Link className="nav-link text-white" to="/request-info">
                        Request Info
                      </Link>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </Container>
        </Col>
      </Row>
    </Container>
  );
};
