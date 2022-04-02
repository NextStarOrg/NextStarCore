import {useSelector} from "react-redux";
import {selectUser} from "selector/selector";
import {UserState} from "redux-oidc";

function useAuth(): [isLogin: boolean, userState: UserState] {
    const userState = useSelector(selectUser);
    let isLogin = false;
    if (userState.user == undefined) {
        isLogin = false;
        return [isLogin, userState];
    }
    if (userState.user.expired) {
        isLogin = false;
        return [isLogin, userState];
    }
    isLogin = true;
    return [isLogin, userState];
}

export default useAuth;
