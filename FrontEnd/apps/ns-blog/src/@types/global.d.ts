interface Window {
    //!!! _devHelper 只会在开发环境的控制台使用 无需在这里声明其类型 源代码也不应该依赖于 _devHelper
    // eslint-disable-next-line
    _devHelper: any;
    debugFlag: boolean;
    _AUTHORITY_ENDPOINT: string;
    _BLOG_SERVICE_API_ENDPOINT: string;
    _ICON_FONT_URL: string;
    _VERSION: string;
}

type Nullable<T> = T | null;
