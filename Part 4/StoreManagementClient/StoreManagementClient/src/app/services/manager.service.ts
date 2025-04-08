import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Manager } from '../Models/Manager.model';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

    private apiUrl = 'https://localhost:7057/api/Manager'; 

  constructor(private http: HttpClient) { }

  getManager(): Observable<Manager> {
    return this.http.get<Manager>(`${this.apiUrl}/GetManager`);
  }

  updateManager(manager: Manager): Observable<Manager> {
    return this.http.put<Manager>(`${this.apiUrl}/UpdateManager`, manager);
  }

  addManager(manager: Manager): Observable<Manager> {
    return this.http.post<Manager>(`${this.apiUrl}/AddManager`, manager);
  }
}
