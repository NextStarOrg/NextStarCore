import { Selector } from "react-redux";
import { RootState } from "rtk/rtk";
import {UserState} from "redux-oidc";

export const selectRoot: Selector<RootState,RootState> = (state) =>
    state;

export const selectUser: Selector<RootState,UserState> = (state) =>
    state.common.oidc;
