import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../Models/Product.model'; // נתיב למודל המוצר

@Injectable({
    providedIn: 'root'
})
export class ProductService {

    private apiUrl = 'https://localhost:7057/api/products'; 

    constructor(private http: HttpClient) { }

    getAllProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(this.apiUrl);
    }

    getProductById(id: number): Observable<Product> {
        const url = `${this.apiUrl}/${id}`;  
        return this.http.get<Product>(url);  
    }

    addProduct(product: Product): Observable<Product> {
        return this.http.post<Product>(this.apiUrl, product);
    }

}
