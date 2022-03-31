export enum UUIDType {
    v1 = 1 << 1,
    v3 = 1 << 3,
    v4 = 1 << 4,
    v5 = 1 << 5,
}

export interface tabInfo {
    title: string;
    description: string;
    type: UUIDType;
}

export const tabListInfo: tabInfo[] = [
    {
        title: "uuid v4",
        description: "由加密强度高的随机值创建",
        type: UUIDType.v4,
    },
    {
        title: "uuid v5",
        description: "由用户提供的名称和名称空间字符串创建",
        type: UUIDType.v5,
    },
    {
        title: "uuid v1",
        description: "从系统时钟创建（加上随机值）",
        type: UUIDType.v1,
    },
    {
        title: "uuid v3",
        description: "与版本5类似，但哈希算法较差",
        type: UUIDType.v3,
    },
];
