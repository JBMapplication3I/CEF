module cef.store.userDashbord.controls.orders.interfaces {
    export interface IOption {
        value: string;
        data: {
            titleKey: string;
            options?: IOption[];
        };
    }
}
