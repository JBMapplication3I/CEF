import { useTranslation } from "react-i18next";
import { Categories } from "../_shared/Categories";
import { useCefConfig } from "./customHooks/useCefConfig";
import { Dropdown } from "react-bootstrap";

type TCategoriesMenuBarRender = "mega" | "across" | "down";
type TCategoriesMenuBarRenderInner = "dropdown" | "mega-tabs" | "category-grid";

interface ICategoriesMenuBarProps {
  render: TCategoriesMenuBarRender;
  renderInner?: TCategoriesMenuBarRenderInner;
  behavior: Array<string>;
  text?: number;
  limitChildren?: number;
  limitGrandChildren?: number;
}

export const CategoriesMenuBar = (props: ICategoriesMenuBarProps) => {
  const { t } = useTranslation();
  const cefConfig = useCefConfig();

  if (!cefConfig || !cefConfig.featureSet.categories.enabled) {
    return null;
  }

  return (
    <Dropdown className="position-static">
      <Dropdown.Toggle
        variant="link"
        as="a"
        id="products-dropdown"
        className="nav-link text-white pointer">
        {t("ui.storefront.common.Product.Plural")}
      </Dropdown.Toggle>
      <Dropdown.Menu className="w-100">
        <Categories />
      </Dropdown.Menu>
    </Dropdown>
  );
};
