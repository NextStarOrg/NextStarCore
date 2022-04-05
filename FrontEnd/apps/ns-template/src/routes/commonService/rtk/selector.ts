import { RootState } from "rtk/rtk";
import { Selector } from "react-redux";
import { IPromptState } from "routes/commonService/rtk/prompt";
import { UserState } from "redux-oidc";
import {ILoadingState} from "routes/commonService/rtk/loading";

export const selectPromptState: Selector<RootState, IPromptState> = (state) =>
    state.common.prompt;

export const selectUserState: Selector<RootState, UserState> = (state) =>
    state.common.oidc;


export const selectLoading: Selector<RootState, ILoadingState> = (state) =>
    state.common.loading;
