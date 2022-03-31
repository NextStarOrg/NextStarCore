import React from "react";
import { Tabs, Space, Tooltip } from "antd";
import { QuestionCircleOutlined } from "@ant-design/icons";

import UUIDv4Component from "./components/uuidv4Component";
import UUIDv5Component from "./components/uuidv5Component";
import UUIDv1Component from "./components/uuidv1Component";
import UUIDv3Component from "./components/uuidv3Component";

import { tabListInfo, UUIDType } from "./index.module";

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
                            {item.type === UUIDType.v4 && <UUIDv4Component />}
                            {item.type === UUIDType.v5 && <UUIDv5Component />}
                            {item.type === UUIDType.v1 && <UUIDv1Component />}
                            {item.type === UUIDType.v3 && <UUIDv3Component />}
                        </Tabs.TabPane>
                    );
                })}
            </Tabs>
        </div>
    );
};
