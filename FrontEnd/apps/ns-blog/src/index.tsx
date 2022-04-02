import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from 'routes/App';
import reportWebVitals from './reportWebVitals';
import {BrowserRouter} from "react-router-dom";
import {Provider} from "react-redux";
import {store} from "store/store";
import {OidcProvider} from "redux-oidc";
import {userManager} from "utils/auth-utils/user-manager";


import {isDev} from "utils/envDetect";
import {initDevHelper} from 'utils/devHelper';
import "locales/i18n";

ReactDOM.render(
    <React.StrictMode>
        <Provider store={store}>
            <OidcProvider store={store} userManager={userManager}>
                <BrowserRouter>
                    <App/>
                </BrowserRouter>
            </OidcProvider>
        </Provider>
    </React.StrictMode>,
    document.getElementById('root')
);

if (isDev) {
    initDevHelper(store);
}

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
