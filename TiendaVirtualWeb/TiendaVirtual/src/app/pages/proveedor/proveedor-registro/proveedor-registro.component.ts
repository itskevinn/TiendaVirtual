import { environment } from 'src/environments/environment';
import { MessageService } from 'primeng/api';
import { ProveedorService } from './../../../services/proveedor.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Proveedor } from 'src/app/models/proveedor';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-proveedor-registro',
  templateUrl: './proveedor-registro.component.html',
  styleUrls: ['./proveedor-registro.component.css']
})
export class ProveedorRegistroComponent implements OnInit {
  proveedor: Proveedor;
  tipo: string;
  tipoDocumentos: string[] = ["Cédula", "Nit", "Cédula de Extranjería", "Pasaporte"];
  formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder, private proveedorService: ProveedorService, private messageService: MessageService) {

  }
  ngOnInit(): void {

    this.proveedor = new Proveedor();
    this.crearFormulario();
  }
  crearFormulario() {
    this.proveedor.tipoDocumento = ''
    this.proveedor.documento = ''
    this.proveedor.nombre = ''
    this.proveedor.productos = [];
    this.formGroup = this.formBuilder.group({
      documento: [this.proveedor.documento, Validators.required],
      nombre: [
        this.proveedor.nombre,
        Validators.required,
      ],
    })
  }
  cambiarTipo(e) {
    this.tipo = e.target.value;
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
    this.proveedor = this.formGroup.value;
    this.proveedor.tipoDocumento = this.tipo;
    console.log(this.proveedor);
    this.proveedorService.post(this.proveedor).subscribe((r) => {
      if (r != null) {
        this.proveedor = r;
        this.formGroup.reset();
        this.mostrarToast("Registro exitoso", "Proveedor registrado exitosamente", "success");
      }
    });
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.messageService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }

}
