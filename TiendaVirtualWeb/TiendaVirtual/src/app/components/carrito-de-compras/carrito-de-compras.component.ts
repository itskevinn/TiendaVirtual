import { FacturaService } from './../../services/factura.service';
import { Factura } from './../../models/factura';
import { MessageService } from 'primeng/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Producto } from './../../models/producto';
import { Detalle } from './../../models/detalle';
import { MensajeService } from '../../services/mensaje.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-carrito-de-compras',
  templateUrl: './carrito-de-compras.component.html',
  styleUrls: ['./carrito-de-compras.component.css']
})
export class CarritoDeComprasComponent implements OnInit {
  @Input() tipo: string;
  detalles: Detalle[] = [];
  factura: Factura;
  total: number
  detalle: Detalle;
  productos: Producto[] = [];
  idInteresado: string;
  descuento: number;
  cantidades: number[];
  subTotalDetalle: number;
  descuentoDetalle: number;
  ivaDetalle: number;
  formGroup: FormGroup;
  constructor(private facturaService: FacturaService, private mensajeService: MensajeService, private formBuilder: FormBuilder, private modalService: MessageService) { }

  ngOnInit(): void {
    console.log(this.tipo);
    this.mensajeService.recibirMensaje().subscribe((producto: Producto) => {
      var producto = producto;
      if (this.validarProductoRegistrado(producto.id) == false) {
        this.detalle = {
          idProducto: producto.id,
          cantidad: null,
          idFactura: 0,
          valorDescontado: 0,
          valorIva: 0,
          descuento: 0,
          producto: producto,
          total: this.total,
          subTotal: this.subTotalDetalle,
          descontado: this.descuentoDetalle,
          precioBase: 0,
          iva: this.ivaDetalle
        }
        this.detalle.descuento = this.detalle.descuento == 0 ? this.detalle.producto.descuento : this.detalle.descuento;
        this.detalle.precioBase = this.detalle.precioBase == 0 ? this.detalle.producto.precioBase : this.detalle.precioBase;
        console.log(this.detalle);
        this.calcularTotalDetalle();
        this.detalles.push(this.detalle);
      }
      else this.mostrarToast("Aviso", "El producto ya está en el carrito, modifique su cantidad", "info")
      return;
    })

  }
  validarProductoRegistrado(id: string) {
    var registrado = false;
    this.detalles.forEach(d => {
      if (d.idProducto == id) {
        registrado = true;
      }
    });
    return registrado;
  }
  validarCantidadDetalles() {
    this.detalles.forEach((d) => {
      if (d.cantidad == 0) {
        return true;
      }
    })
    return false;
  }
  validarIdInteresado() {
    if (this.factura.idInteresado == "") {
      return true;
    }
    return false;
  }
  finalizarCompra() {
    if (this.validarCantidadDetalles() === false) {
      this.factura = new Factura();
      this.factura.detalles = this.detalles;
      this.factura.tipo = this.tipo;
      if (this.tipo == "compra") {
        this.factura.idInteresado = JSON.parse(localStorage.getItem("usuarioLoggeado")).NombreUsuario;
      }
      if (this.tipo == "venta") {
        this.factura.idInteresado = this.idInteresado;
      }
      this.factura.total = this.calcularTotalFactura();
      this.factura.descuentoTotal = this.calcularTotalDescuentoFactura();
      this.factura.ivaTotal = this.calcularTotalIvaFactura();
      this.factura.subTotal = this.calcularSubTotalFactura();
      console.log(this.factura);

      this.facturaService.post(this.factura).subscribe((f) => {
        console.log(f);
        if (f.tipo.toLowerCase() == "venta") {
          this.mostrarToast("Venta realizada", "Venta realizada con éxito", "success")
        }
        else {
          this.mostrarToast("Compra realizada", "Compra realizada con éxito", "success")
        }
        this.limpiarInputs();
      })
    }
    else {
      this.mostrarToast("Error", "Falta una cantidad en los productos agregados", "Error");
    }
  };
  limpiarInputs() {
    this.factura = new Factura();
    this.detalles = [];
  }

  validarCambio(e) {
    this.calcularTotalDetalle();
    this.calcularSubTotalFactura();
    this.calcularTotalFactura();
    this.calcularTotalIvaFactura();
    this.calcularTotalDescuentoFactura();
  }
  onSubmit() {
    if (this.formGroup.invalid) {
      this.mostrarToast("Aviso", "Complete todos los datos", "info");

    } else {
      this.finalizarCompra();
    }
  }
  calcularSubTotalFactura() {
    let subTotal = 0;
    this.detalles.forEach(d => {
      subTotal += d.subTotal;
    });
    return subTotal;
  }
  calcularTotalFactura() {
    let total = 0;
    this.detalles.forEach(d => {
      total += d.total;
    });
    return total;
  }
  calcularTotalDescuentoFactura() {
    let descuento = 0;
    this.detalles.forEach(d => {
      descuento += d.descontado;
    })
    return descuento;
  }
  calcularTotalIvaFactura() {
    let iva = 0;
    this.detalles.forEach(d => {
      iva += d.iva;
    })
    return iva;
  }

  calcularIvaDetalle() {
    this.detalle.iva = this.detalle.precioBase * (this.detalle.producto.iva / 100);
    this.detalle.iva = this.detalle.iva * this.detalle.cantidad;
  }
  calcularDescuentoDetalle() {
    this.detalle.descontado = this.detalle.precioBase * (this.detalle.descuento / 100);
    this.detalle.descontado = this.detalle.descontado * this.detalle.cantidad;
  }
  calcularSubTotalDetalle() {
    this.detalle.subTotal = this.detalle.precioBase * this.detalle.cantidad;
  }
  calcularTotalDetalle() {
    this.calcularSubTotalDetalle();
    this.calcularDescuentoDetalle();
    this.calcularIvaDetalle();
    this.detalle.total = this.detalle.subTotal - this.detalle.descontado + this.detalle.iva;
    console.log("SUBTOTAL: " + this.detalle.subTotal);
    console.log("IVA: " + this.detalle.iva);
    console.log("DESCUENTO: " + this.detalle.descontado);
    console.log(this.detalle.total);
    if (this.detalle.total < 0) {
      this.detalle.total = 0
      return;
    }
  }
  get control() {
    return this.formGroup.controls;
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.modalService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }
  eliminarDetalle(idProducto: string) {
    let index: number;
    index = this.detalles.findIndex(d => d.idProducto === idProducto);
    this.detalles.splice(index, 1);
  }
}
