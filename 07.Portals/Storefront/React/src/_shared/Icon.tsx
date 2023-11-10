import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconDefinition, IconProp } from "@fortawesome/fontawesome-svg-core";

interface IIconProps {
  icon: IconDefinition;
  type: "regular" | "solid" | "brands";
  classes?: string;
}

export const Icon = (props: IIconProps): JSX.Element => {
  const { icon, type, classes } = props;

  function getIcon(): IconDefinition {
    const newIcon: IconDefinition =
      require(`@fortawesome/free-${type}-svg-icons`)[icon as keyof IconProp];
    if (!newIcon) {
      console.log(`Unknown icon: ${icon} type: ${type}`, "Look in Icon.tsx");
      return null;
    }
    return newIcon;
  }

  return <FontAwesomeIcon icon={getIcon()} className={classes ?? ""} />;
};
