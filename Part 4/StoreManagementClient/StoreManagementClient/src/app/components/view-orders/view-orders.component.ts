import { Component, OnInit } from '@angular/core';
import { Order, OrderStatus } from '../../Models/Order.model';
import { OrderService } from '../../services/order.service';
import { CommonModule } from '@angular/common';
import { SupplierService } from '../../services/supplier.service';

@Component({
  selector: 'app-view-orders',
  imports: [CommonModule],
  templateUrl: './view-orders.component.html',
  styleUrls: ['./view-orders.component.css']
})
export class ViewOrdersComponent implements OnInit {
  userID: number = 1; 
  isManager: boolean = false;
  ordersToDisplay: Order[] = [];
  loading = false;
  error: string | null = null;
  managerViewMode: 'pending' | 'all' = 'pending'; 
  orderStatusEnum = OrderStatus;

  constructor(private orderService: OrderService, private supplierService: SupplierService) { }

  ngOnInit(): void {
    if(this.userID==0){
      this.isManager = true;
    }
    // this.isManager = this.userID === 0;
    this.loadOrders();
  }

  loadOrders(): void {
    this.loading = true;
    this.error = null;

    if (this.isManager) {
      if (this.managerViewMode === 'pending') {
        this.orderService.getAllOrders().subscribe({
          next: (data) => {
            this.ordersToDisplay = data.filter(order => order.status === OrderStatus.InProcess); 
            this.loading = false;
          },
          error: (error) => {
            this.error = 'שגיאה בטעינת הזמנות ממתינות לאישור מנהל.';
            console.error('שגיאה בטעינת הזמנות ממתינות לאישור מנהל', error);
            this.loading = false;
          }
        });
      } else {
        this.orderService.getAllOrders().subscribe({
          next: (data) => {
            this.ordersToDisplay = data;
            this.loading = false;
          },
          error: (error) => {
            this.error = 'שגיאה בטעינת כל ההזמנות.';
            console.error('שגיאה בטעינת כל ההזמנות', error);
            this.loading = false;
          }
        });
      }
    } else {
      this.orderService.getOrdersBySupplierId(this.userID).subscribe({
        next: (data) => {
          this.ordersToDisplay = data;
          this.loading = false;
        },
        error: (error) => {
          this.error = 'שגיאה בטעינת ההזמנות של הספק.';
          console.error('שגיאה בטעינת ההזמנות של הספק', error);
          this.loading = false;
        }
      });
    }
  }

  approveSupplierOrder(orderId: number): void {
    this.loading = true;
    this.error = null;
    this.orderService.approveBySupplier(orderId).subscribe({
      next: (response: Order) => {
        console.log(`הזמנה מספר ${orderId} אושרה על ידי הספק.`, response);
        alert(`הזמנה מספר ${orderId} אושרה על ידי הספק בהצלחה.`);
        this.loadOrders(); 
        this.loading = false;
      },
      error: (error: Error) => {
        this.error = `שגיאה באישור הזמנה מספר ${orderId} על ידי הספק: ${error.message}`;
        console.error(`שגיאה באישור הזמנה מספר ${orderId} על ידי הספק`, error);
        alert(this.error);
        this.loading = false;
      }
    });
  }

  approveStoreOwnerOrder(orderId: number): void {
    this.loading = true;
    this.error = null;
    this.orderService.approveByStoreOwner(orderId).subscribe({
      next: (updatedOrder) => {
        console.log(`הזמנה מספר ${orderId} אושרה כהושלמה על ידי המנהל.`, updatedOrder);
        alert(`הזמנה מספר ${orderId} אושרה כהושלמה על ידי המנהל בהצלחה.`);
        this.loadOrders(); 
        this.loading = false;
      },
      error: (error) => {
        this.error = `שגיאה באישור הזמנה מספר ${orderId} על ידי המנהל: ${error.message}`;
        console.error(`שגיאה באישור הזמנה מספר ${orderId} על ידי המנהל`, error);
        alert(this.error);
        this.loading = false;
      }
    });
  }

  setManagerViewMode(mode: 'pending' | 'all'): void {
    this.managerViewMode = mode;
    this.loadOrders();
  }

  canApproveSupplierOrder(order: Order): boolean {
    return !this.isManager && order.status === OrderStatus.Pending;
  }

  canApproveStoreOwnerOrder(order: Order): boolean {
    return this.isManager && order.status === OrderStatus.InProcess;
  }
}

