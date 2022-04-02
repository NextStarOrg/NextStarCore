import React from "react";
import { Button, Result } from "antd";
import { Link } from "react-router-dom";
import NotFoundIcon from "assets/images/404.svg";

const NotFound = () => {
    return (
        <section>
            <Result
                icon={<img style={{ width: "30%" }} src={NotFoundIcon} />}
                extra={
                    <Button type='primary'>
                        <Link to={"/"}>返回首页</Link>
                    </Button>
                }
            />
        </section>
    );
};

export default NotFound;
