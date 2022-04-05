export interface IRouterAboutConfigItem {
    Path: string;
    Name: string;
    Auth: boolean;
}

export interface ISafetyServiceConfigItem {
    BasePath: string;
    ServiceName: string;
    Dashboard: IRouterAboutConfigItem;
    Category: IRouterAboutConfigItem;
    Article: IRouterAboutConfigItem;
}

export interface ICommonAboutConfigItem {
    BasePath: string;
    ServiceName: string;
    Forbidden: IRouterAboutConfigItem;
    NotFound: IRouterAboutConfigItem;
}

export type IRouterAboutConfig = {
    RootPath: string;
    BlogService: ISafetyServiceConfigItem;
    CommonService: ICommonAboutConfigItem;
    Login: IRouterAboutConfigItem;
    Callback: IRouterAboutConfigItem;
    SignOutCallback: IRouterAboutConfigItem;
};

export const RouterAboutConfig: IRouterAboutConfig = {
    RootPath: "/",
    BlogService: {
        BasePath: "blog",
        ServiceName: "安全工具",
        Dashboard: {
            Path: "dashboard",
            Name: "仪表板",
            Auth: true,
        },
        Category: {
            Path: "category",
            Name: "分类",
            Auth: true,
        },
        Article: {
            Path: "article",
            Name: "文章",
            Auth: true,
        },
    },
    CommonService: {
        BasePath: "common",
        ServiceName: "共通",
        Forbidden: {
            Path: "forbidden",
            Name: "未授权页面",
            Auth: true,
        },
        NotFound: {
            Path: "notfound",
            Name: "Not Found",
            Auth: true,
        },
    },
    Login: {
        Path: "login",
        Name: "登录",
        Auth: false,
    },
    Callback: {
        Path: "callback",
        Name: "登录回调",
        Auth: false,
    },
    SignOutCallback: {
        Path: "signoutcallback",
        Name: "退出回调",
        Auth: false,
    },
};
