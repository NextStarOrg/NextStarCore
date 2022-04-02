import { IRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import SignOutCallback from "routes/publicService/SignOutCallback/SignOutCallback";

const SignOutCallbackRouter: IRoute = {
    name: RouterAboutConfig.SignOutCallback.Name,
    path: RouterAboutConfig.SignOutCallback.Path,
    auth: RouterAboutConfig.SignOutCallback.Auth,
    component: SignOutCallback,
};

export default SignOutCallbackRouter;
