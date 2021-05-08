import { ProductoService } from 'src/app/services/producto.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Producto } from 'src/app/models/producto';
import { DetalleService } from 'src/app/services/detalle.service';
import { Detalle } from 'src/app/models/detalle';
import { Usuario } from 'src/app/models/usuario';

@Component({
  selector: 'app-producto-detalle',
  templateUrl: './producto-detalle.component.html',
  styleUrls: ['./producto-detalle.component.css']
})
export class ProductoDetalleComponent implements OnInit {
  id = this.rutaActiva.snapshot.params.id;
  producto: Producto;
  usuario: Usuario;
  detalles: Detalle[]
  constructor(private rutaActiva: ActivatedRoute, private router: Router, private productoService: ProductoService, private detalleService: DetalleService) { }

  ngOnInit(): void {
    this.obtenerUsuario();
    this.id = this.rutaActiva.snapshot.params.id;
    this.buscarProducto(this.id);
  }
  buscarProducto(id: string) {
    this.productoService.get(id).subscribe((u) => {
      if (u != null) {
        this.producto = u;
      }
      else {
        alert("No se encontró el producto");
        this.router.navigate(['/Productos'])
      }
    })
    this.detalleService.getsByProducto(id).subscribe((d) => {
      this.detalles = d;
    })
  }
  obtenerUsuario() {
    this.usuario = JSON.parse(localStorage.getItem('usuarioLoggeado'))
  }
}
