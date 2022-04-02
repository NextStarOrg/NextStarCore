/**
 * This file includes utils for env/runtime detect
 * suchs as browser version, platform. host..
 */

import { trim } from 'lodash';

export const isDev: boolean = process.env.NODE_ENV !== 'production';

/**
 * If your app is served from a sub-directory on your server, you’ll want to set this to the sub-directory.
 * eg: iis vd path.
 * @returns
 */
export const getRelativeBasePath = (): string => {
    return trim(process.env.PUBLIC_URL || '', '/');
};

export const getHostPath = (): string => {
    let iisVDPath = getRelativeBasePath();
    if (iisVDPath) {
        iisVDPath = `/${iisVDPath}`;
    }
    return `${window.location.protocol}//${window.location.hostname}${iisVDPath}${
        window.location.port ? `:${window.location.port}` : ''
    }`;
};
