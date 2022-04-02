import React, { useCallback, useState } from "react";
import useTabQuery from "hooks/useTabQuery";
import { Tabs } from "antd";
import UUIDv1 from "routes/generateService/UUID/components/UUIDv1";
import UUIDv3 from "routes/generateService/UUID/components/UUIDv3";
import UUIDv4 from "routes/generateService/UUID/components/UUIDv4";
import UUIDv5 from "routes/generateService/UUID/components/UUIDv5";

const UUIDType = ["v1", "v3", "v4", "v5"];

const UUID = () => {
    const [activeKey, setActiveKey] = useState(UUIDType[0]);
    const [setTabActiveParams] = useTabQuery(UUIDType, setActiveKey);

    const handleTabSwitch = useCallback(
        (activeKey) => {
            setTabActiveParams(activeKey);
        },
        [setTabActiveParams]
    );
    return (
        <Tabs
            defaultActiveKey={UUIDType[0]}
            activeKey={activeKey}
            onChange={handleTabSwitch}
        >
            <Tabs.TabPane tab='v1' key={UUIDType[0]}>
                <UUIDv1 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='v3' key={UUIDType[1]}>
                <UUIDv3 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='v4' key={UUIDType[2]}>
                <UUIDv4 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='v5' key={UUIDType[3]}>
                <UUIDv5 />
            </Tabs.TabPane>
        </Tabs>
    );
};

export default UUID;
