import { AsyncStatus } from "types";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export type IAuthState = {
    loginStatus: AsyncStatus;
    logoutStatus: AsyncStatus;
};

const authInitialState: IAuthState = {
    logoutStatus: AsyncStatus.None,
    loginStatus: AsyncStatus.None,
};

const authSlice = createSlice({
    name: "auth",
    initialState: authInitialState,
    reducers: {
        changeLogoutStatus: (state, action: PayloadAction<AsyncStatus>) => {
            state.logoutStatus = action.payload;
        },
        changeLoginStatus: (state, action: PayloadAction<AsyncStatus>) => {
            state.loginStatus = action.payload;
        },
    },
});

export const { changeLogoutStatus, changeLoginStatus } = authSlice.actions;

export default authSlice.reducer;
