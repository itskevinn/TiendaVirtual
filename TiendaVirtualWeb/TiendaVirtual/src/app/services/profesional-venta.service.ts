import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ProfesionalVenta } from '../models/profesionalVenta';

@Injectable({
  providedIn: 'root'
})
export class ProfesionalVentaService {
  constructor(private http: HttpClient) { }
  baseUrl = environment.url;
  post(profesional: ProfesionalVenta): Observable<ProfesionalVenta> {
    return this.http.post<ProfesionalVenta>(this.baseUrl + '/ProfesionalVenta', profesional).pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
  gets(): Observable<ProfesionalVenta[]> {
    return this.http.get<ProfesionalVenta[]>(this.baseUrl + '/ProfesionalVenta').pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
  get(id: string): Observable<ProfesionalVenta> {
    var url = `${this.baseUrl + '/ProfesionalVenta'}/${id}`;
    return this.http.get<ProfesionalVenta>(url).pipe((
      tap((r) => {
        console.log(r);
      })
    ))
  }
}
