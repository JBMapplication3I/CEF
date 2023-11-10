import { Container } from "react-bootstrap";

export const Home = (): JSX.Element => {
  return (
    <div>
      <section className="section-featured">
        <Container className="my-5">
          <div className="head-section text-center">
            <h1 className="title">Featured Products</h1>
          </div>
        </Container>
      </section>
    </div>
  );
};
