import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

interface TranslateProps {
  translateKey: string;
  attrs?: {
    [key: string]: string;
  };
}

export const Translate = (props: TranslateProps): JSX.Element => {
  const { translateKey, attrs } = props;

  const [assignableAttributes, setAssignableAttributes] = useState<{ [key: string]: string }>(null);
  const { t } = useTranslation();

  useEffect(() => {
    if (attrs) {
      buildAssignableAttributes();
    }
  }, [attrs]);

  function buildAssignableAttributes(): void {
    const tempAssignableAttributes: { [key: string]: string } = {};
    for (const attrName in attrs) {
      tempAssignableAttributes[attrName] = t(attrs[attrName]);
    }
    setAssignableAttributes(tempAssignableAttributes);
  }

  return <span {...assignableAttributes}>{t(translateKey)}</span>;
};
