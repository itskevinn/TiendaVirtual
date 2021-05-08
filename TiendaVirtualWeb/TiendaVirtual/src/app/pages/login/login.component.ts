import { Login } from './../../models/login';
import { Usuario } from './../../models/usuario';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;
  usuario: Usuario;
  loginValues: Login;
  constructor(private loginService: LoginService, private router: Router) {
    if (this.loginService.usuarioLoggeadoValor) {
      this.router.navigate(['/Inicio']);
    }
  }

  ngOnInit(): void {
    this.usuario = new Usuario();
    this.loginValues = new Login();
  }
  iniciarSesion() {
    this.loginService.login(this.loginValues).pipe(first()).subscribe((l) => {
      if (localStorage.getItem('usuarioLoggeado')) {
        this.recargar()
      }
    }
    );
  }
  recargar() {
    window.location.reload();
  }
  recuperarUsuario() {
    this.usuario = JSON.parse(localStorage.getItem('usuarioLoggeado'));
    if (this.usuario != null) {
      return true;
    }
  }
}
