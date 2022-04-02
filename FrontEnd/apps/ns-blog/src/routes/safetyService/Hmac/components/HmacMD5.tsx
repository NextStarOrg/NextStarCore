import {
    Button,
    Checkbox,
    Divider,
    Input,
    Radio,
    Space,
    Typography,
} from "antd";
import React, { useCallback, useEffect, useState } from "react";
import CryptoJS from "crypto-js";
import { FormatType } from "utils/codeFormatUtils";
import { useTranslation } from "react-i18next";

const HmacMD5 = () => {
    const { t } = useTranslation("SafetyService");
    const [originText, setOriginText] = useState("");
    const [originKey, setOriginKey] = useState("");
    const [cipherText, setCipherText] = useState("");
    const [autoUpdate, setAutoUpdate] = useState<boolean>(true);
    const [codeType, setCodeType] = useState<FormatType>(FormatType.Text);
    const [outCodeType, setOutCodeType] = useState<FormatType>(FormatType.Hex);

    const handleOutput = useCallback(() => {
        textEncodeMd5(originText, originKey);
    }, [originText, codeType, outCodeType]);

    useEffect(() => {
        if (autoUpdate) {
            textEncodeMd5(originText, originKey);
        }
    }, [originText, originKey, codeType, autoUpdate, outCodeType]);

    const textEncodeMd5 = (text: string, key: string) => {
        try {
            if (text == "") {
                setCipherText("");
            } else {
                const result = CryptoJS.HmacMD5(
                    codeType == FormatType.Hex
                        ? CryptoJS.enc.Hex.parse(text)
                        : text,
                    codeType == FormatType.Hex
                        ? CryptoJS.enc.Hex.parse(key)
                        : key
                );
                setCipherText(
                    result.toString(
                        outCodeType == FormatType.Hex
                            ? CryptoJS.enc.Hex
                            : CryptoJS.enc.Base64
                    )
                );
            }
        } catch (err) {
            setCipherText("格式错误");
        }
    };
    return (
        <>
            <Space direction={"vertical"} style={{ width: "100%" }}>
                <Space>
                    <Typography.Text>{t("InputType")}：</Typography.Text>
                    <Radio.Group
                        value={codeType}
                        onChange={(e) => setCodeType(e.target.value)}
                    >
                        <Radio value={FormatType.Text}>Text</Radio>
                    </Radio.Group>
                </Space>
                <Space direction={"vertical"} style={{ width: "100%" }}>
                    <Input.TextArea
                        rows={4}
                        placeholder={t("OriginalArticle")}
                        value={originText}
                        onChange={(e) => setOriginText(e.target.value)}
                        allowClear={true}
                    />
                    <Input.TextArea
                        rows={4}
                        placeholder={"Key"}
                        value={originKey}
                        onChange={(e) => setOriginKey(e.target.value)}
                        allowClear={true}
                    />
                </Space>
            </Space>
            <Divider>
                <Space>
                    <Button onClick={handleOutput}>{t("OutputButton")}</Button>
                    <Checkbox
                        checked={autoUpdate}
                        onChange={(e) => setAutoUpdate(e.target.checked)}
                    >
                        {t("AutoUpdate")}
                    </Checkbox>
                </Space>
            </Divider>
            <Space direction={"vertical"} style={{ width: "100%" }}>
                <Space>
                    <Typography.Text>{t("OutputType")}：</Typography.Text>
                    <Radio.Group
                        value={outCodeType}
                        onChange={(e) => setOutCodeType(e.target.value)}
                    >
                        <Radio value={FormatType.Hex}>Hex</Radio>
                        <Radio value={FormatType.Base64}>Base64</Radio>
                    </Radio.Group>
                </Space>
                <Input.TextArea
                    rows={4}
                    placeholder={t("Ciphertext")}
                    readOnly={true}
                    value={cipherText}
                />
            </Space>
        </>
    );
};

export default HmacMD5;
