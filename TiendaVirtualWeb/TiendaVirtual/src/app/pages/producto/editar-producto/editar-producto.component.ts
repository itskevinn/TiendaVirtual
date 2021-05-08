import { Router, ActivatedRoute } from '@angular/router';
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

  id = this.rutaActiva.snapshot.params.id;
  producto: Producto;
  formGroup: FormGroup;
  constructor(private router: Router, private rutaActiva: ActivatedRoute, private formBuilder: FormBuilder, private productoService: ProductoService, private messageService: MessageService) {

  }
  ngOnInit(): void {
    this.producto = new Producto()
    this.id = this.rutaActiva.snapshot.params.id;
    this.crearFormulario()
    this.buscar();
  }

  buscar() {
    this.productoService.get(this.id).subscribe((p) => {
      console.log(p);
      if (p != null) {
        this.actualizarForm(p);
      }
      else {
        alert('No se encontró el producto');
        this.router.navigate(['/Inicio'])
      }
    })
  }
  actualizarForm(p: Producto) {
    this.control.id.setValue(p.id);
    this.control.nombre.setValue(p.nombre);
    this.control.descripcion.setValue(p.descripcion);
    this.control.precioBase.setValue(p.precioBase);
    this.control.cantidadDisponible.setValue(p.cantidadDisponible);
    this.control.nitProveedor.setValue(p.nitProveedor);
    this.control.descuento.setValue(p.descuento);
    this.control.iva.setValue(p.iva);
  }
  crearFormulario() {
    this.producto.id = ''
    this.producto.nombre = ''
    this.producto.descripcion = '';
    this.producto.cantidadDisponible = null;
    this.producto.descuento = null;
    this.producto.iva = null;
    this.producto.nitProveedor = '';
    this.producto.precioBase = null;
    this.formGroup = this.formBuilder.group({
      id: [this.producto.id, Validators.required],
      nombre: [this.producto.nombre, Validators.required],
      nitProveedor: [this.producto.nitProveedor, Validators.required],
      cantidadDisponible: [{value: this.producto.cantidadDisponible, disabled: true}, Validators.required],
      descuento: [this.producto.descuento, Validators.required],
      iva: [this.producto.iva, Validators.required],
      precioBase: [this.producto.precioBase, Validators.required],
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
