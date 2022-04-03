import {reducer as oidcReducer, UserState} from "redux-oidc";
import {combineReducers} from "@reduxjs/toolkit";
import promptReducer, {IPromptState} from "routes/commonService/rtk/prompt";
import loadingReducer, {ILoadingState} from "routes/commonService/rtk/loading";

export interface ICommonState {
    oidc: UserState;
    prompt: IPromptState;
    loading: ILoadingState
}

export default combineReducers<ICommonState>({
    oidc: oidcReducer,
    prompt: promptReducer,
    loading: loadingReducer
});
