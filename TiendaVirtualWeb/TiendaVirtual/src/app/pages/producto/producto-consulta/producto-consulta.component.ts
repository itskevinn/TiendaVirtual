import { ProductoService } from './../../../services/producto.service';
import { Component, OnInit } from '@angular/core';
import { Producto } from 'src/app/models/producto';

@Component({
  selector: 'app-producto-consulta',
  templateUrl: './producto-consulta.component.html',
  styleUrls: ['./producto-consulta.component.css']
})
export class ProductoConsultaComponent implements OnInit {
  productos: Producto[] = []
  constructor(private productoService: ProductoService) { }

  ngOnInit(): void {
    this.consultarProductos();
  }
  consultarProductos() {
    this.productoService.gets()
      .subscribe(r => this.productos = r);

  }
}
