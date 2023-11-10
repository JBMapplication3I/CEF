import { useTranslation } from "react-i18next";
import { Pagination, Container } from "react-bootstrap";
interface ICatalogPaginationControlsProps {
  page: number;
  pageSize: number;
  total: number;
  totalPages: number;
  navigateToPage: Function;
}

export const CatalogPaginationControls = (
  props: ICatalogPaginationControlsProps
) => {
  const { page, pageSize, total, totalPages, navigateToPage } = props;

  const { t } = useTranslation();

  return (
    <Container className="pagination-box text-center">
      <span className="showing">
        {t("ui.storefront.product.catalog.productCatalog.Showing")} &nbsp;
        <span className="show-card">
          {(page - 1) * pageSize + 1}
          {` to `}
          {page * pageSize >= total ? total : page * pageSize}
        </span>
        &nbsp; {t("ui.storefront.common.Of.Lowercase")} &nbsp;
        <span className="show-or">{total}</span>
      </span>
      <Pagination className="justify-content-center">
        <Pagination.Item
          disabled={page <= 1 ? true : false}
          onClick={() => page > 1 && navigateToPage(1)}
          aria-label="Previous">
          <span aria-hidden="true">&laquo;</span>
        </Pagination.Item>
        <Pagination.Item
          disabled={page <= 1 ? true : false}
          onClick={() => page > 1 && navigateToPage(page - 1)}
          aria-label="Previous">
          <span aria-hidden="true">&laquo;</span>
        </Pagination.Item>
        {Array(totalPages)
          .fill(null)
          .map((el, index): JSX.Element => {
            const num = index + 1;
            return (
              <Pagination.Item
                active={page === num ? true : false}
                onClick={() => navigateToPage(num)}
                key={num.toString()}>
                {num}
              </Pagination.Item>
            );
          })}
        <Pagination.Item
          disabled={page < totalPages ? false : true}
          onClick={() => {
            if (page < totalPages) {
              navigateToPage(page + 1);
            }
          }}
          aria-label="Next">
          <span aria-hidden="true">&raquo;</span>
        </Pagination.Item>
        <Pagination.Item
          disabled={page < totalPages ? false : true}
          onClick={() => {
            if (page < totalPages) {
              navigateToPage(totalPages);
            }
          }}
          aria-label="Next">
          <span aria-hidden="true">&raquo;</span>
        </Pagination.Item>
      </Pagination>
    </Container>
  );
};
