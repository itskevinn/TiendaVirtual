import { Detalle } from './detalle';
export class Factura {
  detalles: Detalle[];
  idInteresado: string;
  idFactura: number;
  subTotal: number;
  descuentoTotal: number;
  tipo: string;
  ivaTotal: number;
  total: number;
}
