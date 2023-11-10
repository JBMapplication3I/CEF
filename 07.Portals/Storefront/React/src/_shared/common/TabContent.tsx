import { Tab } from "react-bootstrap";

interface ITabContentProps {
  label: string; // grabbed by parent
  title?: string; // remove this
  children: JSX.Element[] | JSX.Element;
}

export const TabContent = (props: ITabContentProps) => {
  return <div className="tab-pane show">{props.children}</div>;
};
