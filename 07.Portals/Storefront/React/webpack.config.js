module.exports = {
  output: {
    publicPath: "auto"
  },
  devServer: {
    https: true
  },
  module: {
    rules: [
      {
        test: /\.scss$/,
        use: ["style-loader", "css-loader", "sass-loader"]
      }
    ]
  }
};
