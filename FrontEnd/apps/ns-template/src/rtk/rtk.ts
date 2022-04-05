import {
    combineReducers
} from "@reduxjs/toolkit";
import PublicReducer, {IPublicState} from "routes/publicService/rtk";
import CommonReducer, {ICommonState} from "routes/commonService/rtk";

export type RootState = {
    public: IPublicState;
    common:ICommonState
};

export default combineReducers<RootState>({
    public: PublicReducer,
    common:CommonReducer
});
