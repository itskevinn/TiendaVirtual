import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Producto } from './../models/producto';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

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
}
