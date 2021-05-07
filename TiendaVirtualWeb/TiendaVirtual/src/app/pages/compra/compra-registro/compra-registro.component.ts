import { FacturaService } from './../../../services/factura.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Factura } from './../../../models/factura';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-compra-registro',
  templateUrl: './compra-registro.component.html',
  styleUrls: ['./compra-registro.component.css']
})
export class CompraRegistroComponent implements OnInit {

  factura: Factura;
  formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder, private facturaService: FacturaService, private messageService: MessageService) {

  }
  ngOnInit(): void {
    this.factura = new Factura()
    this.crearFormulario()
  }
  crearFormulario() {
    
  }
  onSubmit() {
    if (this.formGroup.invalid) {
      this.mostrarToast("Aviso", "Complete todos los datos", "info");

    } else {
      this.registrar();
    }
  }

  get control() {
    return this.formGroup.controls;
  }

  registrar() {
    this.factura = this.formGroup.value;
    this.facturaService.post(this.factura).subscribe((r) => {
      if (r != null) {
        this.factura = r;
        this.formGroup.reset();
        this.mostrarToast("Registro exitoso", "Factura registrada exitosamente", "success");
      }
    });
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.messageService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }

}
