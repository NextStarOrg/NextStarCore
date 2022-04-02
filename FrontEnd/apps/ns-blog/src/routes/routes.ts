import {IServiceRoute} from "types";
import CommonServicesRoutes from "routes/commonService/routes";
import SafetyServicesRoutes from "routes/safetyService/routes";
import GenerateServicesRoutes from "routes/generateService/routes";

const routeConfigs: IServiceRoute[] = [
    // 第一个服务的默认页面会成为站点的默认页面
    SafetyServicesRoutes,
    GenerateServicesRoutes,
    // 上面添加其他服务
    CommonServicesRoutes,
];

export default routeConfigs;
