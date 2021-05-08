import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Producto } from './../models/producto';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
const httpOptionsPut = {
  headers: new HttpHeaders({ "Content-Type": "application/json" }),
  responseType: "text",
};
@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  baseUrl: string = environment.url;
  constructor(private http: HttpClient) { }
  post(producto: Producto): Observable<Producto> {
    return this.http.post<Producto>(this.baseUrl + '/Producto', producto).pipe(
      tap((p) => {
        console.log(p);
      })
    )
  }
  gets(): Observable<Producto[]> {
    return this.http.get<Producto[]>(this.baseUrl + '/Producto').pipe(
      tap((p) => {
        console.log(p);
      })
    )
  }
  put(producto: Producto): Observable<Producto> {
    const url = `${this.baseUrl + '/Producto'}/${producto.id}`
    return this.http.put<Producto>(url, producto).pipe(
      tap((f) => {
        console.log(f);
      })
    )
  }
  delete(producto: Producto): Observable<Producto> {
    const id = typeof producto === "string" ? producto : producto.id;
    console.log(id);
    return this.http.delete<Producto>(`${this.baseUrl + '/Producto'}/${id}`).pipe(
      tap((f) => {
        console.log(f);
      })
    )
  }
}
