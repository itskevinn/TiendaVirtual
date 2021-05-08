import { Usuario } from './../models/usuario';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AutentificacionVentasGuard implements CanActivate {
  usuario: Usuario = JSON.parse(localStorage.getItem('usuarioLoggeado'));
  /**
   *
   */
  constructor(private router: Router) {

  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (!this.usuario) {
      this.router.navigate(['/Login'])
      return true;
    }
    if (this.usuario.rol == "Profesional de Ventas") {
      return true;
    }
    return false;

  }

}
