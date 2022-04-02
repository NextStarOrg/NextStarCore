import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export type ILoadingState = {
    isLoading: boolean;
    // 输入key，需要国际化
    message: string;
};
const message = "LoadingText";

const initialState: ILoadingState = {
    isLoading: false,
    message: message,
};

const loadingSlice = createSlice({
    name: "prompt",
    initialState,
    reducers: {
        setPrompt: (state, action: PayloadAction<ILoadingState>) => {
            state = action.payload;
            return state;
        },
        setLoadingStatus: (state, action: PayloadAction<boolean>) => {
            state.isLoading = action.payload;
            state.message = message;
            return state;
        },
    },
});

export const { setPrompt, setLoadingStatus } = loadingSlice.actions;

export default loadingSlice.reducer;
