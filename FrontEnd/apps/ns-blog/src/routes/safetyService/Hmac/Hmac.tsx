import React, { useCallback, useEffect, useState } from "react";
import { Divider, Tabs, Typography } from "antd";
import { useSearchParams } from "react-router-dom";
import _ from "lodash";
import HmacMD5 from "routes/safetyService/Hmac/components/HmacMD5";
import HmacSHA1 from "routes/safetyService/Hmac/components/HmacSHA1";
import HmacSHA224 from "routes/safetyService/Hmac/components/HmacSHA224";
import HmacSHA256 from "routes/safetyService/Hmac/components/HmacSHA256";
import HmacSHA384 from "routes/safetyService/Hmac/components/HmacSHA384";
import HmacSHA512 from "routes/safetyService/Hmac/components/HmacSHA512";
import HmacSHA3 from "routes/safetyService/Hmac/components/HmacSHA3";
import useTabQuery from "hooks/useTabQuery";

const HmacType = [
    "HmacMD5",
    "HmacSHA1",
    "HmacSHA224",
    "HmacSHA256",
    "HmacSHA384",
    "HmacSHA512",
    "HmacSHA3",
];

const Hmac = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const [activeKey, setActiveKey] = useState(HmacType[0]);
    const [setTabActiveParams] = useTabQuery(HmacType, setActiveKey);
    const handleTabSwitch = useCallback(
        (activeKey) => {
            setTabActiveParams(activeKey);
        },
        [setTabActiveParams]
    );

    return (
        <>
            <Typography.Paragraph>
                密钥散列消息认证码（英语：Keyed-hash message authentication
                code），又称散列消息认证码（Hash-based message authentication
                code，缩写为HMAC），是一种通过特别计算方式之后产生的消息认证码（MAC），使用密码散列函数，同时结合一个加密密钥。它可以用来保证资料的完整性，同时可以用来作某个消息的身份验证。
            </Typography.Paragraph>
            <Typography.Paragraph>
                -- 来自
                <Typography.Link
                    href={"https://zh.wikipedia.org/wiki/HMAC"}
                    target={"_blank"}
                    rel={"nofollow noopener noreferrer"}
                >
                    维基百科
                </Typography.Link>
            </Typography.Paragraph>
            <Divider />
            <Tabs
                defaultActiveKey={HmacType[0]}
                activeKey={activeKey}
                onChange={handleTabSwitch}
            >
                <Tabs.TabPane tab='HmacMD5' key={HmacType[0]}>
                    <HmacMD5 />
                </Tabs.TabPane>
                <Tabs.TabPane tab='HmacSHA1' key={HmacType[1]}>
                    <HmacSHA1 />
                </Tabs.TabPane>
                <Tabs.TabPane tab='HmacSHA224' key={HmacType[2]}>
                    <HmacSHA224 />
                </Tabs.TabPane>
                <Tabs.TabPane tab='HmacSHA256' key={HmacType[3]}>
                    <HmacSHA256 />
                </Tabs.TabPane>
                <Tabs.TabPane tab='HmacSHA256' key={HmacType[4]}>
                    <HmacSHA384 />
                </Tabs.TabPane>
                <Tabs.TabPane tab='HmacSHA512' key={HmacType[5]}>
                    <HmacSHA512 />
                </Tabs.TabPane>
                <Tabs.TabPane tab='HmacSHA3' key={HmacType[6]}>
                    <HmacSHA3 />
                </Tabs.TabPane>
            </Tabs>
        </>
    );
};

export default Hmac;
