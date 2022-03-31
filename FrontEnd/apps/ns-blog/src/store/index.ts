import { createStore, combineReducers, applyMiddleware } from "redux";
import logger from "redux-logger";
import promiseMiddleware from "redux-promise";
import thunk from "redux-thunk";
import Reducers from "./reducers";

// 2 日志
//3、存入仓库
let store = createStore(
    combineReducers(Reducers()),
    applyMiddleware(thunk, promiseMiddleware, logger)
);

export default store;
