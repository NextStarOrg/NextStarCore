import authReducer, { IAuthState } from "./auth";
import { combineReducers } from "@reduxjs/toolkit";

export interface IPublicState {
    auth: IAuthState;
}

export default combineReducers<IPublicState>({
    auth: authReducer,
});
