import React, { useState, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import {
  Button,
  Col,
  Card,
  Accordion,
  ListGroup,
  InputGroup,
  Form
} from "react-bootstrap";
import { useLocation } from "react-router-dom";
import { CatalogFilterHeaderButton } from "./CatalogFilterHeaderButton";
import { ISearchTermFilterProps } from "./_CatalogFilterComponentsTypes";
import cvApi from "../../../_api/cvApi";
import { useViewState } from "../../../_shared/customHooks/useViewState";
import { useTranslation } from "react-i18next";
import { SuggestProductCatalogWithProviderDto } from "../../../_api";
import { useCefConfig } from "../../../_shared/customHooks/useCefConfig";
import { CEFConfig } from "../../../_redux/_reduxTypes";
import { SearchSort } from "../../../_api/cvApi._DtoClasses";

export const SearchTermFilter = (props: ISearchTermFilterProps) => {
  const {
    expandedFilterName,
    setExpandedFilterName,
    productSearchViewModel,
    setProductSearchViewModel
  } = props;

  const [customSearchInput, setCustomSearchInput] = useState("");
  const [cardMaxHeight, setCardMaxHeight] = useState(0);
  const [suggestions, setSuggestions] = useState<Array<any>>([]);
  const cefConfig = useCefConfig() as CEFConfig;

  const location = useLocation();
  const { t } = useTranslation();
  const { setRunning, finishRunning, viewState } = useViewState();

  useEffect(() => {
    if (
      productSearchViewModel?.Form?.Query &&
      productSearchViewModel?.Form?.Query !== customSearchInput
    ) {
      setCustomSearchInput(productSearchViewModel?.Form?.Query);
    }
  }, [location]);

  const onSubmitSearch = (
    e:
      | React.MouseEvent<HTMLButtonElement>
      | React.KeyboardEvent<HTMLInputElement>
  ): void => {
    setProductSearchViewModel((productSearchViewModel) => {
      return {
        ...productSearchViewModel,
        Form: {
          ...productSearchViewModel.Form,
          Page: 1,
          Category: undefined,
          Query: customSearchInput || undefined
        }
      };
    });
    e.preventDefault();
  };

  const onSearchInputKeyDown = (
    e: React.KeyboardEvent<HTMLInputElement>
  ): void => {
    if (e.key === "Enter") {
      onSubmitSearch(e);
      return;
    }
    // TODO need to implement debounce on this
    const value = (e.target as HTMLInputElement).value;
    if (!value || !value.length) {
      setSuggestions([]);
      return;
    }
    setRunning();
    cvApi.providers
      .SuggestProductCatalogWithProvider({
        Query: value,
        Page: 1,
        PageSetSize: cefConfig?.catalog.defaultPageSize || 9,
        PageSize: cefConfig?.catalog.defaultPageSize || 9,
        Sort:
          (cefConfig?.catalog.defaultSort as SearchSort) || SearchSort.Relevance
      } as SuggestProductCatalogWithProviderDto)
      .then((result) => {
        setSuggestions(result.data);
        finishRunning();
      })
      .catch((err: any) => {
        finishRunning(true, err);
      });
  };

  return (
    <Col sm={6} md={12} lg={12}>
      <Card className="applied-filters mb-1">
        <Card.Header className="p-2 bg-light text-body">
          <CatalogFilterHeaderButton
            expandedFilterName={expandedFilterName}
            setExpandedFilterName={setExpandedFilterName}
            title={"Search Term"}
            icon={faSearch}
          />
        </Card.Header>
        <Accordion.Collapse
          eventKey="search-collapse"
          className={`show overflow-hidden transition-max-height`}
          style={{
            maxHeight:
              expandedFilterName === "Search Term"
                ? `${cardMaxHeight === 0 ? 350 : cardMaxHeight}px`
                : "0px"
          }}>
          <Card.Body
            className="p-3 bg-light"
            style={{ maxHeight: "50vh", overflowY: "auto" }}>
            <InputGroup>
              <Form.Control
                type="text"
                placeholder={t(
                  "ui.storefront.searchCatalog.controls.externalSearchBox.Search.Ellipses"
                )}
                aria-label="Search"
                role="search"
                value={customSearchInput}
                // TODO: needs debounce
                onKeyDown={onSearchInputKeyDown}
                onChange={(e) => setCustomSearchInput(e.target.value)}
              />
              <InputGroup.Text className="p-0">
                <Button
                  variant="outline-secondary"
                  size="lg"
                  className="p-1 pl-3 pr-3"
                  onClick={(e) => onSubmitSearch(e)}
                  type="button">
                  <FontAwesomeIcon icon={faSearch} />
                </Button>
              </InputGroup.Text>
              {suggestions.length ? (
                <Card.Body
                  className="position-absolute border bg-white"
                  style={{ top: "105%", overflow: "visible" }}>
                  <ListGroup as="ul" className="list-unstyled mb-0">
                    {suggestions.map((suggestion) => (
                      <Accordion.Item
                        key={suggestion.Name}
                        eventKey="search-collapse"
                        className="border-0 p-2"
                        style={{ top: "105%", overflow: "visible" }}>
                        <a href={`/product?seoUrl=${suggestion.SeoUrl}`}>
                          {suggestion.Name}
                        </a>
                      </Accordion.Item>
                    ))}
                  </ListGroup>
                </Card.Body>
              ) : null}
            </InputGroup>
          </Card.Body>
        </Accordion.Collapse>
        {/* <SizeMe monitorHeight={true}>
          {({ size }) => {
            const { height } = size;
            return (
              <div
                className={`card-collapse show overflow-hidden transition-max-height`}
                style={{
                  maxHeight:
                    expandedFilterName === "Search Term"
                      ? `${cardMaxHeight === 0 ? 350 : cardMaxHeight}px`
                      : "0px"
                }}
                id="search-collapse">
                <div
                  className="card-body p-3 bg-light"
                  style={{ maxHeight: "50vh", overflowY: "auto" }}>
                  <div className="input-group">
                    <input
                      className="form-control"
                      type="text"
                      placeholder={t("ui.storefront.searchCatalog.controls.externalSearchBox.Search.Ellipses")}
                      aria-label={t("ui.storefront.searchCatalog.controls.externalSearchBox.Search.Ellipses")}
                      role="search"
                      value={customSearchInput}
                      onKeyDown={onSearchInputKeyPress}
                      onChange={(e) => setCustomSearchInput(e.target.value)}
                    />
                    <div className="input-group-append">
                      <button
                        className="btn btn-outline-secondary"
                        type="button"
                        onClick={(e) => onSubmitSearch(e)}>
                        <FontAwesomeIcon icon={faSearch} />
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            );
          }}
        </SizeMe> */}
      </Card>
    </Col>
  );
};
