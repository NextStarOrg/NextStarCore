import React from "react";
import { Tabs, Space, Tooltip } from "antd";
import { QuestionCircleOutlined } from "@ant-design/icons";

import HexComponent from "./components/hex";
import Base64Component from "./components/base64";
import UrlSafeComponent from "./components/urlSafe";
import NumericComponent from "./components/numeric";
import DistinguishableComponent from "./components/distinguishable";
import CustomeComponent from "./components/custome";

import { tabListInfo, tabType } from "./index.module";

export default () => {
    return (
        <div>
            <Tabs defaultActiveKey='0'>
                {tabListInfo.map((item, index) => {
                    return (
                        <Tabs.TabPane
                            tab={
                                <Space>
                                    <span>{item.title}</span>
                                    <Tooltip title={item.description}>
                                        <QuestionCircleOutlined />
                                    </Tooltip>
                                </Space>
                            }
                            key={index}>
                            {item.type === tabType.hex && <HexComponent />}
                            {item.type === tabType.base64 && (
                                <Base64Component />
                            )}
                            {item.type === tabType.urlSafe && (
                                <UrlSafeComponent />
                            )}
                            {item.type === tabType.numeric && (
                                <NumericComponent />
                            )}
                            {item.type === tabType.distinguishable && (
                                <DistinguishableComponent />
                            )}
                            {item.type === tabType.custome && (
                                <CustomeComponent />
                            )}
                        </Tabs.TabPane>
                    );
                })}
            </Tabs>
        </div>
    );
};
