module cef.store.userDashbord.controls.orders.interfaces {
    export interface IPosition {
        name: string;
        titleKey: string;
        placeholderKey: string;
        hidden?: boolean;
        combineWithNext?: boolean;
        default?: string;
        defaultFirst?: boolean,
        defaultLast?: boolean,
        defaultBy?: (x: IOption | IStaticOption, that) => boolean;
        staticOptions?: IStaticOption[];
    }
}
