import { catchError, tap } from 'rxjs/operators';
import { environment } from './../../environments/environment';
import { Usuario } from './../models/usuario';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  constructor(private http: HttpClient) { }
  baseUrl = environment.url;
  post(usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(this.baseUrl + '/Usuario', usuario).pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
  gets(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(this.baseUrl + '/Usuario').pipe(
      tap((r) => {
        console.log(r);
      })
    )
  }
}
