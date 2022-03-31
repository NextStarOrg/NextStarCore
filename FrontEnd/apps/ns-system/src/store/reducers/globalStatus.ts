import { ReduxTypeEnum } from "enums/store";

export default function GlobalStateReducer(
    state: GlobalStatus = { loading: false, pathname: "/" },
    action: GLobalAction<ReduxTypeEnum, GlobalStatus>
) {
    switch (action.type) {
        case ReduxTypeEnum.globalStatus:
            return { ...state, ...action.payload };
        default:
            return state;
    }
}
