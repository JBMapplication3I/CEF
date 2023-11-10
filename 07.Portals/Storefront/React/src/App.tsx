import { connect } from "react-redux";
import { Redirect, Route, Switch } from "react-router";
import { Layout } from "./Layout";
import { Home } from "./Home/Home";
import { Cart } from "./Cart/Cart";
import { Catalog } from "./Catalog/Catalog";
import { Checkout } from "./Checkout/Checkout";
import { ProductDetails } from "./Catalog/ProductDetails";
import { Dashboard } from "./Dashboard/Dashboard";
import { Registration } from "./Authentication/Registration/Registration";
import { ForgotPassword } from "./Authentication/ForgotPassword";
import { ForgotUsername } from "./Authentication/ForgotUsername";
import { Privacy } from "./Legal/Privacy";
import { createBrowserHistory } from "history";
import { ForcedPasswordReset } from "./Authentication/ForcedPasswordReset";
import { Row, Col } from "react-bootstrap";
import "./_meta/css/clarity.scss";
import { Compare } from "./Catalog/views/Compare";
import { StoreLocator } from "./StoreLocator/StoreLocator";

export const history = createBrowserHistory({
  basename: process.env.PUBLIC_URL
});

const App = (props: any): JSX.Element => {
  const { isAuthenticated, userChecked } = props;

  const authenticatedRoutes = [
    <Route key="/dashboard" path="/dashboard" component={Dashboard} />
  ];

  const unauthenticatedRoutes = [
    <Route
      key="/authentication/forgot-password"
      path="/authentication/forgot-password">
      <Row>
        <Col xs={12} sm={8} md={6} lg={5} className="mx-auto my-5">
          <ForgotPassword />
        </Col>
      </Row>
    </Route>,
    <Route
      key="/authentication/forgot-username"
      path="/authentication/forgot-username">
      <Row>
        <Col xs={12} sm={8} md={6} lg={5} className="mx-auto my-5">
          <ForgotUsername />
        </Col>
      </Row>
    </Route>,
    <Route
      key="/authentication/password-reset"
      path="/authentication/password-reset">
      <Row>
        <Col xs={12} sm={8} md={6} lg={5} className="mx-auto my-5">
          <ForcedPasswordReset />
        </Col>
      </Row>
    </Route>
  ];

  return (
    <Layout>
      <Switch>
        <Route exact path="/" component={Home} />
        <Route path="/cart" component={Cart} />
        <Route path="/catalog" component={Catalog} />
        <Route path="/compare" component={Compare} />
        <Route path="/checkout" component={Checkout} />
        <Route path="/product" component={ProductDetails} />
        <Route path="/privacy-statement" component={Privacy} />
        <Route path="/store-locator" component={StoreLocator} />
        <Route
          key="/authentication/registration"
          path="/authentication/registration"
          component={Registration}
        />
        {userChecked
          ? isAuthenticated
            ? [...authenticatedRoutes]
            : [...unauthenticatedRoutes]
          : [...authenticatedRoutes, unauthenticatedRoutes]}
        <Redirect to="/" />
      </Switch>
    </Layout>
  );
};

const mapStateToProps = (state: any) => {
  return {
    isAuthenticated: state.currentUser.Contact,
    userChecked: state.currentUser.userChecked
  };
};

export default connect(mapStateToProps)(App);
