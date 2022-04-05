import {IServiceRoute} from "types";
import CommonServicesRoutes from "routes/commonService/routes";
import BlogServicesRoutes from "routes/blogService/routes";

const routeConfigs: IServiceRoute[] = [
    // 第一个服务的默认页面会成为站点的默认页面
    BlogServicesRoutes,
    // 上面添加其他服务
    CommonServicesRoutes,
];

export default routeConfigs;
