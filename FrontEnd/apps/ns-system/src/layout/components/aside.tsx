import React, { useEffect, useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Menu } from 'antd';
import { createFromIconfontCN } from '@ant-design/icons';
import { useHistory } from 'react-router-dom';
import { Location, Action } from 'history';

import menuList, { MenuItem } from 'configs/menuList';

import styles from './sub.module.scss';

const { SubMenu } = Menu;
const IconFont = createFromIconfontCN({
  scriptUrl: window._ICON_FONT_URL,
});
const _REACT_BASE_URL = window._REACT_BASE_URL;

function parentMenu(menu: MenuItem) {
  return (
    <SubMenu
      key={menu.key}
      title={
        <span>
          <IconFont type={menu.icon || ''} />
          <span>{menu.title}</span>
        </span>
      }
    >
      {menu.childrens.map((item) => {
        return childMenu(item);
      })}
    </SubMenu>
  );
}
function childMenu(menu: MenuItem) {
  if (menu.link !== undefined) {
    return (
      <Menu.Item key={menu.key} icon={<IconFont type={menu.icon || ''} />}>
        <Link to={_REACT_BASE_URL + menu.link}>{menu.title}</Link>
      </Menu.Item>
    );
  } else {
    return (
      <Menu.Item key={menu.key} icon={<IconFont type={menu.icon || ''} />}>
        {menu.title}
      </Menu.Item>
    );
  }
}

function renderMenuList(menuList: MenuItem[]) {
  let arr: JSX.Element[] = [];
  for (let i = 0; i < menuList.length; i++) {
    if (menuList[i].childrens.length > 0) {
      arr.push(parentMenu(menuList[i]));
    } else {
      arr.push(childMenu(menuList[i]));
    }
  }
  return arr;
}

const Aside = () => {
  const [pathName, setPathName] = useState<string>('');
  const [selected, SetSelected] = useState<string[]>(['']);
  const history = useHistory();
  const location = useLocation();
  history.listen((locations: Location, action: Action) => {
    if (locations.pathname !== pathName) {
      setPathName(locations.pathname);
    }
  });

  useEffect(
    function () {
      if (pathName) {
        SetSelected([pathName]);
      } else {
        SetSelected([location.pathname]);
      }
    },
    [pathName, location.pathname],
  );
  return (
    <aside className={styles.aside}>
      <div className={styles.asideWapper}>
        <Menu
          style={{ width: 256 }}
          className={styles.menuMain}
          mode="inline"
          selectedKeys={selected}
        >
          {renderMenuList(menuList)}
        </Menu>
      </div>
    </aside>
  );
};

export default Aside;
