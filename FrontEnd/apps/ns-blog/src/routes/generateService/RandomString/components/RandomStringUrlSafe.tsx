import {
    Button,
    Input,
    InputNumber,
    List,
    Space,
    Tooltip,
    Typography,
} from "antd";
import React, { useState } from "react";
import _ from "lodash";
import CryptoRandomString, {
    CryptoRandomStringCharacters,
} from "utils/randomString";
import { useTranslation } from "react-i18next";

const RandomStringUrlSafe = () => {
    const { t } = useTranslation("GenerateService");
    const [len, setLen] = useState(32);
    const [count, setCount] = useState(10);
    const [strList, setStrList] = useState<string[]>([]);

    const generateCore = (): string[] => {
        const arr: string[] = [];
        for (let i = 0; i < count; i++) {
            arr.push(CryptoRandomString({ length: len, type: "url-safe" }));
        }
        return arr;
    };

    const handleGenerate = () => {
        setStrList(generateCore());
    };

    return (
        <>
            <Space direction={"vertical"}>
                <Typography.Paragraph>
                    {t("RandomString.FormatComposition")}：
                    <Typography.Text keyboard>
                        {CryptoRandomStringCharacters.urlSafeCharacters.join(
                            " "
                        )}
                    </Typography.Text>
                </Typography.Paragraph>
                <Input.Group compact>
                    <InputNumber
                        addonBefore={t("GeneratedNumber")}
                        defaultValue={count}
                        onChange={(v) =>
                            _.isNumber(v) ? setCount(v) : setCount(count)
                        }
                    />
                    <InputNumber
                        addonBefore={t("GenerateLength")}
                        defaultValue={len}
                        onChange={(v) =>
                            _.isNumber(v) ? setLen(v) : setLen(len)
                        }
                    />
                    <Button type='primary' onClick={handleGenerate}>
                        {t("GenerateButton")}
                    </Button>
                </Input.Group>
                <List
                    bordered
                    dataSource={strList}
                    style={{ overflow: "auth" }}
                    renderItem={(item) => (
                        <List.Item>
                            <Typography.Paragraph copyable>
                                {item}
                            </Typography.Paragraph>
                        </List.Item>
                    )}
                />
            </Space>
        </>
    );
};

export default RandomStringUrlSafe;
