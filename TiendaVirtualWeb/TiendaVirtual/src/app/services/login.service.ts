import { Login } from './../models/login';
import { environment } from './../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, tap } from 'rxjs/operators';
import { Usuario } from '../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  baseUrl: string;
  private usuarioLoggeadoSubject: BehaviorSubject<Usuario>;
  public usuarioLoggeado: Observable<Usuario>;
  constructor(private http: HttpClient) {
    this.usuarioLoggeadoSubject = new BehaviorSubject<Usuario>(JSON.parse(localStorage.getItem('usuarioLoggeado')));
    this.usuarioLoggeado = this.usuarioLoggeadoSubject.asObservable();
    this.baseUrl = environment.url
  }
  public get usuarioLoggeadoValor(): Usuario {
    return this.usuarioLoggeadoSubject.value;
  }
  logout() {
    localStorage.removeItem("usuarioLoggeado");
    this.usuarioLoggeadoSubject.next(null);
  }
  login(login: Login): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/Login', login).pipe(
      map((l) => {
        if (l) {
          localStorage.setItem('usuarioLoggeado', JSON.stringify(l));
          this.usuarioLoggeadoSubject.next(l);
        }
        return l;
      })
    );
  }
}
