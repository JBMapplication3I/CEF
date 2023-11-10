import Interweave, { InterweaveProps, Node } from "interweave";
import NodeMap from "interweave";
import React, { SVGProps, HTMLProps } from "react";

export const InterweaveWrapper = (interweaveProps: InterweaveProps) => {
  function transformElement(
    node: HTMLElement,
    children: Node[]
  ): React.ReactNode {
    if (node.tagName === "svg") {
      let attributes: HTMLProps<Element> = {};
      for (const attr of node.getAttributeNames()) {
        // @ts-ignore
        attributes[attr] = node.getAttribute(attr);
      }
      return <node.nodeName {...attributes}>{children}</node.nodeName>;
    }
  }

  return <Interweave {...interweaveProps} transform={transformElement} />;
};
