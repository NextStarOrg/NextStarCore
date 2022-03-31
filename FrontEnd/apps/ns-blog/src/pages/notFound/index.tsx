import React from "react";
import { useHistory } from "react-router-dom";
import { Result, Button } from "antd";

const Index = () => {
    const history = useHistory();
    function backHome() {
        history.push(window._REACT_BASE_URL);
    }
    return (
        <div>
            <Result
                status='404'
                title='404'
                subTitle='Sorry, the page you visited does not exist.'
                extra={
                    <Button type='primary' onClick={backHome}>
                        Back Home
                    </Button>
                }
            />
        </div>
    );
};

export default Index;
