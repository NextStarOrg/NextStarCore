export interface IRouterAboutConfigItem {
    Path: string;
    Name: string;
    Auth: boolean;
}

export interface ISafetyServiceConfigItem {
    BasePath: string;
    ServiceName: string;
    Dashboard: IRouterAboutConfigItem;
    OneWayEncryption: IRouterAboutConfigItem;
    Hmac: IRouterAboutConfigItem;
}

export interface IGenerateServiceConfigItem {
    BasePath: string;
    ServiceName: string;
    Dashboard: IRouterAboutConfigItem;
    RandomString: IRouterAboutConfigItem;
    UUID: IRouterAboutConfigItem;
}

export interface ICommonAboutConfigItem {
    BasePath: string;
    ServiceName: string;
    Forbidden: IRouterAboutConfigItem;
    NotFound: IRouterAboutConfigItem;
}

export type IRouterAboutConfig = {
    RootPath: string;
    SafetyService: ISafetyServiceConfigItem;
    GenerateService: IGenerateServiceConfigItem;
    CommonService: ICommonAboutConfigItem;
    Login: IRouterAboutConfigItem;
    Callback: IRouterAboutConfigItem;
    SignOutCallback: IRouterAboutConfigItem;
};

export const RouterAboutConfig: IRouterAboutConfig = {
    RootPath: "/",
    SafetyService: {
        BasePath: "safety",
        ServiceName: "安全工具",
        Dashboard: {
            Path: "dashboard",
            Name: "仪表板",
            Auth: false,
        },
        OneWayEncryption: {
            Path: "one-way-encryption",
            Name: "单向加密",
            Auth: false,
        },
        Hmac: {
            Path: "hmac",
            Name: "HMAC",
            Auth: false,
        },
    },
    GenerateService: {
        BasePath: "generate",
        ServiceName: "生成工具",
        Dashboard: {
            Path: "dashboard",
            Name: "仪表板",
            Auth: false,
        },
        RandomString: {
            Path: "random-string",
            Name: "随机字符串",
            Auth: false,
        },
        UUID: {
            Path: "uuid",
            Name: "UUID",
            Auth: false,
        },
    },
    CommonService: {
        BasePath: "common",
        ServiceName: "共通",
        Forbidden: {
            Path: "forbidden",
            Name: "未授权页面",
            Auth: false,
        },
        NotFound: {
            Path: "notfound",
            Name: "Not Found",
            Auth: false,
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
