import { NavMenu } from "./Navigation/NavMenu";
import { Footer } from "./_shared/Footer";

export const Layout = (props: any) => {
  return (
    <div>
      <NavMenu />
      <div className="container-tk main">{props.children}</div>
      <Footer />
    </div>
  );
};
