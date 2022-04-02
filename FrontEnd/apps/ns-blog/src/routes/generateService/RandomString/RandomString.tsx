import React, { useCallback, useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import _ from "lodash";
import { Tabs, Typography } from "antd";
import RandomStringUrlSafe from "routes/generateService/RandomString/components/RandomStringUrlSafe";
import RandomStringNumeric from "routes/generateService/RandomString/components/RandomStringNumeric";
import RandomStringDistinguishable from "routes/generateService/RandomString/components/RandomStringDistinguishable";
import RandomStringCustom from "routes/generateService/RandomString/components/RandomStringCustom";
import { useTranslation } from "react-i18next";
import useTabQuery from "hooks/useTabQuery";

const RandomStringType = ["UrlSafe", "Numeric", "Distinguishable", "Custom"];

const RandomString = () => {
    const { t } = useTranslation("GenerateService");
    const [activeKey, setActiveKey] = useState(RandomStringType[0]);
    const [setTabActiveParams] = useTabQuery(RandomStringType, setActiveKey);

    const handleTabSwitch = useCallback(
        (activeKey) => {
            setTabActiveParams(activeKey);
        },
        [setTabActiveParams]
    );
    return (
        <React.Fragment>
            <Typography.Paragraph>
                {t("RandomString.Description")}
            </Typography.Paragraph>
            <Tabs
                defaultActiveKey={"UrlSafe"}
                activeKey={activeKey}
                onChange={handleTabSwitch}
            >
                <Tabs.TabPane
                    tab={t("RandomString.UrlSafe")}
                    key={RandomStringType[0]}
                >
                    <RandomStringUrlSafe />
                </Tabs.TabPane>
                <Tabs.TabPane
                    tab={t("RandomString.PureDigital")}
                    key={RandomStringType[1]}
                >
                    <RandomStringNumeric />
                </Tabs.TabPane>
                <Tabs.TabPane
                    tab={t("RandomString.EasilyDistinguishable")}
                    key={RandomStringType[2]}
                >
                    <RandomStringDistinguishable />
                </Tabs.TabPane>
                <Tabs.TabPane
                    tab={t("RandomString.Custom")}
                    key={RandomStringType[3]}
                >
                    {activeKey == RandomStringType[3] && <RandomStringCustom />}
                </Tabs.TabPane>
            </Tabs>
        </React.Fragment>
    );
};

export default RandomString;
