import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SupplierService } from '../../services/supplier.service';
import { OrderService } from '../../services/order.service';
import { Supplier } from '../../Models/Supplier.model';
import { OrderProduct } from '../../Models/OrderProduct.model';
import { Order, OrderStatus } from '../../Models/Order.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-place-order',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './place-order.component.html',
  styleUrl: './place-order.component.css'
})
export class PlaceOrderComponent implements OnInit {
  suppliers: Supplier[] = [];
  selectedSupplier?: Supplier;
  orderForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private supplierService: SupplierService,
    private orderService: OrderService
  ) { }

  ngOnInit(): void {
    this.supplierService.getAllSuppliers().subscribe(s => {
      console.log('Suppliers:', s);
      this.suppliers = s;
    });

    this.orderForm = this.fb.group({
      supplierId: [null, Validators.required],
      orderItems: this.fb.array([])
    });
  }

  get orderItems(): FormArray {
    return this.orderForm.get('orderItems') as FormArray;
  }
  onSupplierSelectedById(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const supplierId = Number(target.value);
    const supplier = this.suppliers.find(s => s.id === supplierId);

    if (supplier) {
      this.selectedSupplier = supplier;
      this.orderForm.get('supplierId')?.setValue(supplier.id);
      this.orderItems.clear();   // Clear previous order items if any

      if (supplier.supplierProducts && Array.isArray(supplier.supplierProducts)) {
        supplier.supplierProducts.forEach(p => {
          this.orderItems.push(this.fb.group({
            productId: [p.productId],
            productName: [p.productName],
            pricePerItem: [p.pricePerItem],
            quantity: [p.minOrderQuantity, [Validators.required, Validators.min(p.minOrderQuantity)]]
          }));
        });
      } else {
        console.error('No products available for the selected supplier');
      }
    }
  }
  submitOrder(): void {
    if (this.orderForm.invalid) {
      this.orderForm.markAllAsTouched();
      return;
    }

    const order: Order = {
      id: 0,
      status: OrderStatus.Pending,
      supplierId: this.orderForm.value.supplierId,
      orderproducts: this.orderForm.value.orderItems.map((item: OrderProduct) => ({
        productId: item.productId,
        productName: item.productName,
        quantity: item.quantity,
        pricePerItem: item.pricePerItem
      }))
    };

    this.orderService.createOrder(order).subscribe(() => {
      alert('ההזמנה נשלחה בהצלחה!');
      this.orderForm.reset();
      this.selectedSupplier = undefined;
      this.orderItems.clear();
    }, (error) => {
      console.error('שגיאה בשליחת ההזמנה:', error);
      alert('אירעה שגיאה בשליחת ההזמנה.');
    });
  }}

  