import { LoginService } from 'src/app/services/login.service';
import { Usuario } from './../../models/usuario';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  usuario: Usuario;
  constructor(private router: Router, private loginService: LoginService) { }

  ngOnInit(): void {
    this.usuario = JSON.parse(localStorage.getItem('usuarioLoggeado'));
  }
  cerrarSesion() {
    this.loginService.logout();
    this.router.navigate(['/Login']);
    
    this.recargar();
  }
  recargar() {
    window.location.reload();
  }
}
