import { LoginService } from 'src/app/services/login.service';
import { Component } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { Usuario } from './models/usuario';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TiendaVirtual';
  usuario: Usuario;
  ingreso = false;
  constructor(private primengConfig: PrimeNGConfig, private loginService: LoginService) {
    this.loginService.usuarioLoggeado.subscribe(
      (x) => (this.usuario = x)
    );
    if (this.usuario) {
      this.ingreso = true;
    }
  }

  ngOnInit() {
    this.primengConfig.ripple = true;
  }
}
