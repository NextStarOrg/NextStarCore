import {useSelector} from "react-redux";
import {selectUser} from "selector/selector";
import {UserState} from "redux-oidc";

function useAuth(): [isLogin: boolean, userState: UserState] {
    const userState = useSelector(selectUser);
    if (userState.user == undefined) {
        return [false, userState];
    }
    if (userState.user.expired) {
        return [false, userState];
    }
    return [true, userState];
}

export default useAuth;
