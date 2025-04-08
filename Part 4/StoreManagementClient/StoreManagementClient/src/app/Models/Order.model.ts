import { OrderProduct } from "./OrderProduct.model";

export enum OrderStatus {
    Pending = 0,
    InProcess = 1,
    Completed = 2
}


export interface Order {
    id: number;
    status: OrderStatus;
    supplierId: number;
    orderproducts: OrderProduct[];
}