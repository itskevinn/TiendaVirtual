import { Usuario } from './../../../models/usuario';
import { ProductoService } from './../../../services/producto.service';
import { Component, Input, OnInit } from '@angular/core';
import { Producto } from 'src/app/models/producto';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-producto-consulta',
  templateUrl: './producto-consulta.component.html',
  styleUrls: ['./producto-consulta.component.css']
})
export class ProductoConsultaComponent implements OnInit {
  productos: Producto[] = []
  @Input() claseColumnas?: string;
  columnas: string
  usuario: Usuario

  constructor(private productoService: ProductoService,) { }

  ngOnInit(): void {
    this.consultarProductos();
    this.validarClaseColumnas();
    this.consultarUsuario();
  }
  consultarUsuario() {
    this.usuario = JSON.parse(localStorage.getItem('usuarioLoggeado'));
  }
  consultarProductos() {
    this.productoService.gets()
      .subscribe(r => this.productos = r);
  }
  validarClaseColumnas() {
    if (this.claseColumnas) {
      this.columnas = this.claseColumnas;
    }
    else {
      this.columnas = "row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3"
    }
  }
}
