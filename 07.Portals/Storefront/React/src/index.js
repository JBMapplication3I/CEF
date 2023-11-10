import ReactDOM from "react-dom";
import App from "./App";
import "bootstrap/dist/js/bootstrap.min.js";
import registerServiceWorker from "./registerServiceWorker";
import { BrowserRouter, Route } from "react-router-dom";
import { QueryParamProvider } from "use-query-params";
import { Provider } from "react-redux";
import { store } from "./_redux/store/store";
import { MicroCart } from "./Cart/views";
import { Categories } from "./_shared/Categories";
import { Login } from "./_shared/Login";
import { MiniMenu } from "./_shared/MiniMenu";
import { Registration } from "./Authentication/Registration/Registration";
import { MiniCart } from "./Cart/views/MiniCart";
import { Cart } from "./Cart";
import { Compare } from "./Catalog/views/Compare";
import { Catalog } from "./Catalog/Catalog";
import { AuctionCatalog } from "./Catalog/AuctionCatalog";
import { Checkout } from "./Checkout/Checkout";
import { Dashboard } from "./Dashboard/Dashboard";
import { Home } from "./Home/Home";
import { NavMenu } from "./Navigation/NavMenu";
import { ExternalSearchBox } from "./Catalog/controls";
import { ForgotPassword } from "./Authentication/ForgotPassword";
import { ProductDetails } from "./Catalog";
import { Footer } from "./_shared/Footer";
import { HeaderMid } from "./_shared/HeaderMid";
import { ForcedPasswordReset } from "./Authentication/ForcedPasswordReset";
import { ForgotUsername } from "./Authentication/ForgotUsername";
import { CategoriesMenuBar } from "./_shared/CategoriesMenuBar";
import { LanguageSelectorButton } from "./_shared/LanguageSelectorButton";
import { StoreLocator } from "./StoreLocator/StoreLocator";
import { Translate } from "./_shared/Translate";
import CefConfigKeyEnabled from "./_shared/CefConfigKeyEnabled";
import { Icon } from "./_shared/Icon";
import "./_meta/js/i18n";
import { Suspense } from "react";
import { LoadingWidget } from "./_shared/common/LoadingWidget";

const componentMap = {
  AuctionCatalog: AuctionCatalog,
  Cart: Cart,
  Catalog: Catalog,
  Categories: Categories,
  CategoriesMenuBar: CategoriesMenuBar,
  CefConfigKeyEnabled: CefConfigKeyEnabled,
  Checkout: Checkout,
  Compare: Compare,
  Dashboard: Dashboard,
  ExternalSearchBox: ExternalSearchBox,
  Footer: Footer,
  ForcedPasswordReset: ForcedPasswordReset,
  ForgotPassword: ForgotPassword,
  ForgotUsername: ForgotUsername,
  HeaderMid: HeaderMid,
  Home: Home,
  Icon: Icon,
  LanguageSelectorButton: LanguageSelectorButton,
  Login: Login,
  MicroCart: MicroCart,
  MiniCart: MiniCart,
  MiniMenu: MiniMenu,
  NavMenu: NavMenu,
  ProductDetails: ProductDetails,
  Registration: Registration,
  StoreLocator: StoreLocator,
  Translate: Translate
};

// eslint-disable-next-line no-unused-vars
const registerComponent = (id, component) => {
  componentMap[id] = component;
};

const getComponentById = (id) => {
  /*
  When fetching a component, it is important to return null in case of failure,
  because rendering null will not cause a React error as opposed to rendering undefined.
  */
  if (id) {
    return componentMap[id] || null;
  }
  return null;
};

const parseJsonProps = (JsonProps) => {
  try {
    return JSON.parse(JsonProps);
  } catch (err) {
    return {};
  }
};

const getFallback = (props, hideSuspenseDNNComponents, componentId) => {
  if (props?.hideSuspense || hideSuspenseDNNComponents.includes(componentId)) {
    return "";
  }
  if (props?.suspenseMessage) {
    return props.suspenseMessage;
  }
  return <LoadingWidget size="3x" innerClasses="p-2" />;
};

const baseElement = document.getElementsByTagName("base")[0];
const rootElement = document.getElementById("cef-react-root");
if (rootElement) {
  console.log(`NODE_ENV=${process.env.NODE_ENV}, SPA MODE`);

  const baseUrl = baseElement.getAttribute("href");
  ReactDOM.render(
    <Provider store={store}>
      <BrowserRouter basename={baseUrl}>
        <Suspense fallback={"...Loading"}>
          <App />
        </Suspense>
      </BrowserRouter>
    </Provider>,
    rootElement
  );

  registerServiceWorker();
} else {
  console.log(`NODE_ENV=${process.env.NODE_ENV}, DNN MODE`);

  document.addEventListener("DOMContentLoaded", (e) => {
    const roots = document.querySelectorAll("[data-react-component]");
    for (let i = 0; i < roots.length; i++) {
      const root = roots[i];
      const componentId = root.dataset.reactComponent;
      const componentJsonProps = root.dataset.reactProps;
      const CompTemp = getComponentById(componentId);
      const hideSuspenseDNNComponents = ["CategoriesMenuBar"];
      if (!CompTemp) {
        console.log(
          `Failed to find React component by supplied ID ${componentId} from DNN`
        );
        continue;
      }
      const props = parseJsonProps(componentJsonProps);
      let ChildComp;
      if (props.children) {
        ChildComp = getComponentById(props.children);
      }
      ReactDOM.render(
        <Provider store={store}>
          <BrowserRouter basename="/">
            <QueryParamProvider ReactRouterRoute={Route}>
              <Suspense
                fallback={getFallback(
                  props,
                  hideSuspenseDNNComponents,
                  componentId
                )}>
                <CompTemp {...props}>
                  {props.children ? <ChildComp /> : null}
                </CompTemp>
              </Suspense>
            </QueryParamProvider>
          </BrowserRouter>
        </Provider>,
        root
      );
    }
  });
}
