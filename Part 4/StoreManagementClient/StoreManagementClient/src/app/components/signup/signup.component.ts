import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Supplier } from '../../Models/Supplier.model';
import { SupplierProduct } from '../../Models/SupplierProduct.model';
import { SupplierService } from '../../services/supplier.service';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, RouterModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit {
  supplierForm: FormGroup;
  products: SupplierProduct[] = [];
  newProductForm: FormGroup;

  constructor(private fb: FormBuilder, private supplierService: SupplierService) {
    this.supplierForm = this.fb.group({
      companyName: ['', Validators.required],
      representativeName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.newProductForm = this.fb.group({
      productName: ['', Validators.required],
      pricePerItem: [0, [Validators.required, Validators.min(0.01)]],
      minOrderQuantity: [0, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void { }

  addProduct(): void {
    if (this.newProductForm.valid) {
      const newProduct: SupplierProduct = this.newProductForm.value;
      this.products.push(newProduct);
      this.newProductForm.reset();
    }
  }

  onSubmit(): void {
    if (this.supplierForm.valid) {
      const supplier: Supplier = {
        id: 0,
        companyName: this.supplierForm.value.companyName,
        representativeName: this.supplierForm.value.representativeName,
        phoneNumber: this.supplierForm.value.phoneNumber,
        password: this.supplierForm.value.password,
        supplierProducts: this.products.map(p => ({
          productId: 0,
          productName: p.productName,
          pricePerItem: p.pricePerItem,
          minOrderQuantity: p.minOrderQuantity
        })),
        orders: []
      };

      console.log(supplier);

      this.supplierService.AddSupplier(supplier).subscribe(
        response => {
          console.log('Supplier saved successfully!', response);
          alert('Supplier saved successfully!');
          this.supplierForm.reset();
          this.products = [];
        },
        error => {
          console.error('Error saving supplier', error);
        }
      );
    }
  }
}
