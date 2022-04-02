declare namespace React {
    // eslint-disable-next-line
    function lazy<T extends ComponentType<any>>(
        factory: () => Promise<{ default: T }>
    ): T;
}
