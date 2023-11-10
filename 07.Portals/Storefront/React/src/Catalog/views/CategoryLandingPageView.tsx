import { CategoryModel } from "../../_api/cvApi._DtoClasses";
import { Row, Col, Button, Card } from "react-bootstrap";
interface ICategoryLandingPageViewProps {
  categories: CategoryModel[];
  onCategoryClicked: Function;
  paramsCategory: string;
}

export const CategoryLandingPageView = (
  props: ICategoryLandingPageViewProps
): JSX.Element => {
  const { categories, onCategoryClicked, paramsCategory } = props;
  const [Name, CustomKey] = paramsCategory.split("|");
  const currentCat: CategoryModel = categories.find(
    (cat) => cat.Name === Name && cat.CustomKey === CustomKey
  );

  return (
    <Row>
      {currentCat.Children.map((child) => {
        return (
          <Col xs={12} sm={6} lg={4} xl={3} key={child.CustomKey}>
            <Button
              className="w-100"
              variant=""
              style={{ fontWeight: "bold" }}
              onClick={() =>
                onCategoryClicked(`${child.Name}|${child.CustomKey}`)
              }>
              <Card>
                <Card.Body className="text-center py-5">{child.Name}</Card.Body>
              </Card>
            </Button>
          </Col>
        );
      })}
    </Row>
  );
};
