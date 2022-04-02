const {
    override,
    addWebpackPlugin,
    addWebpackModuleRule,
    addDecoratorsLegacy,
    addWebpackResolve
} = require('customize-cra');
const path = require('path');
const AntdDayjsWebpackPlugin = require('antd-dayjs-webpack-plugin');
const NodePolyfillPlugin = require('node-polyfill-webpack-plugin');

module.exports = {
    webpack: override(
        addWebpackPlugin(new AntdDayjsWebpackPlugin(), new NodePolyfillPlugin({
            excludeAliases: ['console']
        })),
        addWebpackModuleRule({
            test: /\.css$/,
            use: [
                {
                    loader: 'style-loader',
                    options: {
                        insert: function insertAtTop(element) {
                            var parent = document.querySelector('head');
                            var lastInsertedElement =
                                window._lastElementInsertedByStyleLoader;

                            if (!lastInsertedElement) {
                                parent.insertBefore(element, parent.firstChild);
                            } else if (lastInsertedElement.nextSibling) {
                                parent.insertBefore(
                                    element,
                                    lastInsertedElement.nextSibling
                                );
                            } else {
                                parent.appendChild(element);
                            }

                            window._lastElementInsertedByStyleLoader = element;
                        },
                    },
                },
                'css-loader',
            ],
            include: [path.join(__dirname, 'node_modules')],
        }),
        addDecoratorsLegacy()
    ),
};
