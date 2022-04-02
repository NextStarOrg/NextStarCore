import React, { ReactNode } from "react";
import { Tooltip } from "antd";
import { QuestionCircleOutlined } from "@ant-design/icons";
import { TooltipPlacement } from "antd/es/tooltip";

export type IQuestionTooltipProps = {
    /**
     * @description 提示文字
     * @default -
     */
    title: ReactNode | (() => ReactNode),
    /**
     * @description 箭头是否指向目标元素中心
     * @default false
     */
    arrowPointAtCenter?: boolean,
    /**
     * @description 气泡被遮挡时自动调整位置
     * @default true
     */
    autoAdjustOverflow?: boolean,
    /**
     * @description 背景颜色
     * @default -
     */
    tooltipColor?: string,
    /**
     * @description icon颜色
     * @default -
     */
    color?: string,
    /**
     * @description 鼠标移入后延时多少才显示 Tooltip，单位：秒
     * @default 0.1
     */
    mouseEnterDelay?: number,
    /**
     * @description 鼠标移出后延时多少才隐藏 Tooltip，单位：秒
     * @default 0.1
     */
    mouseLeaveDelay?: number,
    /**
     * @description 气泡框位置，可选 top left right bottom topLeft topRight bottomLeft bottomRight leftTop leftBottom rightTop rightBottom
     * @default top
     */
    placement?: TooltipPlacement,
    /**
     * @description 触发行为，可选 hover | focus | click | contextMenu，可使用数组设置多个触发行为
     * @default top
     */
    trigger?:TooltipTrigger | TooltipTrigger[],
}

type TooltipTrigger = "hover" | "focus" | "click" | "contextMenu";

const QuestionTooltip = (props: IQuestionTooltipProps) => {
    return (
        <Tooltip title={props.title}
                 arrowPointAtCenter={props.arrowPointAtCenter}
                 autoAdjustOverflow={props.autoAdjustOverflow}
                 color={props.tooltipColor}
                 mouseEnterDelay={props.mouseEnterDelay}
                 mouseLeaveDelay={props.mouseLeaveDelay}
                 placement={props.placement}
                 trigger={props.trigger}>
            <QuestionCircleOutlined style={{color:props.color}} />
        </Tooltip>
    );
};

export default QuestionTooltip;
