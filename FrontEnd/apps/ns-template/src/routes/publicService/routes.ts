import { IRoute } from "types";
import SignInCallbackRouter from "routes/publicService/SignInCallback/router";
import SignOutCallbackRouter from "routes/publicService/SignOutCallback/router";

const PublicRoutes: IRoute[] = [
    SignInCallbackRouter,
    SignOutCallbackRouter,
];

export default PublicRoutes;
