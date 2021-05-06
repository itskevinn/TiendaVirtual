import { ProveedorService } from './../../../services/proveedor.service';
import { Component, OnInit } from '@angular/core';
import { Proveedor } from 'src/app/models/proveedor';

@Component({
  selector: 'app-proveedor-consulta',
  templateUrl: './proveedor-consulta.component.html',
  styleUrls: ['./proveedor-consulta.component.css']
})
export class ProveedorConsultaComponent implements OnInit {
  proveedores: Proveedor[] = []
  constructor(private proveedorService: ProveedorService) { }

  ngOnInit(): void {
    this.consultarProveedores();
  }
  consultarProveedores() {
    this.proveedorService.gets().subscribe((p) => {
      this.proveedores = p;
      console.log(p);
    })
  }
}
