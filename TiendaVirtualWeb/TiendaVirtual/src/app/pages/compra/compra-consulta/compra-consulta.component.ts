import { Factura } from './../../../models/factura';
import { Component, OnInit } from '@angular/core';
import { FacturaService } from 'src/app/services/factura.service';
import { Usuario } from 'src/app/models/usuario';

@Component({
  selector: 'app-compra-consulta',
  templateUrl: './compra-consulta.component.html',
  styleUrls: ['./compra-consulta.component.css']
})
export class CompraConsultaComponent implements OnInit {
  facturas: Factura[] = []
  usuario: Usuario;
  constructor(private facturaService: FacturaService) { }

  ngOnInit(): void {
    this.facturaService.getsByType("compra").subscribe((f) => {
      this.facturas = f;
    });
  }

}
