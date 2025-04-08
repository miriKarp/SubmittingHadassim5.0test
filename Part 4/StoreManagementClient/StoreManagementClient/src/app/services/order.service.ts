import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../Models/Order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'https://localhost:7057/api/Orders';

  constructor(private http: HttpClient) { }

  createOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}/CreateOrder`, order);
  }
  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/GetAllOrders`);
  }

  getOrdersBySupplierId(supplierId: number): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/GetOrdersBySupplierId/${supplierId}`);
  }

  getOrderById(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/GetOrderById/${id}`);
  }
  getCurrentOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/GetAllOrders`);
  }

  approveBySupplier(id: number): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/approveBySupplier/${id}`, {});
  }

  approveByStoreOwner(id: number): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/approveByStoreOwner/${id}`, {});
  }

  approveOrder(orderId: number): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/approveByStoreOwner/${orderId}`, {});
  }
}
