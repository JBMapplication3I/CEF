/* eslint-disable react-hooks/exhaustive-deps */
import { connect, ConnectedComponent } from "react-redux";
import { CEFConfig, IDashboardSettings } from "../_redux/_reduxTypes";
import { useEffect, useState } from "react";
import { Route, Switch, useLocation, useParams } from "react-router";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faClipboardList,
  faCoffee,
  faFileInvoiceDollar,
  faQuoteRight,
  faReceipt,
  faTachometerAlt,
  IconDefinition
} from "@fortawesome/free-solid-svg-icons";
import { faBell, faHeart, faStar, faUserCircle } from "@fortawesome/free-regular-svg-icons";
import cvApi from "../_api/cvApi";
import { useTranslation } from "react-i18next";

// Dashboard components
import { AddressBook } from "./AddressBook/AddressBook";
import { MyProfile } from "./MyProfile/MyProfile";
import { Invoices } from "./Invoices/Invoices";
import { Orders } from "./Orders/Orders";
import { WishList } from "./WishList/WishList";
import { ShoppingLists } from "./ShoppingList/ShoppingLists";
import { ShoppingListsDetail } from "./ShoppingList/ShoppingListsDetail";
import { Wallet } from "./Wallet/Wallet";
import { AccountProfile } from "./AccountProfile/AccountProfile";
import { InStockAlerts } from "./InStockAlerts/InStockAlerts";
import { Favorites } from "./Favorites/Favorites";
import { Quotes } from "./Quotes/Quotes";
import { SalesGroup } from "./SalesGroup/SalesGroup";
import { Col, Row } from "react-bootstrap";
import { UserModel } from "../_api/cvApi._DtoClasses";
interface IDashboardProps {
  currentUser: UserModel; // redux
  cefConfig?: CEFConfig; //redux
}
interface IMapStateToDashboardProps {
  currentUser: UserModel;
  cefConfig: CEFConfig;
}

const mapStateToProps = (state: IMapStateToDashboardProps) => {
  return {
    currentUser: state.currentUser,
    cefConfig: state.cefConfig
  };
};

