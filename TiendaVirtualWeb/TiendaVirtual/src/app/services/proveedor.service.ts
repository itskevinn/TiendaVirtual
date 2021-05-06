import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Proveedor } from '../models/proveedor';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProveedorService {
  baseUrl: string = environment.url;
  constructor(private http: HttpClient) { }
  post(proveedor: Proveedor): Observable<Proveedor> {
    return this.http.post<Proveedor>(this.baseUrl + '/Proveedor', proveedor).pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
  gets(): Observable<Proveedor[]> {
    return this.http.get<Proveedor[]>(this.baseUrl + '/Proveedor').pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
}
