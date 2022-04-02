import { IRoute } from "types";
import SignInCallbackRouter from "routes/publicService/SignInCallback/router";
import SignOutCallbackRouter from "routes/publicService/SignOutCallback/router";
import LoginRouter from "routes/publicService/Login/router";

const PublicRoutes: IRoute[] = [
    SignInCallbackRouter,
    SignOutCallbackRouter,
    LoginRouter,
];

export default PublicRoutes;
