/**
 * 通用的异步状态枚举
 * None: 异步状态未开始
 * Pending: 已开始，状态未决定
 * Fulfilled: 状态确定并成功
 * Rejected: 状态确定并失败
 */
export enum AsyncStatus {
    None,
    Pending,
    Fulfilled,
    Rejected,
}

export interface IRouteBasic{
    //对于后端 Menu 没有配置的页面： 比如通知，登录履历 需要在 Route 中配置 name, 供面包屑导航使用
    name: string;
    path: string;
    auth:boolean;
    routes?: IRoute[];
}

export interface IRoute extends IRouteBasic{
    component: () => NonNullable<JSX.Element>;
}

export interface IServiceRoute {
    key:string;
    path:string;
    indexComponent:() => NonNullable<JSX.Element>;
    layoutComponent?: () => NonNullable<JSX.Element>;
    routes?:IRoute[];
}
