import { Detalle } from './detalle';
export class Factura {
  detalles: Detalle[];
  idInteresado: string;
  idFactura: number;
  subTotal: number;
  totalDescuento: number;
  totalIva: number;
  total: number;
}
