import React from "react";
import {Button, Result} from "antd";
import {Link} from "react-router-dom";
import ForbiddenIcon from "assets/images/403.svg";

const Forbidden = () => {
    return (
        <section>
            <Result
                icon={<img style={{width: "30%"}} src={ForbiddenIcon}/>}
                subTitle={"暂无授权"}
                extra={
                    <Button type='primary'>
                        <Link to={"/"}>返回首页</Link>
                    </Button>
                }
            />
        </section>
    );
};

export default Forbidden;
