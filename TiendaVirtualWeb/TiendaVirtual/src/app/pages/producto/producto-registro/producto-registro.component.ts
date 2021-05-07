import { Producto } from './../../../models/producto';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductoService } from 'src/app/services/producto.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-producto-registro',
  templateUrl: './producto-registro.component.html',
  styleUrls: ['./producto-registro.component.css']
})
export class ProductoRegistroComponent implements OnInit {

  producto: Producto;
  formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder, private productoService: ProductoService, private messageService: MessageService) {

  }
  ngOnInit(): void {
    this.producto = new Producto()
    this.crearFormulario()
  }
  crearFormulario() {
    this.producto.id = ''
    this.producto.nombre = ''
    this.producto.descripcion = '';
    this.producto.cantidad = null;
    this.producto.descuento = null;
    this.producto.iva = null;
    this.producto.nitProveedor = '';
    this.producto.precio = null;
    this.formGroup = this.formBuilder.group({
      id: [this.producto.id, Validators.required],
      nombre: [this.producto.nombre, Validators.required],
      nitProveedor: [this.producto.nitProveedor, Validators.required],
      cantidad: [this.producto.cantidad, Validators.required],
      descuento: [this.producto.descuento, Validators.required],
      iva: [this.producto.iva, Validators.required],
      precio: [this.producto.precio, Validators.required],
      descripcion: [this.producto.descripcion, Validators.required],
    })
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
    this.producto = this.formGroup.value;
    this.productoService.post(this.producto).subscribe((r) => {
      if (r != null) {
        this.producto = r;
        this.formGroup.reset();
        this.mostrarToast("Registro exitoso", "Producto registrado exitosamente", "success");
      }
    });
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.messageService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }

}