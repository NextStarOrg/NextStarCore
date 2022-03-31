interface Window {
    _REACT_BASE_URL: string;
    _ICON_FONT_URL: string;
    enums: ReduxGlobalEnum;
    callback: Function;
    TencentCaptcha: class;
}
interface GlobalStatus {
    loading?: boolean;
    pathname?: string;
}

interface GLobalAction<T, M> {
    type: T;
    payload: M;
}

interface GlobalRootState {
    userInfo: UserInfo;
    globalStatus: GlobalStatus;
}
