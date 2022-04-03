import React from 'react';
import {RouterAboutConfig} from "assets/consts/RouterAboutName";
import {Navigate} from "react-router-dom";

const BlogIndex = () => {
    return (
        <Navigate to={RouterAboutConfig.BlogService.Dashboard.Path} replace={true}/>
    )
}

export default BlogIndex
