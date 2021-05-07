import { Producto } from './producto';
export class Detalle {
  cantidad: number;
  total: number;
  descontado: number;
  descuento: number;
  idProducto: string;
  idFactura: number;
  producto: Producto;
  precioBase: number;
  iva: number;
  subTotal: number;
}
