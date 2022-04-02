import {
    Button,
    Checkbox,
    Input,
    InputNumber,
    List,
    Space,
    Tooltip,
    Typography,
} from "antd";
import React, { useEffect, useState } from "react";
import _ from "lodash";
import CryptoRandomString, {
    CryptoRandomStringCharacters,
} from "utils/randomString";
import nsStorage from "utils/storage";
import { GenerateRandomStringCustomCharacter } from "assets/consts/StoreCacheName";
import { useSearchParams } from "react-router-dom";
import { useTranslation } from "react-i18next";

const RandomStringCustom = () => {
    const { t } = useTranslation("GenerateService");
    const [searchParams, setSearchParams] = useSearchParams();
    const queryCharacters = searchParams.get("characters")?.toString();
    const storeCharacters = nsStorage.get(GenerateRandomStringCustomCharacter);
    const [len, setLen] = useState(32);
    const [count, setCount] = useState(10);
    const initVale =
        queryCharacters != undefined
            ? queryCharacters
            : storeCharacters != undefined && storeCharacters != ""
            ? storeCharacters
            : CryptoRandomStringCharacters.customDefaultCharacters.join("");
    const [customCharacters, setCustomCharacters] = useState<string>(initVale);
    const [isRemember, setIsRemember] = useState(true);
    const [strList, setStrList] = useState<string[]>([]);

    const generateCore = (): string[] => {
        const arr: string[] = [];
        for (let i = 0; i < count; i++) {
            arr.push(
                CryptoRandomString({
                    length: len,
                    type: "custom",
                    characters: customCharacters,
                })
            );
        }
        return arr;
    };
    const handleGenerate = () => {
        setStrList(generateCore());
    };

    const handleChangeSearchParams = (customCharacters: string) => {
        const queryCharacters = searchParams.get("characters")?.toString();
        if (queryCharacters != undefined) {
            searchParams.set("characters", customCharacters);
        } else {
            searchParams.append("characters", customCharacters);
        }
        setSearchParams(searchParams);
    };

    useEffect(() => {
        handleChangeSearchParams(customCharacters);
        if (isRemember) {
            nsStorage.set(
                GenerateRandomStringCustomCharacter,
                customCharacters
            );
        } else {
            nsStorage.remove(GenerateRandomStringCustomCharacter);
        }
    }, [isRemember, customCharacters]);

    useEffect(() => {
        handleChangeSearchParams(customCharacters);
    }, []);

    return (
        <>
            <Space direction={"vertical"}>
                <Typography.Paragraph>
                    {t("RandomString.FormatComposition")}：
                    <Typography.Text keyboard>
                        {customCharacters.split("").join(" ")}
                    </Typography.Text>
                </Typography.Paragraph>
                <Space style={{ width: "100%" }}>
                    <Input
                        addonBefore={t("RandomString.CustomCharacters")}
                        style={{ width: "560px" }}
                        defaultValue={customCharacters}
                        value={customCharacters}
                        allowClear
                        onChange={(e) => setCustomCharacters(e.target.value)}
                    />
                    <Checkbox
                        checked={isRemember}
                        onChange={(e) => setIsRemember(e.target.checked)}
                    >
                        是否记住
                    </Checkbox>
                    <Button
                        onClick={() =>
                            setCustomCharacters(
                                CryptoRandomStringCharacters.customDefaultCharacters.join(
                                    ""
                                )
                            )
                        }
                    >
                        恢复默认
                    </Button>
                </Space>
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

export default RandomStringCustom;
