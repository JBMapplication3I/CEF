const path = require("path");

module.exports = {
  paths: function (paths, env) {
    paths.appIndexJs = path.resolve(__dirname, "buildsrc/index.js");
    paths.appSrc = path.resolve(__dirname, "buildsrc");
    return paths;
  },
  webpack: function (config, env) {
    // paths here must match footer.ascx imports
    config.output = {
      filename: "[id]-cef-store-[name].js",
      path: path.resolve(__dirname, "build"),
      publicPath: "/DesktopModules/ClarityEcommerce/Shop/"
    };
    config.plugins.forEach((plugin, i) => {
      if (
        plugin.options &&
        plugin.options.filename &&
        plugin.options.filename.includes("static/css")
      ) {
        // output all css as 'clarity.css'
        config.plugins[i].options.filename = "static/css/clarity.css";
      }
    });
    config.optimization.splitChunks.cacheGroups = {
      // output all needed node_modules as '1-cef-store-vendors.js'
      vendors: {
        test: /node_modules.*(?<!\.css)(?<!\.scss)(?<!\.less)$/,
        name: "vendors",
        chunks: "all"
      }
    };
    config.optimization.runtimeChunk = false;
    config.optimization.minimize = env === "production";
    console.log(`NODE_ENV=${env}`);
    console.log("Additional config was applied through config-overrides.js");

    return config;
  }
};
