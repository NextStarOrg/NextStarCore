import React from "react";
import {Card, Row,Col} from "antd";
import {NavLink} from "react-router-dom";
import {GenerateServiceMenus, SafetyServiceMenus} from "assets/consts/MenuAboutName";
import {useTranslation} from "react-i18next";

const Dashboard = () => {
    const {t} = useTranslation();
    return <div style={{padding:"20px"}}>
        <Row>
            {GenerateServiceMenus.map((x,index)=>{
                if(index==0) return <></>;
                return <Col span={6} style={{padding:"0 10px"}}>
                    <NavLink to={x.path}>
                        <Card title={t(`${x.name}`)} hoverable={true} style={{width: "100%", height: "100%"}}>
                            <p>{t(`${x.description}`)}</p>
                        </Card>
                    </NavLink>
                </Col>
            })}

        </Row>
    </div>
};

export default Dashboard;
