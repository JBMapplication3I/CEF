export const currencyFormatter = new Intl.NumberFormat("en-US", {
  style: "currency",
  currency: "USD"
});

export const camelCaseToHumanReadable = (name: string): string => {
  function capitalize(word: string) {
    return word.charAt(0).toUpperCase() + word.substring(1);
  }
  const words = name.match(/[A-Za-z][a-z]*/g) || [];
  return words.map(capitalize).join(" ");
};

export const getObjectFromDotNotation = (str: string, value: any): any => {
  return str
    .split(".")
    .reverse()
    .reduce((prevValue, currentValue) => {
      return {
        [currentValue]: prevValue
      };
    }, value);
};

export const convertSalesItemToBasicProductData = (salesItem: any): any => {
  const { ProductID, ProductName, ProductSeoUrl } = salesItem;
  return {
    ID: ProductID,
    Name: ProductName,
    SeoUrl: ProductSeoUrl
  };
};
