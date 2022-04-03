import { IRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import Forbidden from "routes/commonService/Forbidden/Forbidden";

const ForbiddenRouter: IRoute = {
    path: RouterAboutConfig.CommonService.Forbidden.Path,
    name: RouterAboutConfig.CommonService.Forbidden.Name,
    auth: RouterAboutConfig.CommonService.Forbidden.Auth,
    component: Forbidden,
};

export default ForbiddenRouter;
