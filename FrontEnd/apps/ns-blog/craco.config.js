const CircularDependencyPlugin = require("circular-dependency-plugin");
const AntdDayjsWebpackPlugin = require("antd-dayjs-webpack-plugin");
const lessLocalIdentName = "sl_[name]_[local]_[emoji]_[sha512:hash:base62:6]";

module.exports = {
    style: {
        modules: {
            localIdentName: lessLocalIdentName,
        },
        // 配置兼容性问题，自动添加前缀
        postcssOptions: {
            plugins: [require("autoprefixer")],
        },
    },
    webpack: {
        // alias: {
        //     "@global": path.resolve(__dirname, "src/assets"),
        // },
        plugins: [
            // dayjs 替换 Moment.js
            new AntdDayjsWebpackPlugin(),
        ],
    },
    babel: {
        plugins: [
            [
                "import",
                {
                    libraryName: "@ant-design",
                    libraryDirectory: "icons",
                    camel2DashComponentName: false, // default: true
                },
            ],
        ],
    },
};
