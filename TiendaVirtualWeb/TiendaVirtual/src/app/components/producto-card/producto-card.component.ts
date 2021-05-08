import { Usuario } from './../../models/usuario';
import { MensajeService } from './../../services/mensaje.service';
import { Producto } from './../../models/producto';
import { Component, Input, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-producto-card',
  templateUrl: './producto-card.component.html',
  styleUrls: ['./producto-card.component.css']
})
export class ProductoCardComponent implements OnInit {
  @Input() producto: Producto
  usuario: Usuario
  constructor(private mensajeService: MensajeService, private location: Location) { }

  ngOnInit(): void {
    this.usuario = JSON.parse(localStorage.getItem('usuarioLoggeado'))
  }
  validarRuta(){
    if (this.location.isCurrentPathEqualTo("/RegistrarCompra") || this.location.isCurrentPathEqualTo("/RegistrarVenta")) {
      return true;
    }
  }
  agregarAlCarrito() {
    this.mensajeService.enviarMensaje(this.producto);
  }

}
