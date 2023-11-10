/* eslint-disable jsx-a11y/anchor-is-valid */
import { useEffect, useState } from "react";
import { AggregateTree } from "../_api/cvApi._DtoClasses";
import { Breadcrumb } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { corsLink } from "./common/Cors";
import { useCefConfig } from "./customHooks/useCefConfig";
import { CEFConfig } from "../_redux/_reduxTypes";
import classes from "./Breadcrumbs.module.scss";

interface IBreadcrumbsProps {
  currentCategory: string;
  currentQuery: string;
  categoriesTree: AggregateTree;
  onCategoryClicked: (cat: string) => void;
}

export const Breadcrumbs = (props: IBreadcrumbsProps): JSX.Element => {
  const { currentCategory, currentQuery, categoriesTree, onCategoryClicked } = props;

  const [breadcrumbsFromCatTree, setBreadcrumbsFromCatTree] = useState<string[]>([]);

  const { t } = useTranslation();
  const cefConfig: CEFConfig = useCefConfig();

  function showQueryOnly(): boolean {
    return currentQuery && !currentCategory;
  }

  function queryAndCatExist(): boolean {
    return !!currentCategory && !!currentQuery;
  }

  useEffect(() => {
    if (queryAndCatExist() || showQueryOnly()) {
      return;
    }
    if (categoriesTree && categoriesTree.Children && currentCategory) {
      let breadcrumbsList = getHeritageListForKey(categoriesTree, currentCategory);
      if (!breadcrumbsList) {
        // invalid category
        return;
      }
      let cleanedList = breadcrumbsList.filter((x) => x !== undefined && x !== "N/A");
      setBreadcrumbsFromCatTree(cleanedList);
    }
  }, [categoriesTree, currentCategory, currentQuery]);

  return (
    <>
      <Breadcrumb className="bg-light mb-2" listProps={{ className: "p-2" }}>
        <Breadcrumb.Item className="breadcrumbs-title" id="lblYouAreHereProducts">
          {t("ui.storefront.product.catalog.productCatalog.youAreHere")}
        </Breadcrumb.Item>
        <Breadcrumb.Item
          className={`${classes.noBefore}`}
          href={corsLink(cefConfig, cefConfig?.routes?.catalog?.root)}
          id="breadCatalogRootLink">
          {t("ui.storefront.product.catalog.productCatalog.Catalog")}
        </Breadcrumb.Item>
        {showQueryOnly() ? (
          <Breadcrumb.Item as="span">
            {t("ui.storefront.product.catalog.SearchResults")}
          </Breadcrumb.Item>
        ) : (
          breadcrumbsFromCatTree.map((crumb: string, index: number) => {
            return (
              <Breadcrumb.Item
                href="#"
                role="button"
                key={crumb}
                className={`${index ? "" : "pl-0"}`}
                onClick={() => onCategoryClicked(crumb)}>
                {crumb.split("|")[0]}
              </Breadcrumb.Item>
            );
          })
        )}
      </Breadcrumb>
    </>
  );
};

function getHeritageListForKey(treeNode: AggregateTree, desiredKey: string): string[] {
  if (treeNode.Key === desiredKey) {
    return [desiredKey];
  }

  if (treeNode.Children) {
    for (let child of treeNode.Children) {
      let result: string[] = getHeritageListForKey(child, desiredKey);
      if (result) {
        return [treeNode.Key, ...result];
      }
    }
  } else {
    return null;
  }
}
