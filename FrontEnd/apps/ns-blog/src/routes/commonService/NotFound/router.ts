import { IRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import NotFound from "routes/commonService/NotFound/NotFound";

const NotFoundRouter: IRoute = {
    path: RouterAboutConfig.CommonService.NotFound.Path,
    name: RouterAboutConfig.CommonService.NotFound.Name,
    auth: RouterAboutConfig.CommonService.NotFound.Auth,
    component: NotFound,
};

export default NotFoundRouter;
