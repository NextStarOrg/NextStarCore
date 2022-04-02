import React, { useCallback, useEffect, useState } from "react";
import { Tabs } from "antd";
import { useSearchParams } from "react-router-dom";
import _ from "lodash";
import MD5 from "routes/safetyService/OneWayEncryption/components/MD5";
import SHA1 from "routes/safetyService/OneWayEncryption/components/SHA1";
import SHA224 from "routes/safetyService/OneWayEncryption/components/SHA224";
import SHA256 from "routes/safetyService/OneWayEncryption/components/SHA256";
import SHA384 from "routes/safetyService/OneWayEncryption/components/SHA384";
import SHA512 from "routes/safetyService/OneWayEncryption/components/SHA512";
import SHA3 from "routes/safetyService/OneWayEncryption/components/SHA3";
import useTabQuery from "hooks/useTabQuery";

const OneWayEncryptionType = [
    "MD5",
    "SHA1",
    "SHA224",
    "SHA256",
    "SHA384",
    "SHA512",
    "SHA3",
];

const OneWayEncryption = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const [activeKey, setActiveKey] = useState(OneWayEncryptionType[0]);
    const [setTabActiveParams] = useTabQuery(
        OneWayEncryptionType,
        setActiveKey
    );

    const handleTabSwitch = useCallback(
        (activeKey) => {
            setTabActiveParams(activeKey);
        },
        [setTabActiveParams]
    );
    return (
        <Tabs
            defaultActiveKey={"MD5"}
            activeKey={activeKey}
            onChange={handleTabSwitch}
        >
            <Tabs.TabPane tab='MD5' key={OneWayEncryptionType[0]}>
                <MD5 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='SHA1' key={OneWayEncryptionType[1]}>
                <SHA1 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='SHA224' key={OneWayEncryptionType[2]}>
                <SHA224 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='SHA256' key={OneWayEncryptionType[3]}>
                <SHA256 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='SHA384' key={OneWayEncryptionType[4]}>
                <SHA384 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='SHA512' key={OneWayEncryptionType[5]}>
                <SHA512 />
            </Tabs.TabPane>
            <Tabs.TabPane tab='SHA3' key={OneWayEncryptionType[6]}>
                <SHA3 />
            </Tabs.TabPane>
        </Tabs>
    );
};

export default OneWayEncryption;