export const Dashboard = connect(mapStateToProps)(
  (props: IDashboardProps): JSX.Element => {
    const { cefConfig, currentUser } = props;

    // const [recentOrders, setRecentOrders] = useState([]);
    const [dashboardLinks, setDashboardLinks] = useState<
      Array<IDashboardSettings>
    >([]);

    const { t } = useTranslation();

    useEffect(() => {
      if (
        cefConfig &&
        cefConfig.dashboard &&
        cefConfig.featureSet.login.enabled &&
        currentUser.Contact
      ) {
        populateDashboardLinks();
      }
    }, [cefConfig, currentUser]);

    async function populateDashboardLinks() {
      let enabledLinks: Array<IDashboardSettings> = cefConfig.dashboard
        .filter((x: IDashboardSettings) => x.enabled)
        .sort(
          (linkOne: IDashboardSettings, linkTwo: IDashboardSettings) =>
            linkOne.order - linkTwo.order
        );
      const linksToSet: Array<IDashboardSettings> = await getUsableLinks(
        enabledLinks
      );
      setDashboardLinks(linksToSet);
    }

    async function getUsableLinks(links: Array<IDashboardSettings>) {
      const linksToSet: Array<IDashboardSettings> = [];
      for (let i = 0; i < links.length; i++) {
        const link = links[i];
        const roleRequired = link.reqAnyRoles && link.reqAnyRoles.length;
        const permissionRequired = link.reqAnyPerms && link.reqAnyPerms.length;
        if (!link.enabled) {
          continue;
        }
        if (!roleRequired && !permissionRequired) {
          linksToSet.push(link);
          continue;
        }
        if (roleRequired) {
          const roleResults = [];
          for (let j = 0; j < link.reqAnyRoles.length; j++) {
            const role = link.reqAnyRoles[j];
            let userHasRole;
            try {
              await cvApi.authentication.CurrentUserHasRole({ Name: role });
              userHasRole = true;
            } catch (err) {
              userHasRole = false;
            }
            roleResults.push(userHasRole);
          }
          if (roleResults.includes(true)) {
            linksToSet.push(link);
            continue;
          }
        }
        if (permissionRequired) {
          const permissionResults = [];
          for (let j = 0; j < link.reqAnyPerms.length; j++) {
            const permission = link.reqAnyPerms[i];
            let userHasPermission;
            try {
              await cvApi.authentication.CurrentUserHasPermission({
                Name: permission
              });
              userHasPermission = true;
            } catch (err) {
              userHasPermission = false;
            }
            permissionResults.push(userHasPermission);
          }
          if (permissionResults.includes(true)) {
            linksToSet.push(link);
          }
        }
      }
      return linksToSet;
    }

    const isLinkActive = (link: IDashboardSettings): boolean => {
      const linkIsActive = pathName === link.href;
      const childOfLinkIsActive = link?.children
        ?.map((c) => c.href)
        .includes(pathName);
      return linkIsActive || childOfLinkIsActive;
    };

    // @ts-ignore
    const { id } = useParams();
    const pathName = useLocation().pathname;
    const breadCrumbs = pathName.slice(1).split("/");

    const dashboardTabComponents: {
      [name: string]: {
        component: (() => JSX.Element)
          | ConnectedComponent<(props: any) => JSX.Element, [string]>;
        detailComponent?:
          | (() => JSX.Element)
          | ConnectedComponent<(props: any) => JSX.Element, [string]>;
        icon?: IconDefinition;
      };
    } = {
      MyDashboard: {
        component: () => (
          <Row>
            <Col xs={12}>
              <h1>{t("ui.storefront.common.MyDashboard")}</h1>
            </Col>
            <Col xs={12}>
              <div>{/* Kendo Grid showing recent orders, TO DO */}</div>
            </Col>
            <div className="pb-4">
              <Orders isDashboardMain={true} />
            </div>
            <div className="pb-4">
              <Invoices isDashboardMain={true} />
            </div>
            <div className="pb-4">
              <Quotes isDashboardMain={true} />
            </div>
          </Row>
        ),
        icon: faTachometerAlt
      },
      AccountSettings: { component: (): null => null, icon: faUserCircle },
      MyProfile: { component: MyProfile },
      AccountProfile: { component: AccountProfile },
      AddressBook: { component: AddressBook },
      Wallet: { component: Wallet },
      Orders: {
        component: () => <Orders isDashboardMain={false} />,
        icon: faReceipt
      },
      Invoices: {
        component: () => <Invoices isDashboardMain={false} />,
        icon: faFileInvoiceDollar
      },
      Quotes: {
        component: () => <Quotes isDashboardMain={false} />,
        icon: faQuoteRight
      },
      WishList: { component: WishList, icon: faHeart },
      Favorites: { component: Favorites, icon: faStar },
      InStockAlerts: { component: InStockAlerts, icon: faBell },
      ShoppingLists: {
        component: ShoppingLists,
        icon: faClipboardList,
        detailComponent: ShoppingListsDetail
      },
      SalesGroup: { component: SalesGroup, icon: faCoffee }
    };

    const renderedDashboardPaths: IDashboardSettings[] = dashboardLinks
      .reduce((accu, e) => {
        const combined = [...accu, e];
        if (e.children) {
          combined.push(...e.children);
        }
        return combined;
      }, [])
      .filter((x: IDashboardSettings) => x.enabled && x.href)
      .sort(
        (linkOne: IDashboardSettings, linkTwo: IDashboardSettings) =>
          linkOne.order - linkTwo.order
      );

    const includeSalesGroupDetail: boolean = !!renderedDashboardPaths.find(
      (link) =>
        link.name === "Orders" ||
        link.name === "Invoices" ||
        link.name === "Quotes"
    );

    const getRouterRoutes = () => {
      const routes = [];
      renderedDashboardPaths.forEach((link) => {
        if (dashboardTabComponents[link.name]?.component) {
          routes.push(
            <Route
              exact
              key={link.href}
              path={link.href}
              component={dashboardTabComponents[link.name].component}
            />
          );
        }
        if (dashboardTabComponents[link.name]?.detailComponent) {
          routes.push(
            <Route
              exact
              key={link.href + "/detail/:typeName"}
              path={link.href + "/detail/:typeName"}
              component={dashboardTabComponents[link.name].detailComponent}
            />
          );
        }
      });
      if (includeSalesGroupDetail) {
        routes.push(
          <Route
            key="/dashboard/sales-group/:salesGroupId/:type/:typeId"
            path="/dashboard/sales-group/:salesGroupId/:type/:typeId"
            component={SalesGroup}
          />
        );
      }
      return routes;
    };

    return (
      <main id="main" role="main">
        <Row>
          <Col xs="auto" className="d-print-none cef-dashboard-nav bg-light">
            <nav className="navbar navbar-expand-md navbar-light pt-lg-0">
              <div className="navbar-header pb-2">
                <button
                  className="navbar-toggler"
                  type="button"
                  data-toggle="collapse"
                  data-target="#cef-dashboard-nav-collapse"
                  aria-label="Toggle Navigation"></button>
              </div>
              <div
                className="collapse navbar-collapse"
                id="cef-dashboard-nav-collapse">
                <div className="navbar-nav w-100">
                  <ul className="nav flex-column w-100">
                    {dashboardLinks.map((link) => {
                      return (
                        <li className="nav-item" key={link.href || link.name}>
                          <Link
                            to={
                              link.href ||
                              (link.children && link.children.length
                                ? link.children[0].href
                                : cefConfig.routes.dashboard.root)
                            }
                            className={`nav-link ${
                              isLinkActive(link) ? "active" : ""
                            }`}>
                            {dashboardTabComponents[link.name]?.icon && (
                              <FontAwesomeIcon
                                icon={dashboardTabComponents[link.name]?.icon}
                                className="mb-0 me-1"
                              />
                            )}
                            <span className="nav-text">{t(link.titleKey)}</span>
                          </Link>
                          {link.children && (
                            <ul
                              className="nav flex-column user-dashboard-sub-nav"
                              aria-labelledby="asideDrop">
                              {link.children
                                .filter((child) => child.enabled && child.href)
                                .map((child) => {
                                  return (
                                    <li
                                      key={child.href || child.name}
                                      className="nav-item">
                                      <Link
                                        to={child.href}
                                        className={`nav-link ${
                                          isLinkActive(child) ? "active" : ""
                                        }`}>
                                        <span className="text-capitalize">
                                          {t(child.titleKey)}
                                        </span>
                                      </Link>
                                    </li>
                                  );
                                })}
                            </ul>
                          )}
                        </li>
                      );
                    })}
                  </ul>
                </div>
              </div>
            </nav>
          </Col>
          <Col className="col-pr-12 cef-dashboard-content">
            <Row>
              <Col xs={12}>
                <Row className="row d-none d-sm-block d-print-none">
                  <nav className="breadcrumbs">
                    <ol className="breadcrumb bg-transparent">
                      {breadCrumbs.map((crumb, index): JSX.Element => {
                        return (
                          <li key={crumb} className="breadcrumb-item">
                            <Link
                              className="text-capitalize"
                              to={
                                index === 0
                                  ? `/${crumb}`
                                  : `/dashboard/${crumb}`
                              }>
                              {crumb}
                            </Link>
                          </li>
                        );
                      })}
                    </ol>
                  </nav>
                </Row>
              </Col>
            </Row>
            <Switch>
              {getRouterRoutes()}
            </Switch>
          </Col>
        </Row>
      </main>
    );
  }
);
