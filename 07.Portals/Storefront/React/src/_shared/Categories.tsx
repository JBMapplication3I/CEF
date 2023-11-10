import { useEffect, useState } from "react";
import { faAngleDoubleRight } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { ErrorView } from "./common/ErrorView";
import { LoadingWidget } from "./common/LoadingWidget";
import { useViewState } from "./customHooks/useViewState";
import cvApi from "../_api/cvApi";
import { CategoryModel } from "../_api/cvApi._DtoClasses";
import { useTranslation } from "react-i18next";
import { Nav, ListGroup, Tab, Row, Col, Button } from "react-bootstrap";

export const Categories = (): JSX.Element => {
  const [categories, setCategories] = useState<Array<CategoryModel>>([]);
  const [lastHovered, setLastHovered] = useState<string | null>(null);

  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    getCategories();
  }, []);

  function getCategories(): void {
    setRunning();
    cvApi.categories
      .GetCategoriesThreeLevels()
      .then((result) => {
        setCategories(result.data);
        finishRunning();
      })
      .catch((err) => {
        finishRunning(true, err.message || "Failed to get categories");
      });
  }

  if (!categories || !categories.length) {
    return <LoadingWidget />;
  }

  return (
    <>
      <div className="mega-menu-tabset w-100 d-flex" id="mega-menu-tabset">
        <Nav className="nav-tabs nav-stacked flex-column pt-1 border-right">
          <Nav.Item as="a" href="/catalog" className="small p-0">
            <span className="px-3 py-2 text-uppercase">
              {t("ui.storefront.menu.menuRenderProductVerticalTab.BrowseCategories")}
            </span>
          </Nav.Item>
          <hr className="m-2" />
          {categories !== null && Array.isArray(categories)
            ? categories.map((category): React.ReactNode => {
                const { Name, CustomKey } = category;
                if (category.ParentName) {
                  return null;
                }
                return (
                  <Nav.Item
                    key={Name}
                    as="a"
                    href={`/catalog?category=${Name}|${CustomKey}`}
                    className={`nav-link p-0 ${lastHovered === Name ? "active" : ""}`}
                    onMouseOver={() => setLastHovered(Name)}>
                    <span
                      className={`cef-nav-item dropdown-item text-capitalize ${
                        lastHovered === Name ? "active" : ""
                      }`}>
                      {Name}
                    </span>
                  </Nav.Item>
                );
              })
            : null}
        </Nav>
        <Tab.Content className="p-4" style={{ flexBasis: 0, flexGrow: 1, maxWidth: "100%" }}>
          {categories != null &&
            Array.isArray(categories) &&
            categories.map((category): JSX.Element => {
              const { Name, CustomKey } = category;
              return (
                <Tab.Pane
                  key={Name}
                  active={lastHovered === Name ? true : false}
                  id={"v-" + Name}
                  aria-labelledby={"v-" + Name + "-tab"}>
                  <Row>
                    <Col xs={12}>
                      <div className="mb-3">
                        <span className="dropdown-secondary-header h2 mb-2 text-capitalize">
                          {Name}
                        </span>
                        <Button
                          as="a"
                          variant="link"
                          className="mb-2 pb-0 text-primary"
                          href={`/Catalog?category=${Name}|${CustomKey}`}>
                          {t("ui.storefront.menu.menuRenderVerticalTabPane.seeAll")}
                          <FontAwesomeIcon icon={faAngleDoubleRight} className="ml-1" />
                        </Button>
                      </div>
                    </Col>
                    {category.Children ? (
                      category.Children.map((child): JSX.Element => {
                        return (
                          <Col
                            sm={4}
                            md={3}
                            key={child.Name}
                            className="mb-2 dropdown-tertiary-container">
                            <p className="h5 dropdown-tertiary-header mb-2">
                              <a className="bold" href={`/catalog?category=${child.Name}|${child.CustomKey}`}>
                                <span className="text-capitalize">{child.Name}</span>
                              </a>
                            </p>
                            <div>
                              <ListGroup as="ul" className="list-unstyled">
                                {child.Children && Array.isArray(child.Children) ? (
                                  child.Children.map((grandChild): JSX.Element => {
                                    return (
                                      <ListGroup.Item key={grandChild.CustomKey}>
                                        <a
                                          key={grandChild.Name}
                                          href={`/Catalog?category=${grandChild.Name}|${grandChild.CustomKey}`}>
                                          {grandChild.Name}
                                        </a>
                                      </ListGroup.Item>
                                    );
                                  })
                                ) : (
                                  <ListGroup.Item className="dropdown-tertiary-item">
                                    {t(
                                      "ui.storefront.menu.menuRenderVerticalTabPane.noAdditionalCategories"
                                    )}
                                  </ListGroup.Item>
                                )}
                                <ListGroup.Item className="dropdown-tertiary-item">
                                  <a
                                    href={`/catalog?category=${child.Name}|${child.CustomKey}`}
                                    className="bold">
                                    <span>{t("ui.storefront.menu.menuRenderVerticalTabPane.seeAll")}</span>&nbsp;
                                    <FontAwesomeIcon icon={faAngleDoubleRight} />
                                  </a>
                                </ListGroup.Item>
                              </ListGroup>
                            </div>
                          </Col>
                        );
                      })
                    ) : (
                      <Col xs={12}>
                        <p className="h5 mb-2">
                          {t("ui.storefront.menu.menuRenderVerticalTabPane.noAdditionalCategories")}
                        </p>
                      </Col>
                    )}
                  </Row>
                </Tab.Pane>
              );
            })}
        </Tab.Content>
      </div>
      <ErrorView error={viewState.errorMessage} />
    </>
  );
};
