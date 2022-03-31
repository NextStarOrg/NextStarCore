import React, { lazy, Suspense } from 'react';
import { Router } from 'react-router';
import { Switch, Route } from 'react-router-dom';
import { createBrowserHistory } from 'history';

import SupenseLoading from 'components/SupenseLoading';

const customHistory = createBrowserHistory();
const _REACT_BASE_URL = window._REACT_BASE_URL;
const LayoutIndex = lazy(() => import('./layout'));

export default () => {
  return (
    <React.Fragment>
      <Router history={customHistory}>
        <React.Fragment>
          <Suspense fallback={<SupenseLoading />}>
            <Switch>
              {/*匹配其他路径，将需要全局展示并且不需要 layout的全部放置在此上面*/}
              <Route path={_REACT_BASE_URL} component={LayoutIndex} />
            </Switch>
          </Suspense>
        </React.Fragment>
      </Router>
    </React.Fragment>
  );
};
