import { RootState } from "rtk/rtk";
import { IAuthState } from "./auth";
import { Selector } from "react-redux";

export const selectAuthState: Selector<RootState, IAuthState> = (state) =>
    state.public.auth;
