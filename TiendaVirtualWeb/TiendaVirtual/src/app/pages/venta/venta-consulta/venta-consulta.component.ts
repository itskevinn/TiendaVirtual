import { FacturaService } from './../../../services/factura.service';
import { Factura } from './../../../models/factura';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-venta-consulta',
  templateUrl: './venta-consulta.component.html',
  styleUrls: ['./venta-consulta.component.css']
})
export class VentaConsultaComponent implements OnInit {
  facturas: Factura[] = [];
  constructor(private facturaService: FacturaService) { }

  ngOnInit(): void {
    this.facturaService.getsByType("Venta").subscribe((f) => {
      this.facturas = f;
      console.log(f);
    })
  }

}
