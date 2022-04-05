import { isDev } from "utils/envDetect";
import { Store } from "@reduxjs/toolkit";

/**
 * Dev tools in Console.
 * * 注意： 你的代码不应该依赖于 window._devHelper
 * @param store
 */
export function initDevHelper(store: Store): void {
    if (!isDev) {
        return;
    }
    window._devHelper = {
        //used as a manual flag for debug.
        debugFlag: false,
        get state() {
            return store.getState();
        },
    };
}
