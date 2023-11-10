import axios from "axios";

const instance = axios.create({
  baseURL: "/DesktopModules/ClarityEcommerce/API-Storefront",
  withCredentials: true,
  headers: { "Content-Type": "application/json" },
  paramsSerializer: function (params) {
    let keys = Object.keys(params);
    let stringified = "";
    for (let i = 0; i < keys.length; i++) {
      const key = keys[i],
        param = params[key];
      if (param === undefined) {
        continue;
      }
      if (i > 0) {
        stringified += "&";
      }
      stringified += key;
      if (param instanceof Array) {
        for (let j = 0; j < param.length; j++) {
          const param2 = param[j];
          stringified += "=" + param2 + (j !== param.length - 1 ? "&" + key : "");
        }
      } else if (param instanceof Object) {
        stringified += "=" + encodeURIComponent(JSON.stringify(param));
      } else {
        stringified += "=" + param;
      }
    }
    return stringified;
  }
});

export default instance;
