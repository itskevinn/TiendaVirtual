import { Rol } from './../models/rol';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RolService {

  baseUrl: string = environment.url;
  constructor(private http: HttpClient) { }
  post(rol: Rol): Observable<Rol> {
    return this.http.post<Rol>(this.baseUrl + '/Rol', rol).pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
  gets(): Observable<Rol[]> {
    return this.http.get<Rol[]>(this.baseUrl + '/Rol').pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
}
