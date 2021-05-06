import { Usuario } from './../../../models/usuario';
import { Component, OnInit } from '@angular/core';
import { UsuarioService } from 'src/app/services/usuario.service';

@Component({
  selector: 'app-usuario-consulta',
  templateUrl: './usuario-consulta.component.html',
  styleUrls: ['./usuario-consulta.component.css']
})
export class UsuarioConsultaComponent implements OnInit {
  textoABuscar: string;
  usuarios: Usuario[] = [];
  constructor(private usuarioService: UsuarioService) { }

  ngOnInit(): void {
    this.consultarUsuarios();
  }
  consultarUsuarios() {
    this.usuarioService.gets().subscribe((u) => {
      this.usuarios = u;
    })
  }
}
