import { LiderAvaluo } from './../models/liderAvaluo';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LiderAvaluoService {
  constructor(private http: HttpClient) { }
  baseUrl = environment.url;
  post(liderAvaluo: LiderAvaluo): Observable<LiderAvaluo> {
    return this.http.post<LiderAvaluo>(this.baseUrl + '/LiderAvaluo', liderAvaluo).pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
  gets(): Observable<LiderAvaluo[]> {
    return this.http.get<LiderAvaluo[]>(this.baseUrl + '/LiderAvaluo').pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
  get(id: string): Observable<LiderAvaluo> {
    var url = `${this.baseUrl + '/LiderAvaluo'}/${id}`;
    return this.http.get<LiderAvaluo>(url).pipe((
      tap((r) => {
        console.log(r);
      })
    ))
  }
}
