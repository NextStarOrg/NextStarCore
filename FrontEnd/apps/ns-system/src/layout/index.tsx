import React from 'react';
import { Redirect, Route, Switch } from 'react-router-dom';
import styles from './index.module.scss';
import routeConfigs from './router';
import Aside from './components/aside';
import Header from './components/header';
import ContentFooter from './components/contentFooter';

const _REACT_BASE_URL = window._REACT_BASE_URL;

const Layout = () => {
  return (
    <div className={styles.layout}>
      <Header />
      <section className={styles.section}>
        <Aside />
        <section className={styles.mainSection}>
          <section className={styles.contentMain}>
            <Switch>
              {routeConfigs.map((item, index) => {
                return (
                  <Route
                    key={index}
                    path={_REACT_BASE_URL + item.path}
                    exact={item.exact}
                    component={item.component}
                  />
                );
              })}
              <Redirect to={_REACT_BASE_URL + 'dashboard'} />
            </Switch>
          </section>
          <ContentFooter />
        </section>
      </section>
    </div>
  );
};

export default Layout;
