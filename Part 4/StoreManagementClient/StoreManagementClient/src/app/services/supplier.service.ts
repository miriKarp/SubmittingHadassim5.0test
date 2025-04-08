import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Supplier } from '../Models/Supplier.model';

@Injectable({
    providedIn: 'root'
})
export class SupplierService {

    private apiUrl = 'https://localhost:7057/api/Supplier'; 

    constructor(private http: HttpClient) { }

    getAllSuppliers(): Observable<Supplier[]> {
        return this.http.get<Supplier[]>(`${this.apiUrl}/GetAllSuppliers`);
    }

    getSupplierById(id: number): Observable<Supplier> {
        const url = `${this.apiUrl}/GetSupplierById${id}`; 
        return this.http.get<Supplier>(url);
      }

    AddSupplier(supplier: Supplier): Observable<Supplier> {
        return this.http.post<Supplier>(`${this.apiUrl}/CreateSupplier`, supplier);
    }


}