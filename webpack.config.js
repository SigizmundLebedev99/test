const path = require("path");
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  entry: path.join(__dirname, "./src/Client/script.js"),
  mode: "development",
  output: {
    path: path.join(__dirname, '/wwwroot'),
    filename:  "./bundle.js"
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /(node_modules|bower_components)/,
        use: {
          loader: "babel-loader"
        }
      },
      {
        test: /\.css$/,
        use: [
          "style-loader",
          "css-loader" 
        ]
      }
    ]
  },
  plugins:[
    new HtmlWebpackPlugin({
        template: path.join(__dirname, "src", "Client", "index.html"),
        inject:'body'
    })
  ],
  devServer:{
    contentBase: path.join(__dirname, 'wwwroot'),
    compress: true,
    port: 9000,
    proxy: {
        context : () => true,
        target: 'http://localhost:5000'
    }
  }
};