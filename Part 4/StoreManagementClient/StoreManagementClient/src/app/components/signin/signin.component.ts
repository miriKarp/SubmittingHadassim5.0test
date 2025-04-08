import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { SupplierService } from '../../services/supplier.service';
import { Supplier } from '../../Models/Supplier.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent {
  password!: string;
  errorMessage: string | null = null;

  constructor(private supplierService: SupplierService, private router: Router) { }

  signin(): void {
    if (this.password === 'ADMINE') {
      localStorage.setItem('userId', '0');
      this.router.navigate(['Order']);
      return;
    }

    this.supplierService.getAllSuppliers().subscribe({
      next: (suppliers: Supplier[]) => {
        const loggedInSupplier = suppliers.find(
          s => s.password === this.password
        );

        if (loggedInSupplier) {
          localStorage.setItem('supplierId', loggedInSupplier.id?.toString() || '');
          this.router.navigate(['/View']);
        } else {
          this.errorMessage = 'הסיסמה שהוזנה אינה תואמת לספק קיים. אנא נסה שוב או גש להרשמה.';
        }
      },
      error: (error) => {
        this.errorMessage = 'שגיאה בכניסה.';
        console.error('שגיאה בכניסה', error);
      }
    });
  }
}




// import { Component } from '@angular/core';
// import { Router, RouterLink } from '@angular/router';
// import { SupplierService } from '../../services/Supplier.Service';
// import { Supplier } from '../../Models/Supplier.model';
// import { FormsModule } from '@angular/forms';

// @Component({
//     selector: 'app-signin',
//     standalone: true,
//     imports: [FormsModule, RouterLink], 
//     templateUrl: './signin.component.html',
//     styleUrls: ['./signin.component.css']
// })
// export class SigninComponent {
//     identifier!: string;
//     password!: string;
//     errorMessage: string | null = null;

//     constructor(private supplierService: SupplierService, private router: Router) { }

//     signin(): void {
//         if (this.password === 'ADMINE') {
//             localStorage.setItem('userId', '0');
//             this.router.navigate(['Order']);
//             return;
//         }
//         this.supplierService.getAllSuppliers().subscribe({
//             next: (suppliers: Supplier[]) => {
//                 const loggedInSupplier = suppliers.find(
//                     s => s.password === this.password
//                 );

//                 if (loggedInSupplier) {
//                     localStorage.setItem('supplierId', loggedInSupplier.id?.toString() || '');
//                     this.router.navigate(['/View']);
//                 } else {
//                     this.errorMessage = 'שם משתמש או סיסמה שגויים.';
//                 }
//             },
//             error: (error) => {
//                 this.errorMessage = 'שגיאה בכניסה.';
//                 console.error('שגיאה בכניסה', error);
//             }
//         });
//     }
// }
