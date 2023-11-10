import { useState } from "react";

interface ITabsProps {
  onSwitch?: Function;
  tabClasses?: string;
  tabContainerClasses?: string;
  children: JSX.Element[];
}

export const Tabs = (props: ITabsProps): JSX.Element => {
  const { onSwitch, tabClasses, tabContainerClasses, children } = props;
  const [tabToShow, setTabToShow] = useState<number>(0);

  const filteredChildren = children.filter((c) => c && c.props.label);

  return (
    <div className={`tabs ${tabContainerClasses ?? ""}`}>
      <nav>
        <ul className="nav nav-tabs">
          {filteredChildren.map((c, index: number) => {
            return (
              <li key={c.props.label}>
                <button
                  type="button"
                  className={`nav-link ${tabClasses ?? ""} ${
                    index === tabToShow ? "active" : ""
                  }`}
                  onClick={() => {
                    setTabToShow(index);
                    if (onSwitch) {
                      onSwitch();
                    }
                  }}>
                  {c.props.label}
                </button>
              </li>
            );
          })}
        </ul>
      </nav>
      <div className="my-2">{filteredChildren[tabToShow]}</div>
    </div>
  );
};
