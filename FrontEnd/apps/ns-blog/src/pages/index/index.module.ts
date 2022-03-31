import gitImage from "assets/images/git.png";
import uuidImage from "assets/images/uuid.png";
import randomStringImage from "assets/images/randomString.png";

export interface layoutCardItem {
    img: string;
    title: string;
    description: string;
    path: string;
}

export const layoutCardList: layoutCardItem[] = [
    {
        img: gitImage,
        title: "Git 标签和commit",
        description: "用于规范化commit和label",
        path: "/git",
    },
    {
        img: uuidImage,
        title: "uuid生成",
        description: "uuid生成，包含四个版本",
        path: "/uuid",
    },
    {
        img: randomStringImage,
        title: "随机字符串生成",
        description: "随机字符串生成，多种格式，自定义",
        path: "/randomString",
    },
];
