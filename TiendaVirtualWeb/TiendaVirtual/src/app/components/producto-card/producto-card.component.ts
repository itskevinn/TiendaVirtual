import { MensajeService } from './../../services/mensaje.service';
import { Producto } from './../../models/producto';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-producto-card',
  templateUrl: './producto-card.component.html',
  styleUrls: ['./producto-card.component.css']
})
export class ProductoCardComponent implements OnInit {
  @Input() producto: Producto
  constructor(private mensajeService: MensajeService) { }

  ngOnInit(): void {
  }
  agregarAlCarrito() {
    this.mensajeService.enviarMensaje(this.producto);
  }
}
