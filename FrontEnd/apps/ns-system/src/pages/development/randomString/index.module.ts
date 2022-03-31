export enum tabType {
    hex = 1 << 1,
    base64 = 1 << 3,
    urlSafe = 1 << 4,
    numeric = 1 << 5,
    distinguishable = 1 << 6,
    custome = 1 << 7,
}

export interface tabInfo {
    title: string;
    description: string;
    type: tabType;
}

export const tabListInfo: tabInfo[] = [
    {
        title: "hex",
        description: "十六进制",
        type: tabType.hex,
    },
    {
        title: "base64",
        description: "由base64组合",
        type: tabType.base64,
    },
    {
        title: "url-safe",
        description: "url安全类型组合",
        type: tabType.urlSafe,
    },
    {
        title: "numeric",
        description: "数字组合",
        type: tabType.numeric,
    },
    {
        title: "distinguishable",
        description: "比较容易辨别的 => CDEHKMPRTUWXY012458",
        type: tabType.distinguishable,
    },
    {
        title: "custome",
        description: "自定义字符组合",
        type: tabType.custome,
    },
];
