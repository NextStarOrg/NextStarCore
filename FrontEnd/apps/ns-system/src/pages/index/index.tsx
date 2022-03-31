import React from "react";
import { useHistory } from "react-router-dom";
import { Row, Col, Card } from "antd";

import { layoutCardList } from "./index.module";

const Index = () => {
    const history = useHistory();
    function handleClick(path: string) {
        history.push(path);
    }
    return (
        <div>
            <Row gutter={16}>
                {layoutCardList.map((item, index) => {
                    return (
                        <Col span={6}>
                            <Card
                                hoverable
                                style={{ width: 240 }}
                                cover={<img alt={item.title} src={item.img} />}
                                onClick={() => {
                                    handleClick(item.path);
                                }}>
                                <Card.Meta
                                    title={item.title}
                                    description={item.description}
                                />
                            </Card>
                        </Col>
                    );
                })}
            </Row>
        </div>
    );
};

export default Index;
