import { IRoute } from "types";
import { RouterAboutConfig } from "assets/consts/RouterAboutName";
import SignInCallback from "routes/publicService/SignInCallback/SignInCallback";

const SignInCallbackRouter: IRoute = {
    name: RouterAboutConfig.Callback.Name,
    path: RouterAboutConfig.Callback.Path,
    auth: RouterAboutConfig.Callback.Auth,
    component: SignInCallback,
};

export default SignInCallbackRouter;
