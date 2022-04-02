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

const SHA1 = () => {
    const { t } = useTranslation("SafetyService");
    const [originText, setOriginText] = useState("");
    const [cipherText, setCipherText] = useState("");
    const [autoUpdate, setAutoUpdate] = useState<boolean>(true);
    const [codeType, setCodeType] = useState<FormatType>(FormatType.Text);
    const [outCodeType, setOutCodeType] = useState<FormatType>(FormatType.Hex);
    const handleOutput = useCallback(() => {
        textEncodeMd5(originText);
    }, [originText, codeType]);

    useEffect(() => {
        if (autoUpdate) {
            textEncodeMd5(originText);
        }
    }, [originText, codeType, autoUpdate, outCodeType]);

    const textEncodeMd5 = (text: string) => {
        try {
            if (text == "") {
                setCipherText("");
            } else {
                const result = CryptoJS.SHA1(
                    codeType == FormatType.Hex
                        ? CryptoJS.enc.Hex.parse(text)
                        : text
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
            <Typography.Paragraph>
                SHA-1（英语：Secure Hash Algorithm
                1，中文名：安全散列算法1）是一种密码散列函数，美国国家安全局设计，并由美国国家标准技术研究所（NIST）发布为联邦资料处理标准（FIPS）。SHA-1可以生成一个被称为消息摘要的160位（20字节）散列值，散列值通常的呈现形式为40个十六进制数。
            </Typography.Paragraph>
            <Typography.Paragraph>
                2005年，密码分析人员发现了对SHA-1的有效攻击方法，这表明该算法可能不够安全，不能继续使用，自2010年以来，许多组织建议用SHA-2或SHA-3来替换SHA-1。Microsoft、Google以及Mozilla都宣布，它们旗下的浏览器将在2017年停止接受使用SHA-1算法签名的SSL证书。
            </Typography.Paragraph>
            <Typography.Paragraph>
                2017年2月23日，CWI
                Amsterdam与Google宣布了一个成功的SHA-1碰撞攻击，发布了两份内容不同但SHA-1散列值相同的PDF文件作为概念证明。
            </Typography.Paragraph>
            <Typography.Paragraph>
                2020年，针对SHA-1的选择前缀冲突攻击已经实际可行。建议尽可能用SHA-2或SHA-3取代SHA-1。
            </Typography.Paragraph>
            <Typography.Paragraph>
                -- 来自
                <Typography.Link
                    href={"https://zh.wikipedia.org/wiki/SHA-1"}
                    target={"_blank"}
                    rel={"nofollow noopener noreferrer"}
                >
                    维基百科
                </Typography.Link>
            </Typography.Paragraph>
            <Divider />
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
                <Input.TextArea
                    rows={4}
                    placeholder={t("OriginalArticle")}
                    value={originText}
                    onChange={(e) => setOriginText(e.target.value)}
                    allowClear={true}
                />
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

export default SHA1;
