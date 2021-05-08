import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment.prod';
import { Injectable } from '@angular/core';
import { Detalle } from '../models/detalle';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DetalleService {
  baseUrl: string
  constructor(private http: HttpClient) {
    this.baseUrl = environment.url;
  }
  getsByProducto(id: string): Observable<Detalle[]> {
    var url = `${this.baseUrl + '/Detalle'}/${id}`;
    return this.http.get<Detalle[]>(url).pipe((
      tap((d) => {
        console.log(d);
      })
    ))
  }
}
