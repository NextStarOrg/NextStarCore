import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export type IPromptState = {
    isEditing: boolean;
    message: string;
};
const message = "正在编辑中，是否退出？";

const initialState: IPromptState = {
    isEditing: false,
    message: message,
};

const promptSlice = createSlice({
    name: "prompt",
    initialState,
    reducers: {
        setPrompt: (state, action: PayloadAction<IPromptState>) => {
            state = action.payload;
            return state;
        },
        setPromptStatus: (state, action: PayloadAction<boolean>) => {
            state.isEditing = action.payload;
            state.message = message;
            return state;
        },
    },
});

export const { setPrompt, setPromptStatus } = promptSlice.actions;

export default promptSlice.reducer;
