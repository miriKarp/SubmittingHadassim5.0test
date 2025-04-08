import { Order } from "./Order.model";
import { SupplierProduct } from "./SupplierProduct.model";

export interface Supplier {
  id: number;
  companyName: string;
  representativeName: string;
  phoneNumber: string;
  password: string;
  supplierProducts: SupplierProduct[];
  orders: Order[];
}