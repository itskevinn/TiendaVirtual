import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-venta-registro',
  templateUrl: './venta-registro.component.html',
  styleUrls: ['./venta-registro.component.css']
})
export class VentaRegistroComponent implements OnInit {

  constructor(private location: Location) { }

  ngOnInit(): void {
  }
  volver() {
    this.location.back();
  }
}
