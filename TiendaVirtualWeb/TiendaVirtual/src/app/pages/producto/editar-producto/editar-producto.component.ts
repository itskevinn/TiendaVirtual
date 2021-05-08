import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Producto } from 'src/app/models/producto';
import { ProductoService } from 'src/app/services/producto.service';

@Component({
  selector: 'app-editar-producto',
  templateUrl: './editar-producto.component.html',
  styleUrls: ['./editar-producto.component.css']
})
export class EditarProductoComponent implements OnInit {


  producto: Producto;
  formGroup: FormGroup;
  constructor(private location: Location, private formBuilder: FormBuilder, private productoService: ProductoService, private messageService: MessageService) {

  }
  ngOnInit(): void {
    this.producto = new Producto()
    this.crearFormulario()
  }
  volver() {
    this.location.back();
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
      descripcion: [this.producto.descripcion],
    })
  }
  onSubmit() {
    if (this.formGroup.invalid) {
      this.mostrarToast("Aviso", "Complete todos los datos", "info");

    } else {
      this.editar();
    }
  }

  get control() {
    return this.formGroup.controls;
  }

  editar() {
    this.producto = this.formGroup.value;
    this.productoService.put(this.producto).subscribe((r) => {
      if (r != null) {
        this.producto = r;
        this.formGroup.reset();
        this.mostrarToast("Edición exitosa", "Producto editado exitosamente", "success");
      }
    });
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.messageService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }

}
