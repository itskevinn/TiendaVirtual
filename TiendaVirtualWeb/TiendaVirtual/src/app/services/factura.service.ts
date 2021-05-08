import { environment } from './../../environments/environment';
import { Factura } from './../models/factura';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FacturaService {
  baseUrl: string;
  constructor(private http: HttpClient) {
    this.baseUrl = environment.url;
  }
  post(factura: Factura): Observable<Factura> {
    return this.http.post<Factura>(this.baseUrl + '/Factura', factura).pipe(
      tap((f) => {
        console.log(f);
      })
    );
  }
  gets(): Observable<Factura[]> {
    return this.http.get<Factura[]>(this.baseUrl + '/Factura').pipe(
      tap((f) => {
        console.log(f);
      })
    );
  }
  getsByType(tipo: string): Observable<Factura[]> {
    return this.http.get<Factura[]>(`${this.baseUrl + "/Factura/PorTipo"}/${tipo}`).pipe(
      tap((f) => {
        console.log(f);
      })
    )
  }
}
